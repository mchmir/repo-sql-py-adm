----- Пользователи -----------
select S.UID,
       S.STATUS,
       S.NAME,
       S.CREATEDATE,
       S.UPDATEDATE
from SYS.SYSUSERS as S
---------------БД---------------------------------------
SELECT name AS DatabaseName, DB_ID(name) AS DatabaseID
FROM sys.databases;
-------------Сессии------------------------------------
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
-- kill 10
------------------------------------ 02.05.2025 16:39 -----------------------------------------------------------------
select * from CLOSEPERIODLOGS where month(DATEEXEC)=7 and IDLOG >=625
order by DATEEXEC desc

------------------------------------ 02.05.2025 16:39 -----------------------------------------------------------------
select top 5 *
from Period p
order by p.DateBegin Desc;

select count(*)
from AAAERC7;

select top 1000 *
from AAAERC7;

--------------------------------------------------------------
-- Монопольно захватываем базу
declare @error bit
declare @Q as VarChar(1000)

set @Q='gefest'

-- Монопольный режим
--exec @error=sp_dboption @Q,'single user', 'true'

-- Вернуть базу в рабочий режим
exec @error=sp_dboption @Q,'single user', 'false'

------------Пересчет баланса----------------------------------
declare @IDContract INT
declare @IDPeriod INT

set @IDPERIOD = dbo.fGetIDPeriodMY(6, 2025)
set @IDCONTRACT = dbo.fGetIDContractAC(904847)

exec dbo.spRecalcBalancesRealOnePeriodByContract @IDContract, @IDPeriod
exec dbo.spRecalcBalances @IDContract, @IDPeriod




