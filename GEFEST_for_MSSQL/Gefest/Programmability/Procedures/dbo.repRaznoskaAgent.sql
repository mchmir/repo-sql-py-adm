SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO














CREATE procedure [dbo].[repRaznoskaAgent] (@idperiod int) as 
SET NOCOUNT ON

declare @dBegin datetime
declare @dEnd datetime
set @dBegin= dbo.fGetDatePeriod(@IdPeriod,1)
set @dEnd= dbo.fGetDatePeriod(@IdPeriod,0)

create table #anaA (Iduser int, indicationPro int, nameoperator varchar(20),  indicationTelefon int, indicationContrAkt int,indicationContr INT,indicationWhatsApp int)

--insert #anaA (iduser, nameoperator, indicationPro, indicationContr, indicationTelefon)
--select 9999,'--++--', 0, 0,0

insert #anaA (iduser, nameoperator, indicationPro, indicationContrAkt, indicationContr, indicationTelefon, indicationWhatsApp)
select idagent,name, 0, 0, 0, 0, 0
from agent  with (nolock)
where idtypeagent=1
--where idagent in (19,10,127,17,126,15,12,81,1,30,35,112,115,117,122,124,131,133,135)
--where idagent in (19,10,106,17,11,25,9,15,12,14,81,1,67,30,35,31,102,109,112)


update #anaA
set indicationTelefon=coun
from(select  count (idindication) coun ,isnull(gru.idagent,9999) iduser
from indication i  with (nolock)
inner join gmeter g with (nolock)  on i.idgmeter=g.idgmeter
--and dbo.dateonly(i.dateadd)>=dbo.dateonly(@dBegin) and dbo.dateonly(i.dateadd)<=dbo.dateonly(@dEnd) 
inner join gobject q with (nolock) on g.idgobject=q.idgobject
inner join gru with (nolock) on gru.idgru=q.idgru
where dbo.dateonly(i.datedisplay)>=dbo.dateonly(@dBegin) and dbo.dateonly(i.datedisplay)<=dbo.dateonly(@dEnd) 
and isnull(idtypeindication,0)=1 
group by gru.idagent)dd inner join #anaA a on a.iduser=dd.iduser

update #anaA
set indicationContr=coun
from(select  count (idindication) coun ,isnull(gru.idagent,9999) iduser
from indication i  with (nolock)
inner join gmeter g with (nolock)  on i.idgmeter=g.idgmeter
--and dbo.dateonly(i.dateadd)>=dbo.dateonly(@dBegin) and dbo.dateonly(i.dateadd)<=dbo.dateonly(@dEnd)
inner join gobject q with (nolock) on g.idgobject=q.idgobject
inner join gru with (nolock) on gru.idgru=q.idgru
where dbo.dateonly(i.datedisplay)>=dbo.dateonly(@dBegin) and dbo.dateonly(i.datedisplay)<=dbo.dateonly(@dEnd)
and isnull(idtypeindication,0)=2--(isnull(idtypeindication,0)=2 or isnull(idtypeindication,0)=3)
group by gru.idagent)dd inner join #anaA a on a.iduser=dd.iduser

--update #anaA
--set indicationContrAkt=coun
--from(select  count (1) coun ,gru.idagent iduser
--from indication i  with (nolock)
--inner join gmeter g with (nolock)  on i.idgmeter=g.idgmeter
--and dbo.dateonly(i.dateadd)>=dbo.dateonly(@dBegin) and dbo.dateonly(i.dateadd)<=dbo.dateonly(@dEnd) and isnull(idtypeindication,0)=3
--inner join gobject q with (nolock) on g.idgobject=q.idgobject
----inner join #anaA aa on gru.idgru=q.
--inner join gru with (nolock) on gru.idgru=q.idgru
--group by gru.idagent)dd inner join #anaA a on a.iduser=dd.iduser

