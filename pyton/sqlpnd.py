# -*- coding: utf-8 -*-
import time
import pandas as pd
from mod.func_sql import *
import re    # Импорт модуля для работы с регулярными выражениями


serv = r"DESKTOP-G7SJCTM\SQLEX2014"
base = "ClientTest"
user = ""
pasw = ""


def sql_select():
    response = input('Запускаем код на выполнение? [Y/N]:')
    if re.match("[yYдД]", response):
        sql_file = input('Имя sql-файла без расширения в папке .sql:')
       
        con = sqlconnect(serv, base, user, pasw)
        if not con:
            print("Error connect to dataBase!")
            return
    
        cursor = con.cursor()
        # sqlCommand = sqlCommandfromFile('sql\\'+sqlfile+'.sql') #.split(';')
        query = open('sql\\'+sql_file+'.sql', 'r')
        # for command in sqlCommand:
        # print(command)
        sql_reader = pd.read_sql_query(query.read(), con)
        print(sql_reader)
        
    else:
        print("Отмена выполнения. Всего хорошего!")
        
        
def sql_select_pd():
    response = input('Запускаем код на выполнение? [Y/N]:')
    if re.match("[yYдД]", response):
        i=1
        con = sqlconnect(serv,base,user,pasw)
        if not con:
            print ("Error connect to dataBase!")
            return
    
        sql_command = sqlCommandfromFile(r'sql\sql_pandas.sql').split(';')
        for command in sql_command:
            print(command)
            if len(command)>3:
                result = pd.read_sql(command, con)
                result.to_csv("pyResultSQLpandas.csv", sep='|', encoding='utf-8')
               
            i += 1     
    else:
        print("Отмена выполнения. Всего хорошего!")  
           



#sql_select_pd()
sql_select()
