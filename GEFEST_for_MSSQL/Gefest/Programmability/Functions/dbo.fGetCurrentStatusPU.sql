SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO







create FUNCTION [dbo].[fGetCurrentStatusPU] (@idgobject as int)  
RETURNS int  AS  
BEGIN 
declare @get int
--declare @status int
--set @status=(select idstatusgmeter from gmeter where idgmeter=@idgmeter)

set @get=(SELECT top 1 GMeter.IDStatusGMeter from
GMeter where GMeter.IDGObject=@idgobject
order by GMeter.IDStatusGMeter, GMeter.DateVerify desc)



--if @get is null

--set @get=(select top 1 name
--from oldvalues
--where nametable='GMeter' and 
--namecolumn='IDStatusGMeter'
--and idobject=@idgmeter
--and datevalues>=@date
--order by idoldvalues)

if @get is null
	set @get=2

return @get

END











GO