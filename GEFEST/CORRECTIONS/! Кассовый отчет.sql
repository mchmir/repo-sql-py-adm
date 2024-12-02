
-- report repCashRepGG

declare @IDCashier int
declare @DateB datetime


set @IDCASHIER = 151
set @DateB = dbo.DateOnly('2024-11-04')
/*
+---------+-----------+
|IDTypePay|Name       |
+---------+-----------+
|1        |Наличные   |
|2        |Безналичные|
+---------+-----------+
*/

-- от физических лиц, наличными
select
  d.IDDOCUMENT,
  d.DOCUMENTDATE,
  bb.IDBATCH,
  bb.IDTYPEPAY,
  o.IDOPERATION,
  o.AMOUNTOPERATION,
  b.IDACCOUNTING
-- sum(amountoperation)
from document d with (nolock)
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and batchdate = @DateB
  and IDCashier = @IDCashier
  and IDDispatcher is null
inner join operation o with (nolock)  on d.iddocument=o.iddocument
inner join balance b with (nolock)  on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract
inner join person p with (nolock) on c.idperson=p.idperson
where p.IsJuridical=0 and b.idaccounting<>6 and amountoperation>=0


---
select * from BATCH where IDBATCH = 98080; -- 24 300
select * from DOCUMENT where IDBATCH = 98080;

select * from BATCH where IDBATCH = 98081; -- 97 978
select * from DOCUMENT where IDBATCH = 98081;

select * from BATCH where BATCHDATE='2024-11-04'
select * from BATCH where IDBATCH = 98085; -- 97 978
select * from DOCUMENT where IDBATCH = 98085;



-- От физических лиц за газ картой.
declare @IDCashier int
declare @DateB datetime


set @IDCASHIER = 151
set @DateB = dbo.DateOnly('2024-11-04')

select
  d.IDDOCUMENT,
  d.DOCUMENTDATE,
  bb.IDBATCH,
  bb.IDTYPEPAY,
  o.IDOPERATION,
  o.AMOUNTOPERATION,
  b.IDACCOUNTING
-- sum(amountoperation)
from document d with (nolock)
inner join batch bb with (nolock)  on bb.idbatch=d.idbatch
and batchdate = @DateB
  and IDCashier = @IDCashier and IDDispatcher=113 -- sberbank
inner join operation o with (nolock)  on d.iddocument=o.iddocument
inner join balance b with (nolock)  on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract
inner join person p with (nolock) on c.idperson=p.idperson
where p.IsJuridical=0 and b.idaccounting<>6 and amountoperation>=0


--- От физических лиц за услуги наличными
declare @IDCashier int
declare @DateB datetime


set @IDCASHIER = 151
set @DateB = dbo.DateOnly('2024-11-04')

select
  d.IDDOCUMENT,
  d.DOCUMENTDATE,
  bb.IDBATCH,
  bb.IDTYPEPAY,
  o.IDOPERATION,
  o.AMOUNTOPERATION,
  b.IDACCOUNTING
-- sum(amountoperation)
from document d with (nolock)
inner join batch bb with (nolock)  on bb.idbatch=d.idbatch
and batchdate = @DateB
  and IDCashier = @IDCashier and (IDDispatcher is null)
inner join operation o with (nolock)  on d.iddocument=o.iddocument
inner join balance b with (nolock)  on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract
inner join person p with (nolock) on c.idperson=p.idperson
where p.IsJuridical=0 and b.idaccounting=6


--- От физических лиц за услуги картой
declare @IDCashier int
declare @DateB datetime


set @IDCASHIER = 151
set @DateB = dbo.DateOnly('2024-11-04')

select
  d.IDDOCUMENT,
  d.DOCUMENTDATE,
  bb.IDBATCH,
  bb.IDTYPEPAY,
  o.IDOPERATION,
  o.AMOUNTOPERATION,
  b.IDACCOUNTING
-- sum(amountoperation)
from document d with (nolock)
inner join batch bb with (nolock)  on bb.idbatch=d.idbatch
and batchdate = @DateB
		and IDCashier = @IDCashier and IDDispatcher=113-- sberbank
inner join operation o with (nolock)  on d.iddocument=o.iddocument
inner join balance b with (nolock)  on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract
inner join person p with (nolock) on c.idperson=p.idperson
where p.IsJuridical=0 and b.idaccounting=6


