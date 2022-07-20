SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO




CREATE PROCEDURE [dbo].[spRecalcCashBalance](@IDCashier as int, @Date as datetime) AS

declare @AmountCash as float
declare @PrevAmount as float

declare @IdBatch int

select @IdBatch=IdBatch
from Batch with (nolock)
where IDCashier=@IDCashier
	and BatchDate=@Date 
	and IdTypeBatch=2
	and idStatusBatch=1
	and idTypePay=1

if isnull(@IdBatch,0)>0
begin
	declare @BatchCount int
	declare @BatchAmount float

	select @BatchCount=count(iddocument) from document with (nolock) where idbatch=@IdBatch
	select @BatchAmount=sum(documentamount) from document with (nolock) where idbatch=@IdBatch

	select @BatchCount
	select @BatchAmount

	update batch
	set batchcount=@BatchCount,
		batchamount=@BatchAmount
	where idbatch=@IdBatch
end 
select top 1 @PrevAmount=AmountBalance
from CashBalance cb with (nolock)
where IdCashier=@IdCashier and DateCash<@Date
order by DateCash desc

select @AmountCash=isnull(sum(BatchAmount),0) --считаем сколько пришло ч/з кассу
from Batch with (nolock)
where IdCashier=@IdCashier 
and BatchDate=@Date and IdTypeBatch=2

select @AmountCash=@AmountCash+isnull(sum(BatchAmount),0) --от диспетчеров
from Batch with (nolock)
where IdCashier=@IdCashier 
and BatchDate=@Date and IdTypeBatch=1 and idTypePay=1

select @AmountCash=@AmountCash+isnull(sum(BatchAmount),0) --прочие платежи
from Batch with (nolock)
where IdCashier=@IdCashier and BatchDate=@Date and IdTypeBatch=5  and idTypePay=1

select @AmountCash=@AmountCash-isnull(sum(BatchAmount),0) --инкассация 
from Batch with (nolock)
where IdCashier=@IdCashier and BatchDate=@Date and IdTypeBatch=3 and idTypePay=1

select @AmountCash=@AmountCash-isnull(sum(BatchAmount),0) --выдача денег
from Batch with (nolock)
where IdCashier=@IdCashier and BatchDate=@Date and IdTypeBatch=6 and idTypePay=1

update CashBalance
set AmountBalance=@PrevAmount+@AmountCash
where IdCashier=@IdCashier and DateCash=@Date
GO