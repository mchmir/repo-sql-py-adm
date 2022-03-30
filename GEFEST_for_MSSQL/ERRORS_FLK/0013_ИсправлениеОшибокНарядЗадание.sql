--- Исправление ошибок в наряде задании

SELECT * FROM GMeter g WHERE g.IDGObject IN (SELECT g.IDGObject FROM GObject g WHERE g.IDContract = 912164)
---- 1 подключен
---- 2 отключен

SELECT * FROM OldValues ov WHERE ov.IdObject = 769863 --722397 --809027
ORDER BY ov.DateValues DESC

SELECT dbo.fGetStatusPU((SELECT p.DateEnd FROM Period p WHERE p.IDPeriod = 193), 720495  --758228
---- для OldValues наоборот 1 отключен
----                        2 подключен  
--724727 2
--807269 1
--739159
--807245

--- idObject это IDGmeter
--- последняя цифра по дате должна быть 2 для подключенного счетчика

--UPDATE OldValues SET Name = 1 WHERE IdOldValues = 1052068

SELECT * FROM Contract c WHERE c.Account =2463017 --1742130

