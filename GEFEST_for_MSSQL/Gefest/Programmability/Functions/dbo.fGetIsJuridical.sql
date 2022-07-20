SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO


CREATE FUNCTION [dbo].[fGetIsJuridical] (@IdPerson int, @IdPeriod int)  
RETURNS int AS  
BEGIN 
declare @ret int

select @ret=isJuridical
from person  with (nolock)  
where IdPerson=@IdPerson

select @ret=Name 
from oldvalues  with (nolock)  
where idperiod=(select min(IdPeriod) idp
from OldValues  with (nolock)  
where IdPeriod>@idperiod 
	and idobject=@IdPerson 
	and nametable='Person' 
	and namecolumn='isJuridical')
and idobject=@IdPerson 
	and nametable='Person' 
	and namecolumn='isJuridical'

return @ret
END


GO