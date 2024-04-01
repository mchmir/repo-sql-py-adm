import csv
import re


def check_value_for_decimal(value):
    """
    Функция для проверки, содержит ли значение больше трех цифр после точки или запятой
    """
    # Используем регулярное выражение для проверки наличия более трех цифр после точки или запятой
    pattern = re.compile(r'[.,]\d{5,}')
    return bool(re.search(pattern, value))


def find_lines_with_numbers(file_path):
    """
    Функция для поиска и вывода номеров строк,
    содержащих значения с более чем тремя цифрами после точки или запятой
    """
    line_numbers = []
    with open(file_path, newline='', encoding='utf-8') as csvfile:
        reader = csv.reader(csvfile, delimiter='|')
        for i, row in enumerate(reader, 1):
            # Проверяем каждое значение в строке на соответствие критерию
            if any(check_value_for_decimal(value) for value in row):
                line_numbers.append(i)
    return line_numbers


# Пример файла для проверки
file_path = r"D:\2024\EIRC_GORGAZ_032024_56741.csv"


# Поиск и вывод номеров строк
lines_with_numbers = find_lines_with_numbers(file_path)
print(f'Номера строк с числами, имеющими более четырех знаков после запятой или точки: {lines_with_numbers}')
