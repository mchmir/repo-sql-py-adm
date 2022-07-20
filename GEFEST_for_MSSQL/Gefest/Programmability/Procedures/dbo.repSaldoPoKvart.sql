SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO














CREATE PROCEDURE [dbo].[repSaldoPoKvart] (@idperiod int, @idagent int, @amountbalance float, @idstatusgobject int,  @idstatusgmeter int, @isjuridical int) AS
--declare @idperiod int
--declare @idagent int
----
----
--declare @amountbalance float
--declare @idstatusgobject int
----
--declare @idstatusgmeter int
--set @idperiod=65
--set @idagent=0
--set @idstatusgmeter=0
--set @amountbalance=0
--set @idstatusgobject=2

declare @IdAccounting int
declare @idprevper int
set @idprevper=dbo.fGetPredPeriodVariable(@idperiod)

declare @tbl table (idcontract int, idperiod int)

create table #tmpBalancecc (idcontract int, account  varchar(20),invnumber  varchar(200),  FIO varchar(500), 
	address varchar(500), flat varchar(20) , idstatusgobject varchar(100), countlives int, 
	idstatusgmeter int, gru varchar(100),  agent  varchar(50), beginbalance float, amountnach float,
	amountopl float, endbalance float,Uridikal varchar(100),UridikalDate datetime, amountUr float, amountUrg float)
declare @q varchar(8000)

set @IdAccounting=0
insert into @tbl (idcontract, idperiod)
select  b.idcontract, max(b.idperiod) idperiod
from balance b  with (nolock)
where b.idperiod<=@idprevper 
	and (IdAccounting=@IdAccounting or @IdAccounting=0)
group by b.idcontract
order by b.idcontract

create table #BB (abalance float,idcontract int) 
insert into #BB (abalance,idcontract)
select sum(b.amountbalance) abalance, b.idcontract 
from contract ct (nolock)
inner join balance b  with (nolock) on b.idcontract=ct.idcontract
inner join @tbl t on t.idcontract=b.idcontract and t.idperiod=b.idperiod
where (IdAccounting=@IdAccounting or @IdAccounting=0)
group by b.idcontract,b.idperiod
order by b.idcontract

set @IdAccounting=0
delete from @tbl
insert into @tbl (idcontract, idperiod)
select  b.idcontract, max(b.idperiod) idperiod
from balance b  with (nolock)
where b.idperiod<=@idperiod 
	and (IdAccounting=@IdAccounting or @IdAccounting=0)
group by b.idcontract
order by b.idcontract


create table #EB  (abalance float,idcontract int,idperiod int) 
insert into #EB (abalance,idcontract,idperiod)
select sum(b.amountbalance) abalance, b.idcontract, b.idperiod 
from balance b  with (nolock)
inner join @tbl t on t.idcontract=b.idcontract and t.idperiod=b.idperiod
where (IdAccounting=@IdAccounting or @IdAccounting=0)
group by b.idcontract,b.idperiod
order by b.idcontract

set @IdAccounting=2
delete from @tbl
insert into @tbl (idcontract, idperiod)
select  b.idcontract, max(b.idperiod) idperiod
from balance b  with (nolock)
--where b.idperiod<=@idperiod 
where b.idperiod=@idperiod 
	and (IdAccounting=@IdAccounting or @IdAccounting=0)
and b.amountbalance <0
group by b.idcontract
order by b.idcontract


create table #UR (abalance float,idcontract int) 
insert into #UR (abalance,idcontract)
select sum(b.amountbalance) abalance, b.idcontract 
from balance b  with (nolock)
inner join @tbl t on t.idcontract=b.idcontract and t.idperiod=b.idperiod
where (IdAccounting=@IdAccounting or @IdAccounting=0)
group by b.idcontract,b.idperiod
order by b.idcontract

set @IdAccounting=3
delete from @tbl
insert into @tbl (idcontract, idperiod)
select  b.idcontract, max(b.idperiod) idperiod
from balance b  with (nolock)
--where b.idperiod<=@idperiod 
where b.idperiod=@idperiod 
	and (IdAccounting=@IdAccounting or @IdAccounting=0)
group by b.idcontract
order by b.idcontract

create table #URG (abalance float,idcontract int) 
insert into #URG (abalance,idcontract)
select sum(b.amountbalance) abalance, b.idcontract 
from balance b  with (nolock)
inner join @tbl t on t.idcontract=b.idcontract and t.idperiod=b.idperiod
where (IdAccounting=@IdAccounting or @IdAccounting=0)
group by b.idcontract,b.idperiod
order by b.idcontract


set @q=' insert #tmpBalancecc (idcontract, account, FIO, address, idstatusgobject, countlives, 
	idstatusgmeter, gru,  agent, invnumber,flat,beginbalance,endbalance,amountUr,amountUrg)
