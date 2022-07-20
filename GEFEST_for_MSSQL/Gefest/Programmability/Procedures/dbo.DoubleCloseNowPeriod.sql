SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO




CREATE Procedure [dbo].[DoubleCloseNowPeriod] as 
begin
declare @Tariff numeric (10,4)
declare @TariffNew numeric (10,4)
declare @IdPeriod int
declare @NormaC float
set  @NormaC=2.14
--declare @Koef float
--set  @Koef=2.85

exec spReturnAverage

set @IdPeriod = dbo.fGetNowPeriod()
--логируем
insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
values ('DoubleCloseNowPeriod','Начали',GetDate())
--
--распределим без ПУ
select go.IdGObject, go.IdContract, 
	isnull(go.CountLives,0) countLive, 
	@NormaC Norma,
	f.IdFactUse IdFactUse
into #tmpFU
from Contract c with (nolock) 
inner join GObject go with (nolock)  on c.idcontract = go.idcontract
        and idstatusgobject = 1
left join gmeter gm with (nolock) on gm.idgobject = go.idgobject
        and idstatusgmeter = 1
left join FactUse f with (nolock) on f.IdGObject=go.IdGObject
           and f.IdTypeFU=2
           and f.IdPeriod=@IdPeriod
           and f.IdOperation is null
           and f.IDIndication is null
where gm.IdGmeter is null

update FactUse 
set FactAmount = Norma*CountLive
from factuse f with (nolock) 
inner join #tmpFU fu on fu.IdFactUse=f.IdFactUse

insert FactUse (IdPeriod, FactAmount, IDTypeFU, IdGObject)
select @IdPeriod, Norma*CountLive, 2, IdGObject
from #tmpFU
where IdFactUse is null

drop table #tmpFU
--логируем
insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
values ('DoubleCloseNowPeriod','Распределили без ПУ',GetDate())
--
--начислим по среднему тем у кого есть ПУ и нет показаний
select distinct go.IdGObject, go.IdContract, 
	isnull(go.CountLives,0) countLive, 
	@NormaC Norma,
	f.IdFactUse IdFactUse, c.account
into #tmpFUP 
from Contract c with (nolock)
inner join GObject go with (nolock)  on c.idcontract = go. idcontract
        and idstatusgobject = 1 and isnull(go.CountLives,0)<>0 --Добавил если численость 0 зачем создавать лишние записи
inner join gmeter gm with (nolock) on gm.idgobject = go.idgobject
        and idstatusgmeter = 1
left join document d with (nolock)  on d.idcontract=c.idcontract
	and (d.idtypedocument=7 or d.idtypedocument=12 or d.idtypedocument=18)
	and d.idperiod=@IdPeriod
left join FactUse f with (nolock) on f.IdGObject=go.IdGObject
           and f.IdTypeFU=1
           and f.IdPeriod=@IdPeriod
where f.idfactuse is null and d.iddocument is null

insert FactUse (IdPeriod, FactAmount, IDTypeFU, IdGObject)
select @IdPeriod, Norma*CountLive, 3, IdGObject
from #tmpFUP
where IdFactUse is null 

drop table #tmpFUP
--логируем
insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
values ('DoubleCloseNowPeriod','Начислили по среднему - есть ПУ нет показаний',GetDate())
--
--определим дату начисления
declare @Date DateTime
select @Date=case when month<12 then convert(datetime, str(year)+'-'+str(month+1)+'-01', 20)-1 
else convert(datetime, str(year+1)+'-01-01', 20)-1  end from Period  with (nolock) where IdPeriod=@IdPeriod

--if month(GetDate())<>month(@Date)
--begin
--определим текущий тариф
select top 1 @Tariff = value
from Tariff with (nolock) 
order by IdPeriod desc
--set @Tariff=1
set @TariffNew=388.89
--------------------------------------------------
declare @NullDateDisplay datetime
set @NullDateDisplay='1900-01-01'
--подготовим выборку по показаниям
select c.IdContract, f.IdFactUse, f.FactAmount, isnull(f.IdTypeFU,0) IdTypeFU, 
--@Koef PG,
 @NormaC Norma, 
