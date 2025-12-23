# -*- coding: utf-8 -*-
"""
Универсальный конвертер параметров в вызовах:
  testprocex <PROC> (<assignments>) (<...>) ...

Конфиг для каждой процедуры:
- remove:        какие старые параметры удалить
- new_params:    порядок новых параметров для добавления
- map:           матрица переноса/задания значений новых параметров:
                 'from': [список источников по приоритету]  ИЛИ  'const': 'значение'
                 'force': True -> перезаписать существующее значение параметра
- trigger_old:   {'any_of':[имена]} — набор полей, наличие которых включает правила профиля;
                 если не задано — берём set(remove).
- add_when_no_old: если True — применять map даже если "старых" полей не найдено
- Прим.: значения в 'const' пишутся как токены SQL/PL: '1', 'null', 'False', "'строка'"

Формат сохраняет пробел между именем процедуры и первой '(' и пробелы/переносы между группами.
Запись: UTF-8 без BOM. EOL сохраняем (CRLF/LF).


--- Как настроить функцию
PROFILES["MYPROC"] = {
    "remove": {"a_OLD1","a_OLD2"},
    "new_params": ["a_NEW1","a_NEW2","a_NEW3"],
    "map": {
        "a_NEW1": {"from": ["a_OLD1"]},     # возьмёт первое найденное
        "a_NEW2": {"const": "'abc'"},       # константа (не забудьте кавычки для строк)
        "a_NEW3": {"from": ["a_OLDX","a_OLDY"], "force": True},  # перезапишет существующее
    },
    "add_when_no_old": False,               # добавлять только если встречены "старые"
    "trigger_old": {"any_of": ["a_OLD1","a_OLD2"]},  # опционально, иначе = remove
}

Чтобы обработать только одну функцию, укажите:
ACTIVE_PROCS = ["RECCENIKInsert"]   # либо ["RECEPTInsert"]
START_RE = compile_start_re(ACTIVE_PROCS)
Если захотите автодобавление новых параметров даже без старых полей (с константами) — поставьте add_when_no_old=True в профиле.

"""

from pathlib import Path
import re
import sys
import shutil

# ===================== НАСТРОЙКИ =====================
BASE_DIR = r"C:\septim.git\Septim4.limited\Septim\PgSQL\Proc"
FILE_GLOB = "*.tp"

INPLACE = False               # True -> править файлы (создаст .bak)
BACKUP_EXT = ".bak"
ASSIGN_PER_LINE = 6           # пар name=value на строку при форматировании
PREVIEW_LIMIT = 800           # длина превью изменений в консоли

PRESERVE_EOL = True           # сохранить стиль перевода строк (CRLF/LF)
FORCE_UTF8_NO_BOM = True      # всегда писать UTF-8 без BOM

# Какие профили активировать: None -> все из PROFILES; либо список имён
ACTIVE_PROCS = None
# =====================================================

# --------- ПРОФИЛИ (пример) ---------
PROFILES = {
    # Профиль для RECEPTInsert (пример: перенос налогов и цен, как раньше)
    "RECEPTInsert": {
        "remove": {
            "a_CENAB", "a_DPHA", "a_CENAS",
            "a_CENAB1", "a_DPHA1", "a_CENAS1",
            "a_CENAB2", "a_DPHA2", "a_CENAS2",
            "a_DPH",  # в этом профиле удаляем и a_DPH
        },
        "new_params": [
            "a_TAXCATEGORY", "a_PRICE", "a_WITHTAX",
            "a_PRICETAXGRANT", "a_PRICENONTAXGRANT"
        ],
        "map": {
            "a_TAXCATEGORY":      {"from": ["a_DPH"]},            # DPH -> TAXCATEGORY
            "a_PRICE":            {"from": ["a_CENAS"]},          # CENAS -> PRICE
            "a_WITHTAX":          {"const": "1", "force": True},  # всегда =1, даже если уже есть
            "a_PRICETAXGRANT":    {"from": ["a_CENAS1"]},         # CENAS1 -> PRICETAXGRANT
            "a_PRICENONTAXGRANT": {"from": ["a_CENAS2"]},         # CENAS2 -> PRICENONTAXGRANT
        },
        # Применять правила, только если встретились "старые" поля:
        "add_when_no_old": False,
        # Что считаем "старыми" полями (если опустить — возьмём remove)
        "trigger_old": {"any_of": [
            "a_CENAB", "a_DPHA", "a_CENAS",
            "a_CENAB1", "a_DPHA1", "a_CENAS1",
            "a_CENAB2", "a_DPHA2", "a_CENAS2", "a_DPH"
        ]},
    },

    # Профиль для RECCENIKInsert (пример из вашего ТЗ)
    "RECCENIKInsert": {
        "remove": {
            "a_CENAB", "a_DPHA", "a_CENAS",
            "a_CENAB1", "a_DPHA1", "a_CENAS1",
            "a_CENAB2", "a_DPHA2", "a_CENAS2", "a_DPH"
        },
        "new_params": [
            "a_PRICE", "a_PRICETAXGRANT", "a_PRICENONTAXGRANT"
        ],
        "map": {
            "a_PRICE":            {"from": ["a_CENAS"]},
            "a_PRICETAXGRANT":    {"from": ["a_CENAS1"]},
            "a_PRICENONTAXGRANT": {"from": ["a_CENAS2"]},
        },
        "add_when_no_old": False,
        "trigger_old": {"any_of": [
            "a_CENAB", "a_DPHA", "a_CENAS",
            "a_CENAB1", "a_DPHA1", "a_CENAS1",
            "a_CENAB2", "a_DPHA2", "a_CENAS2"
        ]},
    },
}
# -----------------------------------


