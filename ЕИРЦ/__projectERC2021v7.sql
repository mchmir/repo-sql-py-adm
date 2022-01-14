use GEFEST
go
--------------------------------------------------------------------------

declare @db_id as int

set @db_id = (select
    DB_ID(N'Gefest'))

print (N'IDBD базы данных Gefest - ' + CONVERT(varchar(10), @db_id))

--DBCC FLUSHPROCINDB(@db_id)

--------------------------------------------------------------------------
if OBJECT_ID(N'dbo.AAAERC7', 'U') is not null
  drop table DBO.AAAERC7;
go
--------------------------------------------------------------------------
use GEFEST
go
--------------------------------------------------------------------------
if DB_NAME() <> N'Gefest'
  set noexec on
go
--------------------------------------------------------------------------
--
-- Создать таблицу [dbo].[AAAERC]
--
print (N'Создать таблицу [dbo].[AAAERC7]')
go
--------------------------------------------------------------------------
create table DBO.AAAERC7 (
  ACCOUNT varchar(50) null
 ,YEAR int null
 ,MONTH int null
 ,FIO varchar(500) null
 ,PERSUSTR varchar(250) null
 ,SALDONACHGAZ float null
 ,SALDONACHPENYA float null
 ,SALDONACHSUDVZYSK float null
 ,SALDONACHSUDIZDER float null
 ,SALDONACHSHTRAF float null
 ,SALDONACHDOPUSLUG float null
 ,SALDONACHITOG float null
 ,OPLACHENOGAZ float null
 ,OPLACHENOPENYA float null
 ,OPLACHENOSUDVZYSK float null
 ,OPLACHENOSUDIZDER float null
 ,OPLACHENOSHTRAF float null
 ,OPLACHENODOPUSLUG float null
 ,OPLACHENOITOG float null
 ,PREDPOKAZ float null
 ,TEKPOKAZ float null
 ,CENA float null
 ,KOLVO varchar(8000) null
 ,SUMMANACH float null
 ,SUMMNACHISLGAZ float null
 ,SUMMNACHISLPENYA float null
 ,SUMMNACHISLVZYSK float null
 ,SUMMNACHISLIZDER float null
 ,SUMMNACHISLSHTRAF float null
 ,SUMMNACHISLDOPUSLUG float null
 ,SUMMNACHISLITOG float null
 ,SALDOKONECGAZ float null
 ,SALDOKONECPENYA float null
 ,SALDOKONECVZYSK float null
 ,SALDOKONECIZDER float null
 ,SALDOKONECSHTRAF float null
 ,SALDOKONECDOPUSLUG float null
 ,SALDOKONECITOG float null
 ,PRIMECHANIE varchar(250) null
 ,STREET varchar(50) null
 ,HOUSENUMBER varchar(10) null
 ,HOUSENUMBERCHAR varchar(20) null
 ,FLAT varchar(20) null
) on [PRIMARY]
go
--------------------------------------------------------------------------
if exists (select
      *
    from TEMPDB.SYS.TABLES
    where NAME like '#tmpAAA%')
  drop table #TMPAAA
go
--------------------------------------------------------------------------

set nocount on;
go
--------------------------------------------------------------------------
declare @idperiod as int
declare @month as int
declare @year as int

------------------------------------
------------------------------------
------------------------------------
set @month = 01
set @year = 2022
------------------------------------
------------------------------------
------------------------------------


set @idperiod = (select
    P.IDPERIOD
  from PERIOD P
  where P.YEAR = @year
  and P.MONTH = @month)


declare @maxint int
set @maxint = 0

select distinct
  G.IDGRU into #TMPGRU
from GRU G
left join GOBJECT GT
  on GT.IDGRU = G.IDGRU
left join DBO.CONTRACT C
  on GT.IDCONTRACT = C.IDCONTRACT
inner join PERSON P with (nolock)
  on P.IDPERSON = C.IDPERSON --AND p.isJuridical = 0
  where G.IDGRU not in (8518, 8743, 8626, 8666, 8742, 8738, 8772, 8771, 8768, 8747, 8828, 8781) --AND c.IDContract NOT IN (885122,885123,885124,885125,885126,885127,885128,885129)

-- обобщенное табличное выражение
-- нумеруем выборку ГРУ
with TESTCTE (NUM, IDGRU)
as
(select
    ROW_NUMBER() over (order by G.IDGRU) NUM
   ,G.IDGRU
  from #TMPGRU G)
select
  * into #TMPAAA
from TESTCTE

--Запрос, в котором мы можем использовать CTE
set @maxint = (select
    MAX(A.NUM)
  from #TMPAAA A)

print ('Количество ГРУ = ' + CAST(@maxint as varchar))

declare @i int
declare @j int

declare @idgru int
declare @idcontract int

declare @query nvarchar(max)

set @i = 1

while @i <= @maxint
begin
set @idgru = (select
    IDGRU
  from #TMPAAA A
  where A.NUM = @i)
--PRINT('....обрабатывается...idGRU'+CAST(@idGRU AS VARCHAR))

set @query = 'INSERT INTO dbo.AAAERC7  EXEC repCountNoticeERC21 ' + CAST(@idgru as varchar) + ',' + CAST(@idperiod as varchar) + ';'
exec (@query)
set @i = @i + 1

end

drop table #TMPAAA
drop table #TMPGRU

go
--------------------------------------------------------------------------
declare @countaaa as int;

set @countaaa = (select
    COUNT(*)
  from AAAERC7)

print (N'Колическтво записей - ' + CONVERT(varchar(10), @countaaa));
--------------------------------------------------------------------------
set nocount off;
go
--------------------------------------------------------------------------
exec MSDB.DBO.SP_NOTIFY_OPERATOR @profile_name = N'sqlmail'
                                ,@name = N'SqlMailAdministrator'
                                ,@subject = N'Данные для ЕИРЦ сформированы'
                                ,@body = N'Данные для ЕИРЦ сформированы.';
go
--------------------------------------------------------------------------



