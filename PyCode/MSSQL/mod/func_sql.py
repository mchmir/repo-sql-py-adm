import os
import time
from shutil import copy

import pyodbc
from sqlalchemy import create_engine, exc
from functools import wraps
from time import perf_counter
from typing import Union

from sqlalchemy.engine import Engine
from sqlalchemy.exc import SQLAlchemyError


def timed(func):
    """Функция декоратор, которая при вызове декорируемой функции измеряет время ее выполнения"""
    @wraps(func)
    def wrap(*args, **kwargs):
        t1 = perf_counter()
        result = func(*args, **kwargs)
        t2 = perf_counter()
        print(f"Вызов {func.__name__} занял {t2-t1} секунд, ", end="")
        print(f"(параметры {args}, {kwargs})")
        return result
    return wrap


def sql_engine(host_name: str = "localhost",
               base_name: str = "",
               user_name: str = "",
               passwd: str = "") -> Union[tuple[Engine, None], tuple[bool, SQLAlchemyError]]:
    try:
        return create_engine(f"mssql+pyodbc://{user_name}:{passwd}@{host_name}/{base_name}?driver=SQL+Server"), None
    except exc.SQLAlchemyError as error:
        return False, error


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
        sql_file: str = fd.read()
        fd.close()
        return sql_file
    except (IOError, OSError) as e:
        return print(e)


# удаление файлов по расширению
def removefiles(dir, xDot):
    files = os.listdir(dir)
    file_path = dir
    list_file: list[str] = []
    for xfile in files:

        if os.path.isfile(str(file_path) + xfile):

            if (xfile.endswith(xDot)):

                try:
                    list_file.append(str(file_path) + xfile)
                    os.remove(str(file_path) + xfile)
                except OSError as e:
                    list_file.append(e.filename + " : " + e.strerror)
    return list_file


def get_file_directory(file):
    return os.path.dirname(os.path.abspath(file))


# удаление файлов старше days дней
def del_file_olddays(dir, days):
    now = time.time()
    cutoff = now - (int(days) * 86400)
    files = os.listdir(dir)
    file_path = dir
    list_file: list[str] = []
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
                # list_file.append(str(file_path) + xfile)
                try:
                    list_file.append(str(file_path) + xfile)
                    os.remove(str(file_path) + xfile)
                except OSError as e:
                    list_file.append(e.filename + " : " + e.strerror)
    return list_file
