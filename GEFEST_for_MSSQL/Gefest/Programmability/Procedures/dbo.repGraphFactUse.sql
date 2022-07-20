SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE procedure [dbo].[repGraphFactUse] (@PeriodB int, @PeriodE int) as 
---------------*************Графики**********------------------
--declare @PeriodE int 
--declare @PeriodB int 
--set @PeriodB=11
--set @PeriodE=20

declare @AnalFactuse table (idperiod int, Period Varchar(200), M3 float,KG float, Oplata float, Nach float,Dt float,Kt float)

insert into @AnalFactUse (idperiod,Period)
select p.idperiod, case when len(ltrim(str(p.Month)))=1 then ltrim(str(p.Year))+'-0'+ltrim(str(p.Month)) else ltrim(str(p.Year))+'-'+ltrim(str(p.Month)) end
from period p with (nolock) 
where p.IDPeriod>=@PeriodB and p.IDPeriod<=@PeriodE

update @AnalFactUse
set M3=tt.m3
from @AnalFactUse t
inner join (select  idperiod ,sum(factamount) m3
from Factuse with (nolock) 
where idtypefu=1 and IDPeriod>=@PeriodB and IDPeriod<=@PeriodE
group by idperiod) tt on t.idperiod=tt.idperiod

update @AnalFactUse 
set Dt=isnull(-AA,0)
from @AnalFactUse f
inner join ( select qq.idperiod,sum(AB) AA from
(select idperiod, sum(AmountBalance) AB
from balanceReal  with (nolock) 
where  IDPeriod>=@PeriodB and IDPeriod<=@PeriodE
Group by IdContract, idperiod) qq  where AB<0
group by idperiod)tt on tt.idperiod=f.idperiod

update @AnalFactUse 
set Kt=isnull(AA,0)
from @AnalFactUse f
inner join ( select qq.idperiod,sum(AB) AA from
(select idperiod, sum(AmountBalance) AB
from balanceReal  with (nolock) 
where  IDPeriod>=@PeriodB and IDPeriod<=@PeriodE
Group by IdContract, idperiod) qq  where AB>0
group by idperiod)tt on tt.idperiod=f.idperiod

update @AnalFactUse
set KG=tt.KG
from @AnalFactUse t
inner join (select  idperiod ,sum(factamount) KG
from Factuse with (nolock) 
where idtypefu=2 and IDPeriod>=@PeriodB and IDPeriod<=@PeriodE
group by idperiod) tt on t.idperiod=tt.idperiod

update @AnalFactUse
set Oplata=tt.Oplata
from @AnalFactUse t
inner join (select  idperiod ,sum(documentamount) Oplata
from document with (nolock) 
where idtypeDocument=1 and IDPeriod>=@PeriodB and IDPeriod<=@PeriodE
group by idperiod) tt on t.idperiod=tt.idperiod

update @AnalFactUse
set Nach=-tt.Nach
from @AnalFactUse t
inner join (select  idperiod ,sum(amountoperation) Nach
from document d with (nolock) 
inner join operation o with (nolock) on o.iddocument=d.iddocument
where idtypeoperation=2 and numberoperation<>'99999' and IDPeriod>=@PeriodB and IDPeriod<=@PeriodE
group by idperiod) tt on t.idperiod=tt.idperiod

select * from @AnalFactuse
order by idperiod
GO