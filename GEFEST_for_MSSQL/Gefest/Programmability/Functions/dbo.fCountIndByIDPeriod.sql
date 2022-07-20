SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO







CREATE FUNCTION [dbo].[fCountIndByIDPeriod](@IDdocument int, @idperiod int)
RETURNS int AS  
BEGIN 
declare @ret as int

declare @idgmeter as int
declare @idgobject int
--declare @idgmeter1 int
set @idgmeter=(
select value
from pd
where iddocument=@IDdocument
and idtypepd=7)

set @idgobject=(
select idgobject
from gmeter
where idgmeter=@idgmeter)


--set @idgmeter1=(
--select idgmeter
--from gmeter
--where idgobject=@idgobject and idstatusgmeter=2)

--if (@idgmeter1 is null)
--begin
--set @idgmeter1=(
--select idgmeter
--from gmeter
--where idgobject=@idgobject and idstatusgmeter=1)
--end

--set @ret=(select count(*)
--from indication
--where idgmeter=@idgmeter
--and datedisplay<dbo.fGetDatePeriod(@idperiod,1))

set @ret=(select count(*)
from gobject go
inner join gmeter gm
on go.idgobject=gm.idgobject 
inner join indication i
on i.idgmeter=gm.idgmeter
where go.idgobject=@idgobject
and i.datedisplay<dbo.fGetDatePeriod(@idperiod,1))

return @ret
END





GO