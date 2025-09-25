#!/usr/bin/env python3
# -*- coding: utf-8 -*-

"""
Консольный помощник: конвертация оснований чисел (включая дроби), парсинг/форматирование
размеров, конвертация сетевых скоростей и простые операции с битовыми масками.

Подкоманды:
  • base — перевод чисел между основаниями (2..36), поддерживает дробную часть
  • size — работа с размерами: parse (строка → байты) и format (байты → человекочит.)
  • rate — конвертация скоростей (бит/байт в сек., SI и IEC варианты)
  • mask — просмотр/тест/модификация отдельных битов целого

Особенности:
  • Разрешён символ подчёркивания '_' в числах (для удобства чтения).
  • Для base указывается исходное и целевое основание; дробная точность управляется --prec.
  • Для size поддерживаются как десятичные единицы (KB/MB/GB/…) так и двоичные (KiB/MiB/…),
    а также биты (b, kb, mb, …) — они автоматически переводятся в байты.
  • Для rate доступны синонимы вида 'Mbps', 'MB/s', 'MiB/s', 'kb/s' и т.д.

Быстрые примеры:
  # Перевод основания
  $ %(prog)s base --from 10 --to 2 26.625
  11010.101

  # Парсинг размера
  $ %(prog)s size parse "256 MiB"
  268435456 B

  # Форматирование размера (IEC по умолчанию: KiB/MiB/…)
  $ %(prog)s size format 268435456
  256.00 MiB

  # Форматирование в SI (KB/MB/…)
  $ %(prog)s size format 268435456 --si
  268.44 MB

  # Конвертация скорости
  $ %(prog)s rate 100 --from Mbps --to MiB/s
  11.920928955078125 MiB/s

  # Маски: показать в разных системах
  $ %(prog)s mask show 0xff
  dec=255
  hex=0xFF
  bin=0b11111111

  # Проверить бит №1 (считая от 0 справа)
  $ %(prog)s mask test 0b1010 1
  1

  # Установить/снять/инвертировать бит
  $ %(prog)s mask set    0b1010 0
  $ %(prog)s mask clear  0b1010 3
  $ %(prog)s mask toggle 0b1010 1

Замечания по точности:
  • Для операций с основанием используется Decimal с контекстной точностью 80.
  • Параметр --prec (по умолчанию 16) управляет длиной дробной части в целевом основании.

Авторские синонимы единиц:
  • size: 'kib'→'KiB', 'kb'→'KB', 'mb'→'MB', … ; 'bps'→'b', 'kbps'→'kb' и т.п.
  • rate: поддерживаются варианты с '/s' и формы 'Mbps', 'MiB/s', 'KB/s' и т.д.
"""

import argparse
import re
import string
from decimal import Decimal, getcontext

DIGITS = string.digits + string.ascii_uppercase  # 0-9A-Z


# ---------- base convert (supports fractions) ----------
def char_to_val(ch: str) -> int:
    """
    Преобразует символ цифры к её числовому значению (0..35).

    Параметры:
      ch (str): Одна буква/цифра. Буквы допускаются в любом регистре.

    Возвращает:
      int: Значение цифры (A→10, B→11, …, Z→35).

    Исключения:
      ValueError: если символ не является допустимой цифрой для оснований 2..36.
    """
    ch = ch.upper()
    if ch not in DIGITS:
        raise ValueError(f"Недопустимый символ: {ch}")
    return DIGITS.index(ch)


def parse_in_base(num: str, base: int) -> Decimal:
    """
    Разбирает строковое число в заданном основании base (2..36) и возвращает Decimal.

    Поддерживается:
      • Знак ('+' | '-')
      • Дробная часть через точку '.'
      • Пробелы по краям и подчёркивания '_' внутри числа (игнорируются)

    Примеры:
      parse_in_base("26.625", 10) →  Decimal("26.625")
      parse_in_base("1A.F", 16)   →  Decimal(26.9375)

    Параметры:
      num (str): Строковое представление числа.
      base (int): Основание (2..36).

    Возвращает:
      Decimal: Число в десятичном виде.

    Исключения:
      ValueError: при недопустимом основании или цифре, превышающей основание.
    """
    if not (2 <= base <= 36):
        raise ValueError("Основание должно быть в диапазоне 2..36")
    num = num.strip().upper().replace("_", "")
    sign = -1 if num.startswith("-") else 1
    if num.startswith(("+", "-")):
        num = num[1:]
    if "." in num:
        int_part, frac_part = num.split(".", 1)
    else:
        int_part, frac_part = num, ""
    # integer
    acc = Decimal(0)
    for ch in int_part or "0":
        v = char_to_val(ch)
        if v >= base: raise ValueError(f"Цифра {ch} >= основанию {base}")
        acc = acc * base + v
    # fraction
    if frac_part:
        frac = Decimal(0)
        power = Decimal(base)
        denom = Decimal(1)
        for ch in frac_part:
            v = char_to_val(ch)
            if v >= base: raise ValueError(f"Цифра {ch} >= основанию {base}")
            denom *= power
            frac += Decimal(v) / denom
        acc += frac
    return acc * sign


