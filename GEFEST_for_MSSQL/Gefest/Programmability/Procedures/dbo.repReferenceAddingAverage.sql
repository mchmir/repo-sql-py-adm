SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO













CREATE PROCEDURE [dbo].[repReferenceAddingAverage](@idperiod int) AS
-------------**************Справка по начислению за газ с начислением по средниму*********--------------
--declare @idPeriod int
--set @idPeriod=17

declare @idPeriodPrev int

set @idPeriodPrev=dbo.fGetPredPeriodVariable(@idPeriod)
declare @tariff float
select @tariff=value from tariff t where t.IdPeriod=@IdPeriod

declare @AnalSal table (BeginDt float, BeginKt float,AmountNachPU float, M3NachPU float, 
AmountKGNachPU float, KGNachPU float,AmountNachN float, KGNachN float,
M3KN float, KGKN float, KGKNS float,AmountKN float, AmountKNS float,
AmountNachG float, AmountPay float, AmountPayG float,
EndDt float, EndKt float, tariff float, AbonenVsego float,AbonenSPU float
,AbonenBezPU float,AbonenPoSredProz float, AbonenKorPoSred float, OtsutBolee float, 
OtsutPoc float, abonenBez float, abonots float, PenyAmount float, penyNach float,KorSredMosh float,PerenosOpl float)


insert @AnalSal (BeginKt, tariff)
select isnull(sum(AB),0), @tariff from
(select sum(AmountBalance) AB
from balance b with (nolock) 
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod= @idPeriodPrev
and idaccounting<>6
Group by c.IdContract) qq where AB>0 

update @AnalSal 
set BeginDt=isnull(-AA,0)
from( select sum(AB) AA from
(select sum(AmountBalance) AB
from balance b
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod= @idPeriodPrev
and idaccounting<>6
Group by c.IdContract) qq  where AB<0) ww 

update @AnalSal 
set EndDt=isnull(-AA,0)
from( select sum(AB) AA from
(select sum(AmountBalance) AB
from balance b
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod= @idPeriod
and idaccounting<>6
Group by c.IdContract) qq  where AB<0) ww 

update @AnalSal 
set EndKt=isnull(AA,0)
from( select sum(AB) AA from
(select sum(AmountBalance) AB
from balance b
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod= @idPeriod
and idaccounting<>6
Group by c.IdContract) qq  where AB>0) ww 

update @AnalSal 
set AbonenVsego=re.SumIn
from(select  isnull(count(c.idcontract),0) SumIn
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
	and g.idstatusgobject=1
inner join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
	and gm.idstatusgmeter=1) re

update @AnalSal 
set abonenBez=re.SumIn-AbonenVsego
from(select  isnull(count(c.idcontract),0) SumIn
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
where	 g.idstatusgobject=1 ) re


update @AnalSal 
set AbonenSPU=re.ff
from (select count(idcontract) ff from (select distinct g.idcontract
from gobject g with (nolock)  
inner join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
inner join indication i  with (nolock)  on gm.idgmeter=i.idgmeter
inner join factuse f with (nolock) on f.idindication=i.idindication
	and f.idperiod=@idPeriod)qq) re

update @AnalSal 
set OtsutPoc=AbonenVsego-re.ff
from (select count(idcontract) ff from (select distinct g.idcontract
from gobject g with (nolock)  
inner join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
inner join indication i  with (nolock)  on gm.idgmeter=i.idgmeter
inner join factuse f with (nolock) on f.idindication=i.idindication
	and f.idperiod=@idPeriod)qq) re

update @AnalSal 
set OtsutBolee=AbonenVsego-re.ff
from (select count(idcontract) ff from (select distinct g.idcontract
from gobject g with (nolock)  
inner join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
inner join indication i  with (nolock)  on gm.idgmeter=i.idgmeter
inner join factuse f with (nolock) on f.idindication=i.idindication
	and (f.idperiod=@idPeriod or f.idperiod=@idPeriodPrev)) qq) re

update @AnalSal 
set abonots=re.RazIn
from(select isnull(count(iddocument),0) RazIn
from  document  
where idperiod=@idperiod and idtypedocument=14 
) re

update @AnalSal 
set M3NachPU=isnull(cubes,0),
AmountNachPU=-ao
from (select sum(f.FactAmount) cubes, sum(AmountOperation) ao
from FactUse f with (nolock) 
inner join indication i  with (nolock) on i.idindication=f.idindication
	and isnull(f.IdTypeFU,0)=1 
inner join operation o   with (nolock) on o.idoperation=f.idoperation
inner join document d with (nolock) on d.iddocument=o.iddocument
	and idtypedocument=5
	and  f.IdPeriod=@idperiod) qq

