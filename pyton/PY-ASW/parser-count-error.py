# -*- coding: utf-8 -*-
"""
Статистика по логам proctest (без аргументов CLI, все настройки ниже).

Собирает:
1) "какая ошибка в каком .tp и сколько раз" (по строкам "ERROR NNN: ..." рядом с FILE:).
2) "сколько ошибок в каждом файле" из строки "Test finished with N errors."
3) ДОБАВЛЕНО: сводка по папкам (папка берётся из <END: .../Proc/<FOLDER>/<file>.tp>).
"""

from __future__ import annotations
from pathlib import Path
from collections import defaultdict, Counter
from typing import Dict, List, Tuple, Optional
import re
import csv

# ======================= НАСТРОЙКИ =======================
# Папка с *.log
LOG_DIR = r"D:\Temp\test LOG"

# Маска имён логов
LOG_GLOB = "*.log"

# Префикс имён логов вида "_PgSQL_Proc_<...>_<filebase>.tp.log"
# Нужен, чтобы восстановить нормальное имя .tp из имени лога.
LOG_NAME_PREFIX = "_PgSQL_Proc_"

# Сохранять отчёты?
WRITE_CSV = True
WRITE_TSV = False

# Имена выходных файлов (кладутся в LOG_DIR)
CSV_ERRORS_BY_FILE = "errors_by_file.csv"   # file,error,count
CSV_FILE_TOTALS    = "file_totals.csv"      # file,total_errors
# ДОБАВЛЕНО — по папкам:
CSV_FOLDER_TOTALS  = "folder_totals.csv"    # folder,total_errors
CSV_FOLDER_FILES   = "folder_files.csv"     # folder,file,total_errors

TSV_ERRORS_BY_FILE = "errors_by_file.tsv"
TSV_FILE_TOTALS    = "file_totals.tsv"
# ДОБАВЛЕНО — по папкам:
TSV_FOLDER_TOTALS  = "folder_totals.tsv"
TSV_FOLDER_FILES   = "folder_files.tsv"

# Окно (в строках) для поиска FILE: рядом с ERROR:
LOOKAHEAD = 6
# =========================================================


# ---------- Регэкспы ----------
RE_ERROR = re.compile(r"^ERROR\s+(?P<code>\d+)\s*:\s*(?P<msg>.*)$", re.IGNORECASE)
RE_FILE  = re.compile(r"^FILE\s*:\s*(?P<file>.+?\.tp)\s*$", re.IGNORECASE)
RE_FIN   = re.compile(r"Test\s+finished\s+with\s+(?P<n>\d+)\s+errors\.", re.IGNORECASE)
# <END: /.../Septim/PgSQL/Proc/CRM/casoveuseky.tp>
RE_END   = re.compile(r"<END:\s*(?P<path>[^>]+)>\s*$", re.IGNORECASE)


def read_text_any(p: Path) -> str:
    """Чтение файла с частыми кодировками (UTF-8 (+BOM), cp1251, cp1250, latin-1)."""
    data = p.read_bytes()
    if data.startswith(b"\xEF\xBB\xBF"):
        return data.decode("utf-8-sig")
    for enc in ("utf-8", "cp1251", "cp1250", "latin-1"):
        try:
            return data.decode(enc)
        except UnicodeDecodeError:
            pass
    return data.decode("utf-8", errors="replace")


def guess_tp_from_logname(log_name: str) -> Optional[str]:
    """
    Из имени лога типа _PgSQL_Proc_<folder>_<filebase>.tp.log вернуть "<filebase>.tp".
    Берём токен перед ".tp.log" — допускаем подчёркивания в имени файла.
    """
    base = Path(log_name).name
    if not base.lower().startswith(LOG_NAME_PREFIX.lower()):
        return None
    m = re.search(r"_(?P<filebase>[^.]+)\.tp\.log$", base, re.IGNORECASE)
    return (m.group("filebase") + ".tp") if m else None


def extract_folder_from_end(lines: List[str]) -> Optional[str]:
    """
    Ищем последнюю строку <END: .../Proc/<FOLDER>/<file>.tp> и берём <FOLDER>.
    Поддерживаем / и \\, регистр 'Proc' игнорируем.
    """
    for ln in reversed(lines):
        m = RE_END.search(ln)
        if not m:
            continue
        p = m.group("path").strip()
        s = p.replace("\\", "/")
        s_low = s.lower()
        pos = s_low.rfind("/proc/")
        if pos == -1:
            continue
        after = s[pos + len("/proc/"):]  # "<FOLDER>/file.tp" или "<FOLDER>/.../file.tp"
        folder = after.split("/", 1)[0].strip()
        if folder:
            return folder
    return None


