# -*- coding: utf-8 -*-
"""
Сканирует логи proctest и вставляет строку
    testprocex FUNCTXCACHE_Clear () \
в исходные .tp-файлы на позиции из логов с учётом смещений и правила якоря "test".
"""

from __future__ import annotations
from pathlib import Path
from typing import Tuple, List, Dict
from collections import defaultdict
import re

# ===================== НАСТРОЙКИ =====================
LOG_DIR  = r"D:\Temp\test LOG"   # где *.log
BASE_DIR = r"C:\septim.git\Septim4.limited\Septim\PgSQL\Proc"  # корень .tp
DRY_RUN  = True                 # True — показать, что сделает, но ничего не менять
INSERT_TEXT = "testprocex FUNCTXCACHE_Clear () \\"  # ВСТАВЛЯЕМ ЭТУ СТРОКУ (обратный слеш обязателен)
# =====================================================

ERROR_NEED_CHECK = "does not contain user right check."
LOG_NAME_PREFIX = "_PgSQL_Proc_"

FILE_RE = re.compile(r"^FILE\s*:\s*(?P<file>[^\s]+)", re.IGNORECASE)
LINE_INLINE_RE = re.compile(r"LINE\s*:\s*(?P<ln>\d+)", re.IGNORECASE)
LINE_SOLO_RE   = re.compile(r"^LINE\s*:\s*(?P<ln>\d+)", re.IGNORECASE)


# ---------- утилиты ввода/вывода ----------
def read_text_any(path: Path) -> Tuple[str, str, bool]:
    """
    Чтение файла с определением BOM.
    Возврат: (text, encoding, had_bom)
    """
    data = path.read_bytes()
    had_bom = data.startswith(b"\xEF\xBB\xBF")
    if had_bom:
        # убрать BOM при декодировании
        text = data.decode("utf-8-sig")
        return text, "utf-8", True

    # без BOM — пробуем обычные кодировки
    for enc in ("utf-8", "cp1251", "cp1250", "latin-1"):
        try:
            return data.decode(enc), enc, False
        except UnicodeDecodeError:
            continue
    # последний шанс
    return data.decode("utf-8", errors="replace"), "utf-8*", False


def write_text_keep_newlines(path: Path, text: str, encoding: str, had_bom: bool) -> None:
    """
    Запись текста, сохраняя наличие/отсутствие BOM и тип переводов строк,
    т.к. мы формируем их сами (newline="").
    """
    # если при чтении вернулся "utf-8-sig" (через наше "utf-8" + had_bom=True),
    # то пишем с BOM; иначе — без BOM
    enc_to_write = "utf-8-sig" if had_bom else ("utf-8" if encoding.startswith("utf-8") else encoding)
    with open(path, "w", encoding=enc_to_write, newline="") as f:
        f.write(text)


def detect_newline(lines: List[str]) -> str:
    for ln in lines:
        if ln.endswith("\r\n"):
            return "\r\n"
    return "\n"


def ensure_backup(path: Path) -> None:
    bak = path.with_suffix(path.suffix + ".bak")
    if not bak.exists():
        bak.write_bytes(path.read_bytes())


# ---------- парсинг логов ----------
def parse_issue_blocks(log_path: Path) -> List[Tuple[str, int]]:
    """
    Для каждого блока ошибки 'does not contain user right check.' вернуть (file_name, line_no).
    Ищем FILE и LINE в нескольких строках ниже.
    """
    text, _, _ = read_text_any(log_path)
    lines = text.splitlines()
    out: List[Tuple[str, int]] = []

    i = 0
    L = len(lines)
    while i < L:
        if ERROR_NEED_CHECK in lines[i].lower():
            file_name = None
            line_no   = None
            for j in range(i + 1, min(i + 8, L)):
                if file_name is None:
                    m_file = FILE_RE.search(lines[j])
                    if m_file:
                        file_name = m_file.group("file").strip()

                # LINE может быть как в той же строке (после COMMAND), так и отдельной
                if line_no is None:
                    m1 = LINE_INLINE_RE.search(lines[j])
                    if m1:
                        line_no = int(m1.group("ln"))
                        continue
                    m2 = LINE_SOLO_RE.search(lines[j])
                    if m2:
                        line_no = int(m2.group("ln"))

            if file_name and line_no:
                out.append((file_name, line_no))
        i += 1

    return out


def extract_folder_from_logname(log_name: str, tp_file_name: str) -> str | None:
    """
    Из _PgSQL_Proc_<FOLDER_WITH_UNDERSCORES>_<FILEBASE>.tp.log + FILE:<file>.tp
    достать <FOLDER_WITH_UNDERSCORES>.
    """
    base = Path(log_name).name
    if not base.startswith(LOG_NAME_PREFIX):
        return None
    tail = base[len(LOG_NAME_PREFIX):]  # '<FOLDER>_<FILEBASE>.tp.log'

    file_base = Path(tp_file_name).stem  # 'area' из 'area.tp'
    suffix = f"_{file_base}.tp.log"

    if tail.lower().endswith(suffix.lower()):
        folder = tail[:-len(suffix)]
        return folder or None

    # если формат неожиданный — хотя бы токен до первого "_"
    return tail.split("_", 1)[0] if "_" in tail else None