null IdDocument, isnull(b.IdBalance,0) IdBalance, i.DateDisplay DateDisplay2, dbo.fPrevLastIndicationDate(i.idgmeter,i.DateDisplay) DateDisplay1
into #tmpFact
from FactUse f with (nolock) 
inner join GObject go with (nolock)  on go.IdGObject=f.IdGObject
	and f.Idoperation is null				-----исправил
inner join indication i with (nolock)  on i.idindication=f.idindication
	and dbo.DateOnly(i.DateDisplay)<=@Date
inner join Contract c with (nolock)  on c.IdContract=go.IdContract
left join Balance b with (nolock)  on b.IdContract=c.IdContract
	and b.IdPeriod=@IdPeriod
	and b.idaccounting=1


--!!!!!!!--
--тут нужно придумать как разбить начисление по норме на два - пропорционально 1/2
--подготовим выборку по норме
---insert #tmpFact (IdContract, IdFactUse, FactAmount, IdTypeFU, PG, Norma, IdDocument, IdBalance)
--возьмем сразу с потреблением 1/3
insert #tmpFact (IdContract, IdFactUse, FactAmount, IdTypeFU,  Norma, IdDocument, IdBalance,DateDisplay2,DateDisplay1)
(select c.IdContract, f.IdFactUse, round(f.FactAmount*0.2,3), isnull(f.IdTypeFU,0) IdTypeFU, 
--@Koef PG, 
@NormaC Norma, 
null IdDocument, isnull(b.IdBalance,0) IdBalance, @NullDateDisplay, @NullDateDisplay
from FactUse f with (nolock) 
inner join GObject go with (nolock)  on go.IdGObject=f.IdGObject
	and f.IdOperation is null			------исправил
	and f.IdTypeFU=2
inner join Contract c with (nolock)  on c.IdContract=go.IdContract
left join Balance b with (nolock)  on b.IdContract=c.IdContract
	and b.IdPeriod=@IdPeriod
	and b.idaccounting=1)
----
--подготовим выборку по среднему
---insert #tmpFact (IdContract, IdFactUse, FactAmount, IdTypeFU, PG, Norma, IdDocument, IdBalance)
insert #tmpFact (IdContract, IdFactUse, FactAmount, IdTypeFU,  Norma, IdDocument, IdBalance,DateDisplay2,DateDisplay1)
(select c.IdContract, f.IdFactUse, round(f.FactAmount*0.2,3), isnull(f.IdTypeFU,0) IdTypeFU, 
--@Koef PG, 
@NormaC Norma, 
null IdDocument, isnull(b.IdBalance,0) IdBalance, @NullDateDisplay, @NullDateDisplay
from FactUse f with (nolock) 
inner join GObject go with (nolock)  on go.IdGObject=f.IdGObject
	and f.IdOperation is null			------исправил
	and f.IdTypeFU<>1 and f.IdTypeFU<>2
inner join Contract c with (nolock)  on c.IdContract=go.IdContract
left join Balance b with (nolock)  on b.IdContract=c.IdContract
	and b.IdPeriod=@IdPeriod
	and b.idaccounting=1)
--возьмем сразу с потреблением 2/3 в таблицу нового тарифа
select c.IdContract, f.IDGObject, round(f.FactAmount*0.8,3) FactAmount, isnull(f.IdTypeFU,0) IdTypeFU, 
--@Koef PG, 
@NormaC Norma, 
null IdDocument, isnull(b.IdBalance,0) IdBalance
into #tmpFactNewTariff
from FactUse f with (nolock) 
inner join GObject go with (nolock)  on go.IdGObject=f.IdGObject
	and f.IdOperation is null			------исправил
	and f.IdTypeFU=2
inner join Contract c with (nolock)  on c.IdContract=go.IdContract
left join Balance b with (nolock)  on b.IdContract=c.IdContract
	and b.IdPeriod=@IdPeriod
	and b.idaccounting=1
