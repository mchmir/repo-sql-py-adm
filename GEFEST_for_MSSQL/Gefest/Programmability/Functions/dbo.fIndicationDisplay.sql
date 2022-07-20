SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO


create FUNCTION [dbo].[fIndicationDisplay](@IdGMeter int)
--------------*************Функция поиска показаний по П****************----------------
RETURNS float AS  
BEGIN 
declare @Display float
select top 1 @Display= Display
from indication with (nolock) 
where idgmeter = @IdGMeter 
order by DateDisplay desc 
return @Display
END







GO