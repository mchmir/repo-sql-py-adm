SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO





--exec repGMeterForCountNotice 8497,107,866693












CREATE procedure [dbo].[repGMeterForCountNotice] (@IdGRU int, @IdPeriod int, @IDContract int) as 
---------------*************Инфо по ПУ для Счет-извещение**********------------------
--declare @IdGRU int
--declare @IdPeriod int
--declare @IDContract int
--set @IdPeriod=97
--set @IdGru=8577
--set @IDContract=917614
if @IdPeriod=122
begin
exec dbo.repGMeterForCountNoticeNewTariff @IdGRU, @IdPeriod, @IDContract
end
else
begin
declare @IdPeriodPred int
select top 1 @IdPeriodPred=dbo.fGetPredPeriodVariable(@IdPeriod)
declare @dBegin datetime
declare @dEnd datetime
declare @IDGmeter int
declare @GmeterCount int
set @dBegin= dbo.fGetDatePeriod(@IdPeriod,1)
set @dEnd= dbo.fGetDatePeriod(@IdPeriod,0)

select @IdGRU IdGRU,@IdPeriod IdPeriod,c.Account, c.IdContract, gm.idgmeter, dbo.fGetStatusPU (@dEnd,gm.idgmeter) StatusGMeter,
dbo.fGetCountLives(g.idgobject ,@IdPeriod) CL,
--isnull( dbo.fGetLastBalance(@IdPeriodPred, c.IdContract, 0),0) BalanceNaNach, isnull( dbo.fGetLastBalance (@IdPeriod, c.IdContract, 0),0) BalanceNaKonec, 
convert(float, 0.00) as BalanceBegin,
convert(float, 0.00) as SummOplat,
convert(float, 0.00) as SummaNach,
convert(float, 0.00) as BalanceEnd,
convert(float,0.00) as FactAmount,
convert(float, dbo.fGetLastPGValue(@IDPeriod, 1, @idgru)) as Tariff,
convert(float, dbo.fGetLastPGNorma(@IDPeriod, 1, @idgru)) as Norma,
--np.Surname+' '+left(ltrim(isnull(np.name,'')),1)+'.' +left(ltrim(isnull(np.Patronic,'')),1)  FIO,  
--s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat) Adres, 
--isnull(dbo.fLastIndicationYarMonthGet(gm.IdGMeter,@dEnd),0) GetIndication, --конечные показания но по дате не всегда правильно, потому как у некоторых показаний нету потребления
isnull(dbo.fLastIndicationIDPeriodGet(gm.IdGMeter,@idperiod),0) GetIndication, --конечные показание
convert(datetime, null) DateGetIndication, --дата конечных показаний
isnull(dbo.fLastIndicationIDPeriod(gm.IdGMeter, @IdPeriodPred),0) LastIndication, --начальные показание
convert(datetime, null) DateLastIndication ,
isnull(tt.name,'')+'('+isnull(ltrim(tt.ClassAccuracy),'')+')' Marka,
isnull(gm.Serialnumber,'б/н') Serialnumber,
convert(int, 1) as FactUse, @dBegin as Date, dateadd(yy, tt.servicelife, case when gm.dateverify='1800-01-01' then gm.datefabrication else gm.dateverify end) dateverify,
convert(int,1) as TypeCloseGmeter

into #tmpChetIzvehePU
from contract c with (nolock) 
inner join GObject g  with (nolock) on g.IdContract=c.IdContract
	and g.IdGRU=@IdGru 	--and isnull(c.PrintChetIzvehen,0)=0
--inner join address a  with (nolock) on a.idaddress=g.idaddress
--inner join street s with (nolock)  on s.idstreet=a.idstreet
--inner join house hs  with (nolock) on hs.idhouse=a.idhouse
--inner join gru h with (nolock)  on h.idgru=g.idgru
--inner join person np with (nolock)  on np.idperson=c.Idperson
left join GMeter gm  with (nolock) on gm.IdGObject=g.IdGObject
	and dbo.fGetStatusPU (@dEnd,gm.idgmeter)=1 
left join typegmeter tt with (nolock)  on gm.idtypegmeter=tt.idtypegmeter
left join Document d  with (nolock) on d.idcontract=c.idcontract
and  d.IdPeriod=@IdPeriod and (d.idtypedocument=1  or d.idtypedocument=3)
where c.idcontract=@IDContract
 group by c.IdContract, c.Account, g.IDGobject, 
 --np.Surname, np.name, np.Patronic,  h.name ,s.name, hs.housenumber, hs.housenumberchar, a.flat,
 gm.IdGMeter,  tt.name, tt.ClassAccuracy,
 gm.Serialnumber,tt.servicelife,gm.dateverify,gm.DateFabrication

