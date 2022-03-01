#!/usr/bin/env python
# -*- coding: utf-8 -*-
#
#  test_Window.py
#  
#  Copyright 2020 mchmir
#
from  PyQt5 import QtWidgets, QtGui, QtCore
import sys


@QtCore.pyqtSlot()
def run_clicked():
    print("Выполнить")



app = QtWidgets.QApplication(sys.argv)
window = QtWidgets.QWidget()
window.setWindowTitle("New window")
window.resize(300,300)


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

buttonRun = QtWidgets.QPushButton("&Выполнить")
buttonRun.setStyleSheet("color: blue")
buttonRun.clicked.connect(run_clicked)

buttonClose = QtWidgets.QPushButton("&Закрыть")
buttonClose.setStyleSheet("color: red")
buttonClose.clicked.connect(app.quit)

hbox = QtWidgets.QHBoxLayout()
hbox.addWidget(buttonRun)
hbox.addWidget(buttonClose)

form = QtWidgets.QFormLayout()
form.addRow("&Лицевой счет: ", leAcc)
form.addRow("&Месяц: ", leMonth)
form.addRow("&Год: ", leYear)
form.addRow("&Текст: ", textEdit)
form.addRow(hbox)

window.setLayout(form)





window.show()

sys.exit(app.exec_())