def to_base(val: Decimal, base: int, precision: int = 16) -> str:
    """
    Конвертирует десятичное значение Decimal в строку указанного основания (2..36).

    Параметры:
      val (Decimal): Значение в десятичной системе.
      base (int): Целевое основание (2..36).
      precision (int): Максимальная длина дробной части. Хвостовые нули обрезаются.

    Возвращает:
      str: Представление числа в целевом основании. Дробная часть отделена '.'.

    Исключения:
      ValueError: если основание вне диапазона 2..36.

    Пример:
      to_base(Decimal("26.625"), 2, precision=8) → "11010.101"
    """
    if not (2 <= base <= 36):
        raise ValueError("Основание должно быть в диапазоне 2..36")
    sign = "-" if val < 0 else ""
    val = abs(val)

    # integer part
    i = int(val)
    if i == 0:
        int_str = "0"
    else:
        chunks = []
        while i > 0:
            i, r = divmod(i, base)
            chunks.append(DIGITS[int(r)])
        int_str = "".join(reversed(chunks))

    # fractional part
    frac = val - int(val)
    if precision <= 0 or frac == 0:
        return sign + int_str

    out = []
    for _ in range(precision):
        frac *= base
        d = int(frac)
        out.append(DIGITS[d])
        frac -= d
        if frac == 0:
            break
    # убрать хвостовые нули
    while out and out[-1] == "0":
        out.pop()
    return sign + int_str + ("." + "".join(out) if out else "")


# ---------- sizes ----------
MULT = {
    # bits (to bytes)
    'b': 1 / 8, 'kb': 1e3 / 8, 'mb': 1e6 / 8, 'gb': 1e9 / 8, 'tb': 1e12 / 8,
    # decimal bytes
    'B': 1, 'KB': 1000, 'MB': 1000 ** 2, 'GB': 1000 ** 3, 'TB': 1000 ** 4,
    # binary bytes
    'KiB': 1024, 'MiB': 1024 ** 2, 'GiB': 1024 ** 3, 'TiB': 1024 ** 4,
}
ALIASES = {
    'kib': 'KiB', 'mib': 'MiB', 'gib': 'GiB', 'tib': 'TiB',
    'kb': 'KB', 'mb': 'MB', 'gb': 'GB', 'tb': 'TB',
    'bps': 'b', 'kbps': 'kb', 'mbps': 'mb', 'gbps': 'gb', 'tbps': 'tb'
}


def normalize_unit(u: str) -> str:
    """
    Нормализует обозначение единицы размера (KiB/MiB/…; KB/MB/…; b/kb/…).

    Параметры:
      u (str): Введённая пользователем единица. Регистр и некоторые алиасы игнорируются.

    Возвращает:
      str: Стандартизованное обозначение единицы (с учётом ALIASES).

    Пример:
      'kib' → 'KiB'; 'kb' → 'KB'; 'mbps' → 'mb' (для size — считается мегабит).
    """
    u = u.strip()
    u = ALIASES.get(u.lower(), u)
    if u == 'B': return 'B'
    # keep case for KiB/MiB...
    return u


SIZE_RE = re.compile(r'^\s*([0-9]+(?:\.[0-9]+)?)\s*([A-Za-z/]+)\s*$')


