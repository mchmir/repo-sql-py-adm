SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO



CREATE procedure [dbo].[repConsumptionPoRU] (@dBegin datetime, @dEnd datetime) as 
------------************Отчёт потребление по РУ***********----------
SET NOCOUNT ON 
--declare @dBegin datetime
--declare @dEnd datetime
--set @dBegin='2007-09-01'
--set @dEnd='2007-09-30'


create table #tmprep (IdGRU int, adres varchar(100), InvNumber varchar(20), Agent varchar(100), FactUse float, indic float, contr float, indicAct float,)

insert #tmprep (IdGRU, InvNumber, adres, Agent)
select IdGRU, gru.invnumber, gru.name, a.name 
from GRU  with (nolock) 
inner join agent a with (nolock)  on a.idagent=gru.idagent
--where isnull(idtypegru,0)<>1

update #tmprep
set FactUse=qq.fact
from (select idgru, sum(factamount) fact
from gobject q  with (nolock) 
inner join factuse f with (nolock)  on f.idgobject=q.idgobject
inner join indication i with (nolock)  on f.idindication=i.idindication
where dbo.DateOnly(i.DateDisplay)>=dbo.DateOnly(@dBegin) and dbo.DateOnly(i.DateDisplay)<=dbo.DateOnly(@dEnd) and isnull(f.IdTypeFU,0)=1
group by idgru) qq
inner join #tmprep s on s.IdGRU=qq.idgru

update #tmprep
set indic=p.ff
from (select gr.idgru, sum(gg) ff
from (select g.idgobject, case when count(g.idgobject)>1 then 1 else 1 end gg
from  gobject g with (nolock)  
inner join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
 	and gm.idstatusgmeter=1 and g.idstatusgobject=1
inner join indication i with (nolock)  on i.idgmeter=gm.idgmeter
where dbo.DateOnly(i.DateDisplay)>=dbo.DateOnly(@dBegin) and dbo.DateOnly(i.DateDisplay)<=dbo.DateOnly(@dEnd)
group by g.idgobject)rr 
inner join gobject gr with (nolock) on gr.idgobject=rr.idgobject group by gr.idgru)p 
inner join #tmprep gg on p.idgru=gg.idgru

update #tmprep
set contr=z.contr
from (select g.idGRU,  isnull(count(c.idcontract),0) contr
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and g.idstatusgobject=1
inner join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
and  gm.idstatusgmeter=1
group by g.idGRU)z inner join #tmprep gg on z.idgru=gg.idgru

update #tmprep
set indicAct=p.ff
from (select gr.idgru, sum(gg) ff
from (select g.idgobject, case when count(g.idgobject)>1 then 1 else 1 end gg
from  gobject g with (nolock)  
inner join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
 	and gm.idstatusgmeter=1 and g.idstatusgobject=1
inner join indication i with (nolock)  on i.idgmeter=gm.idgmeter
where dbo.DateOnly(i.DateDisplay)>=dbo.DateOnly(@dBegin) 
and dbo.DateOnly(i.DateDisplay)<=dbo.DateOnly(@dEnd)
--and i.idtypeindication=3
---------------------- 23 may 2019------------------
and i.idtypeindication IN (3,7)  ---- по Акту и Фото
----------------------------------------------------
group by g.idgobject)rr 
inner join gobject gr with (nolock) on gr.idgobject=rr.idgobject group by gr.idgru)p 
inner join #tmprep gg on p.idgru=gg.idgru


update #tmprep set factuse=0 where factuse is null

select *
from #tmprep
order by  Agent,  InvNumber
drop table #tmprep

GO