import os.path
import json
from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker
from datetime import datetime
from sqlalchemy import exc

from classProject.logger_load_json import setup_logger
# классы определения таблиц
from classProject.tableClass import AuditTable, THits, TSession
from classProject.funcAdditional import move_file

# Путь к загружаемым файлам
global_lte_new = 'data/lte/new'
global_lte_loaded = 'data/lte/loaded'
# строка подключения - может быть любой, к любому типу серверу
global_create_engine = 'postgresql://admin:AdminPG$@localhost:5432/work_final23'

# Создаем форматтер для вывода логов в файл
# Подробный лог будет содержать даже имя функции и строку где был вызван
# 2023-02-24 18:53:21,335 - __main__ - INFO - ...
# Файл test_file.json уже загружен в базу данных '2023-02-24  16:35:20.245016' - load_file_if_not_loaded_yet:70
logger = setup_logger()


def load_file_if_not_loaded_yet(json_file_name):
    """
    Функция проверяет по имени файла, загружался ли он в базу.
    Данная информация находится в таблице аудита.
    При результате False в лог будет добавлена запись

    :param json_file_name: загружаемый JSON
    :return: True или False

    """
    # Создаем подключение к базе данных
    engine = create_engine(global_create_engine)
    session = sessionmaker(bind=engine)
    session = session()

    file_name = os.path.basename(json_file_name)

    # Проверяем, есть ли файл уже в базе данных
    record = session.query(AuditTable).filter_by(json_file_name=file_name).first()
    if record is not None:
        logger.info(f"Файл {file_name} уже загружен в базу данных '{record.json_load_datetime}'")
        return False

    return True


def load_json_hits(json_file_name):
    """
    Функция обрабатывает JSON файл с данными hits

    :param json_file_name: JSON файл
    :return:
    """
    logger.info(f"Начата загрузка файла '{json_file_name}'")
    # Проверяем если такой файл уже загружался
    if load_file_if_not_loaded_yet(json_file_name):

        with open(global_lte_new + '/' + json_file_name, 'r') as json_file:
            # Чтобы заменить все NaN на null, установим parse_constant через функцию
            json_obj = json.load(json_file,
                                 parse_constant=lambda x: None if x.lower() == 'nan' else json.decoder.FLOAT_REPR(x))

        # получаем ключ первого элемента словаря
        data = next(iter(json_obj))  # получаем ключ первого элемента словаря
        date_create_json = datetime.strptime(data, '%Y-%m-%d')

        # получаем значение первого элемента словаря
        dataset = json_obj[data]

        if len(dataset) == 0:
            logger.info(f"Файл {json_file_name} не содержит данных.'")
            move_file(global_lte_new + '/' + json_file_name, global_lte_loaded)
            return

        if load_data(dataset, 'hits'):
            # Если загрузка файла прошла успешна,
            # то заполним таблицу аудита
            logger.info(f"Успешная загрузка данных в таблицу GA_HITS: загружен файл'{json_file_name}'")

            # переименовываем и перемещаем загруженный файл
            result, message = move_file(global_lte_new + '/' + json_file_name, global_lte_loaded)
            if result:
                logger.info(message)
            else:
                logger.warning(message)

            if load_audit(date_create_json, json_file_name):
                logger.info(f"Успешная загрузка данных в таблицу аудита")
            else:
                logger.warning(f"Данные при загрузке файла '{json_file_name}' не попали в таблицу аудита! "
                               f"Проверьте данные!")
        else:
            logger.warning(f"Загрузка файла {json_file_name}, в таблицу GA_HITS завершилась ошибкой.'")
    else:
        pass


def load_json_session(json_file_name):
    """
    Функция обрабатывает JSON файл с данными hits

    :param json_file_name: JSON файл
    :return:
    """
    logger.info(f"Начата загрузка файла '{json_file_name}'")
    # Проверяем если такой файл уже загружался
    if load_file_if_not_loaded_yet(json_file_name):

        with open(global_lte_new + '/' + json_file_name, 'r') as json_file:
            # Чтобы заменить все NaN на null, установим parse_constant через функцию
            json_obj = json.load(json_file,
                                 parse_constant=lambda x: None if x.lower() == 'nan' else json.decoder.FLOAT_REPR(x))

        # получаем ключ первого элемента словаря
        data = next(iter(json_obj))  # получаем ключ первого элемента словаря
        date_create_json = datetime.strptime(data, '%Y-%m-%d')

        # получаем значение первого элемента словаря
        dataset = json_obj[data]

        if len(dataset) == 0:
            logger.info(f"Файл {json_file_name} не содержит данных.'")
            move_file(global_lte_new + '/' + json_file_name, global_lte_loaded)
            return

        if load_data(dataset, 'session'):
            # Если загрузка файла прошла успешна,
            # то заполним таблицу аудита
            logger.info(f"Успешная загрузка данных в таблицу GA_SESSION: загружен файл '{json_file_name}'")

            # переименовываем и перемещаем загруженный файл
            result, message = move_file(global_lte_new + '/' + json_file_name, global_lte_loaded)
            if result:
                logger.info(message)
            else:
                logger.warning(message)

            if load_audit(date_create_json, json_file_name):
                logger.info(f"Успешная загрузка данных в таблицу аудита")
            else:
                logger.warning(f"Данные при загрузке файла '{json_file_name}' не попали в таблицу аудита! "
                               f"Проверьте данные!")
        else:
            logger.warning(f"Загрузка файла {json_file_name}, в таблицу GA_SESSION завершилась ошибкой.'")
    else:
        pass


