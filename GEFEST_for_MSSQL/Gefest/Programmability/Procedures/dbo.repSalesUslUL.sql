SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO















CREATE PROCEDURE [dbo].[repSalesUslUL] (@idPeriod int, @Status int, @Path varchar(1000), @MonthName varchar(100) ) AS
---------------**************Отчет по реализации газа по ЮЛ*********-------------------
SET NOCOUNT ON
--declare @IDPeriod int
declare @idPeriodPrev int
declare @tariff float
set @MonthName='по состоянию на '+@MonthName+'г.'
--set @idPeriod=92
select @tariff=value from tariff t where t.IdPeriod=@IdPeriod
set @idPeriodPrev=dbo.fGetPredPeriodVariable(@idPeriod)
---создали общую таблицу
create table #tblItog (IDContract int, Account varchar(50), NameObject varchar(220), NamePerson varchar(220), RNN varchar(12), Address varchar(200), Status int, SaldoB decimal(10,2), SaldoE decimal(10,2),
uslTO decimal(10,2), uslTU decimal(10,2), uslObuch decimal(10,2), uslPusk decimal(10,2),uslPlomb decimal(10,2), uslCredit decimal(10,2), uslProch decimal(10,2), uslAll decimal(10,2), Pay decimal(10,2),
SumUslTO float, SumUslTU decimal(10,2), SumUslObuch decimal(10,2), SumUslPusk decimal(10,2),SumUslPlomb decimal(10,2),SumUslCredit decimal(10,2),SumUslProch decimal(10,2), SumPay decimal(10,2), SumuslAll decimal(10,2),SumCarry decimal(10,2), CarryPay decimal(10,2),
TypeEnd varchar(50))
---создали временную таблицу
create table #Tbl (IDContract int, Account varchar(50), NameObject varchar(220), NamePerson varchar(220), RNN varchar(12), Address varchar(200), Status int, SaldoB decimal(10,2), SaldoE decimal(10,2),
uslTO decimal(10,2), uslTU decimal(10,2), uslObuch decimal(10,2), uslPusk decimal(10,2), uslProch decimal(10,2),uslPlomb decimal(10,2), uslCredit decimal(10,2),uslAll decimal(10,2), Pay decimal(10,2),
SumUslTO float, SumUslTU decimal(10,2), SumUslObuch decimal(10,2), SumUslPusk decimal(10,2),SumUslPlomb decimal(10,2),SumUslCredit decimal(10,2),SumUslProch decimal(10,2), SumPay decimal(10,2), SumuslAll decimal(10,2),SumCarry decimal(10,2), CarryPay decimal(10,2),
TypeEnd varchar(50))
--грузим ЮЛ с договором на ТО 
--во временную таблицу
insert into #Tbl (IDContract, Account, NameObject,NamePerson,RNN,Address, Status, SaldoB, SaldoE, uslTO, uslTU, uslObuch, uslPusk,uslPlomb,uslCredit,uslProch, Pay, uslAll,CarryPay)
select ct.idcontract, ct.account, 
--pr.surname+' '+isnull(pr.name,''), 
ltrim(isnull(pr.surname,'-')) NamePerson,
case when len(isnull(pr.name,''))=0 then isnull(pr.patronic,'') else
isnull(pr.name,'')+''+isnull(', '+pr.patronic,'') end FIO, isnull(pr.RNN,'-'),
s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat) as ADDRESS,
ct.status, isnull(SaldoB.SB,0) SaldoB, isnull(SaldoE.SE,0) SaldoE,
isnull(uTO.ss,0),isnull(uTU.ss,0),isnull(uObuch.ss,0),isnull(uPusk.ss,0),isnull(uPlomb.ss,0),isnull(uCredit.ss,0),isnull(uProch.ss,0),
isnull(Pay1.AmountPay,0) Pay,
isnull(uTO.ss,0)+isnull(uTU.ss,0)+isnull(uObuch.ss,0)+isnull(uPusk.ss,0)+isnull(uPlomb.ss,0)+isnull(uCredit.ss,0)+isnull(uProch.ss,0) uslAll,
isnull(car.AmountPay*-1,0)
from person pr (nolock)
inner join address a (nolock) on a.idaddress=pr.idaddress
inner join street s with (nolock)  on s.idstreet=a.idstreet
inner join house hs  with (nolock) on hs.idhouse=a.idhouse
inner join contract ct (nolock) on ct.idperson=pr.idperson
left join (select idcontract, isnull(AB,0) SB from (select c.IdContract,sum(convert(decimal(10,4),AmountBalance)) AB
from balancereal b with (nolock)
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod=@idPeriodPrev --and dbo.fGetIsJuridical(c.idperson,@idPeriodPrev)=1
and idaccounting=6
Group by c.IdContract)qq) SaldoB on SaldoB.idcontract=ct.idcontract
----
left join (select idcontract, isnull(AB,0) SE from (select c.IdContract,sum(convert(decimal(10,4),AmountBalance)) AB
from balancereal b with (nolock)
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod=@idPeriod --and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1
and idaccounting=6
Group by c.IdContract)qq) SaldoE on SaldoE.idcontract=ct.idcontract
----
left join (select sum(isnull(documentamount,0))ss, d.idcontract
from document d
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value)=11
group by d.idcontract) uTO on uTO.idcontract=ct.idcontract
left join (select sum(isnull(documentamount,0))ss, d.idcontract
from document d
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value)=14
group by d.idcontract) uTU on uTU.idcontract=ct.idcontract
left join (select sum(isnull(documentamount,0))ss, d.idcontract
from document d
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value) in (12,13)
group by d.idcontract) uObuch on uObuch.idcontract=ct.idcontract
left join (select sum(isnull(documentamount,0))ss, d.idcontract
from document d
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value)=10
group by d.idcontract) uPusk on uPusk.idcontract=ct.idcontract
left join (select sum(isnull(documentamount,0))ss, d.idcontract
from document d
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value)=18
group by d.idcontract) uPlomb on uPlomb.idcontract=ct.idcontract
left join (select sum(isnull(documentamount,0))ss, d.idcontract
from document d
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value)=21
group by d.idcontract) uCredit on uCredit.idcontract=ct.idcontract
left join (select sum(isnull(documentamount,0))ss, d.idcontract
from document d
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value) not in (11,14,12,13,10,18,21)
group by d.idcontract) uProch on uProch.idcontract=ct.idcontract
---
left join(select c.idcontract, sum(convert(decimal(10,2),o.amountoperation)) AmountPay --AmountPayUrid
from document d  with (nolock) 
inner join contract c with (nolock) on d.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idperiod)=1 
inner join batch bb   with (nolock) on d.idbatch=bb.idbatch
inner join operation o with (nolock) on o.iddocument=d.iddocument
inner join balance b with (nolock) on b.idbalance=o.idbalance
and b.idaccounting=6 and (idtypePay=1 or idtypepay=2) and d.idperiod=@idperiod and idtypedocument=1
group by c.idcontract) Pay1 on Pay1.idcontract=ct.idcontract

