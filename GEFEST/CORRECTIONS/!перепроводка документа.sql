/*
ИНСТРУКЦИЯ
1. Удаляем операции по документу.
2. exec dbo.spRecalcBalances IDContract, IDPeriod
3. exec dbo.spRecalcBalancesRealOnePeriodByContract IDContract, IDPeriod
4. Проводим документ.
5. exec dbo.spRecalcBalances IDContract, IDPeriod
6. exec dbo.spRecalcBalancesRealOnePeriodByContract IDContract, IDPeriod
*/

/*
delete from operation where iddocument=
*/


declare @IDPERIOD as       INT
declare @IDBATCH as        INT
declare @IDCONTRACT as     INT
declare @IDTYPEDOCUMENT as INT
declare @DOCUMENTDATE as   DATETIME
declare @DOCUMENTAMOUNT as MONEY
declare @IDUSER            INT
declare @IDDOCUMENT        INT
declare @BLERR as          BIT
declare @IDGMETER          INT
declare @IDGOBJECT         INT
declare @IDPERIODPRED as   INT

set @BLERR = 0
--смотрим все балансы для проведения и расчитываем суммы для проведения
set @IDBATCH = 103154
set @IDCONTRACT = 864581
set @IDTYPEDOCUMENT = 1
set @DOCUMENTDATE = '20251106'
set @DOCUMENTAMOUNT = 40000
set @IDUSER = 63
set @IDDOCUMENT = 26427771
set @IDPERIOD = 250 --dbo.fGetNowPeriod()
set @IDPERIODPRED = 249 --dbo.fGetPredPeriod()

declare @IDOPERATION as INT
declare @IDBALANCE as   INT
declare @IDBALANCE2 as  INT
declare @IDBALANCE3 as  INT
declare @IDBALANCE4 as  INT
declare @IDBALANCE6 as  INT
declare @AMOUNT1        FLOAT
declare @AMOUNT2        FLOAT
declare @AMOUNT3        FLOAT
declare @AMOUNT4        FLOAT
declare @AMOUNT6        FLOAT
declare @BALANCE2       FLOAT
declare @BALANCE3       FLOAT
declare @BALANCE4       FLOAT
declare @BALANCE6       FLOAT
--declare @IDPeriodPred as int
declare @NEEDRECALC BIT

select @NEEDRECALC

--set @IDPeriod =172--dbo.fGetNowPeriod() 
--set @IDPeriodPred =171--dbo.fGetPredPeriod() 
set @AMOUNT1 = @DOCUMENTAMOUNT

delete
from OPERATION
where IDDOCUMENT = @IDDOCUMENT
exec DBO.SPRECALCBALANCES @IDCONTRACT, @IDPERIOD
exec DBO.SPRECALCBALANCESREALONEPERIODBYCONTRACT @IDCONTRACT, @IDPERIOD

select top 1 @IDBALANCE4 = IDBALANCE
from BALANCE with (nolock)
where IDPERIOD = @IDPERIOD
  and @IDCONTRACT = IDCONTRACT
  and IDACCOUNTING = 4
if @IDBALANCE4 is not null
  set @BALANCE4 = DBO.FGETLASTBALANCE(@IDPERIOD, @IDCONTRACT, 4)
