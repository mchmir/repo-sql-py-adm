# -*- coding: utf-8 -*-
"""
Правка testprocex RECCENIKInsert в *.tp:
- Удалить: a_CENAB,a_DPHA,a_CENAS,a_CENAB1,a_DPHA1,a_CENAS1,a_CENAB2,a_DPHA2,a_CENAS2
- Добавить ТОЛЬКО:
    a_PRICE            = a_CENAS
    a_PRICETAXGRANT    = a_CENAS1
    a_PRICENONTAXGRANT = a_CENAS2
- a_DPH оставляем без изменений.
- Сохраняем пробел перед первой '(' и промежутки между группами.
- Пишем UTF-8 без BOM; сохраняем исходный стиль EOL (CRLF/LF).
"""

from pathlib import Path
import re
import sys
import shutil

# ===================== НАСТРОЙКИ =====================
BASE_DIR = r"C:\septim.git\Septim4.limited\Septim\PgSQL\Proc"
FILE_GLOB = "*.tp"

INPLACE = True               # True -> правка файлов (создаст .bak)
BACKUP_EXT = ".bak"
ASSIGN_PER_LINE = 6          # пар name=value на строку при форматировании
PREVIEW_LIMIT = 800          # длина превью изменений в консоли

PRESERVE_EOL = True          # сохранить стиль перевода строк (CRLF/LF)
FORCE_UTF8_NO_BOM = True     # всегда писать UTF-8 без BOM
# =====================================================

START_RE = re.compile(r'(?im)^[ \t]*testprocex\s+RECCENIKInsert\b')

# Поля, которые надо удалить (ВНИМАНИЕ: a_DPH тут НЕТ — её не трогаем)
OLD_REMOVE = {
    'a_CENAB', 'a_DPHA', 'a_CENAS', 'a_DPH',
    'a_CENAB1', 'a_DPHA1', 'a_CENAS1',
    'a_CENAB2', 'a_DPHA2', 'a_CENAS2',
}


# ---------- I/O ----------
def sniff_text_and_meta(path: Path):
    raw = path.read_bytes()
    had_bom = raw.startswith(b'\xef\xbb\xbf')
    crlf = raw.count(b'\r\n')
    lf = raw.count(b'\n')
    eol = '\r\n' if crlf and crlf >= (lf - crlf) else '\n'
    if had_bom:
        return raw.decode('utf-8-sig'), 'utf-8', True, eol
    for enc in ('utf-8', 'cp1250', 'cp1251'):
        try:
            return raw.decode(enc), enc, False, eol
        except UnicodeDecodeError:
            continue
    return raw.decode('utf-8', errors='replace'), 'utf-8', False, eol


def write_text_smart(path: Path, text: str, enc_in: str, had_bom_in: bool, eol: str):
    t = text.replace('\r\n', '\n').replace('\r', '\n')
    if PRESERVE_EOL and eol == '\r\n':
        t = t.replace('\n', '\r\n')
    data = t.encode('utf-8') if FORCE_UTF8_NO_BOM else (
        t.encode('utf-8') if enc_in == 'utf-8' else t.encode(enc_in, errors='strict')
    )
    path.write_bytes(data)


# ---------- парсинг ----------
def consume_ws(src: str, i: int):
    n = len(src); j = i
    while j < n and src[j].isspace():
        j += 1
    return src[i:j], j


def extract_group(src: str, i: int):
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
                    out.append("''"); i += 2
                else:
                    out.append(ch); i += 1; in_str = False
            else:
                out.append(ch); i += 1
        else:
            if ch == "'":
                out.append(ch); i += 1; in_str = True
            elif ch == '(':
                depth += 1; out.append(ch); i += 1
            elif ch == ')':
                depth -= 1
                if depth == 0:
                    return ''.join(out).rstrip(), i + 1
                out.append(ch); i += 1
            else:
                out.append(ch); i += 1
    raise ValueError("Незакрытая скобочная группа")


def split_args(s: str):
    args, cur, in_str, i, n = [], [], False, 0, len(s)
    while i < n:
        ch = s[i]
        if in_str:
            if ch == "'":
                if i + 1 < n and s[i+1] == "'":
                    cur.append("''"); i += 2
                else:
                    cur.append(ch); i += 1; in_str = False
            else:
                cur.append(ch); i += 1
        else:
            if ch == "'":
                cur.append(ch); in_str = True; i += 1
            elif ch == ',':
                args.append(''.join(cur).strip()); cur = []; i += 1
            else:
                cur.append(ch); i += 1
    token = ''.join(cur).strip()
    if token != '' or s.endswith(','):
        args.append(token)
    return args


def parse_assignments(s: str):
    pairs = []
    for tok in split_args(s):
        if not tok:
            continue
        eq = tok.find('=')
        if eq < 0:
            pairs.append((tok.strip(), None))
        else:
            name = tok[:eq].strip()
            val  = tok[eq+1:].strip()
            pairs.append((name, val))
    return pairs


def join_assignments(pairs):
    items = [f"{k}={v}" for (k, v) in pairs if v is not None and k]
    if not items:
        return ''
    chunks = []
    for i in range(0, len(items), ASSIGN_PER_LINE):
        chunks.append(','.join(items[i:i+ASSIGN_PER_LINE]))
    indent = ' ' * 25
    return chunks[0] if len(chunks) == 1 else (',\n' + indent).join(chunks)


