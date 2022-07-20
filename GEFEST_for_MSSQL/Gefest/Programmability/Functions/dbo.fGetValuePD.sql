SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO


CREATE FUNCTION [dbo].[fGetValuePD] (@iddocument int,@idtypepd int)  
RETURNS varchar(100)  AS  
BEGIN 
declare @get varchar(2000)

set @get=(select value
from pd with (nolock)  
where iddocument=@iddocument and idtypepd=@idtypepd
)

return @get

END






GO