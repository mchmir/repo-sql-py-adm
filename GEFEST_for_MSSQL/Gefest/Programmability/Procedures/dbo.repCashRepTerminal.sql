SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO












CREATE PROCEDURE [dbo].[repCashRepTerminal] (@IDCashier int, @DateB datetime) AS


--declare @IDCashier int, @DateB datetime
--set @IDCashier=125
--set @DateB='2013-02-06'

declare @SummUslFL float
set @SummUslFL=0
declare @RSummUslFL float
set @RSummUslFL=0
declare @SummUslUL float
set @SummUslUL=0
declare @SummGasFL float
set @SummGasFL=0
declare @RSummGasFL float
set @RSummGasFL=0
declare @SummGasUL float
set @SummGasUL=0
declare @LastAmountBalance float
set @LastAmountBalance=dbo.fLastAmountBalance(@IDCashier,@DateB)
declare @AmountControler float
set @AmountControler=0
declare @IDDispatcher int
set @IDDispatcher =113 --sberabnk

set @DateB = dbo.DateOnly(@DateB)
set @SummGasFL=(select sum(amountoperation)
from document d with (nolock) 
inner join batch bb with (nolock)  on bb.idbatch=d.idbatch
 and dbo.DateOnly(batchdate) = @DateB 
		and IDCashier = @IDCashier 
inner join operation o with (nolock)  on d.iddocument=o.iddocument
inner join balance b with (nolock)  on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract
inner join person p with (nolock) on c.idperson=p.idperson
where p.IsJuridical=0 and b.idaccounting<>6)

set @DateB = dbo.DateOnly(@DateB)
set @SummUslFL=(select sum(amountoperation)
from document d with (nolock) 
inner join batch bb with (nolock)  on bb.idbatch=d.idbatch
 and dbo.DateOnly(batchdate) = @DateB 
		and IDCashier = @IDCashier
inner join operation o with (nolock)  on d.iddocument=o.iddocument
inner join balance b with (nolock)  on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract
inner join person p with (nolock) on c.idperson=p.idperson
where p.IsJuridical=0 and b.idaccounting=6)

set @DateB = dbo.DateOnly(@DateB)
set @RSummGasFL=(select sum(amountoperation)*-1
from document d with (nolock) 
inner join batch bb with (nolock)  on bb.idbatch=d.idbatch
 and dbo.DateOnly(batchdate) = @DateB 
		and IDCashier = @IDCashier
inner join operation o with (nolock)  on d.iddocument=o.iddocument
inner join balance b with (nolock)  on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract
inner join person p with (nolock) on c.idperson=p.idperson
where p.IsJuridical=0 and b.idaccounting<>6 and o.amountoperation<0)

set @DateB = dbo.DateOnly(@DateB)
set @RSummUslFL=(select sum(amountoperation)*-1
from document d with (nolock) 
inner join batch bb with (nolock)  on bb.idbatch=d.idbatch
 and dbo.DateOnly(batchdate) = @DateB 
		and IDCashier = @IDCashier
inner join operation o with (nolock)  on d.iddocument=o.iddocument
inner join balance b with (nolock)  on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract
inner join person p with (nolock) on c.idperson=p.idperson
where p.IsJuridical=0 and b.idaccounting=6 and o.amountoperation<0)


set @SummGasFL=isnull(@SummGasFL,0)+isnull(@SummGasUL,0)
set @SummUslFL=isnull(@SummUslFL,0)+isnull(@SummUslUL,0)


set @AmountControler=(select sum(amountoperation)
from document d with (nolock) 
inner join batch bb with (nolock)  on bb.idbatch=d.idbatch
 and dbo.DateOnly(batchdate) = @DateB and bb.idtypebatch=1
		and IDCashier = @IDCashier
inner join operation o with (nolock)  on d.iddocument=o.iddocument
inner join balance b with (nolock)  on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract
inner join person p with (nolock) on c.idperson=p.idperson)
--select @AmountControler

