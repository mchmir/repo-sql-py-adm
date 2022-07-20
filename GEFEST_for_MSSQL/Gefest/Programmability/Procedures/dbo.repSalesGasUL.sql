SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO










CREATE PROCEDURE [dbo].[repSalesGasUL] (@idPeriod int, @Status int, @Path varchar(1000), @MonthName varchar(100) ) AS
---------------**************Отчет по реализации газа по ЮЛ*********-------------------
SET NOCOUNT ON
--declare @IDPeriod int
declare @idPeriodPrev int
declare @tariff float
set @MonthName='по состоянию на '+@MonthName+'г.'
--set @idPeriod=92
select @tariff=value from tariff t where t.IdPeriod=@IdPeriod
set @idPeriodPrev=dbo.fGetPredPeriodVariable(@idPeriod)

create table #Tbl (IDContract int, Account varchar(50), NameObject varchar(220), NamePerson varchar(220), RNN varchar(12), Address varchar(200), Status int, SaldoB decimal(10,2), SaldoE decimal(10,2),
Potreb float, NachZaGaz decimal(10,2), NachGP decimal(10,2), NachPeny decimal(10,2), AllNach decimal(10,2), Pay decimal(10,2),
SumPotreb float, SumNachZaGaz decimal(10,2), SumNachGP decimal(10,2), SumNachPeny decimal(10,2), SumPay decimal(10,2), SumAllNach decimal(10,2),SumCarry decimal(10,2), CarryPay decimal(10,2))
insert into #Tbl (IDContract, Account, NameObject,NamePerson,RNN,Address, Status, SaldoB, SaldoE, Potreb, NachZaGaz, NachGP, NachPeny, Pay, AllNach,CarryPay)
select ct.idcontract, ct.account, 
--pr.surname+' '+isnull(pr.name,''), 
ltrim(isnull(pr.surname,'')) NamePerson,
case when len(isnull(pr.name,''))=0 then isnull(pr.patronic,'') else
isnull(pr.name,'')+''+isnull(', '+pr.patronic,'') end FIO, isnull(pr.RNN,''),
s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat) as ADDRESS,
ct.status, isnull(SaldoB.SB,0) SaldoB, isnull(SaldoE.SE,0) SaldoE,
isnull(fu.FAmount,0) Potreb, 
(isnull(M3Nach.M3NachPUUrid,0)+isnull(KoreUrPU.KoreUrPOPU,0)+isnull(KGNach.KGNachPUUrid,0)+isnull(KoreUrKG.KoreUrPOKG,0))*@Tariff as NachZaGaz,
--isnull(NachVDGO.AmountNachVDGO,0) NachVDGO,
isnull(NachG1.AmountNachGUrid,0)-isnull(NachG2.AmountNachGUrid,0) NachG,
isnull(PenyNach1.PenyNach,0)-isnull(PenyNach2.PenyNach,0)-isnull(PenyNach3.PenyNach,0) NachPeny,
isnull(Pay1.AmountPay,0)+isnull(Pay2.AmountPay,0)+isnull(Pay3.AmountPay,0)+isnull(Pay4.AmountPay,0)+isnull(Pay5.AmountPay,0)+isnull(Pay6.AmountPay,0) Pay,
((isnull(M3Nach.M3NachPUUrid,0)+isnull(KoreUrPU.KoreUrPOPU,0)+isnull(KGNach.KGNachPUUrid,0)+isnull(KoreUrKG.KoreUrPOKG,0))*@Tariff) +
--isnull(NachVDGO.AmountNachVDGO,0)+
(isnull(NachG1.AmountNachGUrid,0)-isnull(NachG2.AmountNachGUrid,0))+
(isnull(PenyNach1.PenyNach,0)-isnull(PenyNach2.PenyNach,0)-isnull(PenyNach3.PenyNach,0)), isnull(car.AmountPay*-1,0)
from person pr (nolock)
inner join address a (nolock) on a.idaddress=pr.idaddress
inner join street s with (nolock)  on s.idstreet=a.idstreet
inner join house hs  with (nolock) on hs.idhouse=a.idhouse
inner join contract ct (nolock) on ct.idperson=pr.idperson
inner join GObject GO (nolock) on ct.idcontract=GO.idcontract
left join (select idcontract, isnull(AB,0) SB from (select c.IdContract,sum(convert(decimal(10,4),AmountBalance)) AB
from balancereal b with (nolock)
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod=@idPeriodPrev and dbo.fGetIsJuridical(c.idperson,@idPeriodPrev)=1
and idaccounting<>6
Group by c.IdContract)qq) SaldoB on SaldoB.idcontract=ct.idcontract
--Group by c.IdContract)qq where AB<0) SaldoB on SaldoB.idcontract=ct.idcontract
left join (select idcontract, isnull(AB,0) SE from (select c.IdContract,sum(convert(decimal(10,4),AmountBalance)) AB
from balancereal b with (nolock)
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1
and idaccounting<>6
Group by c.IdContract)qq) SaldoE on SaldoE.idcontract=ct.idcontract
--Group by c.IdContract)qq where AB<0) SaldoE on SaldoE.idcontract=ct.idcontract
left join (select sum(fu.FactAmount) FAmount, fu.idgobject from FactUse FU (nolock) where fu.idperiod=@IDPeriod 
group by fu.idgobject) FU on GO.idgobject=fu.idgobject --and fu.idperiod=@IDPeriod
left join (select c.idcontract,  sum(f.FactAmount) M3NachPUUrid, sum(AmountOperation) AmountNachPUUrid
from FactUse f with (nolock) 
inner join indication i  with (nolock) on i.idindication=f.idindication
	and isnull(f.IdTypeFU,0)=1 
inner join operation o   with (nolock) on o.idoperation=f.idoperation
inner join document d with (nolock) on d.iddocument=o.iddocument
	and idtypedocument=5
	and  f.IdPeriod=@idPeriod
inner join contract c with (nolock) on d.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1 
group by c.idcontract) M3Nach on M3Nach.idcontract=ct.idcontract
left join (select c.idcontract, sum(factamount) as KoreUrPOPU, isnull(-sum(documentamount),0) as KoreUrPOPUSum 
from document d with (nolock) 
inner join contract c  with (nolock)  on c.idcontract=d.idcontract
and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1
inner join pd with (nolock)  on pd.iddocument=d.iddocument
and idtypepd=13 
inner join factuse f with (nolock)  on f.iddocument=d.iddocument
and f.idtypefu=1
group by c.idcontract) KoreUrPU on KoreUrPU.idcontract=ct.idcontract
left join (select c.idcontract, sum(f.FactAmount) KGNachPUUrid, sum(AmountOperation) AmountKGNachPUUrid
from FactUse f with (nolock) 
inner join operation o   with (nolock) on o.idoperation=f.idoperation
inner join document d with (nolock) on d.iddocument=o.iddocument
	and idtypedocument=5 
	and isnull(f.IdTypeFU,0)=2
	and f.IdPeriod=@idperiod
inner join contract c with (nolock) on d.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1 
group by c.idcontract) KGNach on KGNach.idcontract=ct.idcontract
left join (select c.idcontract, sum(factamount) KoreUrPOKG, isnull(-sum(documentamount),0) KoreUrPOKGSum from document d with (nolock) 
inner join contract c  with (nolock)  on c.idcontract=d.idcontract
and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1
inner join pd with (nolock)  on pd.iddocument=d.iddocument
and idtypepd=13 
inner join factuse f with (nolock)  on f.iddocument=d.iddocument
and f.idtypefu=2
group by c.idcontract) KoreUrKG on KoreUrKG.idcontract=ct.idcontract
left join (select c.idcontract, isnull(-sum(o.amountoperation),0) AmountNachGUrid
from operation o  with (nolock) 
inner join balance b with (nolock) on b.idbalance=o.idbalance
	and b.idperiod=@idperiod and b.idaccounting=3
	and o.idtypeoperation=2
inner join contract c with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1 
group by c.idcontract) NachG1 on nachg1.idcontract=ct.idcontract
left join (select c.idcontract, isnull(sum(documentamount),0) AmountNachGUrid from document d
inner join contract c  with (nolock)  on c.idcontract=d.idcontract
and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1
inner join operation o with (nolock)  on d.iddocument=o.iddocument
inner join balance b with (nolock)  on b.idbalance=o.idbalance
and b.idaccounting=3
group by c.idcontract) NachG2 on nachg2.idcontract=ct.idcontract
--left join (select c.idcontract, isnull(-sum(o.amountoperation),0) AmountNachVDGO
--from operation o  with (nolock) 
--inner join balance b with (nolock) on b.idbalance=o.idbalance
--	and b.idperiod=@idperiod and b.idaccounting=6
--	and o.idtypeoperation=2
--inner join contract c with (nolock) on b.idcontract=c.idcontract
--and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1 
--group by c.idcontract) NachVDGO on NachVDGO.idcontract=ct.idcontract

