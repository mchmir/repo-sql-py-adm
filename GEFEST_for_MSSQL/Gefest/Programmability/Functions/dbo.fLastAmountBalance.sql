SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fLastAmountBalance] (@IDCashier int, @Date datetime)
RETURNS float AS  
BEGIN 
declare @AmountBalance as float
select top 1 @AmountBalance =  AmountBalance
from CashBalance with (nolock)  
where idcashier  = @IDCashier  and DateCash<@Date
order by DateCash desc
return @AmountBalance
END

GO