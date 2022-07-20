SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO



create FUNCTION [dbo].[fGetIDDocument] (@idcontract int,@idtypedocument int)  
RETURNS int  AS  
BEGIN 
declare @get int

set @get=(select top 1 iddocument
from document with (nolock)  
where idcontract=@idcontract and idtypedocument=@idtypedocument order by iddocument desc
)

return @get

END







GO