SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO











CREATE PROCEDURE [dbo].[spReturnAverage] AS
declare @IdPeriod int
set @IdPeriod=dbo.fGetNowPeriod()
declare @IdPeriodPred int
set @IdPeriodPred=dbo.fGetPredPeriod()
declare @Date DateTime
select @Date=case when month<12 then convert(datetime, str(year)+'-'+str(month+1)+'-01', 20)-1 
else convert(datetime, str(year+1)+'-01-01', 20)-1  end from Period  with (nolock) where IdPeriod=@IdPeriod

declare @IdBatch int

--добавим пачку для документов начисление
insert Batch (IDTypePay, IDPeriod, Name, BatchCount, BatchAmount, BatchDate, IDTypeBatch, IDStatusBatch, NumberBatch)
values (1, @IdPeriod, 'Возврат по среднему', 0, 0, @Date, 4, 2, str(@IdPeriod))
set @IdBatch=scope_identity()

declare @tmpAvFact table (account varchar(20), idcontract int, idgobject int, docamount float, factamount float)

--все у кого было начисление по среднему в прошлом периоде и 
--появились показания в текушем
insert @tmpAvFact (Account, idcontract, idgobject)
select distinct c.account, c.idcontract, g.idgobject
from contract c 
inner join gobject g on g.idcontract=c.idcontract
inner join factuse f on f.idgobject=g.idgobject
	and f.idtypefu=3
 	and f.idperiod=@IdPeriodPred
	and f.Factamount>0        -----------Добавил
inner join factuse fu on fu.idgobject=g.idgobject
 	and fu.idperiod=@IdPeriod
 	and fu.idtypefu=1

---Добавил чтобы снималось начисление по среднему не только за прошлый период
insert @tmpAvFact (Account, idcontract, idgobject)
select distinct c.account, c.idcontract, g.idgobject--,f.idperiod,t.*,d.*
from contract c 
inner join gobject g on g.idcontract=c.idcontract
inner join factuse f on f.idgobject=g.idgobject
	and f.idtypefu=3
 	and f.idperiod<@IdPeriodPred and f.Factamount>0 
inner join factuse fu on fu.idgobject=g.idgobject
 	and fu.idperiod=@IdPeriod
 	and fu.idtypefu=1
left join document d on d.idcontract=c.idcontract
and d.idtypedocument=14 and idbatch not in (66908,68089) and d.idperiod<@IdPeriodPred
left join @tmpAvFact t on t.idgobject=g.idgobject
where t.idgobject is null and d.iddocument is null
group by c.account, c.idcontract, g.idgobject

---Добавил для снятие среднего 
insert @tmpAvFact (Account, idcontract, idgobject)
select distinct c.account, c.idcontract, g.idgobject--,fff.fffiperod , ddd.per--ifg,d.*
from contract c 
inner join gobject g on g.idcontract=c.idcontract
inner join (select idgobject, max(f.idperiod)fffiperod from factuse f 
---and account=1242072
	where f.idtypefu=3
 	and f.idperiod<@IdPeriodPred and f.Factamount>0 group by idgobject) fff on fff.idgobject=g.idgobject
inner join factuse fu on fu.idgobject=g.idgobject
 	and fu.idperiod=@IdPeriod
 	and fu.idtypefu=1
inner join (select idcontract, max(idperiod)per from document d where d.idtypedocument=14 and idbatch not in (66908,68089) group by idcontract) ddd on ddd.idcontract=c.idcontract
and fff.fffiperod>ddd.per
left join @tmpAvFact t on t.idgobject=g.idgobject
where t.idgobject is null ---and d.iddocument is not null
group by c.account, c.idcontract, g.idgobject


DECLARE curAvr CURSOR
READ_ONLY
FOR select idcontract, idgobject from @tmpAvFact

DECLARE @idcontract int
DECLARE @idgobject int
DECLARE @IDLastPeriod int
declare @factamount float
declare @factamountM3 float
declare @docamount float
declare @iddocument int 
declare @IdBalance int
declare @Idoperation int

OPEN curAvr

FETCH NEXT FROM curAvr INTO @idcontract, @idgobject
WHILE (@@fetch_status <> -1)
BEGIN
	IF (@@fetch_status <> -2)
	BEGIN
		--ищем период в котором был возврат последний
---------*******Испр
		set @IDLastPeriod=null
		set @factamount=0
		set @factamountM3=0

		select top 1 @IDLastPeriod=isnull(d.IdPeriod,0)
		from Document d
		where d.IdContract=@idcontract and d.IdPeriod<@IdPeriod 
		and idtypedocument=14 and idbatch not in (66908,68089) order by idperiod desc

		if @IDLastPeriod is null
			set @IDLastPeriod=1
-------************
		--начиная с найденого периода формируем сумму и колличество возврата
		select @docamount=sum(FA) from (
		select f.factamount*(select value from tariff where idperiod=f.idperiod) FA
		from FactUse f
		where f.idgobject=@idgobject and isnull(f.idtypefu,0)=3 and f.idperiod>@IDLastPeriod ) qq

		select @factamount=sum(f.factamount)/2.85
		from FactUse f
		where f.idgobject=@idgobject and isnull(f.idtypefu,0)=3 and f.idperiod>@IDLastPeriod and f.idperiod<63

		select @factamountM3=sum(f.factamount)
		from FactUse f
		where f.idgobject=@idgobject and isnull(f.idtypefu,0)=3 and f.idperiod>@IDLastPeriod and f.idperiod>62
		set @factamount=isnull(@factamount,0)+isnull(@factamountM3,0)
		--соответственно проставляем потребление и сумму
		update @tmpAvFact
		set docamount=@docamount,
			factamount=@factamount
		where idcontract=@idcontract

		--вставляем документ, потребление и проводим его
		insert document (IDContract, IDPeriod, IDBatch, IDTypeDocument, DocumentNumber, DocumentDate, DocumentAmount)
		values (@idcontract, @IdPeriod, @IdBatch, 14, 'Возврат', @Date, @docamount)
		set @iddocument=scope_identity()
		
		select @IdBalance=0
		--поиск подходящего баланса
		select @IdBalance=isnull(idbalance, 0)
		from balance 
		where idcontract=@idcontract and idperiod=@idperiod and idaccounting=1
		
		if @IdBalance=0
		begin
			insert Balance (IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
			values (1, @IdPeriod, @IdContract, 0, 0, 0)
			set @IdBalance=scope_identity()
		end

		insert operation(DateOperation, AmountOperation, NumberOperation, IDBalance, IDDocument, IdTypeOperation)
		values (@Date, @docamount, 99999, @IdBalance, @iddocument, 2)
		set @idoperation=scope_identity()
--исправил		
		insert FactUse (IDPeriod, FactAmount, IDIndication, IDGObject, IDDocument, IDTypeFU, IDOperation)		
		values (@IdPeriod, -@factamount, null, @idgobject, @iddocument, 3, @idoperation)
	END
	FETCH NEXT FROM curAvr INTO @idcontract, @idgobject
END

CLOSE curAvr
DEALLOCATE curAvr

--select * from @tmpAvFact



GO