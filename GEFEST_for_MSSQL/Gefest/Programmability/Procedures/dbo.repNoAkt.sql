SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

CREATE procedure [dbo].[repNoAkt] (@idperiod int, @idgru int, @idagent int) as 

--declare @idperiod int
--declare @idagent int
--declare @idgru int
--set @idperiod=2
--set @idagent=0
--select @idgru=0



declare @dEnd datetime
set @dEnd=dbo.fGetDatePeriod(@IdPeriod,0)
-- declare @idprevper int
-- set @idprevper=dbo.fGetPredPeriodVariable(@idperiod)

create table #tmpGrap (idcontract int, account  varchar(20),invnumber  varchar(20), 
 FIO varchar(500), address varchar(500), flat varchar(20) ,  countlives int, 
	 agent  varchar(50), datedoc datetime,iddoc int, idstayusgmeter int, dateindication datetime, idgmeter int, LastIndication float, memo varchar(2000))

declare @q varchar(8000)
set @q='insert #tmpGrap (idcontract, account, FIO, address,  countlives, 
	  agent, invnumber, idstayusgmeter, idgmeter,memo )
select c.idcontract,  c.account, case when isnull(isJuridical,0)=0 then ltrim(p.surname)+'' ''+ left(isnull(p.name,''''),1)+'' ''+left(isnull(p.patronic,''''),1) else ltrim(p.surname) end FIO, 
	s.name+'' ''+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'''')+''-''+isnull(a.flat,'''') address,
	 g.countlives, ag.name agent, gru.invnumber, gm.idstatusgmeter, gm.idgmeter,c.memo

from contract c with (nolock) 
inner join person p with (nolock) on p.idperson=c.idperson
inner join GObject g with (nolock) on g.idcontract=c.idcontract and idstatusgobject=1
inner join GRU with (nolock) on gru.idgru=g.idgru'
if (isnull(@idgru,0)>0)
	set @q=@q+' and g.idgru='+ltrim(str(@idgru))
set @q=@q+' inner join agent ag with (nolock) on ag.idagent=gru.idagent'
if (isnull(@idagent,0)>0)
	set @q=@q+' and ag.idagent='+ltrim(str(@idagent))
set @q=@q+' inner join address a with (nolock) on a.idaddress=g.idaddress
inner join house h on h.idhouse=a.idhouse
inner join street s with (nolock) on s.idstreet=a.idstreet
left join gmeter gm on gm.idgobject=g.idgobject and idstatusgmeter=1'


exec (@q)

delete #tmpGrap where idstayusgmeter is null
update #tmpGrap set LastIndication=(select top 1 Display from indication i with (nolock) 
where fff.idgmeter=i.idgmeter and i.idtypeindication=3 order by idindication desc )
from #tmpGrap fff 

update #tmpGrap set dateindication=(select top 1 DateDisplay from indication i with (nolock) 
where fff.idgmeter=i.idgmeter and i.idtypeindication=3 order by idindication desc )
from #tmpGrap fff 

update #tmpGrap
set datedoc=(select top 1 dd.documentdate from document dd with (nolock)
where dd.idtypedocument=20 and dd.idcontract=fff.idcontract
order by dd.iddocument desc)

from #tmpGrap fff 

delete #tmpGrap 
where left(convert(varchar, dateadd(yy, @idperiod, datedoc), 20), 10)>Getdate() or left(convert(varchar, dateadd(yy, @idperiod, dateindication), 20), 10)>Getdate()

select * from #tmpGrap order by agent, account

drop table #tmpGrap



GO