left join (select c.idcontract,sum(convert(decimal(10,2),o.amountoperation)) AmountPay
from operation oinner join document d  with (nolock) on o.iddocument=d.iddocument
inner join contract c with (nolock) on d.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod and b.idaccounting=6
where o.idtypeoperation=3 group by c.idcontract) car on car.idcontract=ct.idcontract
where isjuridical=1 --and go.idstatusgobject=1 --отвечает за
and len(isnull(pr.NumberDog,''))>0			   --ЮЛ с договором	>0
order by ct.account
--если выбран статус, то применяем его
if @Status<>3
	begin
		delete from #Tbl where status<>@status
	end
--если статус "закрыт" - найдем тип отключения
if @Status=2
	begin
		update #Tbl set TypeEnd=te.name 
		from #Tbl inner join (select te.name, d.idcontract from document d 
		inner join #Tbl t on d.idcontract=t.idcontract and d.idtypedocument=17
		inner join pd on pd.iddocument=d.iddocument and pd.idtypepd=14
		inner join typeend te on te.IDTypeEnd=pd.value
		) te on te.idcontract=#Tbl.idcontract
	end
--переносим в общую таблицу
insert into #tblItog (IDContract, Account, NameObject,NamePerson,RNN,Address, Status, SaldoB, SaldoE, uslTO, uslTU, uslObuch, uslPusk,uslPlomb,uslCredit,uslProch, Pay, uslAll,CarryPay,TypeEnd)
select IDContract, Account, NameObject,NamePerson,RNN,Address, Status, SaldoB, SaldoE, uslTO, uslTU, uslObuch, uslPusk,uslPlomb,uslCredit,uslProch, Pay, uslAll,CarryPay,TypeEnd from #Tbl
update #tblItog set uslAll=isnull(uslTO,0)+isnull(uslTU,0)+isnull(uslPusk,0)+isnull(uslPlomb,0)+isnull(uslObuch,0)+isnull(uslCredit,0)+isnull(uslProch,0)
where NameObject<>'ИТОГО:'
insert into #tblItog (NameObject,NamePerson, SaldoB, SaldoE, uslTO, uslTU, uslObuch, uslPusk,uslPlomb,uslCredit,uslProch, Pay, uslAll,CarryPay)
select 'ИТОГО:','юр.лица с договором на ТО',sum(SaldoB), sum(SaldoE), sum(uslTO), sum(uslTU), sum(uslObuch), sum(uslPusk),sum(uslPlomb),sum(uslCredit),sum(uslProch), sum(Pay), sum(uslAll),sum(CarryPay)
from #Tbl
--очистили временную таблицу
delete from #Tbl
--грузим ЮЛ без договора на ТО
--во временную таблицу
insert into #Tbl (IDContract, Account, NameObject,NamePerson,RNN,Address, Status, SaldoB, SaldoE, uslTO, uslTU, uslObuch, uslPusk,uslPlomb,uslCredit,uslProch, Pay, uslAll,CarryPay)
select ct.idcontract, ct.account, 
--pr.surname+' '+isnull(pr.name,''), 
ltrim(isnull(pr.surname,'-')) NamePerson,
case when len(isnull(pr.name,''))=0 then isnull(pr.patronic,'') else
isnull(pr.name,'')+''+isnull(', '+pr.patronic,'') end FIO, isnull(pr.RNN,'-'),
s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat) as ADDRESS,
ct.status, isnull(SaldoB.SB,0) SaldoB, isnull(SaldoE.SE,0) SaldoE,
isnull(uTO.ss,0),isnull(uTU.ss,0),isnull(uObuch.ss,0),isnull(uPusk.ss,0),isnull(uPlomb.ss,0),isnull(uCredit.ss,0),isnull(uProch.ss,0),
isnull(Pay1.AmountPay,0) Pay,
isnull(uTO.ss,0)+isnull(uTU.ss,0)+isnull(uObuch.ss,0)+isnull(uPusk.ss,0)+isnull(uPlomb.ss,0)+isnull(uCredit.ss,0)+isnull(uProch.ss,0) uslAll,
isnull(car.AmountPay*-1,0)
from person pr (nolock)
inner join address a (nolock) on a.idaddress=pr.idaddress
inner join street s with (nolock)  on s.idstreet=a.idstreet
inner join house hs  with (nolock) on hs.idhouse=a.idhouse
inner join contract ct (nolock) on ct.idperson=pr.idperson
--inner join GObject GO (nolock) on ct.idcontract=GO.idcontract
left join (select idcontract, isnull(AB,0) SB from (select c.IdContract,sum(convert(decimal(10,4),AmountBalance)) AB
from balancereal b with (nolock)
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod=@idPeriodPrev --and dbo.fGetIsJuridical(c.idperson,@idPeriodPrev)=1
and idaccounting=6
Group by c.IdContract)qq) SaldoB on SaldoB.idcontract=ct.idcontract
----
left join (select idcontract, isnull(AB,0) SE from (select c.IdContract,sum(convert(decimal(10,4),AmountBalance)) AB
from balancereal b with (nolock)
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod=@idPeriod --and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1
and idaccounting=6
Group by c.IdContract)qq) SaldoE on SaldoE.idcontract=ct.idcontract
----
left join (select sum(isnull(documentamount,0))ss, d.idcontract
from document d
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value)=11
group by d.idcontract) uTO on uTO.idcontract=ct.idcontract
left join (select sum(isnull(documentamount,0))ss, d.idcontract
from document d
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value)=14
group by d.idcontract) uTU on uTU.idcontract=ct.idcontract
left join (select sum(isnull(documentamount,0))ss, d.idcontract
from document d
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value) in (12,13)
group by d.idcontract) uObuch on uObuch.idcontract=ct.idcontract
left join (select sum(isnull(documentamount,0))ss, d.idcontract
from document d
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value)=10
group by d.idcontract) uPusk on uPusk.idcontract=ct.idcontract
left join (select sum(isnull(documentamount,0))ss, d.idcontract
from document d
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value)=18
group by d.idcontract) uPlomb on uPlomb.idcontract=ct.idcontract
left join (select sum(isnull(documentamount,0))ss, d.idcontract
from document d
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value)=21
group by d.idcontract) uCredit on uCredit.idcontract=ct.idcontract
left join (select sum(isnull(documentamount,0))ss, d.idcontract
from document d
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value) not in (11,14,12,13,10,18,21)
group by d.idcontract) uProch on uProch.idcontract=ct.idcontract
---
left join(select c.idcontract, sum(convert(decimal(10,2),o.amountoperation)) AmountPay --AmountPayUrid
from document d  with (nolock) 
inner join contract c with (nolock) on d.idcontract=c.idcontract 
--and dbo.fGetIsJuridical(c.idperson,@idperiod)=1 
inner join batch bb   with (nolock) on d.idbatch=bb.idbatch
inner join operation o with (nolock) on o.iddocument=d.iddocument
inner join balance b with (nolock) on b.idbalance=o.idbalance
and b.idaccounting=6 and (idtypePay=1 or idtypepay=2) and d.idperiod=@idperiod and idtypedocument=1
group by c.idcontract) Pay1 on Pay1.idcontract=ct.idcontract

