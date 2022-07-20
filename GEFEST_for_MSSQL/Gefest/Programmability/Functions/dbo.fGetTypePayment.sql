SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fGetTypePayment] (@iddocument int)  
RETURNS int  AS  
BEGIN 
declare @get int
--выясняет был платеж (idtypedocument=1) принят через кассира или агента (например, трансп. файл)
--@get:
--0-касса
--1-агент

select @get=b.idcashier
from document d with (nolock)  
inner join batch b with (nolock)  on d.idbatch=b.idbatch
where iddocument=@iddocument

if @get is null
begin
	--set @get=1
	--return @get
	return 1	
end

--return @get
return 0

END

GO