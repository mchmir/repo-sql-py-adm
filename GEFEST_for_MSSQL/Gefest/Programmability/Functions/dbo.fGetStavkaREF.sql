SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO







Create FUNCTION [dbo].[fGetStavkaREF] (@date datetime)  
RETURNS float  AS  
BEGIN 
declare @get float


set @get=(select top 1 name
from stavka
where  dbo.dateonly(date)<=dbo.dateonly(@date)
order by date desc)

return @get

END











GO