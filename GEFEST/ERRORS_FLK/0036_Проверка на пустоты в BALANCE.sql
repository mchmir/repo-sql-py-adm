-----Проверяем на пустоты предшествующий и нынешний период--------------

select count(1) as COUNTER,
       BL.IDPERIOD,
       BL.IDCONTRACT,
       BL.IDACCOUNTING
from BALANCE as BL
where BL.IDPERIOD between DBO.FGETPREDPERIOD() and DBO.FGETNOWPERIOD()
group by BL.IDPERIOD, BL.IDCONTRACT, BL.IDACCOUNTING
having count(1) > 1

union

select count(1) as COUNTER,
       BLR.IDPERIOD,
       BLR.IDCONTRACT,
       BLR.IDACCOUNTING
from BALANCEREAL as BLR
where BLR.IDPERIOD between DBO.FGETPREDPERIOD() and DBO.FGETNOWPERIOD()
group by BLR.IDPERIOD, BLR.IDCONTRACT, BLR.IDACCOUNTING
having count(1) > 1

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
