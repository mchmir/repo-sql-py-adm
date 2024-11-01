use Gefest
go


select * 
from period p
where p.Month = 9 and p.Year = 2024;
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

-- KILL [SPID]
-- kill 89
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


------------------------------------------------
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