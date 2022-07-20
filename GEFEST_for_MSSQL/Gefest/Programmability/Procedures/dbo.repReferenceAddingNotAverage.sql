SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO














CREATE PROCEDURE [dbo].[repReferenceAddingNotAverage](@idperiod int) AS
-------------**************Справка по начислению за газ без начисления по средниму*********--------------
--declare @idPeriod int
--set @idPeriod=20

declare @NormaC float
--set  @NormaC=2.14
--с 01.06.2015 по приказу 98-п от 27.05.2015
set  @NormaC=2.428

declare @idPeriodPrev int

set @idPeriodPrev=dbo.fGetPredPeriodVariable(@idPeriod)
declare @tariff float
select @tariff=value from tariff t where t.IdPeriod=@IdPeriod
declare @end datetime
set @end=dbo.fGetDatePeriod(@IdPeriod,0)

declare @AnalSal table (BeginDt float, BeginKt float,AmountNachPU float, M3NachPU float, 
KGNachPU float,AmountNachN float, KGNachN float,
M3KN float, KGKN float, AmountKN float, 
AmountNachG float,AmountPay float, AmountPayG float,
EndDt float, EndKt float, tariff float, AbonenVsego float,AbonenSPU float
,AbonenBezPU float, OtsutBolee float, OtsutPoc float, abonenBez float, PenyAmount float, penyNach float,PerenosOpl float)

insert @AnalSal (BeginKt, tariff)
select isnull(sum(AB),0), @tariff from
(select sum(AmountBalance) AB
from balanceReal b with (nolock) 
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod= @idPeriodPrev
and idaccounting<>6
Group by c.IdContract) qq where AB>0 

update @AnalSal 
set BeginDt=isnull(-AA,0)
from( select sum(AB) AA from
(select sum(AmountBalance) AB
from balanceReal b
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod= @idPeriodPrev
and idaccounting<>6
Group by c.IdContract) qq  where AB<0) ww 

update @AnalSal 
set EndDt=isnull(-AA,0)
from( select sum(AB) AA from
(select sum(AmountBalance) AB
from balanceReal b
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod= @idPeriod
and idaccounting<>6
Group by c.IdContract) qq  where AB<0) ww 

update @AnalSal 
set EndKt=isnull(AA,0)
from( select sum(AB) AA from
(select sum(AmountBalance) AB
from balanceReal b
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod= @idPeriod
and idaccounting<>6
Group by c.IdContract) qq  where AB>0) ww 

update @AnalSal 
set M3NachPU=isnull(cubes,0),
AmountNachPU=-ao
from (select sum(f.FactAmount) cubes, sum(AmountOperation) ao
from FactUse f with (nolock) 
inner join indication i  with (nolock) on i.idindication=f.idindication
and f.IdTypeFU=1
inner join operation o   with (nolock) on o.idoperation=f.idoperation
inner join document d  with (nolock) on d.iddocument=o.iddocument
	and d.idtypedocument=5
	and  d.IdPeriod=@idperiod) qq
/*
update @AnalSal 
set KGNachPU=isnull(cubes,0)
from (select sum(f.FactAmount) cubes, sum(AmountOperation) ao
from FactUse f with (nolock) 
inner join operation o   with (nolock) on o.idoperation=f.ifoperation
inner join document d  with (nolock) on d.iddocument=o.iddocument
	and d.idtypedocument=5
	and f.IdTypeFU=2 
	and d.IdPeriod=@idperiod) qq
*/
update @AnalSal 
set KGNachN=isnull(cubes,0),
AmountNachN=-ao
from (select sum(f.FactAmount) cubes, sum(AmountOperation) ao
from FactUse f with (nolock) 
inner join operation o   with (nolock) on o.idoperation=f.idoperation
	and  f.IdTypeFU=2
inner join document d  with (nolock) on d.iddocument=o.iddocument
	and d.idtypedocument=5
	and f.IdPeriod=@idperiod) qq

update @AnalSal 
set M3KN=isnull(cubes,0)
from (select sum(f.FactAmount) cubes
from FactUse f with (nolock) 
inner join document d  with (nolock) on d.iddocument=f.iddocument
	and d.idtypedocument=7
	and f.IdPeriod=@idperiod
	and f.IdTypeFU=1) qq

update @AnalSal 
set KGKN=isnull(cubes,0)
from (select sum(f.FactAmount) cubes
from FactUse f with (nolock) 
inner join document d  with (nolock) on d.iddocument=f.iddocument
	and d.idtypedocument=7
	and f.IdPeriod=@idperiod
	and f.IdTypeFU=2) qq

update @AnalSal 
set AmountKN=isnull(-dd,0)
from (select sum(o.amountoperation) dd
from document d
inner join operation o on o.iddocument=d.iddocument
and isnull(o.numberoperation,0) <> 99999
inner join balance b on b.idbalance=o.idbalance
where d.idtypedocument=7 and d.idperiod=@idperiod and idaccounting<>6) qq

update @AnalSal 
set AmountNachG=isnull(-dd,0)
from (select sum(o.amountoperation) dd
from operation o
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and b.idaccounting=3 and o.idtypeoperation=2) qq

update @AnalSal 
set penyNach=isnull(-dd,0)
from (select sum(o.amountoperation) dd
from operation o
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and b.idaccounting=4 and o.idtypeoperation=2) qq

update @AnalSal 
set AmountPay=isnull(dd,0)
from (select sum(o.amountoperation) dd
from document d
inner join operation o  with (nolock) on o.iddocument=d.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
where d.idtypedocument=1 and d.idperiod=@idperiod and b.idaccounting<>6) qq

update @AnalSal 
set AmountPayG=isnull(dd,0)
from (select sum(o.amountoperation) dd
from operation o with (nolock)inner join balance b with (nolock) on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and b.idaccounting=3 and o.idtypeoperation=1) qq

update @AnalSal 
set PenyAmount=isnull(dd,0)
from (select sum(o.amountoperation) dd
from operation o with (nolock)inner join balance b with (nolock) on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and b.idaccounting=4 and o.idtypeoperation=1) qq


update @AnalSal 
set AbonenVsego=re.SumIn
from(select  isnull(count(c.idcontract),0) SumIn
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
	and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
	and dbo.fGetStatusPU (@end,gm.idgmeter)=1) re

update @AnalSal 
set abonenBez=re.SumIn-AbonenVsego
from(select  isnull(count(c.idcontract),0) SumIn
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
where	dbo.fGetStatusGobject (g.idgobject,@idperiod)=1 ) re


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
inner join indication i with (nolock)  on gm.idgmeter=i.idgmeter
inner join factuse f with (nolock) on f.idindication=i.idindication
	and f.idperiod=@idPeriod)qq) re

update @AnalSal 
set OtsutBolee=AbonenVsego-re.ff
from (select count(idcontract) ff from (select distinct g.idcontract
from gobject g with (nolock)  
inner join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
inner join indication i with (nolock)  on gm.idgmeter=i.idgmeter
inner join factuse f with (nolock) on f.idindication=i.idindication
	and (f.idperiod=@idPeriod or f.idperiod=@idPeriodPrev)) qq) re


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
set AbonenBezPU=abs(KGNachN/@NormaC)

select * from @AnalSal










GO