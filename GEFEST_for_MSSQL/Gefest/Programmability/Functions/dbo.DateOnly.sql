SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[DateOnly]  (@Date as DateTime)  
RETURNS DateTime AS  
BEGIN 
return round(convert(float,@Date),0, 1)
--return convert(datetime, left(convert(varchar, @Date, 20),10))
END
GO