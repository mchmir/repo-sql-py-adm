-- Показания без потребления
--  SELECT *
--  FROM FactUse fu 
--  LEFT JOIN Operation o ON o.IDOperation = fu.IDOperation
--  WHERE fu.IDGObject = 877617
--
--  021
--


declare @IDPERIOD INT;
declare @YEAR INT;
declare @MONTH INT;

set @YEAR = 2025;
set @MONTH = 6;

set @IDPERIOD = DBO.FGETIDPERIODMY(@MONTH, @YEAR);

select I.IDINDICATION as IDINDICATION,
       I.DISPLAY      as [Показания],
       I.IDGMETER     as IDGMETER,
       I.DATEADD      as DATEADD,
       --dbo.fgetIDPeriod(i.DateAdd),
       FU.IDPERIOD    as IDPERIOD,
       FU.FACTAMOUNT  as [Начисления],
       C.ACCOUNT      as [Лицевой счет],
       GM.IDSTATUSGMETER,
       I.IDTYPEINDICATION
from INDICATION I
       join GMETER as GM on I.IDGMETER = GM.IDGMETER
       join GOBJECT as G on GM.IDGOBJECT = G.IDGOBJECT
       join CONTRACT as C on G.IDCONTRACT = C.IDCONTRACT
       left join FACTUSE as FU on I.IDINDICATION = FU.IDINDICATION
where ISNULL(FU.FACTAMOUNT, 0) = 0
  and ISNULL(FU.IDPERIOD, 0) = 0
  and DBO.FGETIDPERIOD(I.DATEADD) = @IDPERIOD
  and GM.IDSTATUSGMETER = 1
  and I.IDTYPEINDICATION <> 5;
-- AND i.IdGMeter = 773957

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

-- Оптимизировано 2 мая 2025

declare @IDPERIOD INT;
declare @YEAR INT;
declare @MONTH INT;

set @YEAR = 2025;
set @MONTH = 6;

set @IDPERIOD = DBO.FGETIDPERIODMY(@MONTH, @YEAR);

select I.IDINDICATION              as IDINDICATION,
       I.DISPLAY                   as [Показания],
       I.IDGMETER                  as IDGMETER,
       I.DATEADD                   as DATEADD,
       null                        as [Начисления],
       C.ACCOUNT                   as [Лицевой счет],
       GM.IDSTATUSGMETER,
       I.IDTYPEINDICATION
from INDICATION I
       join GMETER GM on I.IDGMETER = GM.IDGMETER
       join GOBJECT G on GM.IDGOBJECT = G.IDGOBJECT
       join CONTRACT C on G.IDCONTRACT = C.IDCONTRACT
where not exists (select 1
                  from FACTUSE FU
                  where FU.IDINDICATION = I.IDINDICATION
                    and (isnull(FU.FACTAMOUNT, 0) <> 0 or isnull(FU.IDPERIOD, 0) <> 0))
  and @IDPERIOD = (select top 1 IDPERIOD from PERIOD P where P.YEAR = year(I.DATEADD) and P.MONTH = month(I.DATEADD))
  and GM.IDSTATUSGMETER = 1
  and I.IDTYPEINDICATION <> 5;
