import os.path
from classProject.loadJSON import load_json_session, load_json_hits

global_lte_new = 'data/lte/new'


def load_json_files():
    """
    Основная фабрика для обработки JSON
    Функция просматриваю специально отведенную папку и
    по имени файла запускает специальные функции для обработки
    :return: Ничего не возвращает. Это основная функция процесса.
    """
    # сначала загружаем session, так как она имеет первичный ключ
    for file_name in os.listdir(global_lte_new):
        if file_name.endswith(".json"):
            if "session" in file_name:
                load_json_session(file_name)

    # загружаем таблицу hits
    for file_name in os.listdir(global_lte_new):
        if file_name.endswith(".json"):
            # сначала загружаем session, так как она имеет первичный ключ
            if "hits" in file_name:
                load_json_hits(file_name)


if __name__ == "__main__":
    load_json_files()
