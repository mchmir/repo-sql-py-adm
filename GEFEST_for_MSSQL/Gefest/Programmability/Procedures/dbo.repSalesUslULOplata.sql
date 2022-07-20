SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO




















CREATE PROCEDURE [dbo].[repSalesUslULOplata] (@idPeriod int, @Status int, @Path varchar(1000), @MonthName varchar(100) ) AS
---------------**************Отчет по реализации газа по ЮЛ*********-------------------
SET NOCOUNT ON
--declare @IDPeriod int
declare @idPeriodPrev int
declare @tariff float
set @MonthName='по состоянию на '+@MonthName+'г.'
--set @idPeriod=92
select @tariff=value from tariff t where t.IdPeriod=@IdPeriod
set @idPeriodPrev=dbo.fGetPredPeriodVariable(@idPeriod)
---
declare @T table (idcontract int, adres varchar(200),NameObject varchar(220), NamePerson varchar(220))
insert into @T (idcontract, adres, NameObject,NamePerson)
select distinct c.idcontract,s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(aa.flat) as ADRES,
ltrim(isnull(pr.surname,'-')) NamePerson,
case when len(isnull(pr.name,''))=0 then isnull(pr.patronic,'') else
isnull(pr.name,'')+''+isnull(', '+pr.patronic,'') end FIO
from document d
inner join contract c on c.idcontract=d.idcontract
	and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1 
inner join person pr with (nolock) on c.idperson=pr.idperson
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
	and b.idaccounting=6
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
--and idtypepay=2
inner join GObject g  with (nolock) on g.IdContract=c.IdContract
inner join address aa  with (nolock) on aa.idaddress=g.idaddress
inner join street s with (nolock)  on s.idstreet=aa.idstreet
inner join house hs  with (nolock) on hs.idhouse=aa.idhouse
	and idtypedocument=1 and d.idperiod=@idperiod
--
--if @Status<>0
--	begin
--		delete from #Tbl where status<>@status
--	end
--



--select * from @T
---
create table #tblItog (tp int, IDContract int,TypeAbonent varchar(50), Account varchar(50), NameObject varchar(220), NamePerson varchar(220), RNN varchar(12), Address varchar(200), Status int,
Pay decimal(10,2),DatePay datetime, NameBank varchar(200))
--грузим ЮЛ без договора на ТО
insert into #tblItog (tp,IDContract,TypeAbonent, Account, NameObject, NamePerson, RNN, Address, Status,
Pay, DatePay, NameBank)
select 1,ct.idcontract,'Юридические лица без договора на ТО', ct.account, 
--pr.surname+' '+isnull(pr.name,''), 
t.NameObject,t.NamePerson, isnull(pr.RNN,'-'),
t.ADRES,
ct.status, convert(decimal(10,2),sum(o.amountoperation)) as Suma,
d.documentdate documentdate,isnull(replace(a.name,'яБанк','Банк'),'Касса - '+aa.name) as BANK
from person pr (nolock)
inner join contract ct (nolock) on ct.idperson=pr.idperson
	and dbo.fGetIsJuridical(ct.idperson,@idPeriod)=1 
inner join @T t on t.idcontract=ct.idcontract
inner join document d (nolock) on ct.idcontract=d.idcontract
	and idtypedocument=1 and d.idperiod=@idperiod
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=6
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
--and idtypepay=2
left join agent a with (nolock) on a.idagent=bb.iddispatcher
left join agent aa with (nolock) on aa.idagent=bb.idCashier
where --isjuridical=1 --and go.idstatusgobject=1
len(isnull(pr.NumberDog,''))=0
group by ct.idcontract, ct.account, 
--pr.surname+' '+isnull(pr.name,''), 
t.NameObject,t.NamePerson, isnull(pr.RNN,'-'),
t.ADRES,ct.status, d.documentdate ,a.name, aa.name 
order by ct.account
--грузим ЮЛ с договором на ТО
insert into #tblItog (tp,IDContract,TypeAbonent, Account, NameObject, NamePerson, RNN, Address, Status,
Pay, DatePay, NameBank)
select 2,ct.idcontract,'Юридические лица с договором на ТО', ct.account, 
--pr.surname+' '+isnull(pr.name,''), 
t.NameObject,t.NamePerson, isnull(pr.RNN,'-'),
t.ADRES,
ct.status, convert(decimal(10,2),sum(o.amountoperation)) as Suma,
d.documentdate documentdate,isnull(replace(a.name,'яБанк','Банк'),'Касса - '+aa.name) as BANK
from person pr (nolock)
inner join contract ct (nolock) on ct.idperson=pr.idperson
	and dbo.fGetIsJuridical(ct.idperson,@idPeriod)=1 
