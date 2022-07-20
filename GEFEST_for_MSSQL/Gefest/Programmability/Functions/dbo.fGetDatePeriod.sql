SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fGetDatePeriod](@IDPeriod int, @BeginEnd int)
-----------**********Возвращает максимальную либо минимальную дату периода***********----------------------
RETURNS datetime
AS  
BEGIN
declare @Date datetime 
if @BeginEnd=1
set @Date=(select DateBegin from period  with (nolock) where idperiod=@IDPeriod)
else
set @Date=(select DateEnd from period  with (nolock) where idperiod=@IDPeriod)
return (@Date)
END
GO