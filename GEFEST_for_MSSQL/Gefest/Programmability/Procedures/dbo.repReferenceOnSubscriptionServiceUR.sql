SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO


















CREATE PROCEDURE [dbo].[repReferenceOnSubscriptionServiceUR] (@idPeriod int) AS
---------------**************Справка по абонентской службе*********-------------------
SET NOCOUNT ON

if @idPeriod=122
begin
exec repReferenceOnSubscriptionServiceURDTariff @idPeriod
end
else
begin
--declare @idPeriod int
--set @idPeriod=83

declare @idPeriodPrev int

set @idPeriodPrev=dbo.fGetPredPeriodVariable(@idPeriod)
declare @tariff float
select @tariff=value from tariff t where t.IdPeriod=@IdPeriod

create table #AnalSal (BeginDt float, BeginKt float,BeginDtUR float, BeginKtUR float
,AmountNachPU float,AmountNachPUDT float, M3NachPU float, M3NachPUDT float,
AmountKGNachPU float,AmountKGNachPUDT float, KGNachPU float, KGNachPUDT float,
M3KN float, KGKN float, AmountKN float, 
AmountNachG float, AmountPay float,AmountPayBEZ float, AmountPayG float,AmountPayGBEZNAL float,
EndDt float, EndKt float,EndDtUR float, EndKtUR float
, tariff float, AmountNachPUUrid float, AmountNachPUUridDT float, M3NachPUUrid float, M3NachPUUridDT float, 
AmountKGNachPUUrid float,AmountKGNachPUUridDT float, KGNachPUUrid float, KGNachPUUridDT float,AmountNachNUrid float, 
M3KNUrid float, KGKNUrid float, AmountKNUrid float, 
AmountNachGUrid float, AmountPayUrid float,AmountPayBEZUrid float, AmountPayGUrid float,AmountPayGUridBEZNAL float,
 Koeff float, fschetpog float, fschetpogUr float, tariffff float, KorekCUBURD float, KorekCUB float,KorekUrdAmount float,KorekUrdAmountKG float, KorekKGURD float, KorekKG float,
KorekAmount float,KorekAmountKG float, PenyAmount float, PenyAmountBEZNAL float, PenyNach float, PenyAmountUr float,PenyAmountUrBEZNAL float,  PenyNachUr float,
KoreUrPOPU float, KorePOPU float,KoreUrPOPUSum float, KorePOPUSum float,  KoreUrPOKG float, KorePOKG float,KoreUrPOKGSum float, KorePOKGSum float, PerenosOplUr float, PerenosOplFiz float)


insert #AnalSal (tariffff)

--select case when idperiod<63 then (value*2.85) else value end as tariffff from tariff group by case when idperiod<63 then (value*2.85) else value end  
select top 1 value from tariff order by idperiod desc
--select value tariffff
----, convert(float,null) BeginDt , convert(float,null) BeginKt ,convert(float,null) AmountNachPU,convert(float,null) M3NachPU, 
----convert(float,null) AmountKGNachPU, convert(float,null) KGNachPU,
----convert(float,null) M3KN, convert(float,null) KGKN, convert(float,null) AmountKN, 
----convert(float,null) AmountNachG, convert(float,null) AmountPay, convert(float,null) AmountPayBEZ, convert(float,null) AmountPayG,
----convert(float,null) EndDt, convert(float,null) EndKt, convert(float,null) tariff, 
----convert(float,null) AmountNachPUUrid, convert(float,null) M3NachPUUrid, 
----convert(float,null) AmountKGNachPUUrid, convert(float,null) KGNachPUUrid, convert(float,null) AmountNachNUrid, 
----convert(float,null) M3KNUrid, convert(float,null) KGKNUrid, convert(float,null) AmountKNUrid, 
----convert(float,null) AmountNachGUrid, convert(float,null) AmountPayUrid, convert(float,null) AmountPayBEZUrid, convert(float,null) AmountPayGUrid,
----convert(float,null) Koeff, convert(float,null) fschetpog, convert(float,null) KorekCUBURD, convert(float,null) KorekCUB, convert(float,null) KorekKGURD, convert(float,null) KorekKG
----into #AnalSal
--from tariff  with (nolock) where value is not null group by value 

