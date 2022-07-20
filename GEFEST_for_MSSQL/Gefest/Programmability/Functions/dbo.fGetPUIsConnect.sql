SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fGetPUIsConnect](@IdContract int, @IdPeriod int)  
RETURNS int AS  
BEGIN 
declare @IsConnect int

set @IsConnect=2

select top 1 @IsConnect=DocumentAmount
from Document with (nolock)  
where IdTypeDocument=12
	and IdPeriod<=@IdPeriod
	and IdContract=@IdContract
order by IdDocument desc

return @IsConnect
END
GO