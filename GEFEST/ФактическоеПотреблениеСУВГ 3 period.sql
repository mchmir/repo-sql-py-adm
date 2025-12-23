-- SETTING
DECLARE @Potreblenie BIT;
DECLARE @Month1_1    INT;
DECLARE @Year1_1     INT;
DECLARE @Month1_2    INT;
DECLARE @Year1_2     INT;

DECLARE @Month2_1    INT;
DECLARE @Year2_1     INT;
DECLARE @Month2_2    INT;
DECLARE @Year2_2     INT;

DECLARE @Month3_1    INT;
DECLARE @Year3_1     INT;
DECLARE @Month3_2    INT;
DECLARE @Year3_2     INT;

DECLARE @Period1_1   INT;
DECLARE @Period1_2   INT;
DECLARE @Period2_1   INT;
DECLARE @Period2_2   INT;
DECLARE @Period3_1   INT;
DECLARE @Period3_2   INT;

----------the first period-------------
SET @Month1_1 = 1;
SET @Year1_1  = 2024;
SET @Month1_2 = 12;
SET @Year1_2  = 2024;
----------the second period -----------
SET @Month2_1 = 7;
SET @Year2_1  = 2024;
SET @Month2_2 = 6;
SET @Year2_2  = 2025;
----------the third period -----------
SET @Month3_1 = 1;
SET @Year3_1  = 2025;
SET @Month3_2 = 6;
SET @Year3_2  = 2025;
--------------------------------------
-- с нулевым потреблением, оставляем 0 только для подключенных ОУ и ПУ
-- 1 - без 0 в потрблении 
SET @Potreblenie = 0;

--------------------------------------
-- END SETTING


--- PROGRAM
-- первый период
SET @Period1_1 = dbo.fGetIDPeriodMY(@Month1_1, @Year1_1);
SET @Period1_2 = dbo.fGetIDPeriodMY(@Month1_2, @Year1_2);
-- второй период
SET @Period2_1 = dbo.fGetIDPeriodMY(@Month2_1, @Year2_1);
SET @Period2_2 = dbo.fGetIDPeriodMY(@Month2_2, @Year2_2);
-- третий период
SET @Period3_1 = dbo.fGetIDPeriodMY(@Month3_1, @Year3_1);
SET @Period3_2 = dbo.fGetIDPeriodMY(@Month3_2, @Year3_2);


SELECT
  c.Account,
  s.name + ', ' + LTRIM(STR(h.housenumber)) + ISNULL(h.housenumberchar, '') + '-' + LTRIM(a.flat) AS Address 
INTO #tmpAccount
FROM Contract AS c,
     GObject  AS g,
     Person   AS p,
     Address  AS a,
     Street   AS s,
     House    AS h
WHERE c.IDContract = g.IDContract
  AND c.IDPerson = p.IDPerson
  AND p.IDAddress = a.IDAddress
  AND a.IDStreet = s.IDStreet
  AND a.IDHouse = h.IDHouse
ORDER BY c.Account;

SELECT
  c.Account,
  SUM(fu.FactAmount) AS sumP1
INTO #tmpPotreblenie1
FROM FactUse  AS fu,
     TypeFU   AS tf,
     Contract AS c,
     GObject  AS g
WHERE tf.IDTypeFU = fu.IDTypeFU
  AND fu.IDGObject = g.IDGObject
  AND c.IDContract = g.IDContract
  AND fu.IDPeriod >= @Period1_1
  AND fu.IDPeriod <= @Period1_2
  --AND g.IDStatusGObject = 1
  AND fu.IDTypeFU IN (1, 2)   -- 1 по ПУ, 2 - по кол-ву проживающих
GROUP BY c.Account
ORDER BY c.Account;

SELECT
  c.Account,
  SUM(fu.FactAmount) AS sumP2
INTO #tmpPotreblenie2
FROM FactUse  AS fu,
     TypeFU   AS tf,
     Contract AS c,
     GObject  AS g
WHERE tf.IDTypeFU = fu.IDTypeFU
  AND fu.IDGObject = g.IDGObject
  AND c.IDContract = g.IDContract
  AND fu.IDPeriod >= @Period2_1
  AND fu.IDPeriod <= @Period2_2
  --AND g.IDStatusGObject = 1
  AND fu.IDTypeFU IN (1, 2)
GROUP BY c.Account
ORDER BY c.Account;


SELECT
  c.Account,
  SUM(fu.FactAmount) AS sumP3
INTO #tmpPotreblenie3
FROM FactUse  AS fu,
     TypeFU   AS tf,
     Contract AS c,
     GObject  AS g
WHERE tf.IDTypeFU = fu.IDTypeFU
  AND fu.IDGObject = g.IDGObject
  AND c.IDContract = g.IDContract
  AND fu.IDPeriod >= @Period3_1
  AND fu.IDPeriod <= @Period3_2
  --AND g.IDStatusGObject = 1
  AND fu.IDTypeFU IN (1, 2)
GROUP BY c.Account
ORDER BY c.Account;


SELECT DISTINCT
  a.Account,
  a.Address,
  ISNULL(p1.sumP1, 0) AS [2024г.],
  ISNULL(p2.sumP2, 0) AS [июль 2024г. по июнь 2025г.],
  ISNULL(p3.sumP3, 0) AS [январь 2025г. по июнь 2025г.]
INTO #tmpItog
FROM #tmpAccount AS a
  LEFT JOIN #tmpPotreblenie1 AS p1 ON a.Account = p1.Account
  LEFT JOIN #tmpPotreblenie2 AS p2 ON a.Account = p2.Account
  LEFT JOIN #tmpPotreblenie3 AS p3 ON a.Account = p3.Account
ORDER BY a.Account;

IF @Potreblenie = 0
  BEGIN
  -- с нулевым потреблением
  -- но оставляем с 0 только там где ОУ и ПУ подключены 
  SELECT
    ROW_NUMBER() OVER(ORDER BY i.Account ASC) AS Row#,*
  FROM #tmpItog AS i
  WHERE 
    (i.Account IN (SELECT c.Account
                          FROM Contract AS c
                            JOIN GObject AS  g  ON c.IDContract = g.IDContract
                            JOIN GMeter  AS  g1 ON g.IDGObject  = g1.IDGObject
                          WHERE g.IDStatusGObject = 1 AND g1.IDStatusGMeter = 1)
    ) 
    OR (i.[июль 2024г. по июнь 2025г.]<>0)
    OR (i.[2024г.] <> 0)
    OR (i.[январь 2025г. по июнь 2025г.] <> 0)
  ORDER BY i.Account;
END
ELSE 
  BEGIN
    SELECT
      ROW_NUMBER() OVER(ORDER BY i.Account ASC) AS Row#,*
    FROM #tmpItog AS i
    WHERE i.[июль 2024г. по июнь 2025г.] <> 0
      OR i.[2024г.] <> 0
      OR i.[январь 2025г. по июнь 2025г.] <> 0
    ORDER BY i.Account;
  END
 
---------------------------------------------------------


DROP TABLE #tmpAccount;
DROP TABLE #tmpPotreblenie1;
DROP TABLE #tmpPotreblenie2;
DROP TABLE #tmpPotreblenie3;
DROP TABLE #tmpItog;

