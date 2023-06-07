from tkinter import *
root = Tk()
btn = Button(root, text ='Кнопка', width = 10, height = 2, bg = 'white', fg = 'black', font = 'Arial 14' )
lab = Label(root, text = 'Фамилия:', font = 'Arial 14')
Edit = Entry(root,width = 20)
btn.pack()
lab.pack()
Edit.pack()
root.mainloop()