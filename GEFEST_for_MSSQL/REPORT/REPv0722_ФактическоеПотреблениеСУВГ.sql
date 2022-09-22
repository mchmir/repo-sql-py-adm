/*

#tmpPotreblenie1
январь  2021 - 192
декабрь 2021 - 203

#tmpPotreblenie2
июль  2021  - 198
июнь  2022  - 209

select * from period

*/
DECLARE @Potreblenie BIT;

SET @Potreblenie = 1; -- с нулевым потреблением, 1 - без 0 в потрблении 


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
AND fu.IDPeriod >= 192
AND fu.IDPeriod <= 203
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
AND fu.IDPeriod >= 198
AND fu.IDPeriod <= 209
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
    	

DROP TABLE #tmpAccount;
DROP TABLE #tmpPotreblenie1;
DROP TABLE #tmpPotreblenie2;
--DROP TABLE #tmpPotreblenie3;
DROP TABLE #tmpItog;

