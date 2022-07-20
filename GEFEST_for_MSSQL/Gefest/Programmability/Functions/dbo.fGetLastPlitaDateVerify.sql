SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

create FUNCTION [dbo].[fGetLastPlitaDateVerify] (@idhouse int)  
RETURNS datetime  AS  
BEGIN 
declare @get datetime
set @get=(select top 1 DateVerify
from plita p with (nolock)
inner join GObject g with (nolock) on g.Idgobject=p.Idgobject
inner join address a with (nolock) on a.idaddress=g.idaddress  
inner join house hs with (nolock) on hs.idhouse=a.idhouse
where hs.idhouse=@idhouse order by DateVerify desc)


return @get

END


GO