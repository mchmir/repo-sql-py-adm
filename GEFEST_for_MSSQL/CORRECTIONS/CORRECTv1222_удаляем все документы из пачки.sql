SET ANSI_WARNINGS OFF;
GO

DECLARE @idContract As INT
DECLARE @idDocument As INT    
DECLARE @idPeriod AS INT
DECLARE @Year AS INT
DECLARE @Month AS INT
DECLARE @idAutoDocumentBatch AS INT

DECLARE @idBatch AS INT

SET @Year    = 2022
SET @Month   = 12

SET @idBatch = 94123

SELECT d.IDDocument, c.Account, d.IDBatch, d.DocumentAmount, d.DocumentDate
  FROM Document AS d
       JOIN Contract AS c ON d.IDContract = c.IDContract 
 WHERE d.IDBatch = @idBatch;

SET @idPeriod = (SELECT p.idPeriod FROM  Period p WHERE p.Year = @Year AND p.MONTH = @Month)

DECLARE cursIDDoc CURSOR LOCAL FAST_FORWARD FOR 
  SELECT d.IDDocument, d.IDContract FROM Document d WHERE d.IDBatch = @idBatch AND d.IDPeriod = @idPeriod

-- открываем курсор
OPEN cursIDDoc

FETCH NEXT FROM cursIDDoc INTO @idDocument, @idContract

WHILE @@FETCH_STATUS = 0
BEGIN
     --на каждую итерацию цикла запускаем скрипты с нужными параметрами
    delete from operation where iddocument=@idDocument
    delete from pd where iddocument=@idDocument
    delete from document where iddocument=@idDocument
    exec dbo.spRecalcBalances @idContract,  @idPeriod
    --exec dbo.spRecalcBalancesRealOnePeriodByContract @IDContract, @IDPeriod
    
    --считываем следующую строку курсора
    FETCH NEXT FROM cursIDDoc INTO @idDocument, @idContract
END

CLOSE cursIDDoc
DEALLOCATE cursIDDoc

SET @idAutoDocumentBatch = (SELECT ab.IDAutoBatch FROM AutoBatch ab WHERE ab.IDBatch = @idBatch)
PRINT('@idAutoDocumentBatch = '+CAST(@idAutoDocumentBatch AS VARCHAR))

DELETE FROM AutoDocument WHERE IDAutoBatch = @idAutoDocumentBatch
DELETE FROM AutoBatch WHERE IDBatch = @idBatch

SET ANSI_WARNINGS ON;
GO