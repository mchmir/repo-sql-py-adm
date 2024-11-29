import pyodbc
# pip install pyodbc

# cтрока подключения к вашему SQL Server
connection_string = 'DRIVER={SQL Server};SERVER=gg_app;DATABASE=Gmeter;UID=sa;PWD=sa'

file_path = r"C:\Users\User\Desktop\PoveritelWORK\parse_insert_Горгаз №601.txt"


# Откройте файл SQL и файл для записи ошибок
with open(file_path, 'r') as sql_file, open('../error.txt', 'w') as error_file:
    # Установите соединение с SQL Server
    cnxn = pyodbc.connect(connection_string)
    cursor = cnxn.cursor()

    # Выполните каждую строку SQL из файла
    for line in sql_file:
        try:
            cursor.execute(line)
            cnxn.commit()
        except Exception as e:
            # Если произошла ошибка, запишите строку SQL в файл ошибок
            error_file.write(f"Failed to execute: {line}\n")
            error_file.write(f"Error: {e}\n")
