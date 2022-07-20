SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fLastIndicationDateType](@IdGMeter int, @Date datetime)
RETURNS varchar (100) AS  
BEGIN 
declare @Display varchar (100)
select top 1 @Display=t.name
from indication i with (nolock) 
inner join typeindication t with (nolock) on i.idtypeindication=t.idtypeindication
where i.idgmeter = @IdGMeter and convert(datetime,round(convert(float,i.DateDisplay),0, 1),20)<=convert(datetime,round(convert(float,@Date),0, 1),20)--convert(datetime, @Date, 20) --and  isnull(i.idtypeIndication,0)<>4 
order by i.DateDisplay desc , i.idtypeIndication desc
return @Display
END


GO