SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO








CREATE procedure [dbo].[repСonditionGMeter] (@idagent int, @idgru int, @idtypeGmeter int, @sort int) as 
-- declare @idagent int
-- declare @idgru int
-- declare @idtypeGmeter int
-- 
-- set @idtypegmeter=7


declare @q varchar(8000)
--if (@idagent>0 or @idgru>0 or @IdtypeGmeter>0)
--begin 

set @q='select c.account, isnull(p.surname,'''')+'' ''+left(ltrim(isnull(p.name,'''')),1)+''.''+left(ltrim(isnull(p.patronic,'''')),1)+''.'' FIO,
	s.name+'' ''+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'''')+''-''+isnull(a.flat,'''') address,
	gm.serialnumber, c.memo,
	t.name+'', ''+ltrim(str(t.classaccuracy, 3, 1)) typegmeter, gm.datefabrication, gm.dateverify, gm.dateinstall,
	dateadd(yy, t.servicelife, case when gm.dateverify=''1800-01-01'' then gm.datefabrication else gm.dateverify end) limitdate, d.documentdate, ag.name 
from contract c  with (nolock)
inner join gobject g  with (nolock)   on g.idcontract=c.idcontract
	and g.idstatusgobject=1
inner join address a  with (nolock)  on a.idaddress=g.idaddress
inner join house h  with (nolock)  on h.idhouse=a.idhouse
inner join street s  with (nolock)  on s.idstreet=a.idstreet
inner join person p  with (nolock)  on p.idperson=c.idperson
inner join gru   with (nolock)  on gru.idgru=g.idgru'
if @idagent>0
	set @q=@q+' and gru.idagent='+str(@idagent) 
set @q=@q+' inner join agent ag with (nolock)  on ag.idagent=gru.idagent
left join (select max(documentdate) documentdate, idcontract from document where idtypedocument=20
group by idcontract)d on d.idcontract=c.idcontract '
if @idgru>0
	set @q=@q+' and gru.idgru='+str(@idgru)

set @q=@q+' inner join gmeter gm  with (nolock)   on gm.idgobject=g.idgobject
	and gm.idstatusgmeter=1 
inner join typegmeter t  with (nolock)   on t.idtypegmeter=gm.idtypegmeter '
if @idtypegmeter>0
	set @q=@q+' and t.Idtypegmeter='+str(@idtypegmeter)

if @sort=0
set @q=@q+' order by account, typegmeter, limitdate'
if @sort=2
set @q=@q+' order by typegmeter, account, limitdate'
if @sort=1
set @q=@q+' order by limitdate, account, typegmeter'
print @q
exec (@q)
--end




GO