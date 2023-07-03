import os.path

in_file = r"C:\Users\User\Desktop\Посылки из горгаз 2023\Горгаз №607.txt"

lst = ['upd', 'ins', 'if ', 'del', 'dbc', 'beg', 'com', 'dec']

if os.path.exists(in_file):
    with open(in_file, 'r') as read_file:
        n = 0
        for line in read_file:
            n += 1
            my_string = line.strip()
            if my_string[0:3].lower() not in lst:
                print('проверьте строки: ', my_string[0:20], '...под номером: ', n)
else:
    print('Файл не существует:', in_file)

# print("Разделитель ascii-код {}, ascii-символ {}".format(str(ord('¶')), chr(182)))
