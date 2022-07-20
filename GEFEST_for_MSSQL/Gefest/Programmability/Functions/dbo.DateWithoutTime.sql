SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
---function created 26.08.2020
CREATE FUNCTION [dbo].[DateWithoutTime]  (@Date as DateTime)  
RETURNS VARCHAR(10) AS  
BEGIN
  DECLARE @day VARCHAR(2)
  DECLARE @month VARCHAR(2)
  DECLARE @year VARCHAR(4)
  DECLARE @result VARCHAR(10)

 IF @Date IS NOT NULL  
 BEGIN
      IF LEN(CAST(DAY(@Date) AS VARCHAR))=2 
        SET @day = CAST(DAY(@Date) AS VARCHAR)
     ELSE 
        SET @day = '0'+CAST(DAY(@Date) AS VARCHAR)
      
     IF LEN(CAST(MONTH(@Date) AS VARCHAR))=2 
        SET @month = CAST(MONTH(@Date) AS VARCHAR)
     ELSE 
        SET @month = '0'+CAST(MONTH(@Date) AS VARCHAR) 
      
     SET @year = CAST(YEAR(@Date) AS VARCHAR)    
    
     SET @result =  @day+'.'+@month+'.'+@year
END 
ELSE
      SET @result = 'даты нет'

   RETURN @result

END
GO