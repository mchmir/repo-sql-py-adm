-----Проверяем на пустоты предшествующий и нынешний период--------------

SELECT 
       count(1) AS Counter,
       BL.idPeriod,
       BL.idContract, 
       BL.idaccounting
  FROM Balance AS BL
 WHERE BL.IDPeriod BETWEEN dbo.fGetPredPeriod() AND dbo.fGetNowPeriod()
 GROUP BY BL.idPeriod, BL.idContract, BL.idaccounting
HAVING count(1) > 1

UNION

SELECT 
       count(1) AS Counter,
       BLR.idPeriod,
       BLR.idContract,
       BLR.idaccounting
  FROM BalanceReal AS BLR
 WHERE BLR.IDPeriod BETWEEN dbo.fGetPredPeriod() AND dbo.fGetNowPeriod()
 GROUP BY BLR.idPeriod, BLR.idContract, BLR.idaccounting
HAVING count(1) > 1

--1723082  
 
 ------- Удаляем если вдруг были пустоты- 1701--------------------------------------------
--DELETE FROM Balance  WHERE IDContract IS NULL AND IDPeriod =  dbo.fGetPredPeriod()
--DELETE FROM BalanceReal  WHERE IDContract IS NULL AND IDPeriod = dbo.fGetPredPeriod()
--DELETE FROM BalanceReal  WHERE IDContract IS NULL AND IDPeriod = dbo.fGetNowPeriod()
--DELETE FROM Balance  WHERE IDContract IS NULL AND IDPeriod =  dbo.fGetNowPeriod()
/*
select *
from Balance AS Balance
WHERE Balance.IDPeriod  BETWEEN  dbo.fGetPredPeriod() AND dbo.fGetNowPeriod()
	and balance.idcontract = 888714

select * 
  from Contract as C
  where c.idcontract = 888714;*/
