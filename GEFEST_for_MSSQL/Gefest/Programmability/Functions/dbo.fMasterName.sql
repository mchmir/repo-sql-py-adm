SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fMasterName](@name sysname, @pwd sysname)
RETURNS varchar(200) AS  
BEGIN 
declare @ret as varchar(200)
if @pwd=''
	set @pwd=null
select @ret=name
from master..syslogins
where name=@name and pwdcompare(@pwd, password,0) = 1
if @ret is null
	set @ret=''
return @ret
END

GO