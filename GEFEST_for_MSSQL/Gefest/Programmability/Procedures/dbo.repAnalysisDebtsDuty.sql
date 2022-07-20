SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO





-- =============================================
-- Author:		<Sumlikin,Aleksandr>
-- Create date: <2007-10-08>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[repAnalysisDebtsDuty](@dateB datetime,@amount float,@OnlyOn int)
AS
BEGIN
--declare @dateB datetime
--set @dateB='2005-10-01'
--declare @path varchar (1000)
--set @path='D:\WORK\Gefest\Report\Saldo.xls'

--declare @amount float
--set @amount=-5000
--select convert (varchar,@dateB,103)
--create table #AnalizBalance (idcontract int, account varchar(20),statusgobject varchar(20), mol varchar(100), Balance1 float, Balance2 float, Balance3 float, Balance4 float, Balance5 float, Balance6 float, Balance7 float, Balance8 float, Balance9 float, Balance10 float, Balance11 float, Balance12 float, date varchar(10))
--
--insert into #AnalizBalance (idcontract , account ,statusgobject , mol, Balance1, Balance2, Balance3, Balance4 , Balance5, Balance6 , Balance7, Balance8 , Balance9, Balance10, Balance11, Balance12, date)
select  c.idcontract, c.account+'; '+isnull(p.Surname,'')+' '+isnull(p.name,'')+' '+isnull(p.Patronic,'') as account ,case when g.idstatusgobject=1 then 'П'else 'О'end statusgobject,a.name,
isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (dateadd(month,-11,@dateB)), c.IdContract, 0),0) b12 ,
isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (dateadd(month,-10,@dateB)), c.IdContract, 0),0) b11 ,
isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (dateadd(month,-9,@dateB)), c.IdContract, 0),0) b10 ,
isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (dateadd(month,-8,@dateB)), c.IdContract, 0),0) b9 ,
isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (dateadd(month,-7,@dateB)), c.IdContract, 0),0) b8 ,
isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (dateadd(month,-6,@dateB)), c.IdContract, 0),0) b7 ,
isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (dateadd(month,-5,@dateB)), c.IdContract, 0),0) b6 ,
isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (dateadd(month,-4,@dateB)), c.IdContract, 0),0) b5 ,
isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (dateadd(month,-3,@dateB)), c.IdContract, 0),0) b4 ,
isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (dateadd(month,-2,@dateB)), c.IdContract, 0),0) b3 ,
isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (dateadd(month,-1,@dateB)), c.IdContract, 0),0) b2 ,
dbo.fGetLastBalance (dbo.fGetPeriodDate (@dateB), c.IdContract, 0) b1
,case when isnull(gm.idstatusgmeter,2)=1 then 'П'else 'О'end  idstatusgmeter, convert(varchar,'')urddok,
0 as PeriodPay
into #tmpSaldoPOGRU
from contract c with (nolock)
inner join person p (nolock) on c.IDPerson=p.idperson
inner join gobject g with (nolock) on c.idcontract=g.idcontract
and isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (@dateB), c.IdContract, 0),0)<@amount
inner join gru q with (nolock) on q.idgru=g.idgru
inner join agent a with (nolock) on q.idagent=a.idagent
left join gmeter gm with (nolock) on gm.idgobject=g.idgobject and idstatusgmeter=1
--left join document d with (nolock) on c.idcontract=d.idcontract
--and d.idtypedocument=13
order by account, a.name

if @OnlyOn=1
	begin
		delete from #tmpSaldoPOGRU where statusgobject='О'
	end

update #tmpSaldoPOGRU
set PeriodPay=n.idper
from #tmpSaldoPOGRU t
inner join ( select idcontract, max(idperiod) as idper from document d with (nolock) 
where d.idtypedocument=1 group by idcontract) n on t.idcontract=n.idcontract

delete from #tmpSaldoPOGRU where PeriodPay>dbo.fGetPeriodDate (dateadd(month,-3,@dateB))


update #tmpSaldoPOGRU
set urddok='Ю/д'
from #tmpSaldoPOGRU t
inner join balance b on b.idcontract=t.idcontract and b.idperiod=dbo.fGetPeriodDate (@dateB) and b.idaccounting=2 and b.amountbalance<0
--inner join document d with (nolock) on t.idcontract=d.idcontract and d.idtypedocument=13 and d.idperiod>dbo.fGetPeriodDate (dateadd(month,-11,@dateB))


select * from #tmpSaldoPOGRU
drop table #tmpSaldoPOGRU

--declare @s varchar (8000)
----
--set @s='insert OpenDataSource(''Microsoft.Jet.OLEDB.4.0'',''Data Source="' + @path +
--'";User ID=Admin;Password=;Extended properties=Excel 5.0'')...[Лист2$]
--( c1, c2, c3, c4,  c5, c6, c7,c8,c9, c10,c11,c12,c13,c14,c15,c16,c17)
--select idcontract, account,statusgobject, mol, replace(Balance1,''.'','',''), replace(Balance2,''.'','',''), replace(Balance3,''.'','',''), replace(Balance4,''.'','',''), replace(Balance5,''.'','',''), replace(Balance6,''.'','',''), replace(Balance7,''.'','',''), replace(Balance8,''.'','',''), replace(Balance9,''.'','',''), replace(Balance10,''.'','',''), replace(Balance11,''.'','',''), replace(Balance12,''.'','','') , date 
--from #AnalizBalance'
--exec (@s)
--drop table #AnalizBalance
END





GO