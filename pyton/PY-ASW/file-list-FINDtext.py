#
# все файлы с расширением sql ищем текст "cm_SecuredValue"
# py file-list.py C:\_TASK\Septim sql "cm_SecuredValue"
#
# все файлы ищем текст "cm_SecuredValue"
# py file-list-FINDtext.py C:\_TASK\Septim * "cm_SecuredValue"
#
# все файлы с расширением sql ищем текст "cm_SecuredValue" и выводим строки вхождения
# py file-list.py C:\_TASK\Septim sql "cm_SecuredValue" 1
import os 
import sys


def file_list_findText():

    ext =""
    i = 0
    c_contrl = 0 
    c_file = 0
    c_line = 0

    if len(sys.argv) > 1:
        path = sys.argv[1]
    if len(sys.argv) > 2:
        ext = sys.argv[2]

    filelist = [] 
    for root, dirs, files in os.walk(path): 
      for file in files:
        if len(sys.argv) > 2 and ext!="*":
            if(file.endswith(ext)):
                filelist.append(os.path.join(root,file)) 
        else:
            filelist.append(os.path.join(root,file)) 


    #print all the file names 
    for name in filelist:
        
            file = os.path.normpath(name).split(path)[1]
            
            if len(sys.argv) > 3:
                word_search = sys.argv[3]

                if len(sys.argv) > 4:
                    with open(name, 'r', encoding = "ISO-8859-1") as f:
                        for num, line in enumerate(f):
                            if word_search in line:
                                c_contrl += 1
                                if c_contrl <= 1:
                                    c_file += 1
                                file = os.path.normpath(name).split(path)[1]
                                file = file.replace("\\","/")
                                print("[INFO-line] Found file: {0}, in line {1}".format(file, num + 1))
                                c_line += 1
                        c_contrl = 0
                else:
                    text = open(name, encoding = "ISO-8859-1").read()
                    if word_search in text:
                        file = file.replace("\\","/")
                        i += 1
                        print(file)
            else:
                file = file.replace("\\","/")
                print(file)
                i += 1

    if len(sys.argv) > 4:
        print("\r")
        print("[INFO-total] Found files: {0}, found lines: {1}, required expression: {2}".format(c_file, c_line, word_search)) 
    else:
        print("\r")
        print("[INFO-total] Found files: {0}, required expression: {1}".format(i, word_search))
        
        
def main():
    file_list_findText()
    
if __name__ == "__main__":
    main()