--update #AnalSal 
--set korekCUBURD=gt.ss,
--KorekUrdAmount=-gt.ff
--from #AnalSal ee
--inner join (
--select sum(factamount) ss, sum(documentamount)ff, pd.value dd from document d
--inner join contract c  with (nolock)  on c.idcontract=d.idcontract
--and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1
--inner join pd with (nolock)  on pd.iddocument=d.iddocument
--and idtypepd=13 
--inner join factuse f with (nolock)  on f.iddocument=d.iddocument
--and f.idtypefu=1 
----inner join tariff t with (nolock)  on convert(float,ltrim(replace(pd.value,',','.')))=t.value
--group by pd.value) gt on convert(float,ltrim(replace(gt.dd,',','.')))=ee.tariffff


update #AnalSal 
set KoreUrPOPU=isnull(gt.ss,0),
KoreUrPOPUSum=isnull(-gt.ff,0)
from (select sum(factamount) ss, sum(documentamount)ff from document d with (nolock) 
inner join contract c  with (nolock)  on c.idcontract=d.idcontract
and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1
inner join pd with (nolock)  on pd.iddocument=d.iddocument
and idtypepd=13 
inner join factuse f with (nolock)  on f.iddocument=d.iddocument
and f.idtypefu=1) gt 

update #AnalSal 
set KorePOPU=isnull(gt.ss,0),
KorePOPUSum=isnull(-gt.ff,0)
from (select sum(factamount) ss, sum(documentamount)ff from document d with (nolock) 
inner join contract c  with (nolock)  on c.idcontract=d.idcontract
and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0
inner join pd with (nolock)  on pd.iddocument=d.iddocument
and idtypepd=13 
inner join factuse f with (nolock)  on f.iddocument=d.iddocument
and f.idtypefu=1) gt 

update #AnalSal 
set KoreUrPOKG=isnull(gt.ss,0),
KoreUrPOKGSum=isnull(-gt.ff,0)
from (select sum(factamount) ss, sum(documentamount)ff from document d with (nolock) 
inner join contract c  with (nolock)  on c.idcontract=d.idcontract
and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1
inner join pd with (nolock)  on pd.iddocument=d.iddocument
and idtypepd=13 
inner join factuse f with (nolock)  on f.iddocument=d.iddocument
and f.idtypefu=2) gt 

update #AnalSal 
set KorePOKG=isnull(gt.ss,0),
KorePOKGSum=isnull(-gt.ff,0)
from (select sum(factamount) ss, sum(documentamount)ff from document d with (nolock) 
inner join contract c  with (nolock)  on c.idcontract=d.idcontract
and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0
inner join pd with (nolock)  on pd.iddocument=d.iddocument
and idtypepd=13 
inner join factuse f with (nolock)  on f.iddocument=d.iddocument
and f.idtypefu=2) gt 

--update #AnalSal 
--set korekCUB=gt.ss,
--KorekAmount=-gt.ff
--from #AnalSal ee
--inner join (
--select sum(factamount) ss, sum(documentamount)ff,pd.value dd from document d
--inner join contract c  with (nolock)  on c.idcontract=d.idcontract
--and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0
--inner join pd with (nolock)  on pd.iddocument=d.iddocument
--and idtypepd=13 
--inner join factuse f with (nolock)  on f.iddocument=d.iddocument
--and f.idtypefu=1 
----inner join tariff t with (nolock)  on convert(float,ltrim(replace(pd.value,',','.')))=t.value
--group by pd.value) gt on convert(float,ltrim(replace(gt.dd,',','.')))=ee.tariffff

update #AnalSal 
set korekCUB=gt.ss,
KorekAmount=-gt.ff
from (
select sum(factamount) ss, sum(documentamount)ff from document d with (nolock) 
inner join contract c  with (nolock)  on c.idcontract=d.idcontract
and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0
inner join pd with (nolock)  on pd.iddocument=d.iddocument
and idtypepd=13 
inner join factuse f with (nolock)  on f.iddocument=d.iddocument
and f.idtypefu=1 
) gt 


--update #AnalSal 
--set KorekKGURD=gt.ss,
--KorekUrdAmountKG=-gt.ff
--from #AnalSal ee
--inner join (
--select sum(factamount) ss, sum(documentamount)ff,pd.value dd from document d
--inner join contract c  with (nolock)  on c.idcontract=d.idcontract
--and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1
--inner join pd with (nolock)  on pd.iddocument=d.iddocument
--and idtypepd=13 
--inner join factuse f with (nolock)  on f.iddocument=d.iddocument
--and f.idtypefu=2 
----inner join tariff t with (nolock)  on convert(float,ltrim(replace(pd.value,',','.')))=t.value
--group by pd.value) gt on convert(float,ltrim(replace(gt.dd,',','.')))=ee.tariffff

