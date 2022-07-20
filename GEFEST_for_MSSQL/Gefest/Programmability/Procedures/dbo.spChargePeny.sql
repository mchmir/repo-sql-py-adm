SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO










--Процедура по начислению пени
CREATE PROCEDURE [dbo].[spChargePeny] AS
declare @idperiod int
declare @idprevperiod int
declare @days int
declare @stavka float
declare @dated datetime
declare @Ref float
declare @DateEnd datetime
--логируем
insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
values ('spChargePeny','Начали',GetDate())
--
set @idperiod=dbo.fGetNowPeriod()
set @idprevperiod=dbo.fGetPredPeriod()
select top 1 @Ref=name from stavka order by idstavka desc
--согласно СЗ 423 от 07.06.2013
set @Ref=@Ref*1.5
--
set @stavka = @ref/365/100
select @days=convert(int, dbo.fGetDatePeriod(@idperiod, 2))-convert(int, dbo.fGetDatePeriod(@idperiod, 1))+1
set @dated=dbo.fGetDatePeriod(@idperiod, 1)
set @DateEnd=dbo.fGetDatePeriod(@idperiod, 2)
--delete workpeny

insert workpeny (idcontract, account, bal1, bal2, amountoperation, dated, forpeny, stavka, days, peny,idperiod)
select idcontract, account, bal1, bal2, amountoperation, @dated, bal1+bal2-amountoperation forpeny, @stavka stavka, @days days, null peny,@idperiod
from
(
select c.idcontract, c.account, b1.amountbalance bal1, 
0 bal2, --isnull(b2.amountbalance,0) bal2, на основании ТЗ от 22.02.2018
isnull(op.amountoperation,0) amountoperation, dk.iddocument
from contract c
inner join balancereal b1 on b1.idcontract=c.idcontract
	and b1.idaccounting=1
	and b1.idperiod=@idprevperiod
	--and isnull(b1.amountbalance,0)<0
	and isnull(c.status,0)<>2 and isnull(c.ChargePeny,1)=1
left join balancereal b2 on b2.idcontract=c.idcontract
	and b2.idaccounting=2
	and b2.idperiod=@idprevperiod
	and isnull(b2.amountbalance,0)<0
left join 
(select  d.idcontract, sum(o.amountoperation) amountoperation
from document d
inner join operation o on o.iddocument=d.iddocument
	and (o.idtypeoperation=2 or o.idtypeoperation=3)
	and d.idperiod=@idprevperiod
	and d.idtypedocument=5 
	and isnull(o.numberoperation, '')<>'99999'
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idprevperiod
	and b.idaccounting=1
group by d.idcontract) op on op.idcontract=c.idcontract
left join document dk on dk.idcontract=c.idcontract
	and dk.idperiod=@idperiod
	and (dk.idtypedocument=3 or dk.idtypedocument=7 or dk.idtypedocument=8 or dk.idtypedocument=11)
where dk.iddocument is null
) qq
where bal1+bal2-amountoperation<-100 

