SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO



CREATE FUNCTION [dbo].[fIndicationDate](@IdGMeter int)
--------------*************Функция поиска показаний по П****************----------------
RETURNS datetime AS  
BEGIN 
declare @Display datetime
select top 1 @Display= i.DateDisplay
from indication i with (nolock) 
where i.idgmeter = @IdGMeter --and convert(datetime,round(convert(float,i.DateDisplay),0, 1),20)<=convert(datetime,round(convert(float,@Date),0, 1),20)--convert(datetime, @Date, 20) --and  isnull(i.idtypeIndication,0)<>4 
order by i.DateDisplay desc 
return @Display
END








GO