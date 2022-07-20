SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
CREATE FUNCTION [dbo].[fGetIDPeriod](@DateTime DATETIME)
RETURNS INT
AS  
BEGIN
declare @IDPeriod int
SET @IDPeriod = (SELECT idPeriod FROM Period p WHERE p.Year = YEAR(@DateTime) AND p.Month = MONTH(@DateTime)) 
return (@IDPeriod)
END
GO