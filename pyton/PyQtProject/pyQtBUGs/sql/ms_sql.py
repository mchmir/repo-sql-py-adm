#!/usr/bin/env python
# -*- coding: utf-8 -*-
#
#  ms_sql.py
#  
#  Copyright 2020 mchmir
#  
from PyQt5.QtSql import QSqlDatabase

#-----------------------------------------
SERVER_NAME = 'DESKTOP-VBIAJ1G\SQLHOME14'
DATABASE_NAME = 'Test'
USERNAME = 'sa'
PASSWORD = 'sa303+'
#-----------------------------------------



def createConnection(myDB):
    #создаем соединение с базой с именем myDB
    db = QSqlDatabase.database(myDB)
    #если соединение с именем myDB уже открыто, то ничего не делаем, иначе создаем новое
    if not db.isValid():
        connString = f'DRIVER={{SQL Server}};'\
                f'SERVER={SERVER_NAME};'\
                f'DATABASE={DATABASE_NAME};'\
                f'UID={USERNAME};'\
                f'PWD={PASSWORD}'

 
        db = QSqlDatabase.database(myDB)
        db = QSqlDatabase.addDatabase('QODBC',myDB)
        db.setDatabaseName(connString)
    
    if not db.open():
        print("Error ({0}) on open DB '{2}'\n{1}".format(
            db.lastError().nativeErrorCode(),
            db.lastError().text(),
            myDB))
        db = None
        QSqlDatabase.removeDatabase(myDB)
        return None
    
    
    
    return db

