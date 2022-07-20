SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO



create FUNCTION [dbo].[fGetDocumentDate] (@idcontract int,@idtypedocument int)  
RETURNS datetime  AS  
BEGIN 
declare @get datetime

set @get=(select top 1 DocumentDate
from document with (nolock)  
where idcontract=@idcontract and idtypedocument=@idtypedocument order by iddocument desc
)

return @get

END







GO