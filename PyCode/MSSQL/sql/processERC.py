import time
import tqdm
import re
import pandas as pd

from mod.func_sql import sql_engine

serv: str = "gg-app"
base: str = "Gefest"
user: str = "sa"
password: str = "sa"


def query_database(engine) -> int:
    sql_reader = pd.read_sql_query("SELECT COUNT(*) FROM AAAERC7", engine)
    return sql_reader.iloc[0, 0]


def main() -> None:
    # Время работы скрипта в минутах и интервал между запросами в секундах
    run_time_minutes = 15
    interval_seconds = 5

    # Общее количество итераций (запросов к базе данных)
    total_iterations = (run_time_minutes * 60) // interval_seconds

    # Максимальное количество записей для прогресс-бара
    max_records = 57200

    # Создание соединения с базой данных
    response = input('Запускаем код на выполнение? [Y/N]:')
    if re.match("[yYдД]", response):
        engine, error = sql_engine(serv, base, user, password)

        if not engine:
            print(f'Error connect to dataBase! Error {error}')
            return

        # Кастомизация прогресс-бара
        tqdm_params = {
            'total': max_records,
            'unit': 'row',
            'bar_format': '{l_bar}{bar:40}{r_bar}',
            'ncols': 100,
            'colour': 'green'
        }

        # Инициализация индикатора прогресса
        with tqdm.tqdm(**tqdm_params) as pbar:
            for _ in range(total_iterations):
                # Получение текущего количества записей в таблице
                current_count = query_database(engine)

                # Обновление индикатора прогресса
                pbar.n = current_count
                pbar.refresh()

                # Ожидание перед следующим запросом
                time.sleep(interval_seconds)


if __name__ == "__main__":
    main()