select distinct c.idcontract, c.account, ltrim(p.surname)+'' ''+ ltrim(isnull(p.name,''''))+'' ''+ltrim(isnull(p.patronic,'''')) FIO, 
	s.name+'' ''+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'''') address,
	g.idstatusgobject, g.countlives, isnull(gm.idstatusgmeter,2) idstatusgmeter,
	gru.name gru, ag.name agent, gru.invnumber,a.flat,bb.abalance, eb.abalance, ur.abalance, urg.abalance'--bb.abalance, eb.abalance, ur.abalance, urg.abalance'
set @q=@q+' from contract c with (nolock) 
inner join person p with (nolock) on p.idperson=c.idperson'

if(@isjuridical=1)
	set @q=@q+' and p.isjuridical=1'
set @q=@q+' inner join GObject g with (nolock) on g.idcontract=c.idcontract '

if (isnull(@idstatusgobject,0)>0)
	set @q=@q+' and g.idstatusgobject='+ltrim(str(@idstatusgobject))

set @q=@q+' inner join GRU with (nolock) on gru.idgru=g.idgru'
set @q=@q+' inner join agent ag with (nolock) on ag.idagent=gru.idagent'

if (isnull(@idagent,0)>0)
	set @q=@q+' and ag.idagent='+ltrim(str(@idagent))

set @q=@q+' inner join address a with (nolock) on a.idaddress=g.idaddress
inner join house h on h.idhouse=a.idhouse'


set @q=@q+' inner join street s with (nolock) on s.idstreet=a.idstreet
left join gmeter gm on gm.idgobject=g.idgobject
	and gm.idstatusgmeter=1'

set @q=@q+' left join #bb bb on bb.idcontract=c.idcontract'
set @q=@q+' left join #eb eb on eb.idcontract=c.idcontract'
set @q=@q+' left join #ur ur on ur.idcontract=c.idcontract'
set @q=@q+' left join #urg urg on urg.idcontract=c.idcontract'
set @q=@q+' order by c.idcontract'

exec (@q)

update #tmpBalancecc
set amountnach=-op
from
(select sum(o.amountoperation) op, b.idcontract
from operation o with (nolock) 
inner join balance b with (nolock) on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and o.idtypeoperation<>1 and isnull(numberoperation,0)<>'99999'
--inner join #tmpBalancecc t on t.idcontract=b.idcontract
group by b.idcontract) qq
where qq.idcontract=#tmpBalancecc.idcontract

--
update #tmpBalancecc
set amountopl=op
from
(select sum(o.amountoperation) op, b.idcontract
from operation o with (nolock) 
inner join balance b with (nolock) on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and o.idtypeoperation=1
--inner join #tmpBalancecc t on t.idcontract=b.idcontract
group by b.idcontract) qq
where qq.idcontract=#tmpBalancecc.idcontract
--
update #tmpBalancecc
set Uridikal='Юр.док.',
UridikalDate=qq.datedocument
from
(select d.idcontract,d.documentdate datedocument from document d with (nolock) 
--inner join #tmpBalancecc t on t.idcontract=d.idcontract
where d.idtypedocument=13) qq
where qq.idcontract=#tmpBalancecc.idcontract and (isnull(amountur,0)<0 or isnull(amounturg,0)<0)

update #tmpBalancecc
set idstatusgobject=''
where idstatusgobject=1

update #tmpBalancecc
set idstatusgobject='ОТКЛ'
where idstatusgobject=2

update #tmpBalancecc
set idstatusgobject=(select top 1 name from document d   with (nolock)  
		inner join PD  with (nolock)   on pd.iddocument=d.iddocument 
			and pd.idtypepd=14 
			and d.idtypedocument=17 
			and d.idcontract=#tmpBalancecc.idcontract
		inner join typeend t  with (nolock)   on t.idtypeend=pd.value
		order by d.documentdate desc)
where idstatusgobject='ОТКЛ'

update #tmpBalancecc
set idstatusgobject='ОТКЛ'
where idstatusgobject is null
if (@idstatusgmeter<>0)
delete #tmpBalancecc where idstatusgmeter<>@idstatusgmeter


if (@amountbalance<>0)
begin
if (@amountbalance>0)
select * from #tmpBalancecc where beginbalance>=@amountbalance order by beginbalance, agent
else 
select * from #tmpBalancecc where beginbalance<=@amountbalance order by beginbalance, agent
end
else
select * from #tmpBalancecc  order by  beginbalance, agent

drop table #tmpBalancecc
drop table #BB
drop table #EB
drop table #UR
drop table #URG

--//**старый вариант до 03.04.2013**\\--
--declare @idprevper int
--set @idprevper=dbo.fGetPredPeriodVariable(@idperiod)
--
--create table #tmpBalancecc (idcontract int, account  varchar(20),invnumber  varchar(200),  FIO varchar(500), 
--	address varchar(500), flat varchar(20) , idstatusgobject varchar(100), countlives int, 
--	idstatusgmeter int, gru varchar(100),  agent  varchar(50), beginbalance float, amountnach float,
--	amountopl float, endbalance float,Uridikal varchar(100),UridikalDate datetime, amountUr float, amountUrg float)
------+''-''+a.flat
--declare @q varchar(8000)
--set @q='insert #tmpBalancecc (idcontract, account, FIO, address, idstatusgobject, countlives, 
--	idstatusgmeter, gru,  agent, invnumber,flat)
--select distinct c.idcontract, c.account, ltrim(p.surname)+'' ''+ ltrim(isnull(p.name,''''))+'' ''+ltrim(isnull(p.patronic,'''')) FIO, 
--	s.name+'' ''+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'''') address,
--	g.idstatusgobject, g.countlives, isnull(gm.idstatusgmeter,2) idstatusgmeter,
--	gru.name gru, ag.name agent, gru.invnumber,a.flat
--from contract c with (nolock) 
--inner join person p with (nolock) on p.idperson=c.idperson
--inner join GObject g with (nolock) on g.idcontract=c.idcontract '
--
--if (isnull(@idstatusgobject,0)>0)
--	set @q=@q+' and g.idstatusgobject='+ltrim(str(@idstatusgobject))
--
--set @q=@q+' inner join GRU with (nolock) on gru.idgru=g.idgru'
--set @q=@q+' inner join agent ag with (nolock) on ag.idagent=gru.idagent'
--
--if (isnull(@idagent,0)>0)
--	set @q=@q+' and ag.idagent='+ltrim(str(@idagent))
--
--set @q=@q+' inner join address a with (nolock) on a.idaddress=g.idaddress
--inner join house h on h.idhouse=a.idhouse'
--
--
--set @q=@q+' inner join street s with (nolock) on s.idstreet=a.idstreet
--left join gmeter gm on gm.idgobject=g.idgobject
--	and gm.idstatusgmeter=1'
--exec (@q)
--
--update #tmpBalancecc
--set beginbalance=dbo.fGetLastBalanceReal(@idprevper, idcontract, 0), 
--	endbalance=dbo.fGetLastBalanceReal(@idperiod, idcontract, 0) ,
--amountUr=dbo.fGetLastBalanceReal(@idperiod, idcontract, 2),
--amountUrg=dbo.fGetLastBalanceReal(@idperiod, idcontract, 3)
--
--update #tmpBalancecc
--set amountnach=-op
--from
--(select sum(o.amountoperation) op, t.idcontract
--from operation o with (nolock) 
--inner join balance b with (nolock) on b.idbalance=o.idbalance
--	and b.idperiod=@idperiod
--	and o.idtypeoperation<>1 and isnull(numberoperation,0)<>'99999'
--inner join #tmpBalancecc t on t.idcontract=b.idcontract
--group by t.idcontract) qq
--where qq.idcontract=#tmpBalancecc.idcontract
--
--
--update #tmpBalancecc
--set amountopl=op
--from
--(select sum(o.amountoperation) op, t.idcontract
--from operation o with (nolock) 
--inner join balance b with (nolock) on b.idbalance=o.idbalance
--	and b.idperiod=@idperiod
--	and o.idtypeoperation=1
--inner join #tmpBalancecc t on t.idcontract=b.idcontract
--group by t.idcontract) qq
--where qq.idcontract=#tmpBalancecc.idcontract
--
--update #tmpBalancecc
--set Uridikal='Юр.док.',
--UridikalDate=qq.datedocument
--from
--(select t.idcontract,d.documentdate datedocument from document d with (nolock) 
--inner join #tmpBalancecc t on t.idcontract=d.idcontract
--and d.idtypedocument=13) qq
--where qq.idcontract=#tmpBalancecc.idcontract and (isnull(amountur,0)<0 or isnull(amounturg,0)<0)
--
--update #tmpBalancecc
--set idstatusgobject=''
--where idstatusgobject=1
--
--update #tmpBalancecc
--set idstatusgobject='ОТКЛ'
--where idstatusgobject=2
--
--update #tmpBalancecc
--set idstatusgobject=(select top 1 name from document d   with (nolock)  
--		inner join PD  with (nolock)   on pd.iddocument=d.iddocument 
--			and pd.idtypepd=14 
--			and d.idtypedocument=17 
--			and d.idcontract=#tmpBalancecc.idcontract
--		inner join typeend t  with (nolock)   on t.idtypeend=pd.value
--		order by d.documentdate desc)
--where idstatusgobject='ОТКЛ'
--
--update #tmpBalancecc
--set idstatusgobject='ОТКЛ'
--where idstatusgobject is null
--if (@idstatusgmeter<>0)
--delete #tmpBalancecc where idstatusgmeter<>@idstatusgmeter
--
--
--if (@amountbalance<>0)
--begin
--if (@amountbalance>0)
--select * from #tmpBalancecc where beginbalance>=@amountbalance order by beginbalance, agent
--else 
--select * from #tmpBalancecc where beginbalance<=@amountbalance order by beginbalance, agent
--end
--else
--select * from #tmpBalancecc  order by  beginbalance, agent
--
--drop table #tmpBalancecc
--\\**старый вариант до 03.04.2013**//--










GO