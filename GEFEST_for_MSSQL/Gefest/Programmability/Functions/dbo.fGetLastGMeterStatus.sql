SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fGetLastGMeterStatus] (@dEnd datetime,  @Idgobject int)  
RETURNS int AS  
BEGIN 
declare @get int
select  top 1 @get=q.IDStatusGmeter
from gmeter q  with (nolock)
where q.idgobject=@Idgobject and q.IDStatusGmeter=1 and convert(datetime,round(convert(float,q.dateinstall),0, 1),20)<=@dEnd
order by q.dateinstall desc, idgmeter desc
if @get is null 
	set @get=2
return @get
END


GO