# -*- coding: utf-8 -*-
import pymssql
import logging
import zipfile
import time
import datetime
from mod.func_sql import del_file_olddays, remove_files
from mod.func_sql import copy_archive
from mod.mail import send_email_with_attachment

timeNow = time.strftime("%d%m%Y_%H%M%S")
now = datetime.datetime.now()

# чистить лог по числа месяца
listDayClean = [7, 14, 21, 28, 2]
if now.day in listDayClean:
    remove_files(r"logs\\", ".log")


pathBackup = r"D:\1C-ARCHIVE\\"
pathArchive = "\\\\gg-app\oit\\"
days_archive = 1


serv = r"ARM-110821"
# список баз данных на сервере
bases = ["APC", "lansweeperdb"]
user = ""
pasw = ""

logger = logging.getLogger("sql_backup")
logger.setLevel(logging.INFO)
# create the logging file handler
fh = logging.FileHandler(r"logs\log_sqlBackup.log")
formatter = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')
fh.setFormatter(formatter)
# add handler to logger object
logger.addHandler(fh)


def sqlconnectPY(nHost="localhost", nBase="", nUser="", nPasw=""):
    try:
        return pymssql.connect(host=nHost, user=nUser, password=nPasw, database=nBase)
    except:
        return False


def sqlBackup(nHost, nBase, nFileName):
    """
       Архивирование базы данных
    """

    con = sqlconnectPY(serv, "master", user, pasw)
    # logging.basicConfig(filename="backup.log", level=logging.INFO)
    logger.info("Program started "+timeNow)
    if not con:
        print("Error connect to dataBase! - "+serv)
        logger.error("Error connect to dataBase!" + timeNow)
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
        # print(file)
        logger.info("Удален файл старше "+str(days_archive)+ " дней: " +file)


# Запускаем архивацию базы данных
# sqlBackup(serv, base, fileName)
baselist = ""
for base in bases:
    fileName = pathBackup+base+"_"+timeNow+".bak"
    fileZip = fileName + ".zip"
    baselist = baselist + base +" "
    sqlBackup(serv, base, fileName)

# Удаляем старые архивы в соответствии с настройками
del_old_files(pathBackup, days_archive)

# копируем файлы на сервер
try:
    copy_archive(pathBackup, pathArchive)
    logger.info("Данные скопированы по пути:"+pathArchive)
except Exception as e:
    # print(e)
    logger.error(e)
    logger.error("Данные не скопированы! По пути:"+pathArchive)
    raise


del_old_files(pathArchive, days_archive+4)

# отправка письма о результатах
try:
    emails = ["mchmir@gorgaz.kz", "achernyaev@gorgaz.kz"]
    cc_emails = []
    bcc_emails = []
    subject = "Архивация баз данных с сервера IT-2"
    body_text = "Результаты архивации баз данных:" + baselist
    path = r"logs\log_sqlBackup.log"
    send_email_with_attachment(subject, body_text, emails,cc_emails, bcc_emails, path)
    logger.info("Лог-файл отправлен на почту.")
except Exception as e:
    logger.error(e)
    raise