def parse_one_log(path: Path) -> Tuple[str, Optional[str], Dict[str, int], Optional[int]]:
    """
    Разбор одного .log
    :return:
      tp_name          — "нормальное" имя .tp (best effort)
      folder_name      — имя папки из <END: .../Proc/<FOLDER>/...> (или None)
      per_error_counts — {"ERROR 001: ... || FILE:<name>.tp": count}
      finished_total   — число из "Test finished with N errors." (или None)
    """
    text = read_text_any(path)
    lines = text.splitlines()

    # Папка из <END: ...>
    folder_name = extract_folder_from_end(lines)

    # Кандидаты имён .tp, встречающиеся в логе
    file_mentions = [m.group("file").strip()
                     for m in (RE_FILE.match(ln) for ln in lines)
                     if m]
    tp_name = Counter(file_mentions).most_common(1)[0][0] if file_mentions else None
    if not tp_name:
        tp_name = guess_tp_from_logname(path.name) or path.stem  # запасной вариант

    # Собираем ERROR и привязываем к ближайшему FILE: (вперёд в пределах LOOKAHEAD)
    per_error_counts: Dict[str, int] = defaultdict(int)
    for idx, ln in enumerate(lines):
        m_err = RE_ERROR.match(ln)
        if not m_err:
            continue
        err_text = f"ERROR {m_err.group('code').strip()}: {m_err.group('msg').strip()}"

        owner_file = None
        # Смотрим вперёд
        for j in range(idx + 1, min(idx + 1 + LOOKAHEAD, len(lines))):
            m_file = RE_FILE.match(lines[j])
            if m_file:
                owner_file = m_file.group("file").strip()
                break
        # Если не нашли — берём общий tp_name
        if not owner_file:
            owner_file = tp_name

        key = f"{err_text} || FILE:{Path(owner_file).name}"
        per_error_counts[key] += 1

    # Последнее "Test finished with N errors." — это итог по лог-файлу
    finished_total: Optional[int] = None
    for ln in reversed(lines):
        m_fin = RE_FIN.search(ln)
        if m_fin:
            finished_total = int(m_fin.group("n"))
            break

    return Path(tp_name).name, folder_name, per_error_counts, finished_total