# ---------- вставка с учётом якоря 'test' ----------
def _is_test_line(line: str) -> bool:
    return line.lstrip().lower().startswith("test")


def insert_line_at_anchor(path: Path, at_line_1based: int, text_to_insert: str, *, dry_run: bool) -> Tuple[bool, int, int]:
    """
    Вставляет строку перед якорем.
    - Ищем якорь: если строка at_line начинается на 'test' -> якорь = эта строка,
      иначе поднимаемся вверх до ближайшей строки, начинающейся на 'test'.
      Если не нашли вообще — используем исходную позицию.
    - Не дублируем: если сразу перед якорем уже стоит такая же строка — пропускаем.

    :return: (changed, anchor_line_1based, used_line_1based)
             used_line_1based = исходная позиция (at_line_1based), для логов.
    """
    raw, enc, had_bom = read_text_any(path)
    lines = raw.splitlines(keepends=True)
    newline = detect_newline(lines) if lines else "\n"

    # исходные признаки конца файла
    had_final_nl = raw.endswith(("\r\n", "\n"))

    # целевой индекс
    idx = max(0, min(len(lines), at_line_1based - 1))

    # поиск якоря
    anchor = idx
    if idx < len(lines) and not _is_test_line(lines[idx]):
        for k in range(idx - 1, -1, -1):
            if _is_test_line(lines[k]):
                anchor = k
                break
        # если не нашли — anchor остаётся idx

    # дедуп: если ровно ПЕРЕД якорем уже стоит такая же строка, пропускаем
    if anchor > 0 and lines[anchor - 1].strip() == text_to_insert.strip():
        print(f"  ~ Уже стоит: {path.name} @ line {anchor} (из {at_line_1based})")
        return False, anchor + 1, at_line_1based

    # формируем текст
    new_line = text_to_insert + newline
    new_text = "".join(lines[:anchor] + [new_line] + lines[anchor:])

    # восстановить состояние финального перевода строки
    if had_final_nl and not new_text.endswith(newline):
        new_text += newline
    if not had_final_nl and new_text.endswith(newline):
        new_text = new_text[:-len(newline)]

    if dry_run:
        moved = "" if anchor == idx else f" (смещено вверх до {anchor + 1})"
        print(f"  [DRY] {path} ← вставка @ {at_line_1based}{moved}")
        return True, anchor + 1, at_line_1based

    ensure_backup(path)
    write_text_keep_newlines(path, new_text, enc, had_bom)
    moved = "" if anchor == idx else f" (смещено вверх до {anchor + 1})"
    print(f"  ✔ Изменён: {path} (вставлено @ {at_line_1based}{moved})")
    return True, anchor + 1, at_line_1based


# ---------- основной сценарий ----------
def main():
    log_dir = Path(LOG_DIR)
    base_dir = Path(BASE_DIR)

    if not log_dir.exists():
        print(f"Нет каталога логов: {log_dir}")
        return
    if not base_dir.exists():
        print(f"Нет каталога исходников: {base_dir}")
        return

    # 1) Собираем все вставки по логам → группируем по исходному файлу .tp
    per_file: Dict[Path, List[Tuple[int, str]]] = defaultdict(list)  # src -> [(line_no, log_name)]
    log_files = sorted(log_dir.glob("*.log"))
    if not log_files:
        print(f"В {log_dir} нет *.log")
        return

    for log_path in log_files:
        issues = parse_issue_blocks(log_path)
        if not issues:
            continue
        for file_name, line_no in issues:
            folder = extract_folder_from_logname(log_path.name, file_name)
            if not folder:
                print(f"[{log_path.name}] не удалось определить папку для FILE:{file_name}")
                continue
            src = (base_dir / folder / file_name).resolve()
            if not src.exists():
                print(f"[{log_path.name}] Не найден исходник: {src}")
                continue
            per_file[src].append((line_no, log_path.name))

    if not per_file:
        print("Совпадений в логах не найдено.")
        return

    # 2) По каждому файлу: сортируем и учитываем смещение
    total_ins = 0
    for src_path, items in per_file.items():
        items.sort(key=lambda x: x[0])  # по LINE
        offset = 0
        for line_no, logname in items:
            target = line_no + offset
            changed, anchor_used, _orig = insert_line_at_anchor(
                src_path, target, INSERT_TEXT, dry_run=DRY_RUN
            )
            if changed:
                total_ins += 1
                offset += 1  # после ВСТАВКИ всё ниже смещается на +1

    print(f"\nГотово. Файлов: {len(per_file)}. Вставок: {total_ins}. Режим: {'DRY-RUN' if DRY_RUN else 'WRITE'}.")


if __name__ == "__main__":
    main()
