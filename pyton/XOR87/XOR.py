import os
import re

def xor_cipher( str, key ):
    encript_str = ""
    for letter in str:
        encript_str += chr( ord(letter) ^ key )
    return  encript_str

def run_xor87():
    key  = 87
    file = input('Имя CFG-файла без расширения в текущей папке:')
    dir = os.path.dirname(os.path.abspath(__file__))
    f = open(dir +'\\' + file + '.cfg')
    for line in f.read().splitlines():    #readlines():
        print (xor_cipher(line, key))

    print('\n')
    response = input('Еще файл? [Y/N]:')

    if re.match("[yYдД]", response):
        run_xor87()

run_xor87()
