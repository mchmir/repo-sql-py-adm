# -*- coding: utf-8 -*-
"""
Ищет в логах строку NEEDLE и печатает ПОЛНЫЕ пути исходников .tp,
которые соответствуют логам, где встретилась NEEDLE.

Формат логов: _PgSQL_Proc_<FOLDER_WITH_UNDERSCORES>_<FILEBASE>.tp.log
Путь к исходнику: <BASE_DIR>/<FOLDER_WITH_UNDERSCORES>/<FILE>.tp

Строка NEEDLE и пути задаются КОНСТАНТАМИ ниже.
"""

from __future__ import annotations
from pathlib import Path
import re
from typing import Optional, Tuple, List, Set


# ===== НАСТРОЙКИ  =====
LOG_DIR  = r"D:\Temp\test LOG"  # где лежат *.log
BASE_DIR = r"C:\septim.git\Septim4.limited\Septim\PgSQL\Proc"  # корень исходников .tp
NEEDLE   = 'does not contain user right check'  # искомая строка
CASE_SENSITIVE = False  # учитывать регистр?
PRINT_MISSING  = False  # печатать пути .tp, которых нет на диске
# ======================================


LOG_NAME_PREFIX = "_PgSQL_Proc_"
FILE_RE = re.compile(r"^FILE\s*:\s*(?P<file>[^\s]+)", re.IGNORECASE)


def read_text_any(path: Path) -> Tuple[str, str]:
    """Читает файл, пробуя типичные кодировки. Возвращает (text, encoding)."""
    for enc in ("utf-8-sig", "cp1251", "cp1250", "latin-1", "utf-8"):
        try:
            with open(path, "r", encoding=enc, newline="") as f:
                return f.read(), enc
        except UnicodeDecodeError:
            continue
    with open(path, "r", encoding="utf-8", errors="replace", newline="") as f:
        return f.read(), "utf-8*"


def log_contains_needle(log_path: Path, needle: str, case_sensitive: bool) -> bool:
    text, _ = read_text_any(log_path)
    return (needle in text) if case_sensitive else (needle.lower() in text.lower())


def get_tp_files_from_log(log_path: Path) -> List[str]:
    """Собирает значения FILE:... из лога (могут повторяться)."""
    text, _ = read_text_any(log_path)
    files: List[str] = []
    for line in text.splitlines():
        m = FILE_RE.search(line)
        if m:
            files.append(m.group("file").strip())
    return files


def extract_filebase_from_logname(log_name: str) -> Optional[str]:
    """
    Из _PgSQL_Proc_<FOLDER>_<FILEBASE>.tp.log вернуть '<FILEBASE>.tp'
    Пример: _PgSQL_Proc_Bazove_tabulky_lekcemodel.tp.log -> 'lekcemodel.tp'
    """
    base = Path(log_name).name
    if not base.startswith(LOG_NAME_PREFIX):
        return None
    tail = base[len(LOG_NAME_PREFIX):]  # '<FOLDER>_<FILEBASE>.tp.log'
    if not tail.lower().endswith(".tp.log"):
        return None
    tail_wo_ext = tail[:-len(".tp.log")]
    if "_" not in tail_wo_ext:
        return None
    filebase = tail_wo_ext.rsplit("_", 1)[1]
    if not filebase:
        return None
    return filebase + ".tp"


def extract_folder_from_logname_using_file(log_name: str, tp_file_name: str) -> Optional[str]:
    """
    Из _PgSQL_Proc_<FOLDER_WITH_UNDERSCORES>_<FILEBASE>.tp.log достаём <FOLDER>,
    используя имя файла из FILE: (tp_file_name), чтобы корректно отсечь хвост.
    """
    base = Path(log_name).name
    if not base.startswith(LOG_NAME_PREFIX):
        return None
    tail = base[len(LOG_NAME_PREFIX):]  # '<FOLDER>_<FILEBASE>.tp.log'
    file_base = Path(tp_file_name).stem  # 'lekcemodel' из 'lekcemodel.tp'
    suffix = f"_{file_base}.tp.log"
    if tail.lower().endswith(suffix.lower()):
        folder = tail[:-len(suffix)]
        return folder or None
    # Фолбэк: если формат неожиданный — возьмём до первого "_"
    return tail.split("_", 1)[0] if "_" in tail else None


def main():
    log_dir = Path(LOG_DIR)
    base_dir = Path(BASE_DIR)

    if not log_dir.exists():
        print(f"# Нет каталога логов: {log_dir}")
        return
    if not base_dir.exists():
        print(f"# Нет базового каталога исходников: {base_dir}")
        return

    logs = sorted(log_dir.glob("*.log"))
    if not logs:
        print(f"# В {log_dir} нет *.log")
        return

    results: Set[Path] = set()
    missing: Set[Path] = set()

    for log_path in logs:
        # Ищем только в логах, где встречается нужная строка
        if not log_contains_needle(log_path, NEEDLE, CASE_SENSITIVE):
            continue

        # Сначала пробуем точные FILE: из лога
        tp_files = get_tp_files_from_log(log_path)

        if not tp_files:
            # Если FILE: нет — извлекаем из имени лога
            guessed_tp = extract_filebase_from_logname(log_path.name)
            if guessed_tp:
                folder = extract_folder_from_logname_using_file(log_path.name, guessed_tp)
                if folder:
                    p = (base_dir / folder / guessed_tp).resolve()
                    (results if p.exists() else missing).add(p)
            continue

        # Есть FILE: — сопоставляем каждое с папкой из имени лога
        for tp_name in tp_files:
            folder = extract_folder_from_logname_using_file(log_path.name, tp_name)
            if not folder:
                continue
            p = (base_dir / folder / tp_name).resolve()
            (results if p.exists() else missing).add(p)

    # Вывод: ТОЛЬКО существующие пути .tp, по одному на строку
    for p in sorted(results):
        print(str(p))

    # По желанию — показать ненайденные
    if PRINT_MISSING and missing:
        print("\n# Не найдены на диске:")
        for p in sorted(missing):
            print(str(p))


if __name__ == "__main__":
    main()
