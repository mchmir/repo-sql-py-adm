DECLARE @Acc AS VARCHAR(50)
DECLARE @idContract As INT
DECLARE @idPeriod AS INT
DECLARE @Year AS INT
DECLARE @Month AS INT
DECLARE @idDocument AS INT


SET @Acc = '1152043'
SET @Year = 2021
SET @Month = 9


SET @idPeriod = (SELECT p.idPeriod FROM  Period p WHERE p.Year = @Year AND p.MONTH = @Month)
SET @idContract = (SELECT c.IDContract FROM Contract c WHERE c.Account = @Acc)

SET @idDocument = 20925663

--SELECT d.IDDocument, * FROM Document d WHERE d.IDBatch = 87256

delete operation OUTPUT DELETED.* where iddocument=@IDDocument
delete pd OUTPUT DELETED.* where iddocument=@IDDocument
delete document OUTPUT DELETED.* where iddocument=@IDDocument

--DELETE operation OUTPUT DELETED.* where iddocument IN (SELECT d.IDDocument FROM Document d WHERE d.IDBatch = 87261)
--delete pd OUTPUT DELETED.* where iddocument IN ( SELECT d.IDDocument FROM Document d WHERE d.IDBatch = 87261)
--delete document OUTPUT DELETED.* where iddocument IN (SELECT d.IDDocument FROM Document d WHERE d.IDBatch = 87261)
--DELETE Batch OUTPUT DELETED.* WHERE IDBatch = 87261
--SELECT * FROM Document d WHERE d.IDPeriod = 181 AND d.IDBatch = 87261


exec dbo.spRecalcBalances @IDContract, @IDPeriod
--exec dbo.spRecalcBalancesRealOnePeriodByContract @IDContract, @IDPeriod
--SELECT * FROM Batch b WHERE b.IDPeriod=194