def compile_start_re(active_names):
    names = list(PROFILES.keys()) if active_names is None else list(active_names)
    alt = "|".join(re.escape(n) for n in names)
    return re.compile(rf"(?im)^[ \t]*testprocex\s+(?P<proc>{alt})\b")


START_RE = compile_start_re(ACTIVE_PROCS)


# ---------- I/O ----------
def sniff_text_and_meta(path: Path):
    raw = path.read_bytes()
    had_bom = raw.startswith(b"\xef\xbb\xbf")
    crlf = raw.count(b"\r\n")
    lf = raw.count(b"\n")
    eol = "\r\n" if crlf and crlf >= (lf - crlf) else "\n"
    if had_bom:
        return raw.decode("utf-8-sig"), "utf-8", True, eol
    for enc in ("utf-8", "cp1250", "cp1251"):
        try:
            return raw.decode(enc), enc, False, eol
        except UnicodeDecodeError:
            continue
    return raw.decode("utf-8", errors="replace"), "utf-8", False, eol


def write_text_smart(path: Path, text: str, enc_in: str, had_bom_in: bool, eol: str):
    t = text.replace("\r\n", "\n").replace("\r", "\n")
    if PRESERVE_EOL and eol == "\r\n":
        t = t.replace("\n", "\r\n")
    data = t.encode("utf-8") if FORCE_UTF8_NO_BOM else (
        t.encode("utf-8") if enc_in == "utf-8" else t.encode(enc_in, errors="strict")
    )
    path.write_bytes(data)


# ---------- утилиты парсинга ----------
def consume_ws(src: str, i: int):
    n = len(src)
    j = i
    while j < n and src[j].isspace():
        j += 1
    return src[i:j], j


def extract_group(src: str, i: int):
    n = len(src)
    while i < n and src[i].isspace():
        i += 1
    if i >= n or src[i] != "(":
        raise ValueError("Ожидалась '(' при разборе группы")
    i += 1
    depth = 1
    in_str = False
    out = []
    while i < n:
        ch = src[i]
        if in_str:
            if ch == "'":
                if i + 1 < n and src[i + 1] == "'":
                    out.append("''"); i += 2
                else:
                    out.append(ch); i += 1; in_str = False
            else:
                out.append(ch); i += 1
        else:
            if ch == "'":
                out.append(ch); i += 1; in_str = True
            elif ch == "(":
                depth += 1; out.append(ch); i += 1
            elif ch == ")":
                depth -= 1
                if depth == 0:
                    return "".join(out).rstrip(), i + 1
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
                if i + 1 < n and s[i + 1] == "'":
                    cur.append("''"); i += 2
                else:
                    cur.append(ch); i += 1; in_str = False
            else:
                cur.append(ch); i += 1
        else:
            if ch == "'":
                cur.append(ch); in_str = True; i += 1
            elif ch == ",":
                args.append("".join(cur).strip()); cur = []; i += 1
            else:
                cur.append(ch); i += 1
    token = "".join(cur).strip()
    if token != "" or s.endswith(","):
        args.append(token)
    return args


def parse_assignments(s: str):
    pairs = []
    for tok in split_args(s):
        if not tok:
            continue
        eq = tok.find("=")
        if eq < 0:
            pairs.append((tok.strip(), None))
        else:
            name = tok[:eq].strip()
            val = tok[eq + 1:].strip()
            pairs.append((name, val))
    return pairs


def join_assignments(pairs):
    items = [f"{k}={v}" for (k, v) in pairs if v is not None and k]
    if not items:
        return ""
    chunks = []
    for i in range(0, len(items), ASSIGN_PER_LINE):
        chunks.append(",".join(items[i:i + ASSIGN_PER_LINE]))
    indent = " " * 25
    return chunks[0] if len(chunks) == 1 else (",\n" + indent).join(chunks)


# ---------- логика профилей ----------
def _first_val(name_order, first_vals):
    """Возвращает значение первого найденного из списка имён."""
    if not name_order:
        return None
    for k in name_order:
        if k in first_vals:
            return first_vals[k]
    return None


