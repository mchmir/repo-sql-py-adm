SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
create FUNCTION [dbo].[fGetLastBalanceReal] (@IdPeriod int, @Idcontract int, @IdAccounting int)  
RETURNS float  AS  
BEGIN 
declare @get float
declare @IdPer int

select  top 1 @IdPer= b.idperiod
from balancereal b  with (nolock)
where b.idperiod<=@idperiod 
	and b.idcontract=@Idcontract
	and (IdAccounting=@IdAccounting or @IdAccounting=0)
order by b.idperiod desc

select @get=sum(b.amountbalance) 
from balancereal b  with (nolock)
where b.idperiod=@IdPer
	and b.idcontract=@Idcontract
	and (IdAccounting=@IdAccounting or @IdAccounting=0)

return @get
END
GO