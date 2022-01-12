# -*- coding: utf-8 -*-
#import time
import pandas as pd
from mod.func_sql import sqlconnect
import re    # Импорт модуля для работы с регулярными выражениями
from  datetime import datetime

serv = "ggapp"
base ="Gefest..."
user=""
pasw = ""



        
def sql_select():
    response = input('Запускаем код на выполнение? [Y/N]:')
    if re.match("[yYдД]", response):
        sqlfile = input('Имя sql-файла без расширения в папке .sql:')
       
        con = sqlconnect(serv,base,user,pasw)
        if not con:
            print ("Error connect to dataBase!")
            return
    
        #cursor = con.cursor()
        #sqlCommand = sqlCommandfromFile('sql\\'+sqlfile+'.sql') #.split(';')
        query = open('sql\\'+sqlfile+'.sql', 'r')
        #for command in sqlCommand:
        #print(command)
        sql_reader = pd.read_sql_query(query.read(), con)
        #sql_reader = pd.read_sql(query, con)
        fileCSV = "EIRC_GORGAZ_{:%m%Y}.csv".format(datetime.now())
        sql_reader.to_csv(fileCSV, sep='|', index =False, encoding='utf-8')
        
        
    else:
        print("Отмена выполнения. Всего хорошего!")
        
        

#sql_select_pd()
start_time = datetime.now()
sql_select()
end_time = datetime.now()
print('Duration: {}'.format(end_time - start_time))



##****************** СПРАВКА****************
# import time
# from datetime import timedelta
# start_time = time.monotonic()
# end_time = time.monotonic()
# print(timedelta(seconds=end_time - start_time))
