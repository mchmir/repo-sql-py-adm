SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO







CREATE PROCEDURE [dbo].[DocumentCarryPay] (@IDPeriod as int, 
	@IDContract as int, 
	@DocumentDate as DateTime, 
	@DocumentAmount as money,
	@IDDocument int output,
	@blErr as bit output)
AS

set @blErr = 0
--смотрим все балансы для проведения и расчитываем суммы для проведения

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
declare @IDPeriodPred as int
declare @NeedRecalc bit

set @IDPeriod =dbo.fGetNowPeriod() 
set @IDPeriodPred =dbo.fGetPredPeriod() 
set @Amount1=@DocumentAmount

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
			insert Balance(IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
			values (4,@idperiod,@idcontract,@Balance4,0,0)
			set @IDBalance4=scope_identity()
		end
	end
if isnull(@Balance4,0)<0  --если есть гасим Пеню
begin 
	if @Amount1>(-@Balance4)
		set @Amount4 = -@Balance4;
	else
		set @Amount4 = @Amount1;

	set @Amount1=@Amount1-@Amount4
end

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
			insert Balance(IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
			values (3,@idperiod,@idcontract,@Balance3,0,0)
			set @IDBalance3=scope_identity()
		end
	end
if isnull(@Balance3,0)<0  --если есть гасим гос.пошлину
begin 
	if @Amount1>(-@Balance3)
		set @Amount3 = -@Balance3;
	else
		set @Amount3 = @Amount1;

	set @Amount1=@Amount1-@Amount3
end

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
			insert Balance(IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
			values (6,@idperiod,@idcontract,@Balance6,0,0)
			set @IDBalance6=scope_identity()
		end
	end
if isnull(@Balance6,0)<0  --если есть гасим техническое обслуживание
begin 
	if @Amount1>(-@Balance6)
		set @Amount6 = -@Balance6;
	else
		set @Amount6 = @Amount1;

	set @Amount1=@Amount1-@Amount6
end

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
			insert Balance(IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
			values (2,@idperiod,@idcontract,@Balance2,0,0)
			set @IDBalance2=scope_identity()
		end
	end
if isnull(@Balance2,0)<0 --если есть гасим иск
begin 
	if @Amount1>(-@Balance2)
		set @Amount2 = -@Balance2;
	else
		set @Amount2 = @Amount1;

	set @Amount1=@Amount1-@Amount2
end

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
			insert Balance(IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)	
			values (1, @IDPeriod, @IDContract, 0, 0, 0)
			set @IdBalance=scope_identity()
		end
	end
end

set @NeedRecalc=0
--проводим документ

begin transaction
-- сохраняем документ

if isnull(@Amount1, 0)<>0
begin
	insert into Operation(DateOperation, AmountOperation , NumberOperation, IDBalance, IDDocument, IdTypeOperation)
	values (@DocumentDate, round(@Amount1,6),  0,  @IDBalance,  @IDDocument,  3)
	if @@error<>0	set @blErr = 1

	if @NeedRecalc=0
	begin
		update balance
		set AmountBalance=round(AmountBalance+@Amount1,6)--,AmountPay=AmountPay+@Amount1		where IDContract = @IDContract and IdPeriod=@IdPeriod and IdAccounting=1
		if @@error<>0	set @blErr = 1
	end
end

if isnull(@Amount2, 0)>0
begin
	insert into Operation(DateOperation, AmountOperation , NumberOperation, IDBalance, IDDocument, IdTypeOperation)
	values (@DocumentDate, round(@Amount2,6),  2,  @IDBalance2,  @IDDocument,  3)
	if @@error<>0	set @blErr = 1

	if @NeedRecalc=0
	begin
		update balance
		set AmountBalance=round(AmountBalance+@Amount2,6)--,AmountPay=AmountPay+@Amount2
		where IDBalance=@IDBalance2
		if @@error<>0 set @blErr = 1
	end
end
if isnull(@Amount6, 0)>0
begin
	insert into Operation(DateOperation, AmountOperation , NumberOperation, IDBalance, IDDocument, IdTypeOperation)
	values (@DocumentDate, round(@Amount6,6),  6,  @IDBalance6,  @IDDocument,  3)
	if @@error<>0	set @blErr = 1


	if @NeedRecalc=0
	begin
		update balance
		set AmountBalance=round(AmountBalance+@Amount6,6)--,AmountPay=AmountPay+@Amount6
		where IDBalance=@IDBalance6
		if @@error<>0 set @blErr = 1

	end
end

if isnull(@Amount3, 0)>0
begin
	insert into Operation(DateOperation, AmountOperation , NumberOperation, IDBalance, IDDocument, IdTypeOperation)
	values (@DocumentDate, round(@Amount3,6),  3,  @IDBalance3,  @IDDocument,  3)
	if @@error<>0	set @blErr = 1


	if @NeedRecalc=0
	begin
		update balance
		set AmountBalance=round(AmountBalance+@Amount3,6)--,AmountPay=AmountPay+@Amount3
		where IDBalance=@IDBalance3
		if @@error<>0 set @blErr = 1

	end
end

if isnull(@Amount4, 0)>0
begin
	insert into Operation(DateOperation, AmountOperation , NumberOperation, IDBalance, IDDocument, IdTypeOperation)
	values (@DocumentDate, round(@Amount4,6),  4,  @IDBalance4,  @IDDocument,  3)
	if @@error<>0	set @blErr = 1

	if @NeedRecalc=0
	begin
		update balance
		set AmountBalance=round(AmountBalance+@Amount4,6)--,AmountPay=AmountPay+@Amount4
		where IDBalance=@IDBalance4
		if @@error<>0 set @blErr = 1
	end
end

if @blErr = 0
	commit tran
else
	rollback tran








GO