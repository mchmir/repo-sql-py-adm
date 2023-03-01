import os.path


def move_file(file_path, new_directory):
    """
    Функция получает на вход два параметра:
    имя файла с путем и имя новой папки.
    Функция переименовывает этот файл,
    добавляет префикс "loaded_" и перемещает этот файл в новую папку.

    :param file_path: Файл с указанием пути
    :param new_directory: Новая директория для размещения
    :return: True/False и Строку, которая будет внесена в лог
    """
    file_name = ''
    try:
        file_name = os.path.basename(file_path)
        new_file_name = "loaded_" + file_name
        new_file_path = os.path.join(new_directory, new_file_name)
        os.rename(file_path, new_file_path)
        return True, f"Файл {file_name} был перемещен в {new_directory} с новым именем {new_file_name}"
    except Exception as error:
        return False, f"Файл {file_name} не был перемещен в {new_directory}! Ошибка: {error}"
