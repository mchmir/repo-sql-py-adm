SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fGetPredPeriod]()
RETURNS int AS  
BEGIN
declare @IDPeriod int
set @IDPeriod = dbo.fGetNowPeriod()
return  (select top 1 IDPeriod
from Period  with (nolock)  
where idperiod<@IDPeriod  
order by IDPeriod desc)
END


GO