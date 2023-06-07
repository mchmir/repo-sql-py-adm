
if exists (select * from tempdb.sys.tables where name LIKE '#tmpAAA%') drop table #tmpAAA


TRUNCATE TABLE AAAERC

DECLARE @maxInt int 
SET @maxInt = 0

-- обобщенное табличное выражение
WITH TestCTE (num, idGRU, idPeriod, idContract) AS
(
 SELECT ROW_NUMBER() OVER (ORDER BY g.IDGru, c.IDContract) num, g.IDGru, 167, c.IDContract  
 FROM GRu g  
 left JOIN GObject gt   ON gt.IDGru = g.IDGru
 left JOIN dbo.Contract c  ON gt.IDContract = c.IDContract
 INNER JOIN Person p WITH (NOLOCK) ON p.IDPerson = c.IDPerson AND p.isJuridical = 0
 WHERE g.IDGru  NOT IN (8518,8743,8626,8666,8742,8738,8772,8771,8768,8747,8828,8781)
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

  SET @idperiod = 167
  SET @i = 1
 

  while @i<= @maxInt --1000
     begin
       SET @idGRU =  (SELECT idGRU FROM #tmpAAA a WHERE a.num = @i)
       SET @idContract =  (SELECT a.idContract FROM #tmpAAA a WHERE a.num = @i)
       --SET @query = 'INSERT INTO dbo.AAAERC_p1  EXEC repCountNoticeERCv2 '+CAST(@idGRU AS VARCHAR) +','+CAST(@idperiod AS VARCHAR)+',' + CAST(@idContract AS VARCHAR)
       SET @query = 'INSERT INTO dbo.AAAERC  EXEC repCountNoticeERC '+CAST(@idGRU AS VARCHAR) +','+CAST(@idperiod AS VARCHAR)+',' + CAST(@idContract AS VARCHAR)
       EXEC (@query)
       set @i=@i+1
     end

DROP TABLE #tmpAAA


  /*DECLARE @query NVARCHAR(MAX)
  
  DELETE FROM AAAERC

DECLARE cur CURSOR FOR
SELECT [query] FROM dbo.AAAERC_query aq

OPEN cur

FETCH NEXT FROM cur INTO @query;
WHILE @@FETCH_STATUS = 0
BEGIN   
 EXEC(@query)
FETCH NEXT FROM cur INTO @query;
END

CLOSE cur;
DEALLOCATE cur;*/