# -*- coding: utf-8 -*-
import pandas as pd
from mod.func_sql import sqlconnect
from mod.func_sql import sql_engine
import re    # Импорт модуля для работы с регулярными выражениями
from datetime import datetime

serv = "gg-app"
base = "Gefest"
user = "sa"
pasw = "sa"


def sql_select_engine():
    response = input('Запускаем код на выполнение? [Y/N]:')
    if re.match("[yYдД]", response):
        sql_file = input('Имя sql-файла без расширения в папке .sql:')

        engine = sql_engine(serv, base, user, pasw)
        if not engine:
            print("Error connect to dataBase!")
            return

        query = open('sql\\' + sql_file + '.sql', 'r')

        sql_reader = pd.read_sql_query(query.read(), engine)

        file_csv = "EIRC_GORGAZ_{:%m%Y}.csv".format(datetime.now())
        sql_reader.to_csv(file_csv, sep='|', index=False, encoding='utf-8')

    else:
        print("Отмена выполнения. Всего хорошего!")

        
def sql_select():
    response = input('Запускаем код на выполнение? [Y/N]:')
    if re.match("[yYдД]", response):
        sqlfile = input('Имя sql-файла без расширения в папке .sql:')
       
        con = sqlconnect(serv,base,user,pasw)
        if not con:
            print ("Error connect to dataBase!")
            return
        query = open('sql\\'+sqlfile+'.sql', 'r')

        sql_reader = pd.read_sql_query(query.read(), con)

        fileCSV = "EIRC_GORGAZ_{:%m%Y}.csv".format(datetime.now())
        sql_reader.to_csv(fileCSV, sep='|', index =False, encoding='utf-8')

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
