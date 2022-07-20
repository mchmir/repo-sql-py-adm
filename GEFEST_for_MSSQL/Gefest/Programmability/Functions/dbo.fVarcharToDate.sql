SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO



CREATE FUNCTION [dbo].[fVarcharToDate]  (@v as varchar)  
RETURNS DateTime AS  
BEGIN 
declare @d varchar(10)
set @d=left(@v,10)
return convert(datetime,substring(@d,7,4)+'-'+substring(@d,4,2)+'-'+substring(@d,1,2),20)
END


GO