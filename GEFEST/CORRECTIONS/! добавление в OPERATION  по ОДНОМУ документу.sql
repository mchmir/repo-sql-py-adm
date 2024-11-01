declare @ACCOUNT INT
declare @IDPERIOD INT
declare @IDBALANCE INT
declare @IDCONTRACT INT
declare @DOCUMENTDATE DATETIME
declare @AMOUNTOPERATION FLOAT
declare @IDDOCUMENT INT
declare @IDTYPEDOCUMENT INT
declare @IDACCOUNTING INT

declare @YEAR as INT
declare @MONTH as INT

set @ACCOUNT = 1304041

----- какой документ, тип счета: 1 - Основной долг, 4 Пеня, 6 - Услуги
set @IDACCOUNTING = 1

set @YEAR = 2024
set @MONTH = 10

set @IDPERIOD = (select P.IDPERIOD
                 from PERIOD P
                 where P.YEAR = @YEAR
                   and P.MONTH = @MONTH)


set @IDTYPEDOCUMENT = 1

--- ID не проведенного документа ---
set @IDDOCUMENT = 25032134

set @IDCONTRACT = (select C.IDCONTRACT
                   from CONTRACT C
                   where C.ACCOUNT = @ACCOUNT)
exec DBO.SPRECALCBALANCES @IDCONTRACT, @IDPERIOD
exec DBO.SPRECALCBALANCESREALONEPERIODBYCONTRACT @IDCONTRACT, @IDPERIOD


set @DOCUMENTDATE = (select D.DOCUMENTDATE
                     from DOCUMENT D
                     where D.IDCONTRACT = @IDCONTRACT
                       and D.IDPERIOD = @IDPERIOD
                       and D.IDTYPEDOCUMENT = @IDTYPEDOCUMENT
                       and D.IDDOCUMENT = @IDDOCUMENT)

--SELECT @DocumentDate

set @AMOUNTOPERATION = (select D.DOCUMENTAMOUNT
                        from DOCUMENT D
                        where D.IDCONTRACT = @IDCONTRACT
                          and D.IDPERIOD = @IDPERIOD
                          and D.IDTYPEDOCUMENT = @IDTYPEDOCUMENT
                          and D.IDDOCUMENT = @IDDOCUMENT)

--SELECT @AmountOperation

set @IDBALANCE = (select B.IDBALANCE
                  from BALANCE B
                  where B.IDCONTRACT = @IDCONTRACT
                    and B.IDPERIOD = @IDPERIOD
                    and B.IDACCOUNTING = @IDACCOUNTING)

--SELECT @IDBalance

insert into OPERATION (DATEOPERATION, AMOUNTOPERATION, NUMBEROPERATION, IDBALANCE, IDDOCUMENT, IDTYPEOPERATION)
values (@DOCUMENTDATE, @AMOUNTOPERATION, 0, @IDBALANCE, @IDDOCUMENT, 1);

select *
from OPERATION O
where O.IDDOCUMENT in (select D.IDDOCUMENT from DOCUMENT D where D.IDCONTRACT = @IDCONTRACT and D.IDPERIOD = @IDPERIOD)

-- Перепроводка остатков
exec DBO.SPRECALCBALANCES @IDCONTRACT, @IDPERIOD