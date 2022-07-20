SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO










CREATE PROCEDURE [dbo].[DocumentPayTerminal] (@IDPeriod as int, 
	@IDBatch as int, 
	@IDContract as int, 
	@IDTypeDocument as int, 
	@DocumentDate as DateTime, 
	@DocumentAmount as money,
	@Display as float,
	@UpdateBatch bit,
	@IDUser int, 
	@IDDocument int output,
	@IDIndication as int output,
	@FactAmount as float output,
	@DocumentNumber as varchar (20) output,
	@Premech as varchar (2000) output,
	@blErr as bit output,
	@AmountVDGO as float output,
	@TerminalData as varchar (20)
)
AS
declare @IdGMeter int
declare @IDGObject int

set @blErr = 0
--смотрим все балансы для проведения и расчитываем суммы для проведения

declare @IDOperation as int
declare @IDBalance as int
declare @IDBalance1 as int
declare @IDBalance2 as int
declare @IDBalance3 as int
declare @IDBalance4 as int
declare @IDBalance6 as int
declare @AmountAll float
declare @Amount1 float
declare @Amount2 float
declare @Amount3 float
declare @Amount4 float
declare @Amount6 float
declare @AmountCur6 float
declare @AmountPred6 float
declare @Balance1 float
declare @Balance2 float
declare @Balance3 float
declare @Balance4 float
declare @Balance6 float
declare @BalanceCur6 float
declare @SumOpCur6 float
declare @BalancePred6 float
declare @IDPeriodPred as int
declare @NeedRecalc bit

set @IDPeriod =dbo.fGetNowPeriod() 
set @IDPeriodPred =dbo.fGetPredPeriod() 
set @AmountAll=@DocumentAmount
set @AmountVDGO=0

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
	if @AmountAll>(-@Balance4)
		set @Amount4 = -@Balance4;
	else
		set @Amount4 = @AmountAll;

	set @AmountAll=@AmountAll-@Amount4
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
	if @AmountAll>(-@Balance3)
		set @Amount3 = -@Balance3;
	else
		set @Amount3 = @AmountAll;

	set @AmountAll=@AmountAll-@Amount3
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

--if isnull(@Balance6,0)<0  --если есть гасим техническое обслуживание
--begin 
--	if @AmountAll>(-@Balance6)
--		set @Amount6 = -@Balance6;
--	else
--		set @Amount6 = @AmountAll;
--
--	set @AmountAll=@AmountAll-@Amount6
--end

set @BalancePred6=dbo.fGetLastBalance(@IDPeriodPred, @IDContract,6)

if isnull(@BalancePred6,0)<0  --если есть гасим техническое обслуживание (сначала только за прошлый период)
begin
	--берем услуги только прошлых периодов 
	--выберем операции по услугам в текущем периоде
	set @SumOpCur6=(select sum(amountoperation) from operation op inner join balance b on b.idbalance=op.idbalance
		and b.idcontract=@IDContract and b.idperiod=@IDPeriod and b.IDAccounting=6 and amountoperation>0)

	set @Amount6 = 	isnull(@BalancePred6,0)+isnull(@SumOpCur6,0)
	--если старый долг по услугам погашен не полностью
	if @Amount6 <0
		begin
			--будем гасить долг прошлого периода
			--set @BalancePred6 = isnull(@BalancePred6,0)
			if @AmountAll>(-@Amount6)
				set @AmountPred6 = -@Amount6;
			else
				set @AmountPred6 = @AmountAll;

			set @AmountAll=@AmountAll-@AmountPred6
		end
end
-----

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
	if @AmountAll>(-@Balance2)
		set @Amount2 = -@Balance2;
	else
		set @Amount2 = @AmountAll;

	set @AmountAll=@AmountAll-@Amount2
end

