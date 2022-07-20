SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
CREATE FUNCTION [dbo].[fGetLastIndication](@IdContract int)
RETURNS DATETIME AS  

BEGIN 
declare @get DATETIME;
--возвращает дату последних показаний
SELECT @get = (SELECT TOP 1
 Indication.DateDisplay
FROM dbo.GMeter
INNER JOIN dbo.Indication
  ON GMeter.IDGMeter = Indication.IdGMeter
INNER JOIN dbo.GObject
  ON GMeter.IDGObject = GObject.IDGObject
INNER JOIN dbo.Contract
  ON GObject.IDContract = Contract.IDContract
WHERE Contract.IDContract = @IdContract
ORDER BY DateAdd DESC);

if @get is null
begin
	set @get='1900-01-01'
	return @get
END;

return @get;

END;


GO