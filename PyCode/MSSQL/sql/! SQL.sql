/*
  select type, sum(multi_pages_kb) AS multi_pages_kb
from sys.dm_os_memory_clerks 
where multi_pages_kb <> 0 
group by type

  SELECT @@version

SELECT db_name(dbid), * 
from   sys.sysprocesses

 --select name, database_id from sys.databases 

 --DBCC FLUSHPROCINDB(5) 


sp_configure 'show advanced options', 1
RECONFIGURE
GO
sp_configure 'awe enabled', 1
RECONFIGURE
GO
sp_configure 'max server memory', 6144
RECONFIGURE
GO


 DELETE FROM dbo.tmpAAAERC

 SELECT g.IDGru, 167, c.IDContract  
 INTO dbo.tmpAAAERC 
 FROM GRu g  
 left JOIN GObject gt   ON gt.IDGru = g.IDGru
 left JOIN dbo.Contract c  ON gt.IDContract = c.IDContract
 WHERE g.IDGru  NOT IN (8518,8743,8626,8666,8742,8738,8772,8771,8768,8747,8828,8781)
*/
if exists (select * from tempdb.sys.tables where name LIKE '#tmpAAA%') drop table #tmpAAA

DELETE FROM AAAERC

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

  SET @idperiod = 167
  SET @i = 1

  while @i<1000 -- @maxInt
     begin
       --@RES=(select ParId from #temp where ind=@i) --ВЗЯЛ!!! каждой строки 
       SET @idGRU =  (SELECT idGRU FROM #tmpAAA a WHERE a.num = @i)
       SET @idContract =  (SELECT a.idContract FROM #tmpAAA a WHERE a.num = @i)
       INSERT INTO dbo.AAAERC  EXEC repCountNoticeERC @idGRU, @idperiod,@idContract
       set @i=@i+1
     end

DROP TABLE #tmpAAA
