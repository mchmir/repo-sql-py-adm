SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fShowNumberBatch](@IDBatch int, @CalcNumber int)
RETURNS varchar(20) AS  
BEGIN 
declare @ret as varchar(20)
declare @ret_number as varchar(20)
set @ret=(select top 1 convert(varchar, CONVERT(INT,isnull(DocumentNumber,0))+1) DocumentNumber
from document with (nolock) where idbatch =@IDBatch
order by convert(int, DocumentNumber) desc)
if @ret is null
	set @ret="1"

if @calcnumber=0
	set @ret_number=@ret

return @ret_number
END




GO