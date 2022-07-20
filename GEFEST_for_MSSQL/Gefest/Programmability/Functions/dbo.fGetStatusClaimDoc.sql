SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fGetStatusClaimDoc] (@iddocument int)  
RETURNS int  AS  
BEGIN 
declare @get int
--@get:
--1-Сформирована
--2-Вручена
--3-Закрыта

select @get=count(*)
from pd with (nolock)  
where iddocument=@iddocument and idtypepd=23

if @get=0
begin
	set @get=1
	return @get
end

select @get=count(*)
from pd with (nolock)  
where iddocument=@iddocument and idtypepd=24

if @get=1
begin
	set @get=3
	return @get
end

set @get=2
return @get

END





GO