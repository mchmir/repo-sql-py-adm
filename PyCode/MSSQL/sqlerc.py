# -*- coding: utf-8 -*-
import dateutil
import pandas as pd
from mod.func_sql import sqlconnect
from mod.func_sql import sql_engine
import re    # Импорт модуля для работы с регулярными выражениями
from datetime import datetime
from dateutil.relativedelta import relativedelta


serv = "gg-app"
base = "Gefest"
user = "sa"
password = "sa"


def sql_select_engine():
    response = input('Запускаем код на выполнение? [Y/N]:')
    if re.match("[yYдД]", response):
        sql_file = input('Имя sql-файла без расширения в папке .sql:')

        engine, error = sql_engine(serv, base, user, password)

        if not engine:
            print(f'Error connect to dataBase! Error {error}')
            return

        query = open('sql\\' + sql_file + '.sql', 'r')

        sql_reader = pd.read_sql_query(query.read(), engine)
        num_rows = sql_reader.shape[0]

        year = datetime.now().year
        month_ago = datetime.now().month - 1

        if month_ago < 10:
            month_file = '0' + str(month_ago)

        file_csv = 'EIRC_GORGAZ_{0}{1}_{2}.csv'.format(month_file, year, num_rows)
        sql_reader.to_csv(file_csv, sep='|', index=False, encoding='utf-8')

    else:
        print("Отмена выполнения. Всего хорошего!")

        
def sql_select():
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
        
        
def main():
    start_time = datetime.now()
    sql_select_engine()
    end_time = datetime.now()
    print('Duration: {}'.format(end_time - start_time))


if __name__ == "__main__":
    main()


# ****************** СПРАВКА****************
# import time
# from datetime import timedelta
# start_time = time.monotonic()
# end_time = time.monotonic()
# print(timedelta(seconds=end_time - start_time))
