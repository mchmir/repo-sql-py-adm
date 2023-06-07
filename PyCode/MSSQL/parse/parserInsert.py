import os

# Полный путь к файлу
file_path = r"C:\Users\User\Desktop\PoveritelWORK\Горгаз №601.txt"


def copy_insert_lines(input_filename):
    # Разбиваем путь к файлу на директорию и имя файла
    directory, filename = os.path.split(input_filename)
    # Создаем имя выходного файла, добавляя префикс "insert" к имени входного файла
    output_filename = os.path.join(directory, 'insert_' + filename)

    # Открываем исходный файл для чтения и выходной файл для записи в той же кодировке
    with open(input_filename, 'r') as infile, open(output_filename, 'w', encoding='windows-1251') as outfile:
        # Читаем исходный файл построчно
        for line in infile:
            # Если строка начинается с 'insert', записываем ее в выходной файл
            if line.lower().lstrip().startswith('insert'):
                outfile.write(line)


def main():
    copy_insert_lines(file_path)


if __name__ == "__main__":
    main()