else
  if (select top 1 IDBALANCE
      from BALANCE with (nolock)
      where IDPERIOD < @IDPERIOD
        and @IDCONTRACT = IDCONTRACT
        and IDACCOUNTING = 4
      order by IDPERIOD desc) is not null
    begin
      set @BALANCE4 = DBO.FGETLASTBALANCE(@IDPERIODPRED, @IDCONTRACT, 4)
      if @BALANCE4 < 0
        begin
          print ''
            -- insert Balance(IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
            -- values (4,@idperiod,@idcontract,@Balance4,0,0)
            -- set @IDBalance4=scope_identity()
        end
    end
select @BALANCE4 as BALANCE4
if isnull(@BALANCE4, 0) < 0 --если есть гасим Пеню
  begin
    if @AMOUNT1 > (-@BALANCE4)
      set @AMOUNT4 = -@BALANCE4;
    else
      set @AMOUNT4 = @AMOUNT1;

    set @AMOUNT1 = @AMOUNT1 - @AMOUNT4
  end
---------------------------------
select @AMOUNT1 AMOUNT1
select @BALANCE4 BALANCE4

select top 1 @IDBALANCE3 = IDBALANCE
from BALANCE with (nolock)
where IDPERIOD = @IDPERIOD
  and @IDCONTRACT = IDCONTRACT
  and IDACCOUNTING = 3
if @IDBALANCE3 is not null
  set @BALANCE3 = DBO.FGETLASTBALANCE(@IDPERIOD, @IDCONTRACT, 3)
else
  if (select top 1 IDBALANCE
      from BALANCE with (nolock)
      where IDPERIOD < @IDPERIOD
        and @IDCONTRACT = IDCONTRACT
        and IDACCOUNTING = 3
      order by IDPERIOD desc) is not null
    begin
      set @BALANCE3 = DBO.FGETLASTBALANCE(@IDPERIODPRED, @IDCONTRACT, 3)
      if @BALANCE3 < 0
        begin
          print ''
            -- insert Balance(IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
            -- values (3,@idperiod,@idcontract,@Balance3,0,0)
            -- set @IDBalance3=scope_identity()
        end
    end
select @BALANCE3 as BALANCE3
if isnull(@BALANCE3, 0) < 0 --если есть гасим гос.пошлину
  begin
    if @AMOUNT1 > (-@BALANCE3)
      set @AMOUNT3 = -@BALANCE3;
    else
      set @AMOUNT3 = @AMOUNT1;

    set @AMOUNT1 = @AMOUNT1 - @AMOUNT3
  end
-------------------
select @AMOUNT1 AMOUNT1_2

select top 1 @IDBALANCE6 = IDBALANCE
from BALANCE with (nolock)
where IDPERIOD = @IDPERIOD
  and @IDCONTRACT = IDCONTRACT
  and IDACCOUNTING = 6
if @IDBALANCE6 is not null
  set @BALANCE6 = DBO.FGETLASTBALANCE(@IDPERIOD, @IDCONTRACT, 6)
else
  if (select top 1 IDBALANCE
      from BALANCE with (nolock)
      where IDPERIOD < @IDPERIOD
        and @IDCONTRACT = IDCONTRACT
        and IDACCOUNTING = 6
      order by IDPERIOD desc) is not null
    begin
      set @BALANCE6 = DBO.FGETLASTBALANCE(@IDPERIODPRED, @IDCONTRACT, 6)
      if @BALANCE6 < 0
        begin
          print ''
            -- insert Balance(IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
            -- values (6,@idperiod,@idcontract,@Balance6,0,0)
            -- set @IDBalance6=scope_identity()
        end
    end
select @BALANCE6 as BALANCE6
select @IDBALANCE6 as IDBALANCE6
--if isnull(@Balance6,0)<0  --если есть гасим техническое обслуживание
--begin 
if @AMOUNT1 > (-@BALANCE6)
  set @AMOUNT6 = -@BALANCE6;
else
  set @AMOUNT6 = @AMOUNT1;

set @AMOUNT1 = @AMOUNT1 - @AMOUNT6
--end
select @AMOUNT6 as AMOUNT6

--------------
select @AMOUNT1 AMOUNT1_3

select top 1 @IDBALANCE2 = IDBALANCE
from BALANCE with (nolock)
where IDPERIOD = @IDPERIOD
  and @IDCONTRACT = IDCONTRACT
  and IDACCOUNTING = 2
if @IDBALANCE2 is not null
  set @BALANCE2 = DBO.FGETLASTBALANCE(@IDPERIOD, @IDCONTRACT, 2)
else
  if (select top 1 IDBALANCE
      from BALANCE with (nolock)
      where IDPERIOD < @IDPERIOD
        and @IDCONTRACT = IDCONTRACT
        and IDACCOUNTING = 2
      order by IDPERIOD desc) is not null
    begin
      set @BALANCE2 = DBO.FGETLASTBALANCE(@IDPERIODPRED, @IDCONTRACT, 2)
      if @BALANCE2 < 0
        begin
          print ''
            -- insert Balance(IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
            -- values (2,@idperiod,@idcontract,@Balance2,0,0)
            -- set @IDBalance2=scope_identity()
        end
    end
if isnull(@BALANCE2, 0) < 0 --если есть гасим иск
  begin
    if @AMOUNT1 > (-@BALANCE2)
      set @AMOUNT2 = -@BALANCE2;
    else
      set @AMOUNT2 = @AMOUNT1;

    set @AMOUNT1 = @AMOUNT1 - @AMOUNT2
  end

----------------------
select @AMOUNT1 AMOUNT1_4

if isnull(@AMOUNT1, 0) <> 0 --все что осталось на гашение основного долга
  begin
    select top 1 @IDBALANCE = IDBALANCE
    from BALANCE with (nolock)
    where IDPERIOD = @IDPERIOD
      and @IDCONTRACT = IDCONTRACT
      and IDACCOUNTING = 1

    if isnull(@IDBALANCE, 0) = 0
      begin
        insert BALANCE(IDACCOUNTING, IDPERIOD, IDCONTRACT, AMOUNTBALANCE, AMOUNTCHARGE, AMOUNTPAY)
        select top 1 IDACCOUNTING, @IDPERIOD, IDCONTRACT, AMOUNTBALANCE, 0, 0
        from BALANCE with (nolock)
        where IDPERIOD < @IDPERIOD
          and IDCONTRACT = @IDCONTRACT
          and IDACCOUNTING = 1
        order by IDPERIOD desc
        set @IDBALANCE = scope_identity()

        if isnull(@IDBALANCE, 0) = 0
          begin
            print ''
              -- insert Balance(IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
              -- values (1, @IDPeriod, @IDContract, 0, 0, 0)
              -- set @IdBalance=scope_identity()
          end
      end
  end
---------------------------
select @AMOUNT1 AMOUNT1_5

begin transaction
-- сохраняем документ

if isnull(@AMOUNT1, 0) <> 0
  begin
    insert into OPERATION(DATEOPERATION, AMOUNTOPERATION, NUMBEROPERATION, IDBALANCE, IDDOCUMENT, IDTYPEOPERATION)
    values (@DOCUMENTDATE, @AMOUNT1, 0, @IDBALANCE, @IDDOCUMENT, 1)
    if @@error <> 0 set @BLERR = 1

    if @NEEDRECALC = 0
      begin
        update BALANCE
        set AMOUNTBALANCE=AMOUNTBALANCE + @AMOUNT1,
            AMOUNTPAY=AMOUNTPAY + @AMOUNT1
        where IDCONTRACT = @IDCONTRACT
          and IDPERIOD = @IDPERIOD
          and IDACCOUNTING = 1
        if @@error <> 0 set @BLERR = 1
      end
  end

if isnull(@AMOUNT2, 0) > 0
  begin
    insert into OPERATION(DATEOPERATION, AMOUNTOPERATION, NUMBEROPERATION, IDBALANCE, IDDOCUMENT, IDTYPEOPERATION)
    values (@DOCUMENTDATE, @AMOUNT2, 2, @IDBALANCE2, @IDDOCUMENT, 1)
    if @@error <> 0 set @BLERR = 1

    if @NEEDRECALC = 0
      begin
        update BALANCE
        set AMOUNTBALANCE=AMOUNTBALANCE + @AMOUNT2,
            AMOUNTPAY=AMOUNTPAY + @AMOUNT2
        where IDBALANCE = @IDBALANCE2
        if @@error <> 0 set @BLERR = 1
      end
  end
if isnull(@AMOUNT6, 0) > 0
  begin
    insert into OPERATION(DATEOPERATION, AMOUNTOPERATION, NUMBEROPERATION, IDBALANCE, IDDOCUMENT, IDTYPEOPERATION)
    values (@DOCUMENTDATE, @AMOUNT6, 6, @IDBALANCE6, @IDDOCUMENT, 1)
    if @@error <> 0 set @BLERR = 1


    if @NEEDRECALC = 0
      begin
        update BALANCE
        set AMOUNTBALANCE=AMOUNTBALANCE + @AMOUNT6,
            AMOUNTPAY=AMOUNTPAY + @AMOUNT6
        where IDBALANCE = @IDBALANCE6
        if @@error <> 0 set @BLERR = 1

      end
  end

if isnull(@AMOUNT3, 0) > 0
  begin
    insert into OPERATION(DATEOPERATION, AMOUNTOPERATION, NUMBEROPERATION, IDBALANCE, IDDOCUMENT, IDTYPEOPERATION)
    values (@DOCUMENTDATE, @AMOUNT3, 3, @IDBALANCE3, @IDDOCUMENT, 1)
    if @@error <> 0 set @BLERR = 1


    if @NEEDRECALC = 0
      begin
        update BALANCE
        set AMOUNTBALANCE=AMOUNTBALANCE + @AMOUNT3,
            AMOUNTPAY=AMOUNTPAY + @AMOUNT3
        where IDBALANCE = @IDBALANCE3
        if @@error <> 0 set @BLERR = 1

      end
  end

if isnull(@AMOUNT4, 0) > 0
  begin
    insert into OPERATION(DATEOPERATION, AMOUNTOPERATION, NUMBEROPERATION, IDBALANCE, IDDOCUMENT, IDTYPEOPERATION)
    values (@DOCUMENTDATE, @AMOUNT4, 4, @IDBALANCE4, @IDDOCUMENT, 1)
    if @@error <> 0 set @BLERR = 1

    if @NEEDRECALC = 0
      begin
        update BALANCE
        set AMOUNTBALANCE=AMOUNTBALANCE + @AMOUNT4,
            AMOUNTPAY=AMOUNTPAY + @AMOUNT4
        where IDBALANCE = @IDBALANCE4
        if @@error <> 0 set @BLERR = 1
      end
  end

if @BLERR = 0
  commit tran
else
  rollback tran


--exec dbo.spRecalcBalances @IDContract, @IDPeriod
--exec dbo.spRecalcBalancesRealOnePeriodByContract @IDContract, @IDPeriod

select *
from OPERATION O
where O.IDDOCUMENT = @IDDOCUMENT

