SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO





CREATE FUNCTION [dbo].[fGetSumOfAmountDocumentByIDBatch] (@IdBatch int, @IsPositive int)  
RETURNS varchar(20)  AS  
BEGIN 
--если @IsPositive=1, то считаем сумму для всех документов, которые с "плюсом"
--если @IsPositive=0, то считаем сумму для всех документов, которые с "минусом" (случай, когда деньги возвращают абоненту)
declare @get decimal(20,2)--float

if @IsPositive=1
begin

select @get=sum(documentamount)
from document
where idbatch=@idbatch
and documentamount>0

end

else

begin

select @get=sum(documentamount)
from document
where idbatch=@idbatch
and documentamount<0

end
--в отчете, если нужно выводить "пусто", а не 0
if @get is null
	return ''
return @get
END



GO