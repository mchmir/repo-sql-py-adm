SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO


create FUNCTION [dbo].[fGetPeriodDate](@date datetime)
-----------**********Возвращает период  по дате***********----------------------
RETURNS int
AS  
BEGIN
declare @r int
set @r=(select idperiod from period  with (nolock) where Year=year(@date) and month=month(@date))
return @r
END


GO