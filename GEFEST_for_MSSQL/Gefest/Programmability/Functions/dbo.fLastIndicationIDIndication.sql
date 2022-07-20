SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

create FUNCTION [dbo].[fLastIndicationIDIndication](@IdGMeter int, @Date datetime)
--------------*************Функция поиска показаний по П****************----------------
RETURNS int AS  
BEGIN 
declare @Display int
select top 1 @Display= idindication
from indication with (nolock) 
where idgmeter = @IdGMeter and convert(datetime,round(convert(float,DateDisplay),0, 1),20)<=convert(datetime,round(convert(float,@Date),0, 1),20)--convert(datetime, @Date, 20)-- and  isnull(idtypeIndication,0)<>4 
order by DateDisplay desc , idtypeIndication desc
return @Display
END

GO