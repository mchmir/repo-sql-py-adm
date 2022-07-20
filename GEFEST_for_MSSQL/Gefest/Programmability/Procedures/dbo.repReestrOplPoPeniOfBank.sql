SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO









CREATE PROCEDURE [dbo].[repReestrOplPoPeniOfBank] (@idperiod int) AS
---------------**************Реестр по оплаты пени и госпошлины Касса*********-------------------
SET NOCOUNT ON
--
--declare @idperiod int
--set @idperiod=83

declare @Date datetime
declare @dateE datetime

set @Date=dbo.fGetDatePeriod(@IdPeriod,1)
set @dateE=dbo.fGetDatePeriod(@IdPeriod,0)

create table #Paymentss (documentdate datetime,PenyNarod float,GosNarod float,PenyNarodBan float,GosNarodBan float,PenyKazPost float,GosKazPost float
,PenyTuran float,GosTuran float,PenyCentrCredit float,GosCentrCredit float,PenyCesna float,GosCesna float,PenyCGP float,GosCGP float,PenyAstana float,GosAstana float
,PenyATF float,GosATF float,PenyAlns float,GosAlns float,PenyNur float,GosNur float,PenyCentr float,GosCentr float,PenyRegion float,GosRegion float,PenySber float,GosSber float,PenyEvr float,GosEvr float,
PenyProch float,GosProch float,PenyGCVP float, GosGCVP float,PenyEksim float,GosEksim float)
--declare @i datetime
--set @i=@Date
--while (@i<=@dateE)
--begin
--insert #Paymentss (documentdate,PenyNarod,GosNarod,PenyNarodBan,GosNarodBan,PenyKazPost,GosKazPost
--,PenyTuran,GosTuran,PenyTemir,GosTemir,PenyCesna,GosCesna,PenyCGP,GosCGP,PenyAstana,GosAstana
--,PenyATF,GosATF,PenyAlns,GosAlns,PenyNur,GosNur,PenyCentr,GosCentr,PenyRegion,GosRegion,PenySber,GosSber,PenyEvr,GosEvr,
--PenyProch,GosProch,PenyGCVP, GosGCVP,PenyEksim,GosEksim)
--select @i,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
--
--set @i=dateadd(day,1,@i)
--end

insert #Paymentss (documentdate,PenyNarod,GosNarod,PenyNarodBan,GosNarodBan,PenyKazPost,GosKazPost
,PenyTuran,GosTuran,PenyCentrCredit,GosCentrCredit,PenyCesna,GosCesna,PenyCGP,GosCGP,PenyAstana,GosAstana
,PenyATF,GosATF,PenyAlns,GosAlns,PenyNur,GosNur,PenyCentr,GosCentr,PenyRegion,GosRegion,PenySber,GosSber,PenyEvr,GosEvr,
PenyProch,GosProch,PenyGCVP, GosGCVP,PenyEksim,GosEksim)
select d.documentdate,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and (b.idaccounting=4 or b.idaccounting=3)
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 --and iddispatcher=69
group by d.documentdate 
order by d.documentdate asc

update #Paymentss
set PenyNarod=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=4
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=69
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set GosNarod=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=3
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=69
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set PenyNarodBan=summ
from #Paymentss s
inner join (select  d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=4
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=70
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set GosNarodBan=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=3
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=70
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set PenyKazPost=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=4
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=71
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set GosKazPost=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=3
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=71
group by documentdate ) dd on s.documentdate=dd.documentdate
---PenyTuran
update #Paymentss
set PenyTuran=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=4
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher= 150  ---75
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set GosTuran=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=3
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=150 --75
group by documentdate ) dd on s.documentdate=dd.documentdate

---PenyTemir,GosTemir,PenyCesna,GosCesna,PenyCGP,GosCGP,PenyAstana,GosAstana
update #Paymentss
set PenyCentrCredit=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=4
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=107
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set GosCentrCredit=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=3
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=107
group by documentdate ) dd on s.documentdate=dd.documentdate

