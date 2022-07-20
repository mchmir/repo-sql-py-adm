SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO







--exec repGMeterForCountNotice 8497,107,866693


CREATE procedure [dbo].[repGMeterForCountNoticeNewTariff] (@IdGRU int, @IdPeriod int, @IDContract int) as 
---------------*************Инфо по ПУ для Счет-извещение**********------------------
--declare @IdGRU int
--declare @IdPeriod int
--declare @IDContract int
--set @IdPeriod=97
--set @IdGru=8577
--set @IDContract=917614

declare @IdPeriodPred int
select top 1 @IdPeriodPred=dbo.fGetPredPeriodVariable(@IdPeriod)
declare @dBegin datetime
declare @dEnd datetime
declare @IDGmeter int
declare @GmeterCount int
set @dBegin= dbo.fGetDatePeriod(@IdPeriod,1)
set @dEnd= dbo.fGetDatePeriod(@IdPeriod,0)
declare @FactAmount float
declare @FactAmountNewTariff float
declare @SummaNach float
declare @SummaNachNewTariff float
declare @IDTypeFU int
declare @IDTypeFUNewTariff int



select @IdGRU IdGRU,@IdPeriod IdPeriod,c.Account, c.IdContract, gm.idgmeter, dbo.fGetStatusPU (@dEnd,gm.idgmeter) StatusGMeter,
dbo.fGetCountLives(g.idgobject ,@IdPeriod) CL,
--isnull( dbo.fGetLastBalance(@IdPeriodPred, c.IdContract, 0),0) BalanceNaNach, isnull( dbo.fGetLastBalance (@IdPeriod, c.IdContract, 0),0) BalanceNaKonec, 
convert(float, 0.00) as SummaNach,
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
convert(int,1) as TypeCloseGmeter, convert(int,2) as TypeTariff

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

set @idgmeter=(select top 1 idgmeter from #tmpChetIzvehePU)
--надо проверить, было ли начисление по новому тарифу
declare @BeNewTariff int
set @BeNewTariff=0
select @BeNewTariff=count(d.iddocument) from document d
inner join pd on d.iddocument=pd.iddocument and pd.idtypepd=36
where d.idperiod=@IdPeriod and d.idcontract=@IDContract
		and d.idtypedocument=5 and pd.value='1'
if @BeNewTariff>0 
begin
--если было надо добавить еще строку(и) и посчитать по разным тарифам
	insert into #tmpChetIzvehePU (IdGRU, IdPeriod, Account, IdContract, idgmeter, StatusGMeter, CL, SummaNach, FactAmount, Tariff, Norma, 
	GetIndication, DateGetIndication, LastIndication,DateLastIndication,Marka,Serialnumber,FactUse,Date,dateverify,TypeCloseGmeter,TypeTariff)
	select IdGRU, IdPeriod, Account, IdContract, idgmeter, StatusGMeter, CL, SummaNach, FactAmount, Tariff, Norma, 
	GetIndication, DateGetIndication, LastIndication,DateLastIndication,Marka,Serialnumber,FactUse,Date,dateverify,TypeCloseGmeter,1
	from #tmpChetIzvehePU
	
	select @SummaNach=convert(float,qq.DA)
	from #tmpChetIzvehePU c
	inner join (select -sum(o.amountoperation) DA, f.IdContract--, f.iddocument
	from Document f  with (nolock)
	inner join pd on f.iddocument=pd.iddocument and pd.idtypepd=36
	inner join operation o  with (nolock) on o.iddocument=f.iddocument
	inner join balance b with (nolock) on b.idbalance=o.idbalance
	and b.idaccounting<>4
	where f.IdPeriod=@IdPeriod and f.idcontract=@IDContract
		and f.idtypedocument=5 and pd.value='2'
	group by f.IdContract) qq on qq.IdContract=c.IdContract