--if isnull(@Amount1,0)<>0 --все что осталось на гашение основного долга
--begin 
--	select top 1 @IDBalance=IDBalance from balance with (nolock) 
--	where IDPeriod = @IDPeriod and @IDContract=IDContract and IDAccounting=1
--	
--	if  isnull (@IDBalance, 0) =0
--	begin
--		insert Balance(IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
--		select top 1 IDAccounting, @IDPeriod, IDContract, AmountBalance, 0, 0
--		from Balance with (nolock) 
--		where IdPeriod<@IDPeriod and IdContract=@IdContract and IDAccounting=1
--		order by idperiod desc
--		set @IDBalance=scope_identity()
--		
--		if  isnull (@IDBalance, 0) =0
--		begin
--			insert Balance(IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)	
--			values (1, @IDPeriod, @IDContract, 0, 0, 0)
--			set @IdBalance=scope_identity()
--		end
--	end
--end

--надо высчитать сколько на основной долг
--if isnull(@AmountAll,0)<>0 --все что осталось на гашение основного долга
--begin 
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

set @Balance1=dbo.fGetLastBalance(@IDPeriod, @IDContract, 1)

select @Balance1 as Balance1

if isnull(@Balance1,0)<0 --если есть гасим основной долг
begin 
	if @AmountAll>(-@Balance1)
		set @Amount1 = -@Balance1;
	else
		set @Amount1 = @AmountAll;

	set @AmountAll=@AmountAll-@Amount1
end
--end

set @BalanceCur6=dbo.fGetLastBalance(@IDPeriod, @IDContract,6)
if isnull(@BalanceCur6,0)<0  --если есть гасим техническое обслуживание (за текущий период)
begin
	--берем услуги только текущего периода
	if @AmountPred6>0
		begin
			set @SumOpCur6=@AmountPred6+@BalanceCur6  
		end
	else
		begin
			set @SumOpCur6=@BalanceCur6
		end
			if @AmountAll>(-@SumOpCur6)
				set @AmountCur6 = -@SumOpCur6;
			else
				set @AmountCur6 = @AmountAll;

			set @AmountAll=@AmountAll-@AmountCur6

end
--все что осталось  - на основной долг
set @Amount1 = isnull(@Amount1,0)+@AmountAll
set @NeedRecalc=0

delete operation where IdDocument=@IdDocument
if @@rowcount>0
	set @NeedRecalc=1

if @IDBatch=0 set @IDBatch=null
if @IdDocument=0
begin
	insert into Document (IDPeriod, IDBatch, IDContract, IDTypeDocument, DocumentDate, DocumentAmount, DocumentNumber, IdUser, DateAdd, TerminalData)
	values(@IDPeriod, @IDBatch, @IDContract, @IDTypeDocument, dbo.DateOnly(@DocumentDate), @DocumentAmount,@DocumentNumber, @IDUser, GetDate(), @TerminalData)
	if @@Error <> 0
		set @blErr = 1
	set @IdDocument = scope_identity()
end
else
begin 
	update Document set DocumentDate=@DocumentDate, 
		DocumentAmount=@DocumentAmount,
		IDModify=@IDUser,
		DateModify=GetDate()
	where IdDocument=@IdDocument
	if @@Error <> 0
		set @blErr = 1
end

if @UpdateBatch=1
begin
	update Batch set BatchAmount = BatchAmount+@DocumentAmount where IDBatch = @IDBatch

	if @@Error <> 0
		set @blErr = 1

	update Batch set BatchCount = BatchCount + 1 where IDBatch = @IDBatch
	if @@Error <> 0
		set @blErr = 1
end
--сохраняем показания
if isnull(@IdIndication,0)<>0
begin 
	if @Display=0
	begin
		delete Indication where IdIndication=@IdIndication
		if @@Error <> 0
			set @blErr = 1

		delete PD where IdDocument=@IdDocument and IDTypePD=1
	end
	else
	begin 
		update Indication
		set DateDisplay=@DocumentDate,
		Display=@Display,
		IDModify=@IDUser,
		DateModify=GetDate()
		where IdIndication=@IdIndication
		if @@Error <> 0
			set @blErr = 1

		if exists(select * from FactUse with (nolock) where isnull(IdIndication,0)=@IdIndication)
			update FactUse 
			set FactAmount=@FactAmount,
				iddocument=@IdDocument
			where isnull(IdIndication,0)=@IdIndication
		else
		begin
			set @IDGObject = 
			(
			select g.idgobject from Gmeter g  with (nolock) 
			inner join indication i with (nolock)  on i.idgmeter=g.idgmeter
				and i.idindication=@IdIndication
			) 		
	
			insert into FactUse  (IDPeriod, FactAmount, IDIndication, IDGObject, IDDocument ,idtypeFU)
			values (@IDPeriod, @FactAmount,  @IdIndication, @IDGObject ,@IDDocument,1)
		end

		if not exists(select * from PD with (nolock)  where IDTypePD=1 and IDDocument=@IdDocument)
		begin
			insert into PD (IDTypePD, IDDocument, Value)
			values (1, @IdDocument, ltrim(str(@IdIndication)))
		end
	end
end
else
begin 
	if isnull(@Display,0)<>0
	begin
		select @IdGMeter=IdGMeter
		from Contract c with (nolock) 
		inner join GObject g with (nolock)  on g.idcontract=c.idcontract
			and c.IdContract=@IdContract
		inner join GMeter gm with (nolock)  on gm.IdGObject=g.IdGObject
			where gm.IDStatusGMeter=1
		
		if @@Error <> 0
			set @blErr = 1

		insert into Indication (Display, DateDisplay, IdGMeter, IdUser, DateAdd, Premech, IDTypeindication)
		values (@Display, @DocumentDate, @IdGMeter, @IDUser, GetDate(), @Premech,4)
		if @@Error <> 0
			set @blErr = 1

		set @IdIndication=scope_identity()

		insert into PD (IDTypePD, IDDocument, Value)
		values (1, @IdDocument, ltrim(str(@IdIndication)))

		
		set @IDGObject = 
		(
		select go.idgobject from Gmeter g  with (nolock) 
		inner join gobject go with (nolock)  on g.idgobject=go.idgobject
			where IdGMeter = @IdGMeter
		) 		

		insert into FactUse  (IDPeriod, FactAmount, IDIndication, IDGObject, IDDocument ,idtypeFU)
		values (@IDPeriod, @FactAmount,  @IdIndication, @IDGObject ,@IDDocument,1)
	end
end
--проводим документ

begin transaction
-- сохраняем документ

if isnull(@Amount1, 0)<>0
begin
	insert into Operation(DateOperation, AmountOperation , NumberOperation, IDBalance, IDDocument, IdTypeOperation)
	values (dbo.DateOnly(@DocumentDate), round(@Amount1,6),  0,  @IDBalance,  @IDDocument,  1)
	if @@error<>0	set @blErr = 1

	if @NeedRecalc=0
	begin
		update balance
		set AmountBalance=round(AmountBalance+@Amount1,6),
			AmountPay=round(AmountPay+@Amount1,6)		where IDContract = @IDContract and IdPeriod=@IdPeriod and IdAccounting=1
		if @@error<>0	set @blErr = 1
	end
end

if isnull(@Amount2, 0)>0
begin
	insert into Operation(DateOperation, AmountOperation , NumberOperation, IDBalance, IDDocument, IdTypeOperation)
	values (dbo.DateOnly(@DocumentDate), round(@Amount2,6),  2,  @IDBalance2,  @IDDocument,  1)
	if @@error<>0	set @blErr = 1

	if @NeedRecalc=0
	begin
		update balance
		set AmountBalance=round(AmountBalance+@Amount2,6),
			AmountPay=round(AmountPay+@Amount2,6)
		where IDBalance=@IDBalance2
		if @@error<>0 set @blErr = 1
	end
end

--if isnull(@Amount6, 0)>0
--begin
--	set @AmountVDGO=@Amount6
--	insert into Operation(DateOperation, AmountOperation , NumberOperation, IDBalance, IDDocument, IdTypeOperation)
--	values (@DocumentDate, @Amount6,  6,  @IDBalance6,  @IDDocument,  1)
--	if @@error<>0	set @blErr = 1
--
--
--	if @NeedRecalc=0
--	begin
--		update balance
--		set AmountBalance=AmountBalance+@Amount6,
--			AmountPay=AmountPay+@Amount6
--		where IDBalance=@IDBalance6
--		if @@error<>0 set @blErr = 1
--
--	end
--end

if isnull(@AmountPred6, 0)>0
begin
	set @AmountVDGO=@AmountPred6
	insert into Operation(DateOperation, AmountOperation , NumberOperation, IDBalance, IDDocument, IdTypeOperation)
	values (@DocumentDate, round(@AmountPred6,6),  6,  @IDBalance6,  @IDDocument,  1)
	if @@error<>0	set @blErr = 1


	if @NeedRecalc=0
	begin
		update balance
		set AmountBalance=round(AmountBalance+@AmountPred6,6),
			AmountPay=round(AmountPay+@AmountPred6,6)
		where IDBalance=@IDBalance6
		if @@error<>0 set @blErr = 1

	end
end

if isnull(@AmountCur6, 0)>0
begin
	set @AmountVDGO=@AmountVDGO+@AmountCur6
	insert into Operation(DateOperation, AmountOperation , NumberOperation, IDBalance, IDDocument, IdTypeOperation)
	values (@DocumentDate, round(@AmountCur6,6),  6,  @IDBalance6,  @IDDocument,  1)
	if @@error<>0	set @blErr = 1


	if @NeedRecalc=0
	begin
		update balance
		set AmountBalance=round(AmountBalance+@AmountCur6,6),
			AmountPay=round(AmountPay+@AmountCur6,6)
		where IDBalance=@IDBalance6
		if @@error<>0 set @blErr = 1

	end
end

if isnull(@Amount3, 0)>0
begin
	insert into Operation(DateOperation, AmountOperation , NumberOperation, IDBalance, IDDocument, IdTypeOperation)
	values (dbo.DateOnly(@DocumentDate), round(@Amount3,6),  3,  @IDBalance3,  @IDDocument,  1)
	if @@error<>0	set @blErr = 1


	if @NeedRecalc=0
	begin
		update balance
		set AmountBalance=round(AmountBalance+@Amount3,6),
			AmountPay=round(AmountPay+@Amount3,6)
		where IDBalance=@IDBalance3
		if @@error<>0 set @blErr = 1

	end
end

if isnull(@Amount4, 0)>0
begin
	insert into Operation(DateOperation, AmountOperation , NumberOperation, IDBalance, IDDocument, IdTypeOperation)
	values (dbo.DateOnly(@DocumentDate), round(@Amount4,6),  4,  @IDBalance4,  @IDDocument,  1)
	if @@error<>0	set @blErr = 1

	if @NeedRecalc=0
	begin
		update balance
		set AmountBalance=round(AmountBalance+@Amount4,6),
			AmountPay=round(AmountPay+@Amount4,6)
		where IDBalance=@IDBalance4
		if @@error<>0 set @blErr = 1
	end
end

if @blErr = 0
	commit tran
else
	rollback tran

if @NeedRecalc=1
	exec dbo.spRecalcBalances @IDContract, @IdPeriod










GO