
if exists (select * from tempdb.sys.tables where name LIKE '#tmpAAA%') drop table #tmpAAA
GO

TRUNCATE TABLE dbo.AAAERC;
--ALTER TABLE dbo.AAAERC REBUILD;
GO

DELETE FROM AAAERC
GO


DECLARE @maxInt int 
SET @maxInt = 0

-- обобщенное табличное выражение
WITH TestCTE (num, idGRU, idPeriod, idContract) AS
(
 SELECT ROW_NUMBER() OVER (ORDER BY g.IDGru, c.IDContract) num, g.IDGru, 172, c.IDContract  
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

  SET @idperiod = 172
  SET @i = 1
 

  while @i<= @maxInt --1000
     begin
       SET @idGRU =  (SELECT idGRU FROM #tmpAAA a WHERE a.num = @i)
       SET @idContract =  (SELECT a.idContract FROM #tmpAAA a WHERE a.num = @i)
      -- PRINT ('IDContract ='+ CAST(@idContract AS VARCHAR))

       SET @query = 'INSERT INTO dbo.AAAERC  EXEC repCountNoticeERC '+CAST(@idGRU AS VARCHAR) +','+CAST(@idperiod AS VARCHAR)+',' + CAST(@idContract AS VARCHAR)
       EXEC (@query)
       set @i=@i+1
     end

DROP TABLE #tmpAAA

GO

--58 765 всего записей (на 1 марта 2019), из этих пользователей идет отбор у кого есть счета.  