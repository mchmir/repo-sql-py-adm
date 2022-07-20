SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fGetLastClaimDoc] (@idcontract int)  
RETURNS int  AS  
BEGIN 
declare @get int
--возвращает ID док-та претензии (10)
select @get=max(iddocument)
from document with (nolock)  
where idtypedocument=10 and idcontract=@idcontract

if @get is null
begin
	set @get=0
end
return @get

END

GO