update #AnalSal 
set KorekKGURD=isnull(gt.ss,0),
KorekUrdAmountKG=isnull(-gt.ff,0)
from (
select sum(factamount) ss, sum(documentamount)ff from document d with (nolock) 
inner join contract c  with (nolock)  on c.idcontract=d.idcontract
and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1
inner join pd with (nolock)  on pd.iddocument=d.iddocument
and idtypepd=13 
inner join factuse f with (nolock)  on f.iddocument=d.iddocument
and f.idtypefu=2 
) gt 


--update #AnalSal 
--set KorekUrdAmountKG=KorekUrdAmountKG-isnull(gt.ff,0),
--KorekKGURD=KorekKGURD-isnull(gt.ss,0)
--from (
--select sum(factamount) ss, sum(documentamount)ff from document d
--inner join contract c  with (nolock)  on c.idcontract=d.idcontract
--and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1
--inner join pd with (nolock)  on pd.iddocument=d.iddocument
--and idtypepd=13 
--inner join pd  pdd with (nolock)  on pdd.iddocument=d.iddocument
--and pdd.idtypepd=34 and pdd.value=2
--) gt 

--update #AnalSal 
--set KorekKG=gt.ss,
--KorekAmountKG=-gt.ff
--from #AnalSal ee
--inner join (
--select sum(factamount) ss, sum(documentamount)ff,pd.value dd from document d
--inner join contract c  with (nolock)  on c.idcontract=d.idcontract
--and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0
--inner join pd with (nolock)  on pd.iddocument=d.iddocument
--and idtypepd=13 
--inner join factuse f with (nolock)  on f.iddocument=d.iddocument
--and f.idtypefu=2 
----inner join tariff t with (nolock)  on convert(float,ltrim(replace(pd.value,',','.')))=t.value
--group by pd.value) gt on convert(float,ltrim(replace(gt.dd,',','.')))=ee.tariffff

update #AnalSal 
set KorekKG=isnull(gt.ss,0),
KorekAmountKG=isnull(-gt.ff,0)
from (
select sum(factamount) ss, sum(documentamount)ff from document d with (nolock) 
inner join contract c  with (nolock)  on c.idcontract=d.idcontract
and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0
inner join pd with (nolock)  on pd.iddocument=d.iddocument
and idtypepd=13 
inner join factuse f with (nolock)  on f.iddocument=d.iddocument
and f.idtypefu=2 
) gt 
---посредней мошности
update #AnalSal 
set KorekKG=KorekKG-isnull(gt.ss,0),
KorekAmountKG=KorekAmountKG-isnull(gt.ff,0)
from (
select sum(factamount) ss, sum(documentamount)ff from document d with (nolock) 
inner join contract c  with (nolock)  on c.idcontract=d.idcontract
and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0
inner join pd with (nolock)  on pd.iddocument=d.iddocument
and idtypepd=13 
inner join factuse f with (nolock)  on f.iddocument=d.iddocument
and f.idtypefu=2 
) gt 

delete #AnalSal where KorekKG is null and KorekKGURD is null and korekCUB is null and korekCUBURD is null

update #AnalSal set KorekKG=0 where KorekKG is null 
update #AnalSal set KorekKGURD=0 where KorekKGURD is null 
update #AnalSal set korekCUB=0 where  korekCUB is null 
update #AnalSal set korekCUBURD=0 where korekCUBURD is null
update #AnalSal set KorekAmount=0 where KorekAmount is null
update #AnalSal set KorekUrdAmount=0 where KorekUrdAmount is null
update #AnalSal set KorekAmountKG=0 where KorekAmountKG is null
update #AnalSal set KorekUrdAmountKG=0 where KorekUrdAmountKG is null
update #AnalSal 
--set BeginKt=isnull(AA,0), tariff=@tariff,Koeff=2.85
--from( select sum(AB) AA from
--(select sum(convert(decimal(10,4),AmountBalance)) AB
--from balancereal b with (nolock)
--inner join Contract c with (nolock) on b.IdContract=c.IdContract
--	and IdPeriod=@idPeriodPrev and dbo.fGetIsJuridical(c.idperson,@idPeriodPrev)=0
--and idaccounting<>6
--Group by c.IdContract) qq  where AB>0) ww 

