SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO










CREATE PROCEDURE [dbo].[repSaldoWorkPlace] (@idagent int, @amountbalance float, @idstatusgobject int, @idstatusgmeter int, @idownership int) AS

--declare @idagent int
--declare @amountbalance float
--declare @idstatusgobject int
--declare @idstatusgmeter int
--set @idstatusgmeter=0
----
--set @idagent=10
--set @amountbalance=-15000
--set @idstatusgobject=1

declare @idperiod int
set @idperiod=dbo.fGetNowPeriod()


create table #tmpBalanceWork(idcontract int, account  varchar(20), FIO varchar(1000), 
	address varchar(500), dom varchar(20),kv varchar(20), idstatusgobject varchar(100), 
	idstatusgmeter varchar(100), agent  varchar(50), endbalance float,Uridikal varchar(10), WorkPlace varchar(500),SaldoUr float,dateurdok datetime,memo Varchar(5000),memo1 Varchar(5000))
----+''-''+a.flat
declare @q varchar(8000)
set @q='insert #tmpBalanceWork (idcontract, account, FIO, address,dom,kv, idstatusgobject,
	idstatusgmeter, agent,WorkPlace,dateurdok,memo,memo1)
select c.idcontract, c.account, ltrim(p.surname)+'' ''+ ltrim(isnull(p.name,''''))+'' ''+ltrim(isnull(p.patronic,'''')) FIO, 
	s.name address, ltrim(str(h.housenumber))+'' ''+ isnull(h.housenumberchar,''''),a.flat ,
	g.idstatusgobject, isnull(gm.idstatusgmeter,2) idstatusgmeter,
	 ag.name agent, p.WorkPlace, dbo.fGetDocumentDateUridical(c.idcontract),c.memo,p.memo
from contract c with (nolock) 
inner join person p with (nolock) on p.idperson=c.idperson
inner join GObject g with (nolock) on g.idcontract=c.idcontract '

if (isnull(@idownership,0)>0)
	set @q=@q+' inner join OwnerShip os with (nolock) on os.IDOwnership=p.IDOwnership 
				and p.IDOwnership ='+ltrim(str(@idownership))



if (isnull(@idstatusgobject,0)>0)
	set @q=@q+' and g.idstatusgobject='+ltrim(str(@idstatusgobject))

set @q=@q+' inner join GRU with (nolock) on gru.idgru=g.idgru
inner join agent ag with (nolock) on ag.idagent=gru.idagent'

if (isnull(@idagent,0)>0)
	set @q=@q+' and ag.idagent='+ltrim(str(@idagent))

set @q=@q+' inner join address a with (nolock) on a.idaddress=g.idaddress
inner join house h on h.idhouse=a.idhouse'

set @q=@q+' inner join street s with (nolock) on s.idstreet=a.idstreet
left join gmeter gm on gm.idgobject=g.idgobject
	and gm.idstatusgmeter=1'

exec (@q)

update #tmpBalanceWork
set endbalance=dbo.fGetLastBalance(@idperiod, idcontract, 0) 

update #tmpBalanceWork
set SaldoUr=dbo.fGetLastBalance(@idperiod, idcontract, 2) 

update #tmpBalanceWork
set dateurdok=null
from #tmpBalanceWork 
where isnull(SaldoUr,0)>=0

update #tmpBalanceWork
set Uridikal='Юр.док.'
from #tmpBalanceWork 
where dateurdok is not null

update #tmpBalanceWork
set idstatusgobject=''
where idstatusgobject=1

update #tmpBalanceWork
set idstatusgobject='ОТКЛ'
where idstatusgobject=2
if (@idstatusgmeter<>0)
delete #tmpBalanceWork where idstatusgmeter<>@idstatusgmeter

update #tmpBalanceWork
set idstatusgmeter=''
where idstatusgmeter=1

update #tmpBalanceWork
set idstatusgmeter='ОТКЛ'
where idstatusgmeter=2

update #tmpBalanceWork
set idstatusgobject=(select top 1 name from document d   with (nolock)  
		inner join PD  with (nolock)   on pd.iddocument=d.iddocument 
			and pd.idtypepd=14 
			and d.idtypedocument=17 
			and d.idcontract=#tmpBalanceWork.idcontract
		inner join typeend t  with (nolock)   on t.idtypeend=pd.value
		order by d.documentdate desc)
where idstatusgobject='ОТКЛ'

update #tmpBalanceWork
set idstatusgobject='ОТКЛ'
where idstatusgobject is null


if (@amountbalance<>0)
begin
if (@amountbalance>0)
select * from #tmpBalanceWork where endbalance>=@amountbalance order by account
else 
select * from #tmpBalanceWork where endbalance<=@amountbalance order by account
end
else
select * from #tmpBalanceWork  order by account

drop table #tmpBalanceWork








GO