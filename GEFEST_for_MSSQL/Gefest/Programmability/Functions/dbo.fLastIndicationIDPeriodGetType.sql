SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fLastIndicationIDPeriodGetType] (@IdGMeter int, @IdPeriod int)
RETURNS varchar(100)  AS  
BEGIN 
declare @Display varchar(100)
select top 1 @Display= t.name
from indication i  with (nolock)
inner join typeindication t  with (nolock) on t.idtypeindication=i.idtypeindication
inner join FactUse f  with (nolock) on f.IdIndication=i.IdIndication
	and i.idgmeter = @IdGMeter 
	and f.IdPeriod=@IdPeriod
--and DateDisplay>=@DateB and DateDisplay<=@DateE
--where  isnull(i.idtypeIndication,0)<>4
order by DateDisplay  desc,  i.idindication desc
return @Display
END

GO