SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO








-- =============================================
-- Author:		<Sumlikin,Aleksandr>
-- Create date: <2007-07-04>
-- Description:	<Отчет для выевления абонентов не прошедших проверку ПУ после вручения уведомления на проверку ПУ>
-- =============================================
CREATE PROCEDURE [dbo].[repDoVerifyNO](@idgru int, @idstatusgmeter int, @idagent int,@Date datetime, @idtypegmeter int, @tel int) AS

--declare @idgru int
--declare @idstatusgmeter int
--declare @idagent int
--set @idstatusgmeter=1
--set @idagent=0
--set @idgru=8585


declare @q varchar(8000)
set @q='select c.account, dateadd(yy, t.servicelife, case when g.dateverify=''1800-01-01'' then g.datefabrication else g.dateverify end), '
if @tel=1
	set @q=@q+'ltrim(isnull(p.surname,''''))+'' ''+left(ltrim(isnull(p.name,'''')),1)+''.''+left(ltrim(isnull(p.patronic,'''')),1)+''., т.''+ltrim(isnull(ph.NumberPhone,'''')) as FIO, '
if @tel=0
	set @q=@q+'ltrim(isnull(p.surname,''''))+'' ''+left(ltrim(isnull(p.name,'''')),1)+''.''+left(ltrim(isnull(p.patronic,'''')),1) as FIO, '
	set @q=@q+'s.name+'' ''+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'''')+''-''+isnull(a.flat,'''') address,
	t.name+'', ''+ltrim(str(t.classaccuracy, 3, 1)) gmeter, g.serialnumber, (select top 1 documentdate from document with (nolock) where idcontract=c.idcontract and idtypedocument=21 order by documentdate desc) documentdate,-- and month(d.documentdate)='+ltrim(str(month(@Date)))+ ' and year(d.documentdate)='+ltrim(str(year(@Date)))+ ' order by documentdate desc) documentdate, 
aa.name, 
	dateadd(yy, t.servicelife, case when g.dateverify=''1800-01-01'' then g.datefabrication else g.dateverify end) limitdate, g.memo
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
set @q=@q+'inner join agent aa with (nolock) on aa.idagent=gru.idagent 
inner join document d with (nolock) on c.idcontract=d.idcontract
and d.idtypedocument=21 ---and month(d.documentdate)='+ltrim(str(month(@Date)))+ ' and year(d.documentdate)='+ltrim(str(year(@Date)))+ '
inner join pd with (nolock) on pd.iddocument=d.iddocument
and pd.idtypepd=7
inner join gmeter g with (nolock) on g.idgmeter=convert(int,pd.value)'
if isnull(@idtypegmeter, 0)>0
	set @q=@q+' and g.idtypegmeter='+ltrim(str(@idtypegmeter))
set @q=@q+' inner join typegmeter t with (nolock) on t.idtypegmeter=g.idtypegmeter
 where 1=1 '
if isnull(@idstatusgmeter, 0)>0
	set @q=@q+' and g.IDStatusGMeter='+ltrim(str(@idstatusgmeter)) 
set @q=@q+'and dateadd(yy, t.servicelife, case when g.dateverify=''1800-01-01'' then g.datefabrication else g.dateverify end)< Getdate()--dbo.dateonly((select top 1 documentdate from document with (nolock) where idcontract=c.idcontract and idtypedocument=21 and month(d.documentdate)='+ltrim(str(month(@Date)))+ ' and year(d.documentdate)='+ltrim(str(year(@Date)))+ ' order by documentdate desc)) 
order by c.account'
--print @q
exec (@q)










GO