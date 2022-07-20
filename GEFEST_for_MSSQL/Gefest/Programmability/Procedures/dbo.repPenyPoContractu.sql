SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO





CREATE PROCEDURE [dbo].[repPenyPoContractu] (@idperiod int, @idcontract int) AS
--declare @idperiod int
declare @idperiodPrev int
--declare @idcontract int
--set @idcontract=0
--set @idperiod=48

set @idPeriodPrev=dbo.fGetPredPeriodVariable(@idPeriod)
declare @dEnd datetime
set @dEnd= dbo.fGetDatePeriod(@IdPeriod,0)

select c.idcontract,@dEnd date, dbo.fGetStavkaREF (@dEnd) stavk,c.account, --dbo.fGetLastBalance(@idPeriodPrev, c.IdContract, 0) AmountBalance,
	--dbo.fGetLastBalance(@idPeriod, c.IdContract, 0) AmountBalanceEnd, --s.name+''+ ltrim(str(isnull(h.HouseNumber,'')))+isnull(h.HouseNumberChar,'') Housenumber, a.Flat,
	convert(float, 0) BalPeny, amountoperation ,np.Surname+' '+left(ltrim(isnull(np.name,'')),1)+'.' +left(ltrim(isnull(np.Patronic,'')),1)  FIO
into #tmpPenyContract
from contract c  with (nolock) 
inner join person np with (nolock)  on np.idperson=c.Idperson
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--inner join Address a with (nolock) on a.IdAddress=g.IdAddress
--inner join House h with (nolock) on h.IdHouse=a.IdHouse
--inner join street s with (nolock) on s.IdStreet=h.IdStreet
inner join document d with (nolock) on d.idcontract=c.idcontract
and d.idperiod=@idperiod and idtypedocument=5
inner join operation o with (nolock) on o.iddocument=d.iddocument
inner join balance b with (nolock) on b.idbalance=o.idbalance
and b.idaccounting=4

if (@idcontract<>0)
delete #tmpPenyContract where idcontract<>@idcontract

update #tmpPenyContract
set BalPeny=qq.DocumentAmount
from #tmpPenyContract t 
inner join (select dc.idcontract, forpeny DocumentAmount
from workpeny dc with (nolock)
where	account is not null
	and dc.idperiod=@idPeriod
) qq on qq.idcontract=t.idcontract

--update #tmpPenyContract
--set Nach=qq.ao
--from #tmpPenyContract t 
--inner join (select  d.idcontract, sum(AmountOperation) ao
--from operation o   with (nolock) 
--inner join document d with (nolock) on d.iddocument=o.iddocument
--	and idtypedocument=5
--	and  d.IdPeriod=@idperiod group by d.idcontract) qq on qq.idcontract=t.idcontract
select * from #tmpPenyContract order by account
drop table #tmpPenyContract
GO