SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fShowNumberBatchDispatcher](@CalcNumber int, @date datetime)
RETURNS varchar(20) AS  
BEGIN 
declare @ret as varchar(20)
declare @ret_number as varchar(20)
set @ret=(select top 1 convert(varchar, CONVERT(INT,isnull(NumberBatch,0))+1) NumberBatch
from Batch with (nolock) where name <> '0' and isnumeric(NumberBatch)=1 and year(@date)=year(Batchdate)  and idcashier is not null 
order by CONVERT(INT,NumberBatch) desc)
if @ret is null
	set @ret='1'
if @calcnumber=0
	set @ret_number=@ret
return @ret_number
END







GO