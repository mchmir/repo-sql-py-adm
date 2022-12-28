# -*- coding: utf-8 -*-
import time
import pandas as pd
from mod.func_sql import *
import re  # Импорт модуля для работы с регулярными выражениями

serv = r"DESKTOP-G7SJCTM\SQLEX2014"
base = "ClientTest"
user = "sa"
pasw = "sa303+"

strSql = """
         select * from fio
         """


def simple_sql():
    con = sqlconnect(serv, base, user, pasw)
    if not con:
        print("Error connect to dataBase!")
        return
    try:
        cursor = con.cursor()
        res = cursor.execute(strSql)
        row = res.fetchone()
        while row:
            print(row)
            row = res.fetchone()

    except Exception as e:
        print("Error query to sql-script! - ", e)


def sql_select():
    response = input('Запускаем код на выполнение? [Y/N]:')
    if re.match("[yYдД]", response):
        sql_file = input('Имя sql-файла без расширения в папке .sql:')
        i = 1
        con = sqlconnect(serv, base, user, pasw)
        if not con:
            print("Error connect to dataBase!")
            return

        cursor = con.cursor()
        sql_command = sqlCommandfromFile('sql\\' + sql_file + '.sql').split(';')
        for command in sql_command:
            # print(command)
            if len(command) > 3:
                try:
                    res = cursor.execute(command)
                    row = res.fetchone()
                    while row:
                        print(row)
                        row = res.fetchone()
                except:
                    print("Command number {0} skipped!".format(i))
            i += 1
    else:
        print("Отмена выполнения. Всего хорошего!")


def sql_select_pd():
    response = input('Запускаем код на выполнение? [Y/N]:')
    if re.match("[yYдД]", response):
        i = 1
        con = sqlconnect(serv, base, user, pasw)
        if not con:
            print("Error connect to dataBase!")
            return

        sql_command = sqlCommandfromFile('sql\sql_pandas.sql').split(';')
        for command in sql_command:
            print(command)
            if len(command) > 3:
                result = pd.read_sql(command, con)
                result.to_csv("pyResultSQLpandas.csv", sep='|', encoding='utf-8')

            i += 1
    else:
        print("Отмена выполнения. Всего хорошего!")

    # simple_sql()


sql_select_pd()
# sql_select()
