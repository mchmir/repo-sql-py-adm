SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
CREATE procedure [dbo].[repCountNoticeERCv2] (@IdGRU int, @IdPeriod int, @IDContract int) as 


if @IdPeriod=122
begin
exec dbo.repGMeterForCountNoticeNewTariff @IdGRU, @IdPeriod, @IDContract
end
ELSE
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
convert(float, 0.00) as BalanceBegin,
convert(float, 0.00) as SummOplat,
convert(float, 0.00) as SummaNach,
convert(float, 0.00) as BalanceEnd,
convert(float,0.00) as FactAmount,
convert(float, dbo.fGetLastPGValue(@IDPeriod, 1, @idgru)) as Tariff,
convert(float, dbo.fGetLastPGNorma(@IDPeriod, 1, @idgru)) as Norma,
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
	and g.IdGRU=@IdGru 	
left join GMeter gm  with (nolock) on gm.IdGObject=g.IdGObject
	and dbo.fGetStatusPU (@dEnd,gm.idgmeter)=1 
left join typegmeter tt with (nolock)  on gm.idtypegmeter=tt.idtypegmeter
left join Document d  with (nolock) on d.idcontract=c.idcontract
and  d.IdPeriod=@IdPeriod and (d.idtypedocument=1  or d.idtypedocument=3)

where c.idcontract=@IDContract
 group by c.IdContract, c.Account, g.IDGobject, 
 gm.IdGMeter,  tt.name, tt.ClassAccuracy,
 gm.Serialnumber,tt.servicelife,gm.dateverify,gm.DateFabrication
 

