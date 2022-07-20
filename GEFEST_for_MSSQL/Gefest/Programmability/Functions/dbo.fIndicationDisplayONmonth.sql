SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
CREATE FUNCTION [dbo].[fIndicationDisplayONmonth](@IdGMeter INT, @vMonth INT, @vYear INT )
--------------*************Функция поиска показаний по П****************----------------
RETURNS FLOAT AS  
BEGIN 
declare @Display FLOAT
select top 1 @Display= i.Display
from indication i with (nolock) 
where i.idgmeter = @IdGMeter AND MONTH(i.DateDisplay)=@vMonth AND YEAR(i.DateDisplay)=@vYear
order by i.DateDisplay desc 
return @Display
END



GO