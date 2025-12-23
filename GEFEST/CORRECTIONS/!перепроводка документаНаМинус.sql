
declare @IDPeriod as int
declare @IDContract as int 


set @IDContract = 864581
set @IDPeriod =250--dbo.fGetNowPeriod()



exec dbo.spRecalcBalances @IDContract, @IDPeriod
--exec dbo.spRecalcBalancesRealOnePeriodByContract @IDContract, @IDPeriod