def parse_size(text: str) -> int:
    """
    Парсит строку вида '<число><единица>' и возвращает количество байт (int).

    Поддерживаются:
      • Десятичные байты: B, KB, MB, GB, TB
      • Двоичные байты:  KiB, MiB, GiB, TiB
      • Биты: b, kb, mb, gb, tb  (конвертируются в байты / деление на 8)
      • Алиасы: 'kib'→'KiB', 'kbps'→'kb' и т.д.

    Примеры:
      parse_size("256MiB")    → 268435456
      parse_size("1.5 GB")    → 1500000000
      parse_size("100 mb")    → 12500000     # 100 мегабит → байты

    Важно:
      • Для скоростей используйте подкоманду 'rate', а не 'size parse'.

    Исключения:
      ValueError: при неверном формате или неизвестной единице.
    """
    m = SIZE_RE.match(text)
    if not m:
        raise ValueError("Формат: <число><ед.> например '256MiB', '10 MB', '100Mb/s'")
    val, unit = Decimal(m.group(1)), normalize_unit(m.group(2))
    unit = unit.replace('ib', 'iB') if unit.lower().endswith('ib') else unit
    if unit not in MULT:
        raise ValueError(f"Неизвестная единица: {unit}")
    return int(val * Decimal(MULT[unit]))


def fmt_bytes(n: int, iec: bool = True, width: int = 2) -> str:
    """
    Форматирует количество байт в человекочитаемый вид.

    Параметры:
      n (int): Количество байт.
      iec (bool): True → KiB/MiB/GiB/TiB; False → KB/MB/GB/TB.
      width (int): Количество знаков после запятой.

    Возвращает:
      str: Форматированная строка, например '256.00 MiB' или '268.44 MB'.
    """
    if iec:
        steps = [('B', 1), ('KiB', 1024), ('MiB', 1024 ** 2), ('GiB', 1024 ** 3), ('TiB', 1024 ** 4)]
    else:
        steps = [('B', 1), ('KB', 1000), ('MB', 1000 ** 2), ('GB', 1000 ** 3), ('TB', 1000 ** 4)]
    for i in range(len(steps) - 1, -1, -1):
        name, m = steps[i]
        if n >= m:
            return f"{n / m:.{width}f} {name}"
    return f"{n} B"


# ---------- rates ----------
RATE_UNITS = {
    'b/s': 1, 'bit/s': 1, 'bps': 1,
    'kb/s': 1e3, 'mb/s': 1e6, 'gb/s': 1e9, 'tb/s': 1e12,
    'B/s': 8, 'KB/s': 8 * 1000, 'MB/s': 8 * 1000 ** 2, 'GB/s': 8 * 1000 ** 3, 'TB/s': 8 * 1000 ** 4,
    'KiB/s': 8 * 1024, 'MiB/s': 8 * 1024 ** 2, 'GiB/s': 8 * 1024 ** 3, 'TiB/s': 8 * 1024 ** 4,
    'Mbps': 1e6, 'Gbps': 1e9, 'Kbps': 1e3, 'Tbps': 1e12,
}


def rate_to_bps(value: Decimal, unit: str) -> Decimal:
    """
    Конвертирует скорость из заданной единицы в бит/с (bps).

    Параметры:
      value (Decimal): Числовое значение скорости.
      unit (str): Единица из множества RATE_UNITS (напр. 'Mbps', 'MB/s', 'KiB/s', 'kb/s').

    Возвращает:
      Decimal: Скорость в битах в секунду (bps).

    Исключения:
      ValueError: при неизвестной единице.
    """
    unit = unit.strip()
    if unit not in RATE_UNITS:
        raise ValueError(f"Неизвестная единица скорости: {unit}")
    return value * Decimal(RATE_UNITS[unit])


def rate_from_bps(bps: Decimal, unit: str) -> Decimal:
    """
    Конвертирует скорость из бит/с (bps) в указанную единицу.

    Параметры:
      bps (Decimal): Скорость в бит/с.
      unit (str): Целевая единица из множества RATE_UNITS.

    Возвращает:
      Decimal: Скорость в целевой единице.

    Исключения:
      ValueError: при неизвестной единице.
    """
    unit = unit.strip()
    if unit not in RATE_UNITS:
        raise ValueError(f"Неизвестная единица скорости: {unit}")
    return bps / Decimal(RATE_UNITS[unit])


# ---------- bit masks ----------
def parse_int_auto(s: str) -> int:
    """
    Парсит целое в формате:
      • '0x..' — шестнадцатеричное
      • '0b..' — двоичное
      • '0o..' — восьмеричное
      • иначе — десятичное

    Подчёркивания '_' игнорируются, регистр не важен.

    Пример:
      parse_int_auto("0b1010_0001") → 161
    """
    s = s.strip().lower().replace("_", "")
    if s.startswith("0x"): return int(s, 16)
    if s.startswith("0b"): return int(s, 2)
    if s.startswith("0o"): return int(s, 8)
    return int(s, 10)


