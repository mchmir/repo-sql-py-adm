#!/usr/bin/env python
# -*- coding: utf-8 -*-
#
#  testTreeFILEialog.py
#  
#  Copyright 2020 mchmi <mchmi@DESKTOP-VBIAJ1G>
#  
from PyQt5.QtWidgets import *
from PyQt5.QtCore import *
from PyQt5 import  QtGui

# ---------------------------------------------------------------------
class MainWindow(QMainWindow):
    def __init__(self, parent=None):
        super().__init__(parent)

        self.resize(600,400)
        self.setWindowTitle("Treeview Example")

        self.treeview =  QTreeView(self)

        self.treeview.model = QFileSystemModel()
        self.treeview.model.setRootPath(QDir.currentPath() )
        self.treeview.setModel(self.treeview.model)
        self.treeview.setColumnWidth(0, 200)

        self.setCentralWidget(self.treeview)

        self.treeview.clicked.connect(self.on_treeview_clicked)

# ---------------------------------------------------------------------

    @pyqtSlot(QModelIndex)
    def on_treeview_clicked(self, index):
        indexItem = self.treeview.model.index(index.row(), 0, index.parent())

        # path or filename selected
        fileName = self.treeview.model.fileName(indexItem)
        # full path/filename selected
        filePath = self.treeview.model.filePath(indexItem)

        print(fileName)
        print(filePath)

# ---------------------------------------------------------------------

if __name__ == '__main__':
    import sys
    app = QApplication(sys.argv)
    w = MainWindow()
    w.show()
    sys.exit(app.exec_())
