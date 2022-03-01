#!/usr/bin/env python
# -*- coding: utf-8 -*-
#
#  myWindowsClass.py
#  
#  Copyright 2020 mchmir
#  
from  PyQt5 import QtWidgets, QtGui, QtCore
from  PyQt5.QtCore import *
from  PyQt5.QtGui import QIcon
from  PyQt5.QtSql import *
from  PyQt5.QtWidgets import *
from  sql.ms_sql import createConnection
import sys


class QueryRunner(QThread):
    def __init__(self, query, parent=None):
        super(QueryRunner, self).__init__(parent)
        self.query = query
        return
    
    def run(self):
        self.query.exec_()


class WindowSearch(QtWidgets.QWidget):  #QtWidgets.QWidget
    """ Основное окно с двумя кнопками """
    def __init__(self, parent=None,strSQL=""):
        super(WindowSearch, self).__init__(parent)
               
        QtWidgets.QWidget.__init__(self,parent)
        self.setWindowIcon(QIcon(r'ico\ic_poll.png'))
        self.buttonRun = QtWidgets.QPushButton("&Выполнить")
        self.buttonRun.setStyleSheet("color: blue")
        self.buttonRun.setAutoDefault(True)
        self.buttonRun.clicked.connect(self.run_clicked)

        #self.buttonClose = QtWidgets.QPushButton("&Закрыть")
        #self.buttonClose.setStyleSheet("color: red")
        #self.buttonClose.clicked.connect(QCoreApplication.instance().quit)

        self.hbox = QtWidgets.QHBoxLayout()
        self.hbox.addWidget(self.buttonRun)
        #self.hbox.addWidget(self.buttonClose)
        
        # для варианта 1
        # self.tabView = QtWidgets.QTableView()
        # self.tabView.setSortingEnabled(True)
        # self.tabView.horizontalHeader().setSectionsMovable(True)
        # self.tabView.horizontalHeader().setSortIndicator(-1,Qt.AscendingOrder)
        
        #------ top ---------------------
        self.topLay = QVBoxLayout(self)
        self.topLay.setContentsMargins(6,6,6,6)
        self.lay = QFormLayout()
        self.topLay.addLayout(self.lay)
        self.resultLay = QVBoxLayout()
        self.topLay.addLayout(self.resultLay)
        self.bar = QStatusBar(self)
        self.topLay.addWidget(self.bar)
        
        
        """ strSQL переменная для хранения SQL запроса"""
        self.strSQL = strSQL
        self.db = None
        
    def _getter(self):
        #print("getter used")
        return self.strSQL
            
    def _setter(self, value):
        #print("setter used")
        self.strSQL = value
            
    def _deleter(self):
        #print("deleter used")
        pass
            
    prop = property(_getter, _setter, _deleter, "doc string")
                
    def __call__(self):
        print("Значение=", self.strSQL)
    
    def showQueryResult(self):
        err = self.query.lastError()
        if err.type() != QSqlError.NoError:
            self.bar.showMessage("Error {0} {1}".format(err.number(), err.text()))
            print("Error"+err.text())
        else:
            self.bar.showMessage(
                "Rows affected: {0}  Rows: {1}".format(self.query.numRowsAffected(), self.query.size() ))
            
            #print(str(self.query.numRowsAffected()))    

            
            
            res = self.query.result()
            while res and self.query.isSelect():
                #------------------- Вариант 1------------------------
                # self.tabView.sqlModel = QSqlQueryModel(self.tabView)
                # self.tabView.sqlModel.setQuery(self.query)
                # self.query.first()
                # # промежуточная модель
                # self.tabView.proxyModel = QSortFilterProxyModel(self.tabView)
                # #устанавливаем базовую модель
                # self.tabView.proxyModel.setSourceModel(self.tabView.sqlModel)
                # #передаем промежуточную модель в таблицу
                # self.tabView.setModel(self.tabView.proxyModel)
                # #w.setModel(w.sqlModel) #вариант без прокси
                # if not self.query.nextResult():
                    # break
                # res = self.query.result()
                #------------------- Вариант 2  через createTableView-------------------------
                w = self.createTableView()
                w.sqlModel = QSqlQueryModel(w)
                w.sqlModel.setQuery(self.query)
                self.query.first()
                w.proxyModel = QSortFilterProxyModel(w)
                w.proxyModel.setSourceModel(w.sqlModel)
                w.setModel(w.proxyModel)
                self.resultLay.addWidget(w)
                #if self.query.nextResult():
                    #break
                #res = self.query.result()
                break
                
                
    # Очищает слой при повторном нажатии выполнить
    def clearResult(self):
        self.bar.clearMessage()
        while (self.resultLay.count() > 0 ):
            child = self.resultLay.takeAt(0)
            w = child.widget()
            if w != None: w.deleteLater()            
               
    # Создаем  таблицу для каждого запроса
    def createTableView(self):
        view = QTableView(self)
        vh = view.verticalHeader()
        vh.setDefaultSectionSize(19)
        view.setSortingEnabled(True)
        view.horizontalHeader().setSectionsMovable(True)
        view.horizontalHeader().setSortIndicator(-1,Qt.AscendingOrder)
        return view

    #нажимаем кнопку  Выполнить
    @QtCore.pyqtSlot()
    def run_clicked(self):
        #print("Выполнить скрипт:"+self.strSQL)
        self.clearResult()
        #строка с текстом запроса 
        vSqls = self.strSQL;
        
        vSql = vSqls.split(';')
        
        
        self.db = createConnection('myDB')
        if self.db.open():
            if len(vSql)>=1:
                for sqlString in vSql:
                    if sqlString != "":
                        print("Соединение установлено")
                        self.query = QSqlQuery(self.db)
                        #self.query.prepare(vSql)
                        #print(sqlString)
                
                        #------------------ в потоке ------------------------
                        #self.query.prepare("Select * from table_two where Account = :inacc and Month = :inmonth and Year = :inyear")
                        #self.query.prepare("SELECT * FROM table_one WHERE id >= :key; SELECT * FROM table_one WHERE id = 1")
                        #self.query.prepare(sqlString)
                        self.query.setNumericalPrecisionPolicy(QSql.HighPrecision)
                        
                        # прочитаем все QlineEdit на форме
                        for child in self.findChildren(QLineEdit):
                            #child.clear()
                            
                            par = ':' + child.objectName()
                            sqlString = sqlString.replace(par, child.text())
                            #s = sqlString.replace(':inacc', '1234567')
                            #self.query.bindValue(par, child.text())
                            #self.query.bindValue(':inacc', '1234567')
                            #self.query.bindValue(':inmonth', 4)
                            #self.query.bindValue(':inyear', 2020)
                           
                           
                            
                        #print(sqlString)
                        
                        self.query.exec(sqlString)
                        self.showQueryResult()
                        
                        #self.tr = QueryRunner(self.query)  
                        #self.tr.finished.connect(self.showQueryResult)
                        #self.tr.start()
                
                
                
                        # #---------------------  без потока ---------------
                        # self.query.exec("SELECT * FROM table_one")

                        # w = self.tabView
                        # w.sqlModel = QSqlQueryModel(w)
                        # w.sqlModel.setQuery(self.query)
                        # self.query.first()
                        # w.proxyModel = QSortFilterProxyModel(w)
                        # w.proxyModel.setSourceModel(w.sqlModel)
                        # w.setModel(w.proxyModel)
                        # #w.setModel(w.sqlModel)

        else:
            print("FALSE db")
