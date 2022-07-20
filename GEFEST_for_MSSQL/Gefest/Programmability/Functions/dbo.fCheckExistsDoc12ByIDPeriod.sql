SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fCheckExistsDoc12ByIDPeriod](@IDcontract int, @idperiod int)
RETURNS int AS  
BEGIN 
declare @ret as int

set @ret=(select count(*)
from document
where idtypedocument=12
and idcontract=@idcontract
and idperiod<@idperiod)

return @ret
END

GO