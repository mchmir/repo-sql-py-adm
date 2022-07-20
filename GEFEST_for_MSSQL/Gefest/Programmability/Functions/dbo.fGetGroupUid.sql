SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fGetGroupUid] (@Name int)  
RETURNS int AS  
BEGIN 
declare @ret int
select top 1 @ret=groupuid
from sysusers s
inner join sysmembers m on s.uid=m.memberuid
and s.uid=@Name
inner join sys.database_principals p on p.principal_id=m.groupuid
and is_fixed_role=0

return @ret
END

GO