SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO








CREATE procedure [dbo].[repBigConsumption]  (@DateB datetime, @DateE datetime,@factuseS float, @factuse float, @idagent int,@idgru int, @idtypepu int) as 
-------------***************Отчё большое потребление***********-----------------------
--declare @DateB datetime
--declare @DateE datetime
--declare @idagent int
--declare @idgru int
--declare @factuseS float
--set @factuseS=7
--set @DateB='2011-08-01 0:00:00'
--set @DateE='2011-08-31 0:00:00'
--declare @factuse float
--set @factuse=10
--set @idagent=0
--select @idgru=0--(select idgru from gru where invnumber='05014')
set @DateB=dbo.dateonly(@DateB)
set @DateE=dbo.dateonly(@DateE)
create table #Max (factamount float,idcontract int, account varchar(20), FIO varchar(200), Summafactamount float, display float, datedisplay datetime,datedisplayPrev datetime, name varchar(20), agent varchar(100),countlives int,  memo varchar(2000),gmeter varchar(1000), documentdate datetime)
declare @q varchar(8000)
set @q='insert  #Max (factamount , idcontract, account, FIO, Summafactamount, display, datedisplay,datedisplayPrev, name, agent,countlives, memo,gmeter,documentdate)
 select f.factamount,c.idcontract, c.account, np.Surname+'' ''+isnull(np.name,'''')+''.'' +isnull(np.Patronic,''''), 0, i.display, i.datedisplay,dbo.fLastIndicationDate (i.idgmeter,i.datedisplay-1), 
t.name, ag.name, g.countlives, c.memo,tt.name, doc.documentdate
from factuse f with (nolock) 
inner join gobject g with (nolock) on g.idgobject=f.idgobject
inner join indication i with (nolock) on i.idindication=f.idindication
and  f.IdTypeFU=1 and dbo.dateonly(i.datedisplay)>='+convert(varchar,round(convert(float,@DateB),0, 1))+' and dbo.dateonly(i.datedisplay)<='+convert(varchar,round(convert(float,@DateE),0, 1))+'  
inner join contract c with (nolock) on c.idcontract=g.idcontract
inner join person np with (nolock) on np.idperson=c.idperson
inner join typeindication t with (nolock) on t.idtypeindication=i.idtypeindication
left join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
	and dbo.fGetStatusPU ('+convert(varchar,round(convert(float,@DateE),0, 1))+',gm.idgmeter)=1
left join typegmeter tt with (nolock) on tt.idtypegmeter=gm.idtypegmeter
left join (select max(documentdate) documentdate, idcontract from document where idtypedocument=20
group by idcontract) doc on doc.idcontract=c.idcontract
'
set @q=@q+' inner join GRU with (nolock) on gru.idgru=g.idgru'

if (isnull(@idgru,0)>0)
	set @q=@q+' and g.idgru='+ltrim(str(@idgru))

set @q=@q+' inner join agent ag with (nolock) on ag.idagent=gru.idagent'

if (isnull(@idagent,0)>0)
	set @q=@q+' and ag.idagent='+ltrim(str(@idagent))
if (isnull(@idtypepu,0)>0)
	set @q=@q+' and tt.idtypegmeter='+ltrim(str(@idtypepu))

exec (@q)
update #Max
set Summafactamount=tt.ff
from #Max m
inner join (select idcontract, sum(factamount) ff from #Max group by idcontract) tt on tt.idcontract=m.idcontract

select *
from #Max
where Summafactamount>=@factuseS and Summafactamount<=@factuse
order by account, datedisplay
drop table #Max







GO