SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO







CREATE PROCEDURE [dbo].[repSaldo] (@idperiod int, @idagent int, @idgru int, @idhouse int, @amountbalance float, @idstatusgobject int, @countlives int, @idstatusgmeter int) AS
--declare @idperiod int
--declare @idagent int
--declare @idgru int
--declare @idhouse int
--declare @amountbalance float
--declare @idstatusgobject int
--declare @countlives int
--set @idperiod=22
--set @idagent=10
--set @idgru=8494
--set @amountbalance=330
--set @idstatusgobject=2
--set @countlives=2


declare @idprevper int
set @idprevper=dbo.fGetPredPeriodVariable(@idperiod)

create table #tmpBalance (idcontract int, account  varchar(20),invnumber  varchar(20),  FIO varchar(500), 
	address varchar(500), flat varchar(20) , idstatusgobject varchar(100), countlives int, 
	idstatusgmeter int, gru varchar(100),  agent  varchar(50), beginbalance float, amountnach float,
	amountopl float, endbalance float,Uridikal varchar(10),UridikalDate datetime, amountUr float, amountUrg float)
----+''-''+a.flat
declare @q varchar(8000)
set @q='insert #tmpBalance (idcontract, account, FIO, address, idstatusgobject, countlives, 
	idstatusgmeter, gru,  agent, invnumber,flat)
select c.idcontract, c.account, ltrim(p.surname)+'' ''+ ltrim(isnull(p.name,''''))+'' ''+ltrim(isnull(p.patronic,'''')) FIO, 
	s.name+'' ''+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'''') address,
	g.idstatusgobject, g.countlives, isnull(gm.idstatusgmeter,2) idstatusgmeter,
	gru.name gru, ag.name agent, gru.invnumber,a.flat
from contract c with (nolock) 
inner join person p with (nolock) on p.idperson=c.idperson
inner join GObject g with (nolock) on g.idcontract=c.idcontract '

if (isnull(@idstatusgobject,0)>0)
	set @q=@q+' and g.idstatusgobject='+ltrim(str(@idstatusgobject))

if (isnull(@countlives,0)>=0)
	set @q=@q+' and g.countlives='+ltrim(str(@countlives))

set @q=@q+' inner join GRU with (nolock) on gru.idgru=g.idgru'

if (isnull(@idgru,0)>0)
	set @q=@q+' and g.idgru='+ltrim(str(@idgru))

set @q=@q+' inner join agent ag with (nolock) on ag.idagent=gru.idagent'

if (isnull(@idagent,0)>0)
	set @q=@q+' and ag.idagent='+ltrim(str(@idagent))

set @q=@q+' inner join address a with (nolock) on a.idaddress=g.idaddress
inner join house h on h.idhouse=a.idhouse'

--if (isnull(@idhouse,0)>0)
--	set @q=@q+' and h.idhouse='+ltrim(str(@idhouse))

set @q=@q+' inner join street s with (nolock) on s.idstreet=a.idstreet
left join gmeter gm on gm.idgobject=g.idgobject
	and gm.idstatusgmeter=1'

exec (@q)
if @idhouse>0
begin
delete #tmpBalance
where account not like '___'+convert(varchar,@idhouse)+'%' 
end

update #tmpBalance
set beginbalance=dbo.fGetLastBalance(@idprevper, idcontract, 0), 
	endbalance=dbo.fGetLastBalance(@idperiod, idcontract, 0),
amountUr=dbo.fGetLastBalanceReal(@idperiod, idcontract, 2),
amountUrg=dbo.fGetLastBalanceReal(@idperiod, idcontract, 3) 

update #tmpBalance
set amountnach=-op
from
(select sum(o.amountoperation) op, t.idcontract
from operation o with (nolock) 
inner join balance b with (nolock) on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and o.idtypeoperation<>1
inner join #tmpBalance t on t.idcontract=b.idcontract
group by t.idcontract) qq
where qq.idcontract=#tmpBalance.idcontract


update #tmpBalance
set amountopl=op
from
(select sum(o.amountoperation) op, t.idcontract
from operation o with (nolock) 
inner join balance b with (nolock) on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and o.idtypeoperation=1
inner join #tmpBalance t on t.idcontract=b.idcontract
group by t.idcontract) qq
where qq.idcontract=#tmpBalance.idcontract

update #tmpBalance
set Uridikal='Юр.док.',
UridikalDate=qq.datedocument
from
(select t.idcontract,d.documentdate datedocument from document d with (nolock) 
inner join #tmpBalance t on t.idcontract=d.idcontract
and d.idtypedocument=13) qq
where qq.idcontract=#tmpBalance.idcontract and (isnull(amountur,0)<0 or isnull(amounturg,0)<0)


update #tmpBalance
set idstatusgobject=''
where idstatusgobject=1

update #tmpBalance
set idstatusgobject='ОТКЛ'
where idstatusgobject=2

update #tmpBalance
set idstatusgobject=(select top 1 name from document d   with (nolock)  
		inner join PD  with (nolock)   on pd.iddocument=d.iddocument 
			and pd.idtypepd=14 
			and d.idtypedocument=17 
			and d.idcontract=#tmpBalance.idcontract
		inner join typeend t  with (nolock)   on t.idtypeend=pd.value
		order by d.documentdate desc)
where idstatusgobject='ОТКЛ'

update #tmpBalance
set idstatusgobject='ОТКЛ'
where idstatusgobject is null
if (@idstatusgmeter<>0)
delete #tmpBalance where idstatusgmeter<>@idstatusgmeter

if (@amountbalance<>0)
begin
if (@amountbalance>0)
select * from #tmpBalance where beginbalance>=@amountbalance order by account
else 
select * from #tmpBalance where beginbalance<=@amountbalance order by account
end
else
select * from #tmpBalance  order by account

drop table #tmpBalance





GO