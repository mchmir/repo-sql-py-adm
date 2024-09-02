# Находит проведенные документы из СSV-файла
# Найденные счета направляются в файл found_records.csv
# Структура файла
# Лицевой счет;Сумма;ID-документа
#
# Не найденные данные - отправляются в исходном виде в файл not_found_records
# В итоге, подсчитывается сумма и кол. записей и файлы переименовываются
# Файл ДОГРУЗИТЬ загружаем в систему.
# LOADED - загруженные записи, AWAITING_LOAD - данные ожидают загрузки
#

import csv
import pyodbc
from decimal import Decimal
from datetime import datetime
import os


# Параметры подключения к базе данных
SERVER = 'gg-app'
DATABASE = 'Gefest'
USERNAME = 'sa'
PASSWORD = 'sa'
DRIVER = '{SQL Server}'  # драйвер для SQL Server 2005

# Подключение к базе данных
conn = pyodbc.connect(f'DRIVER={DRIVER};SERVER={SERVER};DATABASE={DATABASE};UID={USERNAME};PWD={PASSWORD}')
cursor = conn.cursor()

# Открываем исходный CSV файл
with open('incoming.csv', 'r', newline='', encoding='utf-8') as csvfile:
    reader = csv.reader(csvfile, delimiter=';')

    # Создаем два новых CSV файла для записи
    with open('found_records.csv', 'w', newline='', encoding='utf-8') as foundfile, \
            open('not_found_records.csv', 'w', newline='', encoding='utf-8') as notfoundfile:

        found_writer = csv.writer(foundfile, delimiter=';')
        notfound_writer = csv.writer(notfoundfile, delimiter=';')

        total_amount = Decimal('0.00')  # Для подсчета общей суммы
        total_records = 0  # Для подсчета количества записей
        total_amount_0 = Decimal('0.00')  # Для подсчета общей суммы загрузившихся
        total_records_0 = 0  # Для подсчета количества записей загрузившихся

        for row in reader:
            # Извлекаем ЛИЦЕВОЙ СЧЕТ и СУММУ
            account = row[2]
            amount_str = row[3].replace(',', '.')  # Сумма как строка
            amount = Decimal(amount_str)  # Конвертируем строку в Decimal
            date_str = row[1]
            # Преобразовываем дату в нужный формат
            document_date = datetime.strptime(date_str, '%d.%m.%Y %H:%M:%S').strftime('%Y-%m-%d')

            # Подготавливаем SQL запрос
            query = f"""
            SELECT d.IDDOCUMENT
            FROM DOCUMENT as d
            JOIN CONTRACT as C on D.IDCONTRACT = C.IDCONTRACT
            WHERE c.ACCOUNT = ? AND d.DOCUMENTAMOUNT = ? AND d.DOCUMENTDATE = ?;
            """

            cursor.execute(query, (account, amount_str, document_date))
            result = cursor.fetchone()

            # Если документ найден, записываем в файл для найденных записей
            if result:
                total_amount_0 += amount  # Добавляем сумму к общей
                total_records_0 += 1  # Увеличиваем количество записей
                found_writer.writerow([account, str(amount), result[0]])
            else:
                # В противном случае записываем в файл для не найденных записей
                notfound_writer.writerow(row)
                total_amount += amount  # Добавляем сумму к общей
                total_records += 1  # Увеличиваем количество записей

cursor.close()
conn.close()

# Получаем текущую дату без времени
current_date = datetime.now().strftime('%d.%m.%Y')

# Формируем новое имя файла
new_filename_0 = f"{current_date}_LOADED_{total_records_0}_records_{total_amount_0}.csv"
new_filename = f"{current_date}_AWAITING_LOAD_{total_records}_records_{total_amount}.csv"

# Переименовываем файлы
os.rename('not_found_records.csv', new_filename)
os.rename('found_records.csv', new_filename_0)