---PenyCesna,GosCesna,PenyCGP,GosCGP,PenyAstana,GosAstana
update #Paymentss
set PenyCesna=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=4
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=77
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set GosCesna=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=3
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=77
group by documentdate ) dd on s.documentdate=dd.documentdate
---PenyCGP,GosCGP,PenyAstana,GosAstana
update #Paymentss
set PenyCGP=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=4
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=91
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set GosCGP=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=3
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=91
group by documentdate ) dd on s.documentdate=dd.documentdate

---PenyAstana,GosAstana
update #Paymentss
set PenyAstana=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=4
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=99
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set GosAstana=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=3
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=99
group by documentdate ) dd on s.documentdate=dd.documentdate

--PenyATF,GosATF,PenyAlns,GosAlns,PenyNur,GosNur,PenyCentr,GosCentr,PenyRegion,GosRegion,PenySber,GosSber,PenyEvr,GosEvr

update #Paymentss
set PenyATF=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=4
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=141 --QIWI
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set GosATF=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=3
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=141 --QIWI
group by documentdate ) dd on s.documentdate=dd.documentdate

--PenyAlns,GosAlns,PenyNur,GosNur,PenyCentr,GosCentr,PenyRegion,GosRegion,PenySber,GosSber,PenyEvr,GosEvr

update #Paymentss
set PenyAlns=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=4
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=104
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set GosAlns=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=3
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=104
group by documentdate ) dd on s.documentdate=dd.documentdate
--PenyNur,GosNur,PenyCentr,GosCentr,PenyRegion,GosRegion,PenySber,GosSber,PenyEvr,GosEvr

update #Paymentss
set PenyNur=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=4
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=105
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set GosNur=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=3
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=105
group by documentdate ) dd on s.documentdate=dd.documentdate
--PenyCentr,GosCentr,PenyRegion,GosRegion,PenySber,GosSber,PenyEvr,GosEvr

--вместо Центркредита платежи Казкома
update #Paymentss
set PenyCentr=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=4
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=128
group by documentdate ) dd on s.documentdate=dd.documentdate
--вместо Центркредита платежи Казкома
update #Paymentss
set GosCentr=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=3
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=128
group by documentdate ) dd on s.documentdate=dd.documentdate

--PenyRegion,GosRegion,PenySber,GosSber,PenyEvr,GosEvr

update #Paymentss
set PenyRegion=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=4
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=134
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set GosRegion=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=3
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=134
group by documentdate ) dd on s.documentdate=dd.documentdate
--PenySber,GosSber,PenyEvr,GosEvr

update #Paymentss
set PenySber=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=4
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=113
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set GosSber=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=3
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=113
group by documentdate ) dd on s.documentdate=dd.documentdate

--PenyEvr,GosEvr

update #Paymentss
set PenyEvr=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=4
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=118
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set GosEvr=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=3
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=118
group by documentdate ) dd on s.documentdate=dd.documentdate

---PenyProch,GosProch,PenyGCVP, GosGCVP,PenyEksim,GosEksim
update #Paymentss
set PenyProch=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=4
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=46
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set GosProch=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=3
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=46
group by documentdate ) dd on s.documentdate=dd.documentdate
---PenyGCVP, GosGCVP,PenyEksim,GosEksim
update #Paymentss
set PenyGCVP=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=4
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=92
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set GosGCVP=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=3
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=92
group by documentdate ) dd on s.documentdate=dd.documentdate

---PenyEksim,GosEksim
update #Paymentss
set PenyEksim=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=4
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=103
group by documentdate ) dd on s.documentdate=dd.documentdate

update #Paymentss
set GosEksim=summ
from #Paymentss s
inner join (select d.documentdate,sum(amountoperation)summ
from document d with (nolock)
inner join contract c with (nolock) on c.idcontract=d.idcontract
and idtypedocument=1 and d.idperiod=@idperiod--dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=3
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2 and iddispatcher=103
group by documentdate ) dd on s.documentdate=dd.documentdate

select * from #Paymentss
drop table #Paymentss







GO