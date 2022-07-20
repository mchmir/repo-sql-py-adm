SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO















--Процедура по начислению за кредитные услуги
CREATE PROCEDURE [dbo].[spChargeCreditUsl](@IdDocument as int) 
AS
declare @idperiod int
declare @dated datetime
declare @year int
declare @month int
declare @amount float
declare @idcontract int

declare @IdBalance int
declare @Idoperation int
--declare @iddocument int

--логируем
--insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
--values ('spChargeCreditUsl','Начали',GetDate())
--
set @idperiod=dbo.fGetNowPeriod()
--set @idperiod=@idperiod
set @dated=(select DocumentDate from document where iddocument=@IdDocument)
set @idcontract=(select idcontract from document where iddocument=@IdDocument)
set @amount=(select DocumentAmount from document where iddocument=@IdDocument)

insert document (IDContract, IDPeriod,  IDTypeDocument, DocumentNumber, DocumentDate, DocumentAmount,Note)
values (@idcontract, @IdPeriod,  24, 'Кредит', @dated, @amount,'за кредитные услуги по графику')
set @iddocument=scope_identity()
---параметры дока
insert into PD (IDTypePD,IDDocument,Value)
values (16,@iddocument,137) --гтмэ

insert into PD (IDTypePD,IDDocument,Value)
values (35,@iddocument,21) --кредитные услуги
---
		
select @IdBalance=0
--поиск подходящего баланса
select @IdBalance=isnull(idbalance, 0)
from balance 
where idcontract=@idcontract and idperiod=@idperiod and idaccounting=6

if @IdBalance=0
	begin
		insert Balance (IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
		values (6, @IdPeriod, @IdContract, @amount, 0, 0)
		set @IdBalance=scope_identity()
	end
else
	begin
		update balance
		set AmountBalance=AmountBalance-@amount
		where IDBalance=@IdBalance
	end

insert operation(DateOperation, AmountOperation,NumberOperation,IDBalance, IDDocument, IdTypeOperation)
values (@dated, @amount*-1,6,  @IdBalance, @iddocument, 2)

exec dbo.spRecalcBalances @IDContract, @IDPeriod
exec dbo.spRecalcBalancesRealOnePeriodByContract @IDContract, @IDPeriod


--логируем
--insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
--values ('spChargeCreditUsl','Закончили',GetDate())
--
--exec dbo.spRecalcBalancesOnePeriod @idperiod
--exec dbo.spRecalcBalancesRealOnePeriod @idperiod























GO