set BeginKt=isnull(AA,0), tariff=@tariff,Koeff=2.85
from( select sum(round (AB,2)) AA from
(select sum(AmountBalance) AB
from balancereal b with (nolock)
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod=@idPeriodPrev and dbo.fGetIsJuridical(c.idperson,@idPeriodPrev)=0
and idaccounting<>6
Group by c.IdContract) qq  where AB>0) ww 


update #AnalSal 
set BeginDt=isnull(-AA,0)
from( select sum(round (AB,2)) AA from
(select sum(AmountBalance) AB
from balancereal b with (nolock)
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod=@idPeriodPrev and dbo.fGetIsJuridical(c.idperson,@idPeriodPrev)=0
and idaccounting<>6
Group by c.IdContract) qq  where AB<0) ww 

update #AnalSal 
set EndDt=isnull(-AA,0)
from( select sum(round (AB,2)) AA from
(select sum(AmountBalance) AB
from balancereal b with (nolock)
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod= @idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0
and idaccounting<>6
Group by c.IdContract) qq  where AB<0) ww 

update #AnalSal 
set EndKt=isnull(AA,0)
from( select sum(round (AB,2)) AA from
(select sum(AmountBalance) AB
from balancereal b with (nolock)
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod= @idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0
and idaccounting<>6
Group by c.IdContract) qq  where AB>0) ww 


update #AnalSal 
set BeginKtUR=isnull(AA,0)
from( select sum(round (AB,2)) AA from
(select sum(convert(decimal(10,4),AmountBalance)) AB
from balancereal b with (nolock)
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod=@idPeriodPrev and dbo.fGetIsJuridical(c.idperson,@idPeriodPrev)=1
and idaccounting<>6
Group by c.IdContract) qq  where AB>0) ww 


update #AnalSal 
set BeginDtUR=isnull(-AA,0)
from( select sum(round (AB,2)) AA from
(select sum(convert(decimal(10,4),AmountBalance)) AB
from balancereal b with (nolock)
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod=@idPeriodPrev and dbo.fGetIsJuridical(c.idperson,@idPeriodPrev)=1
and idaccounting<>6
Group by c.IdContract) qq  where AB<0) ww 

update #AnalSal 
set EndDtUR=isnull(-AA,0)
from( select sum(round (AB,2)) AA from
(select sum(convert(decimal(10,4),AmountBalance)) AB
from balancereal b with (nolock)
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod= @idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1
and idaccounting<>6
Group by c.IdContract) qq  where AB<0) ww 

update #AnalSal 
set EndKtUR=isnull(AA,0)
from( select sum(round (AB,2)) AA from
(select sum(convert(decimal(10,4),AmountBalance)) AB
from balancereal b with (nolock)
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod= @idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1
and idaccounting<>6
Group by c.IdContract) qq  where AB>0) ww 


update #AnalSal 
set M3NachPU=isnull(cubes,0),
AmountNachPU=isnull(ao,0)
from (select sum(f.FactAmount) cubes, sum(AmountOperation) ao
from FactUse f with (nolock) 
inner join indication i with (nolock) on i.idindication=f.idindication
	and isnull(f.IdTypeFU,0)=1 
inner join operation o with (nolock) on o.idoperation=f.idoperation
inner join document d with (nolock) on d.iddocument=o.iddocument
	and idtypedocument=5
	and  f.IdPeriod=@idperiod
inner join contract c with (nolock) on d.idcontract=c.idcontract and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0 ) qq

update #AnalSal 
set KGNachPU=isnull(cubes,0),
AmountKGNachPU=isnull(ao,0)
from (select sum(f.FactAmount) cubes, sum(AmountOperation) ao
from FactUse f with (nolock) 
inner join operation o   with (nolock) on o.idoperation=f.idoperation
inner join document d with (nolock) on d.iddocument=o.iddocument
	and idtypedocument=5 
	and isnull(f.IdTypeFU,0)=2 
	and f.IdPeriod=@idperiod
inner join contract c with (nolock) on d.idcontract=c.idcontract and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0 ) qq


update #AnalSal 
set M3KN=isnull(cubes,0)
from (select sum(f.FactAmount) cubes
from FactUse f with (nolock) 
inner join document d  with (nolock) on d.iddocument=f.iddocument
	and d.idtypedocument=7
	and f.IdPeriod=@idperiod and isnull(f.IdTypeFU,0)=1 
inner join contract c with (nolock) on d.idcontract=c.idcontract and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0) qq

