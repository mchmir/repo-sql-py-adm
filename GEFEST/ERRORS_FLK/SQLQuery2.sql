use Gefest
go
------------------------------------------------------------------------------------------------------------------------
select * from dbo.AAAERC7;
------------------------------------------------------------------------------------------------------------------------
select *
from PERIOD P
where P.MONTH = 9
  and P.YEAR = 2025;
------------------------------------------------------------------------------------------------------------------------
----- Пользователи -----------
select S.UID,
       S.STATUS,
       S.NAME,
       S.CREATEDATE,
       S.UPDATEDATE
from SYS.SYSUSERS as S
-------------------------------------------------
select SPID          as SPID,
       db_name(DBID) as 'Имя БД',
       PROGRAM_NAME  as 'Программа',
       LOGINAME      as 'имя входа SQL Server',
       HOSTNAME      as 'Имя рабочей станции',
       STATUS
from SYS.SYSPROCESSES
where DBID > 0
order by 'Имя БД';


SELECT
    s.session_id              AS SPID,
    DB_NAME(r.database_id)    AS [Имя БД],
    s.program_name            AS [Программа],
    s.login_name              AS [имя входа SQL Server],
    s.host_name               AS [Имя рабочей станции],
    c.client_net_address      AS [Клиентский IP],
    s.status                  AS [Статус],
    c.connect_time            AS [Когда подключился]
FROM sys.dm_exec_sessions     AS s
LEFT JOIN sys.dm_exec_connections AS c ON c.session_id = s.session_id
LEFT JOIN sys.dm_exec_requests    AS r ON r.session_id = s.session_id
--WHERE s.is_user_process = 1
ORDER BY [Имя БД], s.session_id;

SELECT
  s.session_id,
  CONVERT(nvarchar(128), s.context_info) AS [HTTP пользователь],
  s.program_name, s.login_name, s.host_name, c.client_net_address
FROM sys.dm_exec_sessions s
LEFT JOIN sys.dm_exec_connections c ON c.session_id = s.session_id
WHERE s.is_user_process = 1;

-- KILL [SPID]
-- kill 81


-- 1) Спящие, но с открытой транзакцией
SELECT s.session_id, s.status, s.open_transaction_count,
       s.login_name, s.program_name, s.host_name,
       CONVERT(nvarchar(128), s.context_info) AS app_user,
       s.last_request_start_time, s.last_request_end_time
FROM sys.dm_exec_sessions s
WHERE s.is_user_process=1 AND s.open_transaction_count>0
ORDER BY s.last_request_end_time;

-- 2) Блокирующие сессии
SELECT r.blocking_session_id AS blocker,
       s.login_name, s.program_name, s.host_name,
       DB_NAME(r.database_id) AS db_name, r.wait_type, r.wait_time,
       t.text
FROM sys.dm_exec_requests r
JOIN sys.dm_exec_sessions s ON s.session_id = r.blocking_session_id
OUTER APPLY sys.dm_exec_sql_text(r.sql_handle) t
WHERE r.blocking_session_id <> 0
ORDER BY r.wait_time DESC;
-----------------------------------------------

select top 5 * 
from Period p
order by p.DateBegin Desc;

select count(*)
from AAAERC7;

select top 1000 *
from AAAERC7;

------------------------------------------------

SELECT name AS DatabaseName, DB_ID(name) AS DatabaseID
FROM sys.databases;

select *
from AAAERC7
where ACCOUNT = '1952095';

---------------------------------------------------------------
-- Монопольно захватываем базу
declare @error bit
declare @Q as VarChar(1000)

set @Q='gefest'

-- Монопольный режим
--exec @error=sp_dboption @Q,'single user', 'true'

-- Вернуть базу в рабочий режим
exec @error=sp_dboption @Q,'single user', 'false'
------------------------------------------------
-- Поиск всех объектов с  tarif
SELECT
    o.name,
    o.type,
    c.text
FROM
    sysobjects o
    INNER JOIN syscomments c ON o.id = c.id
WHERE
    c.text LIKE '%tarif%'
ORDER BY o.name


-------------------------------------------------------------
CREATE FUNCTION dbo.GenerateHash(@Input NVARCHAR(100))
RETURNS NVARCHAR(40) AS
BEGIN
    RETURN CONVERT(NVARCHAR(40), HASHBYTES('SHA1', @Input), 2)
END;
-- 1
create function dbo.binarytohex(@binary varbinary(max))
returns nvarchar(max)
as
begin
    declare @HEXMAP char(16)
    declare @HEX nvarchar(max)
    declare @i int

    set @HEXMAP = '0123456789abcdef'
    set @HEX = ''
    set @i = 1

    while (@i <= datalength(@BINARY))
    begin
        set @HEX = @HEX +
                  substring(@HEXMAP, convert(int, substring(@BINARY, @i, 1)) / 16 + 1, 1) +
                  substring(@HEXMAP, convert(int, substring(@BINARY, @i, 1)) % 16 + 1, 1)
        set @i = @i + 1
    end

    return @HEX
end;

create function dbo.GenerateHashFunc(@input nvarchar(100))
returns nvarchar(40)
with encryption
as
begin
    declare @hash varbinary(20)
    set @hash = hashbytes('sha1', 'gefestv66' + @input)
    return dbo.binarytohex(@hash)
end;

-- 2 для исправления сначала
drop function dbo.GenerateHashFunc;
-- потом заново dbo.GenerateHashFunc

------------------------------------------------------------------------------
select dbo.GenerateHashFunc(convert(nvarchar(10), getdate(), 112));
select case
    when dbo.GenerateHashFunc(convert(nvarchar(10), getdate(), 112)) = '977570D40DEF69571FC2FDA58ACA7B1085375FF2'
    then 'True'
    else 'False'
end
------------------------------------------------------------------------------
-- заупск процедуру только с помощью секретного слова
create procedure MySecureProcedure
    @HashValue nvarchar(40),
    @SecurityWord nvarchar(10)
    -- другие параметры процедуры
as
begin
    declare @today nvarchar(10)
    set @today = convert(nvarchar(10), getdate(), 112) -- формат yyyymmdd

    declare @expectedhash nvarchar(40)
    set @expectedhash = dbo.GenerateHashFunc(@SecurityWord + @today)

    if @hashvalue <> @expectedhash
    begin
        raiserror('Invalid hash code. Access is denied.', 16, 1)
        return
    end

    -- остальная часть процедуры
end;

exec MySecureProcedure '977570D40DEF69571FC2FDA58ACA7B1085375FF2', 'test';
drop procedure MySecureProcedure;

select  'Удаляем ненужные дубли в Балансах ' + cast(1 as varchar(3));