# ---------- трансформация ----------
def transform_assignments(pairs):
    """
    Удаляем старые ценовые поля и добавляем только новые a_PRICE/a_PRICETAXGRANT/a_PRICENONTAXGRANT.
    a_DPH остаётся нетронутым.
    Возвращает (new_pairs, changed_flag).
    """
    # первые значения по имени
    first_vals = {}
    for k, v in pairs:
        if k and v is not None and k not in first_vals:
            first_vals[k] = v

    has_old = any(k in first_vals for k in OLD_REMOVE)

    v_cenas  = first_vals.get('a_CENAS')
    v_cenas1 = first_vals.get('a_CENAS1')
    v_cenas2 = first_vals.get('a_CENAS2')

    want = {}
    if has_old:
        if 'a_PRICE' not in first_vals and v_cenas is not None:
            want['a_PRICE'] = v_cenas
        if 'a_PRICETAXGRANT' not in first_vals and v_cenas1 is not None:
            want['a_PRICETAXGRANT'] = v_cenas1
        if 'a_PRICENONTAXGRANT' not in first_vals and v_cenas2 is not None:
            want['a_PRICENONTAXGRANT'] = v_cenas2

    out = []
    added = set()

    for (k, v) in pairs:
        # выкидываем только то, что явно в OLD_REMOVE
        if k in OLD_REMOVE:
            continue
        # всё остальное сохраняем (включая a_DPH и любые другие параметры)
        out.append((k, v))
        if k:
            added.add(k)

    for k, v in want.items():
        if k not in added and v is not None:
            out.append((k, v))
            added.add(k)

    return out, has_old


# ---------- обработка одного блока ----------
def convert_one_block(src: str, start_idx: int):
    """
    Позиция сразу после 'testprocex RECCENIKInsert'.
    Сохраняем пробелы/переводы между именем и группами.
    """
    i = start_idx

    ws0, i = consume_ws(src, i)   # пробелы перед первой группой
    g1, i = extract_group(src, i) # присваивания

    # последующие группы + ведущие пробелы
    rest_ws, rest_gr = [], []
    while True:
        ws, j = consume_ws(src, i)
        try:
            g, j2 = extract_group(src, j)
        except Exception:
            i = j
            break
        rest_ws.append(ws)
        rest_gr.append(g)
        i = j2

    # хвост: пробелы + возможный '\'
    tail_m = re.match(r'[ \t]*\\?', src[i:])
    tail = tail_m.group(0) if tail_m else ''
    end_idx = i + (len(tail_m.group(0)) if tail_m else 0)

    # трансформация первой группы
    pairs = parse_assignments(g1)
    new_pairs, changed = transform_assignments(pairs)
    if not changed:
        # старых полей нет — возвращаем исходную «голову» без изменений
        original_tail = src[start_idx:end_idx]
        return original_tail, end_idx

    new_assign_txt = join_assignments(new_pairs)

    parts = []
    parts.append(ws0)
    parts.append('(' + new_assign_txt + ')')
    for ws, g in zip(rest_ws, rest_gr):
        parts.append(ws)
        parts.append('(' + g + ')')
    parts.append(tail)

    new_text = ''.join(parts)
    return new_text, end_idx


# ---------- проход по файлам ----------
def replace_in_text(fulltext: str):
    out, last = [], 0
    changes = []
    for m in START_RE.finditer(fulltext):
        out.append(fulltext[last:m.end()])
        try:
            new_tail, end_idx = convert_one_block(fulltext, m.end())
        except Exception:
            last = m.end()
            continue

        original_fragment = fulltext[m.start():end_idx]
        new_fragment = fulltext[m.start():m.end()] + new_tail

        if original_fragment != new_fragment:
            out.append(new_tail)
            changes.append((
                fulltext.count('\n', 0, m.start()) + 1,
                original_fragment[:PREVIEW_LIMIT].rstrip(),
                new_fragment[:PREVIEW_LIMIT].rstrip(),
            ))
        else:
            out.append(fulltext[m.end():end_idx])

        last = end_idx

    out.append(fulltext[last:])
    return ''.join(out), changes


def main():
    root = Path(BASE_DIR)
    if not root.exists():
        print(f"[ERR] BASE_DIR не найден: %s" % BASE_DIR, file=sys.stderr)
        sys.exit(1)

    total, touched, blocks = 0, 0, 0
    for p in root.rglob(FILE_GLOB):
        total += 1
        text, enc, had_bom, eol = sniff_text_and_meta(p)
        new_text, changes = replace_in_text(text)
        if not changes:
            continue

        touched += 1
        blocks += len(changes)
        print(f"\n=== {p} ===")
        for (line_no, was, became) in changes:
            print(f"[строка {line_no}] изменено:")
            print("— было:")
            print(was)
            print("— стало:")
            print(became)

        if INPLACE and new_text != text:
            bak = p.with_suffix(p.suffix + BACKUP_EXT)
            try:
                if not bak.exists():
                    shutil.copy2(p, bak)
                write_text_smart(p, new_text, enc, had_bom, eol)
                print(f"[ok] Обновлён: {p} (backup: {bak.name})")
            except Exception as e:
                print(f"[ERR] Не удалось записать {p}: {e}", file=sys.stderr)

    print(f"\nГотово. Просмотрено файлов: {total}. "
          f"Файлов с изменениями: {touched}. "
          f"Исправлено вызовов: {blocks}.")


if __name__ == '__main__':
    main()