update #AnalSal 
set KGKN=isnull(cubes,0)
from (select sum(f.FactAmount) cubes
from FactUse f with (nolock) 
inner join document d  with (nolock) on d.iddocument=f.iddocument
	and d.idtypedocument=7
	and f.IdPeriod=@idperiod and isnull(f.IdTypeFU,0)=2 
inner join contract c with (nolock) on d.idcontract=c.idcontract and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0) qq

update #AnalSal 
set AmountKN=isnull(-dd,0)
from (select  sum(documentamount) dd
 from document d
inner join contract c  with (nolock)  on c.idcontract=d.idcontract
and idtypedocument=7 and d.idperiod=@idperiod --and dbo.fGetIsJuridical(c.idperson,36)=1
inner join pd with (nolock)  on pd.iddocument=d.iddocument
and idtypepd=13  and pd.value='0'
) qq

update #AnalSal 
set AmountNachG=isnull(-dd,0)
from (select sum(o.amountoperation) dd
from operation o  with (nolock) 
inner join balance b with (nolock) on b.idbalance=o.idbalance
	and b.idperiod=@idperiod and b.idaccounting in (2,3) --=3
	and o.idtypeoperation=2
inner join contract c with (nolock) on b.idcontract=c.idcontract and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0 ) qq

update #AnalSal 
set AmountNachG=AmountNachG-isnull(gt.ff,0)
from (
select  sum(documentamount)ff from document d
inner join contract c  with (nolock)  on c.idcontract=d.idcontract
and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0
inner join operation o with (nolock)  on d.iddocument=o.iddocument
inner join balance b with (nolock)  on b.idbalance=o.idbalance
and b.idaccounting in (2,3)
) gt 

update #AnalSal 
set PenyNach=isnull(-dd,0)
from (select sum(o.amountoperation) dd
from operation o  with (nolock)
inner join balance b with (nolock) on b.idbalance=o.idbalance
	and b.idperiod=@idperiod and b.idaccounting=4
	and o.idtypeoperation=2
inner join contract c with (nolock) on b.idcontract=c.idcontract where dbo.fGetIsJuridical(c.idperson,@idPeriod)=0 ) qq



update #AnalSal 
set PenyNach=PenyNach-isnull(gt.ff,0)
from (
select sum(documentamount)ff from document d
inner join contract c  with (nolock)  on c.idcontract=d.idcontract
and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0
inner join operation o with (nolock)  on d.iddocument=o.iddocument
inner join balance b with (nolock)  on b.idbalance=o.idbalance
and b.idaccounting=4
) gt 


update #AnalSal 
set PenyNach=PenyNach-isnull(gt.ff,0)
from (
select  sum(o.amountoperation)ff from document d
inner join contract c  with (nolock)  on c.idcontract=d.idcontract
and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0
inner join operation o with (nolock) on o.iddocument=d.iddocument
and isnull(o.numberoperation,0) <> 99999
inner join pd with (nolock)  on pd.iddocument=d.iddocument
and idtypepd=13 
inner join pd  pdd with (nolock)  on pdd.iddocument=d.iddocument
and pdd.idtypepd=34 and pdd.value=2
) gt 

--set AmountPay=round(isnull(dd,0),2,1)
update #AnalSal 
set AmountPay=round(isnull(dd,0),2)
from (select sum(o.amountoperation) dd
from document d  with (nolock) 
inner join contract c with (nolock) on d.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idperiod)=0 
inner join batch bb   with (nolock) on d.idbatch=bb.idbatch
and idtypePay=1  and d.idperiod=@idperiod and idtypedocument=1
inner join operation o with (nolock) on o.iddocument=d.iddocument
inner join balance b with (nolock) on b.idbalance=o.idbalance
--	and b.idperiod=@idperiod
	and (b.idaccounting=1 or b.idaccounting=2)
-- and o.idtypeoperation=1
) qq

update #AnalSal 
set AmountPayBEZ=isnull(dd,0)
from (select sum(o.amountoperation) dd
from document d  with (nolock) 
inner join contract c with (nolock) on d.idcontract=c.idcontract and dbo.fGetIsJuridical(c.idperson,@idperiod)=0 
inner join batch bb   with (nolock) on d.idbatch=bb.idbatch
and idtypePay=2  and d.idperiod=@idperiod and idtypedocument=1
inner join operation o with (nolock) on o.iddocument=d.iddocument
inner join balance b with (nolock) on b.idbalance=o.idbalance
--	and b.idperiod=@idperiod
	and (b.idaccounting=1 or b.idaccounting=2)
--inner join balance b with (nolock) on b.idbalance=o.idbalance
--	and b.idperiod=@idperiod
--	and b.idaccounting<>3 and o.idtypeoperation=1
) qq

