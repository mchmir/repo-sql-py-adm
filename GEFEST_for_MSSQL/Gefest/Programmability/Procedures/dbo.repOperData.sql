SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO






CREATE PROCEDURE [dbo].[repOperData] (@month int, @year int)  AS
/*
declare @month int
declare @year int
set @month=01
set @year=2007
*/
declare @ret table (dday int, m3 float, pay1 float, pay2 float, 
	forsaldo float, sumpay float, summ3 float,
	beginDT float, beginKT float, endDT float, endKT float,den float)

declare @idperiod int
declare @idperiodpred int

select @IdPeriod=idperiod from period where month(dateend)=@month and year(dateend)=@year
if (@idperiod is null)
	set @idperiod=dbo.fGetNowPeriod()
set @idperiodpred=dbo.fGetPredPeriodVariable(@idperiod)

declare @i int
select @i=1

while @i<=31
begin
	insert @ret (dday) values (@i)
	set @i=@i+1
end

update @ret
set beginDT=-qq.BalanceBeg
from (select  Sum( dbo.fGetLastBalanceReal(@idperiodpred, c.IdContract, 0)) BalanceBeg
from Contract c with (nolock)
 where dbo.fGetLastBalanceReal(@idperiodpred, c.IdContract, 0)<0 ) qq

update @ret
set beginKT=qq.BalanceBeg
from (select  Sum( dbo.fGetLastBalanceReal(@idperiodpred, c.IdContract, 0)) BalanceBeg
from Contract c with (nolock)
 where dbo.fGetLastBalanceReal(@idperiodpred, c.IdContract, 0)>0 ) qq

update @ret
set endDT=-qq.BalanceBeg
from (select  Sum( dbo.fGetLastBalanceReal(@idperiod, c.IdContract, 0)) BalanceBeg
from Contract c with (nolock)
where dbo.fGetLastBalanceReal(@idperiod, c.IdContract, 0)<0 ) qq

update @ret
set endKT=qq.BalanceBeg
from (select  Sum(dbo.fGetLastBalanceReal(@idperiod, c.IdContract, 0)) BalanceBeg
from Contract c with (nolock)
where dbo.fGetLastBalanceReal(@idperiod, c.IdContract, 0)>0 ) qq

update @ret
set m3=fa
from (select day(i.datedisplay) ddday, sum(f.factamount) fa
from factuse f
inner join indication i on i.idindication=f.idindication
	and month(i.datedisplay)=@month
	and year(i.datedisplay)=@year
left join document d on d.iddocument=f.iddocument
	and d.idtypedocument=7
where d.iddocument is null
group by day(i.datedisplay)) qq
where dday=ddday

update @ret
set pay1=ba
from (select day(b.batchdate) ddday, sum(b.batchamount) ba
from batch b 
where month(b.batchdate)=@month and
	--and year(b.batchdate)=@year
	 b.idtypepay=1
	and (b.idtypebatch=1 or b.idtypebatch=2)
	and b.idperiod=@idperiod
group by day(b.batchdate)) qq
where dday=ddday

update @ret
set pay2=ba
from (select day(b.batchdate) ddday, sum(b.batchamount) ba
from batch b 
where month(b.batchdate)=@month and
	--and year(b.batchdate)=@year
	b.idtypepay=2
	and (b.idtypebatch=1 or b.idtypebatch=2)
	and b.idperiod=@idperiod
group by day(b.batchdate)) qq
where dday=ddday

set @i=1
declare @sum float

while @i<=31
begin 

set @sum=0

select @sum=sum(isnull(pay1,0)+isnull(pay2,0))
from @ret
where dday<=@i

update @ret
set sumpay=@sum
where dday=@i

set @sum=0

select @sum=sum(isnull(m3,0))
from @ret
where dday<=@i

update @ret
set summ3=@sum
where dday=@i

set @i=@i+1
end


update @ret
set pay1=0
where pay1 is null

update @ret
set pay2=0
where pay2 is null

update @ret
set m3=0
where m3 is null

update @ret
set den=qq.BalanceBeg
from (select  Sum(batchamount) BalanceBeg
from batch b with (nolock)
where b.idperiod=@idperiod
	and (b.idtypebatch=1 or b.idtypebatch=2) and (Month(b.batchdate)<>@Month or year(b.batchdate)<>@year)) qq

select * from @ret





GO