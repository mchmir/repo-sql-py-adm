SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO









CREATE procedure [dbo].[repCountNoticeGMNewTariff] (@IdGRU int, @IdPeriod int) as 
---------------*************Счет-извещение**********------------------
--declare @IdGRU int
--declare @IdPeriod int
--set @IdPeriod=72
------set @IdGru=8633
--select @IdGru=IdGRU from GRU where memo='264'
----select @idgru
declare @IdPeriodPred int
select top 1 @IdPeriodPred=dbo.fGetPredPeriodVariable(@IdPeriod)
declare @dBegin datetime
declare @dEnd datetime
set @dBegin= dbo.fGetDatePeriod(@IdPeriod,1)
set @dEnd= dbo.fGetDatePeriod(@IdPeriod,0)

declare @T table (idcontract int, idgmeter int)
insert into @T(idcontract, idgmeter)
select c.IdContract, max(gm.idgmeter)
from contract c with (nolock) 
inner join GObject g  with (nolock) on g.IdContract=c.IdContract
	and g.IdGRU=@IdGru 	and isnull(c.PrintChetIzvehen,0)=0
left join GMeter gm  with (nolock) on gm.IdGObject=g.IdGObject
	and dbo.fGetStatusPU (@dEnd,gm.idgmeter)=1 
group by c.idcontract

select distinct c.Account, c.IdContract, gm.idgmeter, dbo.fGetStatusPU (@dEnd,gm.idgmeter) StatusGMeter,
dbo.fGetCountLives(g.idgobject ,@IdPeriod) CL,
isnull( dbo.fGetLastBalance(@IdPeriodPred, c.IdContract, 0),0) BalanceNaNach, isnull( dbo.fGetLastBalance (@IdPeriod, c.IdContract, 0),0) BalanceNaKonec, 
isnull (sum(d.Documentamount),0) SummaOplat,
convert(float, 0.00) as SummaNach,
convert(float,0.00) as FactAmount,
convert(float,0.00) as KorrekAmount,
convert(float,0.00) as Peny,
convert(float,0.00) as GosPochlina,
convert(float,0.00) as Oplat,
convert(float,0.00) as Tech,
convert(float,0.00) as PerenosOplat,
convert(float, dbo.fGetLastPGValue(@IDPeriod, 1, @idgru)) as Tariff,
convert(float, dbo.fGetLastPGNorma(@IDPeriod, 1, @idgru)) as Norma,
np.Surname+' '+left(ltrim(isnull(np.name,'')),1)+'.' +left(ltrim(isnull(np.Patronic,'')),1)  FIO,  
s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat) Adres, 
--isnull(dbo.fLastIndicationYarMonthGet(gm.IdGMeter,@dEnd),0) GetIndication, --конечные показания но по дате не всегда правильно, потому как у некоторых показаний нету потребления
isnull(dbo.fLastIndicationIDPeriodGet(gm.IdGMeter,@idperiod),0) GetIndication, --конечные показание
convert(datetime, null) DateGetIndication, --дата конечных показаний
isnull(dbo.fLastIndicationIDPeriod(gm.IdGMeter, @IdPeriodPred),0) LastIndication, --начальные показание
convert(datetime, null) DateLastIndication ,
isnull(tt.name,'')+'('+isnull(ltrim(tt.ClassAccuracy),'')+')' Marka,
isnull(gm.Serialnumber,'б/н') Serialnumber,
convert(int, 1) as FactUse, @dBegin as Date, dateadd(yy, tt.servicelife, case when gm.dateverify='1800-01-01' then gm.datefabrication else gm.dateverify end) dateverify,
convert(int,1) as TypeCloseGmeter

into #tmpChetIzvehe
from contract c with (nolock) 
inner join GObject g  with (nolock) on g.IdContract=c.IdContract
	and g.IdGRU=@IdGru 	and isnull(c.PrintChetIzvehen,0)=0
