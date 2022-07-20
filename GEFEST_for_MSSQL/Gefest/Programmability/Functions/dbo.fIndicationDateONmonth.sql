SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
CREATE FUNCTION [dbo].[fIndicationDateONmonth](@IdGMeter INT, @vMonth INT, @vYear INT )
--------------*************Функция поиска показаний по П****************----------------
RETURNS datetime AS  
BEGIN 
declare @Display datetime
select top 1 @Display= i.DateDisplay
from indication i with (nolock) 
where i.idgmeter = @IdGMeter AND MONTH(i.DateDisplay)=@vMonth AND YEAR(i.DateDisplay)=@vYear
order by i.DateDisplay desc 
return @Display
END



GO