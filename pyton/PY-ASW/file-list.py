# в стиле линукс
# py file-list.py C:\_TASK\_DEV\16507 /
#
# все файлы
# py file-list.py C:\_TASK\_DEV\16507
#
# все файлы с расширением sql
# py file-list.py C:\_TASK\_DEV\16507 sql
#
# все файлы с расширением sql и в стиле линукс
# py file-list.py C:\_TASK\_DEV\16507 sql /


import os 
import sys

#path =r"C:\_TASK\_DEV\16507" 
ext =""

if len(sys.argv) > 1:
    path = sys.argv[1]
if len(sys.argv) > 2:
    ext = sys.argv[2]


filelist = [] 
for root, dirs, files in os.walk(path): 
  for file in files:
    if len(sys.argv) > 2 and ext!="/":
        if(file.endswith(ext)):
            filelist.append(os.path.join(root,file)) 
    else:
        filelist.append(os.path.join(root,file)) 


#print all the file names 
for name in filelist:
    file = os.path.normpath(name).split(path)[1]

    if len(sys.argv) > 3 or ext == "/":
        file = file.replace("\\","/")

    print(file)

