begin try

  --Начало транзакции
  begin transaction
    --Пакет №1 Начало


    declare @ACCOUNT INT;
    declare @IDPERIOD INT;
    declare @IDBALANCE INT;
    declare @IDCONTRACT INT;
    declare @DOCUMENTDATE DATETIME;
    declare @AMOUNTOPERATION FLOAT;
    declare @IDDOCUMENT INT;
    declare @IDTYPEDOCUMENT INT;
    declare @YEAR as INT;
    declare @MONTH as INT;


    set @ACCOUNT = 1231049;
    set @YEAR = 2024;
    set @MONTH = 8;
    --- ID не проведенного документа ---
    set @IDDOCUMENT = 24779479;

    set @IDPERIOD = DBO.FGETIDPERIODMY(@MONTH, @YEAR);

    set @IDTYPEDOCUMENT = 1;

    set @IDCONTRACT = (select C.IDCONTRACT from CONTRACT as C where C.ACCOUNT = @ACCOUNT);

    exec DBO.SPRECALCBALANCES @IDCONTRACT, @IDPERIOD;
    exec DBO.SPRECALCBALANCESREALONEPERIODBYCONTRACT @IDCONTRACT, @IDPERIOD;


    set @DOCUMENTDATE = (select D.DOCUMENTDATE
                         from DOCUMENT as D
                         where D.IDCONTRACT = @IDCONTRACT
                           and D.IDDOCUMENT = @IDDOCUMENT
                           and D.IDPERIOD = @IDPERIOD
                           and D.IDTYPEDOCUMENT = @IDTYPEDOCUMENT);

    --SELECT @DocumentDate

    set @AMOUNTOPERATION = (select D.DOCUMENTAMOUNT
                            from DOCUMENT as D
                            where D.IDCONTRACT = @IDCONTRACT
                              and D.IDDOCUMENT = @IDDOCUMENT
                              and D.IDPERIOD = @IDPERIOD
                              and D.IDTYPEDOCUMENT = @IDTYPEDOCUMENT);

    --SELECT @AmountOperation

    set @IDBALANCE = (select B.IDBALANCE
                      from BALANCE as B
                      where B.IDCONTRACT = @IDCONTRACT
                        and B.IDPERIOD = @IDPERIOD
                        and B.IDACCOUNTING = 1);

    --SELECT @IDBalance

    insert into OPERATION (DATEOPERATION, AMOUNTOPERATION, NUMBEROPERATION, IDBALANCE, IDDOCUMENT, IDTYPEOPERATION)
    values (@DOCUMENTDATE, @AMOUNTOPERATION, 0, @IDBALANCE, @IDDOCUMENT, 1);

    select *
    from OPERATION as O
    where O.IDDOCUMENT in (select D.IDDOCUMENT
                           from DOCUMENT as D
                           where D.IDCONTRACT = @IDCONTRACT
                             and D.IDPERIOD = @IDPERIOD);

    -- Перепроводка остатков
    exec DBO.SPRECALCBALANCES @IDCONTRACT, @IDPERIOD;

--Пакет №1 Конец
end try
begin catch
  --В случае непредвиденной ошибки
  --Откат транзакции
  rollback transaction

  --Выводим сообщение об ошибке
  select ERROR_NUMBER()  as [Номер ошибки],
         ERROR_MESSAGE() as [Описание ошибки];

  --Прекращаем выполнение инструкции
  return;
end catch --Если все хорошо. Сохраняем все изменения
commit transaction
