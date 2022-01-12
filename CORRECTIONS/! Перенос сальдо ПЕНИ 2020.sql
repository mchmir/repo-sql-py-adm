---- 30 марта 2020 года.
---- Создание документа корректировки начисления Пени
---- Корректировка создается на всю сумму начисленной пени
---- Списываем Пеню всем у кого была.

DECLARE @DocumentAmount FLOAT
SET @DocumentAmount = 0
DECLARE @IDPeriod INT
SET @IDPeriod = 182
DECLARE @IDTypeDocument INT
SET @IDTypeDocument = 7 
DECLARE @IDUser INT
SET @IDUser = 51
           -----exec sp_helpuser  51 Mchmir
DECLARE @Note VARCHAR(8000)
SET @Note = 'Списание пени по распоряжению директора, в связи ЧП.'
DECLARE @Date DateTime
SET @Date='31.03.2020'


SELECT 
  b.IDBalance, 
  b.IDAccounting,
  b.IDPeriod,
  b.IDContract, 
  c.Account AS [Лицевой счет],
  b.AmountBalance AS DocumentAmount, 
  b.AmountCharge ASAmountCharge, 
  b.AmountPay AS AmountPay, 
  a.Name AS [Счет],
  ISNULL(p.Surname,'') + ' ' + ISNULL(p.Name,'') +' ' + ISNULL(p.Patronic,'') AS [ФИО]
INTO #tmpPenyaMart   
FROM Contract c, Balance b, Accounting a, Person p WHERE b.IDContract=c.IDContract AND p.IDPerson = c.IDPerson AND  b.IDPeriod = 182 AND  b.IDAccounting = 4 AND 
  a.IDAccounting = b.IDAccounting 
  AND b.AmountBalance <= -0.01
  --AND c.Account IN (3652017,1403106) --(0491019,0511012)  -- 3911038

DECLARE curPeny CURSOR
READ_ONLY
FOR select idcontract, idperiod, IDBalance, DocumentAmount  from  #tmpPenyaMart

  declare @IdBalance int
  DECLARE @idcontract INT
  declare @iddocument int 
  declare @Idoperation int

OPEN curPeny

FETCH NEXT FROM curPeny INTO @idcontract, @idperiod, @IDBalance, @DocumentAmount
WHILE (@@fetch_status <> -1)
BEGIN
	IF (@@fetch_status <> -2)
	BEGIN
    SET @DocumentAmount = @DocumentAmount * (-1)
        -- создаём документ корректировка начисления
        insert document (IDContract, IDPeriod, IDBatch, IDTypeDocument, DocumentNumber, DocumentDate, DocumentAmount, Note, DateAdd, IdUser)
		    values (@idcontract, @IdPeriod,NULL, @IDTypeDocument, '', @Date, @DocumentAmount, @Note, @Date, @IDUser)
		    set @iddocument=scope_identity()
        -- добавляем данные в операции
        insert operation(DateOperation, AmountOperation, NumberOperation, IDBalance, IDDocument, IdTypeOperation)
	     	values (@Date, @DocumentAmount, '', @IdBalance, @iddocument, 3)
		    set @idoperation=scope_identity()  -- это системная функция, которая возвращает последнее значение идентификатора, вставленного в любую таблицу в текущем сеансе в той же области
        
        -- добавляем данные в таблицу PD
        INSERT PD (IDTypePD, IDDocument, Value) VALUES (13, @iddocument, '0');
        INSERT PD (IDTypePD, IDDocument, Value) VALUES (16, @iddocument, '0');
        INSERT PD (IDTypePD, IDDocument, Value) VALUES (34, @iddocument, '1');
        INSERT PD (IDTypePD, IDDocument, Value) VALUES (35, @iddocument, '0');
           
        -- пересчитываем баланс
	      exec dbo.spRecalcBalances @IdContract, @IdPeriod

	END
	FETCH NEXT FROM curPeny INTO @idcontract, @idperiod, @IDBalance, @DocumentAmount
END

CLOSE curPeny
DEALLOCATE curPeny    
  

-- данные кому списали пеню, для истории 
SELECT * FROM #tmpPenyaMart pm
DROP TABLE #tmpPenyaMart       



