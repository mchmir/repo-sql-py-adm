/*
»Ќ—“–” ÷»я
1. ”дал€ем операции по документу.
2. exec dbo.spRecalcBalances IDContract, IDPeriod
3. exec dbo.spRecalcBalancesRealOnePeriodByContract IDContract, IDPeriod
4. ѕроводим документ.
5. exec dbo.spRecalcBalances IDContract, IDPeriod
6. exec dbo.spRecalcBalancesRealOnePeriodByContract IDContract, IDPeriod
*/

/*
delete from operation where iddocument=
*/


declare @IDPeriod as int
declare	@IDBatch as int 
declare @IDContract as int 
declare	@IDTypeDocument as int 
declare @DocumentDate as DateTime 
declare @DocumentAmount as money
declare @IDUser int
declare @IDDocument int 
declare	@blErr as bit 
declare @IdGMeter int
declare @IDGObject INT
declare @IDPeriodPred as int

set @blErr = 0
--смотрим все балансы дл€ проведени€ и расчитываем суммы дл€ проведени€
set @IDBatch=86003
set @IDContract=897330
set @IDTypeDocument=1
set @DocumentDate='2019-09-03'
set @DocumentAmount=1
set @IDUser=58
set @IDDocument=18327467
set @IDPeriod =176--dbo.fGetNowPeriod() 
set @IDPeriodPred =175--dbo.fGetPredPeriod() 

declare @IDOperation as int
declare @IDBalance as int
declare @IDBalance2 as int
declare @IDBalance3 as int
declare @IDBalance4 as int
declare @IDBalance6 as int
declare @Amount1 float
declare @Amount2 float
declare @Amount3 float
declare @Amount4 float
declare @Amount6 float
declare @Balance2 float
declare @Balance3 float
declare @Balance4 float
declare @Balance6 float
--declare @IDPeriodPred as int
declare @NeedRecalc bit

select @NeedRecalc

--set @IDPeriod =172--dbo.fGetNowPeriod() 
--set @IDPeriodPred =171--dbo.fGetPredPeriod() 
set @Amount1=@DocumentAmount

delete from operation where iddocument=@IDDocument
exec dbo.spRecalcBalances @IDContract, @IDPeriod
exec dbo.spRecalcBalancesRealOnePeriodByContract @IDContract, @IDPeriod

select top 1 @IDBalance4=IDBalance from balance  with (nolock) 
where IDPeriod = @IDPeriod and @IDContract=IDContract and IDAccounting=4
if 	@IDBalance4 is not null
	set @Balance4=dbo.fGetLastBalance(@IDPeriod, @IDContract, 4)
ELSE
	if (select top 1 IDBalance from balance  with (nolock) 
		where IDPeriod <@IDPeriod and @IDContract=IDContract and IDAccounting=4 order by idperiod desc) is not null
	begin
		set @Balance4=dbo.fGetLastBalance(@IDPeriodPred, @IDContract, 4)
		if @Balance4<0
		begin
			print ''
