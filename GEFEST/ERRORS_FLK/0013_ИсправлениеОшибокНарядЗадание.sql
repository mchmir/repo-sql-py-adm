--- Исправление ошибок в наряде задании

/*
result by script 0015
+-------+----------+-----------+
|Account|IDContract|CountGMeter|
+-------+----------+-----------+
|2041137|871749    |2          |
|2041306|871918    |2          |
|3751001|892170    |2          |
|0461001|900247    |2          |
+-------+----------+-----------+

*/



SELECT *
  FROM GMeter AS g
 WHERE g.IDGObject IN (SELECT g.IDGObject
                         FROM GObject AS g
                        WHERE g.IDContract = 922870);
---- 1 подключен
---- 2 отключен

SELECT *
  FROM OldValues AS ov
 WHERE ov.IdObject = 757946 --784492 --812901
 ORDER BY ov.DateValues DESC;

SELECT dbo.fGetStatusPU((SELECT p.DateEnd
                           FROM Period as p
                          WHERE p.IDPeriod = 193), 720495);  --758228
---- для OldValues наоборот 1 отключен
----                        2 подключен  
--724727 2
--807269 1
--739159
--807245

--- idObject это IDGmeter
--- последняя цифра по дате должна быть 2 для подключенного счетчика

--UPDATE OldValues SET Name = 1 WHERE IdOldValues in (1178040)

SELECT *
  FROM Contract AS c
 WHERE c.Account = 1641035; --1742130

select *
from CONTRACT where IDCONTRACT = 922870;

