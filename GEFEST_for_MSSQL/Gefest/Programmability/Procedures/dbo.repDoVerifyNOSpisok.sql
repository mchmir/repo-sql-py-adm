SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO







-- Author:		<Sumlikin,Aleksandr>
-- Create date: <2008-09-15>
-- Description:	<Отчет по отключенным ПУ без поверки>
-- =============================================
CREATE PROCEDURE [dbo].[repDoVerifyNOSpisok](@idgru int, @idTypeGmeter int, @idagent int, @idperiod int, @ispol int,@saldo int, @tel int) AS
/*
declare @idgru int
declare @idTypeGmeter int
declare @idagent int
declare @idperiod int
declare @ispol int
declare @saldo int
set @idperiod=dbo.fGetPredPeriod()
set @idTypeGmeter=25
set @idagent=0
set @idgru=0
set @ispol=0
set @saldo=1
*/
declare @dBegin datetime
declare @dEnd datetime
set @dBegin= dbo.fGetDatePeriod(@IdPeriod,1)
set @dEnd= dbo.fGetDatePeriod(@IdPeriod,0)


create table #tmpGmeterDisconect (idcontract int, account  varchar(20),  FIO varchar(500), 
	address varchar(500),   Typegmeter varchar(200),  serNumber varchar(100) , PrichinaOtk varchar(500), 
	DateOtkl datetime, DateProv datetime, datePodkl datetime, Ispolnitel varchar(50), Saldo float,  agent  varchar(50),cntlives int, status int, idgobject int, idgmeter int, idtypegmeter int,IDprichina int,idispol int)

declare @q varchar(8000)
set @q=' insert #tmpGmeterDisconect (idcontract, account,FIO,address,agent,cntlives,Saldo, status,idgobject,Ispolnitel,PrichinaOtk)
select c.idcontract, c.account, ' 
if @tel=1
	set @q=@q+'ltrim(isnull(p.surname,''''))+'' ''+left(ltrim(isnull(p.name,'''')),1)+''.''+left(ltrim(isnull(p.patronic,'''')),1)+''., т.''+ltrim(isnull(ph.NumberPhone,'''')) as FIO, '
