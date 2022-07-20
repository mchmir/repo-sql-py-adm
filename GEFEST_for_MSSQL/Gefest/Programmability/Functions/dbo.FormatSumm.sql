SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

/*************************************************************************/
/* форматирование числовой суммы в "русский" вид */
/*************************************************************************/
CREATE function [dbo].[FormatSumm] (@Value DECIMAL(15,2))
returns char (20)
as
begin
declare @ValueReturn char (20)
set @ValueReturn = Replace(Replace(Replace(CONVERT(char(20),cast(@Value as money),1),' ',''),',',' '),'.',',')
return @ValueReturn
end
GO