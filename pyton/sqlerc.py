# -*- coding: utf-8 -*-
# import time
import pandas as pd
from mod.func_sql import sqlconnect
# Импорт модуля для работы с регулярными выражениями
import re
from datetime import datetime

serv = "gg-app"
base = "Gefest"
user = ""
password = ""


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
        

start_time = datetime.now()
sql_select()
end_time = datetime.now()
print('Duration: {}'.format(end_time - start_time))



##****************** СПРАВКА****************
# import time
# from datetime import timedelta
# start_time = time.monotonic()
# end_time = time.monotonic()
# print(timedelta(seconds = end_time - start_time))
