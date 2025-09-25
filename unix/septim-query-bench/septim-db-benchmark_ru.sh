#!/bin/bash

# ==============================================================================
# RU: ЧТО ДЕЛАЕТ ЭТОТ СКРИПТ И ПОЧЕМУ
# ------------------------------------------------------------------------------
# Назначение
# - Для трёх баз (wd_926_db / wdn_926_db / wds_926_db) снимает метрики
#   производительности и формирует снапшоты данных по запросу:
#       SELECT * FROM admin_septim.OBECNEREPORTY_KONZUMACEUCTENKA_V
#       WHERE DATUMCAS >= DATE '2025-05-01';
#
# Выводимые артефакты (в $OUTDIR):
# - ${tag}_explain_cold.json — «холодный» план с EXPLAIN (ANALYZE, BUFFERS, JSON)
# - ${tag}_explain_hot.json  — «горячий» план (второй прогон того же запроса)
# - ${tag}_count.txt         — COUNT(*) результата (для проверки количества строк)
# - ${tag}_snapshot.csv      — снапшот данных: два столбца
#       row_hash  — SHA-256 хэш строки (row_to_json) для сравнения между БД
#       payload   — сама строка в JSON (точная фиксация данных)
#
# Что создаётся (в $OUTDIR, имена ФИКСИРОВАННЫЕ — предыдущие прогоны перезаписываются):
# - wd_explain_cold.json   wdn_explain_cold.json   wds_explain_cold.json
# - wd_explain_hot.json    wdn_explain_hot.json    wds_explain_hot.json
# - wd_count.txt           wdn_count.txt           wds_count.txt
# - wd_snapshot.csv        wdn_snapshot.csv        wds_snapshot.csv
#
# Почему есть «cold» и «hot»
# - Перед съёмом планов выполняем DISCARD PLANS/SEQUENCES, затем два прогона:
#   первый прогревает кэш/план, второй даёт «горячие» числа (hot). Это
#   позволяет понять эффект прогрева и сравнить стабильную производительность.
#
# Почему EXPLAIN сохраняется как «чистый» JSON
# - Включены настройки psql (\timing off, \pset tuples_only on, \pset footer off),
#   чтобы в файлы не попадали «Time: …», «(1 row)» и рамки; EXPLAIN → чистый JSON.
#
# Зачем нужен snapshot (row_hash, payload)
# - row_hash позволяет быстро сравнивать результаты между версиями БД без
#   зависимости от порядка строк. payload хранит точный JSON каждой строки
#   для выборочной проверки различий.
#
# Требования/предпосылки
# - На сервере PostgreSQL должен быть доступен pgcrypto (digest).
# - Пользователь/роль должны иметь права выполнять:
#     admin_core.X_SESSIONPREPARE(), X_SESSIONLOGIN(...),
#     EXPLAIN, SELECT на используемых объектах.
# - Директория $OUTDIR должна существовать и быть доступной для записи.
#
# Что важно учесть
# - \copy — это метакоманда psql (без «;» в конце строки).
# - Путь к файлу в \copy подставляется оболочкой (heredoc без одинарных кавычек).
# - Холодный/горячий прогоны относятся к серверным буферам PostgreSQL; кэш ОС
#   отдельно не сбрасывается (это нормально для сравнения версий/планов).
#
# Как адаптировать под себя
# - Поменяйте фильтр по датам/представление — в двух местах EXPLAIN и в \copy.
# - При необходимости уберите X_SESSION... если у вас другая модель аутентификации.
# - OUTDIR и список баз (map tag→dbname) задаются в начале скрипта.
#
# Как передать запрос
# - По умолчанию используется запрос по умолчанию, зашитый в скрипте.
# - Либо передайте SQL одной строкой:
#     ./run_bench.sh "SELECT * FROM schema.view WHERE col >= DATE '2025-05-01';"
# - Либо из файла:
#     ./run_bench.sh -f /path/to/query.sql
# Скрипт сам уберёт финальную «;» там, где запрос идёт как подзапрос (COUNT/снапшот),
# и добавит «;» там, где нужна законченная команда (EXPLAIN).
#
# Содержимое файлов
# - *_explain_cold.json / *_hot.json — «чистый» EXPLAIN JSON без мусора (psql-настройки:
#   \timing off, \pset tuples_only on, \pset footer off, \pset format unaligned).
# - *_count.txt — COUNT(*) результата.
# - *_snapshot.csv — две колонки:
#       row_hash  — SHA-256 от row_to_json(row) для сравнения наборов строк;
#       payload   — исходная строка в JSON (для детального diff).
#
# Почему «холодный» и «горячий»
# - Перед замером делаем DISCARD PLANS/SEQUENCES и запускаем запрос дважды:
#   первый прогревает кэш/план, второй показывает «горячие» цифры.
#   Это позволяет честно сравнивать стабильную производительность.
#
# Требования
# - На сервере должна быть доступна extension pgcrypto (digest()).
# - Роль должна уметь выполнять: admin_core.X_SESSIONPREPARE(),
#   X_SESSIONLOGIN(...), EXPLAIN и SELECT на нужных объектах.
# - $OUTDIR должен существовать и быть доступен на запись.
#
# Важные детали
# - \copy — метакоманда psql (без «;» на конце).
# - SQL подставляется как текст через psql-переменные; передавайте доверенные запросы.
# - «Холодный/горячий» — про буферы PostgreSQL; кэш ОС отдельно не сбрасывается —
#   этого достаточно для сравнения версий и планов.
#
# Где смотреть итоговую сводку
# - Используйте анализатор (например, septim-bench-report.sh), который парсит JSON/COUNT/CSV
#   и печатает: cold_ms, hot_ms, rows_plan, rows_real, buffers и сравнение данных.
# ==============================================================================