update #AnalSal 
set AmountPayG=isnull(dd,0)
from (select sum(o.amountoperation) dd
from operation o  with (nolock) 
inner join document d  with (nolock) on o.iddocument=d.iddocument
inner join batch bb   with (nolock) on d.idbatch=bb.idbatch
and idtypePay=1  and d.idperiod=@idperiod and idtypedocument=1
inner join balance b with (nolock) on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0 
	and b.idperiod=@idperiod
	and b.idaccounting=3 and o.idtypeoperation=1) qq

update #AnalSal 
set AmountPayGBEZNAL=isnull(dd,0)
from (select sum(o.amountoperation) dd
from operation o  with (nolock) 
inner join document d  with (nolock) on o.iddocument=d.iddocument
inner join batch bb   with (nolock) on d.idbatch=bb.idbatch
and idtypePay=2  and d.idperiod=@idperiod and idtypedocument=1
inner join balance b with (nolock) on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0 
	and b.idperiod=@idperiod
	and b.idaccounting=3 and o.idtypeoperation=1) qq

update #AnalSal 
set PenyAmount=isnull(dd,0)
from (select sum(o.amountoperation) dd
from operation o  with (nolock) 
inner join document d  with (nolock) on o.iddocument=d.iddocument
inner join batch bb   with (nolock) on d.idbatch=bb.idbatch
and idtypePay=1  and d.idperiod=@idperiod and idtypedocument=1
inner join balance b with (nolock) on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0 
	and b.idperiod=@idperiod
	and b.idaccounting=4 and o.idtypeoperation=1) qq

update #AnalSal 
set PenyAmountBEZNAL=ceiling(isnull(dd,0)*100)/100 --round(isnull(dd,0),2) isnull(dd,0)
from (select sum(o.amountoperation) dd
from operation o  with (nolock) 
inner join document d  with (nolock) on o.iddocument=d.iddocument
inner join batch bb   with (nolock) on d.idbatch=bb.idbatch
and idtypePay=2  and d.idperiod=@idperiod and idtypedocument=1
inner join balance b with (nolock) on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0 
	and b.idperiod=@idperiod
	and b.idaccounting=4 and o.idtypeoperation=1) qq


update #AnalSal 
set PerenosOplFiz=isnull(dd,0)*-1
from (select sum(o.amountoperation) dd
from operation o  with (nolock) 
inner join document d  with (nolock) on o.iddocument=d.iddocument
and d.idperiod=@idperiod and idtypedocument<>7--and idtypedocument=3
inner join balance b with (nolock) on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract and dbo.fGetIsJuridical(c.idperson,@idPeriod)=0 
	and b.idperiod=@idperiod
	and b.idaccounting=6 and o.idtypeoperation=3) qq

---------------------
--для юридических лиц





update #AnalSal 
set M3NachPUUrid=isnull(cubes,0),
AmountNachPUUrid=isnull(ao,0)
from (select sum(f.FactAmount) cubes, sum(AmountOperation) ao
from FactUse f with (nolock) 
inner join indication i  with (nolock) on i.idindication=f.idindication
	and isnull(f.IdTypeFU,0)=1 
inner join operation o   with (nolock) on o.idoperation=f.idoperation
inner join document d with (nolock) on d.iddocument=o.iddocument
	and idtypedocument=5
	and  f.IdPeriod=@idperiod
inner join contract c with (nolock) on d.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1 ) qq

update #AnalSal 
set KGNachPUUrid=isnull(cubes,0),
AmountKGNachPUUrid=isnull(ao,0)
from (select sum(f.FactAmount) cubes, sum(AmountOperation) ao
from FactUse f with (nolock) 
inner join operation o   with (nolock) on o.idoperation=f.idoperation
inner join document d with (nolock) on d.iddocument=o.iddocument
	and idtypedocument=5 
	and isnull(f.IdTypeFU,0)=2
	and f.IdPeriod=@idperiod
inner join contract c with (nolock) on d.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1 ) qq


update #AnalSal 
set M3KNUrid=isnull(cubes,0)
from (select sum(f.FactAmount) cubes
from FactUse f with (nolock) 
inner join document d  with (nolock) on d.iddocument=f.iddocument
	and d.idtypedocument=7
	and f.IdPeriod=@idperiod and isnull(f.IdTypeFU,0)=1 
inner join contract c with (nolock) on d.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1) qq

