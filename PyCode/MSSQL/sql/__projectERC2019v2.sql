
if exists (select * from tempdb.sys.tables where name LIKE '#tmpAAA%') drop table #tmpAAA
GO

if exists (select * from tempdb.sys.tables where name LIKE '#tmpResult%') drop table #tmpResult
GO

DROP TABLE AAAERC2
GO

CREATE TABLE Gefest.dbo.#tmpResult (
  Account VARCHAR(50) NOT NULL
 ,Year INT NOT NULL
 ,Month INT NOT NULL
 ,fio VARCHAR(500) NULL
 ,PersUstr VARCHAR(250) NULL
 ,SaldoNachGaz FLOAT NULL
 ,SaldoNachPenya FLOAT NULL
 ,SaldoNachSudVzysk FLOAT NULL
 ,SaldoNachSudIzder FLOAT NULL
 ,SaldoNachSHtraf FLOAT NULL
 ,SaldoNachDopUslug FLOAT NULL
 ,SaldoNachItog FLOAT NULL
 ,OplachenoGaz FLOAT NULL
 ,OplachenoPenya FLOAT NULL
 ,OplachenoSudVzysk FLOAT NULL
 ,OplachenoSudIzder FLOAT NULL
 ,OplachenoSHtraf FLOAT NULL
 ,OplachenoDopUslug FLOAT NULL
 ,OplachenoItog FLOAT NULL
 ,PredPokaz FLOAT NULL
 ,TekPokaz FLOAT NULL
 ,Cena FLOAT NULL
 ,Kolvo VARCHAR(8000) NULL
 ,SummaNach FLOAT NULL
 ,SummNachislGaz FLOAT NULL
 ,SummNachislPenya FLOAT NULL
 ,SummNachislVzysk FLOAT NULL
 ,SummNachislIzder FLOAT NULL
 ,SummNachislSHtraf FLOAT NULL
 ,SummNachislDopUslug FLOAT NULL
 ,SummNachislItog FLOAT NULL
 ,SaldoKonecGaz FLOAT NULL
 ,SaldoKonecPenya FLOAT NULL
 ,SaldoKonecVzysk FLOAT NULL
 ,SaldoKonecIzder FLOAT NULL
 ,SaldoKonecSHtraf FLOAT NULL
 ,SaldoKonecDopUslug FLOAT NULL
 ,SaldoKonecItog FLOAT NULL
 ,Primechanie VARCHAR(250) NULL
 ,Street VARCHAR(50) NULL
 ,HouseNumber VARCHAR(10) NULL
 ,HouseNumberChar VARCHAR(20) NULL
 ,Flat VARCHAR(20) NULL
 ,CONSTRAINT PK_AAAERC PRIMARY KEY CLUSTERED (Account, Year, Month)
) ON [PRIMARY]
GO


TRUNCATE TABLE dbo.AAAERC
GO

DELETE FROM AAAERC
GO


DECLARE @maxInt int 
SET @maxInt = 0

-- обобщенное табличное выражение
WITH TestCTE (num, idGRU, idPeriod, idContract) AS
(
 SELECT ROW_NUMBER() OVER (ORDER BY g.IDGru, c.IDContract) num, g.IDGru, 169, c.IDContract  
 FROM GRu g  
 left JOIN GObject gt   ON gt.IDGru = g.IDGru
 left JOIN dbo.Contract c  ON gt.IDContract = c.IDContract
 INNER JOIN Person p WITH (NOLOCK) ON p.IDPerson = c.IDPerson AND p.isJuridical = 0
 WHERE g.IDGru  NOT IN (8518,8743,8626,8666,8742,8738,8772,8771,8768,8747,8828,8781) AND c.IDContract NOT IN (885122,885123,885124,885125,885126,885127,885128,885129)
)
SELECT * INTO #tmpAAA FROM TestCTE
--Запрос, в котором мы можем использовать CTE
 SET @maxInt = (SELECT MAX(a.num) FROM #tmpAAA a)

PRINT ('Количество записей ='+ CAST(@maxInt AS VARCHAR))

DECLARE @i INT
DECLARE @idGRU INT
DECLARE @idperiod INT
DECLARE @idContract INT

DECLARE @query NVARCHAR(MAX)

  SET @idperiod = 169
  SET @i = 1
 

  while @i<= 1000--@maxInt --1000
     BEGIN
       SET @idGRU =  (SELECT idGRU FROM #tmpAAA a WHERE a.num = @i)
       SET @idContract =  (SELECT a.idContract FROM #tmpAAA a WHERE a.num = @i)
      -- PRINT ('IDContract ='+ CAST(@idContract AS VARCHAR))

       INSERT INTO #tmpResult  EXEC repCountNoticeERC @idGRU, @idperiod,@idContract
       --EXEC (@query)
       set @i=@i+1
     END


SELECT * INTO dbo.AAAERC2 FROM #tmpResult


DROP TABLE #tmpAAA
DROP TABLE #tmpResult

GO

--58 765 всего записей (на 1 марта 2019), из этих пользователей идет отбор у кого есть счета.  