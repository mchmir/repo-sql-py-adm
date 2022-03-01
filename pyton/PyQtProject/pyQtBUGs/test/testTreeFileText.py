#!/usr/bin/env python
# -*- coding: utf-8 -*-
#
#  testTreeFileText.py
#  
#  Copyright 2020 mchmi <mchmi@DESKTOP-VBIAJ1G>
#  
from PyQt5.Qt import (
    QApplication, QWidget, QSplitter, QTreeView, QTextEdit,
    QFileSystemModel, QVBoxLayout, QDir
)
import os


class MyWidget(QWidget):
    def __init__(self, parent=None):
        super().__init__(parent)

        self.setWindowTitle('Direct tree')

        self.model = QFileSystemModel()
        self.model.setRootPath(QDir.rootPath())

        self.tree = QTreeView()
        self.tree.setModel(self.model)
        self.tree.setRootIndex(self.model.index(os.getcwd()))
        self.tree.doubleClicked.connect(self._on_double_clicked)
        self.tree.setAnimated(False)
        self.tree.setIndentation(20)
        self.tree.setSortingEnabled(True)

        self.textEdit = QTextEdit()

        splitter = QSplitter()
        splitter.addWidget(self.tree)
        splitter.addWidget(self.textEdit)
        splitter.setSizes([50, 200])

        main_layout = QVBoxLayout()
        main_layout.addWidget(splitter)
        self.setLayout(main_layout)

    def _on_double_clicked(self, index):
        file_name = self.model.filePath(index)

        with open(file_name, encoding='utf-8') as f:
            text = f.read()
            self.textEdit.setPlainText(text)


if __name__ == "__main__":
    app = QApplication([])

    win = MyWidget()
    win.resize(600, 400)
    win.show()

    app.exec()
