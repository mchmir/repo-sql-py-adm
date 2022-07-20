SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO




-- =============================================
-- Author:		<Grigoryev,Andrey>
-- Create date: <2013-09-09>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[repAnalysisMainDebtsDuty](@dateB datetime,@amount float,@OnlyOn int)
AS
BEGIN
--declare @dateB datetime
--set @dateB='2005-10-01'
set @amount=@amount*-1
select  c.idcontract, c.account, isnull(p.Surname,'')+' '+isnull(p.name,'')+' '+isnull(p.Patronic,'') as fio ,
s.name+' '+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'')+'-'+isnull(a.flat,'') as adress,
case when g.idstatusgobject=1 then 'П'else 'О' end statusgobject,ag.name,
dbo.fGetLastBalance (dbo.fGetPeriodDate (@dateB), c.IdContract, 1) blsMain,
dbo.fGetLastBalance (dbo.fGetPeriodDate (@dateB), c.IdContract, 4) blsPeny,
dbo.fGetLastBalance (dbo.fGetPeriodDate (@dateB), c.IdContract, 6) blsUslugi,
case when isnull(gm.idstatusgmeter,2)=1 then 'П'else 'О'end  idstatusgmeter, convert(varchar,'')urddok
into #tmpAnalysisMainDebtsDuty
from contract c with (nolock)
inner join person p (nolock) on c.IDPerson=p.idperson
inner join address a with (nolock) on a.idaddress=p.idaddress
inner join house h with (nolock) on a.idhouse=h.idhouse
inner join street s with (nolock) on s.idstreet=a.idstreet
inner join gobject g with (nolock) on c.idcontract=g.idcontract
and isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (@dateB), c.IdContract, 1),0)<@amount
inner join gru q with (nolock) on q.idgru=g.idgru
inner join agent ag with (nolock) on q.idagent=ag.idagent
left join gmeter gm with (nolock) on gm.idgobject=g.idgobject and idstatusgmeter=1
order by account, ag.name

if @OnlyOn=1
	begin
		delete from #tmpAnalysisMainDebtsDuty where statusgobject='О'
	end

update #tmpAnalysisMainDebtsDuty
set urddok='Ю/д'
from #tmpAnalysisMainDebtsDuty t
inner join document d with (nolock) on t.idcontract=d.idcontract and d.idtypedocument=13 and d.idperiod>dbo.fGetPeriodDate (dateadd(month,-11,@dateB))

select * from #tmpAnalysisMainDebtsDuty
drop table #tmpAnalysisMainDebtsDuty


END




GO