if @tel=0
	set @q=@q+'ltrim(isnull(p.surname,''''))+'' ''+left(ltrim(isnull(p.name,'''')),1)+''.''+left(ltrim(isnull(p.patronic,'''')),1) as FIO, '

--ltrim(isnull(p.surname,''''))+'' ''+left(ltrim(isnull(p.name,'''')),1)+''.''+left(ltrim(isnull(p.patronic,'''')),1)+''., т.''+ltrim(isnull(ph.NumberPhone,'''')) as FIO, 
	set @q=@q+'s.name+'' ''+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'''')+''-''+isnull(a.flat,'''') address,
	aa.name,dbo.fGetCountLives(go.idgobject,'+str(@IDPeriod)+') as cntlives, isnull( dbo.fGetLastBalance('+ str(@IdPeriod)+', c.IdContract, 0),0) nanachalo,dbo.fGetStatusPU (GetDate(), ggg.idgmeter), go.idgobject,'''',''''
from contract c with (nolock)
inner join person p with (nolock) on p.idperson=c.idperson
left join phone ph (nolock) on p.idperson=ph.idperson
inner join address a with (nolock) on a.idaddress=p.idaddress
inner join house h with (nolock) on a.idhouse=h.idhouse
inner join street s with (nolock) on s.idstreet=a.idstreet
inner join gobject go with (nolock) on go.idcontract=c.idcontract and go.idstatusGobject=1'
if isnull(@idgru,0)>0
	set @q=@q+ 'and go.idgru='+ltrim(str(@idgru))
set @q=@q+'inner join gru with (nolock) on gru.idgru=go.idgru 
 inner join agent aa with (nolock) on aa.idagent=gru.idagent '
if isnull(@idagent,0)>0
set @q=@q+' and gru.idagent='+ltrim(str(@idagent))
set @q=@q+'left join gmeter ggg with(nolock) on go.idgobject=ggg.idgobject and dbo.fGetStatusPU (GetDate(), ggg.idgmeter)=1 '


exec (@q)
delete #tmpGmeterDisconect where status=1

update #tmpGmeterDisconect
set idgmeter=(select top 1 idgmeter from gmeter with (nolock) where idgobject=c.idgobject order by idgmeter desc)
from #tmpGmeterDisconect c

update #tmpGmeterDisconect
set datePodkl=ggg.dateinstall,
Typegmeter=t.name+', '+ltrim(str(t.classaccuracy, 3, 1)),
serNumber=isnull(ggg.serialnumber,''),
DateProv=dateadd(yy, t.servicelife, case when ggg.dateverify='1800-01-01' then ggg.datefabrication else ggg.dateverify end),
idtypegmeter=ggg.idtypegmeter
from #tmpGmeterDisconect cc
inner join gmeter ggg with(nolock) on cc.idgmeter=ggg.idgmeter
inner join typegmeter t with (nolock) on t.idtypegmeter=ggg.idtypegmeter

delete #tmpGmeterDisconect where dateprov>@dEnd
if (@idTypeGmeter<>0)
delete #tmpGmeterDisconect where idtypegmeter<>@idTypeGmeter
update #tmpGmeterDisconect
set dateotkl=dbo.fGetDocumentDate(cc.idcontract,18),
ispolnitel=isnull(a.name,''),
idispol=a.idagent,
prichinaotk=isnull(aa.name,''),
idprichina=aa.IDTypeReasonDisconnect
from #tmpGmeterDisconect cc
--inner join document d with (nolock) on cc.idcontract=d.idcontract
--and d.idtypedocument=18 --and dbo.dateonly(d.documentdate)>='2009-02-01' and dbo.dateonly(d.documentdate)<='2009-02-28' 
inner join pd with (nolock) on pd.iddocument=dbo.fGetIDDocument(cc.idcontract,18)
and pd.idtypepd=7 and convert(int,pd.value)=cc.idgmeter
inner join pd dd with (nolock) on dd.iddocument=dbo.fGetIDDocument(cc.idcontract,18) 
and dd.idtypepd=16 
inner join agent a with (nolock) on a.idagent=convert(int,dd.value)
inner join pd dc with (nolock) on dc.iddocument=dbo.fGetIDDocument(cc.idcontract,18) 
and dc.idtypepd=33
inner join dbo.TypeReasonDisconnect aa with (nolock) on aa.IDTypeReasonDisconnect=convert(int,dc.value)
--update #tmpGmeterDisconect
--set dateotkl=d.documentdate,
--ispolnitel=isnull(a.name,''),
--idispol=a.idagent,
--prichinaotk=isnull(aa.name,''),
--idprichina=aa.IDTypeReasonDisconnect
--from #tmpGmeterDisconect cc
--inner join document d with (nolock) on cc.idcontract=d.idcontract
--and d.idtypedocument=18 --and dbo.dateonly(d.documentdate)>='2009-02-01' and dbo.dateonly(d.documentdate)<='2009-02-28' 
--inner join pd with (nolock) on pd.iddocument=d.iddocument 
--and pd.idtypepd=7 and convert(int,pd.value)=cc.idgmeter
--inner join pd dd with (nolock) on dd.iddocument=d.iddocument 
--and dd.idtypepd=16 
--inner join agent a with (nolock) on a.idagent=convert(int,dd.value)
--inner join pd dc with (nolock) on dc.iddocument=d.iddocument 
--and dc.idtypepd=33
--inner join dbo.TypeReasonDisconnect aa with (nolock) on aa.IDTypeReasonDisconnect=convert(int,dc.value)
delete #tmpGmeterDisconect where dateotkl is null

if (@ispol<>0)
delete #tmpGmeterDisconect where idispol<>@ispol
delete #tmpGmeterDisconect where typegmeter is null
if (@saldo<>0)
update #tmpGmeterDisconect set Saldo=0

select * from #tmpGmeterDisconect order by  agent,account

drop table #tmpGmeterDisconect







GO