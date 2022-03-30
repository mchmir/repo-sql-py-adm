--- перепроведение документа НОТАРИУС ---------------
--- Изменено 18.04.2019   ---------------------------

declare @IDPeriod as int
declare @IDContract as INT
--DECLARE @Account AS VARCHAR(50)
DECLARE @Account AS INT
DECLARE @Amount AS FLOAT
DECLARE @IDDocument AS int


SET @Account = 1882041
SET @IDPeriod = 195
SET @Amount = 15581.4


SET @IDContract = (SELECT c.IDContract FROM Contract c WHERE c.Account = @Account)

--SELECT @Account

SET @IDDocument = (SELECT d.IDDocument FROM Document d WHERE d.IDContract = @IDContract AND d.IDTypeDocument = 13 AND d.IDPeriod = @IDPeriod AND ROUND(d.DocumentAmount,2) = @Amount) --ORDER BY d.IDDocument DESC)

SELECT @IDDocument

--2-- Операции по нужному документу       
--SELECT * FROM Operation o WHERE o.IDDocument  = 17811301
--3-- Меняем руками сумму -ххх на -2975 где IdTypeOperation = 2(начисление)

--4-- Меняем значение для IDTypePD = 29 {2975}, для IDTypePD = 37 {0} потомучто в процедуре repAccount(карточка абонента), начисление заложено по фрмуле Amount/100* p37 + p29  
--SELECT * FROM pd WHERE IDDocument = 17811301

UPDATE Operation SET AmountOperation = -3517
       OUTPUT INSERTED.IDOperation AS [idOperation],
              DELETED.AmountOperation AS [OldAmountOperation],
              INSERTED.AmountOperation AS [NewAmountOperation]
  WHERE IDDocument = @IDDocument AND IdTypeOperation = 2
                   
UPDATE PD  SET Value = 3517
       OUTPUT INSERTED.IDPD AS [idPD],
              DELETED.Value AS [OldValue],
              INSERTED.Value AS [NewValue]
  WHERE IDDocument = @IDDocument AND IDTypePD = 29

UPDATE PD  SET Value = 0 
       OUTPUT INSERTED.IDPD AS [idPD],
              DELETED.Value AS [OldValue],
              INSERTED.Value AS [NewValue]
  WHERE IDDocument = @IDDocument AND IDTypePD = 37

--5-- Пересчет сальдо по контракту
       
exec dbo.spRecalcBalances @IDContract, @IDPeriod
--exec dbo.spRecalcBalancesRealOnePeriodByContract @IDContract, @IDPeriod
