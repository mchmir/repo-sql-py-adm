from sqlalchemy import create_engine, MetaData, Table
from sqlalchemy.exc import NoSuchTableError

engine = create_engine('postgresql://admin:AdminPG$@localhost:5432/work_final23')
meta = MetaData(schema='admin_app', bind=engine)


tables_to_drop = ['table2', 'table1', 'order_items', 'orders', 'mytable']

for table_name in tables_to_drop:
    try:
        table = Table(table_name, meta, autoload=True, autoload_with=engine, schema='admin_app')
    except NoSuchTableError:
        print(f"Таблицы {table_name} нет в базе данных.")
    else:
        table.drop()
        print(f"Таблица {table_name} удалена.")
