import pandas as pd
import re

from datetime import datetime
from mod.func_sql import sqlconnect
from mod.func_sql import sql_engine
from mod.func_sql import timed


serv: str = "gg-app"
base: str = "Gefest"
user: str = "sa"
password: str = "sa"


@timed
def sql_select_engine() -> None:
    response = input('Запускаем код на выполнение? [Y/N]:')
    if re.match("[yYдД]", response):
        sql_file = input('Имя sql-файла без расширения в папке .sql:')

        engine, error = sql_engine(serv, base, user, password)

        if not engine:
            print(f'Error connect to dataBase! Error {error}')
            return

        query = open('sql\\' + sql_file + '.sql', 'r')

        sql_reader = pd.read_sql_query(query.read(), engine)
        # Столбец Common, может содержать значения NULL
        # и sql_reader возвращает для него тип float64
        # и тогда значения для поля дробные
        # cлед. две строки именно для анализа этого
        # В pandas два типа int int64 и Int64(с большой буквы)
        # Int64 поддерживает NaN типы, int64 - нет.
        sql_reader['Common'] = sql_reader['Common'].astype('Int64')
        print(sql_reader.dtypes)

        num_rows = sql_reader.shape[0]

        if datetime.now().month == 1:
            year = datetime.now().year - 1
            month_ago = 12
        else:
            year = datetime.now().year
            month_ago = datetime.now().month - 1

        month_file: str = ''

        if month_ago < 10:
            month_file = '0' + str(month_ago)
        else:
            month_file = str(month_ago)

        file_csv = f'EIRC_GORGAZ_{month_file}{year}_{num_rows}.csv'
        sql_reader.to_csv(file_csv, sep='|', index=False, encoding='utf-8')

    else:
        print("Отмена выполнения. Всего хорошего!")


def sql_select() -> None:
    response = input('Запускаем код на выполнение? [Y/N]:')
    if re.match("[yYдД]", response):
        sql_file = input('Имя sql-файла без расширения в папке .sql:')

        con = sqlconnect(serv, base, user, password)
        if not con:
            print("Error connect to dataBase!")
            return

        query = open('sql\\'+sql_file+'.sql', 'r')

        sql_reader = pd.read_sql_query(query.read(), con)
        file_csv = "EIRC_GORGAZ_{:%m%Y}.csv".format(datetime.now())
        sql_reader.to_csv(file_csv, sep='|', index=False, encoding='utf-8')
    else:
        print("Отмена выполнения. Всего хорошего!")


def main() -> None:
    start_time = datetime.now()
    sql_select_engine()
    end_time = datetime.now()
    print('Duration: {}'.format(end_time - start_time))


if __name__ == "__main__":
    main()
