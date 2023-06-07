DECLARE @db_id AS INT 

SET @db_id = (SELECT DB_ID(N'Gefest'))

PRINT(N'IDBD базы данных Gefest'+CONVERT(varchar(10), @db_id))

--DBCC FLUSHPROCINDB(@db_id)

USE Gefest
GO

IF OBJECT_ID(N'dbo.AAAERC7', 'U') IS NOT NULL
  DROP TABLE dbo.AAAERC7
GO

USE Gefest
GO

IF DB_NAME() <> N'Gefest' SET NOEXEC ON
GO

--
-- Создать таблицу [dbo].[AAAERC]
--
PRINT (N'Создать таблицу [dbo].[AAAERC7]')
GO
CREATE TABLE dbo.AAAERC7 (
  Account varchar(50) NULL,
  Year int NULL,
  Month int NULL,
  fio varchar(500) NULL,
  PersUstr varchar(250) NULL,
  SaldoNachGaz float NULL,
  SaldoNachPenya float NULL,
  SaldoNachSudVzysk float NULL,
  SaldoNachSudIzder float NULL,
  SaldoNachSHtraf float NULL,
  SaldoNachDopUslug float NULL,
  SaldoNachItog float NULL,
  OplachenoGaz float NULL,
  OplachenoPenya float NULL,
  OplachenoSudVzysk float NULL,
  OplachenoSudIzder float NULL,
  OplachenoSHtraf float NULL,
  OplachenoDopUslug float NULL,
  OplachenoItog float NULL,
  PredPokaz float NULL,
  TekPokaz float NULL,
  Cena float NULL,
  Kolvo varchar(8000) NULL,
  SummaNach float NULL,
  SummNachislGaz float NULL,
  SummNachislPenya float NULL,
  SummNachislVzysk float NULL,
  SummNachislIzder float NULL,
  SummNachislSHtraf float NULL,
  SummNachislDopUslug float NULL,
  SummNachislItog float NULL,
  SaldoKonecGaz float NULL,
  SaldoKonecPenya float NULL,
  SaldoKonecVzysk float NULL,
  SaldoKonecIzder float NULL,
  SaldoKonecSHtraf float NULL,
  SaldoKonecDopUslug float NULL,
  SaldoKonecItog float NULL,
  Primechanie varchar(250) NULL,
  Street varchar(50) NULL,
  HouseNumber varchar(10) NULL,
  HouseNumberChar varchar(20) NULL,
  Flat varchar(20) NULL
)
ON [PRIMARY]
GO

if exists (select * from tempdb.sys.tables where name LIKE '#tmpAAA%') drop table #tmpAAA
GO

--TRUNCATE TABLE dbo.AAAERC7;
---ALTER TABLE dbo.AAAERC REBUILD;
--GO

--DELETE FROM AAAERC7
--GO


DECLARE @maxInt int 
SET @maxInt = 0

SELECT DISTINCT g.IDGru
INTO #tmpGRU
 FROM GRu g  
 LEFT JOIN GObject gt   ON gt.IDGru = g.IDGru
 LEFT JOIN dbo.Contract c  ON gt.IDContract = c.IDContract
 INNER JOIN Person p WITH (NOLOCK) ON p.IDPerson = c.IDPerson --AND p.isJuridical = 0
 WHERE g.IDGru  NOT IN (8518,8743,8626,8666,8742,8738,8772,8771,8768,8747,8828,8781) --AND c.IDContract NOT IN (885122,885123,885124,885125,885126,885127,885128,885129)

-- обобщенное табличное выражение
-- нумеруем выборку ГРУ
WITH TestCTE (num, idGRU) AS
(
 SELECT ROW_NUMBER() OVER (ORDER BY g.IDGru) num, g.IDGru
 FROM #tmpGRU  g  
)
SELECT * INTO #tmpAAA FROM TestCTE

--Запрос, в котором мы можем использовать CTE
SET @maxInt = (SELECT MAX(a.num) FROM #tmpAAA a)

PRINT ('Количество ГРУ ='+ CAST(@maxInt AS VARCHAR))

DECLARE @i INT
DECLARE @j INT

DECLARE @idGRU INT
DECLARE @idperiod INT
DECLARE @idContract INT

DECLARE @query NVARCHAR(MAX)

  SET @idperiod = 195
  SET @i = 1

  while @i<=@maxInt --1000
     begin
       SET @idGRU =  (SELECT idGRU FROM #tmpAAA a WHERE a.num = @i)
       --PRINT('....обрабатывается...idGRU'+CAST(@idGRU AS VARCHAR))

       SET @query = 'INSERT INTO dbo.AAAERC7  EXEC repCountNoticeERC21 '+CAST(@idGRU AS VARCHAR) +','+CAST(@idperiod AS VARCHAR) + ';'
       EXEC (@query) 
       set @i=@i+1
     
     end

DROP TABLE #tmpAAA
DROP TABLE #tmpGRU

GO


EXEC msdb.dbo.sp_notify_operator  
   @profile_name = N'sqlmail',  
   @name = N'SqlMailAdministrator',  
   @subject = N'Данные для ЕИРЦ сформированы',  
   @body = N'Данные для ЕИРЦ сформированы.' ;  
GO


--0:08:59.381  01032021
--0:09:30      01042021
--11:24:65     01052021

