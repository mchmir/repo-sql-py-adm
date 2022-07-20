SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO



-- =============================================
-- Author:		<Sumlikin,Aleksandr>
-- Create date: <12.11.2006>
-- Description:	<Процедура по перерасчету реального баланса по всем контрактом>
-- 7 января 2021  исправлено!
-- =============================================
CREATE         PROCEDURE [dbo].[spRecalcBalancesRealOnePeriod](@idPeriod as int) AS
SET NOCOUNT ON

declare @idPrevPeriod as int
select @idPrevPeriod=(select top 1 idPeriod 
	from period  with (nolock)  
	where idPeriod<@idPeriod 
	order by idPeriod DESC)

---------------------*******************Процедура по перерасчету реального баланса по всем контрактом-------------------************************
select count(1) Counter, idPeriod, idContract, idaccounting
into #tmpBalRal
from BalanceReal with (nolock)  
group by idPeriod, idContract, idaccounting
having count(1)>1
--order by idContract, idPeriod

delete balancereal
from balancereal b with (nolock)  
inner join #tmpBalRal t on t.idcontract=b.idcontract
	and b.idperiod=t.idperiod
	and b.idaccounting=t.idaccounting

drop table #tmpBalRal

insert into BalanceReal (IDAccounting,idPeriod,IDContract,AmountBalance,AmountCharge,AmountPay)
( select b1.IDAccounting,@idPeriod idPeriod
	,b1.IDContract,b1.AmountBalance
	,b1.AmountCharge,b1.AmountPay
from Balance B1 with (nolock)  
left join BalanceReal B2 with (nolock)   on b1.idContract=b2.idContract
	and b1.idAccounting=b2.idAccounting
	and b2.idPeriod=@idPeriod
where b2.idBalanceReal is null  and b1.idPeriod=@idPeriod  
 /* ---------------------070121---------
  AND  (b1.IDContract IS NOT NULL AND b2.IDContract IS not NULL)
  --------------------------------*/
  )

insert into BalanceReal (IDAccounting,idPeriod,IDContract,AmountBalance,AmountCharge,AmountPay)
( select b1.IDAccounting,@idPeriod idPeriod
	,b1.IDContract,b1.AmountBalance
	,b1.AmountCharge,b1.AmountPay
from BalanceReal B1 with (nolock)  
left join BalanceReal B2 with (nolock)   on b1.idContract=b2.idContract
	and b1.idAccounting=b2.idAccounting
	and b2.idPeriod=@idPeriod
where b2.idBalanceReal is null  and b1.idPeriod=@idPrevPeriod and b1.AmountBalance<>0
  /*  ---------------------070121---------
  AND  (b1.IDContract IS NOT NULL AND b2.IDContract IS not NULL)
  --------------------------------*/
)

update BalanceReal 
set AmountBalance=isnull(brl.AmountBalance,0)+isnull(qq.ao,0)
from balanceReal  br with (nolock)  
left join (
	select b.idaccounting, c.idcontract, sum(o.amountoperation) ao
	from Contract c  with (nolock)  
	inner join balance b  with (nolock)  on b.idcontract=c.idcontract
		and b.idperiod=@idperiod
	inner join operation o with(nolock) on o.idbalance=b.idbalance
		and isnull(o.numberoperation,0) <> 99999
	group by c.idcontract, b.idaccounting) qq on qq.idcontract=br.idcontract
	and br.idaccounting=qq.idaccounting
left join (select * from BalanceReal with (nolock)   where IdPeriod=@idPrevPeriod) brl
	on brl.idcontract=br.idcontract
	and brl.idaccounting=br.idaccounting
where br.idperiod=@idPeriod






GO