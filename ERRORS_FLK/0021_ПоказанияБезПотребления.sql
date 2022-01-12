--SELECT * 
--  FROM FactUse fu 
--  LEFT JOIN Operation o ON o.IDOperation = fu.IDOperation
--  WHERE fu.IDGObject = 877617
--
--  GO 

SELECT
  i.IDIndication,
  i.Display AS [Показания]
  , i.IdGMeter
  , i.DateAdd
  , dbo.fgetIDPeriod(i.DateAdd)
  , fu.IDPeriod
  , fu.FactAmount AS [Начисления] 
  , c.Account AS  [Лицевой счет]
  , gm.IDStatusGMeter
  , i.IDTypeIndication
  FROM Indication i 
       left JOIN FactUse fu  ON i.IDIndication = fu.IDIndication 
       INNER JOIN Gmeter gm ON i.IdGMeter = gm.IDGMeter
       INNER JOIN GObject g ON gm.IDGObject = g.IDGObject
       INNER JOIN Contract c ON g.IDContract = c.IDContract
  WHERE ISNULL(fu.FactAmount, 0)=0 
         AND ISNULL(fu.IDPeriod,0) = 0 
            AND dbo.fgetIDPeriod(i.DateAdd) IN (203) 
              AND gm.IDStatusGMeter = 1 
               AND i.IDTypeIndication<>5 -- AND i.IdGMeter = 773957

-- GO 
--
--SELECT i.Display
--, i.IdGMeter
--, i.DateAdd
--, dbo.fgetIDPeriod(i.DateAdd)
--FROM Indication i 
--  WHERE i.IdGMeter = 773957

--SELECT * FROM Indication i WHERE i.IDIndication = 9869458
--DELETE FROM Indication WHERE IDIndication = 9786821