update #AnalSal 
set KGKNUrid=isnull(cubes,0)
from (select sum(f.FactAmount) cubes
from FactUse f with (nolock) 
inner join document d  with (nolock) on d.iddocument=f.iddocument
	and d.idtypedocument=7
	and f.IdPeriod=@idperiod and isnull(f.IdTypeFU,0)=2 
inner join contract c with (nolock) on d.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1) qq

update #AnalSal 
set AmountKNUrid=isnull(dd,0)
from (select sum(d.documentamount) dd
from document d  with (nolock) 
inner join contract c with (nolock) on d.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1 
where d.idtypedocument=7
	and d.idperiod=@idperiod
) qq


update #AnalSal 
set AmountNachGUrid=isnull(-dd,0)
from (select sum(o.amountoperation) dd
from operation o  with (nolock) 
inner join balance b with (nolock) on b.idbalance=o.idbalance
	and b.idperiod=@idperiod and b.idaccounting=3
	and o.idtypeoperation=2
inner join contract c with (nolock) on b.idcontract=c.idcontract
 and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1 ) qq

update #AnalSal 
set AmountNachGUrid=AmountNachGUrid-isnull(gt.ff,0)
from (
select sum(documentamount)ff from document d
inner join contract c  with (nolock)  on c.idcontract=d.idcontract
and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1
inner join operation o with (nolock)  on d.iddocument=o.iddocument
inner join balance b with (nolock)  on b.idbalance=o.idbalance
and b.idaccounting=3
) gt 

update #AnalSal 
set PenyNachUr=isnull(-dd,0)
from (select sum(o.amountoperation) dd
from operation o  with (nolock) 
inner join balance b with (nolock) on b.idbalance=o.idbalance
	and b.idperiod=@idperiod and b.idaccounting=4
	and o.idtypeoperation=2
inner join contract c with (nolock) on b.idcontract=c.idcontract
 and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1 ) qq

update #AnalSal 
set PenyNachUr=PenyNachUr-isnull(gt.ff,0)
from (
select  sum(documentamount)ff from document d
inner join contract c  with (nolock)  on c.idcontract=d.idcontract
and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1
inner join operation o with (nolock)  on d.iddocument=o.iddocument
inner join balance b with (nolock)  on b.idbalance=o.idbalance
and b.idaccounting=4
) gt 

update #AnalSal 
set PenyNachUr=PenyNachUr-isnull(gt.ff,0)
from (
select  sum(documentamount)ff from document d
inner join contract c  with (nolock)  on c.idcontract=d.idcontract
and idtypedocument=7 and d.idperiod=@idPeriod and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1
inner join pd with (nolock)  on pd.iddocument=d.iddocument
and idtypepd=13 
inner join pd  pdd with (nolock)  on pdd.iddocument=d.iddocument
and pdd.idtypepd=34 and pdd.value=2
) gt 

update #AnalSal 
set AmountPayUrid=isnull(dd,0)
from (select round(sum(o.amountoperation),2) dd
from document d  with (nolock) 
inner join contract c with (nolock) on d.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idperiod)=1 
inner join batch bb   with (nolock) on d.idbatch=bb.idbatch
and idtypePay=1  and d.idperiod=@idperiod and idtypedocument=1
--inner join balance b with (nolock) on b.idbalance=o.idbalance
inner join operation o with (nolock) on o.iddocument=d.iddocument
inner join balance b with (nolock) on b.idbalance=o.idbalance
--	and b.idperiod=@idperiod
	and (b.idaccounting=1 or b.idaccounting=2)
--	and b.idperiod=@idperiod
--	and b.idaccounting<>3 and o.idtypeoperation=1
) qq

update #AnalSal 
set AmountPayBEZUrid=isnull(dd,0)
from (select sum(o.amountoperation) dd
from document d  with (nolock)
inner join contract c with (nolock) on d.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idperiod)=1 
inner join batch bb   with (nolock) on d.idbatch=bb.idbatch
and idtypePay=2  and d.idperiod=@idperiod and idtypedocument=1
inner join operation o with (nolock) on o.iddocument=d.iddocument
inner join balance b with (nolock) on b.idbalance=o.idbalance
--	and b.idperiod=@idperiod
	and (b.idaccounting=1 or b.idaccounting=2)
--inner join balance b with (nolock) on b.idbalance=o.idbalance
--	and b.idperiod=@idperiod
--	and b.idaccounting<>3 and o.idtypeoperation=1
) qq

