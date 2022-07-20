SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fLastIndicationIDPeriodGet] (@IdGMeter int, @IdPeriod int)
RETURNS float AS  
BEGIN 
declare @Display float
select top 1 @Display= i.Display
from indication i  with (nolock)
inner join FactUse f  with (nolock) on f.IdIndication=i.IdIndication
	and i.idgmeter = @IdGMeter 
	and f.IdPeriod=@IdPeriod
--and DateDisplay>=@DateB and DateDisplay<=@DateE
--where  isnull(i.idtypeIndication,0)<>4
order by DateDisplay  desc,  i.idindication desc
return @Display
END
GO