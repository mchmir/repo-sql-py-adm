SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO



CREATE procedure [dbo].[repCountNoticeOne] (@IdDocument int) as

--declare @iddocument int
--set @iddocument=2716425


declare @IdPeriod int

set @IdPeriod=dbo.fGetPredPeriod()
declare @IdPeriodPred int
select top 1 @IdPeriodPred=IdPeriod from Period with (nolock)  where IdPeriod<@IdPeriod order by IdPeriod desc
declare @dEnd datetime
set @dEnd= dbo.fGetDatePeriod(@IdPeriod,0)
declare @dBegin datetime
select  @dBegin=convert(datetime, str(year)+'-'+str(month)+'-01', 20) from Period with (nolock) where IdPeriod=@IdPeriod

declare @ChetIzvehe table (Account varchar(50), IDContract int, idgmeter int, StatusGMeter int, CL int, 
	BalanceNaNach float, BalanceNaKonec float, SummaOplat float,
Oplat float,PerenosOplat float,tech float,
 SummaNach float, FactAmount float,
	KorrekAmount float, GosPochlina float, Tariff float, Norma float, FIO varchar(100), Adres varchar (100),
	GetIndication float, DateGetIndication datetime, LastIndication float, DateLastIndication datetime,
	Marka varchar(50), Serialnumber varchar(50), PayNow float, PayNowDate datetime, DateBegin datetime, 
	FactUse int, Kassir varchar (50), SumProp varchar (500), Indication float,SumPropKZ varchar (500), Peny float)

insert @ChetIzvehe (Account, IDContract, idgmeter, StatusGMeter, CL, 
	BalanceNaNach, BalanceNaKonec, SummaOplat, FIO, Adres,
	GetIndication, LastIndication, Marka, Serialnumber, PayNow, PayNowDate, DateBegin, Kassir,FactUse,Peny)
(select c.Account, c.IdContract, gm.idgmeter, dbo.fGetStatusPU (@dEnd,gm.idgmeter) StatusGMeter,
dbo.fGetCountLives(g.idgobject ,@IdPeriod) CL,
isnull( dbo.fGetLastBalance(@IdPeriodPred, c.IdContract, 0),0) BalanceNaNach, isnull( dbo.fGetLastBalance (@IdPeriod, c.IdContract, 0),0) BalanceNaKonec, 
isnull (sum(d.Documentamount),0) SummaOplat,
np.Surname+' '+left(ltrim(isnull(np.name,'')),1)+'.' +left(ltrim(isnull(np.Patronic,'')),1) FIO,  
s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat) Adres, 
--isnull(dbo.fLastIndicationYarMonthGet(gm.IdGMeter,@dEnd),0) GetIndication,--конечные показания но по дате не всегда правильно, потому как у некоторых показаний нету потребления
isnull(dbo.fLastIndicationIDPeriodGet(gm.IdGMeter,@idperiod),0)  GetIndication,  --конечные показание
isnull(dbo.fLastIndicationIDPeriod(gm.IdGMeter, @IdPeriodPred),0)  LastIndication, --начальные показание
isnull(tt.name,'')+'('+isnull(ltrim(tt.ClassAccuracy),'')+')' Marka,
isnull(gm.Serialnumber,'б/н') Serialnumber, opn.DocumentAmount, opn.DocumentDate, @dBegin, ag.name,1,0
from contract c with (nolock) 
inner join GObject g  with (nolock) on g.IdContract=c.IdContract
inner join address a  with (nolock) on a.idaddress=g.idaddress
inner join street s with (nolock)  on s.idstreet=a.idstreet
inner join house hs  with (nolock) on hs.idhouse=a.idhouse
inner join gru h with (nolock)  on h.idgru=g.idgru
inner join person np with (nolock)  on np.idperson=c.Idperson
inner join document opn with (nolock) on opn.idcontract=c.idcontract
	and opn.iddocument=@IdDocument
inner join batch b  with (nolock)  on b.idbatch=opn.idbatch
left join Agent ag with (nolock) on b.IDCashier=ag.idagent
left join GMeter gm  with (nolock) on gm.IdGObject=g.IdGObject
	and dbo.fGetStatusPU (@dEnd,gm.idgmeter)=1
