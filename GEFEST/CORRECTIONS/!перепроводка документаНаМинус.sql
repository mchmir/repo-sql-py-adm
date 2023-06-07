
declare @IDPeriod as int
declare @IDContract as int 


set @IDContract = 916143
set @IDPeriod =176--dbo.fGetNowPeriod() 



exec dbo.spRecalcBalances @IDContract, @IDPeriod
--exec dbo.spRecalcBalancesRealOnePeriodByContract @IDContract, @IDPeriod