update #AnalSal 
set AmountPayGUrid=isnull(dd,0)
from (select sum(o.amountoperation) dd
from operation o  with (nolock)
inner join document d  with (nolock) on o.iddocument=d.iddocument
inner join batch bb   with (nolock) on d.idbatch=bb.idbatch
and idtypePay=1  and d.idperiod=@idperiod and idtypedocument=1 
inner join balance b with (nolock) on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1 
	and b.idperiod=@idperiod
	and b.idaccounting=3 and o.idtypeoperation=1) qq

update #AnalSal 
set AmountPayGUridBEZNAL=isnull(dd,0)
from (select sum(o.amountoperation) dd
from operation o  with (nolock)
inner join document d  with (nolock) on o.iddocument=d.iddocument
inner join batch bb   with (nolock) on d.idbatch=bb.idbatch
and idtypePay=2  and d.idperiod=@idperiod and idtypedocument=1 
inner join balance b with (nolock) on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1 
	and b.idperiod=@idperiod
	and b.idaccounting=3 and o.idtypeoperation=1) qq

update #AnalSal 
set PenyAmountUr=isnull(dd,0)
from (select sum(o.amountoperation) dd
from operation o  with (nolock)
inner join document d  with (nolock) on o.iddocument=d.iddocument
inner join batch bb   with (nolock) on d.idbatch=bb.idbatch
and idtypePay=1  and d.idperiod=@idperiod and idtypedocument=1 
inner join balance b with (nolock) on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1 
	and b.idperiod=@idperiod
	and b.idaccounting=4 and o.idtypeoperation=1) qq

update #AnalSal 
set PenyAmountUrBEZNAL=isnull(dd,0)
from (select sum(o.amountoperation) dd
from operation o  with (nolock)
inner join document d  with (nolock) on o.iddocument=d.iddocument
inner join batch bb   with (nolock) on d.idbatch=bb.idbatch
and idtypePay=2  and d.idperiod=@idperiod and idtypedocument=1 
inner join balance b with (nolock) on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract 
and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1 
	and b.idperiod=@idperiod
	and b.idaccounting=4 and o.idtypeoperation=1) qq

update #AnalSal 
set PerenosOplUr=isnull(dd,0)*-1
from (select sum(o.amountoperation) dd
from operation o  with (nolock) 
inner join document d  with (nolock) on o.iddocument=d.iddocument
and d.idperiod=@idperiod and idtypedocument<>7--and idtypedocument=3
inner join balance b with (nolock) on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract and dbo.fGetIsJuridical(c.idperson,@idPeriod)=1 
	and b.idperiod=@idperiod
	and b.idaccounting=6 and o.idtypeoperation=3) qq


-------------------
--select c.idcontract,  dbo.fGetLastBalanceReal(@idPeriodPrev, c.IdContract, 0) AmountBalance,
select distinct c.idcontract,  isnull(dbo.fGetLastBalanceRealBezUslug(@idPeriodPrev, c.IdContract, 0) ,0) AmountBalance,
		convert(float, 0) DAP, convert(float ,0) ForSaldo , isnull(dbo.fGetIsJuridical(c.idperson,@idPeriod),0) Status
		
into #tmpForSaldo
from contract c  with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
left join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
	and gm.idstatusgmeter=1

update #tmpForSaldo
set DAP=qq.DocumentAmount
from #tmpForSaldo t 
inner join (select dc.idcontract, sum(o.amountoperation) DocumentAmount
from document dc with (nolock)
inner join operation o with (nolock) on o.iddocument=dc.iddocument
and dc.idtypedocument=1
	and dc.idperiod=@idPeriod
inner join balance b with (nolock) on b.idbalance=o.idbalance
--	and b.idperiod=@idperiod
	and b.idaccounting<>6
group by dc.idcontract) qq on qq.idcontract=t.idcontract




update #tmpForSaldo
set ForSaldo=case when AmountBalance<0 then 
	case when DAP>-(AmountBalance) then -(AmountBalance) else DAP end
	else 0 end
from #tmpForSaldo 


update #AnalSal 
set fschetpog=(select sum (ForSaldo) from #tmpForSaldo where status=0)
update #AnalSal 
set fschetpogUr=(select sum (ForSaldo) from #tmpForSaldo where status=1)


drop table #tmpForSaldo

select * from #AnalSal order by tariffff desc
drop table #AnalSal

end




















GO