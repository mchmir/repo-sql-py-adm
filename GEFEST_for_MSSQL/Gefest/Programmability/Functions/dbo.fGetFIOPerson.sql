SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO









CREATE FUNCTION [dbo].[fGetFIOPerson] (@date datetime,@idPerson int, @FIO varchar(20))  
RETURNS varchar(8000)  AS  
BEGIN 
declare @get varchar(8000)

set @get=(select top 1 name
from oldvalues
where nametable='Person' and 
namecolumn=@FIO
and idobject=@idPerson
and dbo.dateonly(datevalues)>=dbo.dateonly(@date)
order by datevalues asc,idoldvalues asc)

if @get is null
begin
if @FIO='Surname'
set @get=(select isnull(Surname,'') from person where idperson=@idperson)
if @FIO='Name'
set @get=(select isnull(Name,'') from person where idperson=@idperson)
if @FIO='Patronic'
set @get=(select isnull(Patronic,'') from person where idperson=@idperson)
if @FIO='Iduser'
set @get=(select isnull(iduser,'') from person where idperson=@idperson)
end	
return @get


END













GO