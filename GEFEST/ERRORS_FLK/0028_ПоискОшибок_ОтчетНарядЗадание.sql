-- исправляем только где  1 и 2
-- где 2 и 1 для информации, является ошибкой с т.з. базы но на отчеты не влияет


DECLARE @IDPeriod AS INT;
DECLARE @dEnd     AS DATETIME;
DECLARE @Month    AS INT;
DECLARE @Year     AS INT;

SET @Month = 6;
SET @Year = 2023;

SET @IDPeriod = dbo.fGetIDPeriodMY(@Month, @Year);
SET @dEnd     = dbo.fGetDatePeriod(@IDPeriod, 0);


SELECT 
       c.Account,
       c.IDContract,
       g.IDGMeter,
       g.IDStatusGMeter,
       dbo.fGetStatusPU2(@dEnd, g.IDGMeter)
  FROM GMeter   AS g, 
       GObject  AS g1,
       Contract AS c
 WHERE g1.IDGObject = g.IDGObject
   AND c.IDContract = g1.IDContract
   --AND g.IDGMeter =722547 
   AND g.IDStatusGMeter <> dbo.fGetStatusPU2(@dEnd, g.IDGMeter)
   AND dbo.fGetStatusPU2(@dEnd, g.IDGMeter) <> 3
 ORDER BY g.IDStatusGMeter;


-- Исправление подстановка IDGmeter и исправление Name = 2
/*

+-------+----------+--------+
|Account|IDContract|IDGMeter|
+-------+----------+--------+
|1753018|867269    |813165  |
|2772010|879216    |813138  |
+-------+----------+--------+


SELECT * 
  FROM OldValues AS ov
 WHERE ov.IdObject =  813138
 ORDER BY ov.DateValues DESC;

UPDATE OldValues 
   SET Name = 2 
 WHERE IdOldValues in (1133742);

--SELECT * FROM GMeter g WHERE g.IDGMeter = 718821  778139 803778




SELECT 
       c.Account
  FROM Contract AS c
 WHERE c.IDContract = (SELECT IDContract 
                         FROM GObject 
                        WHERE IDGObject = 904658)
*/

