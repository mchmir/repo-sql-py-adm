SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO



create FUNCTION [dbo].[fGetChangesForObject](@idsending int,@tablename varchar(50),@idobject int)
RETURNS varchar(8000) AS  
BEGIN 
declare @ret varchar(8000)
set @ret=''
select @ret=@ret+str(idtypeaction)+','
from changes
where idsending=@idsending
and tablename=@tablename and idobject=@idobject

return @ret
END



GO