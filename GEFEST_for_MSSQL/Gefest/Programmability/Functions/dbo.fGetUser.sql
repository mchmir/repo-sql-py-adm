SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

create FUNCTION [dbo].[fGetUser](@Iduser int)  
RETURNS varchar(200) AS  
BEGIN 
declare @name varchar(200)
select @name=name from sysusers where uid=@iduser
return isnull(@name,'')
END

GO