# -*- coding: utf-8 -*-
import pyodbc
import pymssql
import time

timeNow = time.strftime("%d%m%Y_%H%M%S")
fileName = r"D:\ClientTest_"+timeNow+".bak" 


serv = "DESKTOP-G7SJCTM\SQLEX2014"
base ="ClientTest"
user=""
pasw = ""



strSql = """
         select * from fio
"""



def sqlconnect(nHost="localhost",nBase="",nUser="",nPasw=""):
    try:
        return pyodbc.connect("DRIVER={SQL Server};SERVER="+nHost+";DATABASE="+nBase+";UID="+nUser+";PWD="+nPasw)
    except:
        return False

def sqlconnectPY(nHost="localhost",nBase="",nUser="",nPasw=""):
    try:
        return pymssql.connect(host=nHost, user=nUser, password=nPasw, database=nBase)
    except:
        return False

def main():
    con = sqlconnect(serv,base,user,pasw)
    if not con:
        print ("Error connect to dataBase!")
        return
    try:
        cursor = con.cursor()
        res = cursor.execute(strSql)
        row = res.fetchone()
        while row:
            print(row)
            row = res.fetchone()
    except:
        print("Error query to sql-script!")
        
        
def sqlBackup(nHost,nBase,nFileName):
    """
    Архивирование базы данных
    
    """
    con = sqlconnect(serv,"master",user,pasw)
    if not con:
        print ("Error connect to dataBase!")
        return
    try:
        cursor = con.cursor()
        #сначала завершим транзакцию (она по умолчанию открылась сама, а нам этого не надо, иначае бекап не пройдет)
        cursor.execute("COMMIT TRANSACTION")
        cursor.execute(r"BACKUP DATABASE ["+nBase+r"] TO DISK =N'"+nFileName+r"' WITH INIT")
        #ну и восстановим транзакцию на всякий (может оно и не нужно)
        cursor.execute("BEGIN TRANSACTION")
        con.commit()
        con.close()
        print("Архивирование успешно завершено в файл: "+nFileName)
        
    except Exception as e:
        print(e)
        raise
    
def sqlBackupPY(nHost,nBase,nFileName):
    """
    Архивирование базы данных
    
    """
    con = sqlconnectPY(serv,"master",user,pasw)
    if not con:
        print ("Error connect to dataBase!")
        return
    try:
        cursor = con.cursor()
        #сначала завершим транзакцию (она по умолчанию открылась сама, а нам этого не надо, иначае бекап не пройдет)
        cursor.execute("COMMIT TRANSACTION")
        cursor.execute(r"BACKUP DATABASE ["+nBase+r"] TO DISK =N'"+nFileName+r"' WITH INIT")
        #ну и восстановим транзакцию на всякий (может оно и не нужно)
        cursor.execute("BEGIN TRANSACTION")
        con.commit()
        con.close()
        print("Архивирование успешно завершено в файл: "+nFileName)
        
    except Exception as e:
        print(e)
        raise

#main()

sqlBackup(serv, base, fileName)
    
#sqlBackupPY(serv, base, fileName)
