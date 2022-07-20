SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO






CREATE PROCEDURE [dbo].[repNariadForCut] (@strContract varchar (7001)) AS

--set @strContract='908815,911485,911492'
declare @idperiod int
declare @idprevper int
select @idperiod=dbo.fGetNowPeriod()
select @idprevper=dbo.fGetPredPeriod()

create table #tmpBalance (idcontract int, account  varchar(20),  FIO varchar(500), address varchar(500), 
	flat varchar(20) , idstatusgobject varchar(100), countlives int, 
	idstatusgmeter int, beginbalance float, amountnach float,
	amountopl float, endbalance float, Indication float, agent varchar(50), dateInd datetime,telefon varchar(8000), note varchar(8000))
declare @q varchar(8000)
set @q='insert #tmpBalance (idcontract, account, FIO, address, idstatusgobject, countlives, 
	idstatusgmeter, flat, Indication, agent, dateInd,telefon, note)
select c.idcontract, c.account, ltrim(p.surname)+'' ''+ ltrim(left(isnull(p.name,'' ''),1))+'' ''+ltrim(left(isnull(p.patronic,'' ''),1)) FIO, 
	s.name+'', ''+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'''')+''-''+isnull(a.flat,'''') address,
	g.idstatusgobject, g.countlives, isnull(gm.idstatusgmeter,2) idstatusgmeter, a.flat, 
	dbo.fLastIndication(gm.idgmeter, GetDate()), ag.name, dbo.fLastIndicationDate(gm.idgmeter, GetDate()),ppp.numberphone tel, c.memo
from contract c with (nolock) 
inner join person p with (nolock) on p.idperson=c.idperson
	and c.idcontract in ('+@strContract+') 
left join phone ppp with(nolock) on p.idperson=ppp.idperson
inner join GObject g with (nolock) on g.idcontract=c.idcontract 
inner join GRU with (nolock) on gru.idgru=g.idgru
inner join agent ag with (nolock) on ag.idagent=gru.idagent
inner join address a with (nolock) on a.idaddress=g.idaddress
inner join house h on h.idhouse=a.idhouse
inner join street s with (nolock) on s.idstreet=a.idstreet
left join gmeter gm on gm.idgobject=g.idgobject
	and gm.idstatusgmeter=1'

exec (@q)

update #tmpBalance
set beginbalance=dbo.fGetLastBalance(@idprevper, idcontract, 0), 
	endbalance=dbo.fGetLastBalance(@idperiod, idcontract, 0) 

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
set idstatusgobject=''
where idstatusgobject=1

update #tmpBalance
set idstatusgobject='ОТК.'
where idstatusgobject=2

/*
update #tmpBalance
set idstatusgobject=(select top 1 name from document d   with (nolock)  
		inner join PD  with (nolock)   on pd.iddocument=d.iddocument 
			and pd.idtypepd=14 
			and d.idtypedocument=17 
			and d.idcontract=#tmpBalance.idcontract
		inner join typeend t  with (nolock)   on t.idtypeend=pd.value
		order by d.documentdate desc)
where idstatusgobject='ОТКЛ'
*/
update #tmpBalance
set idstatusgobject='ОТК.'
where idstatusgobject is null

select * from #tmpBalance  order by account

drop table #tmpBalance




GO