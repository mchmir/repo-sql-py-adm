SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fLastIndicationIDPeriodGetDate] (@IdGMeter int, @IdPeriod int)
RETURNS datetime  AS  
BEGIN 
declare @Display datetime
select top 1 @Display= i.dateDisplay
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