-- Создать базу данных work_final23:
CREATE DATABASE work_final23;


CREATE USER adminskillbox;
ALTER USER adminskillbox WITH PASSWORD 'ski11$0x';
-- права на базу данных
GRANT ALL PRIVILEGES ON DATABASE work_final23 TO adminskillbox;

CREATE SCHEMA admin_core;
CREATE SCHEMA admin_app;


-- права на схемы
GRANT USAGE ON SCHEMA admin_core TO adminskillbox;
GRANT USAGE ON SCHEMA admin_app  TO adminskillbox;

GRANT CREATE ON SCHEMA admin_app  TO adminskillbox;
GRANT CREATE ON SCHEMA admin_core TO adminskillbox;

--SET DATABASE work_final23;

-- схема по умолчанию
ALTER ROLE adminskillbox SET search_path = admin_app, admin_core;
-- ALTER ROLE adminskillbox SET search_path to admin_app;
-- ALTER ROLE adminskillbox RESET admin_app;


ALTER DEFAULT PRIVILEGES IN SCHEMA admin_core
GRANT SELECT, INSERT, UPDATE, DELETE, REFERENCES, TRIGGER ON TABLES TO adminskillbox;

ALTER DEFAULT PRIVILEGES IN SCHEMA admin_core
GRANT USAGE, SELECT ON SEQUENCES TO adminskillbox;

ALTER DEFAULT PRIVILEGES IN SCHEMA admin_app
GRANT EXECUTE ON FUNCTIONS TO adminskillbox;



CREATE USER userskillbox;
ALTER  USER userskillbox WITH PASSWORD 'skillbox';

GRANT CONNECT ON DATABASE work_final23 TO userskillbox;
GRANT USAGE ON SCHEMA admin_app TO userskillbox;

GRANT ALL PRIVILEGES ON ALL TABLES    IN SCHEMA admin_app TO userskillbox;
GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA admin_app TO userskillbox;
GRANT ALL PRIVILEGES ON ALL FUNCTIONS IN SCHEMA admin_app TO userskillbox;
--GRANT SELECT ON ALL TABLES IN SCHEMA admin_app TO userskillbox;

ALTER ROLE userskillbox SET search_path to admin_app;

-- при создании новых таблиц, последовательностей и функций в схеме admin_app,
-- пользователю myuser будут автоматически предоставлены права на выполнение всех указанных операций.
ALTER DEFAULT PRIVILEGES IN SCHEMA admin_app
GRANT SELECT, INSERT, UPDATE, DELETE, REFERENCES, TRIGGER ON TABLES TO userskillbox;

ALTER DEFAULT PRIVILEGES IN SCHEMA admin_app
GRANT USAGE, SELECT ON SEQUENCES TO userskillbox;

ALTER DEFAULT PRIVILEGES IN SCHEMA admin_app
GRANT EXECUTE ON FUNCTIONS TO userskillbox;


-- после тестирования пользователя 
-- добавим роль для приложений
CREATE ROLE appexec;
GRANT ALL PRIVILEGES ON DATABASE work_final23 TO appexec;
GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA admin_app TO appexec;
ALTER DEFAULT PRIVILEGES IN SCHEMA admin_app GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO appexec;
ALTER DEFAULT PRIVILEGES IN SCHEMA admin_app GRANT USAGE, SELECT ON SEQUENCES TO appexec;
ALTER DEFAULT PRIVILEGES IN SCHEMA admin_app GRANT EXECUTE ON FUNCTIONS TO appexec;
GRANT userskillbox TO appexec;


-- тестовые данные
-- id, которое является первичным ключом и автоматически генерируется с помощью типа данных SERIAL
-- отношение между таблицами table1 и table2, связав поле table1_id таблицы table2 с полем id таблицы table1.
-- Это отношение является связью один ко многим, так как одна запись в table1 может иметь несколько связанных записей в table2.
CREATE TABLE admin_app.table1 (
  id SERIAL PRIMARY KEY,
  varchar_col VARCHAR(255),
  text_col TEXT,
  numeric_col NUMERIC(10,2),
  integer_col INTEGER,
  date_col DATE,
  time_col TIME,
  timestamp_col TIMESTAMP,
  boolean_col BOOLEAN,
  json_col JSON,
  xml_col XML
);

