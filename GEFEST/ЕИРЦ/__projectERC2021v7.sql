USE Gefest
GO
--------------------------------------------------------------------------

DECLARE @db_id AS INT 

SET @db_id = (SELECT DB_ID(N'Gefest'))

PRINT(N'IDBD базы данных Gefest - ' + CONVERT(varchar(10), @db_id))

--DBCC FLUSHPROCINDB(@db_id)

--------------------------------------------------------------------------
IF OBJECT_ID(N'dbo.AAAERC7', 'U') IS NOT NULL
  DROP TABLE dbo.AAAERC7;
GO
--------------------------------------------------------------------------
USE Gefest
GO
--------------------------------------------------------------------------
IF DB_NAME() <> N'Gefest' SET NOEXEC ON
GO
--------------------------------------------------------------------------
--
-- Создать таблицу [dbo].[AAAERC]
--
PRINT (N'Создать таблицу [dbo].[AAAERC7]')
GO
--------------------------------------------------------------------------
CREATE TABLE dbo.AAAERC7 (
  Account             varchar(50) NULL,
  Year                int NULL,
  Month               int NULL,
  fio                 varchar(500) NULL,
  PersUstr            varchar(250) NULL,
  SaldoNachGaz        float NULL,
  SaldoNachPenya      float NULL,
  SaldoNachSudVzysk   float NULL,
  SaldoNachSudIzder   float NULL,
  SaldoNachSHtraf     float NULL,
  SaldoNachDopUslug   float NULL,
  SaldoNachItog       float NULL,
  OplachenoGaz        float NULL,
  OplachenoPenya      float NULL,
  OplachenoSudVzysk   float NULL,
  OplachenoSudIzder   float NULL,
  OplachenoSHtraf     float NULL,
  OplachenoDopUslug   float NULL,
  OplachenoItog       float NULL,
  PredPokaz           float NULL,
  TekPokaz            float NULL,
  Cena                float NULL,
  Kolvo               varchar(8000) NULL,
  SummaNach           float NULL,
  SummNachislGaz      float NULL,
  SummNachislPenya    float NULL,
  SummNachislVzysk    float NULL,
  SummNachislIzder    float NULL,
  SummNachislSHtraf   float NULL,
  SummNachislDopUslug float NULL,
  SummNachislItog     float NULL,
  SaldoKonecGaz       float NULL,
  SaldoKonecPenya     float NULL,
  SaldoKonecVzysk     float NULL,
  SaldoKonecIzder     float NULL,
  SaldoKonecSHtraf    float NULL,
  SaldoKonecDopUslug  float NULL,
  SaldoKonecItog      float NULL,
  Primechanie         varchar(250) NULL,
  Street              varchar(50) NULL,
  HouseNumber         varchar(10) NULL,
  HouseNumberChar     varchar(20) NULL,
  Flat                varchar(20) NULL
)
ON [PRIMARY]
GO
--------------------------------------------------------------------------
if exists (select * 
             from tempdb.sys.tables
            where name LIKE '#tmpAAA%'
          )
  drop table #tmpAAA
GO
--------------------------------------------------------------------------
SET NOCOUNT ON;
GO
--------------------------------------------------------------------------
DECLARE @IDPeriod AS INT
DECLARE @Month    AS INT
DECLARE @Year     AS INT

------------------------------------
------------------------------------
------------------------------------
      SET @Month = 7
      SET @Year = 2023
------------------------------------
------------------------------------
------------------------------------


SET @IDPeriod = dbo.fGetIDPeriodMY(@Month, @Year);

DECLARE @maxInt INT;
SET     @maxInt = 0;

SELECT DISTINCT
       g.IDGru
  INTO #tmpGRU
  FROM GRu AS g
    LEFT JOIN  dbo.GObject  AS gt ON gt.IDGru      = g.IDGru
    LEFT JOIN  dbo.Contract AS c  ON gt.IDContract = c.IDContract
    INNER JOIN dbo.Person AS p WITH (NOLOCK) ON p.IDPerson = c.IDPerson --AND p.isJuridical = 0
 WHERE g.IDGru NOT IN (8518, 8743, 8626, 8666, 8742, 8738, 8772, 8771, 8768, 8747, 8828, 8781)
     --AND c.IDContract NOT IN (885122,885123,885124,885125,885126,885127,885128,885129)

-- обобщенное табличное выражение
-- нумеруем выборку ГРУ
WITH TestCTE (num, idGRU) AS
(
 SELECT 
        ROW_NUMBER() OVER (ORDER BY g.IDGru) num,
        g.IDGru
   FROM #tmpGRU AS g
)
SELECT * INTO #tmpAAA FROM TestCTE;

--Запрос, в котором мы можем использовать CTE
SET @maxInt = (SELECT MAX(a.num) FROM #tmpAAA AS a);

PRINT (N'Количество ГРУ = '+ CAST(@maxInt AS VARCHAR));

DECLARE @i INT;
DECLARE @idGRU INT;
DECLARE @query NVARCHAR(MAX);

  SET @i = 1;

  WHILE @i <= @maxInt
    BEGIN
      SET @idGRU =  (SELECT idGRU FROM #tmpAAA a WHERE a.num = @i)
      SET @query = 'INSERT INTO dbo.AAAERC7  EXEC repCountNoticeERC21 ' + CAST(@idGRU AS VARCHAR) + ',' + CAST(@IDPeriod AS VARCHAR) + ';'
      EXEC (@query)
      SET @i = @i + 1
    END

DROP TABLE #tmpAAA;
DROP TABLE #tmpGRU;

GO
--------------------------------------------------------------------------
DECLARE @countAAA as int;

SET @countAAA = (SELECT count(*)
                   FROM AAAERC7
                );

PRINT(N'Колическтво записей - ' + CONVERT(varchar(10), @countAAA));
--------------------------------------------------------------------------
SET NOCOUNT OFF;
GO
--------------------------------------------------------------------------
EXEC msdb.dbo.sp_notify_operator  
   @profile_name = N'sqlmail',  
   @name = N'SqlMailAdministrator',  
   @subject = N'Данные для ЕИРЦ сформированы',  
   @body = N'Данные для ЕИРЦ сформированы.' ;  
GO
--------------------------------------------------------------------------