def show_formats(x: int) -> str:
    """
    Возвращает строку с представлением числа в десятичной, шестнадцатеричной и двоичной системах.

    Пример вывода:
      dec=255
      hex=0xFF
      bin=0b11111111
    """
    return f"dec={x}\nhex=0x{x:X}\nbin=0b{format(x, 'b')}\n"


# ---------- CLI ----------
def main():
    parser = argparse.ArgumentParser(description="Base/size/rate/mask helper")
    sub = parser.add_subparsers(dest="cmd", required=True)

    # base convert
    p_base = sub.add_parser("base", help="Перевод оснований (с дробями)")
    p_base.add_argument("value", help="Напр. 26.625, 0x2A.4 нет; основание задаётся флагами")
    p_base.add_argument("--from", dest="base_from", type=int, required=True)
    p_base.add_argument("--to", dest="base_to", type=int, required=True)
    p_base.add_argument("--prec", type=int, default=16, help="точность дробной части (по умолчанию 16)")
    # sizes
    p_sz = sub.add_parser("size", help="Размеры: parse/format")
    ss = p_sz.add_subparsers(dest="scmd", required=True)
    p_parse = ss.add_parser("parse", help="Парсит строку и выдаёт байты")
    p_parse.add_argument("text", help="Напр. '256MiB', '100 Mb', '1.2 GB'")
    p_fmt = ss.add_parser("format", help="Форматирует байты в удобный вид")
    p_fmt.add_argument("bytes", type=int)
    p_fmt.add_argument("--iec", action="store_true", help="Показывать в KiB/MiB/…")
    p_fmt.add_argument("--si", action="store_true", help="Показывать в KB/MB/…")
    p_fmt.add_argument("--prec", type=int, default=2)

    # rates
    p_rate = sub.add_parser("rate", help="Конвертер скоростей")
    p_rate.add_argument("value", type=str, help="Число, напр. 100")
    p_rate.add_argument("--from", dest="rfrom", required=True, help="Напр. Mbps, MB/s, MiB/s")
    p_rate.add_argument("--to", dest="rto", required=True, help="Куда конвертировать")

    # masks
    p_mask = sub.add_parser("mask", help="Битовые операции")
    sm = p_mask.add_subparsers(dest="mcmd", required=True)
    p_show = sm.add_parser("show")
    p_show.add_argument("value")
    p_test = sm.add_parser("test")
    p_test.add_argument("value")
    p_test.add_argument("bit", type=int)
    p_set = sm.add_parser("set")
    p_set.add_argument("value")
    p_set.add_argument("bit", type=int)
    p_clear = sm.add_parser("clear")
    p_clear.add_argument("value")
    p_clear.add_argument("bit", type=int)
    p_toggle = sm.add_parser("toggle")
    p_toggle.add_argument("value")
    p_toggle.add_argument("bit", type=int)

    args = parser.parse_args()
    getcontext().prec = 80  # точность для Decimal

    if args.cmd == "base":
        v10 = parse_in_base(args.value, args.base_from)
        print(to_base(v10, args.base_to, args.prec))

    elif args.cmd == "size":
        if args.scmd == "parse":
            b = parse_size(args.text)
            print(f"{b} B")
        else:
            iec = args.iec or not args.si
            print(fmt_bytes(args.bytes, iec=iec, width=args.prec))

    elif args.cmd == "rate":
        bps = rate_to_bps(Decimal(args.value), args.rfrom)
        out = rate_from_bps(bps, args.rto)
        print(f"{out.normalize()} {args.rto}")

    elif args.cmd == "mask":
        if args.mcmd == "show":
            x = parse_int_auto(args.value)
            print(show_formats(x))
        else:
            x = parse_int_auto(args.value)
            bit = args.bit
            if bit < 0: raise SystemExit("bit >= 0")
            if args.mcmd == "test":
                print("1" if (x >> bit) & 1 else "0")
            elif args.mcmd == "set":
                print(show_formats(x | (1 << bit)))
            elif args.mcmd == "clear":
                print(show_formats(x & ~(1 << bit)))
            elif args.mcmd == "toggle":
                print(show_formats(x ^ (1 << bit)))


if __name__ == "__main__":
    main()