----
insert into #tmpFactNewTariff (IdContract,IDGObject,FactAmount,IdTypeFU,Norma,IdDocument,IdBalance)
select c.IdContract, f.IDGObject, round(f.FactAmount*0.8,3) FactAmount, isnull(f.IdTypeFU,0) IdTypeFU, 
--@Koef PG, 
@NormaC Norma, 
null IdDocument, isnull(b.IdBalance,0) IdBalance
from FactUse f with (nolock) 
inner join GObject go with (nolock)  on go.IdGObject=f.IdGObject
	and f.IdOperation is null			------исправил
	and f.IdTypeFU<>1 and f.IdTypeFU<>2
inner join Contract c with (nolock)  on c.IdContract=go.IdContract
left join Balance b with (nolock)  on b.IdContract=c.IdContract
	and b.IdPeriod=@IdPeriod
	and b.idaccounting=1
--обновим объем потребления в factuse 1/3
update FactUse 
set FactAmount=ff.FactAmount   
from FactUse f
inner join #tmpFact ff with (nolock)  on f.IdFactUse=ff.IdFactUse
and f.IdTypeFU<>1

--!!!!!!!!--

--!!!!!!!!--
--select * from #tmpFact
declare @IdBatch int

--добавим пачку для документов начисление
insert Batch (IDTypePay, IDPeriod, Name, BatchCount, BatchAmount, BatchDate, IDTypeBatch, IDStatusBatch, NumberBatch)values (1, @IdPeriod, 'Начисление', 0, 0, @Date, 4, 2, str(@IdPeriod))
set @IdBatch=scope_identity()
--логируем
insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
values ('CloseNowPeriod','Создали пачку для документов',GetDate())

