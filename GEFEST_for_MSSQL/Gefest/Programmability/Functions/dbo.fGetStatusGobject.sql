SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO



create FUNCTION [dbo].[fGetStatusGobject] (@Idgobject int, @IdPeriod int)  
RETURNS int AS  
BEGIN 
declare @ret int

select @ret=idstatusgobject
from gobject  with (nolock)  
where Idgobject=@Idgobject

select @ret=Name 
from oldvalues  with (nolock)  
where idperiod=(select min(IdPeriod) idp
from OldValues  with (nolock)  
where IdPeriod>@idperiod 
	and idobject=@Idgobject 
	and nametable='GObject' 
	and namecolumn='IDStatusGObject')
and idobject=@Idgobject 
	and nametable='GObject' 
	and namecolumn='IDStatusGObject'

return @ret
END



GO