--	inner join operation o  with (nolock) on o.iddocument=qq.iddocument
--	inner join factuse ff with (nolock) on o.idoperation=ff.idoperation

	select  @IDTypeFU=isnull(ff.idtypefu,1)
	from #tmpChetIzvehePU c
	inner join (select -sum(o.amountoperation) DA, f.IdContract, f.iddocument
	from Document f  with (nolock)
	inner join pd on f.iddocument=pd.iddocument and pd.idtypepd=36
	inner join operation o  with (nolock) on o.iddocument=f.iddocument
	inner join balance b with (nolock) on b.idbalance=o.idbalance
	and b.idaccounting<>4
	where f.IdPeriod=@IdPeriod and f.idcontract=@IDContract
		and f.idtypedocument=5 and pd.value='2'
	group by f.IdContract,f.iddocument) qq on qq.IdContract=c.IdContract
	inner join operation o  with (nolock) on o.iddocument=qq.iddocument
	inner join factuse ff with (nolock) on o.idoperation=ff.idoperation

	select @SummaNachNewTariff=convert(float,qq.DA)
	from #tmpChetIzvehePU c
	inner join (select -sum(o.amountoperation) DA, f.IdContract
	from Document f  with (nolock)
	inner join pd on f.iddocument=pd.iddocument and pd.idtypepd=36
	inner join operation o  with (nolock) on o.iddocument=f.iddocument
	inner join balance b with (nolock) on b.idbalance=o.idbalance
	and b.idaccounting<>4
	where f.IdPeriod=@IdPeriod and f.idcontract=@IDContract
		and f.idtypedocument=5 and pd.value='1'
	group by f.IdContract) qq on qq.IdContract=c.IdContract

	select @IDTypeFUNewTariff=isnull(ff.idtypefu,1)
	from #tmpChetIzvehePU c
	inner join (select f.IdContract, f.iddocument
	from Document f  with (nolock)
	inner join pd on f.iddocument=pd.iddocument and pd.idtypepd=36
	inner join operation o  with (nolock) on o.iddocument=f.iddocument
	inner join balance b with (nolock) on b.idbalance=o.idbalance
	and b.idaccounting<>4
	where f.IdPeriod=@IdPeriod and f.idcontract=@IDContract
		and f.idtypedocument=5 and pd.value='1'
	group by f.IdContract,f.iddocument) qq on qq.IdContract=c.IdContract
	inner join operation o  with (nolock) on o.iddocument=qq.iddocument
	inner join factuse ff with (nolock) on o.idoperation=ff.idoperation

	update #tmpChetIzvehePU
	--set SummaNach=@SummaNach-@SummaNachNewTariff, 
	set FactUse=@IDTypeFU where TypeTariff=2
	update #tmpChetIzvehePU
	set SummaNach=@SummaNach where TypeTariff=2
	update #tmpChetIzvehePU
	set SummaNach=@SummaNachNewTariff, FactUse=@IDTypeFUNewTariff where TypeTariff=1

	update #tmpChetIzvehePU
	set Tariff=Tariff*(select top 1 value from Tariff with (nolock) where idperiod=@idperiod),
	Norma=Norma*(select top 1 value from Tariff with (nolock) where idperiod=@idperiod) where TypeTariff=2

	update #tmpChetIzvehePU
	set Tariff=Tariff*(select top 1 value from Tariff with (nolock) where idperiod=@idperiod+1),
	Norma=Norma*(select top 1 value from Tariff with (nolock) where idperiod=@idperiod+1) where TypeTariff=1

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
		inner join pd on d.iddocument=pd.iddocument and pd.idtypepd=36 and pd.value='2'
		group by g.IdContract) qq on qq.IdContract=c.IdContract where TypeTariff=2

		update #tmpChetIzvehePU
		set FactAmount=isnull(qq.FA,0)
		from #tmpChetIzvehePU c
		inner join (select sum(f.Factamount) fA, g.IdContract
		from Factuse f  with (nolock)
		inner join Gobject g  with (nolock) on g.idgobject=f.idgobject
		inner join operation o with (nolock) on o.idoperation=f.idoperation
		inner join document d  with (nolock) on d.iddocument=o.iddocument
		and f.IdPeriod=@IdPeriod and d.idtypedocument=5
		inner join pd on d.iddocument=pd.iddocument and pd.idtypepd=36 and pd.value='1'
		group by g.IdContract) qq on qq.IdContract=c.IdContract where TypeTariff=1
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
		inner join pd on d.iddocument=pd.iddocument and pd.idtypepd=36 and pd.value='2'
		group by g.IdContract,gm.idgmeter) qq on qq.IdContract=c.IdContract and qq.idgmeter=c.idgmeter and TypeTariff=2

		update #tmpChetIzvehePU
		set FactAmount=isnull(qq.FA,0)
		from #tmpChetIzvehePU c
		inner join (select sum(f.Factamount) fA, g.IdContract, gm.idgmeter
		from Factuse f  with (nolock)
		inner join Gobject g  with (nolock) on g.idgobject=f.idgobject
		left join GMeter gm  with (nolock) on gm.IdGObject=g.IdGObject
			--and dbo.fGetStatusPU (@dEnd,gm.idgmeter)=1 
		inner join operation o with (nolock) on o.idoperation=f.idoperation
		inner join document d  with (nolock) on d.iddocument=o.iddocument
		and f.IdPeriod=@IdPeriod and d.idtypedocument=5 and g.IdContract=@IDContract
		inner join pd on d.iddocument=pd.iddocument and pd.idtypepd=36 and pd.value='1'
		group by g.IdContract,gm.idgmeter) qq on qq.IdContract=c.IdContract and qq.idgmeter=c.idgmeter and TypeTariff=1
	end 
end 
else
begin
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


select case when t.getindication=0 then 'Норма б-ша/по норме:' else 'ЕҚ б-ша/по ПУ:' end as info, t.cl,t.norma,t.factuse,t.factamount,t.tariff,t.summanach,
t.lastindication, t.datelastindication, t.getindication, t.dategetindication, t.marka, t.serialnumber,t.dateverify, t.TypeCloseGmeter,isnull(t.idgmeter,0) idgmeter
from #tmpChetIzvehePU t

drop table #tmpChetIzvehePU














GO