def main() -> None:
    log_dir = Path(LOG_DIR)
    logs = sorted(log_dir.glob(LOG_GLOB))
    if not logs:
        print(f"В {log_dir} нет файлов по маске {LOG_GLOB}")
        return

    # Агрегаты:
    file_to_errors: Dict[str, Counter] = defaultdict(Counter)   # file -> {error_text: count}
    error_to_files: Dict[str, Counter] = defaultdict(Counter)   # error_text -> {file: count}
    file_totals: Dict[str, int] = {}                            # file -> N (из "Test finished...")
    file_to_folder: Dict[str, str] = {}                         # file -> folder

    # Промежуточное: для последующей сводки по папкам
    for log in logs:
        tp_name, folder_name, per_error_this_log, finished_total = parse_one_log(log)

        if folder_name:
            file_to_folder.setdefault(tp_name, folder_name)

        # перельём считанное по ошибкам (ERROR ...)
        for key, cnt in per_error_this_log.items():
            err_text, _, file_part = key.partition("||")
            err_text = err_text.strip()
            file_only = file_part.replace("FILE:", "").strip() if file_part else tp_name

            file_to_errors[Path(file_only).name][err_text] += cnt
            error_to_files[err_text][Path(file_only).name] += cnt

        # итог по файлу
        if finished_total is not None:
            file_totals[tp_name] = finished_total

    # ---------- Печать сводок по файлам/ошибкам ----------
    print("\n=== Ошибки по файлам (какая ошибка в каком .tp и сколько раз) ===")
    for f, counter in sorted(file_to_errors.items(),
                             key=lambda kv: (-sum(kv[1].values()), kv[0].lower())):
        total = sum(counter.values())
        print(f"\n{f}  — всего записей ошибок: {total}")
        for err, cnt in counter.most_common():
            print(f"  {cnt:4d} × {err}")

    print("\n=== Общая сводка по ошибкам (в каких файлах встречаются) ===")
    for err_text, files_counter in sorted(error_to_files.items(),
                                          key=lambda kv: (-sum(kv[1].values()), kv[0].lower())):
        total = sum(files_counter.values())
        print(f"\n{total:4d} × {err_text}")
        for f, cnt in files_counter.most_common():
            print(f"    {cnt:4d} × {f}")

    print("\n=== Сколько ошибок в каждом файле (из 'Test finished with N errors.') ===")
    for f, n in sorted(file_totals.items(), key=lambda kv: (-kv[1], kv[0].lower())):
        print(f"{n:5d}  {f}")

    # ---------- ДОБАВЛЕНО: сводка по папкам ----------
    # Собираем: папка -> {file: total_errors_in_file}; затем считаем суммы по папкам
    folder_files: Dict[str, Counter] = defaultdict(Counter)
    for f, n in file_totals.items():
        folder = file_to_folder.get(f, "UNKNOWN")
        folder_files[folder][f] += n

    folder_totals: Dict[str, int] = {folder: sum(counter.values())
                                     for folder, counter in folder_files.items()}

    print("\n=== Ошибки по папкам (по итогам 'Test finished with N errors.') ===")
    for folder, total in sorted(folder_totals.items(), key=lambda kv: (-kv[1], kv[0].lower())):
        files_counter = folder_files[folder]
        print(f"\n{total:5d}  {folder}")
        for f, cnt in files_counter.most_common():
            print(f"  {cnt:5d}  {f}")

    # ---------- Запись CSV/TSV по желанию ----------
    if WRITE_CSV or WRITE_TSV:
        if WRITE_CSV:
            with open(log_dir / CSV_ERRORS_BY_FILE, "w", newline="", encoding="utf-8") as fw:
                w = csv.writer(fw)
                w.writerow(["file", "error", "count"])
                for f, counter in file_to_errors.items():
                    for err, cnt in counter.items():
                        w.writerow([f, err, cnt])

            with open(log_dir / CSV_FILE_TOTALS, "w", newline="", encoding="utf-8") as fw:
                w = csv.writer(fw)
                w.writerow(["file", "total_errors"])
                for f, n in file_totals.items():
                    w.writerow([f, n])

            # ДОБАВЛЕНО — CSV по папкам
            with open(log_dir / CSV_FOLDER_TOTALS, "w", newline="", encoding="utf-8") as fw:
                w = csv.writer(fw)
                w.writerow(["folder", "total_errors"])
                for folder, total in folder_totals.items():
                    w.writerow([folder, total])

            with open(log_dir / CSV_FOLDER_FILES, "w", newline="", encoding="utf-8") as fw:
                w = csv.writer(fw)
                w.writerow(["folder", "file", "total_errors"])
                for folder, counter in folder_files.items():
                    for f, cnt in counter.items():
                        w.writerow([folder, f, cnt])

        if WRITE_TSV:
            with open(log_dir / TSV_ERRORS_BY_FILE, "w", newline="", encoding="utf-8") as fw:
                w = csv.writer(fw, delimiter="\t")
                w.writerow(["file", "error", "count"])
                for f, counter in file_to_errors.items():
                    for err, cnt in counter.items():
                        w.writerow([f, err, cnt])

            with open(log_dir / TSV_FILE_TOTALS, "w", newline="", encoding="utf-8") as fw:
                w = csv.writer(fw, delimiter="\t")
                w.writerow(["file", "total_errors"])
                for f, n in file_totals.items():
                    w.writerow([f, n])

            # ДОБАВЛЕНО — TSV по папкам
            with open(log_dir / TSV_FOLDER_TOTALS, "w", newline="", encoding="utf-8") as fw:
                w = csv.writer(fw, delimiter="\t")
                w.writerow(["folder", "total_errors"])
                for folder, total in folder_totals.items():
                    w.writerow([folder, total])

            with open(log_dir / TSV_FOLDER_FILES, "w", newline="", encoding="utf-8") as fw:
                w = csv.writer(fw, delimiter="\t")
                w.writerow(["folder", "file", "total_errors"])
                for folder, counter in folder_files.items():
                    for f, cnt in counter.items():
                        w.writerow([folder, f, cnt])

        print("\nФайлы отчётов записаны в:", log_dir)


if __name__ == "__main__":
    main()
