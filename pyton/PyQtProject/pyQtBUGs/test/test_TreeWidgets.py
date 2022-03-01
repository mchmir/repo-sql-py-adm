#!/usr/bin/env python
# -*- coding: utf-8 -*-
#
#  test_TreeWidgets.py
#  
#  Copyright 2020 mchmir
#  
from PyQt5 import QtCore, QtGui, QtWidgets
from PyQt5.QtWidgets import *
from PyQt5.QtCore import *
from PyQt5 import  QtGui

tree = {
'parent 1': ["child 1-1"],
'child 1-2': [],
'child 1-3': ["child 1-1-1"],
'children 2-1': ["..."],
"parent 2": ["..."],
"parent 3": [],
"parent 4": []     
}


class MainWindow(QtWidgets.QMainWindow):
    def __init__(self, parent=None):
        super(MainWindow, self).__init__(parent)

        self.tree_view = QtWidgets.QTreeView()
        self.setCentralWidget(self.tree_view)

        model = QtGui.QStandardItemModel()
        self.populateTree(tree, model.invisibleRootItem())
        self.tree_view.setModel(model)
        model.setHeaderData(0, QtCore.Qt.Horizontal, 'Записи')
        model.setHeaderData(1, QtCore.Qt.Horizontal, 'Записи2')
        self.tree_view.expandAll()
        self.tree_view.selectionModel().selectionChanged.connect(self.onSelectionChanged)
        #self.tree_view.clicked.connect(self.on_treeview_clicked)

    def populateTree(self, children, parent):
        for child in children:
            child_item = QtGui.QStandardItem(child)
            parent.appendRow(child_item)
            if isinstance(children, dict):
                self.populateTree(children[child], child_item)

    def onSelectionChanged(self, *args):
        for sel in self.tree_view.selectedIndexes():
            val = "/"+sel.data()
            while sel.parent().isValid():
                sel = sel.parent()
                val = "/"+ sel.data()+ val
            print(val)
            

if __name__ == '__main__':
    import sys
    app = QtWidgets.QApplication(sys.argv)
    w = MainWindow()
    w.show()
    sys.exit(app.exec_())
