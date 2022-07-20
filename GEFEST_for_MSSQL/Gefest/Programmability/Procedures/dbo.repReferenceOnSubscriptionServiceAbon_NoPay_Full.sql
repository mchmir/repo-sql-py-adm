SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO












CREATE PROCEDURE [dbo].[repReferenceOnSubscriptionServiceAbon_NoPay_Full] (@idPeriod int) AS
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
	, convert(float , null) FactUse, convert(float , null) PrevFactUse,c.Status,convert(float, 0) DAPPred, convert(varchar,'')urddok
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

update #tmpForSaldo
set urddok='Ю/д'
from #tmpForSaldo t
inner join balance b on b.idcontract=t.idcontract and b.idperiod=@IDPeriod and b.idaccounting=2 and b.amountbalance<0



	select '1. Не плативших - с переплатой' as name, ct.account, isnull(p.Surname,'')+' '+isnull(p.name,'')+' '+isnull(p.Patronic,'') as fio,
	s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat) as ADDRESS, urddok, c.amountbalance
	from #tmpForSaldo c
	inner join contract ct on c.idcontract=ct.idcontract
	inner join person p (nolock) on ct.IDPerson=p.idperson
	inner join address a (nolock) on a.idaddress=p.idaddress
	inner join street s with (nolock)  on s.idstreet=a.idstreet
	inner join house hs  with (nolock) on hs.idhouse=a.idhouse
	where mDAP=0 and isnull(c.status,0)<>2 and c.amountbalance>0

union all

	select '2. Не плативших - с долгом' as name, ct.account, isnull(p.Surname,'')+' '+isnull(p.name,'')+' '+isnull(p.Patronic,'') as fio,
	s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat) as ADDRESS, urddok, c.amountbalance
	from #tmpForSaldo c
	inner join contract ct on c.idcontract=ct.idcontract
	inner join person p (nolock) on ct.IDPerson=p.idperson
	inner join address a (nolock) on a.idaddress=p.idaddress
	inner join street s with (nolock)  on s.idstreet=a.idstreet
	inner join house hs  with (nolock) on hs.idhouse=a.idhouse
	where mDAP=0 and isnull(c.status,0)<>2 and c.amountbalance<0

union all

	select '3. Не плативших два месяца - с переплатой' as name, ct.account, isnull(p.Surname,'')+' '+isnull(p.name,'')+' '+isnull(p.Patronic,'') as fio,
	s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat) as ADDRESS, urddok, c.amountbalance
	from #tmpForSaldo c
	inner join contract ct on c.idcontract=ct.idcontract
	inner join person p (nolock) on ct.IDPerson=p.idperson
	inner join address a (nolock) on a.idaddress=p.idaddress
	inner join street s with (nolock)  on s.idstreet=a.idstreet
	inner join house hs  with (nolock) on hs.idhouse=a.idhouse
	where isnull(c.status,0)<>2 and mDAP=0 and DAPPred=0 and c.amountbalance>0

union all

	select '4. Не плативших два месяца - с долгом' as name, ct.account, isnull(p.Surname,'')+' '+isnull(p.name,'')+' '+isnull(p.Patronic,'') as fio,
	s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat) as ADDRESS, urddok, c.amountbalance
	from #tmpForSaldo c
	inner join contract ct on c.idcontract=ct.idcontract
	inner join person p (nolock) on ct.IDPerson=p.idperson
	inner join address a (nolock) on a.idaddress=p.idaddress
	inner join street s with (nolock)  on s.idstreet=a.idstreet
	inner join house hs  with (nolock) on hs.idhouse=a.idhouse
	where isnull(c.status,0)<>2 and mDAP=0 and DAPPred=0 and c.amountbalance<0






/*
select  c.idcontract, c.amountbalance
from #tmpForSaldo c
where mDAP=0 and isnull(status,0)<>2

select  c.idcontract, c.amountbalance
from #tmpForSaldo c
where mDAP=0 and isnull(status,0)<>2 and c.amountbalance>=0

select  c.idcontract, c.amountbalance
from #tmpForSaldo c
where mDAP=0 and isnull(status,0)<>2 and c.amountbalance<0

select  c.idcontract, c.amountbalance
from #tmpForSaldo c
where isnull(status,0)<>2 and mDAP=0 and DAPPred=0 and c.amountbalance>=0

select  c.idcontract, c.amountbalance
from #tmpForSaldo c
where isnull(status,0)<>2 and mDAP=0 and DAPPred=0 and c.amountbalance<0
*/

--select * from #tmpForSaldo

drop table #tmpForSaldo





















GO