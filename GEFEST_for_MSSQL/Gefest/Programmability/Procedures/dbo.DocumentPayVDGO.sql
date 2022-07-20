SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO








CREATE PROCEDURE [dbo].[DocumentPayVDGO] (@IDPeriod as int, 
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
	@blErr as bit output)
AS
declare @IdGMeter int
declare @IDGObject int

set @blErr = 0
--смотрим все балансы для проведения и расчитываем суммы для проведения

declare @IDOperation as int
declare @IDBalance as int
declare @IDBalance2 as int
declare @IDBalance3 as int
declare @IDBalance4 as int
declare @IDBalance6 as int
declare @Amount6 float
declare @Balance6 float
declare @IDPeriodPred as int
declare @NeedRecalc bit

set @IDPeriod =dbo.fGetNowPeriod() 
set @IDPeriodPred =dbo.fGetPredPeriod() 
set @Amount6=@DocumentAmount

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
	else
	begin
		insert Balance(IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
			values (6,@idperiod,@idcontract,0,0,0)
			set @IDBalance6=scope_identity()
	end
--if isnull(@Balance6,0)<0  --если есть гасим техническое обслуживание
--begin 
--	if @Amount1>(-@Balance6)
--		set @Amount6 = -@Balance6;
--	else
--		set @Amount6 = @Amount1;
--
--	set @Amount1=@Amount1-@Amount6
--end


set @NeedRecalc=0

delete operation where IdDocument=@IdDocument
if @@rowcount>0
	set @NeedRecalc=1

if @IDBatch=0 set @IDBatch=null
if @IdDocument=0
begin
	insert into Document (IDPeriod, IDBatch, IDContract, IDTypeDocument, DocumentDate, DocumentAmount, DocumentNumber, IdUser, DateAdd)
	values(@IDPeriod, @IDBatch, @IDContract, @IDTypeDocument, @DocumentDate, @DocumentAmount,@DocumentNumber, @IDUser, GetDate())
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

if @blErr = 0
	commit tran
else
	rollback tran

if @NeedRecalc=1
	exec dbo.spRecalcBalances @IDContract, @IdPeriod








GO