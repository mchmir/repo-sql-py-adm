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
from mod.funcs  import ConfigSectionMap, ReturnListSection
from mod.treeView import CreateModelTreeView

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
        
        path = r"setting\menuList.ini"
        model = CreateModelTreeView(path)
        
        self.tv.setModel(model)
        #отдаем модель с данными в Tree
        self.tv.setModel(model)
        
        #self.tv.expandAll()
        # sti.index(0,0) -> QmodelIndex
        self.tv.expand(model.index(0,0)) # открое все дочерние 1-го уровня, 0 строка и 0 столбец
        
        #ширина первого столбца 170 
        self.tv.setColumnWidth(0,170)
        
        self.setCentralWidget(self.tv)
        
        
        #------ события --------------------------------------------------------
        self.tv.selectionModel().selectionChanged.connect(self.select_click)
        # двойной клик
        self.tv.doubleClicked.connect(self.on_double_clicked)
        
    # при двойном клике по ветке дерева
    @QtCore.pyqtSlot()
    def select_click(self):
        
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
        
    @QtCore.pyqtSlot()
    def on_double_clicked(self):

        # читается вся строка!!!
        for sel in self.tv.selectedIndexes():
          #данные для обычной роли по умолчанию, взвращает словарь
          #данных который поступает в модель
          dataDR = sel.data()
          print("dataDR:" + str(dataDR))
        
          #пользовательские данные 
          #в нашей модели это имя ini-файла  из модели (txt файла)
          #dataUR = sel.data(role=Qt.UserRole)
          #print("dataUR:" + str(dataUR))"""
        
          
          
        
if __name__ == '__main__':
    import sys
    app = QtWidgets.QApplication(sys.argv)
    w = MainWindow()
    w.resize(400, 300)
    w.tv.setColumnHidden(1,True)  #скрывает второй столбец
    w.tv.resizeColumnToContents(0) # растягиваем по содержанию
    #w.tv.resizeColumnToContents(1)
    w.show()
    sys.exit(app.exec_())
        
        

