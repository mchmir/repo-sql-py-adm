SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO



CREATE procedure [dbo].[repDoubleIndication] (@idperiod int, @idgru int, @idagent int, @countind int) as 

--declare @idperiod int
--declare @idagent int
--declare @idgru int
--declare @countind int
--set @countind=0
--set @idperiod=36
--set @idagent=10
--select @idgru=0--(select idgru from gru where invnumber='05014')



declare @dEnd datetime
set @dEnd=dbo.fGetDatePeriod(@IdPeriod,1)
-- declare @idprevper int
-- set @idprevper=dbo.fGetPredPeriodVariable(@idperiod)

create table #tmpGrap (idcontract int, account  varchar(20),invnumber  varchar(20),  FIO varchar(500), address varchar(500), flat varchar(20) , idstatusgobject int, countlives int, 
	idstatusgmeter int, gru varchar(100),  agent  varchar(50), LastIndication float, LastIndicationDate datetime,
	memo varchar(200), NumberGmeter varchar(100), TypeGmeter varchar(100), idgmeter int, factuse float, countind int, DateVerify datetime, DateAkt datetime)

declare @q varchar(8000)
set @q='insert #tmpGrap (idcontract, account, FIO, address, idstatusgobject, countlives, 
	idstatusgmeter, gru,  agent, invnumber,flat, idgmeter,memo,DateVerify)
select c.idcontract,  c.account, case when isnull(isJuridical,0)=0 then ltrim(p.surname)+'' ''+ left(isnull(p.name,''''),1)+'' ''+left(isnull(p.patronic,''''),1) else ltrim(p.surname) end FIO, 
	s.name+'' ''+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'''') address,
	g.idstatusgobject, g.countlives, isnull(gm.idstatusgmeter,2) idstatusgmeter,
	gru.name gru, ag.name agent, gru.invnumber,a.flat, gm.idgmeter, c.Memo, gm.DateVerify
from contract c with (nolock) 
inner join person p with (nolock) on p.idperson=c.idperson
inner join GObject g with (nolock) on g.idcontract=c.idcontract and isnull(status,0)<>2 and g.idstatusgobject=1
 inner join GRU with (nolock) on gru.idgru=g.idgru'

if (isnull(@idgru,0)>0)
	set @q=@q+' and g.idgru='+ltrim(str(@idgru))

set @q=@q+' inner join agent ag with (nolock) on ag.idagent=gru.idagent'

if (isnull(@idagent,0)>0)
	set @q=@q+' and ag.idagent='+ltrim(str(@idagent))

set @q=@q+' inner join address a with (nolock) on a.idaddress=g.idaddress
inner join house h on h.idhouse=a.idhouse'

set @q=@q+' inner join street s with (nolock) on s.idstreet=a.idstreet
left join gmeter gm on gm.idgobject=g.idgobject and idstatusgmeter=1'

exec(@q)

update #tmpGrap
set numbergmeter=isnull(g.SerialNumber,'б/н'),
typegmeter=ltrim(tt.name)+',' +ltrim(str(tt.ClassAccuracy))
from #tmpGrap t
inner join gmeter g with (nolock) on g.idgmeter=t.idgmeter
inner join typegmeter tt with (nolock) on tt.idtypegmeter=g.idtypegmeter

update #tmpGrap
set LastIndication=dbo.fLastIndication(IdGMeter, @dEnd+1)
,LastIndicationDate=dbo.fLastIndicationDate(IdGMeter, @dEnd+1)
where idgmeter is not null

update #tmpGrap
set DateAkt=(select top 1 documentdate from document d with (nolock) inner join pd with (nolock) on pd.iddocument=d.iddocument 
and pd.idtypepd=26  where idcontract=t.idcontract and idtypedocument=20 order by documentdate desc)
from #tmpGrap t

update #tmpGrap
set countind=tt.coon
from #tmpGrap t
inner join ( select count(0) coon, g.idcontract
from gobject g with (nolock)
inner join #tmpGrap t on t.idcontract=g.idcontract
inner join indication i  with (nolock) on i.idgmeter=t.idgmeter
and lastindication=i.display
group by g.idcontract having count(0)>1) tt on t.idcontract=tt.idcontract

if (@countind>0)
begin
delete #tmpGrap
where isnull(countind,0)<@countind 
end

select * from #tmpGrap order by agent, account

drop table #tmpGrap
GO