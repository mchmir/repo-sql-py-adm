SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO







CREATE Procedure [dbo].[spRealizationsOfOperations] as 
begin
declare @IdPeriod int
set @IdPeriod = dbo.fGetNowPeriod()

declare @IDDocument as int
declare @IDContract as int
declare @DocumentAmount as float
declare @DocumentDate as datetime
declare @IdTypeDocument int
declare @IdBatch int
declare @display float
declare @iduser int
declare @idindication int
declare @note varchar(8000)
declare @factamount float
declare @err  as  bit
declare @AmountVDGO as float
--логируем
insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
values ('spRealizationsOfOperations','Начали',GetDate())
--
declare Loc cursor for 
select d.iddocument, d.idcontract , d.documentamount, d.documentdate, d.IdTypeDocument, d.IdBatch,
	i.display, d.iduser, i.idindication, d.note, f.factamount
from document d 
left join PD on PD.iddocument=d.iddocument
	and PD.idtypePD=1
left join indication i on i.idindication=convert(int, PD.Value)
left join factuse f on f.idindication=i.idindication
where d.iddocument in (
select d1.iddocument
from Document d1 
	left join operation o on d1.iddocument = o.iddocument
	inner join contract c on d1.idcontract = c.idcontract
	inner join batch b on d1.idbatch = b.idbatch
where d1.idperiod = @IdPeriod and d1.idbatch is not null and o.idoperation is null and d1.IdTypeDocument = 1 
)

open Loc

FETCH NEXT FROM Loc INTO @IDDocument, @IDContract, @DocumentAmount, @DocumentDate, @IdTypeDocument, @IdBatch,
		@display, @iduser, @idindication, @note, @factamount
WHILE @@FETCH_STATUS=0
 BEGIN
	set @err=0
		exec DocumentPay @IDPeriod, @IDBatch, @IDContract, @IDTypeDocument, @DocumentDate, @DocumentAmount, @Display, 0, @IDUser, @IDDocument, @IDIndication, @FactAmount, '', @note, @err, @AmountVDGO
	if @err<>0 
		select 'error'+str(@err)

	FETCH NEXT FROM Loc INTO @IDDocument, @IDContract, @DocumentAmount, @DocumentDate, @IdTypeDocument, @IdBatch,
		@display, @iduser, @idindication, @note, @factamount

end
CLOSE Loc
DEALLOCATE Loc
--логируем
insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
values ('spRealizationsOfOperations','Закончили',GetDate())
--
end


GO