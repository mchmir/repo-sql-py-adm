SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO




CREATE PROCEDURE [dbo].[repReestrUnplomb] (@DB datetime, @DE datetime) AS
/*
declare @DB as datetime
declare @DE as datetime
set @DB='2017-03-01'
set @DE='2017-03-31'
*/
declare @t table (Number int identity not null, IDContract int, IDgmeter int, Account varchar(50), plombnumber1 int, plombnumber2 int, dateplomb datetime, agent varchar(50), gostate varchar(10))
insert into @t (IDContract, IDGmeter, Account, plombnumber1, plombnumber2, dateplomb, agent, gostate)
--снятие пломб
select ct.idcontract, gm.idgmeter, ct.account, 
case when isnull(len(gmph.plombnumber1),0)=0 then 1 else 0 end,
case when isnull(len(gmph.plombnumber2),0)=0 then 1 else 0 end,
gmph.dateplomb, ag.name, case when go.IDStatusGObject=1 then 'подкл.' else 'откл.' end
from contract ct
inner join gobject go on go.idcontract=ct.idcontract
inner join gmeter gm on gm.idgobject=go.idgobject
inner join gmeterplombhistory gmph on gm.idgmeter=gmph.idgmeter
inner join agent ag on ag.idagent=gmph.idagentplomb
where gmph.indicationplomb>0
--ct.account='0501064'
and gmph.idtypeplombwork=2
and gmph.dateplomb>=@DB and gmph.dateplomb<=@DE
order by gmph.dateplomb
--установка пломб
select number, t.account, t.agent, t.plombnumber1+t.plombnumber2 as cntplomb, 
convert(varchar(12), t.dateplomb, 104) as dateuninst, convert(varchar(12), gmph.dateplomb , 104) as dateinst, 
gostate
from @t t
left join (
select ct.idcontract, gmph.dateplomb 
from contract ct
inner join gobject go on go.idcontract=ct.idcontract
inner join gmeter gm on gm.idgobject=go.idgobject
inner join gmeterplombhistory gmph on gm.idgmeter=gmph.idgmeter
inner join agent ag on ag.idagent=gmph.idagentplomb
where gmph.indicationplomb>0
--ct.account='0501064'
and gmph.idtypeplombwork=1
and gmph.dateplomb>=@DB and gmph.dateplomb<=@DE
) gmph on gmph.idcontract=t.idcontract and gmph.dateplomb>=t.dateplomb


--left join gmeterplombhistory gmph on t.idgmeter=gmph.idgmeter
--	and gmph.indicationplomb>0
--	and gmph.idtypeplombwork=1
--	and gmph.dateplomb>=t.dateplomb
--left join agent ag on ag.idagent=gmph.idagentplomb
--order by number

--select * from @t


GO