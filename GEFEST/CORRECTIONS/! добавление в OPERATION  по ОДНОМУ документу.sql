DECLARE @Account INT
DECLARE @IDPeriod INT
DECLARE @IDBalance INT
DECLARE @IDContract INT 
DECLARE @DocumentDate DATETIME
DECLARE @AmountOperation FLOAT
DECLARE @IDDocument INT
DECLARE @IDTypeDocument INT
DECLARE @IDAccounting INT

DECLARE @Year AS INT
DECLARE @Month AS INT

SET @Account = 2741205

----- какой документ, тип счета: 1 - Основной долг, 4 Пеня, 6 - Услуги
SET @IDAccounting = 1 

SET @Year = 2023
SET @Month = 3

SET @idPeriod = (SELECT p.idPeriod FROM  Period p WHERE p.Year = @Year AND p.MONTH = @Month)


SET @IDTypeDocument = 1

--- ID не проведенного документа ---
SET @IDDocument = 22892661

SET @IDContract = (SELECT c.IDContract FROM Contract c WHERE c.Account = @Account)
exec dbo.spRecalcBalances @IDContract, @IDPeriod
exec dbo.spRecalcBalancesRealOnePeriodByContract @IDContract, @IDPeriod


SET @DocumentDate = (SELECT d.DocumentDate FROM Document d WHERE d.IDContract = @IDContract AND d.IDPeriod = @IDPeriod AND d.IDTypeDocument = @IDTypeDocument AND d.IDDocument = @IDDocument)

--SELECT @DocumentDate

SET @AmountOperation = (SELECT d.DocumentAmount FROM Document d WHERE d.IDContract = @IDContract AND d.IDPeriod = @IDPeriod AND d.IDTypeDocument = @IDTypeDocument AND d.IDDocument = @IDDocument)

--SELECT @AmountOperation

SET @IDBalance = (SELECT b.IDBalance FROM Balance b WHERE b.IDContract = @IDContract AND b.IDPeriod = @IDPeriod AND b.IDAccounting = @IDAccounting)

--SELECT @IDBalance

INSERT INTO Operation (DateOperation, AmountOperation, NumberOperation, IDBalance, IDDocument, IdTypeOperation)
 VALUES (@DocumentDate, @AmountOperation, 0, @IDBalance, @IDDocument, 1);

SELECT * FROM Operation o 
WHERE o.IDDocument IN (SELECT d.idDocument FROM Document d WHERE d.IDContract = @IDContract AND d.IDPeriod = @IDPeriod)

-- Перепроводка остатков
exec dbo.spRecalcBalances @IDContract, @IDPeriod