set @idgmeter=(select top 1 idgmeter from #tmpChetIzvehePU)
set @gmetercount=(select count(idgmeter) from #tmpChetIzvehePU)
set @idgmeter=(select top 1 idgmeter from #tmpChetIzvehePU)

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

select ac.idaccounting, ac.nname, @IDContract idcontract,
isnull( dbo.fGetLastBalance(@IdPeriodPred, @IDContract, ac.idaccounting),0) as BalanceBegin,
convert(float, 0.00) as SummOplat,
convert(float, 0.00) as SummaNach,
isnull( dbo.fGetLastBalance(@IdPeriod, @IDContract, ac.idaccounting),0) as BalanceEnd
into #tmpAcc
from accounting ac

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



--#############################################################################
SELECT 
  cip.Account,
  CASE 
  WHEN cip.LastIndication>0 AND ISNULL(cip.GetIndication,0) = 0 THEN 'По норме:'
  WHEN ISNULL(cip.LastIndication,0)=0 AND ISNULL(cip.GetIndication,0)=0 THEN 'По норме:'
  ELSE  'ПУ:№'+ CAST(cip.Serialnumber AS VARCHAR) + ','+ CAST(DAY(cip.dateverify) AS VARCHAR)+'.'+CAST(Month(cip.dateverify) AS VARCHAR)+'.'+CAST(YEAR(cip.dateverify) AS VARCHAR)
  END AS PersUstr,
  ------------
   convert(float, 0.00),--(SELECT ROUND(a.BalanceBegin,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 1) AS SaldoNachGaz,
   convert(float, 0.00),--(SELECT ROUND(a.BalanceBegin,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 4) AS SaldoNachPenya,
   convert(float, 0.00),--(SELECT ROUND(a.BalanceBegin,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 2) AS SaldoNachSudVzysk,
   convert(float, 0.00),--(SELECT ROUND(a.BalanceBegin,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 3) AS SaldoNachSudIzder,
   convert(float, 0.00) AS SaldoNachSHtraf, --(SELECT ROUND(a.BalanceBegin,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 5) AS SaldoNachSHtraf,
   convert(float, 0.00),--(SELECT ROUND(a.BalanceBegin,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 6) AS SaldoNachDopUslug,
   convert(float, 0.00),--(SELECT ROUND(SUM(a.BalanceBegin),2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract ) AS SaldoNachItog,
  --------
   convert(float, 0.00),--(SELECT ROUND(a.Summoplat ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 1) AS OplachenoGaz,
   convert(float, 0.00),--(SELECT ROUND(a.Summoplat ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 4) AS OplachenoPenya,
   convert(float, 0.00),--(SELECT ROUND(a.Summoplat ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 2) AS OplachenoSudVzysk,
   convert(float, 0.00),--(SELECT ROUND(a.Summoplat ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 3) AS OplachenoSudIzder,
   convert(float, 0.00) AS OplachenoSHtraf, --(SELECT ROUND(a.Summoplat ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 5) AS OplachenoSHtraf,
   convert(float, 0.00),--(SELECT ROUND(a.Summoplat ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 6) AS OplachenoDopUslug,
   convert(float, 0.00),--(SELECT ROUND(SUM(a.Summoplat),2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract ) AS OplachenoItog,
  --------
  ROUND(cip.LastIndication,2) AS PredPokaz,
  ROUND(cip.GetIndication, 2) AS TekPokaz,
  CASE 
  WHEN cip.LastIndication>0 AND ISNULL(cip.GetIndication,0) = 0 THEN ROUND(cip.Norma,2)
  WHEN ISNULL(cip.LastIndication,0)=0 AND ISNULL(cip.GetIndication,0)=0 THEN ROUND(cip.Norma,2)
  ELSE ROUND(cip.Tariff, 2)
  END AS Cena,
  ----------
  CASE 
  WHEN cip.LastIndication>0 AND ISNULL(cip.GetIndication,0) = 0 THEN CAST(cip.CL AS CHAR(2))+' чел.'
  WHEN ISNULL(cip.LastIndication,0)=0 AND ISNULL(cip.GetIndication,0)=0 THEN CAST(cip.CL AS CHAR(2))+' чел.'
  ELSE REPLACE(CAST(cip.FactAmount AS VARCHAR) + ' m3','.',',')
  END AS Kolvo,
  ----------
  ROUND(cip.SummaNach, 2) AS SummaNach,
  ----------
   convert(float, 0.00),--(SELECT ROUND(a.SummaNach ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 1) AS SummNachislGaz,
   convert(float, 0.00),--(SELECT ROUND(a.SummaNach ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 4) AS SummNachislPenya,
   convert(float, 0.00),--(SELECT ROUND(a.SummaNach ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 2) AS SummNachislVzysk,
   convert(float, 0.00),--(SELECT ROUND(a.SummaNach ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 3) AS SummNachislIzder,
  convert(float, 0.00) AS SummNachislSHtraf,--(SELECT ROUND(a.SummaNach ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 5) AS SummNachislSHtraf,
   convert(float, 0.00),--(SELECT ROUND(a.SummaNach ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 6) AS SummNachislDopUslug,
   convert(float, 0.00),--(SELECT ROUND(SUM(a.SummaNach),2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract ) AS SummNachislItog,
  ----------
   convert(float, 0.00),--(SELECT ROUND(a.BalanceEnd,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 1) AS SaldoKonecGaz,
   convert(float, 0.00),--(SELECT ROUND(a.BalanceEnd,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 4) AS SaldoKonecPenya,
   convert(float, 0.00),--(SELECT ROUND(a.BalanceEnd,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 2) AS SaldoKonecVzysk,
   convert(float, 0.00),--(SELECT ROUND(a.BalanceEnd,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 3) AS SaldoKonecSudIzder,
   convert(float, 0.00) AS SaldoKonecSHtraf,--(SELECT ROUND(a.BalanceEnd,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 5) AS SaldoKonecSHtraf,
   convert(float, 0.00),--(SELECT ROUND(a.BalanceEnd,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 6) AS SaldoKonecDopUslug,
   convert(float, 0.00),--(SELECT ROUND(SUM(a.BalanceEnd),2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract ) AS SaldoKonecItog,
  -----
  CASE 
  WHEN cip.dateverify < GetDATE() THEN 'Срочно сдайте ПУ на поверку!'
  WHEN cip.TypeCloseGmeter = 2 THEN  'Срочно сдайте ПУ на поверку!'
  WHEN cip.TypeCloseGmeter = 14 THEN 'ПУ не опломбирован!'
  ELSE ''
  END AS Primechanie

  FROM #tmpChetIzvehePU cip
  left join (select idcontract from gobject  with (nolock)  where idstatusgobject=2 and idgru=@idgru and isnull( dbo.fGetLastBalance (@IdPeriod, IdContract, 0),0)>-1) c on c.idcontract=cip.idcontract
  where c.idcontract is null



--#############################################################################


drop table #tmpChetIzvehePU
drop table #tmpAcc
--drop table #tmpAcc2

end


GO