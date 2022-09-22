
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

DECLARE @Period1_1 INT;
DECLARE @Period1_2 INT;
DECLARE @Period2_1 INT;
DECLARE @Period2_2 INT;

----------the first period-------------
SET @Month1_1 = 1;
SET @Year1_1  = 2021;

SET @Month1_2 = 12;
SET @Year1_2  = 2021;
----------the second period -----------
SET @Month2_1 = 7;
SET @Year2_1  = 2021;
SET @Month2_2 = 6;
SET @Year2_2  = 2022;
--------------------------------------
SET @Potreblenie = 0; -- с нулевым потреблением, 1 - без 0 в потрблении 

--------------------------------------
-- END SETTING


--- PROGRAM

SET @Period1_1 = dbo.fGetIDPeriodMY(@Month1_1, @Year1_1);
SET @Period1_2 = dbo.fGetIDPeriodMY(@Month1_2, @Year1_2);

SET @Period2_1 = dbo.fGetIDPeriodMY(@Month2_1, @Year2_1);
SET @Period2_2 = dbo.fGetIDPeriodMY(@Month2_2, @Year2_2);






SELECT
  c.Account
 ,s.name + ', ' + LTRIM(STR(h.housenumber)) + ISNULL(h.housenumberchar, '') + '-' + LTRIM(a.flat) AS Address 
INTO #tmpAccount
FROM Contract c
    ,GObject g
    ,Person p
    ,Address a
    ,Street s
    ,House h
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
FROM FactUse fu
    ,TypeFU tf
    ,Contract c
    ,GObject g
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
FROM FactUse fu
    ,TypeFU tf
    ,Contract c
    ,GObject g
WHERE tf.IDTypeFU = fu.IDTypeFU
AND fu.IDGObject = g.IDGObject
AND c.IDContract = g.IDContract
AND fu.IDPeriod >= @Period2_1
AND fu.IDPeriod <= @Period2_2
--AND g.IDStatusGObject = 1
AND fu.IDTypeFU IN (1, 2)
GROUP BY c.Account
ORDER BY c.Account;

/*
-- с июля 2020 по июнь 2021
SELECT
  c.Account
 ,SUM(fu.FactAmount) AS summ3 
INTO #tmpPotreblenie3
FROM FactUse fu
    ,TypeFU tf
    ,Contract c
    ,GObject g
WHERE tf.IDTypeFU = fu.IDTypeFU
AND fu.IDGObject = g.IDGObject
AND c.IDContract = g.IDContract
AND fu.IDPeriod >= 186
AND fu.IDPeriod <= 197
--AND g.IDStatusGObject = 1
AND fu.IDTypeFU IN (1, 2)
GROUP BY c.Account
ORDER BY c.Account;
*/

SELECT DISTINCT
  a.Account,
  a.Address,
  ISNULL(p1.sumP1, 0) AS [2021г],
  ISNULL(p2.sumP2, 0) AS [0721г-0622г] 
-- ,ISNULL(p.summ3, 0) AS [0721г-0622г]
INTO #tmpItog
FROM #tmpAccount as a
LEFT JOIN #tmpPotreblenie1 as p1
  ON a.Account = p1.Account
LEFT JOIN #tmpPotreblenie2 as p2
  ON a.Account = p2.Account
--LEFT JOIN #tmpPotreblenie3 p3
--  ON a.Account = p3.Account
ORDER BY a.Account;

IF @Potreblenie = 0 
  BEGIN
  -- с нулевым потреблением
  SELECT
    ROW_NUMBER() OVER(ORDER BY i.Account ASC) AS Row#,*
  FROM #tmpItog i
  --WHERE i.[0721г-0622г] <> 0
  --OR i.[2021г] <> 0
  --OR i.[2020г] <> 0
  ORDER BY i.Account;
END
ELSE 
  BEGIN
    SELECT
      ROW_NUMBER() OVER(ORDER BY i.Account ASC) AS Row#,*
    FROM #tmpItog i
    WHERE i.[0721г-0622г] <> 0
      OR i.[2021г] <> 0
      --OR i.[2020г] <> 0
    ORDER BY i.Account;
  END  
 
---------------------------------------------------------


DROP TABLE #tmpAccount;
DROP TABLE #tmpPotreblenie1;
DROP TABLE #tmpPotreblenie2;
--DROP TABLE #tmpPotreblenie3;
DROP TABLE #tmpItog;

