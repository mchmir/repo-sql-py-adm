SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fGetPredPeriodVariable](@IDPeriod int)
RETURNS int AS  
BEGIN
return  (select top 1 IDPeriod
from Period  with (nolock)  
where idperiod<@IDPeriod  
order by IDPeriod desc)
END



GO