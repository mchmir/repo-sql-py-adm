BEGIN TRY

--Начало транзакции 
BEGIN TRANSACTION 
--Пакет №1 Начало 


  DECLARE @Account INT;
  DECLARE @IDPeriod INT;
  DECLARE @IDBalance INT;
  DECLARE @IDContract INT;
  DECLARE @DocumentDate DATETIME;
  DECLARE @AmountOperation FLOAT;
  DECLARE @IDDocument INT;
  DECLARE @IDTypeDocument INT;
  DECLARE @Year AS INT;
  DECLARE @Month AS INT;


  SET @Account = 2103046;
  SET @Year    = 2023;
  SET @Month   = 12;
  --- ID не проведенного документа ---
  SET @IDDocument = 23889494;
  
  SET @idPeriod = dbo.fGetIDPeriodMY(@Month, @Year);
  
  SET @IDTypeDocument = 1;
  
  SET @IDContract = (SELECT c.IDContract FROM Contract AS c WHERE c.Account = @Account);

  exec dbo.spRecalcBalances @IDContract, @IDPeriod;
  exec dbo.spRecalcBalancesRealOnePeriodByContract @IDContract, @IDPeriod;
  
  
  SET @DocumentDate    = (SELECT d.DocumentDate 
                            FROM Document as d 
                           WHERE d.IDContract     = @IDContract
                             AND d.IDDOCUMENT     = @IDDocument
                             AND d.IDPeriod       = @IDPeriod
                             AND d.IDTypeDocument = @IDTypeDocument);
  
  --SELECT @DocumentDate
  
  SET @AmountOperation = (SELECT d.DocumentAmount
                            FROM Document AS d 
                           WHERE d.IDContract    = @IDContract
                             AND d.IDDOCUMENT     = @IDDocument
                             AND d.IDPeriod       = @IDPeriod
                             AND d.IDTypeDocument = @IDTypeDocument);
  
  --SELECT @AmountOperation
  
  SET @IDBalance       = (SELECT b.IDBalance 
                            FROM Balance as b 
                           WHERE b.IDContract   = @IDContract
                             AND b.IDPeriod     = @IDPeriod 
                             AND b.IDAccounting = 1);
  
  --SELECT @IDBalance

  INSERT INTO Operation (DateOperation, AmountOperation, NumberOperation, IDBalance,  IDDocument, IdTypeOperation)
                 VALUES (@DocumentDate, @AmountOperation, 0,              @IDBalance, @IDDocument, 1);
  
  SELECT * FROM Operation as o
    WHERE o.IDDocument IN (SELECT d.idDocument
                             FROM Document as d
                            WHERE d.IDContract = @IDContract
                              AND d.IDPeriod   = @IDPeriod);

  -- Перепроводка остатков
  exec dbo.spRecalcBalances @IDContract, @IDPeriod;

--Пакет №1 Конец
END TRY 
                  BEGIN CATCH
                  --В случае непредвиденной ошибки 
                  --Откат транзакции 
                  ROLLBACK TRANSACTION 
  
                  --Выводим сообщение об ошибке 
                      SELECT ERROR_NUMBER() AS [Номер ошибки], 
                             ERROR_MESSAGE() AS [Описание ошибки]; 

                  --Прекращаем выполнение инструкции 
                      RETURN; 
                  END CATCH --Если все хорошо. Сохраняем все изменения 
COMMIT TRANSACTION
