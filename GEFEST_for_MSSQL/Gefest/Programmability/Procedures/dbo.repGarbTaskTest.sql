SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO



CREATE procedure [dbo].[repGarbTaskTest] (@idperiod int, @idgru int, @idagent int, @idhouse int, @Indication int, @IndNo6Month int, @idstatusgobject int, @idstatusgmeter int,@countlives int,@countind int, @countindmax int, @Prichina int, @akt int,@PeriodAkt int, @TypeInfringements int) as 
---------***********Наряд-задание*********-----------------------
--declare @idperiod int
--declare @idagent int
--declare @idgru int
--declare @idhouse int
--declare @Indication int
--declare @idstatusgobject int
--declare @idstatusgmeter int
--declare @countlives int
--declare @countind int
--declare @countindmax int
--set @countindmax=0
--set @countind=0
--set @countlives=-1
--set @idperiod=63
--set @idagent=0
--select @idgru=(select idgru from gru where invnumber='05004')
--set @idstatusgobject=0
--set @idstatusgmeter=0
--set @Indication=0
--declare @tel int
--set @tel=0
--declare @Prichina int
--set @Prichina=0
--declare @akt int
--set @akt=0
--declare @PeriodAkt int
--set @PeriodAkt=0
--declare @TypeInfringements int
--set @TypeInfringements=1

declare @dEnd datetime
set @dEnd=dbo.fGetDatePeriod(@IdPeriod,0)
-- declare @idprevper int
-- set @idprevper=dbo.fGetPredPeriodVariable(@idperiod)

create table #tmpGrap (idcontract int, account  varchar(20),invnumber  varchar(20),  FIO varchar(500), address varchar(500), flat varchar(20) , idstatusgobject int, countlives int, 
	idstatusgmeter int, gru varchar(100),  agent  varchar(50), LastIndication float, LastIndicationDate datetime,
	telefon varchar(8000),memo varchar(8000), NumberGmeter varchar(100), TypeGmeter varchar(100), idgmeter int, factuse float, countind int, dateverify datetime, Prichina varchar(300),dateAkt datetime)

declare @q varchar(8000)
set @q='insert #tmpGrap (idcontract, account, FIO, address, idstatusgobject, countlives, 
	idstatusgmeter, gru,  agent, invnumber,flat, idgmeter,telefon,dateverify,memo)
