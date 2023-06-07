# print("Разделитель ascii-код {}, ascii-символ {}".format(str(ord('¶')), chr(182)))

in_file = 'C:/Users/User/Desktop/Новая папка/Горгаз №594.txt'

lst = ['upd', 'ins', 'if ', 'del']

with open(in_file, 'r') as read_file:
    n = 0
    for line in read_file:
        n += 1
        my_string = line.strip()
        if my_string[0:3] not in lst:
            print('проверьте строки: ', my_string[0:20], '...под номером: ', n)