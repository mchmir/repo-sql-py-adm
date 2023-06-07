import os
import re


def parse_insert_string(input_string):
    """
    Функция для преобразования строки INSERT в строку INSERT с проверкой EXISTS
    """
    # Удаление начальных и конечных пробелов и символов новой строки
    input_string = input_string.strip()

    # Разбираем входную строку
    match = re.match(r"insert Gmeter \((.*)\) values \((.*)\)", input_string, re.I)
    if match:
        columns = [column.strip() for column in match.group(1).split(',')]
        values = [value.strip() for value in match.group(2).split(',')]

        # Находим индексы ключевых полей
        idstatusgmeter_index = columns.index('IDStatusGMeter')
        idtypegmeter_index = columns.index('IDTypeGMeter')
        idcontract_index = columns.index('IDContract')

        # Создаем строку условия для EXISTS
        condition = f"IDStatusGMeter = {values[idstatusgmeter_index]} AND IDTypeGMeter = {values[idtypegmeter_index]} AND IDContract = {values[idcontract_index]}"

        # Формируем и возвращаем новую SQL строку
        new_sql = f"BEGIN TRY IF NOT EXISTS (SELECT 1 FROM Gmeter WHERE {condition}) BEGIN {input_string} END END TRY BEGIN CATCH END CATCH \n"
        return new_sql


def transform_insert_lines(input_filename):
    # Создаем имя выходного файла, добавляя префикс "parse_" к входному файлу
    output_filename = os.path.dirname(input_filename) + os.sep + 'parse_' + os.path.basename(input_filename)

    # Открываем исходный файл для чтения и выходной файл для записи
    with open(input_filename, 'r') as infile, open(output_filename, 'w', encoding='windows-1251') as outfile:
        # Читаем исходный файл построчно
        for line in infile:
            # Если строка начинается с 'insert', преобразуем ее
            if line.lower().startswith('insert'):

                # Формируем новую строку SQL
                new_line = parse_insert_string(line)
                # Записываем новую строку в выходной файл
                outfile.write(new_line)


# DBCC DBREINDEX ('Gmeter','PK_GMeter', 0)
# DBCC CHECKTABLE ('Gmeter')
# DBCC CHECKIDENT ('Gmeter')

file_path = r"C:\Users\User\Desktop\PoveritelWORK\insert_Горгаз №601.txt"
transform_insert_lines(file_path)
