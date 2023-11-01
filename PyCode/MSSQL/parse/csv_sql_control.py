import csv
import pyodbc
from decimal import Decimal
from datetime import datetime

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

        for row in reader:
            # Извлекаем ЛИЦЕВОЙ СЧЕТ и СУММУ
            account = row[2]
            # amount = Decimal(row[3].replace(',', '.'))
            amount = row[3].replace(',', '.')
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

            cursor.execute(query, (account, amount, document_date))
            result = cursor.fetchone()

            # Если документ найден, записываем в файл для найденных записей
            if result:
                found_writer.writerow([account, str(amount), result[0]])
            else:
                # В противном случае записываем в файл для не найденных записей
                notfound_writer.writerow(row)

cursor.close()
conn.close()