--добавим документы начисление для старого тарифа
--insert Document (IDContract, IDPeriod, IDBatch, IDTypeDocument, DocumentNumber, DocumentDate, DocumentAmount)
--(select distinct IdContract, @IdPeriod, @IDBatch, 5, 'Начисление',  @Date, 0
--from #tmpFact)
--добавим документы начисление для нового тарифа
--insert Document (IDContract, IDPeriod, IDBatch, IDTypeDocument, DocumentNumber, DocumentDate, DocumentAmount)
--(select distinct IdContract, @IdPeriod, @IDBatch, 5, 'Начисление_',  @Date, 0
--from #tmpFactNewTariff)

--логируем

insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
values ('CloseNowPeriod','Провели документы',GetDate())
--
--update #tmpFact 
--set IdDocument=d.IdDocument   
--from #tmpFact f
--inner join Document d with (nolock)  on d.IdContract=f.IdContract
--	and d.IdTypeDocument=5
--	and d.IdPeriod=@IdPeriod
--	and d.DocumentDate=@Date

--update #tmpFactNewTariff 
--set IdDocument=d.IdDocument   
--from #tmpFactNewTariff f
--inner join Document d with (nolock)  on d.IdContract=f.IdContract
--	and d.IdTypeDocument=5
--	and d.IdPeriod=@IdPeriod
--	and d.DocumentDate=@Date
--	and d.DocumentNumber='Начисление_'
----сразу добавим параметры документа для нового тарифа
--insert into pd(IDTypePD,IDDocument,Value)
--select distinct 36,IdDocument,'1' 
--from #tmpFactNewTariff
----
--update Document 
--set DocumentNumber='Начисление'   
--from Document d
--inner join #tmpFactNewTariff f with (nolock)  on d.IdDocument=f.IdDocument
--	
select * from #tmpFactNewTariff
select * from #tmpFact



declare  @tmpFact2 table(idfactuse int, FactAmount float, idoperation int)

declare @Count int
declare @IdDoc int
DECLARE @IdContract int 
DECLARE @IdFactUse int 
DECLARE @FactAmount float 
DECLARE @IdTypeFU int
--DECLARE @PG float 
declare @DateDisplay1 datetime
declare @DateDisplay2 datetime
DECLARE @Norma float
DECLARE @IdBalance int
declare @AmountOperation float
declare @number int
declare @IDOperation int
declare @IDGObject int
declare @CountDays as int
declare @CountDayOT int
declare @CountDayNT int
DECLARE @FactAmountDay float 
declare @FactAmountNT float
declare @DateX as datetime --когда закончил действовать тариф
set @DateX='2015-03-06'
DECLARE cur CURSOR
READ_ONLY
--FOR select  distinct  IdContract, IdFactUse, FactAmount, IdTypeFU, PG, Norma, IdDocument, IdBalance from #tmpFact
FOR select  distinct  IdContract, IdFactUse, FactAmount, IdTypeFU,  Norma, IdDocument, IdBalance from #tmpFact
where IdTypeFU<>1
OPEN cur
--поехали
set @Count=0
--FETCH NEXT FROM cur INTO @IdContract, @IdFactUse, @FactAmount, @IdTypeFU, @PG, @Norma, @IdDoc, @IdBalance
FETCH NEXT FROM cur INTO @IdContract, @IdFactUse, @FactAmount, @IdTypeFU,  @Norma, @IdDoc, @IdBalance

WHILE (@@fetch_status <> -1)
BEGIN
	IF (@@fetch_status <> -2)
	BEGIN
		set @Count=@Count+1
		--Если нужно добавляем баланс
		if @IdBalance=0
		begin
			insert Balance (IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
			values (1, @IdPeriod, @IdContract, 0, 0, 0)
			set @IdBalance=scope_identity()
		end
		set @number=0		
		set @AmountOperation=0
		insert Document (IDContract, IDPeriod, IDBatch, IDTypeDocument, DocumentNumber, DocumentDate, DocumentAmount)
		values (@IdContract, @IdPeriod, @IDBatch, 5, 'Начисление',  @Date, 0)
		set @IdDoc=scope_identity()

		--расчитаем сумму операции
		
		if @IdTypeFU=2 -- по норме
		begin
			set @AmountOperation=@FactAmount*@Tariff--кг
			insert into pd(IDTypePD,IDDocument,Value)
			values (36,@IdDoc,'2')
		end
		if @IdTypeFU=3 -- по среднему
		begin
			set @number=99999
			set @AmountOperation=@FactAmount*@Tariff --кг
			insert into pd(IDTypePD,IDDocument,Value)
			values (36,@IdDoc,'2')
		end

		--Добавляем операцию начисление
		insert Operation (DateOperation, AmountOperation, NumberOperation, IDBalance, IDDocument, IdTypeOperation)
		values (@Date, -@AmountOperation, @number, @IdBalance, @IdDoc, 2)
		set @Idoperation=scope_identity()
--		if @IdTypeFU=2 -- по норме
--		begin
--			insert into @tmpFact2 (idfactuse, FactAmount, idoperation)
--			values(@idfactuse,@FactAmount,@idoperation)
--		end
-------***Добавил		
		update Factuse
		set idoperation=@idoperation
		where idfactuse=@idfactuse
		
	END
--	FETCH NEXT FROM cur INTO @IdContract, @IdFactUse, @FactAmount, @IdTypeFU, @PG, @Norma, @IdDoc, @IdBalance
	FETCH NEXT FROM cur INTO @IdContract, @IdFactUse, @FactAmount, @IdTypeFU,  @Norma, @IdDoc, @IdBalance

END

CLOSE cur
DEALLOCATE cur
------
select * from @tmpFact2
------
------
DECLARE cur2 CURSOR
READ_ONLY
--FOR select  distinct  IdContract, IdFactUse, FactAmount, IdTypeFU, PG, Norma, IdDocument, IdBalance from #tmpFact
FOR select  distinct  IdContract, IDGObject, FactAmount, IdTypeFU,  Norma, IdDocument, IdBalance from #tmpFactNewTariff

OPEN cur2
--поехали
set @Count=0
--FETCH NEXT FROM cur INTO @IdContract, @IdFactUse, @FactAmount, @IdTypeFU, @PG, @Norma, @IdDoc, @IdBalance
FETCH NEXT FROM cur2 INTO @IdContract, @IDGObject, @FactAmount, @IdTypeFU,  @Norma, @IdDoc, @IdBalance

WHILE (@@fetch_status <> -1)
BEGIN
	IF (@@fetch_status <> -2)
	BEGIN
		set @Count=@Count+1
		--Если нужно добавляем баланс
		if @IdBalance=0
		begin
			insert Balance (IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
			values (1, @IdPeriod, @IdContract, 0, 0, 0)
			set @IdBalance=scope_identity()
		end
		set @number=0		
		set @AmountOperation=0
		--if @IdTypeFU=2 -- по норме
			--set @AmountOperation=@FactAmount*@TariffNew--кг

		insert Document (IDContract, IDPeriod, IDBatch, IDTypeDocument, DocumentNumber, DocumentDate, DocumentAmount)
		values (@IdContract, @IdPeriod, @IDBatch, 5, 'Начисление',  @Date, 0)
		set @IdDoc=scope_identity()
		--(select distinct IdContract, @IdPeriod, @IDBatch, 5, 'Начисление_',  @Date, 0
		--from #tmpFactNewTariff)
		insert into pd(IDTypePD,IDDocument,Value)
		values (36,@IdDoc,'1')
		if @IdTypeFU=2 -- по норме
		begin
			set @AmountOperation=@FactAmount*@TariffNew--кг
		end
		if @IdTypeFU=3 -- по среднему
		begin
			set @number=99999
			set @AmountOperation=@FactAmount*@TariffNew --кг
		end
		--Добавляем операцию начисление
		insert Operation (DateOperation, AmountOperation, NumberOperation, IDBalance, IDDocument, IdTypeOperation)
		values (@Date, -@AmountOperation, @number, @IdBalance, @IdDoc, 2)
		set @Idoperation=scope_identity()
-------***Добавил		
		insert into Factuse (IDPeriod, FactAmount, IDGObject, IDTypeFU, IDOperation)
		values (@IdPeriod,@FactAmount,@IDGObject,@IdTypeFU,@Idoperation)
		
	END
--	FETCH NEXT FROM cur INTO @IdContract, @IdFactUse, @FactAmount, @IdTypeFU, @PG, @Norma, @IdDoc, @IdBalance
	FETCH NEXT FROM cur2 INTO @IdContract, @IDGObject, @FactAmount, @IdTypeFU,  @Norma, @IdDoc, @IdBalance

END

CLOSE cur2
DEALLOCATE cur2
-----
DECLARE cur3 CURSOR
READ_ONLY
--FOR select  distinct  IdContract, IdFactUse, FactAmount, IdTypeFU, PG, Norma, IdDocument, IdBalance from #tmpFact
FOR select  distinct  IdContract, IdFactUse, FactAmount, IdTypeFU,  Norma, IdDocument, IdBalance, DateDisplay2,DateDisplay1 from #tmpFact
where IdTypeFU=1
OPEN cur3
--поехали
set @Count=0
--FETCH NEXT FROM cur INTO @IdContract, @IdFactUse, @FactAmount, @IdTypeFU, @PG, @Norma, @IdDoc, @IdBalance
FETCH NEXT FROM cur3 INTO @IdContract, @IdFactUse, @FactAmount, @IdTypeFU,  @Norma, @IdDoc, @IdBalance, @DateDisplay2,@DateDisplay1

WHILE (@@fetch_status <> -1)
BEGIN
	IF (@@fetch_status <> -2)
	BEGIN
		set @Count=@Count+1
		--Если нужно добавляем баланс
		if @IdBalance=0
		begin
			insert Balance (IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
			values (1, @IdPeriod, @IdContract, 0, 0, 0)
			set @IdBalance=scope_identity()
		end
		set @number=0		
		set @AmountOperation=0
		set @CountDays =abs(datediff(dd,@DateDisplay2,@DateDisplay1))
		if @DateDisplay2<=@DateX--'2015-02-07'
		begin
			set @CountDayOT =datediff(dd,@DateDisplay1,@DateDisplay2)
			set @CountDayNT =0
		end	
		else
		begin
			set @CountDayOT =datediff(dd,@DateDisplay1,@DateX)
			set @CountDayNT =datediff(dd,@DateX,@DateDisplay2)
		end	

		if @DateDisplay1>@DateX--'2015-02-07'
		begin
			set @CountDayOT =0
			set @CountDayNT =@CountDays
		end	
				
		set @FactAmountDay= round(@FactAmount/@CountDays,3)

			insert Document (IDContract, IDPeriod, IDBatch, IDTypeDocument, DocumentNumber, DocumentDate, DocumentAmount)
			values (@IdContract, @IdPeriod, @IDBatch, 5, 'Начисление',  @Date, 0)
			set @IdDoc=scope_identity()
			if @CountDayOT>0
			begin
				insert into pd(IDTypePD,IDDocument,Value)
				values (36,@IdDoc,'2')
			end
		--расчитаем сумму операции
			if @CountDayOT>0
			begin
				set @AmountOperation=@FactAmountDay*@CountDayOT*@Tariff --кубы
			end
			if @CountDayNT>0 and @CountDayOT=0
			begin
				set @AmountOperation=@FactAmountDay*@CountDayNT*@TariffNew --кубы
				insert into pd(IDTypePD,IDDocument,Value)
				values (36,@IdDoc,'1')
			end
			--Добавляем операцию начисление
			insert Operation (DateOperation, AmountOperation, NumberOperation, IDBalance, IDDocument, IdTypeOperation)
			values (@Date, -@AmountOperation, @number, @IdBalance, @IdDoc, 2)
			set @Idoperation=scope_identity()
	-------***Добавил		
			if @CountDayNT>0 and @CountDayOT>0
			begin
				update Factuse
				set idoperation=@idoperation, FactAmount=round(@FactAmountDay*@CountDayOT,3)
				where idfactuse=@idfactuse
			end
			if @CountDayNT=0 and @CountDayOT>0
			begin
				update Factuse
				set idoperation=@idoperation
				where idfactuse=@idfactuse
			end
			if @CountDayNT>0 and @CountDayOT=0
			begin
				update Factuse
				set idoperation=@idoperation
				where idfactuse=@idfactuse
			end
			---для нового тарифа
			if @CountDayNT>0 and @CountDayOT>0
			begin
				set @FactAmountNT=round(@FactAmount-@FactAmountDay*@CountDayOT,3)
				insert Document (IDContract, IDPeriod, IDBatch, IDTypeDocument, DocumentNumber, DocumentDate, DocumentAmount)
				values (@IdContract, @IdPeriod, @IDBatch, 5, 'Начисление',  @Date, 0)
				set @IdDoc=scope_identity()
				insert into pd(IDTypePD,IDDocument,Value)
				values (36,@IdDoc,'1')
				set @AmountOperation=@FactAmountNT*@TariffNew --кг
				--Добавляем операцию начисление
				insert Operation (DateOperation, AmountOperation, NumberOperation, IDBalance, IDDocument, IdTypeOperation)
				values (@Date, -@AmountOperation, @number, @IdBalance, @IdDoc, 2)
				set @Idoperation=scope_identity()
				--Добавим потребление по новому тарифу
				insert into Factuse (IDPeriod, FactAmount, IDGObject, IDTypeFU, IDOperation, IDIndication)
				select @IdPeriod,round(@FactAmountNT,3),IDGObject,1,@Idoperation, IDIndication
				from FactUse where idfactuse=@idfactuse
			end	
		
	END
--	FETCH NEXT FROM cur INTO @IdContract, @IdFactUse, @FactAmount, @IdTypeFU, @PG, @Norma, @IdDoc, @IdBalance
	FETCH NEXT FROM cur3 INTO @IdContract, @IdFactUse, @FactAmount, @IdTypeFU,  @Norma, @IdDoc, @IdBalance, @DateDisplay2,@DateDisplay1

END

CLOSE cur3
DEALLOCATE cur3
--Начислем пеню
exec spChargePeny
--Начислим за услуги ВДГО
exec spChargeVDGO
--пересчитаем сумму документов
update document
set DocumentAmount=-t.summa
from document with (nolock) 
left join (select sum(operation.AmountOperation) as summa,iddocument
	from operation with (nolock) 
	group by operation.iddocument
) T on document.iddocument=t.iddocument
where document.idtypedocument=5 and document.idperiod=@idPeriod

drop table #tmpFact
drop table #tmpFactNewTariff

--подчистим лишнее
--delete from factuse where idfactuse in(select idfactuse from factuse f
--inner join operation o on f.idoperation=o.idoperation
--where amountoperation=0
--and dateoperation=@Date)
--
--delete from operation 
--where amountoperation=0
--and dateoperation=@Date

--логируем
insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
values ('CloseNowPeriod','Пересчитаем балансы',GetDate())
--
--пересчитаем сумму остатков 
exec dbo.spRecalcBalancesOnePeriod @IdPeriod
exec dbo.spRecalcBalancesRealOnePeriod @idPeriod
--Определяем дату следующего периода
select @Date=case when month<12 then convert(datetime, str(year)+'-'+str(month+1)+'-01', 20) else convert(datetime, str(year+1)+'-01-01', 20)  end from Period where IdPeriod=@IdPeriod

insert Period (Year, Month, DateBegin, DateEnd)
values (Year(@Date), Month(@Date), @Date, case when month(@Date)<12 then convert(datetime, str(year(@Date))+'-'+str(month(@Date)+1)+'-01', 20)-1 
else convert(datetime, str(year(@Date)+1)+'-01-01', 20)-1  end)

set @IdPeriod=scope_identity()

insert Tariff (Value, IdPeriod,IDTypeTariff)
values (@TariffNew,@IdPeriod,1)
--select (
--select top 1 value
--from tariff  with (nolock) 
--order by idtariff desc) , @IdPeriod,1

update factuse
set IdPeriod=@idperiod
from FactUse f with (nolock) 
inner join indication i with (nolock)  on i.idindication=f.idindication
	and dbo.DateOnly(i.DateDisplay)>=@Date

--меняем численность проживающих где есть документы со сроком действия.

declare @idperiodpr int
declare @month int
declare @year int
select @idperiodpr=dbo.fGetPredPeriod()

select @month=month(dateend), @year=year(dateend) from period where Idperiod=@idperiodpr

update gobject
set countlives=convert(int, pd1.value)
from document d
inner join PD on pd.iddocument=d.iddocument
	and d.idtypedocument=2
	--and d.idperiod=33
	and pd.idtypepd=6
	and month(convert(datetime, pd.value, 104))=@month
	and year(convert(datetime, pd.value, 104))=@year
inner join contract c on c.idcontract=d.idcontract
inner join PD PD1 on pd1.iddocument=d.iddocument
	and pd1.idtypepd=5
left join document d1 on d1.idcontract=c.idcontract
and d1.idtypedocument=2 and d1.documentdate>d.documentdate
inner join gobject g on g.idcontract=c.idcontract
where d1.iddocument is null

insert into oldvalues(nametable,namecolumn,idobject,idperiod,datevalues,name)
select 'GObject','CountLives',g.idgobject,dbo.fGetNowPeriod(),dbo.DateOnly(GetDate()), pd1.value
from document d
inner join PD on pd.iddocument=d.iddocument
	and d.idtypedocument=2
	--and d.idperiod=33
	and pd.idtypepd=6
	and month(convert(datetime, pd.value, 104))=@month
	and year(convert(datetime, pd.value, 104))=@year
inner join contract c on c.idcontract=d.idcontract
inner join PD PD1 on pd1.iddocument=d.iddocument
	and pd1.idtypepd=5
left join document d1 on d1.idcontract=c.idcontract
and d1.idtypedocument=2 and d1.documentdate>d.documentdate
inner join gobject g on g.idcontract=c.idcontract
where d1.iddocument is null

--exec dbo.RecalcBalancesOnePeriod @IdPeriod
--логируем
insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
values ('CloseNowPeriod','Закончили',GetDate())
--

--end
end













GO