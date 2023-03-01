import logging
import os
from datetime import datetime


def setup_logger():
    # создаем логгер
    logger = logging.getLogger(__name__)
    logger.setLevel(logging.DEBUG)

    # лог-файл будет создаваться каждый день
    log_dir = "log/"
    filename = f"log_load_json_{datetime.now().strftime('%Y-%m-%d')}.log"

    if not os.path.exists(log_dir + filename):
        open(log_dir + filename, "w").close()

    log_file = log_dir + filename
    # создаем обработчик для записи логов в файл
    file_handler = logging.FileHandler(log_file, encoding="utf-8")
    file_handler.setLevel(logging.DEBUG)

    # Создаем форматтер для вывода логов в файл
    # Подробный лог будет содержать даже имя функции и строку где был вызван
    # 2023-02-24 18:53:21,335 - __main__ - INFO - ...
    # Файл test_file.json уже загружен в базу данных '2023-02-24  16:35:20.245016' - load_file_if_not_loaded_yet:70
    formatter = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s - %(funcName)s:%(lineno)d')
    file_handler.setFormatter(formatter)

    # добавляем обработчик в логгер
    logger.addHandler(file_handler)

    return logger