--			insert Balance(IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
--			values (4,@idperiod,@idcontract,@Balance4,0,0)
--			set @IDBalance4=scope_identity()
		end
	end
select @Balance4 as Balance4
if isnull(@Balance4,0)<0  --если есть гасим ѕеню
begin 
	if @Amount1>(-@Balance4)
		set @Amount4 = -@Balance4;
	else
		set @Amount4 = @Amount1;

	set @Amount1=@Amount1-@Amount4
end
---------------------------------
SELECT @Amount1 Amount1
SELECT @Balance4 Balance4

select top 1 @IDBalance3=IDBalance from balance  with (nolock) 
where IDPeriod = @IDPeriod and @IDContract=IDContract and IDAccounting=3
if 	@IDBalance3 is not null
	set @Balance3=dbo.fGetLastBalance(@IDPeriod, @IDContract, 3)
ELSE
	if (select top 1 IDBalance from balance  with (nolock) 
		where IDPeriod <@IDPeriod and @IDContract=IDContract and IDAccounting=3 order by idperiod desc) is not null
	begin
		set @Balance3=dbo.fGetLastBalance(@IDPeriodPred, @IDContract, 3)
		if @Balance3<0
		begin
			print ''
--			insert Balance(IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
--			values (3,@idperiod,@idcontract,@Balance3,0,0)
--			set @IDBalance3=scope_identity()
		end
	end
select @Balance3 as Balance3
if isnull(@Balance3,0)<0  --если есть гасим гос.пошлину
begin 
	if @Amount1>(-@Balance3)
		set @Amount3 = -@Balance3;
	else
		set @Amount3 = @Amount1;

	set @Amount1=@Amount1-@Amount3
END
-------------------
SELECT @Amount1 Amount1_2

select top 1 @IDBalance6=IDBalance from balance  with (nolock) 
where IDPeriod = @IDPeriod and @IDContract=IDContract and IDAccounting=6
if 	@IDBalance6 is not null
	set @Balance6=dbo.fGetLastBalance(@IDPeriod, @IDContract,6)
ELSE
	if (select top 1 IDBalance from balance  with (nolock) 
		where IDPeriod <@IDPeriod and @IDContract=IDContract and IDAccounting=6 order by idperiod desc) is not null
	begin
		set @Balance6=dbo.fGetLastBalance(@IDPeriodPred, @IDContract, 6)
		if @Balance6<0
		begin
			print ''
--			insert Balance(IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
--			values (6,@idperiod,@idcontract,@Balance6,0,0)
--			set @IDBalance6=scope_identity()
		end
	end
select @Balance6 as Balance6
select @IDBalance6 as IDBalance6
--if isnull(@Balance6,0)<0  --если есть гасим техническое обслуживание
--begin 
	if @Amount1>(-@Balance6)
		set @Amount6 = -@Balance6;
	else
		set @Amount6 = @Amount1;

	set @Amount1=@Amount1-@Amount6
--end
select @Amount6 as Amount6

--------------
  SELECT @Amount1 Amount1_3

select top 1 @IDBalance2=IDBalance from balance  with (nolock) 
where IDPeriod = @IDPeriod and @IDContract=IDContract and IDAccounting=2
if 	@IDBalance2 is not null
	set @Balance2=dbo.fGetLastBalance(@IDPeriod, @IDContract, 2)
ELSE
	if (select top 1 IDBalance from balance  with (nolock) 
		where IDPeriod <@IDPeriod and @IDContract=IDContract and IDAccounting=2 order by idperiod desc) is not null
	begin
		set @Balance2=dbo.fGetLastBalance(@IDPeriodPred, @IDContract, 2)
		if @Balance2<0
		begin
			print ''
--			insert Balance(IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
--			values (2,@idperiod,@idcontract,@Balance2,0,0)
--			set @IDBalance2=scope_identity()
		end
	end
if isnull(@Balance2,0)<0 --если есть гасим иск
begin 
	if @Amount1>(-@Balance2)
		set @Amount2 = -@Balance2;
	else
		set @Amount2 = @Amount1;

	set @Amount1=@Amount1-@Amount2
END

----------------------
  SELECT @Amount1 Amount1_4

if isnull(@Amount1,0)<>0 --все что осталось на гашение основного долга
begin 
	select top 1 @IDBalance=IDBalance from balance with (nolock) 
	where IDPeriod = @IDPeriod and @IDContract=IDContract and IDAccounting=1
	
	if  isnull (@IDBalance, 0) =0
	begin
		insert Balance(IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
		select top 1 IDAccounting, @IDPeriod, IDContract, AmountBalance, 0, 0
		from Balance with (nolock) 
		where IdPeriod<@IDPeriod and IdContract=@IdContract and IDAccounting=1
		order by idperiod desc
		set @IDBalance=scope_identity()
		
		if  isnull (@IDBalance, 0) =0
		begin
			print ''
--			insert Balance(IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)	
--			values (1, @IDPeriod, @IDContract, 0, 0, 0)
--			set @IdBalance=scope_identity()
		end
	end
end
---------------------------
SELECT @Amount1 Amount1_5

begin transaction
-- сохран€ем документ

if isnull(@Amount1, 0)<>0
begin
	insert into Operation(DateOperation, AmountOperation , NumberOperation, IDBalance, IDDocument, IdTypeOperation)
	values (@DocumentDate, @Amount1,  0,  @IDBalance,  @IDDocument,  1)
	if @@error<>0	set @blErr = 1

	if @NeedRecalc=0
	begin
		update balance
		set AmountBalance=AmountBalance+@Amount1,
			AmountPay=AmountPay+@Amount1
		where IDContract = @IDContract and IdPeriod=@IdPeriod and IdAccounting=1
		if @@error<>0	set @blErr = 1
	end
end

if isnull(@Amount2, 0)>0
begin
	insert into Operation(DateOperation, AmountOperation , NumberOperation, IDBalance, IDDocument, IdTypeOperation)
	values (@DocumentDate, @Amount2,  2,  @IDBalance2,  @IDDocument,  1)
	if @@error<>0	set @blErr = 1

	if @NeedRecalc=0
	begin
		update balance
		set AmountBalance=AmountBalance+@Amount2,
			AmountPay=AmountPay+@Amount2
		where IDBalance=@IDBalance2
		if @@error<>0 set @blErr = 1
	end
end
if isnull(@Amount6, 0)>0
begin
	insert into Operation(DateOperation, AmountOperation , NumberOperation, IDBalance, IDDocument, IdTypeOperation)
	values (@DocumentDate, @Amount6,  6,  @IDBalance6,  @IDDocument,  1)
	if @@error<>0	set @blErr = 1


	if @NeedRecalc=0
	begin
		update balance
		set AmountBalance=AmountBalance+@Amount6,
			AmountPay=AmountPay+@Amount6
		where IDBalance=@IDBalance6
		if @@error<>0 set @blErr = 1

	end
end

if isnull(@Amount3, 0)>0
begin
	insert into Operation(DateOperation, AmountOperation , NumberOperation, IDBalance, IDDocument, IdTypeOperation)
	values (@DocumentDate, @Amount3,  3,  @IDBalance3,  @IDDocument,  1)
	if @@error<>0	set @blErr = 1


	if @NeedRecalc=0
	begin
		update balance
		set AmountBalance=AmountBalance+@Amount3,
			AmountPay=AmountPay+@Amount3
		where IDBalance=@IDBalance3
		if @@error<>0 set @blErr = 1

	end
end

if isnull(@Amount4, 0)>0
begin
	insert into Operation(DateOperation, AmountOperation , NumberOperation, IDBalance, IDDocument, IdTypeOperation)
	values (@DocumentDate, @Amount4,  4,  @IDBalance4,  @IDDocument,  1)
	if @@error<>0	set @blErr = 1

	if @NeedRecalc=0
	begin
		update balance
		set AmountBalance=AmountBalance+@Amount4,
			AmountPay=AmountPay+@Amount4
		where IDBalance=@IDBalance4
		if @@error<>0 set @blErr = 1
	end
end

if @blErr = 0
	commit tran
else
	rollback tran


--exec dbo.spRecalcBalances @IDContract, @IDPeriod
--exec dbo.spRecalcBalancesRealOnePeriodByContract @IDContract, @IDPeriod

SELECT * FROM Operation o WHERE o.IDDocument = @IDDocument

