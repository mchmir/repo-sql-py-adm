import csv
from datetime import datetime, timedelta

# Кортеж с датами
dates_tuple = ('26.09.2023', '01.10.2023', '02.10.2023')

# Чтение CSV файла
with open('source_file.csv', 'r') as file:
    reader = csv.reader(file, delimiter=';')
    data = [row for row in reader]

# Фильтрация данных по датам и сохранение в новые файлы
for i in range(len(dates_tuple) - 1):
    start_date = datetime.strptime(dates_tuple[i], '%d.%m.%Y')
    end_date = datetime.strptime(dates_tuple[i + 1], '%d.%m.%Y')

    # Отфильтрованные строки
    filtered_rows = [row for row in data if
                     start_date <= datetime.strptime(row[1].split(' ')[0], '%d.%m.%Y') < end_date]

    # Вычисление общей суммы
    total_sum = sum([float(row[3].replace(',', '.')) for row in filtered_rows])

    # Создание имени файла
    filename = f"{dates_tuple[i]}_{(end_date - timedelta(days=1)).strftime('%d.%m.%Y')}_" \
               f"{len(filtered_rows)}_{total_sum:.2f}_{datetime.now().strftime('%d%m%Y_%H%M%S')}.csv"

    # Запись данных в файл
    with open(filename, 'w', newline='') as file:
        writer = csv.writer(file, delimiter=';')
        writer.writerows(filtered_rows)

print("Файлы успешно созданы!")
