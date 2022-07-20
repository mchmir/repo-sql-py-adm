SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO










CREATE FUNCTION [dbo].[fGetWork] (@date datetime,@idPerson int, @WorkPlace varchar(20))  
RETURNS varchar(8000)  AS  
BEGIN 
declare @get varchar(8000)

set @get=(select top 1 name
from oldvalues
where nametable='person' and 
namecolumn=@WorkPlace
and idobject=@idPerson
and dbo.dateonly(datevalues)>=dbo.dateonly(@date)
order by datevalues asc,idoldvalues asc)

if @get is null
begin
if @WorkPlace='WorkPlace'
set @get=(select isnull(WorkPlace,'') from person where idperson=@idperson)
end	
if @WorkPlace='Iduser'
set @get=(select isnull(iduser,'') from person where idperson=@idperson)
return @get


END














GO