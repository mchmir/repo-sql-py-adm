#!/usr/bin/env python
# -*- coding: utf-8 -*-
#
#  test_QTreeView.py
#  
#  Copyright 2020 mchmir
#  
#  This program is free software; you can redistribute it and/or modify
from PyQt5 import QtCore, QtGui, QtWidgets
from PyQt5.QtWidgets import *
from PyQt5.QtCore import *
from PyQt5 import  QtGui

#dictTree = {}


class MainWindow(QtWidgets.QMainWindow):
    def __init__(self, parent=None):
        # унаследуем все методы и пр. 
        # вызываем конструктор базового класса для производного класса MainWindow
        super(MainWindow, self).__init__(parent)
        
        #создаем главное окно
        self.window = QtWidgets.QWidget()
        
        # задаем заголовок окна
        self.window.setWindowTitle("QStandardModel")
        # создаем иерархический список и parent у него само window
        self.tv = QtWidgets.QTreeView() 
        self.setCentralWidget(self.tv)
        # QStandardItemModel реализует двухмерную таблицу и иерархическую модели
        sti = QtGui.QStandardItemModel() # без self модель существует внутри объекта
        
        #добавляем узлы QStandardItem это каждый элемент модели QStandardItemModel
        self.rootItem1 = QtGui.QStandardItem('QAbstractItemView') #строка 1 столбец 1
        self.rootItem2 = QtGui.QStandardItem('Базовый класс') #строка 1 столбец 2
        
        self.item1 = QtGui.QStandardItem('QListView')
        self.item1.setEditable(False) # запрет на редактирование элемента
        self.item2 = QtGui.QStandardItem('Список')
        self.item2.setEditable(False)
        
        self.item3 = QtGui.QStandardItem('ID_Number = 1')
        self.item1.setData('id=1',Qt.UserRole)
        
        #добавляем строку
        self.rootItem1.appendRow([self.item1, self.item2, self.item3])
        
        self.item1 = QtGui.QStandardItem('QTableView')
        self.item2 = QtGui.QStandardItem('Таблица')
        
        self.item3 = QtGui.QStandardItem()
        self.item3.setData('id=2',role=Qt.UserRole)
        
        
        #добавляем строку
        self.rootItem1.appendRow([self.item1, self.item2, self.item3])
        
        self.item1 = QtGui.QStandardItem('QTreeView')
        self.item2 = QtGui.QStandardItem('Иерархический список')
        
        self.item3 = QtGui.QStandardItem('ID_Number = 3')
        
        
        #добавляем строку
        self.rootItem1.appendRow([self.item1, self.item2, self.item3])
        
        #добавляем в модель данные, оборачиваем
        sti.appendRow([self.rootItem1, self.rootItem2])
        
        #-------------------Принцип построение идентичный-----------------------------------
        #добавляем узлы QStandardItem это каждый элемент модели QStandardItemModel
        self.rootItem1 = QtGui.QStandardItem('QAbstractItemView') #строка 1 столбец 1
        self.rootItem2 = QtGui.QStandardItem('Базовый класс') #строка 1 столбец 2
        
        self.item1 = QtGui.QStandardItem('QListView')
        self.item2 = QtGui.QStandardItem('Список')
        self.item3 = QtGui.QStandardItem('ID_Number = 4')
        self.item3.setData(Qt.UserRole)
        
        #добавляем строку
        self.rootItem1.appendRow([self.item1, self.item2, self.item3])
        
               
        self.item1 = QtGui.QStandardItem('QTableView')
        self.item2 = QtGui.QStandardItem('Таблица')
        self.item3 = QtGui.QStandardItem('ID_Number = 5')
        self.item3.setData(Qt.UserRole)
        
        #добавляем строку
        self.rootItem1.appendRow([self.item1, self.item2, self.item3])
        
        self.item1 = QtGui.QStandardItem('QTreeView')
        self.item2 = QtGui.QStandardItem('Иерархический список')
        self.item3 = QtGui.QStandardItem('ID_Number = 6')
        self.item3.setData(Qt.UserRole)
        
        #добавляем строку
        self.rootItem1.appendRow([self.item1, self.item2, self.item3])
        
        #добавляем в модель данные, оборачиваем
        sti.appendRow([self.rootItem1, self.rootItem2])
        #------------------------------------------------------
        
        #-------------------Принцип построение идентичный-----------------------------------
        #добавляем узлы QStandardItem это каждый элемент модели QStandardItemModel
        self.rootItem1 = QtGui.QStandardItem('QAbstractItemView') #строка 1 столбец 1
        self.rootItem2 = QtGui.QStandardItem('Базовый класс') #строка 1 столбец 2
        
        self.item1 = QtGui.QStandardItem('QListView')
        self.item2 = QtGui.QStandardItem('Список')
        
        #добавляем строку
        self.rootItem1.appendRow([self.item1, self.item2])
        
        self.item1 = QtGui.QStandardItem('QTableView')
        self.item2 = QtGui.QStandardItem('Таблица')
        
        #добавляем строку
        self.rootItem1.appendRow([self.item1, self.item2])
        
        self.item1 = QtGui.QStandardItem('QTreeView')
        self.item2 = QtGui.QStandardItem('Иерархический список')
        
        #добавляем строку
        self.rootItem1.appendRow([self.item1, self.item2])
        
        #добавляем в модель данные, оборачиваем
        sti.appendRow([self.rootItem1, self.rootItem2])
        #------------------------------------------------------
        
        
        #заголовки 
        sti.setHorizontalHeaderLabels(['Класс','Описание'])
        
        #Открытие все
       
        
        #отдаем модель с данными в Tree
        self.tv.setModel(sti)
        
        #self.tv.expandAll()
        # sti.index(0,0) -> QmodelIndex
        self.tv.expand(sti.index(0,0)) # открое все дочерние 1-го уровня, 0 строка и 0 столбец
        
        #ширина первого столбца 170
        self.tv.setColumnWidth(0,170)
        
        
        #self.tv.activated.connect(self.handle_dblclick)
        self.tv.selectionModel().selectionChanged.connect(self.handle_dblclick)
        
    # при двойном клике по ветке дерева
    @QtCore.pyqtSlot()
    def handle_dblclick(self):
        
        sel = self.tv.currentIndex()
        dataDR = sel.data(role=Qt.DisplayRole)
        dataUR = sel.data(role=Qt.UserRole)
        print("dataDR:" + str(dataDR))
        print("dataUR:" + str(dataUR))
        
        # читается вся строка!!!
        #for sel in self.tv.selectedIndexes():
           
          #данные для обычной роли по умолчанию, взвращает словарь
          #данных который поступает в модель
          #dataDR = sel.data(role=Qt.DisplayRole)
          #print("dataDR:" + str(dataDR))
        
          #пользовательские данные 
          #в нашей модели это имя ini-файла  из модели (txt файла)
          #dataUR = sel.data(role=Qt.UserRole)
          #print("dataUR:" + str(dataUR))"""
        
       
          
        
if __name__ == '__main__':
    import sys
    app = QtWidgets.QApplication(sys.argv)
    w = MainWindow()
    w.resize(400, 300)
    w.show()
    sys.exit(app.exec_())
        
        

