# -*- coding: utf-8 -*-
from PyQt5 import QtWidgets



class MyWindow(QtWidgets.QWidget):
    def __init__(self, parent = None):
        QtWidgets.QWidget.__init__(self, parent)
        self.btnQuit = QtWidgets.QPushButton("&Закрыть окно")
        #-------------------------------------------------------------
        self.btnDialog = QtWidgets.QPushButton("Диалоговое окно")
        self.btnDialogButton = QtWidgets.QPushButton("MessageBox")
        self.btnInformation = QtWidgets.QPushButton("Вывод текстового сообщения")
        self.btnQuestion = QtWidgets.QPushButton("Окно подтверждения запроса")
        self.btnWarning = QtWidgets.QPushButton("Предупреждение")
        self.btnAbout = QtWidgets.QPushButton("О программе")
        self.btnInput1 = QtWidgets.QPushButton("Запрос текста")
        self.btnInput2 = QtWidgets.QPushButton("Запрос  числа")
        self.btnInput3 = QtWidgets.QPushButton("Выбор из списка")
        self.btnInput4 = QtWidgets.QPushButton("Выбор каталога, файла")
        self.btnError = QtWidgets.QPushButton("Сообщение об ошибке")
        
        
        
        #-------------------------------------------------------------
        self.vbox = QtWidgets.QVBoxLayout()
        #--------------------------------------------------------------
        self.vbox.addWidget(self.btnDialog)
        self.vbox.addWidget(self.btnDialogButton)
        self.vbox.addWidget(self.btnInformation)
        self.vbox.addWidget(self.btnQuestion)
        self.vbox.addWidget(self.btnWarning)
        self.vbox.addWidget(self.btnAbout)
        self.vbox.addWidget(self.btnInput1)
        self.vbox.addWidget(self.btnInput2)
        self.vbox.addWidget(self.btnInput3)
        self.vbox.addWidget(self.btnInput4)
        self.vbox.addWidget(self.btnError)
        
        self.vbox.addWidget(self.btnQuit)
        #---------------------------------------------------------------
        self.setLayout(self.vbox)
        #self.connect(self.btnQuit, QtCore.SIGNAL("clicked()"),QtWidgets.qApp.quit)
        #---------------------------------------------------------------
        self.btnDialog.clicked.connect(self.on_clicked)
        self.btnDialogButton.clicked.connect(self.on_message_click)
        self.btnInformation.clicked.connect(self.on_information)
        self.btnQuestion.clicked.connect(self.on_question)
        self.btnWarning.clicked.connect(self.on_warning)
        self.btnAbout.clicked.connect(self.on_about)
        self.btnInput1.clicked.connect(self.on_input_text)
        self.btnInput2.clicked.connect(self.on_input_int)
        self.btnInput3.clicked.connect(self.on_input_item)
        self.btnInput4.clicked.connect(self.on_input_file)
        self.btnError.clicked.connect(self.on_input_error)
        
    #---------------------------------------------------------------
    """
    Простое модальное, диалоговое окно 
    
    """  
    def on_clicked(self):
        dialog = QtWidgets.QDialog()
        dialog.setModal(True) # окно будет модальным
        result =  dialog.exec_() 
        if result == QtWidgets.QDialog.Accepted: 
            print ("Нажата кноnка ОК") 
                # Здесь nолучаем данные из диалогового окна 
        else:
            print ("Нажата кноnка Cancel") 
        
    #---------------------------------------------------------------
    """
Если в nараметре parent указана ссылка на родительское окно, то диаЛоговое окно будет 
центрироваться относительно родительского окна, а не относительно экрана. Параметр 
flags задает тиn окна (см. разд. 20.2). В nараметре <Иконка> могут быть указаны следующие 
атрибуты из класса QMessageBox: 
+ Noicon-О-нет иконки; 
+ Question-4 - иконка со знаком воnроса; 
+ Information-1-иконка информационного сообщения; 
+ warning-2-иконка nредуnреждающего сообщения; 
+ Critical-з-иконка критического сообщения. 
В nараметре buttons указываются следующие атрибуты (или их комбинация через оnера­
тор 1 ) из класса QMessageBox: 
+ NoButton-кноnки не установлены; 
+ Ok-кноnка ОК с ролью AcceptRole; 
+ Cancel-кноnка Cancel с ролью RejectRole; 
+ Yes-кноnка Yes с ролью YesRole; 
+ YesToAll-кноnка Yes to All с ролью YesRole; 
+ No-кноnка No с ролью NoRole; 
+ NoToAll-кнопка No to All с ролью NoRole; 
+ Open-кноnка Open с ролью AcceptRole; 
+ Close-кноnка Close с ролью RejectRole; 
+ Save - кноnка Save с ролью AcceptRole; 
+ SaveAll-кноnка Save All с ролью AcceptRole; 
+ Discard-кноnка Discard или Don't Save (надnись на кноnке зависит от оnерационной 
системы) с ролью DestructiveRole; 
+ Apply-кноnка Apply с ролью ApplyRole; 
+ Reset-кноnка Reset с ролью ResetRole; 
+ RestoreDefaults-кнопка Restore Defaults с ролью ResetRole; 
+ Help-кноnка Help с ролью HelpRole; 
+ AЬort-кнопка Abort с ролью RejectRole; 
+ Retry-кноnка Retry с ролью AcceptRole; 
+ Ignore-кноnка lgnore с ролью AcceptRole. 
Поведение кноnок описывается с помощью ролей. В качестве роли можно указать следую­
щие атрибуты из класса QMessageBox:  
+ Invalid.Role--1-ошибочная роль; 
+ AcceptRole - о-нажатие кнопки устанавливает код возврата равным значению атри­
бута Accepted; 
+ RejectRole-1-нажатие кнопки устанавливает код возврата равным значению .атри­
бута Rejected; 
+ DestructiveRole-2-кнопка для отказа от изменений; 
+ ActionRole-з-нажатие кнопки приводит к выполнению операции, которая не связа-на с закрытием окна; 
+ HelpRole-4-кнопка для отображения справки; 
+ YesRole-5-кнопка Yes; 
+ NoRole - б-кнопка No; 
+ ResetRole-7 - кнопка для установки значений по умолчанию; 
+ ApplyRole-в-кнопка для принятия изменений.   
    """
    def on_message_click(self):
        #dialogB = QtWidgets.QDialogButtonBox(QtWidgets.QDialogButtonBox.Ok | QtWidgets.QDialogButtonBox.YesToAll | QtWidgets.QDialogButtonBox.Save | QtWidgets.QDialogButtonBox.Cancel)
        message = QtWidgets.QMessageBox(QtWidgets.QMessageBox.Critical, "Подтверждение на выход", 
                                            "Вы действительно хотите закрыть окно?", 
                                            buttons = QtWidgets.QMessageBox.Yes | QtWidgets.QMessageBox.No |
                                            QtWidgets.QMessageBox.No)
        result2 = message.exec_()
        if result2 == QtWidgets.QMessageBox.Yes: 
            print ("Нажата кноnка YES") 
                # Здесь nолучаем данные из диалогового окна 
        else:
            print ("Нажата кноnка No")
    
    #---------------------------------------------------------------- 
    def on_information(self):
        QtWidgets.QMessageBox.information(window, "Текст заголовка", "Текст сообщения", 
                                       buttons=QtWidgets.QMessageBox.Close, 
                                       defaultButton=QtWidgets.QMessageBox.Close) 
    #-------------------------------------------------------------------------
    def on_question(self):
        QtWidgets.QMessageBox.question(window, "Текст заголовка", 
                                            "Вы действительно хотите вьmолнить действие?", 
                                            buttons=QtWidgets.QMessageBox.Yes | QtWidgets.QMessageBox.No |
                                            QtWidgets.QMessageBox.Cancel, 
                                            defaultButton=QtWidgets.QMessageBox.Cancel) 
    #----------------------------------------------------------------------------
    def on_warning(self):
        QtWidgets.QMessageBox.warning(window, "Текст заголовка", 
                                            "Вы действительно хотите вьmолнить действие?", 
                                            buttons=QtWidgets.QMessageBox.Yes | QtWidgets.QMessageBox.No |
                                            QtWidgets.QMessageBox.Cancel, 
                                            defaultButton=QtWidgets.QMessageBox.Cancel)
    #--------------------------------------------------------------------------------
    def on_about(self):
        QtWidgets.QMessageBox.about(window, "Текст заголовка", "Оnисание nрограммы") 
    #--------------------------------------------------------------------------------
    def on_input_text(self):
        s, ok = QtWidgets.QInputDialog.getText(window, "Это заголовок окна",
                                           "Это текст подсказки",
                                           text="Значение по умолчанию") 
        if ok: 
            print("Bвeдeнo значение:", s) 
    #-------------------------------------------------------------------------------
    def on_input_int(self):
        n, ok = QtWidgets.QInputDialog.getInt(window, "Это заголовок окна", 
                                          "Это текст подсказки", 
                                          value=50, min=0, max=100, step=2) 
        if ok: 
            print ("Введено значение:", n)
        
        n2, ok2 = QtWidgets.QInputDialog.getDouble(window, "Это заголовок окна", 
                                          "Это текст подсказки", 
                                          value=50.0, max=100.0, min=0.0, 
                                          decimals=2) 
        if ok2: 
            print ("Введено значение:", n2)
    #-------------------------------------------------------------------------------
    def on_input_item(self):
        s, ok = QtWidgets.QInputDialog.getItem(window, "Это заголовок окна",
                                           "Это текст подсказки", ["Пункт 1", "Пункт 2", "Пункт 3"],
                                           current=1, editable=False) 
        if ok: 
            print ("Текст выераннога пункта:", s) 
    #--------------------------------------------------------------------------------
    def on_input_file(self):
        dirM = QtWidgets.QFileDialog.getExistingDirectory(parent=window, 
                                                     directory=QtCore.QDir.currentPath())
        print(dirM)
        
        f = QtWidgets.QFileDialog.getOpenFileName(window,
                                               "Заголовок окна", directory=QtCore.QDir.currentPath(), 
                                               filter="All(*);; Images(*.png *.jpg)") 
        print(f)
    #---------------------------------------------------------------------------------
    def on_input_error(self):
        errorM = QtWidgets.QErrorMessage()
        errorM.showMessage("На данной странице была допущена ошибка")   
        errorM.exec_()
        
        progress = QtWidgets.QProgressDialog("Текст над индикатором", "Остановить процесс",0,100)
        progress.exec_()
            
    
            
            
if __name__ == "__main__":
    import sys
    app = QtWidgets.QApplication(sys.argv)
    window = MyWindow()
    window.setWindowTitle("Диалоговые окна")
    window.resize(300, 100)
    window.show()
    sys.exit(app.exec_())



