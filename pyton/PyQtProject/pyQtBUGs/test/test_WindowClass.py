#!/usr/bin/env python
# -*- coding: utf-8 -*-
#
#  test_Window.py
#  
#  Copyright 2020 mchmir
#
from  PyQt5 import QtWidgets, QtGui, QtCore
import sys


class WindowsSearch(QtWidgets.QWidget):
    """ Основное окно с двумя кнопками """
    def __init__(self, parent=None,strSQL=""):
        
          
        QtWidgets.QWidget.__init__(self,parent)
        
        self.buttonRun = QtWidgets.QPushButton("&Выполнить")
        self.buttonRun.setStyleSheet("color: blue")
        self.buttonRun.clicked.connect(self.run_clicked)

        self.buttonClose = QtWidgets.QPushButton("&Закрыть")
        self.buttonClose.setStyleSheet("color: red")
        self.buttonClose.clicked.connect(app.quit)

        self.hbox = QtWidgets.QHBoxLayout()
        self.hbox.addWidget(self.buttonRun)
        self.hbox.addWidget(self.buttonClose)
        
        self.strSQL = strSQL
        
    @property                 # чтение
    def StrSQL(self):
        #print('getting x')
        return self.strSQL
        
    @StrSQL.setter            # запись
    def StrSQL(self, value):
        #print('setting x')
        self.strSQL = value
                
    def __call__(self):
        print("Значение=", self.strSQL)

    @QtCore.pyqtSlot()
    def run_clicked(self):
        print("Выполнить скрипт:"+self.strSQL)


if __name__ == "__main__":
    app = QtWidgets.QApplication(sys.argv)
    
    #создаём окно и передаем строку запроса в переменную
    #window = WindowsSearch(strSQL="SELECT * FROM table")
    window = WindowsSearch()
    
    #print(window.strSQL)
    window.StrSQL = "SELECT * FROM TABLE3"
    print(window.StrSQL)
    
    window.setWindowTitle("New window")
    window.resize(300,300)
    
    #Дополнительная третья кнопка
    buttonRun2 = QtWidgets.QPushButton("&Выполнить")
    buttonRun2.setStyleSheet("color: maroon")
    buttonRun2.clicked.connect(WindowsSearch(strSQL="SELECT * FROM table2")) # выполняется __call__
    window.hbox.addWidget(buttonRun2)

    leMonth = QtWidgets.QLineEdit()
    #leMonth.setInputMask("09;_")
    validator = QtGui.QIntValidator(1,12)
    leMonth.setValidator(validator)
    leMonth.setPlaceholderText('Значение от 1 до 12')

    leYear = QtWidgets.QLineEdit()
    #leYear.setInputMask("9999;_")
    leYear.setValidator(QtGui.QIntValidator(2005,2100,parent=window))
    leYear.setPlaceholderText('Год от 2005 до 2100')


    leAcc = QtWidgets.QLineEdit()
    leAcc.setInputMask("9999999;_")

    textEdit = QtWidgets.QTextEdit("Здесь будет sql скрипт")

    form = QtWidgets.QFormLayout()
    form.addRow("&Лицевой счет: ", leAcc)
    form.addRow("&Месяц: ", leMonth)
    form.addRow("&Год: ", leYear)
    form.addRow("&Текст: ", textEdit)
    form.addRow(window.hbox)

    window.setLayout(form)





    window.show()

    sys.exit(app.exec_())