left join (select c.idcontract,sum(convert(decimal(10,2),o.amountoperation)) AmountPay
from operation oinner join document d  with (nolock) on o.iddocument=d.iddocument
inner join contract c with (nolock) on d.idcontract=c.idcontract 
--and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod and b.idaccounting=6
where o.idtypeoperation=3 group by c.idcontract) car on car.idcontract=ct.idcontract
where pr.isjuridical=1 --and go.idstatusgobject=1 --отвечает за
and len(isnull(pr.NumberDog,''))=0			   --ЮЛ без договора =0	
order by ct.account
--если указан статус
--то применяем его
if @Status<>3
	begin
		delete from #Tbl where status<>@status
	end
--если статус "закрыт" - найдем тип отключения
if @Status=2
	begin
		update #Tbl set TypeEnd=te.name 
		from #Tbl inner join (select te.name, d.idcontract from document d 
		inner join #Tbl t on d.idcontract=t.idcontract and d.idtypedocument=17
		inner join pd on pd.iddocument=d.iddocument and pd.idtypepd=14
		inner join typeend te on te.IDTypeEnd=pd.value
		) te on te.idcontract=#Tbl.idcontract
	end
--переносим из временной в общую таблицу
insert into #tblItog (IDContract, Account, NameObject,NamePerson,RNN,Address, Status, SaldoB, SaldoE, uslTO, uslTU, uslObuch, uslPusk,uslPlomb,uslCredit,uslProch, Pay, uslAll,CarryPay, TypeEnd)
select IDContract, Account, NameObject,NamePerson,RNN,Address, Status, SaldoB, SaldoE, uslTO, uslTU, uslObuch, uslPusk,uslPlomb,uslCredit,uslProch, Pay, uslAll,CarryPay, TypeEnd 
from #Tbl
update #tblItog set uslAll=isnull(uslTO,0)+isnull(uslTU,0)+isnull(uslPusk,0)+isnull(uslPlomb,0)+isnull(uslObuch,0)+isnull(uslCredit,0)+isnull(uslProch,0)
where NameObject<>'ИТОГО:'
insert into #tblItog (NameObject,NamePerson, SaldoB, SaldoE, uslTO, uslTU, uslObuch, uslPusk,uslPlomb,uslCredit,uslProch, Pay, uslAll,CarryPay)
select 'ИТОГО:','юр.лица без договора на ТО',sum(SaldoB), sum(SaldoE), sum(uslTO), sum(uslTU), sum(uslObuch), sum(uslPusk),sum(uslPlomb),sum(uslCredit),sum(uslProch), sum(Pay), sum(uslAll),sum(CarryPay)
from #Tbl
--очистили временную таблицу
delete from #Tbl 
----
declare @TFizPrev table (idcontract int, status int)
insert into @TFizPrev (idcontract, status)
select c.idcontract, c.status from
Contract c with (nolock) 
	where dbo.fGetIsJuridical(c.idperson,@idPeriodPrev)=0
