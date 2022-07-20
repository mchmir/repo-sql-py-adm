SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fGetLastPayment] (@idcontract int)  
RETURNS datetime  AS  
BEGIN 
declare @get datetime
--возвращает дату последней оплаты
select @get=max(documentdate)
from document with (nolock)  
--idtypedocument=1 (тип документа "сбор оплаты за газ")
where idtypedocument=1 and idcontract=@idcontract

if @get is null
begin
	set @get='1900-01-01'
	return @get
end


return @get

END


GO