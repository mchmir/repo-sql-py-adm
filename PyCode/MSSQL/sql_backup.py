# -*- coding: utf-8 -*-
import pymssql 
import logging
import zipfile
import time
from mod.func_sql import del_file_olddays

timeNow = time.strftime("%d%m%Y_%H%M%S")
fileName = r"D:\DataBaseBackup\ClientTest_"+timeNow+".bak" 
fileZip = fileName + ".zip"

pathBackup = "D:\DataBaseBackup\\"
days_archive = 1


serv = "DESKTOP-G7SJCTM\SQLEX2014"
base ="ClientTest"
user="sa"
pasw = "sa303+"

logger = logging.getLogger("sql_backup")
logger.setLevel(logging.INFO)
# create the logging file handler
fh = logging.FileHandler("logs\log_sqlBackup.log")
formatter = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')
fh.setFormatter(formatter)
# add handler to logger object
logger.addHandler(fh)



def sqlconnectPY(nHost="localhost",nBase="",nUser="",nPasw=""):
    try:
        return pymssql.connect(host=nHost, user=nUser, password=nPasw, database=nBase)
    except:
        return False
    
        
def sqlBackup(nHost,nBase,nFileName):
    """
     Архивирование базы данных
    
    """
      
    con = sqlconnectPY(serv,"master",user,pasw)
    #logging.basicConfig(filename="backup.log", level=logging.INFO)
    logger.info("Program started "+timeNow)
    if not con:
        print ("Error connect to dataBase!")
        logger.error("Error connect to dataBase!" +timeNow)
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
        logger.info("Архивирование успешно завершено в файл: "+nFileName)
        backup_zip = zipfile.ZipFile(fileZip, 'w')
        backup_zip.write(fileName, compress_type=zipfile.ZIP_DEFLATED)
        backup_zip.close()
        logger.info("Архив сжат в ZIP: "+fileZip)
    except Exception as e:
        print(e)
        raise
    
def del_old_files(path, days):
    listFiles = del_file_olddays(path, days)
    for file in listFiles:
        #print(file)
        logger.info("Удален файл старше "+str(days_archive)+ " дней: " +file)
    
# Запускаем архивацию базы данных
sqlBackup(serv, base, fileName)
# Удаляем старые архивы в соответствии с настройками
del_old_files(pathBackup, days_archive)
    