if @Status<>3
	begin
		delete from @TFizPrev where status<>@status
	end


declare @TFizP table (idcontract int, status int)
insert into @TFizP (idcontract, status)
select c.idcontract, c.status from
Contract c with (nolock) 
	where dbo.fGetIsJuridical(c.idperson,@idPeriod)=0
if @Status<>3
	begin
		delete from @TFizP where status<>@status
	end
----
insert into #tblItog (NameObject,NamePerson)
--declare @IDPeriod int
--declare @idPeriodPrev int
--set @IDPeriod=97
--set @idPeriodPrev=96
select 'справочно','физические лица'
--,SaldoB, SaldoE, uslTO, uslTU, uslObuch, uslPusk,uslProch, Pay, CarryPay
update #tblItog set SaldoB=(select sum(AB) from(select sum(convert(decimal(10,4),AmountBalance)) AB
from balancereal b with (nolock)
inner join @TFizPrev tprev on tprev.idcontract=b.IdContract
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod=@idPeriodPrev --and dbo.fGetIsJuridical(c.idperson,@idPeriodPrev)=0
and idaccounting=6 )qq) where NameObject='справочно'
---
update #tblItog set SaldoE=(select sum(AB) from(select sum(convert(decimal(10,4),AmountBalance)) AB
from balancereal b with (nolock)
inner join @TFizP tprev on tprev.idcontract=b.IdContract
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod=@idPeriod --and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0
and idaccounting=6)qq) where NameObject='справочно'
---
update #tblItog set uslTO=(select sum(convert(decimal(10,2),isnull(documentamount,0))) ss from document d
inner join @TFizP tprev on tprev.idcontract=d.IdContract
inner join Contract c with (nolock) on d.IdContract=c.IdContract
	--and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idPeriod
