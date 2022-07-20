SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO







CREATE PROCEDURE [dbo].[repReestrOplPoPeniOfKass] (@idperiod int) AS
---------------**************Реестр по оплаты пени и госпошлины Касса*********-------------------
--------------- изменена 3 мая 2019 г. кассир Сапоненко IdAgent = 151
SET NOCOUNT ON

--declare @idperiod int
--set @idperiod=83

declare @Date datetime
declare @dateE datetime

set @Date=dbo.fGetDatePeriod(@IdPeriod,1)
set @dateE=dbo.fGetDatePeriod(@IdPeriod,0)

create table #Paymentss (documentdate datetime,Peny1 float,Gos1 float,Peny2 float,Gos2 float,Peny3 float,Gos3 float,Peny4 float,Gos4 float)
declare @i datetime
set @i=@Date
while (@i<=@dateE)
begin
insert #Paymentss (documentdate,Peny1,Gos1,Peny2,Gos2,Peny3,Gos3,Peny4,Gos4)
select @i,0,0,0,0,0,0,0,0

set @i=dateadd(day,1,@i)
end

update #Paymentss
set Peny1=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=4
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=1 and idCashier=45
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set Gos1=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=3
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=1 and idCashier=45
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set Peny2=summ
from #Paymentss s
inner join (select  d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=4
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=1 and idCashier=73
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set Gos2=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=3
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=1 and idCashier=73
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set Peny3=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=4
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=1 and idCashier in (151)--(114,129,148) --- кассир Сапоненко 3 мая 2019 г. 
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set Gos3=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=3
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=1 and idCashier in (151)--(114,129,148)
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set Peny4=summ
from #Paymentss s
inner join (select dbo.DateOnly(d.documentdate) as documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=4
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=1 and idCashier=125
group by dbo.DateOnly(documentdate) ) dd on s.documentdate=dd.documentdate

update #Paymentss
set Gos4=summ
from #Paymentss s
inner join (select dbo.DateOnly(d.documentdate) as documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=3
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=1 and idCashier=125
group by dbo.DateOnly(documentdate) ) dd on s.documentdate=dd.documentdate


select * from #Paymentss
drop table #Paymentss



GO