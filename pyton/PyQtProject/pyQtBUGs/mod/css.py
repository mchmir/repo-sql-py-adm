#!/usr/bin/env python
# -*- coding: utf-8 -*-
#
#  css.py
#  
#  Copyright 2020 mchmir 
#  

styleSheet = """ * {
                    font-size: 14px; 
                    font-family: SegoeUE, Helvetica, sans-serif; 
                    color: #4d4d4d;}
                    
                    QTreeView {
                            alternate-background-color: #f6fafb;
                            /*background: #e8f4fc;*/}
                            
                    QTreeView::item:open {
                            /*background-color: #c5ebfb;*/
                            color: blue;}
                            
                    /*Текст */
                    QTextEdit#helperText {font-size: 14px; 
                                          font-style: italic;  
                                          font-family: 'Times New Roman', Times, serif; 
                                          color: #666699;}
                    QTableView{
                            border-style : Solid;
                            border-color : #cccccc;
                            border-width : 1px;
                            gridline-color:#c2d1f0;}

                    QTableView QHeaderView::section {background-color: qlineargradient(x1:0, y1:0, x2:0, y2:1,stop:0 #d6e0f5, stop: 0.5 #d6e0f5,stop: 0.6 #c2d1f0, stop:1 #c2d1f0);
                                                     color: #1f3d7a;
                                                     padding-left: 4px;
                                                     border: 1px solid #c2d1f0;}            
                    
            """
            
            
def getStyleWindow(param):
    if param == 'window':
        return  styleSheet
    else:
        return  ''
    
