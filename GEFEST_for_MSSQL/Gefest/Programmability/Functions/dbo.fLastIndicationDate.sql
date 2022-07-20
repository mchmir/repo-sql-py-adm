SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fLastIndicationDate](@IdGMeter int, @Date datetime)
RETURNS datetime AS  
BEGIN 
declare @Display datetime
select top 1 @Display= i.DateDisplay
from indication i with (nolock) 
where i.idgmeter = @IdGMeter and convert(datetime,round(convert(float,i.DateDisplay),0, 1),20)<=convert(datetime,round(convert(float,@Date),0, 1),20)--convert(datetime, @Date, 20) --and  isnull(i.idtypeIndication,0)<>4 
order by i.DateDisplay desc , i.idtypeIndication desc
return @Display
END




GO