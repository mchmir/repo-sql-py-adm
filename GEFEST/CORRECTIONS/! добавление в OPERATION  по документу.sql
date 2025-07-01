USE Gefest
GO

DECLARE @Account INT
DECLARE @IDPeriod INT
DECLARE @IDBalance INT
DECLARE @IDContract INT 
DECLARE @DocumentDate DATETIME
DECLARE @AmountOperation FLOAT
DECLARE @IDDocument INT
DECLARE @IDTypeDocument INT


SET @Account = 2361130
SET @IDPeriod = 245

SET @IDTypeDocument = 24

SET @IDContract = (SELECT c.IDContract FROM Contract c WHERE c.Account = @Account)
exec dbo.spRecalcBalances @IDContract, @IDPeriod
exec dbo.spRecalcBalancesRealOnePeriodByContract @IDContract, @IDPeriod


SET @DocumentDate = (SELECT d.DocumentDate FROM Document d WHERE d.IDContract = @IDContract AND d.IDPeriod = @IDPeriod AND d.IDTypeDocument = @IDTypeDocument)

--SELECT @DocumentDate

SET @AmountOperation = (SELECT d.DocumentAmount FROM Document d WHERE d.IDContract = @IDContract AND d.IDPeriod = @IDPeriod AND d.IDTypeDocument = @IDTypeDocument)

--SELECT @AmountOperation

SET @IDBalance = (SELECT b.IDBalance FROM Balance b WHERE b.IDContract = @IDContract AND b.IDPeriod = @IDPeriod AND b.IDAccounting = 1)

--SELECT @IDBalance

SET @IDDocument = (SELECT d.IDDocument FROM Document d WHERE d.IDContract = @IDContract AND d.IDPeriod = @IDPeriod AND d.IDTypeDocument = @IDTypeDocument)

--SELECT @IDDocument

INSERT INTO Operation (DateOperation, AmountOperation, NumberOperation, IDBalance, IDDocument, IdTypeOperation)
 VALUES (@DocumentDate, @AmountOperation, 0, @IDBalance, @IDDocument, 1);

SELECT * FROM Operation o 
WHERE o.IDDocument IN (SELECT d.idDocument FROM Document d WHERE d.IDContract = @IDContract AND d.IDPeriod = @IDPeriod)

-- Перепроводка остатков
exec dbo.spRecalcBalances @IDContract, @IDPeriod