-- Показания без потребления
--  SELECT *
--  FROM FactUse fu 
--  LEFT JOIN Operation o ON o.IDOperation = fu.IDOperation
--  WHERE fu.IDGObject = 877617
--
--  021

DECLARE @IDPeriod INT;
DECLARE @Year     INT;
DECLARE @Month    INT;

SET @Year = 2024;
SET @Month = 5;

SET @IDPeriod = dbo.fGetIDPeriodMY(@Month, @Year);

SELECT
       i.IDIndication AS IDIndication,
       i.Display      AS [Показания],
       i.IdGMeter     AS IdGMeter,
       i.DateAdd      AS DateAdd,
       --dbo.fgetIDPeriod(i.DateAdd),
       fu.IDPeriod    AS IDPeriod,
       fu.FactAmount  AS [Начисления],
       c.Account      AS [Лицевой счет],
       gm.IDStatusGMeter,
       i.IDTypeIndication
  FROM Indication i 
       LEFT  JOIN FactUse  AS fu  ON i.IDIndication = fu.IDIndication 
       INNER JOIN Gmeter   AS gm  ON i.IdGMeter     = gm.IDGMeter
       INNER JOIN GObject  AS g   ON gm.IDGObject   = g.IDGObject
       INNER JOIN Contract AS c   ON g.IDContract   = c.IDContract
  WHERE ISNULL(fu.FactAmount, 0) = 0
    AND ISNULL(fu.IDPeriod, 0)   = 0
    AND dbo.fgetIDPeriod(i.DateAdd) = @IDPeriod
    AND gm.IDStatusGMeter = 1
    AND i.IDTypeIndication <> 5; -- AND i.IdGMeter = 773957

-- GO 
--
--SELECT i.Display
--, i.IdGMeter
--, i.DateAdd
--, dbo.fgetIDPeriod(i.DateAdd)
--FROM Indication i 
--  WHERE i.IdGMeter = 773957

--SELECT * FROM Indication i WHERE i.IDIndication = 9869458
--DELETE FROM Indication WHERE IDIndication = 11036804

