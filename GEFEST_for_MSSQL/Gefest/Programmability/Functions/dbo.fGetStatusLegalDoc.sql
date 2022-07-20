SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fGetStatusLegalDoc] (@iddocument int)  
RETURNS int  AS  
BEGIN 
declare @get int
--@get:
--1-Начало работы
--2-Передача дела в суд
--3-Решение суда

select @get=count(*)
from pd with (nolock)  
where iddocument=@iddocument and idtypepd=9

if @get=0
begin
	set @get=1
	return @get
end

select @get=count(*)
from pd with (nolock)  
where iddocument=@iddocument and idtypepd=10

if @get=1
begin
	set @get=3
	return @get
end

set @get=2
return @get

END




GO