left join typegmeter tt with (nolock)  on gm.idtypegmeter=tt.idtypegmeter
--left join Balance b  with (nolock) on b.IdContract=c.IdContract
	--and b.IdPeriod=@IdPeriodPred and b.IDAccounting=1
--left join Balance q with (nolock)  on q.IdContract=c.IdContract
--	and q.IdPeriod=@IdPeriod and q.IDAccounting=1
left join Document d  with (nolock) on d.idcontract=c.idcontract
and d.IdPeriod=@IdPeriod
	and d.idtypedocument=1 
where right(c.account,3)<>'600'
group by c.IdContract, c.Account, g.IDGobject,-- b.AmountBalance, q.AmountBalance, --f.Documentamount,
np.Surname, np.name, np.Patronic,  h.name ,s.name, hs.housenumber, hs.housenumberchar, 
gm.IdGMeter, a.flat, tt.name, tt.ClassAccuracy,
gm.Serialnumber, opn.DocumentAmount, opn.DocumentDate, ag.name)

update @ChetIzvehe
set Oplat=convert(float,qq.DA)
from @ChetIzvehe c
inner join (select sum(d.Documentamount) DA, IdContract
from Document d  with (nolock)
where IdPeriod=@IdPeriod and idtypedocument=1 
group by d.IdContract) qq on qq.IdContract=c.IdContract

update @ChetIzvehe
set PerenosOplat=convert(float,qq.DA)
from @ChetIzvehe c
inner join (select sum(Documentamount) DA, IdContract
from Document d  with (nolock)
where IdPeriod=@IdPeriod 	and d.idtypedocument=3 
group by IdContract) qq on qq.IdContract=c.IdContract


update @ChetIzvehe
set SummaNach=convert(float,qq.DA),FactUse=isnull(ff.idtypefu,1)
from @ChetIzvehe c
inner join (select -sum(o.amountoperation) DA, f.IdContract, f.iddocument
from Document f  with (nolock)
inner join operation o  with (nolock) on o.iddocument=f.iddocument
inner join balance b with (nolock) on b.idbalance=o.idbalance
and b.idaccounting<>4
where f.IdPeriod=@IdPeriod
	and f.idtypedocument=5 
group by f.IdContract,f.iddocument) qq on qq.IdContract=c.IdContract
inner join operation o with (nolock) on o.iddocument=qq.iddocument 
inner join factuse ff with (nolock) on ff.idoperation=o.idoperation

update @ChetIzvehe
set GosPochlina=convert(float,qq.AO)
from @ChetIzvehe c
inner join (select sum(o.amountoperation) AO, IdContract
from Document f  with (nolock) 
inner join operation o with (nolock) on f.iddocument=o.iddocument
where f.IdPeriod=@IdPeriod and f.idtypedocument=13 and o.idtypeoperation=2 
group by f.IdContract) qq on qq.IdContract=c.IdContract

update @ChetIzvehe
set Peny=convert(float,qq.AO)
from @ChetIzvehe c
inner join (select -sum(o.amountoperation) AO, f.IdContract
from Document f  with (nolock) 
inner join operation o with (nolock) on f.iddocument=o.iddocument
and f.IdPeriod=@IdPeriod and f.idtypedocument=5 and o.idtypeoperation=2 
inner join balance b with (nolock) on o.idbalance=b.idbalance
and idaccounting=4
group by f.IdContract) qq on qq.IdContract=c.IdContract

update @ChetIzvehe
set tech=convert(float,qq.AO)
from @ChetIzvehe c
inner join (select -sum(o.amountoperation) AO, f.IdContract
from Document f  with (nolock) 
inner join operation o with (nolock) on f.iddocument=o.iddocument
and f.IdPeriod=@IdPeriod and (f.idtypedocument=6 or f.idtypedocument=17 or f.idtypedocument=24) and o.idtypeoperation=2 
inner join balance b with (nolock) on o.idbalance=b.idbalance
and idaccounting=6
group by f.IdContract) qq on qq.IdContract=c.IdContract

