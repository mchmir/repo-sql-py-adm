in_file = r"C:\Users\User\Desktop\Посылки из горгаз 2023\Горгаз №590.txt"

lst = ['upd', 'ins', 'if ', 'del']

with open(in_file, 'r') as read_file:
    n = 0
    for line in read_file:
        n += 1
        my_string = line.strip()
        if my_string[0:3] not in lst:
            print('проверьте строки: ', my_string[0:20], '...под номером: ', n)


# print("Разделитель ascii-код {}, ascii-символ {}".format(str(ord('¶')), chr(182)))
