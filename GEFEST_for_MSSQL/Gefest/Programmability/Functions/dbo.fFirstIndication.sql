SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fFirstIndication](@IdGMeter int, @Date datetime)
RETURNS float AS  
BEGIN 
declare @Display float
select top 1 @Display=  Display
from indication  with (nolock)
where idgmeter = @IdGMeter and convert(datetime,round(convert(float,DateDisplay),0, 1),20)>=convert(datetime,round(convert(float,@Date),0, 1),20)--convert(datetime,  @Date , 20) --and  isnull(idtypeIndication,0)<>4
order by DateDisplay 
return @Display
END




GO