and convert(int,pd.value)=11) where NameObject='справочно'
---
update #tblItog set uslTU=(select sum(convert(decimal(10,2),isnull(documentamount,0))) ss from document d
inner join @TFizP tprev on tprev.idcontract=d.IdContract
inner join Contract c with (nolock) on d.IdContract=c.IdContract
	--and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value)=14) where NameObject='справочно'
---
update #tblItog set uslObuch=(select sum(convert(decimal(10,2),isnull(documentamount,0))) ss from document d
inner join @TFizP tprev on tprev.idcontract=d.IdContract
inner join Contract c with (nolock) on d.IdContract=c.IdContract
	--and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value) in (12,13)) where NameObject='справочно'
---
update #tblItog set uslPusk=(select sum(convert(decimal(10,2),isnull(documentamount,0))) ss from document d
inner join @TFizP tprev on tprev.idcontract=d.IdContract
inner join Contract c with (nolock) on d.IdContract=c.IdContract
	--and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value)=10) where NameObject='справочно'
---
update #tblItog set uslPlomb=(select sum(convert(decimal(10,2),isnull(documentamount,0))) ss from document d
inner join @TFizP tprev on tprev.idcontract=d.IdContract
inner join Contract c with (nolock) on d.IdContract=c.IdContract
	--and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value)=18) where NameObject='справочно'
---
update #tblItog set uslCredit=(select sum(convert(decimal(10,2),isnull(documentamount,0))) ss from document d
inner join @TFizP tprev on tprev.idcontract=d.IdContract
inner join Contract c with (nolock) on d.IdContract=c.IdContract
	--and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value)=21) where NameObject='справочно'
---
update #tblItog set uslProch=(select sum(convert(decimal(10,2),isnull(documentamount,0))) ss from document d
inner join @TFizP tprev on tprev.idcontract=d.IdContract
inner join Contract c with (nolock) on d.IdContract=c.IdContract
	--and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value) not in (11,14,12,13,10,18,21)) where NameObject='справочно'

update #tblItog set uslProch=uslProch-(select isnull(sum(o.amountoperation),0) dd
from operation o  with (nolock) 
inner join document d  with (nolock) on o.iddocument=d.iddocument
	and d.idperiod=@idperiod and idtypedocument=7--and idtypedocument=3
inner join pd on pd.iddocument=d.iddocument
	and idtypepd=35 and convert(int,pd.value) not in (11,14,12,13,10,18,21)
