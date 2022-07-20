SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
CREATE FUNCTION [dbo].[fGetLastBalanceTotal] (@IdPeriod int, @Idcontract int)  
RETURNS float  AS  
BEGIN 
declare @get float
declare @IdPer int
select  top 1 @IdPer= b.idperiod
from balance b  with (nolock)
where b.idperiod<=@idperiod 
	and b.idcontract=@Idcontract

order by b.idperiod desc
select @get=sum(b.amountbalance) 
from balance b  with (nolock)
where b.idperiod=@IdPer
	and b.idcontract=@Idcontract
return @get
END






GO