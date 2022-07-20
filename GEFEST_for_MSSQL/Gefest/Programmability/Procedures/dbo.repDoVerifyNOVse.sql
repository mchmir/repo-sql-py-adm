SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO







-- =============================================
-- Author:		<Sumlikin,Aleksandr>
-- Create date: <2008-01-28>
-- Description:	<Отчет для выевления абонентов не прошедших проверку ПУ>
-- =============================================
CREATE PROCEDURE [dbo].[repDoVerifyNOVse](@idgru int, @idstatusgmeter int, @idagent int, @tel int, @CM int) AS

--declare @idgru int
--declare @idagent int
--declare @idstatusgmeter int
--set @idstatusgmeter=1
--set @idagent=10
--set @idgru=0

declare @IDPeriod int
declare @q varchar(8000)
set @IDPeriod=(select max(idperiod) from Period)
set @q='select c.account, '
if @tel=1
	set @q=@q+'ltrim(isnull(p.surname,''''))+'' ''+left(ltrim(isnull(p.name,'''')),1)+''.''+left(ltrim(isnull(p.patronic,'''')),1)+''., т.''+ltrim(isnull(ph.NumberPhone,'''')) as FIO, '
if @tel=0
	set @q=@q+'ltrim(isnull(p.surname,''''))+'' ''+left(ltrim(isnull(p.name,'''')),1)+''.''+left(ltrim(isnull(p.patronic,'''')),1) as FIO, '
	set @q=@q+'s.name+'' ''+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'''')+''-''+isnull(a.flat,'''') address,
	t.name+'', ''+ltrim(str(t.classaccuracy, 3, 1)) gmeter, g.serialnumber, g.datefabrication, '
    set @q=@q+'dbo.fLastIndicationIDPeriod(g.idgmeter,'+convert(varchar(5),@IDPeriod)+') as ind,dbo.fGetCountLives(go.idgobject,'+convert(varchar(5),@IDPeriod)+') as cntlives'
	set @q=@q+',dateadd(yy, t.servicelife, case when g.dateverify=''1800-01-01'' then g.datefabrication else g.dateverify end) as limitdate , aa.name agent
from contract c with (nolock)
inner join person p with (nolock) on p.idperson=c.idperson
left join phone ph (nolock) on p.idperson=ph.idperson
inner join address a with (nolock) on a.idaddress=p.idaddress
inner join house h with (nolock) on a.idhouse=h.idhouse
inner join street s with (nolock) on s.idstreet=a.idstreet
inner join gobject go with (nolock) on go.idcontract=c.idcontract '
if isnull(@idgru,0)>0
	set @q=@q+ 'and go.idgru='+ltrim(str(@idgru))
set @q=@q+'inner join gru with (nolock) on gru.idgru=go.idgru '
if isnull(@idagent,0)>0
set @q=@q+' and gru.idagent='+ltrim(str(@idagent))
set @q=@q+'inner join gmeter g with (nolock) on go.idgobject=g.idgobject inner join agent aa with (nolock) on aa.idagent=gru.idagent '
if isnull(@idstatusgmeter, 0)>0
	set @q=@q+' and g.IDStatusGMeter='+ltrim(str(@idstatusgmeter))
set @q=@q+'inner join typegmeter t with (nolock) on t.idtypegmeter=g.idtypegmeter
and dateadd(yy, t.servicelife, case when g.dateverify=''1800-01-01'' then g.datefabrication else g.dateverify end)<'''+left(convert(varchar, dateadd(MM, @CM, Getdate()), 20), 10)+' '' 
order by c.account'



exec (@q)








GO