update @AnalSal 
set KorSredMosh=isnull(-dd,0)
from (select sum(o.amountoperation) dd
from document d
inner join operation o on o.iddocument=d.iddocument
and isnull(o.numberoperation,0)=99999
inner join balance b on b.idbalance=o.idbalance
where d.idtypedocument=7 and d.idperiod=@idperiod and idaccounting<>6) qq

update @AnalSal 
set KGNachPU=isnull(cubes,0),
AmountKGNachPU=-ao
from (select sum(f.FactAmount) cubes, sum(AmountOperation) ao
from FactUse f with (nolock) 
inner join operation o   with (nolock) on o.idoperation=f.idoperation
inner join document d with (nolock) on d.iddocument=o.iddocument
	and idtypedocument=5 
	and isnull(f.IdTypeFU,0)=3 
	and f.IdPeriod=@idperiod) qq

update @AnalSal 
set KGNachN=isnull(cubes,0),
AmountNachN=-ao
from (select sum(f.FactAmount) cubes, sum(AmountOperation) ao
from FactUse f with (nolock) 
inner join operation o   with (nolock) on o.idoperation=f.idoperation
inner join document d with (nolock) on d.iddocument=o.iddocument
	and idtypedocument=5
	and f.IdPeriod=@idperiod
	and  isnull(f.IdTypeFU,0)=2 ) qq

update @AnalSal 
set M3KN=isnull(cubes,0)
from (select sum(f.FactAmount) cubes
from FactUse f with (nolock) 
inner join document d  with (nolock) on d.iddocument=f.iddocument
	and d.idtypedocument=7
	and f.IdPeriod=@idperiod and isnull(f.IdTypeFU,0)=1) qq

update @AnalSal 
set KGKN=isnull(cubes,0)
from (select sum(f.FactAmount) cubes
from FactUse f with (nolock) 
inner join document d  with (nolock) on d.iddocument=f.iddocument
	and d.idtypedocument=7
	and f.IdPeriod=@idperiod and isnull(f.IdTypeFU,0)=2 ) qq

update @AnalSal 
set AmountKN=isnull(-dd,0)
from (select sum(o.amountoperation) dd
from document d
inner join operation o on o.iddocument=d.iddocument
and isnull(o.numberoperation,0)<>99999
inner join balance b on b.idbalance=o.idbalance
where d.idtypedocument=7 and d.idperiod=@idperiod and idaccounting<>6) qq

update @AnalSal 
set KGKNS=isnull(cubes,0)
from (select sum(f.FactAmount) cubes
from FactUse f with (nolock) 
inner join document d  with (nolock) on d.iddocument=f.iddocument
	and d.idtypedocument=14
	and f.IdPeriod=@idperiod and isnull(f.IdTypeFU,0)=3) qq

update @AnalSal 
set AmountKNS=isnull(-dd,0)
from (select sum(d.documentamount) dd
from document d  with (nolock) 
where d.idtypedocument=14 and d.idperiod=@idperiod) qq

update @AnalSal 
set AmountNachG=isnull(-dd,0)
from (select sum(o.amountoperation) dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod and b.idaccounting in (2,3) --=3
	and o.idtypeoperation=2) qq


update @AnalSal 
set PenyNach=isnull(-dd,0)
from (select sum(o.amountoperation) dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod and b.idaccounting=4
	and o.idtypeoperation=2) qq

update @AnalSal 
set AmountPay=isnull(dd,0)
from (select sum(o.amountoperation) dd
from document d  with (nolock) 
inner join operation o  with (nolock) on o.iddocument=d.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
where d.idtypedocument=1 and d.idperiod=@idperiod and b.idaccounting<>6) qq

update @AnalSal 
set AmountPayG=isnull(dd,0)
from (select sum(o.amountoperation) dd
from operation o  with (nolock) 
inner join balance b with (nolock) on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and b.idaccounting=3 and o.idtypeoperation=1) qq

update @AnalSal 
set PenyAmount=isnull(dd,0)
from (select sum(o.amountoperation) dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and b.idaccounting=4 and o.idtypeoperation=1) qq

update @AnalSal 
set PerenosOpl=isnull(dd,0)*-1
from (select sum(o.amountoperation) dd
from operation o  with (nolock) 
inner join document d  with (nolock) on o.iddocument=d.iddocument
and d.idperiod=@idperiod and idtypedocument<>7
inner join balance b with (nolock) on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract
	and b.idperiod=@idperiod
	and b.idaccounting=6 and o.idtypeoperation=3) qq


update @AnalSal 
set AbonenBezPU=abs(KGNachN/6.1)
update @AnalSal 
set AbonenPoSredProz=abs(KGNachPU/6.1)
update @AnalSal 
set AbonenKorPoSred=abs(KGKNS/6.1)

select * from @AnalSal











GO