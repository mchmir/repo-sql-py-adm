-- Есть начисления по услугам за период, но нет платежей

select *
from CONTRACT as C
where C.IDCONTRACT in (select IDCONTRACT
                       from DOCUMENT as D
                       where D.IDTYPEDOCUMENT = 24
                         and D.IDPERIOD = 171
                         except
                       select IDCONTRACT
                       from DOCUMENT as D
                       where D.IDTYPEDOCUMENT = 1
                         and D.IDPERIOD = 171
);

select *
from BALANCEREAL as BR
where BR.IDCONTRACT in (select IDCONTRACT
                        from DOCUMENT as D
                        where D.IDTYPEDOCUMENT = 24
                          and D.IDPERIOD = 171
                          except
                        select IDCONTRACT
                        from DOCUMENT as D
                        where D.IDTYPEDOCUMENT = 1
                          and D.IDPERIOD = 171
)
  and BR.IDPERIOD = 171
  and BR.AMOUNTBALANCE = 0;


---  ************************  Итоговый выполнять после закрытия периода ************************
-- и сальдо по услугам  = 0
declare @IDPERIOD INT;
declare @YEAR INT;
declare @MONTH INT;

set @YEAR = 2022;
set @MONTH = 9;

set @IDPERIOD = DBO.FGETIDPERIODMY(@MONTH, @YEAR);

select *
from CONTRACT as C
where C.IDCONTRACT in (select BR.IDCONTRACT
                       from BALANCEREAL as BR
                       where BR.IDCONTRACT in (select IDCONTRACT
                                               from DOCUMENT as D
                                               where D.IDTYPEDOCUMENT = 24
                                                 and D.IDPERIOD = @IDPERIOD
                                                 except
                                               select IDCONTRACT
                                               from DOCUMENT as D
                                               where D.IDTYPEDOCUMENT = 1
                                                 and D.IDPERIOD = @IDPERIOD
                       )
                         and BR.IDPERIOD = @IDPERIOD
                         and BR.AMOUNTBALANCE = 0
                         and BR.IDACCOUNTING = 6
);


---1942076 и 2471004,3151096  194 период норм

---  ************************ Предварительный ************************
-- и сальдо по услугам  = 0
declare @IDPERIOD INT;
declare @YEAR INT;
declare @MONTH INT;

set @YEAR = 2024;
set @MONTH = 9;


set @IDPERIOD = DBO.FGETIDPERIODMY(@MONTH, @YEAR);

select *
from CONTRACT as C
where C.IDCONTRACT in (select BR.IDCONTRACT
                       from BALANCE as BR
                       where BR.IDCONTRACT in (select IDCONTRACT
                                               from DOCUMENT as D
                                               where D.IDTYPEDOCUMENT = 24 -- услуги
                                                 and D.IDPERIOD = @IDPERIOD
                                                 except
                                               select IDCONTRACT
                                               from DOCUMENT as D
                                               where D.IDTYPEDOCUMENT = 1 -- оплата
                                                 and D.IDPERIOD = @IDPERIOD
                       )
                         and BR.IDPERIOD = @IDPERIOD
                         and BR.AMOUNTBALANCE = 0
                         and BR.IDACCOUNTING = 6
);


/*

+----------+-------+
|IDContract|Account|
+----------+-------+
|878874    |2764001|
+----------+-------+









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

