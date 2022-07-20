SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

CREATE PROCEDURE [dbo].[spRecalcBalancesOnePeriod](@idPeriod as int) AS
declare @idPrevPeriod as int
select @idPrevPeriod=(select top 1 idPeriod 
	from period  with (nolock)  
	where idPeriod<@idPeriod 
	order by idPeriod DESC)
------------------------********************Процедура по перерасчету баланса по всем контрактом***********-----------------------
insert into Balance (IDAccounting,idPeriod,IDContract,AmountBalance,AmountCharge,AmountPay)
( select b1.IDAccounting,@idPeriod idPeriod
	,b1.IDContract,b1.AmountBalance
	,b1.AmountCharge,b1.AmountPay
from Balance B1 with (nolock)  
left outer join Balance B2 with (nolock)   on b1.idContract=b2.idContract
 and b1.idAccounting=b2.idAccounting
 and b2.idPeriod=@idPeriod
where b2.idBalance is null --and b1.AmountBalance<>0 
and b1.idPeriod=@idPrevPeriod
 /* ---------------------070121---------
  AND  (b1.IDContract IS NOT NULL AND b2.IDContract IS not NULL)
  --------------------------------*/
)

update balance set balance.AmountBalance=T.AmountBalance
	,balance.AmountCharge=T.AmountCharge
	,balance.AmountPay=T.AmountPay
from balance with (nolock)  
inner join (select NewBal.IDAccounting IDAccounting,@IDPeriod IDPeriod,NewBal.IDContract IDContract
		,isnull(balance.AmountBalance,0)+isnull(sum(operation.AmountOperation),0) as AmountBalance
		,isnull(balance.AmountCharge,0)+isnull(sum(case when TypeOperation.IdTypeOperation=2 then operation.AmountOperation else 0 end),0) as AmountCharge
		,isnull(balance.AmountPay,0)+isnull(sum(case when TypeOperation.IdTypeOperation=1 then operation.AmountOperation else 0 end),0) as AmountPay
	from (select * from balance bbb  with (nolock)  where idperiod=@idPrevPeriod) balance
		right outer join balance NewBal with (nolock)   on balance.idContract=NewBal.idContract and balance.idAccounting=NewBal.idAccounting
		left outer join operation  with (nolock)  on operation.idbalance=NewBal.idbalance
		left outer join TypeOperation  with (nolock)  on operation.idTypeOperation=TypeOperation.idTypeOperation
	where NewBal.idPeriod=@idPeriod
	group by NewBal.IDAccounting,NewBal.IDContract,balance.AmountBalance,balance.AmountCharge,balance.AmountPay
) T on balance.idPeriod=T.idPeriod and balance.idContract=T.idContract and balance.idAccounting=T.idAccounting
GO