inner join @T t on t.idcontract=ct.idcontract
inner join document d (nolock) on ct.idcontract=d.idcontract
	and idtypedocument=1 and d.idperiod=@idperiod
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=6
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
--and idtypepay=1
left join agent a with (nolock) on a.idagent=bb.iddispatcher
left join agent aa with (nolock) on aa.idagent=bb.idCashier
where --isjuridical=1 --and go.idstatusgobject=1
len(isnull(pr.NumberDog,''))>0
group by ct.idcontract, ct.account, 
--pr.surname+' '+isnull(pr.name,''), 
t.NameObject,t.NamePerson, isnull(pr.RNN,'-'),
t.ADRES,ct.status, d.documentdate ,a.name,aa.name 
order by ct.account
----
declare @TFiz table (idcontract int, status int)
insert into @TFiz (idcontract, status)
select ct.idcontract, ct.status
from person pr (nolock)
inner join contract ct (nolock) on ct.idperson=pr.idperson
	and dbo.fGetIsJuridical(ct.idperson,@idPeriod)=0 
--
--if @Status<>0
--	begin
--		delete from @TFiz where status<>@status
--	end
--

insert into #tblItog (tp,TypeAbonent,NameObject, Pay)
select 3,'Физические лица','БАНК',
convert(decimal(10,2),sum(o.amountoperation)) as Suma
from person pr (nolock)
inner join contract ct (nolock) on ct.idperson=pr.idperson
	and dbo.fGetIsJuridical(ct.idperson,@idPeriod)=0 
inner join document d (nolock) on ct.idcontract=d.idcontract
	and idtypedocument=1 and d.idperiod=@idperiod
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=6
inner join @TFiz tf on tf.idcontract=ct.idcontract

--where isjuridical=0
insert into #tblItog (tp,TypeAbonent,NameObject, Pay)
select 3,'Физические лица','КАССА',
convert(decimal(10,2),sum(o.amountoperation)) as Suma
from person pr (nolock)
inner join contract ct (nolock) on ct.idperson=pr.idperson
	and dbo.fGetIsJuridical(ct.idperson,@idPeriod)=0 
inner join document d (nolock) on ct.idcontract=d.idcontract
	and idtypedocument=1 and d.idperiod=@idperiod
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=1
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=6
inner join @TFiz tf on tf.idcontract=ct.idcontract
--where isjuridical=0

---
--

declare @SQL varchar(8000)
set @SQL='insert OpenDataSource(''Microsoft.Jet.OLEDB.4.0'',''Data Source="' + @path +
'";User ID=Admin;Password=;Extended properties=Excel 5.0'')...[Лист2$]
(c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14, c15, c16, c17, c18, c19, c20, c21,c22,c23,c24)'
set @SQL=@SQL+' select' 
if @status <3
	begin
		set @SQL=@SQL+' Account, NameObject,NamePerson,RNN, Address, replace(SaldoB,''.'','',''), replace(uslTO,''.'','',''), '
		set @SQL=@SQL+'replace(uslTU,''.'','',''), replace(uslObuch,''.'','',''), replace(uslPusk,''.'','',''), '
		set @SQL=@SQL+'replace(uslProch,''.'','',''),replace(uslAll,''.'','',''),replace(Pay,''.'','',''), replace(CarryPay,''.'','',''), replace(SaldoE,''.'','',''), '
		set @SQL=@SQL+'replace(SumuslTO,''.'','',''), replace(SumuslTU,''.'','',''), '
		set @SQL=@SQL+'replace(SumuslObuch,''.'','',''), replace(SumuslPusk,''.'','',''),replace(SumuslProch,''.'','',''), replace(SumuslAll,''.'','',''), '
		set @SQL=@SQL+'replace(SumPay,''.'','',''),replace(SumCarry,''.'','',''), '''+@MonthName+''''
		set @SQL=@SQL+' from #tblItog where status='+convert(varchar(5),@status)+' or isnull(status,5)=5' 
	end
else
	begin
		set @SQL=@SQL+' Account, NameObject,NamePerson,RNN, Address, replace(SaldoB,''.'','',''), replace(uslTO,''.'','',''), '
		set @SQL=@SQL+'replace(uslTU,''.'','',''), replace(uslObuch,''.'','',''), replace(uslPusk,''.'','',''), '
		set @SQL=@SQL+'replace(uslProch,''.'','',''),replace(uslAll,''.'','',''),replace(Pay,''.'','',''), replace(CarryPay,''.'','',''), replace(SaldoE,''.'','',''), '
		set @SQL=@SQL+'replace(SumuslTO,''.'','',''), replace(SumuslTU,''.'','',''), '
		set @SQL=@SQL+'replace(SumuslObuch,''.'','',''), replace(SumuslPusk,''.'','',''),replace(SumuslProch,''.'','',''), replace(SumuslAll,''.'','',''), '
		set @SQL=@SQL+'replace(SumPay,''.'','',''),replace(SumCarry,''.'','',''), '''+@MonthName+''''
		set @SQL=@SQL+' from #tblItog'
	end

--select @SQL
--exec (@SQL)
select * from #tblItog order by tp--where status=@status or isnull(status,5)=5
--select * from #tblItog where idcontract is null

drop table #tblItog

















GO