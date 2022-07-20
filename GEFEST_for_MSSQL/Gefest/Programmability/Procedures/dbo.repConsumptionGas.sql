SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO






CREATE PROCEDURE [dbo].[repConsumptionGas] (@IdPeriod int )  AS
---------***************Отчет о потребление газа*******------------
declare @dEnd datetime
set @dEnd= dbo.fGetDatePeriod(@IdPeriod,0)

select IdGRU, Name,InvNumber, isGM, sum(CL) CL, sn, hn, IdHouse, counn, sumC, count (1) coun, convert(float, 0.00) amountm3, convert(float, 0.00) amountkg, IdStatusGObject
into #cls
from(select g.IdGRU, g.Name,  g.InvNumber, convert(int,dbo.fGetCountLives(o.idgobject, @IdPeriod)) CL, 
	case when gm.IdGmeter is null then 0 else 1 end isGM, s.Name sn, ltrim(str(h.HouseNumber))+h.HouseNumberChar hn,h.IdHouse, 
	convert(int,0)as counn, convert(int,0)as sumC, o.IdStatusGObject
from GRU  g with (nolock) 
inner join GObject o  with (nolock)  on o.IdGRU=g.IdGRU
--and dbo.fGetStatusGobject (o.idgobject,@IdPeriod)=1
inner join contract c  with (nolock) on o.idcontract=c.idcontract
inner join Address a with (nolock) on a.IdAddress=o.IdAddress
inner join House h with (nolock) on h.IdHouse=a.IdHouse
inner join street s with (nolock) on s.IdStreet=h.IdStreet
left join Gmeter gm with (nolock) on gm.IdGObject=o.IdGObject
	and dbo.fGetStatusPU (@dEnd,gm.idgmeter)=1) qq
group by IdHouse, sn, hn, isGM, IdGRU, Name,  InvNumber, counn, sumC, IdStatusGObject

update #cls
set counn=convert(int,ff.summ)
from(select gru.idgru, count(idcontract) summ
from gobject g with (nolock)
inner join gru with (nolock) on gru.idgru=g.idgru
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
group by gru.idgru) ff inner join #cls c on c.idgru=ff.idgru

update #cls
set sumC=convert(int,ff.summ)
from(select count(idcontract) summ
from gobject g with (nolock)
inner join gru with (nolock) on gru.idgru=g.idgru and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1) ff

update #cls
set amountm3=ff.summ
from (select sum(f.factamount) summ, a.idhouse, case when gm.IdGmeter is null then 0 else 1 end isGM, g.idgru, IdStatusGObject
from GRU  g with (nolock)
inner join GObject o  with (nolock)  on o.IdGRU=g.IdGRU
--	and o.IdStatusGObject=1
inner join Address a with (nolock) on a.IdAddress=o.IdAddress
inner join factuse f  with (nolock) on f.idgobject=o.idgobject
	and f.idperiod=@IdPeriod
	and isnull(f.idtypefu,0)=1
left join Gmeter gm with (nolock) on gm.IdGObject=o.IdGObject
and dbo.fGetStatusPU (@dEnd,gm.idgmeter)=1
group by IdHouse, case when gm.IdGmeter is null then 0 else 1 end, g.idgru, IdStatusGObject) ff
inner join #cls c on c.idhouse=ff.idhouse and c.isGM=ff.isGM and ff.idgru=c.idgru and c.IdStatusGObject=ff.IdStatusGObject

update #cls
set amountkg=ff.summ
from (select sum(f.factamount) summ, a.idhouse, case when gm.IdGmeter is null then 0 else 1 end isGM, g.idgru, IdStatusGObject
from GRU  g with (nolock)
inner join GObject o  with (nolock)  on o.IdGRU=g.IdGRU
--	and o.IdStatusGObject=1
inner join Address a with (nolock) on a.IdAddress=o.IdAddress
inner join factuse f  with (nolock) on f.idgobject=o.idgobject
	and f.idperiod=@IdPeriod
	--and isnull(f.idtypefu,0)=2
and f.idtypefu=2
left join Gmeter gm with (nolock) on gm.IdGObject=o.IdGObject
and dbo.fGetStatusPU (@dEnd,gm.idgmeter)=1
group by IdHouse, case when gm.IdGmeter is null then 0 else 1 end, g.idgru, IdStatusGObject) ff
inner join #cls c on c.idhouse=ff.idhouse and c.isGM=ff.isGM and ff.idgru=c.idgru  and c.IdStatusGObject=ff.IdStatusGObject

select @IDPEriod, * from #cls
order by InvNumber
--select sum(cl) from #cls
drop table #cls





GO