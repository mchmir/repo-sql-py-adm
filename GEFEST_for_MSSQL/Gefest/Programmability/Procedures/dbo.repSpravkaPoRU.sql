SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO


CREATE procedure [dbo].[repSpravkaPoRU] (@idperiod int, @idgru int, @idagent int, @idhouse int, @count int, @fact int) as 

--declare @idperiod int
--declare @idagent int
--declare @idgru int
--declare @idhouse int
--declare @count int
--declare @fact int
--set @idperiod=25
--set @idagent=10
--set @idgru=8497
--set @count=-1
--set @fact=0

declare @dEnd datetime
set @dEnd=dbo.fGetDatePeriod(@IdPeriod,1)
declare @idprevper int
set @idprevper=dbo.fGetPredPeriodVariable(@idperiod)

create table #tmpGrap (idcontract int, account  varchar(20),invnumber  varchar(20),  FIO varchar(500), address varchar(500), flat varchar(20) , idstatusgobject int, countlives int, 
	idstatusgmeter int, gru varchar(100),  agent  varchar(50), LastIndication float, LastIndicationDate datetime,
 EndIndication float, EndIndicationDate datetime,
	NumberGmeter varchar(100), TypeGmeter varchar(100), idgmeter int, factuse float, tipindicationend varchar(100),tipindicationlast varchar(100),memo varchar(8000))

declare @q varchar(8000)
set @q='insert #tmpGrap (idcontract, account, FIO, address, idstatusgobject, countlives, 
	idstatusgmeter, gru,  agent, invnumber,flat, idgmeter, memo)
select c.idcontract, c.account, p.surname+'' ''+ left(ltrim(isnull(p.name,'''')),1)+'' ''+left(ltrim(isnull(p.patronic,'''')),1) FIO, 
	s.name+'' ''+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'''') address,
	g.idstatusgobject, g.countlives, isnull(gm.idstatusgmeter,2) idstatusgmeter,
	gru.name gru, ag.name agent, gru.invnumber,a.flat, gm.idgmeter,c.memo
from contract c with (nolock) 
inner join person p with (nolock) on p.idperson=c.idperson
inner join GObject g with (nolock) on g.idcontract=c.idcontract '
set @q=@q+' inner join GRU with (nolock) on gru.idgru=g.idgru'

if (isnull(@idgru,0)>0)
	set @q=@q+' and g.idgru='+ltrim(str(@idgru))

set @q=@q+' inner join agent ag with (nolock) on ag.idagent=gru.idagent'

if (isnull(@idagent,0)>0)
	set @q=@q+' and ag.idagent='+ltrim(str(@idagent))

set @q=@q+' inner join address a with (nolock) on a.idaddress=g.idaddress
inner join house h on h.idhouse=a.idhouse'

set @q=@q+' inner join street s with (nolock) on s.idstreet=a.idstreet
left join gmeter gm on gm.idgobject=g.idgobject and idstatusgmeter=1'

exec (@q)

if @idhouse>0
begin
delete #tmpGrap
where account not like '___'+convert(varchar,@idhouse)+'%' 
end
if @count>-1
begin
delete #tmpGrap
where countlives<>@count
end

update #tmpGrap
set numbergmeter=isnull(g.SerialNumber,'б/н'),
typegmeter=ltrim(tt.name)+',' +ltrim(str(tt.ClassAccuracy,3,1))
from #tmpGrap t
inner join gmeter g with (nolock) on g.idgmeter=t.idgmeter
inner join typegmeter tt with (nolock) on tt.idtypegmeter=g.idtypegmeter

update #tmpGrap
set LastIndication=dbo.fLastIndication(IdGMeter, @dEnd-1)
,LastIndicationDate=dbo.fLastIndicationDate(IdGMeter, @dEnd-1),
tipindicationLast=dbo.fLastIndicationDateType(IdGMeter, @dEnd-1),
EndIndication=dbo.fLastIndicationIDPeriodGet(IdGMeter,@idperiod)
,EndIndicationDate=dbo.fLastIndicationIDPeriodGetdate(IdGMeter,@idperiod),
tipindicationend=dbo.fLastIndicationIDPeriodGetType(IdGMeter,@idperiod)
where idgmeter is not null


update #tmpGrap
set factuse=tt.ss
from #tmpGrap t
inner join (select g.idcontract, sum(f.factamount)ss from #tmpGrap r 
inner join gobject g with (nolock) on r.idcontract=g.idcontract
inner join factuse f with (nolock) on f.idgobject=g.idgobject
and idtypefu=1 and f.idperiod=@IdPeriod group by g.idcontract)tt on t.idcontract=tt.idcontract
delete #tmpGrap where idstatusgmeter<>1 and isnull(factuse,0)=0
if @fact=0
delete #tmpGrap where isnull(factuse,0)<>0

select * from #tmpGrap order by account

drop table #tmpGrap

GO