inner join balance b with (nolock) on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract 
	and b.idperiod=@idperiod
	and b.idaccounting=6 
inner join @TFizP tprev on tprev.idcontract=d.IdContract) where NameObject='справочно'

---
update #tblItog set Pay=(select sum(convert(decimal(10,2),o.amountoperation)) AmountPay --AmountPayUrid
from document d  with (nolock) 
inner join @TFizP tprev on tprev.idcontract=d.IdContract
inner join contract c with (nolock) on d.idcontract=c.idcontract 
--and dbo.fGetIsJuridical(c.idperson,@idperiod)=0
inner join batch bb   with (nolock) on d.idbatch=bb.idbatch
and (idtypePay=1 or idtypePay=2)  and d.idperiod=@idperiod and idtypedocument=1
inner join operation o with (nolock) on o.iddocument=d.iddocument
inner join balance b with (nolock) on b.idbalance=o.idbalance
and b.idaccounting=6) where NameObject='справочно'
---
update #tblItog set CarryPay=(select sum(convert(decimal(10,2),o.amountoperation)) AmountPay
from operation oinner join document d  with (nolock) on o.iddocument=d.iddocument
inner join @TFizP tprev on tprev.idcontract=d.IdContract
inner join contract c with (nolock) on d.idcontract=c.idcontract 
--and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod and b.idaccounting=6
where o.idtypeoperation=3 and d.idtypedocument<>7)*-1 where NameObject='справочно'
---
update #tblItog set uslAll=isnull(uslTO,0)+isnull(uslTU,0)+isnull(uslPusk,0)+isnull(uslPlomb,0)+isnull(uslObuch,0)+isnull(uslCredit,0)+isnull(uslProch,0)
where isnull(NameObject,'')<>'ИТОГО:'
--
----грузим во временную таблицу абонентов с кредитными услугами
declare @TFizCredit table (idcontract int, status int)
insert into @TFizCredit (idcontract, status)
select distinct c.idcontract, c.status from
Contract c with (nolock) 
inner join person p on p.idperson=c.idperson
inner join GrafikForCreditUsl gr on gr.idcontract=c.idcontract
inner join Balance b on b.idcontract=c.idcontract 	and (b.idperiod=@idPeriodPrev or b.idperiod=@idPeriod) and b.idaccounting=6 and b.amountbalance<0
and p.idclassifier=7
if @Status<>3
	begin
		delete from @TFizCredit where status<>@status
	end
