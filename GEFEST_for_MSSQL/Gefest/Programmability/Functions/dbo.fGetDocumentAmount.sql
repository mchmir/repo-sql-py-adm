SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
--- function create 27.08.2020

CREATE  FUNCTION [dbo].[fGetDocumentAmount] (@idcontract INT, @idtypedocument INT, @idperiod INT)  
RETURNS float  AS  
BEGIN 
declare @get float

set @get=(select SUM(DocumentAmount) from document with (nolock)  
where idcontract=@idcontract and idtypedocument=@idtypedocument AND IDPeriod = @idperiod
  --order by iddocument desc
)

return @get

END







GO