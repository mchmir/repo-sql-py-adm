SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO






















CREATE PROCEDURE [dbo].[repDoVerifyGTM](@idtypegmeter int, @date datetime, @NeedCheck int) AS
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
declare @dd as varchar(20)
declare @q varchar(8000)
declare @qq varchar(80)
set @d=convert(varchar(20), dateadd(month,1,dateadd(day,1-day(@date),@date))-1)
set @dd=convert(varchar(20), dateadd(day,1-day(@date),@date))
--set @d=@d+'-'+convert(varchar(4), month(@date))+'-30'
print @d
print @dd
set @q='declare @s as int'
set @q=@q+' declare @T table (idcontract int, account varchar(20), FIO varchar(200), address varchar(200), gmeter varchar(200), 
serialnumber varchar(20), datefabrication datetime, idgru int, limitdate datetime)
insert into @T (idcontract,account, FIO, address, gmeter, serialnumber, datefabrication, idgru, limitdate)
select c.idcontract, c.account, ltrim(isnull(p.surname,''''))+'' ''+isnull(p.name,'''')+'' ''+isnull(p.patronic,'''') FIO,
	s.name+'' ''+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'''')+''-''+isnull(a.flat,'''') address,
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

if @idtypegmeter=1
	begin
		set @qq=' set @s=1'
		set @q=@q+' and t.ClassAccuracy=1.6'--GTM   --+ltrim(str(@idtypegmeter))
	end
else
	begin
		set @qq=' set @s=2'
		set @q=@q+' and t.ClassAccuracy<>1.6'--GGS   --+ltrim(str(@idtypegmeter))
	end
set @q=@q+' and dateadd(yy, t.servicelife, case when g.dateverify=''1800-01-01'' then g.datefabrication else g.dateverify end)<='''+@d+''''--+left(convert(varchar, dateadd(MM, 1, Getdate()), 20), 10)+' '' '
--set @q=@q+' and dateadd(yy, t.servicelife, case when g.dateverify=''1800-01-01'' then g.datefabrication else g.dateverify end)>='''+@dd+''''--+left(convert(varchar, dateadd(MM, 1, Getdate()), 20), 10)+' '' '
--set @q=@q+' and h.idhouse=34738' --Юбилейная 3В
set @q=@q+' order by go.idgru'

if @NeedCheck=1
	begin
		set @d=convert(varchar(20),GetDate())
		print @d
		set @q=@q+@qq
		set @q=@q+' declare @w as int'
		set @q=@q+' set @w=(select count(*) from @t)'
		set @q=@q+' update contract set CountDoVerify=isnull(CountDoVerify,0)+1, DateDoVerify=GetDate() from contract inner join @t tbl on contract.idcontract=tbl.idcontract'
		set @q=@q+' insert into CountDoVerify(Count, DatePrint, Type) values(@w, '''+@d+''', @s)'
	end

--set @q=@q+' select rgtm.*, c.account, c.FIO, c.address, c.gmeter, c.serialnumber, c.datefabrication, c.idgru, c.limitdate
--from @T  c
--inner join RequestGTM rgtm with (nolock) on rgtm.idcontract=c.idcontract
--order by c.address'

set @q=@q+' select c.account, c.FIO, c.address, c.gmeter, c.serialnumber, c.datefabrication, c.idgru, c.limitdate
from @T  c
order by c.idgru'


exec (@q)






















GO