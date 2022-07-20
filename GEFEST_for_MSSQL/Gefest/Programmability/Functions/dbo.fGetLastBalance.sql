SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fGetLastBalance] (@IdPeriod int, @Idcontract int, @IdAccounting int)  
RETURNS float  AS  
BEGIN 
declare @get float
declare @IdPer int
select  top 1 @IdPer= b.idperiod
from balance b  with (nolock)
where b.idperiod<=@idperiod 
	and b.idcontract=@Idcontract
	and (IdAccounting=@IdAccounting or @IdAccounting=0)
order by b.idperiod desc
select @get=sum(b.amountbalance) 
from balance b  with (nolock)
where b.idperiod=@IdPer
	and b.idcontract=@Idcontract
	and (IdAccounting=@IdAccounting or @IdAccounting=0)
return @get
END






GO