inner join address a  with (nolock) on a.idaddress=g.idaddress
inner join street s with (nolock)  on s.idstreet=a.idstreet
inner join house hs  with (nolock) on hs.idhouse=a.idhouse
inner join gru h with (nolock)  on h.idgru=g.idgru
inner join person np with (nolock)  on np.idperson=c.Idperson
left join @t gm1 on gm1.IdContract=c.IdContract
left join GMeter gm  with (nolock) on gm1.idgmeter=gm.idgmeter--gm.IdGObject=g.IdGObject
	--and dbo.fGetStatusPU (@dEnd,gm.idgmeter)=1 
left join typegmeter tt with (nolock)  on gm.idtypegmeter=tt.idtypegmeter
left join Document d  with (nolock) on d.idcontract=c.idcontract
and  d.IdPeriod=@IdPeriod and (d.idtypedocument=1  or d.idtypedocument=3)
 group by c.IdContract, c.Account, g.IDGobject, 
 np.Surname, np.name, np.Patronic,  h.name ,s.name, hs.housenumber, hs.housenumberchar, 
 gm.IdGMeter, a.flat, tt.name, tt.ClassAccuracy,
 gm.Serialnumber,tt.servicelife,gm.dateverify,gm.DateFabrication


update #tmpChetIzvehe
set Oplat=convert(float,qq.DA)
from #tmpChetIzvehe c
inner join (select sum(d.Documentamount) DA, IdContract
from Document d  with (nolock)
where IdPeriod=@IdPeriod and idtypedocument=1 
group by d.IdContract) qq on qq.IdContract=c.IdContract

update #tmpChetIzvehe
set PerenosOplat=convert(float,qq.DA)
from #tmpChetIzvehe c
inner join (select sum(Documentamount) DA, IdContract
from Document d  with (nolock)
where IdPeriod=@IdPeriod 	and d.idtypedocument=3 
group by IdContract) qq on qq.IdContract=c.IdContract

update #tmpChetIzvehe
set SummaNach=convert(float,qq.DA)
from #tmpChetIzvehe c
inner join (select -sum(o.amountoperation) DA, f.IdContract
from Document f  with (nolock)
inner join operation o  with (nolock) on o.iddocument=f.iddocument
inner join balance b with (nolock) on b.idbalance=o.idbalance
and b.idaccounting<>4
where f.IdPeriod=@IdPeriod
	and f.idtypedocument=5 
group by f.IdContract) qq on qq.IdContract=c.IdContract

update #tmpChetIzvehe
set FactUse=isnull(ff.idtypefu,1)
from #tmpChetIzvehe c
inner join (select f.IdContract, f.iddocument
from Document f  with (nolock)
inner join operation o  with (nolock) on o.iddocument=f.iddocument
inner join balance b with (nolock) on b.idbalance=o.idbalance
and b.idaccounting<>4
where f.IdPeriod=@IdPeriod
	and f.idtypedocument=5 
group by f.IdContract,f.iddocument) qq on qq.IdContract=c.IdContract
inner join operation o  with (nolock) on o.iddocument=qq.iddocument
inner join factuse ff with (nolock) on o.idoperation=ff.idoperation

update #tmpChetIzvehe
set GosPochlina=convert(float,qq.AO)
from #tmpChetIzvehe c
inner join (select sum(o.amountoperation) AO, f.IdContract
from Document f  with (nolock) 
inner join operation o with (nolock) on f.iddocument=o.iddocument
and f.IdPeriod=@IdPeriod and f.idtypedocument=13 and o.idtypeoperation=2 
inner join balance b with (nolock) on o.idbalance=b.idbalance
and idaccounting=3
group by f.IdContract) qq on qq.IdContract=c.IdContract

update #tmpChetIzvehe
set Peny=convert(float,qq.AO)
from #tmpChetIzvehe c
inner join (select -sum(o.amountoperation) AO, f.IdContract
from Document f  with (nolock) 
inner join operation o with (nolock) on f.iddocument=o.iddocument
and f.IdPeriod=@IdPeriod and f.idtypedocument=5 and o.idtypeoperation=2 
inner join balance b with (nolock) on o.idbalance=b.idbalance
and idaccounting=4
group by f.IdContract) qq on qq.IdContract=c.IdContract