set @idgmeter=(select top 1 idgmeter from #tmpChetIzvehePU)
set @gmetercount=(select count(idgmeter) from #tmpChetIzvehePU)
--if isnull(@idgmeter,0)=0 and @gmetercount<2 
--	begin
--		set @idgmeter=(select top 1 gm.idgmeter from  GObject g  with (nolock) 
--		left join GMeter gm  with (nolock) on gm.IdGObject=g.IdGObject
--		and g.IdGRU=@IdGru and g.idcontract=@idcontract
--		order by gm.idgmeter desc)
--		
--		if isnull(dbo.fLastIndicationIDPeriodGet(@idgmeter,@idperiod),0)>0
--			begin
--				update #tmpChetIzvehePU
--				set idgmeter=(select top 1 gm.idgmeter from  GObject g  with (nolock) 
--				left join GMeter gm  with (nolock) on gm.IdGObject=g.IdGObject
--				and g.IdGRU=@IdGru and g.idcontract=@idcontract
--				order by gm.idgmeter desc)
--
--				update #tmpChetIzvehePU
--				set GetIndication=isnull(dbo.fLastIndicationIDPeriodGet(@idgmeter,@idperiod),0)
--
--				update #tmpChetIzvehePU
--				set LastIndication=isnull(dbo.fLastIndicationIDPeriod(@idgmeter, @IdPeriodPred),0)
--
--				update #tmpChetIzvehePU
--				set marka=qq.marka 
--				from #tmpChetIzvehePU c inner join (select isnull(tt.name,'')+'('+isnull(ltrim(tt.ClassAccuracy),'')+')' marka, gm.idgmeter
--				from  GObject g  with (nolock) 
--				left join GMeter gm  with (nolock) on gm.IdGObject=g.IdGObject
--				left join typegmeter tt with (nolock)  on gm.idtypegmeter=tt.idtypegmeter
--				where g.IdGRU=@IdGru and g.idcontract=@idcontract) qq on qq.idgmeter=c.idgmeter
--
--				update #tmpChetIzvehePU
--				set Serialnumber=qq.Serialnumber 
--				from #tmpChetIzvehePU c inner join (select isnull(gm.Serialnumber,'б/н') Serialnumber, gm.idgmeter
--				from  GObject g  with (nolock) 
--				left join GMeter gm  with (nolock) on gm.IdGObject=g.IdGObject
--				left join typegmeter tt with (nolock)  on gm.idtypegmeter=tt.idtypegmeter
--				where g.IdGRU=@IdGru and g.idcontract=@idcontract) qq on qq.idgmeter=c.idgmeter
--
--				update #tmpChetIzvehePU
--				set dateverify=qq.dateverify 
--				from #tmpChetIzvehePU c inner join (select dateadd(yy, tt.servicelife, case when gm.dateverify='1800-01-01' then gm.datefabrication else gm.dateverify end) dateverify, gm.idgmeter
--				from  GObject g  with (nolock) 
--				left join GMeter gm  with (nolock) on gm.IdGObject=g.IdGObject
--				left join typegmeter tt with (nolock)  on gm.idtypegmeter=tt.idtypegmeter
--				where g.IdGRU=@IdGru and g.idcontract=@idcontract) qq on qq.idgmeter=c.idgmeter
--			end
--	end 

