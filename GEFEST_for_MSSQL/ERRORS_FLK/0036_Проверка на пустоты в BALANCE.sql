-----Проверяем на пустоты предшествующий и нынешний период--------------

select count(1) Counter,Balance.idPeriod,Balance.idContract, balance.idaccounting
from Balance AS Balance
WHERE Balance.IDPeriod  BETWEEN  dbo.fGetPredPeriod() AND dbo.fGetNowPeriod()
group by Balance.idPeriod, Balance.idContract, balance.idaccounting
having count(1)>1

UNION

select count(1) Counter,Balance.idPeriod,Balance.idContract, balance.idaccounting
from BalanceReal AS Balance
WHERE Balance.IDPeriod  BETWEEN  dbo.fGetPredPeriod() AND dbo.fGetNowPeriod()
group by Balance.idPeriod, Balance.idContract, balance.idaccounting
HAVING count(1)>1

  
 
 ------- Удаляем если вдруг были пустоты- 1701--------------------------------------------
--DELETE FROM Balance  WHERE IDContract IS NULL AND IDPeriod =  dbo.fGetPredPeriod()
--DELETE FROM BalanceReal  WHERE IDContract IS NULL AND IDPeriod = dbo.fGetPredPeriod()
--DELETE FROM BalanceReal  WHERE IDContract IS NULL AND IDPeriod = dbo.fGetNowPeriod()
--DELETE FROM Balance  WHERE IDContract IS NULL AND IDPeriod =  dbo.fGetNowPeriod()