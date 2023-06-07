SELECT 
       gph.PlombNumber1,
       LEFT(gph.PlombNumber1, 4) AS Seria, 
       gph.DatePlomb 
  FROM GMeterPlombHistory AS gph
 WHERE NOT gph.PlombNumber1 IS NULL

    UNION

SELECT 
       gph.PlombNumber2,
       LEFT(gph.PlombNumber2, 4) AS Seria, 
       gph.DatePlomb 
  FROM GMeterPlombHistory AS gph
 WHERE NOT gph.PlombNumber2 IS NULL
 ORDER BY 1