def load_audit(date_create_json, json_file_name):
    """
    Функция загружает данные в таблицу аудита

    :param date_create_json: Дата JSONA
    :param json_file_name:  имя файла
    :return: Возвращает True или False
            Ведет подробное логирование
    """
    # создаем соединение с базой данных
    engine = create_engine(global_create_engine)

    # создаем сессию для работы с базой данных
    session = sessionmaker(bind=engine)
    session = session()

    try:
        audit = AuditTable(
            json_date=date_create_json,
            json_file_name=json_file_name,
            json_load_datetime=datetime.now()
        )
        session.add(audit)
        session.commit()
    except exc.SQLAlchemyError as error:
        session.rollback()  # Rollback the changes on error
        logger.warning(f"Загрузка данных в таблицу аудита завершилась ошибкой: '{error}'")
        return False
    finally:
        session.close()  # Close the connection

    return True


def load_data(dataset, table_name):
    """
    Функция загружает данные в таблицы: по имени таблицы
    и в таблицу аудита, в случае успешной загрузки

    :param dataset:  Собственно сам JSON
    :param table_name: имя таблицы hits или session
    :return: True или False
    """
    # создаем соединение с базой данных
    engine = create_engine(global_create_engine)

    # создаем сессию для работы с базой данных
    session = sessionmaker(bind=engine)
    session = session()

    if table_name == 'hits':
        count = 0
        # преобразуем датасет и сохраняем данные в таблицу hits
        try:
            for data in dataset:
                try:
                    new_hit = THits(session_id=data['session_id'],
                                    hit_date=datetime.strptime(data['hit_date'], '%Y-%m-%d').date(),
                                    hit_time=data['hit_time'],
                                    hit_number=data['hit_number'],
                                    hit_type=data['hit_type'],
                                    hit_referer=data['hit_referer'],
                                    hit_page_path=data['hit_page_path'],
                                    event_category=data['event_category'],
                                    event_action=data['event_action'],
                                    event_label=data['event_label'],
                                    event_value=data['event_value'])
                    session.add(new_hit)
                    session.commit()
                    count += 1
                except exc.SQLAlchemyError as error:
                    session.rollback()  # Rollback the changes on error
                    logger.warning(f"Загрузка данных в таблицу GA_HITS для session_id={data['session_id']} "
                                   f"завершилась ошибкой: '{error}'")
        except exc.SQLAlchemyError as error:
            session.rollback()  # Rollback the changes on error
            logger.warning(f"Загрузка данных в таблицу GA_HITS завершилась ошибкой: '{error}'")
            return False
        finally:
            session.close()  # Close the connection
        logger.info(f"Загружено записей: '{count}'")
        return True

    elif table_name == 'session':
        count = 0
        # преобразуем датасет и сохраняем данные в таблицу hits
        try:
            for data in dataset:
                try:
                    # проверяем по ключу
                    existing_hit = session.query(THits).filter_by(
                        session_id=data['session_id']
                    ).first()

                    if existing_hit:
                        # Обновляем атрибуты существующей записи
                        existing_hit.client_id = data['client_id'],
                        existing_hit.visit_date = datetime.strptime(data['visit_date'], '%Y-%m-%d').date(),
                        existing_hit.visit_time = data['visit_time'],
                        existing_hit.visit_number = data['visit_number'],
                        existing_hit.utm_source = data['utm_source'],
                        existing_hit.utm_medium = data['utm_medium'],
                        existing_hit.utm_campaign = data['utm_campaign'],
                        existing_hit.utm_adcontent = data['utm_adcontent'],
                        existing_hit.utm_keyword = data['utm_keyword'],
                        existing_hit.device_category = data['device_category'],
                        existing_hit.device_os = data['device_os'],
                        existing_hit.device_brand = data['device_brand'],
                        existing_hit.device_model = data['device_model'],
                        existing_hit.device_screen_resolution = data['device_screen_resolution'],
                        existing_hit.device_browser = data['device_browser'],
                        existing_hit.geo_country = data['geo_country'],
                        existing_hit.geo_city = data['geo_city']
                        logger.info(f"Запись в таблице GA_SESSION с ID:{data['session_id']} "
                                    f"уже существует и будет обновлена.")
                    else:
                        # Создаем новую запись
                        visits = TSession(session_id=data['session_id'],
                                          client_id=data['client_id'],
                                          visit_date=datetime.strptime(data['visit_date'], '%Y-%m-%d').date(),
                                          visit_time=data['visit_time'],
                                          visit_number=data['visit_number'],
                                          utm_source=data['utm_source'],
                                          utm_medium=data['utm_medium'],
                                          utm_campaign=data['utm_campaign'],
                                          utm_adcontent=data['utm_adcontent'],
                                          utm_keyword=data['utm_keyword'],
                                          device_category=data['device_category'],
                                          device_os=data['device_os'],
                                          device_brand=data['device_brand'],
                                          device_model=data['device_model'],
                                          device_screen_resolution=data['device_screen_resolution'],
                                          device_browser=data['device_browser'],
                                          geo_country=data['geo_country'],
                                          geo_city=data['geo_city'])

                        session.add(visits)
                        session.commit()
                        count += 1
                except exc.SQLAlchemyError as error:
                    session.rollback()  # Rollback the changes on error
                    logger.warning(f"Загрузка данных в таблицу GA_SESSION для session_id={data['session_id']} "
                                   f"завершилась ошибкой: '{error}'")
        except exc.SQLAlchemyError as error:
            session.rollback()  # Rollback the changes on error
            logger.warning(f"Загрузка данных в таблицу GA_SESSION завершилась ошибкой: '{error}'")
            return False
        finally:
            session.close()  # Close the connection
        logger.info(f"Загружено записей: '{count}'")
        return True
