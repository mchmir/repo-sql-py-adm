﻿DECLARE @idContract As INT
DECLARE @idDocument As INT    
DECLARE @idPeriod AS INT
DECLARE @Year AS INT
DECLARE @Month AS INT
DECLARE @idAutoDocumentBatch AS INT

DECLARE @idBatch AS INT

SET @Year = 2021
SET @Month = 9
SET @idBatch = 91195


SET @idPeriod = (SELECT p.idPeriod FROM  Period p WHERE p.Year = @Year AND p.MONTH = @Month)

DECLARE cursIDDoc CURSOR LOCAL FAST_FORWARD FOR 
  SELECT d.IDDocument, d.IDContract FROM Document d WHERE d.IDBatch = @idBatch

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