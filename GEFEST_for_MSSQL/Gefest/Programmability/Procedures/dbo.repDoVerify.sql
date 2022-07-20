SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO



CREATE PROCEDURE [dbo].[repDoVerify](@idgru int, @idtypegmeter int, @isservice int, @Per int,@idagent int) AS
--------------*********Уведомление на вручения***********------------------------
/*
declare @idgru int
declare @idtypegmeter int
declare @isservice int
declare @Per int
declare @idagent int
set @idtypegmeter=0
set @idgru=0--(select idgru from gru where invnumber=01047)
set @isservice=1
set @Per=0
set @idagent=10
*/
declare @q varchar(8000)
set @q='
select c.account, ltrim(isnull(p.surname,''''))+'' ''+isnull(p.name,'''')+'' ''+isnull(p.patronic,'''') FIO,
	s.name+'' ''+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'''')+''-''+isnull(a.flat,'''') address,
	t.name+'', ''+ltrim(str(t.classaccuracy, 3, 1)) gmeter, g.serialnumber, g.datefabrication, 
	dateadd(yy, t.servicelife, case when g.dateverify=''1800-01-01'' then g.datefabrication else g.dateverify end) limitdate
from contract c with (nolock)
inner join person p with (nolock) on p.idperson=c.idperson
inner join address a with (nolock) on a.idaddress=p.idaddress
inner join house h with (nolock) on a.idhouse=h.idhouse
inner join street s with (nolock) on s.idstreet=a.idstreet
inner join gobject go with (nolock) on go.idcontract=c.idcontract'
if (isnull(@idgru,0)>0)
	set @q=@q+' and go.idgru='+ltrim(str(@idgru))
set @q=@q+'	and go.idstatusgobject=1
inner join gmeter g with (nolock) on go.idgobject=g.idgobject
	and g.idstatusgmeter=1
inner join typegmeter t with (nolock) on t.idtypegmeter=g.idtypegmeter'

if (isnull(@idagent,0)>0)
	set @q=@q+' inner join GRU with (nolock) on gru.idgru=go.idgru  inner join agent ag with (nolock) on ag.idagent=gru.idagent  and ag.idagent='+ltrim(str(@idagent))
if isnull(@per, 0)>0
set @q=@q+' left join document d with (nolock) on c.idcontract=d.idcontract
and d.idtypedocument=21 
left join pd with (nolock) on pd.iddocument=d.iddocument
and pd.idtypepd=7 and g.idgmeter=convert(int,pd.value)'



set @q=@q+' where 1=1 '
if isnull(@idtypegmeter, 0)>0
	set @q=@q+' and g.idtypegmeter='+ltrim(str(@idtypegmeter))
if isnull(@isservice, 0)>0
	set @q=@q+' and dateadd(yy, t.servicelife, case when g.dateverify=''1800-01-01'' then g.datefabrication else g.dateverify end)<'''+left(convert(varchar, dateadd(MM, 1, Getdate()), 20), 10)+' '' '
if isnull(@per, 0)>0
set @q=@q+'and d.iddocument is null'
set @q=@q+' order by c.account'



exec (@q)



GO