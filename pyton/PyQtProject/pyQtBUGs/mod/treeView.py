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
from .funcs  import ConfigSectionMap, ReturnListSection



def CreateModelTreeView(path):
                
        sti = QtGui.QStandardItemModel() # без self модель существует внутри объекта
        
        # ini файл перебор
        #path = r"setting\menuList.ini"
        sectionsList = ReturnListSection(path)
        
        row = 0
        for sectionList in sectionsList:
            #добавляем узлы QStandardItem это каждый элемент модели QStandardItemModel
            rootItem1 = QtGui.QStandardItem(sectionList)
            
            rootItem1.setEditable(False)# запрет на редактирование элемента 
            
            dictSection = ConfigSectionMap(path,sectionList)
            #print(dictSection)
            for listMenu, paramsValue in dictSection.items():
                v = paramsValue.split('::')
                if len(v)>1:
                    userRoll = v[0]
                    descript = v[1]
                    
                else:
                    userRoll = v[0]
                    descript = ""
                item1 = QtGui.QStandardItem()
                item1.setData(listMenu,Qt.DisplayRole)
                item1.setEditable(False) # запрет на редактирование элемента
                item1.setData(userRoll,Qt.UserRole)
                
                item2 = QtGui.QStandardItem()
                item2.setData(descript,Qt.DisplayRole)
                item2.setEditable(False) # запрет на редактирование элемента
                
                rootItem1.appendRow([item1, item2])
                
            data = [rootItem1]
            for index, element in enumerate(data):  #enumarate() итерирует data = [10,15] ->  0,1,10 - 0,2,15 (строка - столбец - элемент)
                sti.setItem(row,index,element)
            
            row += 1
        
        #заголовки 
        sti.setHorizontalHeaderLabels(['Класс','Описание'])
        
        
        return sti
        
def getDictListTree(path,sectionsList):
    
    dictSection = ConfigSectionMap(path,sectionsList)
    
    return dictSection
    
    
      
 
        