CREATE TABLE admin_app.table2 (
  id SERIAL PRIMARY KEY,
  table1_id INTEGER REFERENCES table1(id),
  varchar_col VARCHAR(255),
  text_col TEXT,
  numeric_col NUMERIC(10,2),
  integer_col INTEGER,
  date_col DATE,
  time_col TIME,
  timestamp_col TIMESTAMP,
  boolean_col BOOLEAN,
  json_col JSON,
  xml_col XML
);

CREATE TABLE admin_core.table3 (
  id SERIAL PRIMARY KEY,
  varchar_col VARCHAR(255),
  text_col TEXT,
  numeric_col NUMERIC(10,2),
  integer_col INTEGER,
  date_col DATE,
  time_col TIME,
  timestamp_col TIMESTAMP,
  boolean_col BOOLEAN,
  json_col JSON,
  xml_col XML
);

CREATE TABLE admin_core.table4 (
  id SERIAL PRIMARY KEY,
  table1_id INTEGER REFERENCES table3(id),
  varchar_col VARCHAR(255),
  text_col TEXT,
  numeric_col NUMERIC(10,2),
  integer_col INTEGER,
  date_col DATE,
  time_col TIME,
  timestamp_col TIMESTAMP,
  boolean_col BOOLEAN,
  json_col JSON,
  xml_col XML
);

CREATE TABLE orders (
    order_id SERIAL PRIMARY KEY,
    customer_name VARCHAR(255) NOT NULL,
    order_date DATE NOT NULL,
    total_amount NUMERIC(10, 2) NOT NULL
);

-- Создание таблицы "order_items"
CREATE TABLE order_items (
    item_id SERIAL PRIMARY KEY,
    order_id INTEGER NOT NULL REFERENCES orders(order_id),
    product_name VARCHAR(255) NOT NULL,
    quantity INTEGER NOT NULL,
    price NUMERIC(10, 2) NOT NULL
);

-- Заполнение таблиц тестовыми данными
INSERT INTO orders (customer_name, order_date, total_amount)
VALUES
    ('John Smith', '2022-02-01', 100.00),
    ('Jane Doe', '2022-02-02', 200.00),
    ('Bob Johnson', '2022-02-03', 300.00);

INSERT INTO order_items (order_id, product_name, quantity, price)
VALUES
    (1, 'Product 1', 2, 50.00),
    (1, 'Product 2', 1, 25.00),
    (2, 'Product 3', 3, 33.33),
    (3, 'Product 4', 4, 75.00),
    (3, 'Product 5', 2, 25.00);



-- шаблон для функций
-- все функции создаются только от admin
-- функции в имени которых нет нижнего подчеркивания
-- будем считать предоставленными функциями
SELECT x_drop_func('TestFunc');
/**
 * Тестовая функция
 */
CREATE OR REPLACE FUNCTION admin_app.TestFunc(
  IN  a_TESTPARAM1         TEXT

) RETURNS void
LANGUAGE plpgsql SECURITY DEFINER AS $$
BEGIN

  RAISE NOTICE 'We are returning a value a_TESTPARAM1 = %', a_TESTPARAM1;

END; $$;

SELECT x_grant_func('TestFunc', 'appexec');

-- под пользователем userskillbox вызов SELECT TestFunc('Проверка');
-- должен работать корректно


-- шаблон для функций
-- все функции создаются только от admin
-- таблица TABLE3 расположена в схеме CORE
-- предоставим данные через функцию для 
-- пользователя USERSKILLBOX, который не имее доступ к таблице
-- перед созданием функции мы удалим ее через спец.ф. x_drop_func
-- после создания предостави грант через спец.ф. x_grant_func
SELECT x_drop_func('TestFuncSELECT');
/**
 * Тестовая функция
 */
CREATE OR REPLACE FUNCTION admin_app.TestFuncSELECT(
  IN  a_TESTPARAM1         TEXT
) RETURNS SETOF table3 -- возврат как тип таблицы
LANGUAGE plpgsql SECURITY DEFINER AS $$  -- функция работает от имени создателя функции
DECLARE
  l_ROW                    RECORD;
BEGIN

  RETURN QUERY
  SELECT * FROM admin_core.table3;

END; $$;

SELECT x_grant_func('TestFuncSELECT', 'appexec');

-- SELECT id, varchar_col, numeric_col  FROM table3;
-- ОШИБКА: отношение "table3" не существует

-- SELECT id, varchar_col, numeric_col  FROM admin_core.table3;
-- ОШИБКА: нет доступа к схеме admin_core

SELECT * FROM TestFuncSELECT('sdds');
-- должен отображаться нормально
