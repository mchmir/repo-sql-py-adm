from sqlalchemy import create_engine, Column, Integer, String, schema, Date, Time, Text, ForeignKey
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import relationship

# Создаем подключение к базе данных
engine = create_engine('postgresql://admin:AdminPG$@localhost:5432/work_final23')

# Создаем базовый класс модели
Base = declarative_base()


# Определяем модель таблицы
class TSession(Base):
    __tablename__ = 'ga_sessions'
    __table_args__ = {'schema': 'admin_app'}
    # 1. ID визита
    session_id = Column(String, primary_key=True)
    # 2. ID посетителя
    client_id = Column(String)
    # 3. дата визита
    visit_date = Column(Date)
    # 4. время визита (в JSON часто просто 0 или 1)?
    visit_time = Column(String)
    # 5. порядковый номер визита клиента
    visit_number = Column(Integer)
    # 6. канал привлечения
    utm_source = Column(String)
    # 7. тип привлечения
    utm_medium = Column(String)
    # 8. рекламная компания
    utm_campaign = Column(String)
    # 9. контент видимо - описание нет
    utm_adcontent = Column(String)
    # 10. ключевое слово
    utm_keyword = Column(String)
    # 11. тип устройства
    device_category = Column(String)
    # 12. ОС устройства
    device_os = Column(String)
    # 13. марка устройства
    device_brand = Column(String)
    # 14. модель устройства
    device_model = Column(String)
    # 15. разрешение экрана
    device_screen_resolution = Column(String)
    # 16. браузер
    device_browser = Column(String)
    # 17. страна
    geo_country = Column(String)
    # 18. город
    geo_city = Column(String)


# Определяем модель таблицы
class THits(Base):
    __tablename__ = 'ga_hits'
    __table_args__ = {'schema': 'admin_app'}
    # первичный ключ
    id = Column(Integer, primary_key=True, autoincrement=True)
    # 1 ID визита ключ
    session_id = Column(String, ForeignKey('admin_app.ga_sessions.session_id'))
    ga_sessions = relationship("TSession")
    # 2 дата события
    hit_date = Column(Date)
    # 3 Время события
    hit_time = Column(String)
    # 4 порядковый номер события в рамках сессии
    hit_number = Column(Integer)
    # 5 тип события
    hit_type = Column(String)
    # 6 источник события
    hit_referer = Column(Text)
    # 7 страница события
    hit_page_path = Column(Text)
    # 8 тип действия
    event_category = Column(String)
    # 9 действие
    event_action = Column(String)
    # 10 тег действия
    event_label = Column(String)
    # 11 значение результат действия
    event_value = Column(String)


# Создаем таблицу в базе данных
Base.metadata.create_all(engine)

# Проверяем, что таблица была создана успешно
print('Таблица создана успешно')
