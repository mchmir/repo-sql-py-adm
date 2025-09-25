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
# ==============================================================================

set -euo pipefail

OUTDIR="/home/mikhailchm/1115"
mkdir -p "$OUTDIR"

# список баз и их короткие теги для файлов
declare -A DBS=(
  [wd]="wd_926_db"
  [wdn]="wdn_926_db"
  [wds]="wds_926_db"
)

for tag in "${!DBS[@]}"; do
  db="${DBS[$tag]}"
  f_explain_cold="$OUTDIR/${tag}_explain_cold.json"
  f_explain_hot="$OUTDIR/${tag}_explain_hot.json"
  f_count="$OUTDIR/${tag}_count.txt"
  f_snapshot="$OUTDIR/${tag}_snapshot.csv"

  echo "== $db ($tag) =="

  # -X чтобы не подхватывать .psqlrc (и избежать стартовых ошибок)
  psql -X "dbname=$db" -v ON_ERROR_STOP=1 <<SQL
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
SELECT *
FROM admin_septim.OBECNEREPORTY_KONZUMACEUCTENKA_V
WHERE DATUMCAS >= DATE '2025-05-01';
\o

\o $f_explain_hot
EXPLAIN (ANALYZE, BUFFERS, FORMAT JSON)
SELECT *
FROM admin_septim.OBECNEREPORTY_KONZUMACEUCTENKA_V
WHERE DATUMCAS >= DATE '2025-05-01';
\o

\o $f_count
SELECT COUNT(*) AS rows_cnt
FROM admin_septim.OBECNEREPORTY_KONZUMACEUCTENKA_V
WHERE DATUMCAS >= DATE '2025-05-01';
\o

\pset tuples_only off
\pset footer on
\timing on

create extension if not exists pgcrypto;

\set snapfile '$f_snapshot'
\copy (SELECT encode(digest(row_to_json(t)::text,'sha256'),'hex') AS row_hash, row_to_json(t) AS payload FROM admin_septim.OBECNEREPORTY_KONZUMACEUCTENKA_V t WHERE datumcas >= DATE '2025-05-01') TO $f_snapshot CSV HEADER
SQL

done

echo "Готово. Файлы в $OUTDIR:"
ls -lh "$OUTDIR"
