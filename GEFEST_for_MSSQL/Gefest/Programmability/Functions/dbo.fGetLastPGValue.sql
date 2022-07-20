SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fGetLastPGValue] (@IdPeriod int, @IdTypePG int, @IdGRU int)  
  
RETURNS varchar(50)  AS  
BEGIN 
declare @ret varchar(50)
select top 1 @ret=Value
from PG  with (nolock)  
where IDPeriod <= @IdPeriod 
	and IDTypePG = @IdTypePG
	and IdGRU= @IdGRU
order by IDPeriod desc
return @ret
END
GO