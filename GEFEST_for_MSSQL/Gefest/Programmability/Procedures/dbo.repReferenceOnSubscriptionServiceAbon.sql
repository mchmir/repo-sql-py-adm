SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO










CREATE PROCEDURE [dbo].[repReferenceOnSubscriptionServiceAbon] (@idPeriod int) AS
---------------**************Справка по абонентской службе*********-------------------
SET NOCOUNT ON --with(index(

--declare @idPeriod int
declare @date datetime

declare @idPeriodPrev int
declare @DateT datetime
declare @month int
declare @year int 
declare @day int

declare @IDDispatcher int
set @IDDispatcher=113 --sberbank

--set @idPeriod=69--dbo.GetNowPeriod()
set @date=dbo.fGetDatePeriod(@IdPeriod,0)

set @DateT=dbo.DateOnly(@Date)
set @idPeriodPrev=dbo.fGetPredPeriodVariable(@idPeriod)

select c.idcontract , convert(float, 0) DA,convert(float, 0) DAUS, dbo.fGetLastBalanceReal(@idPeriodPrev, c.IdContract, 0) AmountBalance,
	dbo.fGetLastBalanceReal(@idPeriod, c.IdContract, 0) AmountBalanceEnd,
	convert(float, 0) DAP, convert(float ,0) ForSaldo, 
	convert(float, 0) mDAP,convert(float, 0) mDAPUS,convert(float, 0) DAPUS,	convert(float ,0) mForSaldo,
	g.IdStatusGObject, isnull(gm.idstatusGMeter,0) idstatusGMeter
	, convert(float , null) FactUse, convert(float , null) PrevFactUse,c.Status,convert(float, 0) DAPPred
into #tmpForSaldo
from contract c  with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and g.idcontract=916555
and g.idgobject<>916602
left join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
	and gm.idstatusgmeter=1
group by c.idcontract, g.IdStatusGObject,c.Status,idstatusGMeter

update #tmpForSaldo
set DA=qq.DocumentAmount
from #tmpForSaldo t 
inner join (select d.idcontract, sum(d.DocumentAmount) DocumentAmount
from document d with (nolock) 
--inner join operation o with (nolock)  on o.iddocument=d.iddocument
where d.idtypedocument=1 and dbo.DateOnly(d.documentdate)=@DateT 
--inner join balance b with (nolock) on b.idbalance=o.idbalance
--and idaccounting<>6 
group by d.idcontract) qq on qq.idcontract=t.idcontract


update #tmpForSaldo
set DAP=qq.DocumentAmount
from #tmpForSaldo t 
inner join (select dc.idcontract, sum(dc.DocumentAmount) DocumentAmount
from document dc with (nolock)
--inner join operation o with (nolock)  on o.iddocument=dc.iddocument
where	dc.idtypedocument=1 and dc.idperiod=@idPeriod
	and dbo.dateonly(dc.DocumentDate)<@DateT
--inner join balance b with (nolock) on b.idbalance=o.idbalance
--and idaccounting<>6 
group by dc.idcontract) qq on qq.idcontract=t.idcontract


update #tmpForSaldo
set DAPUS=qq.DocumentAmount
from #tmpForSaldo t 
inner join (select dc.idcontract, sum(o.amountoperation) DocumentAmount
from document dc with (nolock)
inner join operation o with (nolock)  on o.iddocument=dc.iddocument
and	dc.idtypedocument=1 and dc.idperiod=@idPeriod
	and dbo.dateonly(dc.DocumentDate)<@DateT
inner join balance b with (nolock) on b.idbalance=o.idbalance
and idaccounting=6 
group by dc.idcontract) qq on qq.idcontract=t.idcontract

update #tmpForSaldo
set DAPPred=qq.DocumentAmount
from #tmpForSaldo t 
inner join (select dc.idcontract, sum(dc.DocumentAmount) DocumentAmount
from document dc with (nolock)
--inner join operation o with (nolock)  on o.iddocument=dc.iddocument
where	dc.idtypedocument=1 and dc.idperiod=@idPeriodPrev
--inner join balance b with (nolock) on b.idbalance=o.idbalance
--and idaccounting<>6 
group by dc.idcontract) qq on qq.idcontract=t.idcontract

update #tmpForSaldo
set ForSaldo=case when AmountBalance+DAP<0 then 
	case when DA>-(AmountBalance+DAP) then -(AmountBalance+DAP) else DA end
	else 0 end
from #tmpForSaldo

update #tmpForSaldo
set mDAP=isnull(DAP,0)+isnull(DA,0)
from #tmpForSaldo

update #tmpForSaldo
set mDAPUS=isnull(DAPUS,0)+isnull(DAUS,0)
from #tmpForSaldo

update #tmpForSaldo
set mForSaldo=case when AmountBalance<0 then 
	case when mDAP>-(AmountBalance) then -(AmountBalance) else mDAP end
	else 0 end
from #tmpForSaldo

set @DateT=@DateT+1

update #tmpForSaldo 
set FactUse=fa
from  #tmpForSaldo t 
inner join (select g.idcontract, sum(f.factamount) fa
from gobject g with (nolock) 
inner join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
inner join indication i  with (nolock)  on gm.idgmeter=i.idgmeter
	and i.datedisplay<@DateT
inner join factuse f with (nolock) on f.idindication=i.idindication
	and f.idperiod=@idPeriod 
left join operation o with (nolock) on o.idoperation=f.idoperation
left join document d with (nolock) on o.iddocument=d.iddocument
	and d.idtypedocument=7 
where isnull(d.iddocument,0)=0
group by g.idcontract) qq on t.idcontract=qq.idcontract

update #tmpForSaldo 
set PrevFactUse=fa
from  #tmpForSaldo t 
inner join (select g.IdContract, sum(f.factamount) fa
from gobject g with (nolock)  
inner join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
inner join indication i with (nolock)  on gm.idgmeter=i.idgmeter
	and i.datedisplay<@DateT
inner join factuse f with (nolock) on f.idindication=i.idindication
	and f.idperiod=@idPeriodPrev
group by g.idcontract) qq on t.idcontract=qq.idcontract

set @DateT=@DateT-1

select   convert(float ,0) DA,convert(float ,0) DAUS, convert(float, 0) FS, convert(float ,0) mDA, convert(float ,0) mDAUS,convert(float, 0) mFS, convert(float, 0) 
	SumIn,convert(float ,0) RazIn, convert(float ,0) BDMI,  convert(float ,0) Cub, convert(float, 0)BBeg , convert(float, 0)BBegMin, 
	convert(float ,0) mKassa,convert(float ,0) mKassaUS, convert(float ,0) mBank,convert(float ,0) mBankUS--, convert(float ,0) mNarBankPKO, convert(float ,0) mNarBankB,
	-- convert(float ,0) mKazPost, convert(float ,0) mTuranAlem, 
,convert(float, 0)BEnd , convert(float, 0)BEndMin, convert(float, 0)NetPotrebl
	,convert(float, 0)M3KN, convert(float, 0)KGKN,convert(float, 0)CountContract,convert(float, 0)CountOplat,convert(float, 0)NetOplat,convert(float, 0)NetOplatBol,
convert(float, 0) UslugiOPl,convert(float, 0) GazOpl,convert(float, 0) PoshOpl,
convert(float, 0) PenyOpl,convert(float, 0) NachUslugi,convert(float, 0) NachGaz,convert(float, 0) NachPeny,convert(float, 0) NachPosh,convert(float, 0) OplSever,convert(float, 0) OplNarodBank,convert(float, 0) OplCesna,convert(float, 0) OplNarodBankomat,convert(float, 0) OplKazPochta,convert(float, 0) OplTuran,
convert(float, 0) PoPU,convert(float, 0) PosredMoh,convert(float, 0) korek, convert(float, 0) SumNotIn
	--,convert(float, 0)NetOplatDolg,convert(float, 0)NetOplatDolgPred
into #tmpForanAnalitik

update #tmpForanAnalitik 
set DA=df.DA,DAUS=df.DAUS, FS=df.FS, mDA=df.mDA,mDAUS=df.mDAUS, mFS=df.mFS
from (select SUM(DA) DA, SUM(DAUS) DAUS,SUM(ForSaldo) FS, sum(mDAP) mDA, sum(mDAPUS) mDAUS,SUM(mForSaldo) mFS from #tmpForSaldo) df

update #tmpForanAnalitik 
set SumIn=re.SumIn
from(select  isnull(count(c.idcontract),0) SumIn
from #tmpForSaldo c
--from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
	and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
	and dbo.fGetStatusPU (@date,gm.idgmeter)=1) re

--re.SumIn
--from(select  count(c.idcontract) SumIn
--from #tmpForSaldo c
--where c.idstatusgobject=1 and c.idstatusgmeter=1) re

update #tmpForanAnalitik 
set SumNotIn=re.SumIn-#tmpForanAnalitik.SumIn
from(select  isnull(count(c.idcontract),0) SumIn
from #tmpForSaldo c
--from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
where	dbo.fGetStatusGobject (g.idgobject,@idperiod)=1 ) re
--re.SumNotIn
--from(select  count(c.idcontract) SumNotIn
--from #tmpForSaldo c
--where c.idstatusgobject=1 and c.idstatusgmeter=0) re

update #tmpForanAnalitik 
set CountOplat=re.SumIn
from(select  count(c.idcontract) SumIn
from #tmpForSaldo c
where mDAP>0 and isnull(status,0)<>2) re

update #tmpForanAnalitik 
set Korek=isnull(-gt.ff,0)
from (
select  sum(documentamount)ff from document d with (nolock) 
inner join pd with (nolock)  on pd.iddocument=d.iddocument
and idtypepd=13  and idtypedocument=7 and d.idperiod=@idPeriod 
inner join factuse f with (nolock)  on f.iddocument=d.iddocument
and (f.idtypefu=2 or f.idtypefu=1)
) gt

update #tmpForanAnalitik 
set NetOplat=re.SumIn
from(select  count(c.idcontract) SumIn
from #tmpForSaldo c
where mDAP=0 and isnull(status,0)<>2 and AmountBalance<>0) re

update #tmpForanAnalitik 
set NetOplatBol=re.SumIn
from(select  count(c.idcontract) SumIn
from #tmpForSaldo c
where isnull(status,0)<>2 and mDAP=0 and DAPPred=0  and AmountBalance<>0) re

update #tmpForanAnalitik 
set CountContract=re.SumIn
from(select  count(c.idcontract) SumIn
from #tmpForSaldo c
where isnull(status,0)<>2) re

-- update #tmpForanAnalitik 
-- set NetOplatDolg=re.SumIn
-- from(select  count(c.idcontract) SumIn
-- from #tmpForSaldo c
-- where mDAP=0 and isnull(status,0)<>2 and AmountBalanceEnd<0) re
-- 
-- update #tmpForanAnalitik 
-- set NetOplatDolgPred=re.SumIn
-- from(select  count(c.idcontract) SumIn
-- from #tmpForSaldo c
-- where mDAP=0 and isnull(status,0)<>2 and AmountBalanceEnd<0 and AmountBalance<0 and DapPred=0 ) re

update #tmpForanAnalitik 
set NetPotrebl=re.summa
from (select count(idcontract) Summa 
from #tmpForSaldo c 
where c.idstatusgobject=1 and c.idstatusgmeter=1 and c.factuse=0 and c.factuse is not null)re

update #tmpForanAnalitik 
set M3KN=isnull(cubes,0)
from (select sum(f.FactAmount) cubes
from FactUse f with (nolock) 
inner join document d  with (nolock) on d.iddocument=f.iddocument
	and d.idtypedocument=7
	and f.IdPeriod=@idperiod
	and f.IdTypeFU=1) qq

update #tmpForanAnalitik 
set KGKN=isnull(cubes,0)
from (select sum(f.FactAmount) cubes
from FactUse f with (nolock) 
inner join document d  with (nolock) on d.iddocument=f.iddocument
	and d.idtypedocument=7
	and f.IdPeriod=@idperiod
	and f.IdTypeFU=2) qq

update #tmpForanAnalitik 
set RazIn=re.ff
from (select count(idcontract) ff from (select distinct g.idcontract
from gobject g with (nolock)  
inner join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
inner join indication i  with (nolock)  on gm.idgmeter=i.idgmeter
inner join factuse f with (nolock) on f.idindication=i.idindication
	and f.idperiod=@idPeriod)qq) re

update #tmpForanAnalitik 
set BDMI=re.SumIn
from(select count(c.idcontract) SumIn 
from #tmpForSaldo c where FactUse is not null or PrevFactUse is not null) re

update #tmpForanAnalitik
set Cub=re.SumIn
from(select isnull(sum(FactUse),0) SumIn from #tmpForSaldo c where FactUse is not null) re

update #tmpForanAnalitik
set BBeg=re.AA
from(select sum(AB) AA from
(select sum(AmountBalance) AB
from balancereal b
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod= @idPeriodPrev
Group by c.IdContract) qq  where AB>0) re

update #tmpForanAnalitik
set BBegMin=-re.AA
from(select sum(AB) AA from
(select sum(AmountBalance) AB
from balancereal b
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod= @idPeriodPrev
Group by c.IdContract) qq  where AB<0) re

update #tmpForanAnalitik
set BEnd=re.AA
from(select sum(AB) AA from
(select sum(AmountBalance) AB
from balancereal b
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod= @idPeriod
Group by c.IdContract) qq  where AB>0) re

update #tmpForanAnalitik
set BEndMin=-re.AA
from(select sum(AB) AA from
(select sum(AmountBalance) AB
from balancereal b
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod= @idPeriod
Group by c.IdContract) qq  where AB<0) re


update #tmpForanAnalitik
set UslugiOpl=qq.DocumentAmount
from (select  sum(o.amountoperation) DocumentAmount
from document dc with (nolock)
inner join operation o with (nolock)  on o.iddocument=dc.iddocument
and	dc.idtypedocument=1 and dc.idperiod=@idPeriod
inner join balance b with (nolock) on b.idbalance=o.idbalance
and idaccounting=6 
) qq 

update #tmpForanAnalitik 
set PosredMoh=isnull(-gt.ff,0)
from (
select sum(documentamount)ff from document d with (nolock) 
inner join pd with (nolock)  on pd.iddocument=d.iddocument
and idtypepd=34 and isnull(value,0)=2 and idtypedocument=7 and d.idperiod=@idPeriod  

) gt 

update #tmpForanAnalitik
set GazOpl=qq.DocumentAmount
from (select  sum(o.amountoperation) DocumentAmount
from document dc with (nolock)
inner join operation o with (nolock)  on o.iddocument=dc.iddocument
and	dc.idtypedocument=1 and dc.idperiod=@idPeriod
inner join balance b with (nolock) on b.idbalance=o.idbalance
and (idaccounting=1 or idaccounting=2)
) qq 

update #tmpForanAnalitik
set PoshOpl=qq.DocumentAmount
from (select  sum(o.amountoperation) DocumentAmount
from document dc with (nolock)
inner join operation o with (nolock)  on o.iddocument=dc.iddocument
and	dc.idtypedocument=1 and dc.idperiod=@idPeriod
inner join balance b with (nolock) on b.idbalance=o.idbalance
and idaccounting=3 
) qq 

update #tmpForanAnalitik
set PenyOpl=qq.DocumentAmount
from (select  sum(o.amountoperation) DocumentAmount
from document dc with (nolock)
inner join operation o with (nolock)  on o.iddocument=dc.iddocument
and	dc.idtypedocument=1 and dc.idperiod=@idPeriod
inner join balance b with (nolock) on b.idbalance=o.idbalance
and idaccounting=4 
) qq 

--update #tmpForanAnalitik
--set NachUslugi=qq.DocumentAmount
--from (select sum(o.amountoperation) DocumentAmount
--from document dc with (nolock)
--inner join operation o with (nolock)  on o.iddocument=dc.iddocument
--and	dc.idtypedocument=5 and dc.idperiod=@idPeriod
--inner join balance b with (nolock) on b.idbalance=o.idbalance
--and idaccounting=6) qq 
--*************************************************************************************************

update #tmpForanAnalitik 
set NachUslugi=isnull(dd,0)
from(
select sum(documentamount)dd
from document d
where idtypedocument=24 and d.idperiod=@idPeriod)qq
--from (select sum(o.amountoperation) dd
--from operation o  with (nolock) 
--inner join balance b with (nolock) on b.idbalance=o.idbalance
--	and b.idperiod=98 and b.idaccounting=6
--	and o.idtypeoperation=2) qq

update #tmpForanAnalitik 
set NachUslugi=NachUslugi-isnull(DocumentAmount,0)
from (select  sum(o.amountoperation) DocumentAmount
from document dc with (nolock)
inner join operation o with (nolock)  on o.iddocument=dc.iddocument
and	dc.idtypedocument=7 and dc.idperiod=@idPeriod
inner join balance b with (nolock) on b.idbalance=o.idbalance
and idaccounting=6) qq

update #tmpForanAnalitik
set NachGaz=isnull(-qq.DocumentAmount,0)
from (select  sum(AmountOperation) DocumentAmount
from FactUse f with (nolock) 
inner join operation o   with (nolock) on o.idoperation=f.idoperation
	and  (f.IdTypeFU=1 or f.IdTypeFU=2) and f.IdPeriod=@idPeriod
inner join balance b with (nolock) on o.idbalance=b.idbalance
and idaccounting=1
inner join document d  with (nolock) on d.iddocument=o.iddocument
	and d.idtypedocument=5) qq 

update #tmpForanAnalitik
set NachPeny=isnull(-qq.DocumentAmount,0)
from (select sum(o.amountoperation) DocumentAmount
from document dc with (nolock)
inner join operation o with (nolock)  on o.iddocument=dc.iddocument
and	dc.idtypedocument=5 and dc.idperiod=@idPeriod
inner join balance b with (nolock) on b.idbalance=o.idbalance
and idaccounting=4) qq 

update #tmpForanAnalitik 
set NachPeny=NachPeny-isnull(gt.ff,0)
from (
select sum(documentamount)ff from document d
inner join operation o with (nolock)  on d.iddocument=o.iddocument
and idtypedocument=7 and d.idperiod=@idPeriod 
inner join balance b with (nolock)  on b.idbalance=o.idbalance
and b.idaccounting=4
) gt 



update #tmpForanAnalitik 
set NachPosh=isnull(-dd,0)
from (select sum(o.amountoperation) dd
from operation o  with (nolock) 
inner join balance b with (nolock) on b.idbalance=o.idbalance
	and b.idperiod=@idperiod and b.idaccounting in (2,3)--=3
	and o.idtypeoperation=2) qq

update #tmpForanAnalitik 
set NachPosh=NachPosh-isnull(gt.ff,0)
from (
select sum(documentamount)ff from document d
inner join operation o with (nolock)  on d.iddocument=o.iddocument
and idtypedocument=7 and d.idperiod=@idPeriod 
inner join balance b with (nolock)  on b.idbalance=o.idbalance
and b.idaccounting in (2,3)--=3
) gt 

update #tmpForanAnalitik
set mBank=isnull(ff.d,0)
from (select sum(d.DocumentAmount)d 
from  batch b  with (nolock) 
inner join document d with (nolock) on d.idbatch=b.idbatch
	and (isnull(b.IDCashier,0)=46 or b.idcashier is null or (b.IDDispatcher=@IDDispatcher and b.idcashier is not null))
	and d.idperiod=@IdPeriod and d.idtypedocument=1)ff



update #tmpForanAnalitik
set mKassa=isnull(ff.d,0)
from (select sum(d.DocumentAmount)d from  batch b with (nolock) 
	inner join document d with (nolock) on d.idbatch=b.idbatch
		and (isnull(b.IDCashier, 0)<>46 and b.idcashier is not null and isnull(b.IDDispatcher, 0)<>@IDDispatcher)
		and d.idperiod=@IdPeriod and d.idtypedocument=1)ff



update #tmpForanAnalitik
set OplNarodBank=isnull(ff.d,0)
from (select sum(d.DocumentAmount)d from  batch b with (nolock) 
	inner join document d with (nolock) on d.idbatch=b.idbatch
		and d.idperiod=@IdPeriod and b.iddispatcher=69 and b.idcashier is null
		and d.idtypedocument=1)ff

update #tmpForanAnalitik
set OplNarodBankomat=isnull(ff.d,0)
from (select sum(d.DocumentAmount)d from  batch b with (nolock) 
	inner join document d with (nolock) on d.idbatch=b.idbatch
		and d.idperiod=@IdPeriod and b.iddispatcher=70 and b.idcashier is null
		and d.idtypedocument=1)ff

update #tmpForanAnalitik
set OplKazPochta=isnull(ff.d,0)
from (select sum(d.DocumentAmount)d from  batch b with (nolock) 
	inner join document d with (nolock) on d.idbatch=b.idbatch
		and d.idperiod=@IdPeriod and b.iddispatcher=71 and b.idcashier is null
		 and d.idtypedocument=1)ff

update #tmpForanAnalitik
set OplTuran=isnull(ff.d,0)
from (select sum(d.DocumentAmount)d from  batch b with (nolock) 
	inner join document d with (nolock) on d.idbatch=b.idbatch
		and d.idperiod=@IdPeriod and b.iddispatcher=128 and b.idcashier is null
		and d.idtypedocument=1)ff

update #tmpForanAnalitik
set OplCesna=isnull(ff.d,0)
from (select sum(d.DocumentAmount)d from  batch b with (nolock) 
	inner join document d with (nolock) on d.idbatch=b.idbatch
		and d.idperiod=@IdPeriod and b.iddispatcher=77 and b.idcashier is null
		and d.idtypedocument=1)ff

update #tmpForanAnalitik
set OplSever=isnull(ff.d,0)
from (select sum(d.DocumentAmount)d from  batch b with (nolock) 
	inner join document d with (nolock) on d.idbatch=b.idbatch
		and d.idperiod=@IdPeriod and b.iddispatcher=99 and b.idcashier is null
		and d.idtypedocument=1)ff

update #tmpForanAnalitik 
set POPU=isnull(cubes,0)
from (select sum(f.FactAmount) cubes
from FactUse f with (nolock) 
inner join operation o   with (nolock) on o.idoperation=f.idoperation
inner join document d with (nolock) on d.iddocument=o.iddocument
	and idtypedocument=5 
	and isnull(f.IdTypeFU,0)=2 
	and f.IdPeriod=@idperiod) qq

select *
from #tmpForanAnalitik

drop table #tmpForSaldo
drop table #tmpForanAnalitik



----declare @idPeriod int
----declare @date datetime
--
----declare @idPeriod int
----declare @date datetime
--
--declare @idPeriodPrev int
--declare @DateT datetime
--declare @month int
--declare @year int 
--declare @day int
--
----set @idPeriod=69--dbo.GetNowPeriod()
----set @date='2010-10-13'
--
--set @DateT=dbo.DateOnly(@Date)
--set @idPeriodPrev=dbo.fGetPredPeriodVariable(@idPeriod)
--
--select c.idcontract , convert(float, 0) DA,convert(float, 0) DAUS, dbo.fGetLastBalanceReal(@idPeriodPrev, c.IdContract, 0) AmountBalance,
--	dbo.fGetLastBalanceReal(@idPeriod, c.IdContract, 0) AmountBalanceEnd,
--	convert(float, 0) DAP,convert(float, 0) DAPUS, convert(float ,0) ForSaldo, 
--	convert(float, 0) mDAP,convert(float, 0) mDAPUS,	convert(float ,0) mForSaldo,
--	g.IdStatusGObject, isnull(gm.idstatusGMeter,0) idstatusGMeter
--	, convert(float , null) FactUse, convert(float , null) PrevFactUse,c.Status,convert(float, 0) DAPPred
--into #tmpForSaldo
--from contract c  with (nolock) 
--inner join gobject g with (nolock)  on g.idcontract=c.idcontract
----and g.idcontract=916555
--and g.idgobject<>916602
--left join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
--	and gm.idstatusgmeter=1
--group by c.idcontract, g.IdStatusGObject,c.Status,idstatusGMeter
--
--update #tmpForSaldo
--set DA=qq.DocumentAmount
--from #tmpForSaldo t 
--inner join (select d.idcontract, sum(d.DocumentAmount) DocumentAmount
--from document d with (nolock) 
----inner join operation o with (nolock)  on o.iddocument=d.iddocument
--where d.idtypedocument=1 and dbo.DateOnly(d.documentdate)=@DateT 
----inner join balance b with (nolock) on b.idbalance=o.idbalance
----and idaccounting<>6 
--group by d.idcontract) qq on qq.idcontract=t.idcontract
--
--update #tmpForSaldo
--set DAUS=qq.DocumentAmount
--from #tmpForSaldo t 
--inner join (select d.idcontract, sum(o.amountoperation) DocumentAmount
--from document d with (nolock) 
--inner join operation o with (nolock)  on o.iddocument=d.iddocument
--and d.idtypedocument=1 and dbo.DateOnly(d.documentdate)=@DateT 
--inner join balance b with (nolock) on b.idbalance=o.idbalance
--and idaccounting=6 
--group by d.idcontract) qq on qq.idcontract=t.idcontract
--
--update #tmpForSaldo
--set DAP=qq.DocumentAmount
--from #tmpForSaldo t 
--inner join (select dc.idcontract, sum(dc.DocumentAmount) DocumentAmount
--from document dc with (nolock)
----inner join operation o with (nolock)  on o.iddocument=dc.iddocument
--where	dc.idtypedocument=1 and dc.idperiod=@idPeriod
--	and dbo.dateonly(dc.DocumentDate)<@DateT
----inner join balance b with (nolock) on b.idbalance=o.idbalance
----and idaccounting<>6 
--group by dc.idcontract) qq on qq.idcontract=t.idcontract
--
--update #tmpForSaldo
--set DAPUS=qq.DocumentAmount
--from #tmpForSaldo t 
--inner join (select dc.idcontract, sum(o.amountoperation) DocumentAmount
--from document dc with (nolock)
--inner join operation o with (nolock)  on o.iddocument=dc.iddocument
--and	dc.idtypedocument=1 and dc.idperiod=@idPeriod
--	and dbo.dateonly(dc.DocumentDate)<@DateT
--inner join balance b with (nolock) on b.idbalance=o.idbalance
--and idaccounting=6 
--group by dc.idcontract) qq on qq.idcontract=t.idcontract
--
--update #tmpForSaldo
--set DAPPred=qq.DocumentAmount
--from #tmpForSaldo t 
--inner join (select dc.idcontract, sum(dc.DocumentAmount) DocumentAmount
--from document dc with (nolock)
----inner join operation o with (nolock)  on o.iddocument=dc.iddocument
--where	dc.idtypedocument=1 and dc.idperiod=@idPeriodPrev
----inner join balance b with (nolock) on b.idbalance=o.idbalance
----and idaccounting<>6 
--group by dc.idcontract) qq on qq.idcontract=t.idcontract
--
--update #tmpForSaldo
--set ForSaldo=case when AmountBalance+DAP<0 then 
--	case when DA>-(AmountBalance+DAP) then -(AmountBalance+DAP) else DA end
--	else 0 end
--from #tmpForSaldo
--
--update #tmpForSaldo
--set mDAP=isnull(DAP,0)+isnull(DA,0)
--from #tmpForSaldo
--
--update #tmpForSaldo
--set mDAPUS=isnull(DAPUS,0)+isnull(DAUS,0)
--from #tmpForSaldo
--
--update #tmpForSaldo
--set mForSaldo=case when AmountBalance<0 then 
--	case when mDAP>-(AmountBalance) then -(AmountBalance) else mDAP end
--	else 0 end
--from #tmpForSaldo
--
--set @DateT=@DateT+1
--
--update #tmpForSaldo 
--set FactUse=fa
--from  #tmpForSaldo t 
--inner join (select g.idcontract, sum(f.factamount) fa
--from gobject g with (nolock) 
--inner join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
--inner join indication i  with (nolock)  on gm.idgmeter=i.idgmeter
--	and i.datedisplay<@DateT
--inner join factuse f with (nolock) on f.idindication=i.idindication
--	and f.idperiod=@idPeriod 
--left join operation o with (nolock) on o.idoperation=f.idoperation
--left join document d with (nolock) on o.iddocument=d.iddocument
--	and d.idtypedocument=7 
--where isnull(d.iddocument,0)=0
--group by g.idcontract) qq on t.idcontract=qq.idcontract
--
--update #tmpForSaldo 
--set PrevFactUse=fa
--from  #tmpForSaldo t 
--inner join (select g.IdContract, sum(f.factamount) fa
--from gobject g with (nolock)  
--inner join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
--inner join indication i with (nolock)  on gm.idgmeter=i.idgmeter
--	and i.datedisplay<@DateT
--inner join factuse f with (nolock) on f.idindication=i.idindication
--	and f.idperiod=@idPeriodPrev
--group by g.idcontract) qq on t.idcontract=qq.idcontract
--
--set @DateT=@DateT-1
--
--select   convert(float ,0) DA,convert(float ,0) DAUS, convert(float, 0) FS, convert(float ,0) mDA, convert(float ,0) mDAUS,convert(float, 0) mFS, convert(float, 0) 
--	SumIn,convert(float ,0) RazIn, convert(float ,0) BDMI,  convert(float ,0) Cub, convert(float, 0)BBeg , convert(float, 0)BBegMin, 
--	convert(float ,0) mKassa,convert(float ,0) mKassaUS, convert(float ,0) mBank,convert(float ,0) mBankUS--, convert(float ,0) mNarBankPKO, convert(float ,0) mNarBankB,
--	-- convert(float ,0) mKazPost, convert(float ,0) mTuranAlem, 
--,convert(float, 0)BEnd , convert(float, 0)BEndMin, convert(float, 0)NetPotrebl
--	,convert(float, 0)M3KN, convert(float, 0)KGKN,convert(float, 0)CountContract,convert(float, 0)CountOplat,convert(float, 0)NetOplat,convert(float, 0)NetOplatBol
--	--,convert(float, 0)NetOplatDolg,convert(float, 0)NetOplatDolgPred
--into #tmpForanAnalitik
--
--update #tmpForanAnalitik 
--set DA=df.DA,DAUS=df.DAUS, FS=df.FS, mDA=df.mDA,mDAUS=df.mDAUS, mFS=df.mFS
--from (select SUM(DA) DA, SUM(DAUS) DAUS,SUM(ForSaldo) FS, sum(mDAP) mDA, sum(mDAPUS) mDAUS,SUM(mForSaldo) mFS from #tmpForSaldo) df
--
--update #tmpForanAnalitik 
--set SumIn=re.SumIn
--from(select  count(c.idcontract) SumIn
--from #tmpForSaldo c
--where c.idstatusgobject=1 and c.idstatusgmeter=1) re
--
--update #tmpForanAnalitik 
--set CountOplat=re.SumIn
--from(select  count(c.idcontract) SumIn
--from #tmpForSaldo c
--where mDAP>0 and isnull(status,0)<>2) re
--
--update #tmpForanAnalitik 
--set NetOplat=re.SumIn
--from(select  count(c.idcontract) SumIn
--from #tmpForSaldo c
--where mDAP=0 and isnull(status,0)<>2) re
--
--update #tmpForanAnalitik 
--set NetOplatBol=re.SumIn
--from(select  count(c.idcontract) SumIn
--from #tmpForSaldo c
--where isnull(status,0)<>2 and mDAP=0 and DAPPred=0) re
--
--update #tmpForanAnalitik 
--set CountContract=re.SumIn
--from(select  count(c.idcontract) SumIn
--from #tmpForSaldo c
--where isnull(status,0)<>2) re
--
---- update #tmpForanAnalitik 
---- set NetOplatDolg=re.SumIn
---- from(select  count(c.idcontract) SumIn
---- from #tmpForSaldo c
---- where mDAP=0 and isnull(status,0)<>2 and AmountBalanceEnd<0) re
---- 
---- update #tmpForanAnalitik 
---- set NetOplatDolgPred=re.SumIn
---- from(select  count(c.idcontract) SumIn
---- from #tmpForSaldo c
---- where mDAP=0 and isnull(status,0)<>2 and AmountBalanceEnd<0 and AmountBalance<0 and DapPred=0 ) re
--
--update #tmpForanAnalitik 
--set NetPotrebl=re.summa
--from (select count(idcontract) Summa 
--from #tmpForSaldo c 
--where c.idstatusgobject=1 and c.idstatusgmeter=1 and c.factuse=0 and c.factuse is not null)re
--
--update #tmpForanAnalitik 
--set M3KN=isnull(cubes,0)
--from (select sum(f.FactAmount) cubes
--from FactUse f with (nolock) 
--inner join document d  with (nolock) on d.iddocument=f.iddocument
--	and d.idtypedocument=7
--	and f.IdPeriod=@idperiod
--	and f.IdTypeFU=1) qq
--
--update #tmpForanAnalitik 
--set KGKN=isnull(cubes,0)
--from (select sum(f.FactAmount) cubes
--from FactUse f with (nolock) 
--inner join document d  with (nolock) on d.iddocument=f.iddocument
--	and d.idtypedocument=7
--	and f.IdPeriod=@idperiod
--	and f.IdTypeFU=2) qq
--
--update #tmpForanAnalitik 
--set RazIn=re.ff
--from (select count(idcontract) ff from (select distinct g.idcontract
--from gobject g with (nolock)  
--inner join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
--inner join indication i  with (nolock)  on gm.idgmeter=i.idgmeter
--inner join factuse f with (nolock) on f.idindication=i.idindication
--	and f.idperiod=@idPeriod)qq) re
--
--update #tmpForanAnalitik 
--set BDMI=re.SumIn
--from(select count(c.idcontract) SumIn 
--from #tmpForSaldo c where FactUse is not null or PrevFactUse is not null) re
--
--update #tmpForanAnalitik
--set Cub=re.SumIn
--from(select isnull(sum(FactUse),0) SumIn from #tmpForSaldo c where FactUse is not null) re
--
--update #tmpForanAnalitik
--set BBeg=re.SumIn
--from(select isnull(sum(AmountBalance),0) SumIn from #tmpForSaldo c where AmountBalance>0) re
--
--update #tmpForanAnalitik
--set BBegMin=-re.SumIn
--from(select isnull(sum(AmountBalance),0) SumIn from #tmpForSaldo c where AmountBalance<0) re
--
--update #tmpForanAnalitik
--set BEnd=re.SumIn
--from(select isnull(sum(AmountBalanceEnd),0) SumIn from #tmpForSaldo c where AmountBalanceEnd>0) re
--
--update #tmpForanAnalitik
--set BEndMin=-re.SumIn
--from(select isnull(sum(AmountBalanceEnd),0) SumIn from #tmpForSaldo c where AmountBalanceEnd<0) re
--
--update #tmpForanAnalitik
--set mBank=isnull(ff.d,0)
--from (select sum(d.DocumentAmount)d 
--from  batch b  with (nolock) 
--inner join document d with (nolock) on d.idbatch=b.idbatch
--	and (isnull(b.IDCashier,0)=46 or b.idcashier is null)
--	and d.idperiod=@IdPeriod and d.idtypedocument=1 
----inner join operation o with (nolock)  on o.iddocument=d.iddocument
----inner join balance bb with (nolock) on bb.idbalance=o.idbalance
----and bb.idaccounting<>6
--where dbo.dateonly(d.DocumentDate)<=@DateT )ff
--
--update #tmpForanAnalitik
--set mBankUS=isnull(ff.d,0)
--from (select sum(o.amountoperation)d 
--from  batch b  with (nolock) 
--inner join document d with (nolock) on d.idbatch=b.idbatch
--	and (isnull(b.IDCashier,0)=46 or b.idcashier is null)
--	and d.idperiod=@IdPeriod and d.idtypedocument=1 
--inner join operation o with (nolock)  on o.iddocument=d.iddocument
--inner join balance bb with (nolock) on bb.idbalance=o.idbalance
--and bb.idaccounting=6
--where dbo.dateonly(d.DocumentDate)<=@DateT )ff
--
--update #tmpForanAnalitik
--set mKassa=isnull(ff.d,0)
--from (select sum(d.DocumentAmount)d from  batch b with (nolock) 
--	inner join document d with (nolock) on d.idbatch=b.idbatch
--		and (isnull(b.IDCashier, 0)<>46 and b.idcashier is not null)
--		and d.idperiod=@IdPeriod and d.idtypedocument=1
----inner join operation o with (nolock)  on o.iddocument=d.iddocument
----inner join balance bb with (nolock) on bb.idbalance=o.idbalance
----and bb.idaccounting<>6
--	where dbo.dateonly(d.DocumentDate)<=@DateT )ff
--
--update #tmpForanAnalitik
--set mKassaUS=isnull(ff.d,0)
--from (select sum(o.amountoperation)d from  batch b with (nolock) 
--	inner join document d with (nolock) on d.idbatch=b.idbatch
--		and (isnull(b.IDCashier, 0)<>46 and b.idcashier is not null)
--		and d.idperiod=@IdPeriod and d.idtypedocument=1
--inner join operation o with (nolock)  on o.iddocument=d.iddocument
--inner join balance bb with (nolock) on bb.idbalance=o.idbalance
--and bb.idaccounting=6
--	where dbo.dateonly(d.DocumentDate)<=@DateT )ff
--
----update #tmpForanAnalitik
----set mNarBankPKO=isnull(ff.d,0)
----from (select sum(d.DocumentAmount)d from  batch b with (nolock) 
----	inner join document d with (nolock) on d.idbatch=b.idbatch
----		and d.idperiod=@IdPeriod and b.iddispatcher=69 and b.idcashier is null
----		and d.idtypedocument=1
----inner join operation o with (nolock)  on o.iddocument=d.iddocument
----inner join balance bb with (nolock) on bb.idbalance=o.idbalance
----and bb.idaccounting<>6
----		where dbo.dateonly(d.DocumentDate)<=@DateT)ff
----
----update #tmpForanAnalitik
----set mNarBankB=isnull(ff.d,0)
----from (select sum(d.DocumentAmount)d from  batch b with (nolock) 
----	inner join document d with (nolock) on d.idbatch=b.idbatch
----		and d.idperiod=@IdPeriod and b.iddispatcher=70 and b.idcashier is null
----		and d.idtypedocument=1
----inner join operation o with (nolock)  on o.iddocument=d.iddocument
----inner join balance bb with (nolock) on bb.idbalance=o.idbalance
----and bb.idaccounting<>6
----		where dbo.dateonly(d.DocumentDate)<=@DateT)ff
----
----update #tmpForanAnalitik
----set mKazPost=isnull(ff.d,0)
----from (select sum(d.DocumentAmount)d from  batch b with (nolock) 
----	inner join document d with (nolock) on d.idbatch=b.idbatch
----		and d.idperiod=@IdPeriod and b.iddispatcher=71 and b.idcashier is null
----		 and d.idtypedocument=1
----inner join operation o with (nolock)  on o.iddocument=d.iddocument
----inner join balance bb with (nolock) on bb.idbalance=o.idbalance
----and bb.idaccounting<>6
----		where dbo.dateonly(d.DocumentDate)<=@DateT)ff
----
----update #tmpForanAnalitik
----set mTuranAlem=isnull(ff.d,0)
----from (select sum(d.DocumentAmount)d from  batch b with (nolock) 
----	inner join document d with (nolock) on d.idbatch=b.idbatch
----		and d.idperiod=@IdPeriod and b.iddispatcher=75 and b.idcashier is null
----		and d.idtypedocument=1
----inner join operation o with (nolock)  on o.iddocument=d.iddocument
----inner join balance bb with (nolock) on bb.idbalance=o.idbalance
----and bb.idaccounting<>6
----		where dbo.dateonly(d.DocumentDate)<=@DateT)ff
--select *
--from #tmpForanAnalitik
--
--drop table #tmpForSaldo
--drop table #tmpForanAnalitik
--
















GO