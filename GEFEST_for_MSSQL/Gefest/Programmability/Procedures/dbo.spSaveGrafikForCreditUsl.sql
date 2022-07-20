SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO





-------------****************Проведение юридического документа********************-------------------------------------
CREATE PROCEDURE [dbo].[spSaveGrafikForCreditUsl] (
	@IdDocument as int,
	@blErr as bit output
)
AS
declare @IDPeriod as float
declare @IDContract as int
declare @AmountPay as int
declare @CountMonth as int
declare @i as int

select @CountMonth = pd.value from pd where pd.iddocument=@IdDocument and pd.idtypepd=1
select @AmountPay = DocumentAmount/@CountMonth from document where iddocument=@IdDocument
select @IDContract = IDContract from document where iddocument=@IdDocument
select @IDPeriod = IDPeriod from document where iddocument=@IdDocument
set @i = 1
set @blErr = 0

begin transaction

while @i <= @CountMonth
begin
	-- 
	set @IDPeriod = @IDPeriod + 1
	insert into dbo.GrafikForCreditUsl (IDPeriod, IDContract, IdDocument, AmountPay)
	values (@IDPeriod, @IDContract, @IdDocument, @AmountPay)
	if @@Error <> 0 set @blErr = 1
	--select @blErr
	--
	set @i = @i + 1
end

exec spChargeCreditUsl @IdDocument


if @blErr = 0
	commit transaction
else 
	rollback transaction






GO