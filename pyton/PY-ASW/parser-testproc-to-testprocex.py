# -*- coding: utf-8 -*-
"""
Конвертер testproc → testprocex c гарантией записи без BOM.

Что делает:
  - Находит строки, начинающиеся с:  testproc <PROC> (<values>) (<idvars>) (<param_names>) (<idnames>) [\]
  - Позиционно сопоставляет <values> к <param_names> и формирует:
        testprocex <PROC> (a_...=..., ...) (ID=...) \
  - Имена процедуры берёт из самого матча (или ограничивает фильтром PROC_FILTER).
  - Пишет без BOM (UTF-8), сохраняя исходные переводы строк (CRLF/LF).
  - Создаёт .bak при INPLACE=True.

Настройки в блоке "НАСТРОЙКИ" ниже.
"""

from pathlib import Path
import re
import sys
import shutil

# ===================== НАСТРОЙКИ =====================
BASE_DIR = r"C:\septim.git\Septim4.limited\Septim\PgSQL\Proc"
FILE_GLOB = "*.tp"

INPLACE = True             # True -> править файлы на месте (с .bak)
BACKUP_EXT = ".bak"        # расширение бэкапа
ASSIGN_PER_LINE = 6        # сколько пар name=value на строку

# Фильтр по имени процедуры:
#   PROC_FILTER = 'RECCENIKInsert'                 # только одно имя
#   PROC_FILTER = ['RECCENIKInsert','RECEPTInsert']# несколько имён
#   PROC_FILTER = None                             # все процедуры
PROC_FILTER = 'RECCENIKInsert'

# Кодеки/переводы
PRESERVE_EOL = True        # сохранять стиль переводов строк (CRLF/LF)
FORCE_UTF8_NO_BOM = True   # всегда записывать UTF-8 без BOM (т.е. даже если в исходнике был BOM)
# =====================================================


def compile_start_re(proc_filter):
    if proc_filter is None:
        pattern = r'(?im)^[ \t]*testproc\s+(?P<proc>[A-Za-z_][A-Za-z_0-9]*)\b'
    elif isinstance(proc_filter, (list, tuple, set)):
        alt = '|'.join(re.escape(p) for p in proc_filter)
        pattern = rf'(?im)^[ \t]*testproc\s+(?P<proc>{alt})\b'
    else:
        pattern = rf'(?im)^[ \t]*testproc\s+(?P<proc>{re.escape(proc_filter)})\b'
    return re.compile(pattern)


START_RE = compile_start_re(PROC_FILTER)


def sniff_text_and_meta(path: Path):
    """Возвращает (text, enc_detected, had_bom, eol), где:
       - enc_detected: 'utf-8', 'cp1250', 'cp1251' (best effort)
       - had_bom: был ли UTF-8 BOM в исходнике
       - eol: '\r\n' или '\n' (по большинству строк)
    """
    raw = path.read_bytes()
    had_bom = raw.startswith(b'\xef\xbb\xbf')

    # эвристика по переводу строк
    # если встречается \r\n и есть одиночные \n после удаления \r\n — используем '\n', иначе '\r\n'
    # но чаще хотим «преобладающий» стиль, сделаем проще:
    crlf = raw.count(b'\r\n')
    lf = raw.count(b'\n')
    eol = '\r\n' if crlf and crlf >= (lf - crlf) else '\n'

    # попытки декодирования
    if had_bom:
        text = raw.decode('utf-8-sig')
        return text, 'utf-8', True, eol
    for enc in ('utf-8', 'cp1250', 'cp1251'):
        try:
            text = raw.decode(enc)
            return text, enc, False, eol
        except UnicodeDecodeError:
            continue
    # fallback
    text = raw.decode('utf-8', errors='replace')
    return text, 'utf-8', False, eol


def write_text_smart(path: Path, text: str, enc_in: str, had_bom_in: bool, eol: str):
    """
    Пишет текст согласно политикам:
      - если FORCE_UTF8_NO_BOM=True -> всегда UTF-8 без BOM
      - иначе -> писать в enc_in, и если enc_in == 'utf-8':
                   * без BOM (мы не хотим его больше добавлять)
      - EOL нормализуем под исходный стиль, если PRESERVE_EOL=True
    """
    # нормализуем переводы строк
    if PRESERVE_EOL:
        t = text.replace('\r\n', '\n').replace('\r', '\n')
        if eol == '\r\n':
            t = t.replace('\n', '\r\n')
    else:
        t = text.replace('\r\n', '\n').replace('\r', '\n')

    if FORCE_UTF8_NO_BOM:
        data = t.encode('utf-8')
    else:
        if enc_in == 'utf-8':
            # записываем без BOM
            data = t.encode('utf-8')
        else:
            data = t.encode(enc_in, errors='strict')

    path.write_bytes(data)


def extract_group(src: str, i: int):
    """Читает группу () с учётом кавычек. Возвращает (content, new_index_after_group)."""
    n = len(src)
    while i < n and src[i].isspace():
        i += 1
    if i >= n or src[i] != '(':
        raise ValueError("Ожидалась '(' при разборе группы")
    i += 1
    depth = 1
    in_str = False
    out = []
    while i < n:
        ch = src[i]
        if in_str:
            if ch == "'":
                if i + 1 < n and src[i+1] == "'":
                    out.append("''")
                    i += 2
                else:
                    out.append(ch)
                    i += 1
                    in_str = False
            else:
                out.append(ch)
                i += 1
        else:
            if ch == "'":
                out.append(ch)
                i += 1
                in_str = True
            elif ch == '(':
                depth += 1
                out.append(ch)
                i += 1
            elif ch == ')':
                depth -= 1
                if depth == 0:
                    return ''.join(out).rstrip(), i + 1
                out.append(ch)
                i += 1
            else:
                out.append(ch)
                i += 1
    raise ValueError("Незакрытая скобочная группа")


