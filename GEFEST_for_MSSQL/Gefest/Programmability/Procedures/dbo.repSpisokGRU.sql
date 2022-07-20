SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO







CREATE procedure [dbo].[repSpisokGRU] (@idagent int, @idgru int) as


--declare @idagent int
--declare @idgru int
--set @idagent=0
--set @idgru=0


create table #tmpGrap (idgru int, memo  varchar(20),invnumber  varchar(20),   countvsego int, countOtkl int, 
	countSPU int, countBezPU int,  agent  varchar(50), address varchar(200),StatusZakr int)

declare @q varchar(8000)
set @q='insert #tmpGrap (idgru, memo,invnumber,countvsego,agent,address)
select gru.idgru, gru.memo, gru.invnumber,count(c.idcontract),ag.name, s.name+'' ''+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'''') 
from contract c with (nolock) 
inner join GObject g with (nolock) on g.idcontract=c.idcontract '
set @q=@q+' inner join GRU with (nolock) on gru.idgru=g.idgru 
inner join address a with (nolock) on a.idaddress=gru.idaddress
inner join house h with (nolock) on h.idhouse=a.idhouse
inner join street s with (nolock) on s.idstreet=a.idstreet'


if (isnull(@idgru,0)>0)
	set @q=@q+' and g.idgru='+ltrim(str(@idgru))

set @q=@q+' inner join agent ag with (nolock) on ag.idagent=gru.idagent'

if (isnull(@idagent,0)>0)
	set @q=@q+' and ag.idagent='+ltrim(str(@idagent))

set @q=@q+' group by gru.idgru, gru.memo, gru.invnumber,ag.name,s.name,h.housenumber,h.housenumberchar'


exec (@q)

update #tmpGrap
set countOtkl=f.cc
from #tmpGrap g
inner join (select t.idgru,count(c.idcontract) cc
from #tmpGrap t
inner join GObject g with (nolock) on t.idgru=g.idgru
and idstatusgobject=2
inner join contract c  with (nolock) on c.idcontract=g.idcontract group by t.idgru ) f on f.idgru=g.idgru


update #tmpGrap
set countSPU=f.cc
from #tmpGrap g
inner join (select g.idgru,count(c.idcontract) cc
from GObject g with (nolock) 
left join gmeter go on go.idgobject=g.idgobject
and go.idstatusgmeter=1 and idstatusgobject=1
inner join contract c  with (nolock) on c.idcontract=g.idcontract where go.idgmeter is not null group by g.idgru ) f on f.idgru=g.idgru

update #tmpGrap
set countBezPU=isnull(f.cc,0)-isnull(countOtkl,0)
from #tmpGrap g
inner join (select g.idgru,count(c.idcontract) cc
from GObject g with (nolock) 
left join gmeter go on go.idgobject=g.idgobject
and go.idstatusgmeter=1 and idstatusgobject=1
inner join contract c  with (nolock) on c.idcontract=g.idcontract where go.idgmeter is null group by g.idgru ) f on f.idgru=g.idgru

update #tmpGrap
set StatusZakr=f.cc
from #tmpGrap g
inner join (select t.idgru,count(c.idcontract) cc
from #tmpGrap t
inner join GObject g with (nolock) on t.idgru=g.idgru
inner join contract c  with (nolock) on c.idcontract=g.idcontract and isnull(c.status,0)=2 group by t.idgru ) f on f.idgru=g.idgru

--update #tmpGrap
--set countBezPU=countvsego-countSPU
--from #tmpGrap g



select * from #tmpGrap order by agent,memo
drop table #tmpGrap



GO