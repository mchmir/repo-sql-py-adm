SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fLastIndicationYarMonthGet] (@IdGMeter int, @date datetime)
RETURNS float AS  
BEGIN 
declare @Display float
select top 1 @Display= i.Display
from indication i  with (nolock)
	where i.idgmeter = @IdGMeter 
	and month(DateDisplay)=month(@date) and year(DateDisplay)=year(@date)
order by DateDisplay  desc,  i.idindication desc
return @Display
END

GO