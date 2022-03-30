/*-- Есть начисления по услугам за период, но нет платежей*/
SELECT * FROM Contract c
WHERE c.IDContract IN 
(SELECT idcontract FROM Document d WHERE d.IDTypeDocument = 24 AND d.IDPeriod = 171
EXCEPT
SELECT idcontract FROM Document d WHERE d.IDTypeDocument = 1 AND d.IDPeriod = 171)



SELECT * FROM BalanceReal br
WHERE br.IDContract IN 
(SELECT idcontract FROM Document d WHERE d.IDTypeDocument = 24 AND d.IDPeriod = 171
EXCEPT
SELECT idcontract FROM Document d WHERE d.IDTypeDocument = 1 AND d.IDPeriod = 171)
  AND br.IDPeriod = 171 AND br.AmountBalance =0

*/

---  ************************  Итоговый выполнять после закрытия периода ************************
-- и сальдо по услугам  = 0
DECLARE @IDPeriod INT
SET @IDPeriod = 203

SELECT * FROM Contract c
WHERE c.IDContract IN 
(SELECT br.IDContract FROM BalanceReal br
WHERE br.IDContract IN 
(SELECT idcontract FROM Document d WHERE d.IDTypeDocument = 24 AND d.IDPeriod = @IDPeriod
EXCEPT
SELECT idcontract FROM Document d WHERE d.IDTypeDocument = 1 AND d.IDPeriod = @IDPeriod)
  AND br.IDPeriod = @IDPeriod AND br.AmountBalance = 0 AND br.IDAccounting =6)

  ---1942076 и 2471004,3151096  194 период норм

---  ************************  Итоговый Предварительный ************************
-- и сальдо по услугам  = 0
DECLARE @IDPeriod INT
SET @IDPeriod = 206

SELECT * FROM Contract c
WHERE c.IDContract IN 
(SELECT br.IDContract FROM Balance br
WHERE br.IDContract IN 
(SELECT idcontract FROM Document d WHERE d.IDTypeDocument = 24 AND d.IDPeriod = @IDPeriod
EXCEPT
SELECT idcontract FROM Document d WHERE d.IDTypeDocument = 1 AND d.IDPeriod = @IDPeriod)
  AND br.IDPeriod = @IDPeriod AND br.AmountBalance = 0 AND br.IDAccounting =6)

  /*
861308
*/

/*
  SELECT * FROM Balance br WHERE br.IDContract = 869418 AND br.IDPeriod >=190

  
  UPDATE balancereal set amountbalance = -1026.76 where idbalancereal=16393946;
  update balancereal set amountbalance = -1178 where idbalancereal=16393409;
  UPDATE balancereal set amountbalance = -1026.76 where idbalancereal=16498988;
  update balancereal set amountbalance = -1178 where idbalancereal=16498989;
  
   update balancereal set amountcharge =  -1026.76 where idbalancereal=16393946;
   update balancereal set amountcharge =  -1026.76 where idbalancereal=16498988;
  update balancereal set amountcharge = -1178 where idbalancereal=16498989;

SELECT * FROM BalanceReal br WHERE br.IDContract = 914965 AND br.IDPeriod >=193

  update balance set amountbalance = -1026.76 where idbalance=16862876;
  update balance set amountbalance =  -1178 where idbalance=16862877;
  
  update balance set amountcharge = -1026.76 where idbalance=16862876;
  update balance set amountcharge = -1178 where idbalance=16862877;
*/

