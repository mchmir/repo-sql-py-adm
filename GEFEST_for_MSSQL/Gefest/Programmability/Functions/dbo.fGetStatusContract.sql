SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO




create FUNCTION [dbo].[fGetStatusContract] (@Idcontract int, @IdPeriod int)  
RETURNS int AS  
BEGIN 
declare @ret int

select @ret=status
from contract  with (nolock)  
where Idcontract=@Idcontract

select @ret=Name 
from oldvalues  with (nolock)  
where idperiod=(select min(IdPeriod) idp
from OldValues  with (nolock)  
where IdPeriod>@idperiod 
	and idobject=@Idcontract
	and nametable='Contract' 
	and namecolumn='Status')
and idobject=@Idcontract 
	and nametable='Contract' 
	and namecolumn='Status'

return @ret
END




GO