set @idgmeter=(select top 1 idgmeter from #tmpChetIzvehePU)

--if isnull(@idgmeter,0)=0 and @gmetercount<2 
--begin
	update #tmpChetIzvehePU
	set SummaNach=convert(float,qq.DA),FactUse=isnull(ff.idtypefu,1)
	from #tmpChetIzvehePU c
	inner join (select -sum(o.amountoperation) DA, f.IdContract, f.iddocument
	from Document f  with (nolock)
	inner join operation o  with (nolock) on o.iddocument=f.iddocument
	inner join balance b with (nolock) on b.idbalance=o.idbalance
	and b.idaccounting<>4
	where f.IdPeriod=@IdPeriod and f.idcontract=@IDContract
		and f.idtypedocument=5 
	group by f.IdContract,f.iddocument) qq on qq.IdContract=c.IdContract
	inner join operation o  with (nolock) on o.iddocument=qq.iddocument
	inner join factuse ff with (nolock) on o.idoperation=ff.idoperation
--end
--else
--begin
--	update #tmpChetIzvehePU
--	set SummaNach=convert(float,qq.DA),FactUse=isnull(ff.idtypefu,1)
--	from #tmpChetIzvehePU c
--	inner join (select -sum(o.amountoperation) DA, f.IdContract, f.iddocument, gm.idgmeter
--	from Document f  with (nolock)
--	inner join operation o  with (nolock) on o.iddocument=f.iddocument
--	inner join factuse ff with (nolock) on o.idoperation=ff.idoperation
--	left join GMeter gm  with (nolock) on gm.IdGObject=ff.IdGObject
--		and dbo.fGetStatusPU (@dEnd,gm.idgmeter)=1 
--	inner join balance b with (nolock) on b.idbalance=o.idbalance
--	and b.idaccounting<>4
--	where f.IdPeriod=@IdPeriod and f.idcontract=@IDContract
--		and f.idtypedocument=5 
--	group by f.IdContract,f.iddocument,gm.idgmeter) qq on qq.IdContract=c.IdContract and qq.idgmeter=c.idgmeter
--	inner join operation o  with (nolock) on o.iddocument=qq.iddocument
--end

update #tmpChetIzvehePU
set Tariff=Tariff*(select top 1 value from Tariff with (nolock) where idperiod=@idperiod),
Norma=Norma*(select top 1 value from Tariff with (nolock) where idperiod=@idperiod)

if isnull(@idgmeter,0)=0 and @gmetercount<2 
begin
	update #tmpChetIzvehePU
	set FactAmount=qq.FA
	from #tmpChetIzvehePU c
	inner join (select sum(f.Factamount) fA, g.IdContract
	from Factuse f  with (nolock)
	inner join Gobject g  with (nolock) on g.idgobject=f.idgobject
	inner join operation o with (nolock) on o.idoperation=f.idoperation
	inner join document d  with (nolock) on d.iddocument=o.iddocument
	and f.IdPeriod=@IdPeriod and d.idtypedocument=5
		--and g.IdGRU=@IdGru 
	group by g.IdContract) qq on qq.IdContract=c.IdContract
end 
	else
begin
	update #tmpChetIzvehePU
	set FactAmount=qq.FA
	from #tmpChetIzvehePU c
	inner join (select sum(f.Factamount) fA, g.IdContract, gm.idgmeter
	from Factuse f  with (nolock)
	inner join Gobject g  with (nolock) on g.idgobject=f.idgobject
	left join GMeter gm  with (nolock) on gm.IdGObject=g.IdGObject
		--and dbo.fGetStatusPU (@dEnd,gm.idgmeter)=1 
	inner join operation o with (nolock) on o.idoperation=f.idoperation
	inner join document d  with (nolock) on d.iddocument=o.iddocument
	and f.IdPeriod=@IdPeriod and d.idtypedocument=5 and g.IdContract=@IDContract
	group by g.IdContract,gm.idgmeter) qq on qq.IdContract=c.IdContract and qq.idgmeter=c.idgmeter
end 





update #tmpChetIzvehePU
set DateGetIndication=(select top 1 convert(datetime,qq.datedisplay,20) from  indication qq  with (nolock) 
where qq.display=c.GetIndication and c.idgmeter=qq.idgmeter order by datedisplay desc)
from #tmpChetIzvehePU c

update #tmpChetIzvehePU
set TypeCloseGmeter=(
select top 1 pd.value from document d 
inner join pd on pd.iddocument=d.iddocument
and idtypepd=33 and d.idcontract=c.idcontract and d.idcontract=@IDContract
order by d.documentdate desc) from #tmpChetIzvehePU c where c.statusGmeter=2

update #tmpChetIzvehePU
set TypeCloseGmeter=999 from #tmpChetIzvehePU
inner join (select idcontract from gobject where idstatusgobject=2 and idcontract=@IDContract) obj on obj.idcontract=#tmpChetIzvehePU.idcontract


update #tmpChetIzvehePU
set TypeCloseGmeter=1
where TypeCloseGmeter is null

update #tmpChetIzvehePU
set dategetindication=null where getindication=0

update #tmpChetIzvehePU
set DateLastIndication=(select top 1 convert(datetime,qq.datedisplay,20) from  indication qq  with (nolock) 
where qq.display=c.LastIndication and c.idgmeter=qq.idgmeter and  qq.datedisplay<isnull(c.DateGetIndication,GetDate()) order by datedisplay desc)
from #tmpChetIzvehePU c


--select case when t.getindication=0 then 'Норма б-ша/по норме:' else 'ЕҚ б-ша/по ПУ:' end as info, t.cl,t.norma,t.factuse,t.factamount,t.tariff,t.summanach,
--t.lastindication, t.datelastindication, t.getindication, t.dategetindication, t.marka, t.serialnumber,t.dateverify, t.TypeCloseGmeter,isnull(t.idgmeter,0) idgmeter
--from #tmpChetIzvehePU t
--
--drop table #tmpChetIzvehePU
select ac.idaccounting, ac.nname, @IDContract idcontract,
isnull( dbo.fGetLastBalance(@IdPeriodPred, @IDContract, ac.idaccounting),0) as BalanceBegin,
convert(float, 0.00) as SummOplat,
convert(float, 0.00) as SummaNach,
isnull( dbo.fGetLastBalance(@IdPeriod, @IDContract, ac.idaccounting),0) as BalanceEnd
into #tmpAcc
from accounting ac
--inner join balance b with (nolock) on b.idaccounting
--i Document f  with (nolock) 
--inner join operation o with (nolock) on f.iddocument=o.iddocument
--and f.IdPeriod=@IdPeriod and f.idtypedocument=1 
--and idaccounting=6

--insert into #tmpChetIzvehePU (info)

update #tmpAcc
set SummOplat=convert(float,qq.AO)
from #tmpAcc c
inner join (select sum(o.amountoperation) AO, b.idaccounting
from Document f  with (nolock) 
inner join operation o with (nolock) on f.iddocument=o.iddocument
and f.IdPeriod=@IdPeriod and f.idtypedocument=1 
inner join balance b with (nolock) on o.idbalance=b.idbalance
where f.idcontract = @IDContract
group by b.idaccounting) qq on qq.idaccounting=c.idaccounting

update #tmpAcc
set SummaNach=convert(float,qq.AO)*-1
from #tmpAcc c
inner join (select sum(o.amountoperation) AO, b.idaccounting
from Document f  with (nolock) 
inner join operation o with (nolock) on f.iddocument=o.iddocument
and f.IdPeriod=@IdPeriod and f.idtypedocument<>1
inner join balance b with (nolock) on o.idbalance=b.idbalance
where f.idcontract = @IDContract
group by b.idaccounting) qq on qq.idaccounting=c.idaccounting


select case when t.getindication=0 then 'Норма б-ша/по норме:' else 'ЕҚ б-ша/по ПУ:' end as info, t.cl,t.norma,t.factuse,t.factamount,
t.tariff,
t.balancebegin, t.summoplat, t.summanach, t.balanceend,
t.lastindication, t.datelastindication, t.getindication, t.dategetindication, t.marka, t.serialnumber,t.dateverify, t.TypeCloseGmeter,isnull(t.idgmeter,0) idgmeter
from #tmpChetIzvehePU t

union all
select nname as info,0 as cl,0 as norma,99 as factuse,0 as factamount,0 as tariff,balancebegin,summoplat,summanach,balanceend,
0 as lastindication,'' as datelastindication,0 as getindication,'' as dategetindication,'' as marka,'' as serialnumber,'' as dateverify,
0 as TypeCloseGmeter,0 as idgmeter
from #tmpAcc
--where  (balancebegin<>0 or summoplat<>0 or summanach<>0 or balanceend<>0)
where idaccounting=1

union all
select nname as info,0 as cl,0 as norma,99 as factuse,0 as factamount,0 as tariff,balancebegin,summoplat,summanach,balanceend,
0 as lastindication,'' as datelastindication,0 as getindication,'' as dategetindication,'' as marka,'' as serialnumber,'' as dateverify,
0 as TypeCloseGmeter,0 as idgmeter
from #tmpAcc
--where  (balancebegin<>0 or summoplat<>0 or summanach<>0 or balanceend<>0)
where idaccounting=4

union all
select nname as info,0 as cl,0 as norma,99 as factuse,0 as factamount,0 as tariff,balancebegin,summoplat,summanach,balanceend,
0 as lastindication,'' as datelastindication,0 as getindication,'' as dategetindication,'' as marka,'' as serialnumber,'' as dateverify,
0 as TypeCloseGmeter,0 as idgmeter
from #tmpAcc
--where  (balancebegin<>0 or summoplat<>0 or summanach<>0 or balanceend<>0)
where idaccounting not in (1,4,5) --5-штраф

union all
select 'ИТОГО' as info,0 as cl,0 as norma,99 as factuse,0 as factamount,0 as tariff,sum(balancebegin) as balancebegin,
sum(summoplat) as summoplat,sum(summanach) as summanach,sum(balanceend) as balanceend,
0 as lastindication,'' as datelastindication,0 as getindication,'' as dategetindication,'' as marka,'' as serialnumber,'' as dateverify,
0 as TypeCloseGmeter,0 as idgmeter
from #tmpAcc
--where  balancebegin<>0 or summoplat<>0 or summanach<>0 or balanceend<>0
--where idaccounting<>5

--union all 
--select 'Төлемнің  сомасы/ Сумма платежа' as info,0 as cl,0 as norma,99 as factuse,0 as factamount,0 as tariff,0 as balancebegin,
--0 as summoplat,0 as summanach,0 as balanceend,
--0 as lastindication,'' as datelastindication,0 as getindication,'' as dategetindication,'' as marka,'' as serialnumber,'' as dateverify,
--0 as TypeCloseGmeter,0 as idgmeter

--select * from #tmpAcc

drop table #tmpChetIzvehePU
drop table #tmpAcc

end











GO