SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO














CREATE PROCEDURE [dbo].[repDoVerifyByHouse](@date datetime) AS
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
declare @d as varchar(20)
declare @q varchar(8000)
declare @qq varchar(80)
set @d=convert(varchar(20), dateadd(month,1,dateadd(day,1-day(@date),@date))-1)
--set @d=@d+'-'+convert(varchar(4), month(@date))+'-30'
print @d
set @q='declare @s as int'
set @q=@q+' declare @T table (idcontract int, account varchar(20), FIO varchar(200), address varchar(200),idhouse int, StreetHouse varchar(200),  gmeter varchar(200), 
serialnumber varchar(20), datefabrication datetime, idgru int, limitdate datetime)
insert into @T (idcontract,account, FIO, address,idhouse,StreetHouse, gmeter, serialnumber, datefabrication, idgru, limitdate)
select c.idcontract, c.account, ltrim(isnull(p.surname,''''))+'' ''+isnull(p.name,'''')+'' ''+isnull(p.patronic,'''') FIO,
	s.name+'' ''+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'''')+''-''+isnull(a.flat,'''') address,
	h.idhouse,s.name+'' ''+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'''') StreetHouse,
	t.name+'', ''+ltrim(str(t.classaccuracy, 3, 1)) gmeter, g.serialnumber, g.datefabrication,  go.idgru,
	dateadd(yy, t.servicelife, case when g.dateverify=''1800-01-01'' then g.datefabrication else g.dateverify end) limitdate
from contract c with (nolock)
inner join person p with (nolock) on p.idperson=c.idperson
inner join address a with (nolock) on a.idaddress=p.idaddress
inner join house h with (nolock) on a.idhouse=h.idhouse
inner join street s with (nolock) on s.idstreet=a.idstreet
inner join gobject go with (nolock) on go.idcontract=c.idcontract'
set @q=@q+'	and go.idstatusgobject=1
inner join gmeter g with (nolock) on go.idgobject=g.idgobject
	and g.idstatusgmeter=1
inner join typegmeter t with (nolock) on t.idtypegmeter=g.idtypegmeter'

set @q=@q+' where 1=1 and c.idcontract<>870889 '  --Калиничев А.В. по распоряжению Суворова Д.В. 02.09.2013
set @q=@q+' and dateadd(yy, t.servicelife, case when g.dateverify=''1800-01-01'' then g.datefabrication else g.dateverify end)<'''+@d+''''--+left(convert(varchar, dateadd(MM, 1, Getdate()), 20), 10)+' '' '
set @q=@q+' order by go.idgru'


set @q=@q+' select c.account, c.FIO, c.address, c.gmeter, c.serialnumber, c.datefabrication, c.idgru, c.limitdate
from @T  c
order by c.address'

set @q=@q+' select c.StreetHouse, count(c.StreetHouse) ct, convert(varchar(2),month(c.limitdate))+''.''+convert(varchar(4),year(c.limitdate)) mn
from @T  c
group by c.StreetHouse,convert(varchar(2),month(c.limitdate))+''.''+convert(varchar(4),year(c.limitdate))
order by ct desc'



exec (@q)














GO