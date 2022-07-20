SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO









CREATE procedure [dbo].[repSpisokDomPodkKGS] (@idAgent int) as




	select  left(c.account,4) as account,gru.invnumber, s.name+' '+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'') addres ,
	convert(int ,0) con, convert(float ,0) KolPro,convert(int ,0) conot, convert(float ,0) KolProot, ag.name, ag.idagent
	into #tmpForddd
	from contract c with (nolock) 
	inner join GObject g with (nolock) on g.idcontract=c.idcontract 
	--and IDStatusGObject=2
	 inner join GRU with (nolock) on gru.idgru=g.idgru 
	inner join address a with (nolock) on a.idaddress=g.idaddress
	inner join house h with (nolock) on h.idhouse=a.idhouse
	inner join street s with (nolock) on s.idstreet=a.idstreet
	inner join agent ag on ag.idagent=gru.idagent 
	 group by left(c.account,4),gru.invnumber ,s.name,h.housenumber,h.housenumberchar, ag.name, ag.idagent
	order by invnumber

if @idAgent>0
begin
	delete from #tmpForddd where idagent<>@idAgent
end

update #tmpForddd
set con=ss.con1,
KolPro=ss.KolPro1
from #tmpForddd dd
inner join ( select  left(c.account,4) as account1--s.name+' '+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'') addres1 
,count(c.idcontract) con1, sum(isnull(CountLives,0)) KolPro1
from contract c with (nolock) 
inner join GObject g with (nolock) on g.idcontract=c.idcontract 
inner join GRU with (nolock) on gru.idgru=g.idgru 
inner join address a with (nolock) on a.idaddress=g.idaddress
inner join house h with (nolock) on h.idhouse=a.idhouse
inner join street s with (nolock) on s.idstreet=a.idstreet
where --dbo.fGetStatusGobject (g.idgobject, 132)=1 
IDStatusGObject=1
 group by left(c.account,4)--s.name,h.housenumber,h.housenumberchar
) ss on ss.account1=dd.account

update #tmpForddd
set conot=ss.con1,
KolProot=ss.KolPro1
from #tmpForddd dd
inner join ( select   left(c.account,4) as account1--s.name+' '+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'') addres1 
,count(c.idcontract) con1, sum(isnull(CountLives,0)) KolPro1
from contract c with (nolock) 
inner join GObject g with (nolock) on g.idcontract=c.idcontract 
inner join GRU with (nolock) on gru.idgru=g.idgru 
inner join address a with (nolock) on a.idaddress=g.idaddress
inner join house h with (nolock) on h.idhouse=a.idhouse
inner join street s with (nolock) on s.idstreet=a.idstreet
where --dbo.fGetStatusGobject (g.idgobject, 132)=2
IDStatusGObject=2
 group by left(c.account,4)--s.name,h.housenumber,h.housenumberchar
) ss on ss.account1=dd.account



select * from #tmpForddd
drop table #tmpForddd





GO