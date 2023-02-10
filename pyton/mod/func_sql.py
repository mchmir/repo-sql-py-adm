# import os, sys, time
import os
import time
from shutil import copy

import pyodbc
# from subprocess import call
# from shutil import copyfile,copy
from sqlalchemy import create_engine


def sql_engine(nhost="localhost", nbase="", nuser="", npasw=""):
    try:
        return create_engine(f"mssql+pyodbc://{nuser}:{npasw}@{nhost}/{nbase}?driver=SQL+Server")
    except:
        return False


def sqlconnect(nHost="localhost", nBase="", nUser="", nPasw=""):
    try:
        return pyodbc.connect(
            "DRIVER={SQL Server};SERVER=" + nHost + ";DATABASE=" + nBase + ";UID=" + nUser + ";PWD=" + nPasw)
    except:
        return False


def sqlconnect_backup(nHost="localhost", nBase="", nUser="", nPasw=""):
    try:
        return pyodbc.connect(
            "DRIVER={SQL Server};SERVER=" + nHost + ";DATABASE=" + nBase + ";UID=" + nUser + ";PWD=" + nPasw,
            autocommit=True)
    except:
        return False


def copy_archive(dir_src, dir_dst):
    for filename in os.listdir(dir_src):
        if filename.endswith('.zip'):
            copy(dir_src + filename, dir_dst)


def sqlCommandfromFile(filename):
    # Open and read the file as a single buffer
    try:
        fd = open(filename, 'r')
        sqlFile = fd.read()
        fd.close()
        return sqlFile
    except (IOError, OSError) as e:
        return print(e)


# удаление файлов по расширению
def removefiles(dir, xDot):
    files = os.listdir(dir)
    file_path = dir
    listFile = []
    # import glob
    # os.chdir(" ")
    # for file in glob.glob("*.txt")
    # print(file)
    for xfile in files:

        if os.path.isfile(str(file_path) + xfile):

            if (xfile.endswith(xDot)):

                try:
                    listFile.append(str(file_path) + xfile)
                    os.remove(str(file_path) + xfile)
                except OSError as e:
                    listFile.append(e.filename + " : " + e.strerror)
    return listFile


def get_file_directory(file):
    return os.path.dirname(os.path.abspath(file))


# удаление файлов старше days дней
def del_file_olddays(dir, days):
    now = time.time()
    cutoff = now - (int(days) * 86400)
    files = os.listdir(dir)
    file_path = dir
    listFile = []
    # import glob
    # os.chdir(" ")
    # for file in glob.glob("*.txt")
    # print(file)
    for xfile in files:

        if os.path.isfile(str(file_path) + xfile):
            t = os.stat(str(file_path) + xfile)
            c = t.st_ctime
            # print(c)

            # delete file if older than  $days
            if (xfile.endswith(".bak") or xfile.endswith(".zip")) and c < cutoff:
                # print("Удален файл: " + str(file_path) + xfile)
                # listFile.append(str(file_path) + xfile)
                try:
                    listFile.append(str(file_path) + xfile)
                    os.remove(str(file_path) + xfile)
                except OSError as e:
                    listFile.append(e.filename + " : " + e.strerror)
    return listFile
