SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO











CREATE procedure [dbo].[repReestrUrLicVDGO] (@dEnd datetime, @per int) as 
---Реестр юр.лиц для вдго для выеснения оплаты и ревизий

--declare @dEnd datetime
--set @dEnd='2008-07-31'
--declare @per int
--set @per=1

declare @idperiod int
set @idperiod=dbo.fGetPeriodDate(@dEnd)


select go.idgobject, c.account, ltrim(isnull(p.surname,''))+' '+isnull(p.name,'')+' '+isnull(p.patronic,'') FIO,
	s.name+' '+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'')+'-'+isnull(a.flat,'') address, c.DateBegin, convert (varchar(50),'') AktNumber, convert(datetime,null) AktDate, convert (varchar(2000),'') Naimen,
 convert (varchar(50),'') NomerPlate, convert (float,0.00)  Oplata,convert (float,0.00)  SumOplata, convert(datetime,null) OplataDate, c.SummaDogovora, convert (int,0) KolOplat, convert (int,0) KolReviz,
	t.name+', '+ltrim(str(t.classaccuracy, 3, 1)) gmeter,
 
case when dbo.fGetStatusContract(c.idcontract,@idperiod)=0 then 'Не определен' else
case when dbo.fGetStatusContract(c.idcontract,@idperiod)=1 then 'Активен' else
case when dbo.fGetStatusContract(c.idcontract,@idperiod)=2 then 'Закрыт'  end end end as asd, dbo.fGetStatusContract(c.idcontract,@idperiod) ff

into #tmpReestURlicc
from contract c with (nolock)
inner join person p with (nolock) on p.idperson=c.idperson
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join address a with (nolock) on a.idaddress=p.idaddress
inner join house h with (nolock) on a.idhouse=h.idhouse
inner join street s with (nolock) on s.idstreet=a.idstreet
inner join gobject go with (nolock) on go.idcontract=c.idcontract
left join GMeter gm  with (nolock) on gm.IdGObject=go.IdGObject
	and dbo.fGetStatusPU (@dEnd,gm.idgmeter)=1 
left join typegmeter t with (nolock) on t.idtypegmeter=gm.idtypegmeter
order by account

if @per>-1
delete #tmpReestURlicc where ff<>@per

update #tmpReestURlicc
set aktNumber=rr.documentnumber,
AktDate=rr.documentdate,
Naimen=rr.name
from #tmpReestURlicc cc
inner join (select top 1 documentnumber,idgobject,documentdate, tt.name from documentvdgo dd with(nolock) 
inner join pdvdgo pd with(nolock) on pd.iddocumentvdgo=dd.iddocumentvdgo 
and dd.idtypedocumentVDGO=3 and dbo.dateonly(dd.documentdate)<dbo.dateonly(@dEnd) and IDTypePDVDGO=3 
inner join dbo.TypeMalfunction tt with(nolock) on convert(int,pd.value)=tt.idtypemalfunction order by dd.documentdate) rr on rr.idgobject=cc.idgobject

update #tmpReestURlicc
set KolReviz=rr.con
from #tmpReestURlicc cc
inner join (select count(0) con,idgobject from documentvdgo dd with(nolock) 
where dd.idtypedocumentVDGO=3 and dbo.dateonly(dd.documentdate)<dbo.dateonly(@dEnd) 
group by idgobject) rr on rr.idgobject=cc.idgobject

update #tmpReestURlicc
set  NomerPlate=(select top 1 documentnumber from documentvdgo dd with(nolock) 
where dd.idtypedocumentVDGO=9 and dbo.dateonly(dd.documentdate)<dbo.dateonly(@dEnd)and dd.idgobject=cc.idgobject order by dd.documentdate),
OplataDate=(select top 1 documentdate from documentvdgo dd with(nolock) 
where dd.idtypedocumentVDGO=9 and dbo.dateonly(dd.documentdate)<dbo.dateonly(@dEnd)and dd.idgobject=cc.idgobject order by dd.documentdate),
Oplata=(select top 1 documentamount from documentvdgo dd with(nolock) 
where dd.idtypedocumentVDGO=9 and dbo.dateonly(dd.documentdate)<dbo.dateonly(@dEnd)and dd.idgobject=cc.idgobject order by dd.documentdate)
from #tmpReestURlicc cc

update #tmpReestURlicc
set  KolOplat=rr.sums/case when isnull(summadogovora,0)=0 then 1 else summadogovora end
from #tmpReestURlicc cc
inner join (select idgobject,sum(documentamount) sums from documentvdgo dd with(nolock) 
where dd.idtypedocumentVDGO=9 and dbo.dateonly(dd.documentdate)<dbo.dateonly(@dEnd) 
group by idgobject) rr on rr.idgobject=cc.idgobject



update #tmpReestURlicc
set  SumOplata=rr.sums
from #tmpReestURlicc cc
inner join (select idgobject,sum(documentamount) sums from documentvdgo dd with(nolock) 
where dd.idtypedocumentVDGO=9 and dbo.dateonly(dd.documentdate)<dbo.dateonly(@dEnd) 
group by idgobject) rr on rr.idgobject=cc.idgobject

select * from #tmpReestURlicc order by account
drop table #tmpReestURlicc

GO