declare @Info table (ID int,Nomer varchar(20), Otkogo varchar(200), Kassir varchar(100), Summa float, Rashod float,
Ostatok float, NanNac float, Schet varchar(10), iddispatcher float, idtypebatch int, SummCard float)
--От физических лиц за газ
insert into @Info (ID, Nomer, Otkogo, Kassir, Summa, Rashod, Ostatok, NanNac, Schet, iddispatcher, idtypebatch)
select 1, b.NumberBatch Nomer, 
--case when b.IdDispatcher is null then 
--	case when b.IdTypeBatch>2 then 
--		case when CHARINDEX('@', b.Name)>0 
--		then substring(b.name, CHARINDEX('@', b.Name)+1, len(b.Name)-CHARINDEX('@', b.Name)) 
--		else ''
--		end
--	 else 'От физических лиц за газ' 
--	 end 
--else a.Name 
--end 
'От физических лиц за газ' Otkogo
,A1.Name Kassir,
--case when b.IdTypeBatch not in (3,6) then 
--	case when b.IdTypeBatch=2 
--	then res.amount--@SummGasFL
--	else BatchAmount 
--	end
--else '' end 
--res.amount Summa
@SummGasFL Summa
--,case when b.IdTypeBatch in (3,6) then
--b.batchamount else
--case when b.IdTypeBatch=2 then (-1)*convert(float, dbo.fGetSumOfAmountDocumentByIDBatch(b.idbatch,0))
--else '' end  end Rashod
,@RSummGasFL Rashod
,cb.amountbalance Ostatok, @LastAmountBalance NanNac,--dbo.fLastAmountBalance(@IDCashier,@DateB) NanNac,
--case when b.name = '' or  b.name = '0' then '1210.1'
--else case when CHARINDEX('@', b.Name)>0 then substring(b.name, 1, CHARINDEX('@', b.Name)-1) else '1210.1' end end Schet
'1211' Schet,
--,isnull(b.iddispatcher,0) iddispatcher,
@AmountControler
,b.idtypebatch idtypebatch
from Batch b  with (nolock)
--inner join (select bb.idbatch idbatch, sum(amountoperation) amount
--from document d with (nolock) 
--inner join batch bb with (nolock)  on bb.idbatch=d.idbatch
-- and dbo.DateOnly(batchdate) = @DateB 
--		and IDCashier = @IDCashier
--inner join operation o with (nolock)  on d.iddocument=o.iddocument
--inner join balance b with (nolock)  on b.idbalance=o.idbalance
--inner join contract c with (nolock) on b.idcontract=c.idcontract
--inner join person p with (nolock) on c.idperson=p.idperson
--where p.IsJuridical=0 and b.idaccounting<>6 and bb.IdTypeBatch=2 --Прием оплаты от потребителей через кассу
--group by bb.idbatch) res on res.idbatch=b.idbatch
	inner join agent a1 with (nolock)  on b.IDCashier  = a1.idagent
		and dbo.DateOnly(batchdate) = @DateB 
		and IDCashier = @IDCashier and b.idtypebatch=1
	inner join cashbalance cb with (nolock)  on b.idcashier = cb.idcashier 
		and dbo.DateOnly(datecash) = @DateB
left join agent a with (nolock)  on b.iddispatcher  = a.idagent


--От физических лиц за услуги
if isnull(@SummUslFL,0)>0 or @RSummUslFL>0
begin
	insert into @Info (ID,Nomer, Otkogo, Kassir, Summa, Rashod, Ostatok, NanNac, Schet, iddispatcher, idtypebatch)
	select 2,b.NumberBatch Nomer, 'От физических лиц за услуги',A1.Name Kassir,
	@SummUslFL,
@RSummUslFL,cb.amountbalance Ostatok, dbo.fLastAmountBalance(@IDCashier,@DateB) NanNac,
	'1212',
@AmountControler,
b.idtypebatch idtypebatch
	from Batch b  with (nolock)
		inner join agent a1 with (nolock)  on b.IDCashier  = a1.idagent
			and dbo.DateOnly(batchdate) = @DateB 
			and IDCashier = @IDCashier and b.idtypebatch=1
		inner join cashbalance cb with (nolock)  on b.idcashier = cb.idcashier 
			and dbo.DateOnly(datecash) = @DateB
	left join agent a with (nolock)  on b.iddispatcher  = a.idagent
end

--инкассация в кассу
insert into @Info (id,Nomer, Otkogo, Kassir, Summa, Rashod, Ostatok, NanNac, Schet, iddispatcher, idtypebatch)
select 55,b.NumberBatch Nomer, 
case when b.IdDispatcher is null then 
	case when b.IdTypeBatch>2 then 
		case when CHARINDEX('@', b.Name)>0 then substring(b.name, CHARINDEX('@', b.Name)+1, len(b.Name)-CHARINDEX('@', b.Name)) else '' end
	 else 'От физических лиц за газ' end else a.Name end Otkogo
,A1.Name Kassir,
case when b.IdTypeBatch not in (3,6) then 
case when b.IdTypeBatch=2 then res.amount--@SummGasFL
else BatchAmount end
else '' end Summa
,case when b.IdTypeBatch in (3,6) then
b.batchamount else
case when b.IdTypeBatch=2 then (-1)*convert(float, dbo.fGetSumOfAmountDocumentByIDBatch(b.idbatch,0))
else '' end  end Rashod
,cb.amountbalance Ostatok, @LastAmountBalance NanNac,--dbo.fLastAmountBalance(@IDCashier,@DateB) NanNac,
case when b.name = '' or  b.name = '0' then '1211'
else case when CHARINDEX('@', b.Name)>0 then substring(b.name, 1, CHARINDEX('@', b.Name)-1) else '1211' end end Schet
--,isnull(b.iddispatcher,0) iddispatcher,
,@AmountControler,
b.idtypebatch idtypebatch
from Batch b  with (nolock)
inner join (select bb.idbatch idbatch, 0 amount
from batch bb with (nolock)  
where bb.IdTypeBatch>2 and dbo.DateOnly(batchdate) = @DateB
		and IDCashier = @IDCashier
group by bb.idbatch) res on res.idbatch=b.idbatch
	inner join agent a1 with (nolock)  on b.IDCashier  = a1.idagent
		and dbo.DateOnly(batchdate) = @DateB 
		and IDCashier = @IDCashier
	inner join cashbalance cb with (nolock)  on b.idcashier = cb.idcashier 
		and dbo.DateOnly(datecash) = @DateB