select c.idcontract,  c.account, case when isnull(isJuridical,0)=0 then ltrim(p.surname)+'' ''+ left(isnull(p.name,''''),1)+'' ''+left(isnull(p.patronic,''''),1) else ltrim(p.surname) end FIO, 
	s.name+'' ''+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'''') address,
	g.idstatusgobject, g.countlives, isnull(gm.idstatusgmeter,2) idstatusgmeter,
	gru.name gru, ag.name agent, gru.invnumber,a.flat, gm.idgmeter,
	 ppp.numberphone tel ,gm.dateverify, c.Memo 

 from contract c with (nolock) 
inner join person p with (nolock) on p.idperson=c.idperson
left join phone ppp with(nolock) on p.idperson=ppp.idperson
 inner join GObject g with (nolock) on g.idcontract=c.idcontract and isnull(status,0)<>2'

if (isnull(@idstatusgobject,0)>0)
	set @q=@q+' and g.idstatusgobject='+ltrim(str(@idstatusgobject))
if (isnull(@countlives,0)>=0)
	set @q=@q+' and g.countlives='+ltrim(str(@countlives))

set @q=@q+' inner join GRU with (nolock) on gru.idgru=g.idgru'

if (isnull(@idgru,0)>0)
	set @q=@q+' and g.idgru='+ltrim(str(@idgru))

set @q=@q+' inner join agent ag with (nolock) on ag.idagent=gru.idagent'

if (isnull(@idagent,0)>0)
	set @q=@q+' and ag.idagent='+ltrim(str(@idagent))

set @q=@q+' inner join address a with (nolock) on a.idaddress=g.idaddress
inner join house h on h.idhouse=a.idhouse'

set @q=@q+' inner join street s with (nolock) on s.idstreet=a.idstreet
left join gmeter gm on gm.idgobject=g.idgobject and idstatusgmeter=1'


exec (@q)
if @idhouse>0
begin
delete #tmpGrap
where account not like '___'+convert(varchar,@idhouse)+'%' 
end
if (@idstatusgmeter=1)
begin
delete #tmpGrap
where idgmeter is null
end
if (@idstatusgmeter=2)
begin
delete #tmpGrap
where idgmeter is not null
end

update #tmpGrap
set numbergmeter=isnull(g.SerialNumber,'б/н'),
Prichina=ltrim(tt.name)+',' +ltrim(str(tt.ClassAccuracy))
from #tmpGrap t
inner join gmeter g with (nolock) on g.idgmeter=t.idgmeter
inner join typegmeter tt with (nolock) on tt.idtypegmeter=g.idtypegmeter

update #tmpGrap
set Prichina=(select top 1 tttt.name from document dd with (nolock) 
inner join pd with (nolock) on pd.iddocument=dd.iddocument and idtypepd=33  and dd.idtypedocument=18
inner join dbo.TypeReasonDisconnect tttt with (nolock) on tttt.idTypeReasonDisconnect=convert(int,pd.value) and dd.idcontract=fff.idcontract order by dd.iddocument desc)
from #tmpGrap fff where idstatusgmeter=2

if (@PeriodAkt<>0) 
begin 
update #tmpGrap set dateAkt=(select top 1 DateDisplay from indication i with (nolock) 
where fff.idgmeter=i.idgmeter and i.idtypeindication=3 order by idindication desc )
from #tmpGrap fff 
delete #tmpGrap where isnull(dateAkt,'1990-01-01')>left(convert(varchar, dateadd(yyyy,-@PeriodAkt,Getdate()), 20), 10)  or  dbo.dateonly(isnull(dateverify,'1990-01-01'))>left(convert(varchar, dateadd(yyyy,-1,Getdate()), 20), 10) or idgmeter is null
end

if (@Prichina>0)
begin
delete #tmpGrap
where Prichina is null
end

update #tmpGrap
set LastIndication=dbo.fLastIndication(IdGMeter, @dEnd+1)
,LastIndicationDate=dbo.fLastIndicationDate(IdGMeter, @dEnd+1)
where idgmeter is not null

--update #tmpGrap
--set memo=ty.name
--from #tmpGrap tt
--inner join document d with (nolock) on tt.idcontract=d.idcontract
--and tt.idstatusgobject is not null and idtypedocument=17
--inner join pd with (nolock) on d.iddocument=pd.iddocument
--and idtypepd=14
--inner join typeend ty with (nolock) on ty.idtypeend=convert(int,pd.value)

update #tmpGrap
set factuse=f.factamount
from #tmpGrap t
inner join gobject g with (nolock) on t.idcontract=g.idcontract
inner join factuse f with (nolock) on f.idgobject=g.idgobject
and idtypefu=1 and f.idperiod=@IdPeriod

update #tmpGrap
set countind=tt.coon
from #tmpGrap t
inner join ( select count(0) coon, g.idcontract
from gobject g with (nolock)
inner join #tmpGrap t on t.idcontract=g.idcontract
inner join indication i  with (nolock) on i.idgmeter=t.idgmeter
and lastindication=i.display
group by g.idcontract having count(0)>1) tt on t.idcontract=tt.idcontract
if (@TypeInfringements>0)
begin
update #tmpGrap
set memo=tt.name
from #tmpGrap t
inner join contract c with (nolock) on c.idcontract=t.idcontract
inner join TypeInfringements tt with (nolock) on c.idTypeInfringements=tt.idTypeInfringements
delete #tmpGrap where memo='нет'
end 

if (@countind>0 and @countindmax=0)
begin
delete #tmpGrap
where isnull(countind,0)<@countind
end
if (@countindmax>0)
begin
delete #tmpGrap
where isnull(countind,0)<@countind 
delete #tmpGrap
where isnull(countind,0)>@countindmax
end
if (@IndNo6Month>0)
begin
delete #tmpGrap
where datediff(mm,LastIndicationDate,GetDate())<6
end
if (@Indication=1)
begin
select * from #tmpGrap where factuse is null order by agent, account
end
else
begin
select * from #tmpGrap order by agent, account
end
drop table #tmpGrap















GO