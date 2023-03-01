from sqlalchemy import create_engine, Column, Integer, String, schema
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import sessionmaker

# Создаем подключение к базе данных
engine = create_engine('postgresql://admin:AdminPG$@localhost:5432/work_final23')

# Создаем базовый класс модели
Base = declarative_base()


# Определяем модель таблицы
class MyTable(Base):
    __tablename__ = 'mytable'
    __table_args__ = {'schema': 'admin_app'}
    id = Column(Integer, primary_key=True)
    name = Column(String)
    value = Column(Integer)


# Создаем таблицу в базе данных
Base.metadata.create_all(engine)

# Проверяем, что таблица была создана успешно
print('Таблица создана успешно')

# Создаем сессию для работы с базой данных
Session = sessionmaker(bind=engine)
session = Session()

# Добавляем данные в таблицу
data = MyTable(name='test', value=123)
session.add(data)
session.commit()

# Выполняем запрос к таблице
result = session.query(MyTable).filter(MyTable.name == 'test').first()
print(result.id, result.name, result.value)