def transform_assignments_with_profile(pairs, profile):
    """
    Основная трансформация по профилю.
    Возвращает (new_pairs, changed_flag).
    """
    remove = set(profile.get("remove", []))
    new_params_order = list(profile.get("new_params", []))
    mapping = dict(profile.get("map", {}))
    add_when_no_old = bool(profile.get("add_when_no_old", False))

    trigger_cfg = profile.get("trigger_old")
    if trigger_cfg and "any_of" in trigger_cfg:
        trigger_set = set(trigger_cfg["any_of"])
    else:
        trigger_set = remove

    # соберём первые значения по имени
    first_vals = {}
    for k, v in pairs:
        if k and v is not None and k not in first_vals:
            first_vals[k] = v

    has_old = any(k in first_vals for k in trigger_set)

    # подсчитаем целевые значения для новых параметров
    target_vals = {}   # new_name -> value (строка токена)
    for new_name, rule in mapping.items():
        if not (has_old or add_when_no_old):
            # правила не активируем, если старых нет и так настроено
            continue
        # вычисляем значение
        val = None
        if "from" in rule:
            val = _first_val(rule["from"], first_vals)
        if val is None and "const" in rule:
            val = rule["const"]
        # если значение получено — считаем целевым
        if val is not None:
            target_vals[new_name] = (val, bool(rule.get("force", False)))

    changed = False
    out = []
    seen = set()

    # первый проход: копируем всё, удаляя старые; при необходимости перезаписываем force-параметры
    for (k, v) in pairs:
        if k in remove:
            changed = True
            continue  # выкинули старое поле

        # если это один из настраиваемых новых параметров и для него есть target с force=True
        if k in target_vals and target_vals[k][1]:  # force
            new_val = target_vals[k][0]
            if v != new_val:
                changed = True
            out.append((k, new_val))
            seen.add(k)
            continue

        # обычный параметр — оставляем как есть
        out.append((k, v))
        if k:
            seen.add(k)

    # второй проход: добавить недостающие новые параметры (без force или если их вовсе не было)
    # соблюдаем порядок new_params_order; добавляем только те, что вычислены
    for new_name in new_params_order:
        if new_name in target_vals and new_name not in seen:
            out.append((new_name, target_vals[new_name][0]))
            seen.add(new_name)
            changed = True

    return out, changed


# ---------- обработка одного вызова ----------
def convert_one_block(src: str, start_idx: int, profile):
    """
    Позиция сразу после 'testprocex <PROC>'.
    Сохраняем пробелы/переводы между именем и группами.
    Возвращает (new_tail_text, end_idx).
    """
    i = start_idx

    # пробелы перед первой группой
    ws0, i = consume_ws(src, i)

    # первая группа — присваивания
    g1, i = extract_group(src, i)

    # последующие группы + их ведущие пробелы
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

    # хвост: пробелы + необязательный '\'
    tail_m = re.match(r"[ \t]*\\?", src[i:])
    tail = tail_m.group(0) if tail_m else ""
    end_idx = i + (len(tail_m.group(0)) if tail_m else 0)

    # трансформация
    pairs = parse_assignments(g1)
    new_pairs, changed = transform_assignments_with_profile(pairs, profile)

    if not changed:
        # ничего не меняем
        return src[start_idx:end_idx], end_idx

    new_assign_txt = join_assignments(new_pairs)

    parts = []
    parts.append(ws0)
    parts.append("(" + new_assign_txt + ")")
    for ws, g in zip(rest_ws, rest_gr):
        parts.append(ws)
        parts.append("(" + g + ")")
    parts.append(tail)

    new_text = "".join(parts)
    return new_text, end_idx


# ---------- проход по файлам ----------
def replace_in_text(fulltext: str):
    out, last = [], 0
    changes = []
    for m in START_RE.finditer(fulltext):
        proc_name = m.group("proc")
        profile = PROFILES.get(proc_name)
        if not profile:
            continue

        out.append(fulltext[last:m.end()])  # включаем заголовок до конца имени

        try:
            new_tail, end_idx = convert_one_block(fulltext, m.end(), profile)
        except Exception:
            last = m.end()
            continue

        original_fragment = fulltext[m.start():end_idx]
        new_fragment = fulltext[m.start():m.end()] + new_tail

        if original_fragment != new_fragment:
            out.append(new_tail)
            changes.append((
                fulltext.count("\n", 0, m.start()) + 1,
                original_fragment[:PREVIEW_LIMIT].rstrip(),
                new_fragment[:PREVIEW_LIMIT].rstrip(),
            ))
        else:
            out.append(fulltext[m.end():end_idx])

        last = end_idx

    out.append(fulltext[last:])
    return "".join(out), changes


def main():
    root = Path(BASE_DIR)
    if not root.exists():
        print(f"[ERR] BASE_DIR не найден: {BASE_DIR}", file=sys.stderr)
        sys.exit(1)

    total, touched, calls = 0, 0, 0
    for p in root.rglob(FILE_GLOB):
        total += 1
        text, enc, had_bom, eol = sniff_text_and_meta(p)
        new_text, changes = replace_in_text(text)
        if not changes:
            continue

        touched += 1
        calls += len(changes)
        print(f"\n=== {p} ===")
        for (line_no, was, became) in changes:
            print(f"[строка {line_no}] изменено:")
            print("— было:");  print(was)
            print("— стало:"); print(became)

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
          f"Исправлено вызовов: {calls}.")


if __name__ == "__main__":
    main()
