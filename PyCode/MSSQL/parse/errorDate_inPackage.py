from datetime import datetime
import re

# Укажите путь к файлу
file_path = r"C:\Users\User\Desktop\PoveritelWORK\ГазМонтажПоверка № 238.txt"

# Попробуем использовать кодировку cp1251 (или другую подходящую)
with open(file_path, 'r', encoding='cp1251') as file:
    data = file.readlines()

# Регулярное выражение для поиска всех дат в формате ДД.ММ.ГГГГ или ДДММГГГГ
date_pattern = r'\b\d{1,2}\.?\d{1,2}\.?\d{4}\b'


# Функция для проверки валидности даты
def is_valid_date(date_str):
    # Добавляем точки между элементами даты, если они отсутствуют
    if len(date_str) == 8 and not '.' in date_str:
        date_str = date_str[:2] + '.' + date_str[2:4] + '.' + date_str[4:]

    try:
        parsed_date = datetime.strptime(date_str, '%d.%m.%Y')
        # Проверяем, что год больше 1900 и меньше или равен текущему
        if parsed_date.year < 1900 or parsed_date.year > datetime.now().year:
            return False
        return True
    except ValueError:
        return False


# Список для хранения некорректных дат
invalid_dates = []

# Проходим по каждой строке файла
for line_number, line in enumerate(data, start=1):  # Нумеруем строки начиная с 1
    columns = line.split('¶')  # Разделяем строку по разделителю ¶

    # Если колонок меньше, чем требуется, пропускаем строку
    if len(columns) < 14:
        continue

    # Определяем, какой это тип строки - 'document' или 'gmeter'
    if columns[2] == 'document':
        # Даты находятся в 9 и 14 столбцах (с учетом возможных пустых колонок)
        date_1 = columns[8].strip()  # 9 элемент
        date_2 = columns[13].strip()  # 14 элемент
        print(date_1, date_2)
        # Проверяем, что это дата по регулярному выражению
        if date_1 and re.match(date_pattern, date_1):
            # Проверяем валидность даты
            if not is_valid_date(date_1):
                invalid_dates.append((date_1, line.strip(), line_number))
        else:
            invalid_dates.append((f"'{date_1}' is not a valid date", line.strip(), line_number))

        if date_2 and re.match(date_pattern, date_2):
            # Проверяем валидность даты
            if not is_valid_date(date_2):
                invalid_dates.append((date_2, line.strip(), line_number))
        else:
            invalid_dates.append((f"'{date_2}' is not a valid date", line.strip(), line_number))

    elif columns[2] == 'gmeter':
        # Даты находятся в 10, 11 и 13 столбцах (с учетом возможных пустых колонок)
        date_1 = columns[9].strip()  # 10 элемент
        date_2 = columns[10].strip()  # 11 элемент
        date_3 = columns[12].strip()  # 13 элемент
        print(date_1, date_2, date_3)
        # Проверяем даты по регулярному выражению и валидность
        if date_1 and re.match(date_pattern, date_1):
            if not is_valid_date(date_1):
                invalid_dates.append((date_1, line.strip(), line_number))
        else:
            invalid_dates.append((f"'{date_1}' is not a valid date", line.strip(), line_number))

        if date_2 and re.match(date_pattern, date_2):
            if not is_valid_date(date_2):
                invalid_dates.append((date_2, line.strip(), line_number))
        else:
            invalid_dates.append((f"'{date_2}' is not a valid date", line.strip(), line_number))

        if date_3 and re.match(date_pattern, date_3):
            if not is_valid_date(date_3):
                invalid_dates.append((date_3, line.strip(), line_number))
        else:
            invalid_dates.append((f"'{date_3}' is not a valid date", line.strip(), line_number))

# Выводим все некорректные даты и строки, где они найдены
if invalid_dates:
    print("Некорректные даты найдены:")
    for date, line, line_number in invalid_dates:
        print(f"Дата: {date} в строке {line_number}: {line}")
else:
    print("Все даты корректны.")