set @dated=dbo.fGetDatePeriod(@idperiod, 2)
/*
insert workpeny (idcontract, dated, forpeny, stavka, days, idperiod)
select idcontract, documentdate, 
case when forpeny>ao then ao else forpeny end forpeny, 
@stavka, convert(int, @dated)-convert(int, documentdate)+1, @idperiod
from
(
select d.idcontract, d.documentdate, sum(o.amountoperation) ao, -forpeny forpeny
from workpeny w
inner join document d on d.idcontract=w.idcontract
	and d.idperiod=@idperiod
	and d.idtypedocument=1
	and d.documentdate<@DateEnd
inner join operation o on o.iddocument=d.iddocument
	and o.idtypeoperation=1
inner join balance b on b.idcontract=d.idcontract
	and b.idperiod=@idperiod
	and (b.idaccounting=1 or b.idaccounting=2)
group by d.idcontract, d.documentdate, forpeny
) qq
*/

	declare Locss cursor for 
	select d.idcontract, d.documentdate, sum(o.amountoperation) ao, -forpeny forpeny
	from workpeny w
	inner join document d on d.idcontract=w.idcontract
		and d.idperiod=@idperiod
		and d.idtypedocument=1
		and d.documentdate<=@DateEnd
		and w.idperiod=@idperiod
	inner join operation o on o.iddocument=d.iddocument
		and o.idtypeoperation=1
	inner join balance b on b.idcontract=d.idcontract
		and b.idperiod=@idperiod
		and b.idaccounting=1 -- or b.idaccounting=2)  на основании ТЗ от 22.02.2018
	group by d.idcontract, d.documentdate, forpeny
	declare @IDContract int
	declare @DocumentDate datetime
	declare @AO float
	declare @ForPeny float
	declare @SumFP float

	open Locss
	
	FETCH NEXT FROM Locss INTO @IDContract, @DocumentDate, @AO, @ForPeny
	WHILE @@FETCH_STATUS=0
	 BEGIN

		select @SumFP=sum(forpeny) 
		from WorkPeny
		Where idcontract=@IDContract and idperiod=@idperiod

		if (@SumFP<0)
		begin

			insert workpeny (idcontract, dated, forpeny, stavka, days, idperiod)
			values (@IDContract, @Documentdate, case when -@SumFP>@ao then @ao else -@SumFP end, @stavka, convert(int, @dated)-convert(int, @documentdate)+1, @idperiod)
		end
		FETCH NEXT FROM Locss INTO @IDContract, @DocumentDate, @AO, @ForPeny
	
	end
	CLOSE Locss
	DEALLOCATE Locss


update workpeny
set peny=forpeny*stavka*days
 
--select * from workpeny order by idcontract
--select sum(forpeny), sum(peny) from workpeny
--
--select * from workpeny where idcontract=860246
--select sum(forpeny), sum(peny) from workpeny where idcontract=860246


--select * from accounting
--select * from typedocument
--select * from typeoperation

set @dated=dbo.fGetDatePeriod(@idperiod, 2)

declare @IdBatch int

--добавим пачку для документов начисление
select @idbatch=idbatch from batch where idperiod=@idperiod and idtypebatch=4 and name='Начисление'

DECLARE curPeny CURSOR
READ_ONLY
FOR select idcontract, sum(peny) Forpeny from workpeny where idperiod=@idperiod group by idcontract having sum(peny)<0

--DECLARE @idcontract int
--declare @Forpeny float
declare @IdBalance int
declare @Idoperation int
declare @iddocument int

set @idcontract=0
set @Forpeny=0

OPEN curPeny

FETCH NEXT FROM curPeny INTO @idcontract, @Forpeny
WHILE (@@fetch_status <> -1)
BEGIN
	IF (@@fetch_status <> -2)
	BEGIN
		select @iddocument=0
		select  @iddocument=iddocument from document where idcontract=@idcontract and idperiod=@idperiod and idtypedocument=5
	
		if @iddocument=0
		begin
			insert document (IDContract, IDPeriod, IDBatch, IDTypeDocument, DocumentNumber, DocumentDate, DocumentAmount)
			values (@idcontract, @IdPeriod, @IdBatch, 5, 'Начисление', @dated, @Forpeny)
			set @iddocument=scope_identity()
		end
				
		select @IdBalance=0
		--поиск подходящего баланса
		select @IdBalance=isnull(idbalance, 0)
		from balance 
		where idcontract=@idcontract and idperiod=@idperiod and idaccounting=4
		
		if @IdBalance=0
		begin
			insert Balance (IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
			values (4, @IdPeriod, @IdContract, @Forpeny, 0, 0)
			set @IdBalance=scope_identity()
		end

		insert operation(DateOperation, AmountOperation,  IDBalance, IDDocument, IdTypeOperation)
		values (@dated, @Forpeny,  @IdBalance, @iddocument, 2)

	END
	FETCH NEXT FROM curPeny INTO @idcontract, @Forpeny
END

CLOSE curPeny
DEALLOCATE curPeny
--логируем
insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
values ('spChargePeny','Закончили',GetDate())
--


















GO