set -euo pipefail

SCRIPT_NAME="$(basename "$0")"
SCRIPT_VERSION="v2 (built on 2025-09-25)"

echo "== ${SCRIPT_NAME} ${SCRIPT_VERSION} =="

if [[ "${1:-}" == "--version" || "${1:-}" == "-V" ]]; then
  echo "${SCRIPT_NAME} v${SCRIPT_VERSION}"
  exit 0
fi

OUTDIR="/home/mikhailchm/septim-query-bench/report"
mkdir -p "$OUTDIR"

# --- разбор аргументов: запрос строкой или из файла (-f) ---
QUERY_RAW=""
if [[ $# -eq 0 ]]; then
  # дефолт: твой прежний запрос
  QUERY_RAW="SELECT * FROM admin_septim.OBECNEREPORTY_KONZUMACEUCTENKA_V WHERE DATUMCAS >= DATE '2025-05-01';"
elif [[ "$1" == "-f" ]]; then
  [[ -f "${2:-}" ]] || { echo "No such file: $2" >&2; exit 1; }
  QUERY_RAW="$(cat "$2")"
else
  # всё, что передали — считаем SQL (с пробелами)
  QUERY_RAW="$*"
fi

# нормализация: уберём финальную ';' для подзапросов и добавим там, где нужно целиком
QUERY_NOSEMI="$(printf '%s' "$QUERY_RAW" | sed -e 's/[[:space:]]\+$//' -e 's/;[[:space:]]*$//')"
QUERY_SEMI="${QUERY_NOSEMI};"

# список баз и их короткие теги для файлов (имена файлов — БЕЗ меток)
declare -A DBS=(
  [wd]="wd_926_db"
  [wdn]="wdn_926_db"
  [wds]="wds_926_db"
)

for tag in wd wdn wds; do
  db="${DBS[$tag]}"
  f_explain_cold="$OUTDIR/${tag}_explain_cold.json"
  f_explain_hot="$OUTDIR/${tag}_explain_hot.json"
  f_count="$OUTDIR/${tag}_count.txt"
  f_snapshot="$OUTDIR/${tag}_snapshot.csv"

  echo "== $db ($tag) =="

  # -X чтобы не подхватывать .psqlrc
  psql -X "dbname=$db" \
       -v ON_ERROR_STOP=1 \
       -v q_semi="$QUERY_SEMI" \
       -v q_nosemi="$QUERY_NOSEMI" <<SQL
\timing off
\pset pager off
\pset footer off
\pset tuples_only on
\x off
\pset format unaligned

create extension if not exists pgcrypto;

select admin_core.X_SESSIONPREPARE();
select X_SESSIONLOGIN('admin', 'aswpha', 'asw', a_BEGINSESSION := true);

DISCARD PLANS;
DISCARD SEQUENCES;

\o $f_explain_cold
EXPLAIN (ANALYZE, BUFFERS, FORMAT JSON)
:q_semi
\o

\o $f_explain_hot
EXPLAIN (ANALYZE, BUFFERS, FORMAT JSON)
:q_semi
\o

\o $f_count
SELECT COUNT(*) AS rows_cnt
FROM (
  :q_nosemi
) AS t;
\o

\pset tuples_only off
\pset footer on
\timing on

COPY (
  SELECT
    encode(digest(row_to_json(t)::text,'sha256'),'hex') AS row_hash,
    row_to_json(t)                                      AS payload
  FROM (
    :q_nosemi
  ) AS t
) TO STDOUT WITH (FORMAT CSV, HEADER true)\g $f_snapshot
SQL

done

echo "Готово. Файлы в $OUTDIR:"
ls -lh "$OUTDIR"