update @ChetIzvehe
set Tariff=1, Norma=2.14

update @ChetIzvehe
set Tariff=Tariff*(select top 1 value from Tariff with (nolock) where idperiod=@idperiod)

update @ChetIzvehe
set Norma=Norma*(select top 1 value from Tariff with (nolock) where idperiod=@idperiod)

update @ChetIzvehe
set KorrekAmount=qq.DA
from @ChetIzvehe c
inner join (select -sum(f.Documentamount) DA, IdContract
from Document f  with (nolock) 
where f.IdPeriod=@IdPeriod 
	and (f.idtypedocument=7 or f.idtypedocument=11 or f.idtypedocument=14)group by f.IdContract) qq on qq.IdContract=c.IdContract

update @ChetIzvehe
set FactAmount=qq.FA
from @ChetIzvehe c
inner join (select sum(f.Factamount) fA, g.IdContract
from Factuse f  with (nolock)
inner join Gobject g  with (nolock) on g.idgobject=f.idgobject
inner join operation o with (nolock) on o.idoperation=f.idoperation
inner join document d  with (nolock) on d.iddocument=o.iddocument
where f.IdPeriod=@IdPeriod and d.idtypedocument=5
	--and g.IdGRU=@IdGru 
group by g.IdContract) qq on qq.IdContract=c.IdContract

update @ChetIzvehe
set indication=isnull(i.display,0)
from factuse f  with (nolock)
inner join indication i  with (nolock)on i.idindication=f.idindication
and f.iddocument=@iddocument

--update @ChetIzvehe
--set DateGetIndication=convert(datetime,qq.datedisplay,20)
--from @ChetIzvehe c
--inner join indication qq  with (nolock) on qq.idgmeter=c.idgmeter
--inner join factuse f  with (nolock) on f.idindication=qq.idindication
--	and f.idperiod=@IdPeriod
--where qq.display=c.GetIndication 
update @ChetIzvehe
set DateGetIndication=(select top 1 convert(datetime,qq.datedisplay,20) from  indication qq  with (nolock) where qq.display=c.GetIndication and c.idgmeter=qq.idgmeter and  qq.datedisplay<@dEnd+1 order by datedisplay desc)
from @ChetIzvehe c
--inner join (select top 1 convert(datetime,qq.datedisplay,20)rr indication qq  with (nolock) where qq.display=c.GetIndication )d
--
--
update @ChetIzvehe
set DateLastIndication=(select top 1 convert(datetime,qq.datedisplay,20) from  indication qq  with (nolock) where qq.display=c.LastIndication and c.idgmeter=qq.idgmeter and  qq.datedisplay<isnull(c.DateGetIndication,GetDate()) order by datedisplay desc)
from @ChetIzvehe c

--update @ChetIzvehe
--set DateLastIndication=convert(datetime,qq.datedisplay,20)
--from @ChetIzvehe c
--inner join indication qq  with (nolock) on qq.idgmeter=c.idgmeter
--where qq.display=c.LastIndication

update @ChetIzvehe
set KorrekAmount=0
where KorrekAmount is null

update @ChetIzvehe
set Peny=0
where Peny is null

update @ChetIzvehe
set GosPochlina=0
where GosPochlina is null

update @ChetIzvehe
set tech=0
where tech is null

declare @sumprop varchar (500)
declare @da bigint
select @da=convert(bigint,documentamount) from document  with (nolock) where iddocument=@IdDocument

exec dbo.spStringBigValueReturn @da, 1, @sumprop output

update @ChetIzvehe
set SumProp=@sumprop

declare @sumpropKZ varchar (500)
exec dbo.spStringBigValueReturnKZ @da, 1, @sumpropKZ output

update @ChetIzvehe
set SumPropKZ=@sumpropKZ

update @ChetIzvehe
set Oplat=0 where oplat is null
update @ChetIzvehe
set PerenosOplat=0 where PerenosOplat is null

select * 
from @ChetIzvehe
order by account













GO