--
insert into #Tbl (IDContract, Account, NameObject,NamePerson,RNN,Address, Status, SaldoB, SaldoE, uslTO, uslTU, uslObuch, uslPusk,uslPlomb,uslCredit,uslProch, Pay, uslAll,CarryPay)
select ct.idcontract, ct.account, 
--pr.surname+' '+isnull(pr.name,''), 
ltrim(isnull(pr.surname,'-')) NamePerson,
case when len(isnull(pr.name,''))=0 then isnull(pr.patronic,'') else
isnull(pr.name,'')+''+isnull(' '+pr.patronic,'') end FIO, isnull(pr.RNN,'-'),
s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat) as ADDRESS,
ct.status, isnull(SaldoB.SB,0) SaldoB, isnull(SaldoE.SE,0) SaldoE,
isnull(uTO.ss,0),isnull(uTU.ss,0),isnull(uObuch.ss,0),isnull(uPusk.ss,0),isnull(uPlomb.ss,0),isnull(uCredit.ss,0),isnull(uProch.ss,0),
isnull(Pay1.AmountPay,0) Pay,
isnull(uTO.ss,0)+isnull(uTU.ss,0)+isnull(uObuch.ss,0)+isnull(uPusk.ss,0)+isnull(uPlomb.ss,0)+isnull(uCredit.ss,0)+isnull(uProch.ss,0) uslAll,
isnull(car.AmountPay*-1,0)
from person pr (nolock)
inner join address a (nolock) on a.idaddress=pr.idaddress
inner join street s with (nolock)  on s.idstreet=a.idstreet
inner join house hs  with (nolock) on hs.idhouse=a.idhouse
inner join contract ct (nolock) on ct.idperson=pr.idperson
inner join @TFizCredit fc on fc.idcontract=ct.idcontract
--inner join GObject GO (nolock) on ct.idcontract=GO.idcontract
left join (select idcontract, isnull(AB,0) SB from (select fc.IdContract,sum(convert(decimal(10,4),AmountBalance)) AB
from balancereal b with (nolock)
inner join @TFizCredit fc on fc.idcontract=b.idcontract
	and IdPeriod=@idPeriodPrev and idaccounting=6
Group by fc.IdContract)qq) SaldoB on SaldoB.idcontract=ct.idcontract
--Group by c.IdContract)qq where AB<0) SaldoB on SaldoB.idcontract=ct.idcontract
----
left join (select idcontract, isnull(AB,0) SE from (select fc.IdContract,sum(convert(decimal(10,4),AmountBalance)) AB
from balancereal b with (nolock)
inner join @TFizCredit fc on fc.idcontract=b.idcontract
	and IdPeriod=@idPeriod and idaccounting=6
Group by fc.IdContract)qq) SaldoE on SaldoE.idcontract=ct.idcontract
--Group by c.IdContract)qq where AB<0) SaldoE on SaldoE.idcontract=ct.idcontract
----
left join (select sum(isnull(documentamount,0))ss, d.idcontract
from document d
inner join @TFizCredit fc on fc.idcontract=d.idcontract
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value)=11
group by d.idcontract) uTO on uTO.idcontract=ct.idcontract
left join (select sum(isnull(documentamount,0))ss, d.idcontract
from document d
inner join @TFizCredit fc on fc.idcontract=d.idcontract
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value)=14
group by d.idcontract) uTU on uTU.idcontract=ct.idcontract
left join (select sum(isnull(documentamount,0))ss, d.idcontract
from document d
inner join @TFizCredit fc on fc.idcontract=d.idcontract
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value) in (12,13)
group by d.idcontract) uObuch on uObuch.idcontract=ct.idcontract
left join (select sum(isnull(documentamount,0))ss, d.idcontract
from document d
inner join @TFizCredit fc on fc.idcontract=d.idcontract
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value)=10
group by d.idcontract) uPusk on uPusk.idcontract=ct.idcontract
left join (select sum(isnull(documentamount,0))ss, d.idcontract
from document d
inner join @TFizCredit fc on fc.idcontract=d.idcontract
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value)=18
group by d.idcontract) uPlomb on uPlomb.idcontract=ct.idcontract
left join (select sum(isnull(documentamount,0))ss, d.idcontract
from document d
inner join @TFizCredit fc on fc.idcontract=d.idcontract
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value)=21
group by d.idcontract) uCredit on uCredit.idcontract=ct.idcontract
left join (select sum(isnull(documentamount,0))ss, d.idcontract
from document d
inner join @TFizCredit fc on fc.idcontract=d.idcontract
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and convert(int,pd.value) not in (11,14,12,13,10,18,21)
group by d.idcontract) uProch on uProch.idcontract=ct.idcontract
---
left join(select c.idcontract, sum(convert(decimal(10,2),o.amountoperation)) AmountPay --AmountPayUrid
from document d  with (nolock) 
inner join @TFizCredit fc on fc.idcontract=d.idcontract
inner join contract c with (nolock) on fc.idcontract=c.idcontract 
inner join batch bb   with (nolock) on d.idbatch=bb.idbatch
inner join operation o with (nolock) on o.iddocument=d.iddocument
inner join balance b with (nolock) on b.idbalance=o.idbalance
and b.idaccounting=6 and (idtypePay=1 or idtypepay=2) and d.idperiod=@idperiod and idtypedocument=1
group by c.idcontract) Pay1 on Pay1.idcontract=ct.idcontract

left join (select c.idcontract,sum(convert(decimal(10,2),o.amountoperation)) AmountPay
from operation oinner join document d  with (nolock) on o.iddocument=d.iddocument
inner join @TFizCredit fc on fc.idcontract=d.idcontract
inner join contract c with (nolock) on fc.idcontract=c.idcontract 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod and b.idaccounting=6
where o.idtypeoperation=3 group by c.idcontract) car on car.idcontract=ct.idcontract
order by ct.account
--переносим из временной в общую таблицу
insert into #tblItog (NameObject,NamePerson)
select 'в т.ч.','кредитные услуги'
--
insert into #tblItog (IDContract, Account, NameObject,NamePerson,RNN,Address, Status, SaldoB, SaldoE, uslTO, uslTU, uslObuch, uslPusk,uslPlomb,uslCredit,uslProch, Pay, uslAll,CarryPay, TypeEnd)
select IDContract, Account, NameObject,NamePerson,RNN,Address, Status, SaldoB, SaldoE, uslTO, uslTU, uslObuch, uslPusk,uslPlomb,uslCredit,uslProch, Pay, uslAll,CarryPay, TypeEnd 
from #Tbl

