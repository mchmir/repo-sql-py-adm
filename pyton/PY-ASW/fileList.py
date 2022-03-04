import os 
import sys

#path =r"C:\_TASK\_DEV\16507" 
#we shall store all the file names in this list 
if len(sys.argv) > 1:
    path = sys.argv[1]
'''if len(sys.argv) > 2:
        b = sys.argv[2]'''

filelist = [] 
for root, dirs, files in os.walk(path): 
  for file in files: 
    #append the file name to the list 
    filelist.append(os.path.join(root,file)) 
    
#print all the file names 
for name in filelist: 
    file = os.path.normpath(name).split(path)[1]
    print(file)

