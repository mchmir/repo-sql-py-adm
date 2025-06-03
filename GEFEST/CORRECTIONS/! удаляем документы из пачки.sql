DECLARE @idContract As INT
DECLARE @idDocument As INT    
DECLARE @idPeriod AS INT
DECLARE @Year AS INT
DECLARE @Month AS INT
DECLARE @idAutoDocumentBatch AS INT

DECLARE @deletedCount INT
DECLARE @lastPrintTime DATETIME

SET @deletedCount = 0
SET @lastPrintTime = GETDATE()

DECLARE @idBatch AS INT

SET @Year = 2025
SET @Month = 4
SET @idBatch = 99073


SET @idPeriod = (SELECT p.idPeriod FROM  Period p WHERE p.Year = @Year AND p.MONTH = @Month)

DECLARE cursIDDoc CURSOR LOCAL FAST_FORWARD FOR 
  SELECT d.IDDocument, d.IDContract FROM Document d WHERE d.IDBatch = @idBatch

-- открываем курсор
OPEN cursIDDoc

FETCH NEXT FROM cursIDDoc INTO @idDocument, @idContract

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Вложенный курсор по операциям
    DECLARE @opID INT
    DECLARE cursOps CURSOR LOCAL FAST_FORWARD FOR
        SELECT IDOperation FROM OPERATION WHERE IDDocument = @idDocument

    OPEN cursOps
    FETCH NEXT FROM cursOps INTO @opID

    WHILE @@FETCH_STATUS = 0
    BEGIN
        DELETE FROM FACTUSE WHERE IDOPERATION = @opID
        FETCH NEXT FROM cursOps INTO @opID
    END

    CLOSE cursOps
    DEALLOCATE cursOps

    -- Удаление документа и связанного
    DELETE FROM OPERATION WHERE IDDocument = @idDocument
    DELETE FROM PD WHERE IDDocument = @idDocument
    DELETE FROM DOCUMENT WHERE IDDocument = @idDocument

    -- Увеличиваем счётчик
    SET @deletedCount = @deletedCount + 1

    -- Проверка, прошло ли больше 5 секунд с последнего PRINT
    IF DATEDIFF(SECOND, @lastPrintTime, GETDATE()) >= 5
    BEGIN
        PRINT 'Удалено документов: ' + CAST(@deletedCount AS VARCHAR)
        SET @lastPrintTime = GETDATE()
    END

    -- Перерасчёт
    EXEC dbo.spRecalcBalances @idContract, @idPeriod

    -- Следующая строка
    FETCH NEXT FROM cursIDDoc INTO @idDocument, @idContract
END

CLOSE cursIDDoc
DEALLOCATE cursIDDoc

SET @idAutoDocumentBatch = (SELECT ab.IDAutoBatch FROM AutoBatch ab WHERE ab.IDBatch = @idBatch)
PRINT('@idAutoDocumentBatch = '+CAST(@idAutoDocumentBatch AS VARCHAR))

DELETE FROM AutoDocument WHERE IDAutoBatch = @idAutoDocumentBatch
DELETE FROM AutoBatch WHERE IDBatch = @idBatch