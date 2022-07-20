SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fGetCountLives] (@IdGObject int, @IdPeriod int)  
RETURNS int AS  
BEGIN 
declare @ret int

select @ret=countlives
from GObject  with (nolock)  
where idgobject=@idgobject

select top 1 @ret=Name 
from oldvalues  with (nolock)  
where idperiod=(select min(IdPeriod) idp
from OldValues  with (nolock)  
where IdPeriod>@idperiod 
	and idobject=@idgobject 
	and nametable='GObject' 
	and namecolumn='CountLives')
and idobject=@idgobject 
	and nametable='GObject' 
	and namecolumn='CountLives'
order by idoldvalues 

return @ret
END

GO