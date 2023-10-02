
in_file = r"C:\Users\User\Desktop\PoveritelWORK\toPoveritel\Горгаз №618.txt"

lst = 'upd', 'ins', 'if ', 'del', 'dbc', 'beg', 'com', 'dec',

try:
    with open(in_file, 'r') as read_file:
        n = 0
        for line in read_file:
            n += 1
            my_string = line.strip()
            if my_string[0:3].lower() not in lst:
                print('проверьте строки: ', my_string[0:20], '...под номером: ', n)
except FileNotFoundError:
    print('Файл не существует:', in_file)

# print("Разделитель ascii-код {}, ascii-символ {}".format(str(ord('¶')), chr(182)))
