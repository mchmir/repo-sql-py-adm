# -*- coding: utf-8 -*-
in_file = input('Имя файла с расширением:')

lst = ['up', 'in', 'if']

with open(in_file, 'r') as read_file:
    n = 0
    for line in read_file:
        n += 1
        my_string = line.strip()
        if my_string[0:2] not in lst:
            print('проверьте строки: ', my_string[0:20], '...под номером: ', n)
print('')
print('')
print('')
input('Для выхода нажмите ENTER...')
