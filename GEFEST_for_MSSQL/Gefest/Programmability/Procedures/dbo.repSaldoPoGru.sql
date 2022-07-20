SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO







CREATE PROCEDURE [dbo].[repSaldoPoGru] (@idperiod int,  @idstatusgobject int, @idstatusgmeter int) AS
---------------**************Справка по абонентской службе*********-------------------
SET NOCOUNT ON 
--declare @idperiod int
--declare @idstatusgobject int
--declare @idstatusgmeter int
declare @idperiodPrev int


--set @idperiod=40
--set @idstatusgobject=0
--set @idstatusgmeter=0
set @idPeriodPrev=dbo.fGetPredPeriodVariable(@idPeriod)
declare @dEnd datetime
set @dEnd= dbo.fGetDatePeriod(@IdPeriod,0)

select c.idcontract,gru.idgru,gru.name GRUNAME, aa.name Agent,gru.invnumber,gru.memo, dbo.fGetLastBalance(@idPeriodPrev, c.IdContract, 0) AmountBalance,
	dbo.fGetLastBalance(@idPeriod, c.IdContract, 0) AmountBalanceEnd, s.name,a.idhouse, ltrim(str(isnull(h.HouseNumber,'')))+isnull(h.HouseNumberChar,'') Housenumber,
	convert(float, 0) DAP, convert(float,0)Nach, g.IdStatusGObject, dbo.fGetStatusPU (@dEnd,gm.idgmeter) StatusGMeter, convert(int, 0) KolKV, convert(int, 0) Otkl, convert(int, 0)SPU, convert(int, 0)BezPU
into #tmpSaldoPOGRU
from contract c  with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
inner join Address a with (nolock) on a.IdAddress=g.IdAddress
inner join House h with (nolock) on h.IdHouse=a.IdHouse
inner join street s with (nolock) on s.IdStreet=h.IdStreet
inner join gru with (nolock) on g.idgru=gru.idgru
inner join agent aa with (nolock) on gru.idagent=aa.idagent
left join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
	and dbo.fGetStatusPU (@dEnd,gm.idgmeter)=1 

if (@idstatusgobject<>0)
delete #tmpSaldoPOGRU where idstatusgobject<>@idstatusgobject

if (@idstatusgmeter<>0)
delete #tmpSaldoPOGRU where statusgmeter<>@idstatusgmeter

update #tmpSaldoPOGRU
set DAP=qq.DocumentAmount
from #tmpSaldoPOGRU t 
inner join (select dc.idcontract, sum(dc.DocumentAmount) DocumentAmount
from document dc with (nolock)
where	dc.idtypedocument=1
	and dc.idperiod=@idPeriod
group by dc.idcontract) qq on qq.idcontract=t.idcontract

update #tmpSaldoPOGRU
set Nach=qq.ao
from #tmpSaldoPOGRU t 
inner join (select  d.idcontract, sum(AmountOperation) ao
from operation o   with (nolock) 
inner join document d with (nolock) on d.iddocument=o.iddocument
	and idtypedocument=5
	and  d.IdPeriod=@idperiod group by d.idcontract) qq on qq.idcontract=t.idcontract


select idgru, Agent,invnumber,memo,GRUNAME, idhouse, name+' '+ housenumber Adres,count(0) KolKV, convert(int, 0) Otkl, convert(int, 0)SPU, convert(int, 0)BezPU, convert(float,0)KreditZad, convert(float,0)Debzad, convert(float,0)Oplata, convert(float,0)Nach,convert(float,0)KreditZadKon, convert(float,0)DebzadKon
into #Kontab
from #tmpSaldoPOGRU group by idgru, Agent,GRUNAME,invnumber,memo, idhouse, name, housenumber
--select * from #tmpSaldoPOGRU
update #Kontab
set Otkl=tt.con
from (select idhouse hous, count(0)con from #tmpSaldoPOGRU where idstatusgobject=2 group by idhouse)tt
where idhouse=tt.hous

update #Kontab
set SPU=tt.con
from (select idhouse hous, count(0)con from #tmpSaldoPOGRU where idstatusgobject=1 and statusgmeter=1 group by idhouse)tt
where idhouse=tt.hous

update #Kontab
set BezPU=tt.con
from (select idhouse hous, count(0)con from #tmpSaldoPOGRU where idstatusgobject=1 and statusgmeter=2 group by idhouse)tt
where idhouse=tt.hous

update #Kontab
set KreditZad=-tt.bal
from (select idhouse hous,sum(AmountBalance) bal from #tmpSaldoPOGRU where AmountBalance<0 group by idhouse)tt
where idhouse=tt.hous

update #Kontab
set debZad=tt.bal
from (select idhouse hous,sum(AmountBalance) bal from #tmpSaldoPOGRU where AmountBalance>0 group by idhouse)tt
where idhouse=tt.hous

update #Kontab
set KreditZadKon=-tt.bal
from (select idhouse hous,sum(AmountBalanceend) bal from #tmpSaldoPOGRU where AmountBalanceend<0 group by idhouse)tt
where idhouse=tt.hous

update #Kontab
set debZadKon=tt.bal
from (select idhouse hous,sum(AmountBalanceend) bal from #tmpSaldoPOGRU where AmountBalanceend>0 group by idhouse)tt
where idhouse=tt.hous

update #Kontab
set oplata=tt.bal
from (select idhouse hous,sum(DAP) bal from #tmpSaldoPOGRU group by idhouse)tt
where idhouse=tt.hous

update #Kontab
set Nach=-tt.bal
from (select idhouse hous,sum(Nach) bal from #tmpSaldoPOGRU group by idhouse)tt
where idhouse=tt.hous

select * from #kontab order by Adres
drop table #tmpSaldoPOGRU
drop table #kontab


GO