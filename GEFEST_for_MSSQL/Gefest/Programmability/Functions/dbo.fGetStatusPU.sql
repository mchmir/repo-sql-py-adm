SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO






CREATE FUNCTION [dbo].[fGetStatusPU] (@date datetime,@idgmeter int)  
RETURNS int  AS  
BEGIN 
declare @get int
--declare @status int
--set @status=(select idstatusgmeter from gmeter where idgmeter=@idgmeter)

set @get=(select top 1 name
from oldvalues
where nametable='GMeter' and 
namecolumn='IDStatusGMeter'
and idobject=@idgmeter
and dbo.dateonly(datevalues)<=dbo.dateonly(@date)
order by datevalues desc,idoldvalues desc)



--if @get is null

--set @get=(select top 1 name
--from oldvalues
--where nametable='GMeter' and 
--namecolumn='IDStatusGMeter'
--and idobject=@idgmeter
--and datevalues>=@date
--order by idoldvalues)

if @get is null
	return 2
if @get=1
	set @get=2
else
	set @get=1 
return @get

END










GO