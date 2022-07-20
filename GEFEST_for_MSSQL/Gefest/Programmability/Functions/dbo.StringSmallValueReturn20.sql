SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[StringSmallValueReturn20] (@Value as char(2))  
RETURNS varchar(15) AS  
BEGIN 
declare @strValue as varchar(15)
if @Value = '1'
	set @strValue = 'один'
else if @Value = '2'
	set @strValue = 'два'
else if @Value = '3'
	set @strValue = 'три'
else if @Value = '4'
	set @strValue = 'четыре'
else if @Value = '5'
	set @strValue = 'пять'
else if @Value = '6'
	set @strValue = 'шесть'
else if @Value = '7'
	set @strValue = 'семь'
else if @Value = '8'
	set @strValue = 'восемь'
else if @Value = '9'
	set @strValue = 'девять'
else if @Value = '10'
	set @strValue = 'десять'
else if @Value = '11'
	set @strValue = 'одинадцать'
else if @Value = '12'
	set @strValue = 'двенадцать'
else if @Value = '13'
	set @strValue = 'тринадцать'
else if @Value = '14'
	set @strValue = 'четырнадцать'
else if @Value = '15'
	set @strValue = 'пятнадцать'
else if @Value = '16'
	set @strValue = 'шестнадцать'
else if @Value = '17'
	set @strValue = 'семнадцать'
else if @Value = '18'
	set @strValue = 'восемнадцать'
else if @Value = '19'
	set @strValue = 'девятнадцать'
else if @Value = '20'
	set @strValue = 'двадцать'
return @strValue
END
GO