left join agent a with (nolock)  on b.iddispatcher  = a.idagent

--от юрлиц за газ
insert into @Info (id,Nomer, Otkogo, Kassir,Summa, Rashod, Ostatok, NanNac, Schet, iddispatcher, idtypebatch)
select 3,d.documentnumber, (case when len(isnull(p.NumberDog,''))>0 then '' else 'б/д ' end)+ c.Account+', ИИН '+isnull(p.RNN,'')+', '+isnull(p.name,'')+', '+isnull(p.Surname,'')+', '+
s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat),
A1.Name Kassir,	sum(o.amountoperation) amountoperation ,0,cb.amountbalance Ostatok, @LastAmountBalance NanNac,
	'1211',
--isnull(bb.iddispatcher,0) iddispatcher,
@AmountControler
,bb.idtypebatch idtypebatch
from document d with (nolock) 
inner join batch bb with (nolock)  on bb.idbatch=d.idbatch
 and dbo.DateOnly(batchdate) = @DateB 
		and IDCashier = @IDCashier
inner join agent a1 with (nolock)  on bb.IDCashier  = a1.idagent
			and dbo.DateOnly(batchdate) = @DateB 
			and IDCashier = @IDCashier
		inner join cashbalance cb with (nolock)  on bb.idcashier = cb.idcashier 
			and dbo.DateOnly(datecash) = @DateB
	left join agent a2 with (nolock)  on bb.iddispatcher  = a2.idagent
inner join operation o with (nolock)  on d.iddocument=o.iddocument
inner join balance b with (nolock)  on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract
inner join person p with (nolock) on c.idperson=p.idperson
inner join address a (nolock) on a.idaddress=p.idaddress
inner join street s with (nolock)  on s.idstreet=a.idstreet
inner join house hs  with (nolock) on hs.idhouse=a.idhouse
where p.IsJuridical=1  and b.idaccounting<>6 --and len(isnull(p.NumberDog,''))>0
group by c.Account,d.documentnumber,p.RNN,p.Surname,p.name,s.name,hs.housenumber,hs.housenumberchar,a.flat,
A1.Name,cb.amountbalance,bb.iddispatcher,bb.idtypebatch,p.NumberDog


--от юрлиц за услуги
insert into @Info (id,Nomer, Otkogo, Kassir,Summa, Rashod, Ostatok, NanNac, Schet, iddispatcher, idtypebatch)
select 3,d.documentnumber, (case when len(isnull(p.NumberDog,''))>0 then '' else 'б/д ' end)+c.Account+', ИИН '+isnull(p.RNN,'')+', '+isnull(p.name,'')+', '+isnull(p.Surname,'')+', '+
s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat),
A1.Name Kassir,	sum(o.amountoperation) amountoperation,0,cb.amountbalance Ostatok, @LastAmountBalance NanNac,
	'1212',
@AmountControler,
bb.idtypebatch idtypebatch
from document d with (nolock) 
inner join batch bb with (nolock)  on bb.idbatch=d.idbatch
 and dbo.DateOnly(batchdate) = @DateB 
		and IDCashier = @IDCashier
inner join agent a1 with (nolock)  on bb.IDCashier  = a1.idagent
			and dbo.DateOnly(batchdate) = @DateB 
			and IDCashier = @IDCashier
		inner join cashbalance cb with (nolock)  on bb.idcashier = cb.idcashier 
			and dbo.DateOnly(datecash) = @DateB
	left join agent a2 with (nolock)  on bb.iddispatcher  = a2.idagent
inner join operation o with (nolock)  on d.iddocument=o.iddocument
inner join balance b with (nolock)  on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract
inner join person p with (nolock) on c.idperson=p.idperson
inner join address a (nolock) on a.idaddress=p.idaddress
inner join street s with (nolock)  on s.idstreet=a.idstreet
inner join house hs  with (nolock) on hs.idhouse=a.idhouse
where p.IsJuridical=1  and b.idaccounting=6 --and len(isnull(p.NumberDog,''))>0
group by c.Account, d.documentnumber,p.RNN,p.Surname,p.name,s.name,hs.housenumber,hs.housenumberchar,a.flat,
A1.Name,cb.amountbalance,bb.iddispatcher,bb.idtypebatch,p.NumberDog


select id, Nomer, Otkogo, Kassir, Summa, Rashod,
Ostatok, NanNac, Schet, iddispatcher, SummCard, 0 as SummCash, 0 as ttt  from @Info
order by id, Nomer desc














GO