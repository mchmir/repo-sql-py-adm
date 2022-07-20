SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO

CREATE procedure [dbo].[repСonditionGobject] ( @dBegin datetime, @dEnd datetime, @idagent int, @idgru int, @idhouse int, @idtypeGmeter int, @typeOtkl int, @sort int) as 
---------***********Справка по состоянию Объекта учета*********-----------------------
--declare @dBegin datetime
--declare @dEnd datetime
--declare @idagent int
--declare @idgru int
--declare @idhouse int
--declare @idtypeGmeter int
--declare @typeOtkl int
--declare @sort int

--set @dBegin='2006-12-01'
--set @dEnd='2007-01-31'
--set @idagent=10
--set @idgru=0
--set @idhouse=0
--set @idtypeGmeter=0
--set @typeOtkl=0
--set @sort=1

set @dBegin=dbo.dateonly(@dBegin)
set @dEnd=dbo.dateonly(@dEnd)

create table #tmpGraps (account  varchar(20),invnumber  varchar(20),  FIO varchar(500), address varchar(500), flat varchar(20) ,  
	gru varchar(100),  agent  varchar(50), NumberGmeter varchar(100), TypeGmeter varchar(100), DateFabrication datetime, DateInstall datetime
	,DateVerify datetime, DateNextVerify datetime, TypeOtkl varchar(100),iddocument int)

declare @q varchar(8000)
set @q='insert #tmpGraps (account, FIO, address, 
	gru,  agent, invnumber,flat, iddocument, typeotkl)
select c.account, p.surname+'' ''+ left(ltrim(isnull(p.name,'''')),1)+'' ''+left(ltrim(isnull(p.patronic,'''')),1)FIO, 
	s.name+'' ''+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'''') address,
	gru.name gru, ag.name agent, gru.invnumber,a.flat, d.iddocument,2 from document d  with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument in (18,20) and dbo.dateonly(d.DocumentDate)>='+convert(varchar,round(convert(float,@dBegin),0, 1))
set @q=@q+'and dbo.dateonly(d.DocumentDate)<='+convert(varchar,round(convert(float,@dEnd),0, 1))
set @q=@q+'inner join person p with (nolock) on p.idperson=c.idperson
inner join GObject g with (nolock) on g.idcontract=c.idcontract'
set @q=@q+' inner join GRU with (nolock) on gru.idgru=g.idgru'
if (isnull(@idgru,0)>0)
	set @q=@q+' and g.idgru='+ltrim(str(@idgru))
set @q=@q+' inner join agent ag with (nolock) on ag.idagent=gru.idagent'
if (isnull(@idagent,0)>0)
	set @q=@q+' and ag.idagent='+ltrim(str(@idagent))
set @q=@q+' inner join address a with (nolock) on a.idaddress=g.idaddress
inner join house h on h.idhouse=a.idhouse'
--if (isnull(@idhouse,0)>0)
--	set @q=@q+' and h.idhouse='+ltrim(str(@idhouse))
set @q=@q+' inner join street s with (nolock) on s.idstreet=a.idstreet'
--inner join gmeter gm with (nolock) on gm.idgobject=g.idgobject and idstatusgmeter=2' 
--if (isnull(@idtypeGmeter,0)>0)
--set @q=@q+' and gm.idtypegmeter=' +ltrim(str(@idtypeGmeter))
--set @q=@q+' inner join typegmeter t with (nolock) on t.idtypegmeter=gm.idtypegmeter '
exec (@q)

if @idhouse>0
begin
delete #tmpGraps
where account not like '___'+convert(varchar,@idhouse)+'%' 
end

update #tmpGraps
set typeotkl=pd.value
from #tmpGraps t
inner join pd  with (nolock) on t.iddocument=pd.iddocument
and idtypepd=28
if @typeOtkl=1
delete #tmpGraps where typeOtkl=1
if @typeOtkl=2
delete #tmpGraps where typeOtkl=2

set @q='update #tmpGraps set NumberGmeter=gm.SerialNumber
,TypeGmeter=tt.name+'' ''+ltrim(str(tt.ClassAccuracy)),DateFabrication=gm.DateFabrication,DateInstall=gm.DateInstall
,DateVerify=gm.DateVerify, DateNextVerify=convert(datetime,ltrim(str(year(gm.DateVerify)+tt.ServiceLife))+''-''+ltrim(str(month(gm.DateVerify)))+''-''+ltrim(str(day(gm.DateVerify))),20)
from #tmpGraps t
inner join pd  with (nolock)   on pd.iddocument=t.iddocument
	and pd.idtypepd=7
inner join gmeter gm  with (nolock)   on gm.idgmeter=convert(int,pd.value)
inner join typegmeter tt  with (nolock)   on tt.idtypegmeter=gm.idtypegmeter'
if (isnull(@idtypeGmeter,0)>0)
set @q=@q+' and gm.idtypegmeter=' +ltrim(str(@idtypeGmeter))

exec (@q)

set @q='select * from #tmpGraps order by'
if @sort=-1
set @q=@q+' account,typegmeter,dateNextVerify'
if @sort=0
set @q=@q+' typegmeter, account, dateNextVerify'
if @sort=1
set @q=@q+' dateNextVerify, account, typegmeter'

exec (@q)

drop table #tmpGraps
GO