SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
CREATE FUNCTION [dbo].[fGetLastOffOU](@IdContract int)
RETURNS VARCHAR(50) AS  

BEGIN 
declare @get DATETIME;
DECLARE @result VARCHAR(50)

--возвращает дату последних отключений
SELECT @get = (SELECT TOP 1  d.DocumentDate 
               FROM  Document d WHERE d.IDContract = @IdContract AND d.IDTypeDocument = 17 
               ORDER BY d.DocumentDate DESC);

if @get is null
  begin
	  set @result='Отключений OU не было или было 5 лет назад. См. архив.'
  END;
ELSE
  BEGIN
    SET @result = dbo.DateWithoutTime(@get)  
  END;


return @result;

END;


GO