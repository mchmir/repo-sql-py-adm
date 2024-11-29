# Обрабатывает шаблон JSON, файл выгрузки запроса для формирования скрипта загрузки
#
#

import json

# Шаблон строки SQL
template = ("begin try "
            "insert {table} ({columns}) "
            "values ({values}) "
            "end try "
            "begin catch end catch \n"
            "if @@error<>0 begin ROLLBACK TRANSACTION end ")


def write_all_at_once(data, table_name, output_file):
    """
    Записывает весь SQL-код в файл одной операцией.
    """
    sql_statements = []
    for item in data:
        columns, values = prepare_columns_and_values(item)
        sql = template.format(
            table=table_name,
            columns=", ".join(columns),
            values=", ".join(values)
        )
        sql_statements.append(sql)

    with open(output_file, 'w', encoding='windows-1251') as file:
        file.write("\n".join(sql_statements))

    print(f"Результат сохранен в файл: {output_file}")


def write_line_by_line(data, table_name, output_file):
    """
    Построчная запись SQL-кода в файл.
    Подходит для обработки больших данных, во избежание утечки памяти
    """
    with open(output_file, 'w', encoding='windows-1251') as file:
        for item in data:
            columns, values = prepare_columns_and_values(item)
            sql = template.format(
                table=table_name,
                columns=", ".join(columns),
                values=", ".join(values)
            )
            file.write(sql + "\n")

    print(f"Результат сохранен в файл: {output_file}")


def prepare_columns_and_values(item):
    """
    Формирует списки столбцов и значений для SQL-строки.
    """
    columns = []
    values = []
    for key, value in item.items():
        columns.append(key)
        if value is None:  # Проверяем, является ли значение None
            values.append("null")  # SQL-значение NULL
        elif isinstance(value, str):
            values.append("'" + value.replace("'", "''") + "'")  # Экранирование кавычек
        else:
            values.append(str(value))
    return columns, values


def main():
    # JSON-файл с данными
    json_file = 'in-data\House.json'

    # Имя таблицы
    table_name = "House"

    # Файл для сохранения результата
    output_file = r'out-data\result.sql'

    # Чтение данных из JSON
    with open(json_file, 'r', encoding='utf-8') as file:
        data = json.load(file)

    # Выбор метода записи
    method = input("Select the method of recording (1 - line-by-line, 2 - total): ").strip()
    if method == "1":
        write_line_by_line(data, table_name, output_file)
    elif method == "2":
        write_all_at_once(data, table_name, output_file)
    else:
        print("Incorrect recording method selection.")


if __name__ == "__main__":
    main()
