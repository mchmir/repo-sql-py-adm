SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO












--Процедура по начислению за услуги
CREATE PROCEDURE [dbo].[spChargeVDGO] AS
declare @idperiod int
declare @dated datetime
declare @year int
declare @month int
declare @amount float
declare @idcontract int

--логируем
insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
values ('spChargeVDGO','Начали',GetDate())
--
set @idperiod=dbo.fGetNowPeriod()
--set @idperiod=@idperiod
set @dated=dbo.fGetDatePeriod(@idperiod, 2)
select @year=[year] from period where idperiod=@idperiod
select @month=[month] from period where idperiod=@idperiod

DECLARE curGraf CURSOR
READ_ONLY
FOR select distinct GTO.idcontract, GTO.amount 
from GrafikTO GTO 
left join (select doc.idcontract from document doc 
inner join pd pd1 on pd1.iddocument=doc.iddocument
	and pd1.idtypepd=16 and pd1.value=90
inner join pd pd2 on pd2.iddocument=doc.iddocument
	and pd2.idtypepd=35 and pd2.value=11
where doc.idtypedocument=24 and doc.idperiod=(select p.idperiod from period p where p.[year]=@year and p.[month]=@month)) 
doc on doc.idcontract=gto.idcontract
where GTO.[year]=@year and GTO.[month]=@month and doc.idcontract is null

--DECLARE @idcontract int
--declare @Forpeny float
declare @IdBalance int
declare @Idoperation int
declare @iddocument int

set @idcontract=0
set @amount=0

OPEN curGraf

FETCH NEXT FROM curGraf INTO @idcontract, @amount
WHILE (@@fetch_status <> -1)
BEGIN
	IF (@@fetch_status <> -2)
	BEGIN
--		select @idcontract
		insert document (IDContract, IDPeriod,  IDTypeDocument, DocumentNumber, DocumentDate, DocumentAmount,Note)
		values (@idcontract, @IdPeriod,  24, 'ТО', @dated, @amount,'за ТО по графику')
		set @iddocument=scope_identity()
		---параметры дока
		insert into PD (IDTypePD,IDDocument,Value)
		values (16,@iddocument,90) --вдго

		insert into PD (IDTypePD,IDDocument,Value)
		values (35,@iddocument,11) --ТО ГО
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

	END
	FETCH NEXT FROM curGraf INTO @idcontract, @amount
END

CLOSE curGraf
DEALLOCATE curGraf
--логируем
insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
values ('spChargeVDGO','Закончили',GetDate())
--
--exec dbo.spRecalcBalancesOnePeriod @idperiod
--exec dbo.spRecalcBalancesRealOnePeriod @idperiod




















GO