update #anaA
set indicationContrAkt=coun
from(select  count (idindication) coun ,isnull(i.idagent,9999) iduser
from indication i  with (nolock)
inner join gmeter g with (nolock)  on i.idgmeter=g.idgmeter
--and dbo.dateonly(i.dateadd)>=dbo.dateonly(@dBegin) and dbo.dateonly(i.dateadd)<=dbo.dateonly(@dEnd) 
inner join gobject q with (nolock) on g.idgobject=q.idgobject
--inner join #anaA aa on gru.idgru=q.
inner join gru with (nolock) on gru.idgru=q.idgru
where dbo.dateonly(i.datedisplay)>=dbo.dateonly(@dBegin) and dbo.dateonly(i.datedisplay)<=dbo.dateonly(@dEnd)  
and isnull(idtypeindication,0)=3
group by i.idagent)dd inner join #anaA a on a.iduser=dd.iduser

------ 23 may 2019 ----------------------------
update #anaA
set indicationWhatsApp=ISNULL(coun,0)
from(select  count (idindication) coun ,isnull(gru.idagent,9999) iduser
from indication i  with (nolock)
inner join gmeter g with (nolock)  on i.idgmeter=g.idgmeter
--and dbo.dateonly(i.dateadd)>=dbo.dateonly(@dBegin) and dbo.dateonly(i.dateadd)<=dbo.dateonly(@dEnd) 
inner join gobject q with (nolock) on g.idgobject=q.idgobject
inner join gru with (nolock) on gru.idgru=q.idgru
where dbo.dateonly(i.datedisplay)>=dbo.dateonly(@dBegin) and dbo.dateonly(i.datedisplay)<=dbo.dateonly(@dEnd) 
and (isnull(idtypeindication,0)=7 or isnull(idtypeindication,0)=8)
group by gru.idagent)dd inner join #anaA a on a.iduser=dd.iduser
-----------------------------------------------------------

update #anaA
set indicationPro=coun
from(select  count (idindication) coun ,isnull(gru.idagent,9999) iduser
from indication i  with (nolock)
inner join gmeter g with (nolock)  on i.idgmeter=g.idgmeter
--and dbo.dateonly(i.dateadd)>=dbo.dateonly(@dBegin) and dbo.dateonly(i.dateadd)<=dbo.dateonly(@dEnd) 
inner join gobject q with (nolock) on g.idgobject=q.idgobject
inner join gru with (nolock) on gru.idgru=q.idgru
where dbo.dateonly(i.datedisplay)>=dbo.dateonly(@dBegin) and dbo.dateonly(i.datedisplay)<=dbo.dateonly(@dEnd) 
and (isnull(idtypeindication,0)=4 or isnull(idtypeindication,0)=5)
group by gru.idagent)dd inner join #anaA a on a.iduser=dd.iduser

update #anaA
set indicationPro=indicationPro+coun
from(select  count (idindication) coun ,isnull(gru.idagent,9999) iduser
from indication i  with (nolock)
inner join gmeter g with (nolock)  on i.idgmeter=g.idgmeter
--and dbo.dateonly(i.dateadd)>=dbo.dateonly(@dBegin) and dbo.dateonly(i.dateadd)<=dbo.dateonly(@dEnd) 
inner join gobject q with (nolock) on g.idgobject=q.idgobject
--inner join #anaA aa on gru.idgru=q.
inner join gru with (nolock) on gru.idgru=q.idgru
where dbo.dateonly(i.datedisplay)>=dbo.dateonly(@dBegin) and dbo.dateonly(i.datedisplay)<=dbo.dateonly(@dEnd)  
and isnull(idtypeindication,0)=3
group by gru.idagent)dd inner join #anaA a on a.iduser=dd.iduser

update #anaA
set indicationPro=indicationPro-indicationContrAkt

delete from #anaA 
where  indicationContr=0 and indicationTelefon=0 and indicationPro=0 AND indicationContrAkt=0 AND indicationWhatsApp=0

insert #anaA (iduser, nameoperator, indicationPro, indicationContrAkt, indicationContr, indicationTelefon, indicationWhatsApp)
select  37,'Личный кабинет',count (idindication),0,0,0,0
from dbo.Contract c
inner join dbo.GObject g with (nolock) on g.IDContract=c.IDContract
inner join dbo.GMeter gm with (nolock) on gm.IDGObject=g.IDGObject
inner join dbo.Indication i with (nolock) on i.IdGMeter=gm.IdGMeter
where i.iduser=37 and dbo.dateonly(i.datedisplay)>=dbo.dateonly(@dBegin) and dbo.dateonly(i.datedisplay)<=dbo.dateonly(@dEnd)


select * from #anaA
order by nameoperator
drop table #anaA











GO