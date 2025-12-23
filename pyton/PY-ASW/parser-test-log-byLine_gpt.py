# -*- coding: utf-8 -*-
"""
Ищет в логах строку needle и печатает полные пути исходников .tp,
которые соответствуют логам, где встречается needle.

Формат логов: _PgSQL_Proc_<FOLDER>_<FILEBASE>.tp.log
Путь к исходнику: <base_dir>/<FOLDER>/<FILE>

По возможности берём FILE:... из лога; если нет — используем FILEBASE из имени лога.

:: искомая строка без учёта регистра
python find_error_sources.py --needle "column \"taxgid\" does not exist" ^
  --log-dir "D:\Temp\test LOG" ^
  --base-dir "C:\septim.git\Septim4.limited\Septim\PgSQL\Proc"

:: чувствительный к регистру
python find_error_sources.py --needle "column \"taxgid\" does not exist" --case-sensitive ^
  --log-dir "D:\Temp\test LOG" ^
  --base-dir "C:\septim.git\Septim4.limited\Septim\PgSQL\Proc"

:: при желании увидеть и те пути, что не нашлись на диске:
python find_error_sources.py --needle "column \"taxgid\" does not exist" --print-missing ^
  --log-dir "D:\Temp\test LOG" ^
  --base-dir "C:\septim.git\Septim4.limited\Septim\PgSQL\Proc"

"""

from __future__ import annotations
import argparse
import re
from pathlib import Path
from typing import Tuple, List, Optional, Set

LOG_NAME_PREFIX = "_PgSQL_Proc_"

DEFAULT_LOG_DIR = r"D:\Temp\test LOG_"
DEFAULT_BASE_DIR = r"C:\septim.git\Septim4.limited\Septim\PgSQL\Proc"

FILE_RE = re.compile(r"^FILE\s*:\s*(?P<file>[^\s]+)", re.IGNORECASE)


def read_text_any(path: Path) -> Tuple[str, str]:
    """ Читает файл с несколькими кодировками. Возвращает (text, encoding)."""
    for enc in ("utf-8-sig", "cp1251", "cp1250", "latin-1", "utf-8"):
        try:
            with open(path, "r", encoding=enc, newline="") as f:
                return f.read(), enc
        except UnicodeDecodeError:
            continue
    with open(path, "r", encoding="utf-8", errors="replace", newline="") as f:
        return f.read(), "utf-8*"


def extract_filebase_from_logname(log_name: str) -> Optional[str]:
    """
    Из _PgSQL_Proc_<FOLDER>_<FILEBASE>.tp.log вернуть FILEBASE + '.tp'
    Пример: _PgSQL_Proc_Bazove_tabulky_lekcemodel.tp.log -> 'lekcemodel.tp'
    """
    base = Path(log_name).name
    if not base.startswith(LOG_NAME_PREFIX):
        return None
    tail = base[len(LOG_NAME_PREFIX):]  # '<FOLDER>_<FILEBASE>.tp.log'
    if not tail.lower().endswith(".tp.log"):
        return None
    tail_wo_ext = tail[: -len(".tp.log")]
    if "_" not in tail_wo_ext:
        return None
    filebase = tail_wo_ext.rsplit("_", 1)[1]
    if not filebase:
        return None
    return filebase + ".tp"


def extract_folder_from_logname_using_file(log_name: str, tp_file_name: str) -> Optional[str]:
    """
    Из _PgSQL_Proc_<FOLDER_WITH_UNDERSCORES>_<FILEBASE>.tp.log достать <FOLDER_WITH_UNDERSCORES>,
    используя имя файла из FILE:(tp_file_name) для правильного отсечения хвоста.
    """
    base = Path(log_name).name
    if not base.startswith(LOG_NAME_PREFIX):
        return None
    tail = base[len(LOG_NAME_PREFIX):]  # '<FOLDER>_<FILEBASE>.tp.log'
    file_base = Path(tp_file_name).stem  # 'lekcemodel' из 'lekcemodel.tp'
    suffix = f"_{file_base}.tp.log"
    if tail.lower().endswith(suffix.lower()):
        folder = tail[: -len(suffix)]
        return folder or None
    # Фолбэк: взять токен до первого подчёркивания (может быть неверно для сложных папок)
    return tail.split("_", 1)[0] if "_" in tail else None


def get_tp_files_from_log(log_path: Path) -> List[str]:
    """Собрать все значения FILE:... из лога (могут быть дубликаты)."""
    text, _ = read_text_any(log_path)
    files = []
    for line in text.splitlines():
        m = FILE_RE.search(line)
        if m:
            files.append(m.group("file").strip())
    return files


def log_contains_needle(log_path: Path, needle: str, case_sensitive: bool) -> bool:
    text, _ = read_text_any(log_path)
    if case_sensitive:
        return needle in text
    return needle.lower() in text.lower()


def main():
    ap = argparse.ArgumentParser(description="Список .tp-файлов, где в логах встретилась заданная строка.")
    ap.add_argument("--log-dir", default=DEFAULT_LOG_DIR, help="Каталог логов (*.log)")
    ap.add_argument("--base-dir", default=DEFAULT_BASE_DIR, help="База исходников .tp (Proc)")
    ap.add_argument("--needle", required=True, help='Искомая строка, напр. column "taxgid" does not exist')
    ap.add_argument("--case-sensitive", action="store_true", help="Чувствительный к регистру поиск (по умолчанию — нет)")
    ap.add_argument("--print-missing", action="store_true", help="Печатать пути, которые не найдены на диске")
    args = ap.parse_args()

    log_dir = Path(args.log_dir)
    base_dir = Path(args.base_dir)

    if not log_dir.exists():
        print(f"Нет каталога логов: {log_dir}")
        return
    if not base_dir.exists():
        print(f"Нет базового каталога исходников: {base_dir}")
        return

    results: Set[Path] = set()
    missing: Set[Path] = set()

    logs = sorted(log_dir.glob("*.log"))
    if not logs:
        print(f"В {log_dir} нет *.log")
        return

    for log_path in logs:
        # Пропускаем логи без искомой строки
        if not log_contains_needle(log_path, args.needle, args.case_sensitive):
            continue

        # Пробуем взять FILE:... из лога
        tp_files = get_tp_files_from_log(log_path)
        if not tp_files:
            # fallback: FILE не найден, пытаемся извлечь из имени лога
            guessed_tp = extract_filebase_from_logname(log_path.name)
            if guessed_tp:
                folder = extract_folder_from_logname_using_file(log_path.name, guessed_tp)
                if folder:
                    p = base_dir / folder / guessed_tp
                    if p.exists():
                        results.add(p.resolve())
                    else:
                        missing.add(p.resolve())
            continue

        # Для каждого FILE: сопоставляем с папкой из имени лога
        for tp_name in tp_files:
            folder = extract_folder_from_logname_using_file(log_path.name, tp_name)
            if not folder:
                continue
            p = base_dir / folder / tp_name
            if p.exists():
                results.add(p.resolve())
            else:
                missing.add(p.resolve())

    # Вывод — только существующие пути .tp
    for p in sorted(results):
        print(str(p))

    # Опционально — показать ненайденные (на случай рассинхронизаций)
    if args.print_missing and missing:
        print("\n# Не найдены на диске:")
        for p in sorted(missing):
            print(str(p))


if __name__ == "__main__":
    main()
