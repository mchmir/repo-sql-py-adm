from sqlalchemy import create_engine, Column, Integer, String, Date, DateTime
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import sessionmaker
from datetime import date, datetime

# Создаем подключение к базе данных
engine = create_engine('postgresql://admin:AdminPG$@localhost:5432/work_final23')

# Создаем базовый класс модели
Base = declarative_base()


# Определяем модель таблицы
class MyTable(Base):
    __tablename__ = 'json_load_audit'
    __table_args__ = {'schema': 'admin_app'}
    id = Column(Integer, primary_key=True, autoincrement=True)
    # 1 Дата из файла JSON, как правило первое свойство типа массив в JSON
    json_date = Column(Date)
    # 2 Имя загружаемого файла
    json_file_name = Column(String)
    # 3 Дата загрузки файла
    json_load_datetime = Column(DateTime)


# Создаем таблицу в базе данных
Base.metadata.create_all(engine)

# Проверяем, что таблица была создана успешно
print('Таблица создана успешно')

# Создаем сессию для работы с базой данных
Session = sessionmaker(bind=engine)
session = Session()

# Добавляем тестовые данные в таблицу
data = MyTable(json_date=date.today(),
               json_file_name='test_file.json',
               json_load_datetime=datetime.now())
session.add(data)
session.commit()

# Выполняем запрос к таблице
result = session.query(MyTable).filter(MyTable.json_file_name == 'test_file.json').first()
print(result.id, result.json_date, result.json_file_name, result.json_load_datetime)
