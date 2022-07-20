SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO





CREATE Procedure [dbo].[DoubleActionForPeriod] as
begin
--use Gefest


-- Монопольно захватываем базу
declare @error bit
declare @Q as VarChar(1000)
--set @Q=db_name()
set @Q='gefest'
exec @error=sp_dboption @Q,'single user', 'true'
set @error=0
if @error=0
begin
declare @path as varchar(1000)
--set @path='D:\Backup\Gefest\ClosePeriod'+convert(varchar(10),GetDate(), 120)+'.bak'
set @path='D:\ClosePeriod'+convert(varchar(10),GetDate(), 120)+'.bak'
--BACKUP DATABASE [Gefest] TO  DISK = @path  WITH  NOINIT ,  NOUNLOAD ,  NAME = N'Gefest backup',  NOSKIP ,  STATS = 10,  NOFORMAT 
--BACKUP DATABASE [Gefest] TO  DISK = @path WITH NOFORMAT, INIT,  NAME = N'Gefest backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10
--логируем
insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
values ('DoubleActionForPeriod','Сделали BackUp',GetDate())
--
---препроводим документы на которые не создалась операция
	exec dbo.spRealizationsOfOperations
	--закрываем период
	exec dbo.DoubleCloseNowPeriod
	--эту процедру только при закрытии декабря 2012,2013
    --exec dbo.PenyaNewYear
	--определим дату начала нового периода
	declare @IdPeriod int
	declare @FirstDate datetime
	set @IdPeriod = dbo.fGetNowPeriod()
	select @FirstDate=DateBegin from period where IdPeriod=@IdPeriod
	
	--подготовим документы для перепроведения
	select d.iddocument, d.idcontract , d.documentamount, d.documentdate, d.IdTypeDocument, d.IdBatch,
		i.display, d.iduser, i.idindication, d.note, sum(f.factamount) factamount
	into #tmpDoc
	from document d 
	left join PD on PD.iddocument=d.iddocument
		and PD.idtypePD=1
	left join indication i on i.idindication=convert(int, PD.Value)
	left join factuse f on f.idindication=i.idindication
	where dbo.DateOnly (d.DocumentDate) >=@FirstDate
		and d.IdTypeDocument=1
	group by d.iddocument, d.idcontract , d.documentamount, d.documentdate, d.IdTypeDocument, d.IdBatch,
		i.display, d.iduser, i.idindication, d.note

	--
	delete operation --, d.idperiod
	from operation  o
		inner join #tmpDoc d on o.iddocument = d.iddocument
	
	--перенесем документы в новый период
	update document
	set IDPeriod = dbo.fGetNowPeriod()
	from document d 
	inner join #tmpDoc td on td.iddocument = d.iddocument
	
	--и пачки тоже
	update Batch
	set IDPeriod = dbo.fGetNowPeriod()
	from Batch b 
	where  dbo.DateOnly (BatchDate)>=@FirstDate
	
	--- перепроведем документы
	
	declare @IDDocument as int
	declare @IDContract as int
	declare @DocumentAmount as float
	declare @DocumentDate as datetime
	declare @IdTypeDocument int
	declare @IdBatch int
	declare @display float
	declare @iduser int
	declare @idindication int
	declare @note varchar(8000)
	declare @factamount float
	declare @err  as  bit
	declare @AmountVDGO as float
	
	declare Loc cursor for 
	select d.iddocument, d.idcontract , d.documentamount, d.documentdate, d.IdTypeDocument, d.IdBatch,
		d.display, d.iduser, d.idindication, d.note, d.factamount
	from #tmpDoc d 
	
	open Loc
	
	FETCH NEXT FROM Loc INTO @IDDocument, @IDContract, @DocumentAmount, @DocumentDate, @IdTypeDocument, @IdBatch,
			@display, @iduser, @idindication, @note, @factamount
	WHILE @@FETCH_STATUS=0
	 BEGIN
		set @err=0
			exec DocumentPay @IDPeriod, @IDBatch, @IDContract, @IDTypeDocument, @DocumentDate, @DocumentAmount, @Display, 0, @IDUser, @IDDocument, @IDIndication, @FactAmount, '', @note, @err, @AmountVDGO
		if @err<>0 
			select 'error'+str(@err)
	
		FETCH NEXT FROM Loc INTO @IDDocument, @IDContract, @DocumentAmount, @DocumentDate, @IdTypeDocument, @IdBatch,
			@display, @iduser, @idindication, @note, @factamount
	
	end
	CLOSE Loc
	DEALLOCATE Loc

-- Чистка мусора
--declare @Q as VarChar(1000)
--set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
--Exec(@Q)
--set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
--Exec(@Q)

