SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fGetLastPCValue] (@IdPeriod int, @IdTypePC int, @IdContract int)  
----------------------********Возвращает последние значение по типу и контракту************---------------------
RETURNS varchar(50)  AS  
BEGIN 
declare @ret varchar(50)
select top 1 @ret=PC.Value
from PC  with (nolock) 
where IDPeriod <= @IdPeriod 
	and IDTypePC = @IdTypePC 
	and IdContract= @IdContract
order by IdPeriod desc
return @ret
END
GO