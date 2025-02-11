import os
import pyodbc
import time
from tabulate import tabulate

# Читаем настройки
import configparser

config = configparser.ConfigParser()
config.read("ini\\configLogs.ini", encoding="utf-8")

server = config["Database"]["server"]
port = config["Database"]["port"]
database = config["Database"]["database"]
user = config["Database"]["user"]
password = config["Database"]["password"]
interval = int(config["Settings"]["interval"])
sql_file = config["Settings"]["sql_file"]

# Подключение
conn_str = f"DRIVER={{SQL Server}};SERVER={server},{port};DATABASE={database};UID={user};PWD={password}"
conn = pyodbc.connect(conn_str)
cursor = conn.cursor()

# Ввод параметров
month = input("Введите месяц (MM): ")
year = input("Введите год (YYYY): ")

# Загружаем SQL-запрос
with open(sql_file, "r", encoding="utf-8") as f:
    query = f.read().replace("{MONTH}", month).replace("{YEAR}", year)

try:
    while True:
        os.system("cls" if os.name == "nt" else "clear")  # Полная очистка консоли
        cursor.execute(query)
        columns = [column[0] for column in cursor.description]
        data = [[str(item).replace(".000000", "") for item in row] for row in cursor.fetchall()]

        print(tabulate(data, headers=columns, tablefmt="fancy_grid", stralign="center"))
        print("\nДля выхода нажмите Ctrl+C")
        time.sleep(interval)  # Ожидание перед следующим обновлением

except KeyboardInterrupt:
    print("\nВыход из программы. До свидания!")
