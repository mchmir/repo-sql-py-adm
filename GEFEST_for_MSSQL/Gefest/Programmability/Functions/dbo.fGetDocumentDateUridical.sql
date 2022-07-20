SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO



create FUNCTION [dbo].[fGetDocumentDateUridical] (@idcontract int)  
RETURNS datetime  AS  
BEGIN 
declare @get datetime

set @get=(select top 1 d.documentdate
from document d with (nolock) 
inner join pd on pd.iddocument=d.iddocument
and pd.idtypepd=22 and pd.value<>2 and d.idtypedocument=13 
and idcontract=@idcontract order by d.iddocument desc)

return @get

END







GO