declare @i int
declare @r int
set @i=1
while (@i<15)
begin
insert Balance (idContract,idAccounting,idPeriod,AmountBalance,AmountCharge,AmountPay)
select Document.idContract, 1, Document.idPeriod,0,0,0
from Document
inner join operation on operation.iddocument=document.iddocument
inner join balance on balance.idBalance=operation.idBalance
	and Document.idContract<>Balance.idContract
left outer join balance BB on BB.idContract=Document.idContract
	and BB.idPeriod=Document.idPeriod
where BB.idBalance is null

/* Ищем двойные балансы */
select count(1) Counter,Balance.idPeriod,Balance.idContract, balance.idaccounting
into #TempBB
from Balance
group by Balance.idPeriod, Balance.idContract, balance.idaccounting
having count(1)>1
order by Balance.idContract, Balance.idPeriod
if ((select count(*) from #TempBB)>0)
set @r=1
else
set @r=0

/* Удаляем ненужные дубли */
declare @idContract1 int
declare @idPeriod1 int
declare @idBalance int
--declare @idContract int
--declare @idPeriod int
declare @idOperation int
declare @idaccounting int
declare @idaccounting1 int
declare Curss cursor for select Balance.idBalance,Balance.idContract, Balance.idPeriod,Operation.idOperation, Balance.idaccounting
from #TempBB
inner join Balance on #TempBB.idContract=Balance.idContract
	and #TempBB.idPeriod=Balance.idPeriod
	and #TempBB.idaccounting=Balance.idaccounting
left outer join operation on operation.idBalance=Balance.idBalance
order by #TempBB.idContract, #TempBB.idPeriod, Operation.idOperation DESC

set @idContract1 = 0
set @idPeriod1 = 0

begin tran
open curss
fetch next from curss into @idBalance, @idContract, @idPeriod, @idOperation , @idaccounting
while @@Fetch_Status=0
begin
	if @idContract1 = @idContract and @idPeriod1 = @idPeriod and @idaccounting1 = @idaccounting and @idOperation is null
	begin
		delete Balance where idBalance=@idBalance
	end
	else
	if @idContract1 = @idContract and @idPeriod1 = @idPeriod and @idaccounting1 = @idaccounting
	begin
		/* Переносим операции на один баланс */
		update operation
		set idBalance=@idBalance
		from operation
		inner join Balance on operation.idbalance=balance.idbalance
			and balance.idperiod=@idPeriod
			and balance.idContract=@idContract
	end
	set @idContract1 = @idContract
	set @idPeriod1 = @idPeriod
	set @idaccounting1=@idaccounting
	fetch next from curss into @idBalance, @idContract, @idPeriod, @idOperation, @idaccounting
end
close curss
deallocate curss
commit tran
drop Table #TempBB

/* Исправление привязки операций */
update operation
set idBalance=BB.idBalance
from Document
inner join operation on operation.iddocument=document.iddocument
inner join balance on balance.idBalance=operation.idBalance
	and Document.idContract<>Balance.idContract
left outer join balance BB on BB.idContract=Document.idContract
	and BB.idPeriod=Document.idPeriod
if (@r=0)
begin
set @i=15
select 'Двойных балансов нет'
end
else
set @i=@i+1
end

--declare @IdPeriod int
set @IdPeriod = dbo.fGetPredPeriod()
exec  dbo.spRecalcBalancesOnePeriod  @IdPeriod
set @IdPeriod = dbo.fGetPredPeriod()
exec dbo.spRecalcBalancesRealOnePeriod @IdPeriod
--declare @IdPeriod int
set @IdPeriod = dbo.fGetNowPeriod()
exec  dbo.spRecalcBalancesOnePeriod  @IdPeriod
set @IdPeriod = dbo.fGetNowPeriod()
exec dbo.spRecalcBalancesRealOnePeriod @IdPeriod

insert pd ( IDTypePD, IDDocument, Value)
select 22,ss,2 from(
select pd.value vv,d.iddocument ss,dbo.fGetLastBalance(@IdPeriod, d.IdContract, 2) dd , c.account
from document d
inner join contract c on c.idcontract=d.idcontract
and idtypedocument=13 and dbo.fGetLastBalance(@IdPeriod, d.IdContract, 2)>=0
left join pd on pd.iddocument=d.iddocument
and pd.value=2 and idtypepd=22)dd where vv is null
--логируем
insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
values ('DoubleActionForPeriod','Закончили',GetDate())

-- Вернуть базу в рабочий режим
--declare @error bit
---declare @Q as VarChar(1000)
--set @Q=db_name()
----set @Q='gefest'
----exec @error=sp_dboption @Q,'single user', 'false'

-- Чистка мусора
--declare @Q as VarChar(1000)
--set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
--Exec(@Q)
--set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
--Exec(@Q)
select 'Период закрыт'
end
else
begin
select 'Ошибка захвата базы монопольно'
end
end





















GO