left join (select c.idcontract,  isnull(-sum(o.amountoperation),0) PenyNach
from operation o  with (nolock) 
inner join balance b with (nolock) on b.idbalance=o.idbalance
	and b.idperiod=@idperiod and b.idaccounting=4
	and o.idtypeoperation=2
inner join contract c with (nolock) on b.idcontract=c.idcontract
 and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1
group by c.idcontract) PenyNach1 on PenyNach1.idcontract=ct.idcontract
left join (select c.idcontract,  isnull(sum(documentamount),0) PenyNach  from document d
inner join contract c  with (nolock)  on c.idcontract=d.idcontract
and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1
inner join operation o with (nolock)  on d.iddocument=o.iddocument
inner join balance b with (nolock)  on b.idbalance=o.idbalance
and b.idaccounting=4 
group by c.idcontract) PenyNach2 on PenyNach2.idcontract=ct.idcontract
left join (select c.idcontract,  isnull(sum(documentamount),0) PenyNach  from document d
inner join contract c  with (nolock)  on c.idcontract=d.idcontract
and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1
inner join pd with (nolock)  on pd.iddocument=d.iddocument
and idtypepd=13 
inner join pd  pdd with (nolock)  on pdd.iddocument=d.iddocument
and pdd.idtypepd=34 and pdd.value=2
group by c.idcontract) PenyNach3 on PenyNach3.idcontract=ct.idcontract
left join(select c.idcontract, round(sum(o.amountoperation),2) AmountPay --AmountPayUrid
from document d  with (nolock) 
inner join contract c with (nolock) on d.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idperiod)=1 
inner join batch bb   with (nolock) on d.idbatch=bb.idbatch
and idtypePay=1  and d.idperiod=@idperiod and idtypedocument=1
inner join operation o with (nolock) on o.iddocument=d.iddocument
inner join balance b with (nolock) on b.idbalance=o.idbalance
and (b.idaccounting=1 or b.idaccounting=2)
group by c.idcontract) Pay1 on Pay1.idcontract=ct.idcontract
left join(select c.idcontract, sum(o.amountoperation) AmountPay-- AmountPayBEZUrid
from document d  with (nolock)
inner join contract c with (nolock) on d.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idperiod)=1 
inner join batch bb   with (nolock) on d.idbatch=bb.idbatch
and idtypePay=2  and d.idperiod=@idperiod and idtypedocument=1
inner join operation o with (nolock) on o.iddocument=d.iddocument
inner join balance b with (nolock) on b.idbalance=o.idbalance
and (b.idaccounting=1 or b.idaccounting=2)
group by c.idcontract) Pay2 on Pay2.idcontract=ct.idcontract
left join(select c.idcontract, sum(o.amountoperation) AmountPay --AmountPayGUrid
from operation o  with (nolock)
inner join document d  with (nolock) on o.iddocument=d.iddocument
inner join batch bb   with (nolock) on d.idbatch=bb.idbatch
and idtypePay=1  and d.idperiod=@idperiod and idtypedocument=1 
inner join balance b with (nolock) on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1 
	and b.idperiod=@idperiod
	and b.idaccounting=3 and o.idtypeoperation=1
group by c.idcontract) Pay3 on Pay3.idcontract=ct.idcontract
left join(select c.idcontract, sum(o.amountoperation) AmountPay --AmountPayGUridBEZNAL
from operation o  with (nolock)
inner join document d  with (nolock) on o.iddocument=d.iddocument
inner join batch bb   with (nolock) on d.idbatch=bb.idbatch
and idtypePay=2  and d.idperiod=@idperiod and idtypedocument=1 
inner join balance b with (nolock) on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1 
	and b.idperiod=@idperiod
	and b.idaccounting=3 and o.idtypeoperation=1
group by c.idcontract) Pay4 on Pay4.idcontract=ct.idcontract
left join(select c.idcontract, sum(o.amountoperation) AmountPay --PenyAmountUr
from operation o  with (nolock)
inner join document d  with (nolock) on o.iddocument=d.iddocument
inner join batch bb   with (nolock) on d.idbatch=bb.idbatch
and idtypePay=1  and d.idperiod=@idperiod and idtypedocument=1 
inner join balance b with (nolock) on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1 
	and b.idperiod=@idperiod
	and b.idaccounting=4 and o.idtypeoperation=1
group by c.idcontract) Pay5 on Pay5.idcontract=ct.idcontract
left join(select c.idcontract, sum(o.amountoperation) AmountPay --PenyAmountUrBEZNAL
from operation o  with (nolock)
inner join document d  with (nolock) on o.iddocument=d.iddocument
inner join batch bb   with (nolock) on d.idbatch=bb.idbatch
and idtypePay=2  and d.idperiod=@idperiod and idtypedocument=1 
inner join balance b with (nolock) on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1 
	and b.idperiod=@idperiod
	and b.idaccounting=4 and o.idtypeoperation=1
group by c.idcontract) Pay6 on Pay6.idcontract=ct.idcontract
left join (select c.idcontract,sum(convert(decimal(10,2),o.amountoperation)) AmountPay
from operation oinner join document d  with (nolock) on o.iddocument=d.iddocument
inner join contract c with (nolock) on d.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod and b.idaccounting<>6
where o.idtypeoperation=3 group by c.idcontract) car on car.idcontract=ct.idcontract
where isjuridical=1 --and go.idstatusgobject=1
order by ct.account
--
if @Status<>3
	begin
		delete from #Tbl where status<>@status
	end