def split_args(s: str):
    """Разбивает аргументы по запятым, учитывая строки в одиночных кавычках."""
    args = []
    cur = []
    in_str = False
    i = 0
    n = len(s)
    while i < n:
        ch = s[i]
        if in_str:
            if ch == "'":
                if i + 1 < n and s[i+1] == "'":
                    cur.append("''")
                    i += 2
                else:
                    cur.append(ch)
                    i += 1
                    in_str = False
            else:
                cur.append(ch)
                i += 1
        else:
            if ch == "'":
                cur.append(ch)
                in_str = True
                i += 1
            elif ch == ',':
                args.append(''.join(cur).strip())
                cur = []
                i += 1
            else:
                cur.append(ch)
                i += 1
    token = ''.join(cur).strip()
    if token != '' or s.endswith(','):
        args.append(token)
    return args


def normalize_value(tok: str) -> str:
    """Пустые -> '', null/NULL -> null, остальное оставляем как есть (числа, bool, идентификаторы, строки)."""
    t = tok.strip()
    if t == '':
        return "''"
    if t.lower() == 'null':
        return 'null'
    return t


def format_assignments(names, values):
    pairs = []
    m = min(len(names), len(values))
    for i in range(m):
        name = names[i].strip()
        if not name:
            continue
        val = normalize_value(values[i])
        pairs.append(f"{name}={val}")
    if not pairs:
        return ''
    chunks = []
    for i in range(0, len(pairs), ASSIGN_PER_LINE):
        chunks.append(','.join(pairs[i:i+ASSIGN_PER_LINE]))
    indent = ' ' * 25
    if len(chunks) == 1:
        return chunks[0]
    return (',\n' + indent).join(chunks)


def build_testprocex(values_s: str, idvars_s: str, names_s: str, idnames_s: str, proc_name: str):
    values  = split_args(values_s)
    names   = split_args(names_s)
    idvars  = split_args(idvars_s)
    idnames = split_args(idnames_s)

    assigns = format_assignments(names, values)

    # сформируем часть с ключами: сопоставляем по позиции (минимум)
    id_pairs = []
    k = min(len(idnames), len(idvars))
    for i in range(k):
        nm = idnames[i].strip()
        vv = idvars[i].strip()
        if nm:
            id_pairs.append(f"{nm}={vv}")
    id_part = ','.join(id_pairs)

    inside = assigns
    return f"testprocex {proc_name} ({inside}) ({id_part}) \\"


def convert_one_block(src: str, start_idx: int, proc_name: str):
    """Считает 4 группы и возвращает (new_text, end_idx)."""
    i = start_idx
    g1, i = extract_group(src, i)   # (<values>)
    g2, i = extract_group(src, i)   # (<idvars>)
    g3, i = extract_group(src, i)   # (<param_names>)
    g4, i = extract_group(src, i)   # (<idnames>)
    # поглотим хвост: пробелы + необязательный слеш
    tail = re.match(r'[ \t]*\\?', src[i:])
    if tail:
        i += len(tail.group(0))
    new_line = build_testprocex(g1, g2, g3, g4, proc_name)
    return new_line, i


def replace_in_text(fulltext: str):
    """Возвращает (new_text, changes: list[(line_no, original_fragment, new_line)])."""
    out = []
    last = 0
    changes = []
    for m in START_RE.finditer(fulltext):
        out.append(fulltext[last:m.start()])
        try:
            proc_name = m.group('proc')
            new_line, end_idx = convert_one_block(fulltext, m.end(), proc_name)
        except Exception as e:
            # если не получилось — оставляем исходный фрагмент без изменений
            out.append(fulltext[m.start():m.end()])
            last = m.end()
            continue

        original_fragment = fulltext[m.start():end_idx]
        line_no = fulltext.count('\n', 0, m.start()) + 1
        changes.append((line_no, original_fragment, new_line))

        out.append(new_line)
        if not new_line.endswith('\n'):
            out.append('\n')
        last = end_idx

    out.append(fulltext[last:])
    return ''.join(out), changes


def main():
    root = Path(BASE_DIR)
    if not root.exists():
        print(f"[ERR] BASE_DIR не найден: {root}", file=sys.stderr)
        sys.exit(1)

    total_files = 0
    touched_files = 0
    total_blocks = 0

    for p in root.rglob(FILE_GLOB):
        total_files += 1
        text, enc, had_bom, eol = sniff_text_and_meta(p)
        new_text, changes = replace_in_text(text)
        if not changes:
            continue

        touched_files += 1
        total_blocks += len(changes)
        print(f"\n=== {p} ===")
        for (line_no, orig, new_line) in changes:
            print(f"[строка {line_no}]")
            preview = '\n'.join(orig.strip().splitlines()[:3])
            print("— исходное начало:")
            print(preview)
            print("— результат:")
            print(new_line)

        if INPLACE and new_text != text:
            bak = p.with_suffix(p.suffix + BACKUP_EXT)
            try:
                if not bak.exists():
                    shutil.copy2(p, bak)
                write_text_smart(p, new_text, enc, had_bom, eol)
                print(f"[ok] Обновлён: {p} (backup: {bak.name})")
            except Exception as e:
                print(f"[ERR] Не удалось записать {p}: {e}", file=sys.stderr)

    print(f"\nГотово. Просмотрено файлов: {total_files}. "
          f"Файлов с изменениями: {touched_files}. "
          f"Преобразовано блоков: {total_blocks}.")


if __name__ == '__main__':
    main()