update #tmpChetIzvehe
set tech=convert(float,qq.AO)
from #tmpChetIzvehe c
inner join (select -sum(o.amountoperation) AO, f.IdContract
from Document f  with (nolock) 
inner join operation o with (nolock) on f.iddocument=o.iddocument
and f.IdPeriod=@IdPeriod and (f.idtypedocument=6 or f.idtypedocument=17 or f.idtypedocument=24) and o.idtypeoperation=2 
inner join balance b with (nolock) on o.idbalance=b.idbalance
and idaccounting=6
group by f.IdContract) qq on qq.IdContract=c.IdContract

update #tmpChetIzvehe
set Tariff=Tariff*(select top 1 value from Tariff with (nolock) where idperiod=@idperiod),
Norma=Norma*(select top 1 value from Tariff with (nolock) where idperiod=@idperiod)

update #tmpChetIzvehe
set KorrekAmount=qq.DA
from #tmpChetIzvehe c
inner join (select -sum(f.Documentamount) DA, IdContract
from Document f  with (nolock) 
where f.IdPeriod=@IdPeriod 
	and (f.idtypedocument=7 or f.idtypedocument=11 or f.idtypedocument=14)
group by f.IdContract) qq on qq.IdContract=c.IdContract

update #tmpChetIzvehe
set FactAmount=qq.FA
from #tmpChetIzvehe c
inner join (select sum(f.Factamount) fA, g.IdContract
from Factuse f  with (nolock)
inner join Gobject g  with (nolock) on g.idgobject=f.idgobject
inner join operation o with (nolock) on o.idoperation=f.idoperation
inner join document d  with (nolock) on d.iddocument=o.iddocument
and f.IdPeriod=@IdPeriod and d.idtypedocument=5
	--and g.IdGRU=@IdGru 
group by g.IdContract) qq on qq.IdContract=c.IdContract


update #tmpChetIzvehe
set korrekAmount=0
where korrekAmount is null

update #tmpChetIzvehe
set DateGetIndication=(select top 1 convert(datetime,qq.datedisplay,20) from  indication qq  with (nolock) where qq.display=c.GetIndication and c.idgmeter=qq.idgmeter order by datedisplay desc)
from #tmpChetIzvehe c
--inner join (select top 1 convert(datetime,qq.datedisplay,20)rr indication qq  with (nolock) where qq.display=c.GetIndication )d

update #tmpChetIzvehe
set TypeCloseGmeter=(
select top 1 pd.value from document d 
inner join pd on pd.iddocument=d.iddocument
and idtypepd=33 and d.idcontract=c.idcontract
order by d.documentdate desc) from #tmpChetIzvehe c where c.statusGmeter=2

update #tmpChetIzvehe
set TypeCloseGmeter=999 from #tmpChetIzvehe
inner join (select idcontract from gobject where idstatusgobject=2) obj on obj.idcontract=#tmpChetIzvehe.idcontract


update #tmpChetIzvehe
set TypeCloseGmeter=1
where TypeCloseGmeter is null

update #tmpChetIzvehe
set DateLastIndication=(select top 1 convert(datetime,qq.datedisplay,20) from  indication qq  with (nolock) where qq.display=c.LastIndication and c.idgmeter=qq.idgmeter and  qq.datedisplay<isnull(c.DateGetIndication,GetDate()) order by datedisplay desc)
from #tmpChetIzvehe c
--inner join indication qq  with (nolock)  on qq.idgmeter=c.idgmeter
--where qq.display=c.LastIndication and qq.datedisplay<isnull(c.DateGetIndication,GetDate())


select t.* 
from #tmpChetIzvehe t
left join (select idcontract from gobject  with (nolock)  where idstatusgobject=2 and idgru=@idgru and isnull( dbo.fGetLastBalance (@IdPeriod, IdContract, 0),0)>-1) c on c.idcontract=t.idcontract
where c.idcontract is null
order by account

drop table #tmpChetIzvehe





















GO