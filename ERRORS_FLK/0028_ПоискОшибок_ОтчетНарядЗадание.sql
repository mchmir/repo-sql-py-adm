-- исправляем только где  1 и 2
-- где 2 и 1 для информации, является ошибкой с т.з. базы но на отчеты не влияет


declare @IDPeriod as INT
DECLARE @Month AS INT
DECLARE @Year AS INT

SET @Month = 12
SET @Year = 2021
SET @IDPeriod = (SELECT p.IDPeriod FROM Period p WHERE p.Year = @Year AND p.Month=@Month)


SELECT c.Account, c.IDContract,
 g.IDGMeter, g.IDStatusGMeter, dbo.fGetStatusPU2((SELECT p.DateEnd FROM Period p WHERE p.IDPeriod =  @IDPeriod), g.IDGMeter)
  FROM GMeter g, GObject g1, Contract c 
  WHERE  
   g1.IDGObject = g.IDGObject AND c.IDContract = g1.IDContract AND 
  --g.IDGMeter =722547 AND 
  g.IDStatusGMeter <> dbo.fGetStatusPU2((SELECT p.DateEnd FROM Period p WHERE p.IDPeriod =  @IDPeriod), g.IDGMeter)
  AND dbo.fGetStatusPU2((SELECT p.DateEnd FROM Period p WHERE p.IDPeriod =  @IDPeriod), g.IDGMeter) <>3
  ORDER BY g.IDStatusGMeter


-- Исправление подстановка IDGmeter и исправление Name = 2 
--SELECT * FROM OldValues ov WHERE ov.IdObject =790209 ORDER BY ov.DateValues DESC

--SELECT * FROM GMeter g WHERE g.IDGMeter = 774631