--
insert into #tblItog (NameObject, SaldoB, SaldoE, uslTO, uslTU, uslObuch, uslPusk,uslPlomb,uslCredit,uslProch, Pay, uslAll,CarryPay)
select 'ИТОГО:',sum(SaldoB), sum(SaldoE), sum(uslTO), sum(uslTU), sum(uslObuch), sum(uslPusk),sum(uslPlomb),sum(uslCredit),sum(uslProch), sum(Pay), sum(uslAll),sum(CarryPay)
from #tblItog where idcontract IS NULL
--

--

declare @SQL varchar(8000)
set @SQL='insert OpenDataSource(''Microsoft.Jet.OLEDB.4.0'',''Data Source="' + @path +
'";User ID=Admin;Password=;Extended properties=Excel 5.0'')...[Лист2$]
(c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14, c15, c16, c17, c18, c19, c20, c21,c22,c23,c24,c25,c26,c27,c28,c29)'
set @SQL=@SQL+' select' 
if @status <3
	begin
		set @SQL=@SQL+' Account, NameObject,NamePerson,RNN, Address, replace(SaldoB,''.'','',''), replace(uslTO,''.'','',''), '
		set @SQL=@SQL+'replace(uslTU,''.'','',''), replace(uslObuch,''.'','',''), replace(uslPusk,''.'','',''),replace(uslPlomb,''.'','',''), '
		set @SQL=@SQL+'replace(uslCredit,''.'','',''),replace(uslProch,''.'','',''),replace(uslAll,''.'','',''),replace(Pay,''.'','',''), replace(CarryPay,''.'','',''), replace(SaldoE,''.'','',''), '
		set @SQL=@SQL+'replace(SumuslTO,''.'','',''), replace(SumuslTU,''.'','',''), '
		set @SQL=@SQL+'replace(SumuslObuch,''.'','',''), replace(SumuslPusk,''.'','',''),replace(SumuslPlomb,''.'','',''),replace(SumuslCredit,''.'','',''),replace(SumuslProch,''.'','',''), replace(SumuslAll,''.'','',''), '
		set @SQL=@SQL+'replace(SumPay,''.'','',''),replace(SumCarry,''.'','',''), '''+@MonthName+''', TypeEnd'
		set @SQL=@SQL+' from #tblItog where status='+convert(varchar(5),@status)+' or isnull(status,5)=5' 
	end
else
	begin
		set @SQL=@SQL+' Account, NameObject,NamePerson,RNN, Address, replace(SaldoB,''.'','',''), replace(uslTO,''.'','',''), '
		set @SQL=@SQL+'replace(uslTU,''.'','',''), replace(uslObuch,''.'','',''), replace(uslPusk,''.'','',''), replace(uslPlomb,''.'','',''), '
		set @SQL=@SQL+'replace(uslCredit,''.'','',''),replace(uslProch,''.'','',''),replace(uslAll,''.'','',''),replace(Pay,''.'','',''), replace(CarryPay,''.'','',''), replace(SaldoE,''.'','',''), '
		set @SQL=@SQL+'replace(SumuslTO,''.'','',''), replace(SumuslTU,''.'','',''), '
		set @SQL=@SQL+'replace(SumuslObuch,''.'','',''), replace(SumuslPusk,''.'','',''),replace(SumuslPlomb,''.'','',''),replace(SumuslCredit,''.'','',''), replace(SumuslProch,''.'','',''), replace(SumuslAll,''.'','',''), '
		set @SQL=@SQL+'replace(SumPay,''.'','',''),replace(SumCarry,''.'','',''), '''+@MonthName+''', TypeEnd'
		set @SQL=@SQL+' from #tblItog'
	end

--select @SQL
exec (@SQL)
--select * from #tblItog where status=@status or isnull(status,5)=5
--select * from #tblItog where idcontract is null
--select * from #tblItog

drop table #Tbl

drop table #tblItog





























GO