--

update #Tbl
set SumPotreb=(select sum(Potreb) from #TBL)
update #Tbl
set SumNachZaGaz=(select sum(NachZaGaz) from #TBL)
----update #Tbl
----set SumNachVDGO=(select sum(NachVDGO) from #TBL)
update #Tbl
set SumNachGP=(select sum(NachGP) from #TBL)
update #Tbl
set SumNachPeny=(select sum(NachPeny) from #TBL)
update #Tbl
set SumPay=(select sum(Pay) from #TBL)
--update #Tbl
--set AllNach=(select NachZaGaz+NachGP+NachPeny from #TBL)
update #Tbl
set SumCarry=(select sum(CarryPay) from #TBL)
update #Tbl
set SumAllNach=(select sum(AllNach) from #TBL)


declare @SQL varchar(8000)
set @SQL='insert OpenDataSource(''Microsoft.Jet.OLEDB.4.0'',''Data Source="' + @path +
'";User ID=Admin;Password=;Extended properties=Excel 5.0'')...[Лист2$]
(c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14, c15, c16, c17, c18, c19, c20, c21,c22)'
set @SQL=@SQL+' select' 
if @status <3
	begin
		set @SQL=@SQL+' Account, NameObject,NamePerson,RNN, Address, replace(SaldoB,''.'','',''),  replace(Potreb,''.'','',''), replace(NachZaGaz,''.'','',''), '
		set @SQL=@SQL+'replace(NachGP,''.'','',''), replace(NachPeny,''.'','',''), replace(AllNach,''.'','',''), '
		set @SQL=@SQL+'replace(Pay,''.'','',''), replace(CarryPay,''.'','',''), replace(SaldoE,''.'','',''), '
		set @SQL=@SQL+'replace(SumPotreb,''.'','',''), replace(SumNachZaGaz,''.'','',''), '
		set @SQL=@SQL+'replace(SumNachGP,''.'','',''), replace(SumNachPeny,''.'','',''), replace(SumAllNach,''.'','',''), '
		set @SQL=@SQL+'replace(SumPay,''.'','',''),replace(SumCarry,''.'','',''), '''+@MonthName+''''
		set @SQL=@SQL+' from #Tbl where status='+convert(varchar(5),@status)
	end
else
	begin
		set @SQL=@SQL+' Account, NameObject,NamePerson,RNN,Address, replace(SaldoB,''.'','',''),  replace(Potreb,''.'','',''), replace(NachZaGaz,''.'','',''), '
		set @SQL=@SQL+'replace(NachGP,''.'','',''), replace(NachPeny,''.'','',''), replace(AllNach,''.'','',''), '
		set @SQL=@SQL+'replace(Pay,''.'','',''), replace(CarryPay,''.'','',''), replace(SaldoE,''.'','',''), '
		set @SQL=@SQL+'replace(SumPotreb,''.'','',''), replace(SumNachZaGaz,''.'','',''), '
		set @SQL=@SQL+'replace(SumNachGP,''.'','',''), replace(SumNachPeny,''.'','',''), replace(SumAllNach,''.'','',''), '
		set @SQL=@SQL+'replace(SumPay,''.'','',''),replace(SumCarry,''.'','',''), '''+@MonthName+''''
		set @SQL=@SQL+' from #Tbl'
	end

--select @SQL
exec (@SQL)
--select * from #tbl
drop table #Tbl







GO