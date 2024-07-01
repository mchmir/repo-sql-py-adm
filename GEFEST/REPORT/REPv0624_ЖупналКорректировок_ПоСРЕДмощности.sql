/*
TypeCharge
+------------+-------------------+
|IDTypeCharge|Name               |
+------------+-------------------+
|1           |Разное             |
|2           |По средней мощности|
+------------+-------------------+

TypeDocument
+--------------+------------------------------------+-----+
|IDTypeDocument|Name                                |Level|
+--------------+------------------------------------+-----+
|7             |Корректировка начисления            |2    |
+--------------+------------------------------------|-----+

TypePD
+--------+--------------------------------------------+
|IDTypePD|Name                                        |
+--------+--------------------------------------------+
|34      |Тип Коректировки                            |
+--------+--------------------------------------------+


*/
------------------------------------------------------------------------------------------------------------------------


------------------------------------------------------------------------------------------------------------------------
-- Найти все счета с отрицательной суммой
WITH NegativeAmounts AS (
    SELECT C.ACCOUNT, SUM(D.DOCUMENTAMOUNT) as NEGATIVE_AMOUNT
    FROM DOCUMENT AS D
    JOIN CONTRACT AS C ON D.IDCONTRACT = C.IDCONTRACT
    JOIN PD ON D.IDDOCUMENT = PD.IDDOCUMENT
    WHERE IDTYPEDOCUMENT = 7
      AND PD.IDTYPEPD = 34
      AND PD.VALUE = 2
      AND DOCUMENTAMOUNT < 0
      AND YEAR(DOCUMENTDATE) >= 2019
    GROUP BY C.ACCOUNT
),
-- Найти все счета с положительной суммой
PositiveAmounts AS (
    SELECT C.ACCOUNT, SUM(D.DOCUMENTAMOUNT) as POSITIVE_AMOUNT
    FROM DOCUMENT AS D
    JOIN CONTRACT AS C ON D.IDCONTRACT = C.IDCONTRACT
    JOIN PD ON D.IDDOCUMENT = PD.IDDOCUMENT
    WHERE IDTYPEDOCUMENT = 7
      AND PD.IDTYPEPD = 34
      AND PD.VALUE = 2
      AND DOCUMENTAMOUNT > 0
      AND YEAR(DOCUMENTDATE) >= 2019
    GROUP BY C.ACCOUNT
)
SELECT COALESCE(NA.ACCOUNT, PA.ACCOUNT) AS ACCOUNT, NA.NEGATIVE_AMOUNT, PA.POSITIVE_AMOUNT
FROM NegativeAmounts AS NA
FULL OUTER JOIN PositiveAmounts AS PA ON NA.ACCOUNT = PA.ACCOUNT
WHERE NA.ACCOUNT IS NULL OR PA.ACCOUNT IS NULL
ORDER BY ACCOUNT;

------------------------------------------------------------------------------------------------------------------------
-- 200 счетов
select distinct C.ACCOUNT
from DOCUMENT as D
  join CONTRACT as C on D.IDCONTRACT = C.IDCONTRACT
  join PD on D.IDDOCUMENT = PD.IDDOCUMENT
where IDTYPEDOCUMENT=7 and PD.IDTYPEPD = 34 and PD.VALUE=2
   and YEAR(DOCUMENTDATE)>=2019;

------------------------------------------------------------------------------------------------------------------------
select C.ACCOUNT, D.IDDOCUMENT, D.IDPERIOD, D.DOCUMENTDATE, D.DOCUMENTAMOUNT
from DOCUMENT as D
  join CONTRACT as C on D.IDCONTRACT = C.IDCONTRACT
  join PD on D.IDDOCUMENT = PD.IDDOCUMENT
where IDTYPEDOCUMENT=7 and PD.IDTYPEPD = 34 and PD.VALUE=2
  and DOCUMENTAMOUNT < 0
  and YEAR(DOCUMENTDATE)>=2019;


select C.ACCOUNT, D.IDDOCUMENT, D.IDPERIOD, D.DOCUMENTDATE, D.DOCUMENTAMOUNT
from DOCUMENT as D
  join CONTRACT as C on D.IDCONTRACT = C.IDCONTRACT
  join PD on D.IDDOCUMENT = PD.IDDOCUMENT
where IDTYPEDOCUMENT=7 and PD.IDTYPEPD = 34 and PD.VALUE=2
  and DOCUMENTAMOUNT > 0
  and YEAR(DOCUMENTDATE)>=2019;