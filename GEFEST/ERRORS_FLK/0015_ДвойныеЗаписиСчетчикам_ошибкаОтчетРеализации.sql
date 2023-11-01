/**
  Двойные записи по счетчикам
  Только на одном ЛС 3751001 два счетчика подключенных и два отключенных
  других ЛС быть не должно
 */

-- set statistics profile on

DECLARE @dEnd     DATETIME;
DECLARE @IdPeriod INT;
DECLARE @Year     INT;
DECLARE @Month    INT;

SET @Year = 2023;
SET @Month = 10;

set @IDPeriod = dbo.fGetIDPeriodMY(@Month, @Year);
set @dEnd     = dbo.fGetDatePeriod(@IdPeriod, 0);


SELECT DISTINCT
  --gm.idgmeter, gm.Serialnumber, COUNT( c.IDContract)
      c.Account,
      c.IdContract,
      gm.idgmeter,
      gm.Serialnumber,
      dbo.fGetStatusPU(@dEnd, gm.idgmeter) AS StatusGMeter
 INTO #tmpTab0015
 from contract as c with (nolock)
   inner join GObject as g  with (nolock) on g.IdContract = c.IdContract
   left join  GMeter  as gm with (nolock) on gm.IdGObject = g.IdGObject
     and dbo.fGetStatusPU(@dEnd, gm.idgmeter) = 1
   left join typegmeter as tt with (nolock) on gm.idtypegmeter = tt.idtypegmeter
   left join Document   as d  with (nolock) on d.idcontract    = c.idcontract
     and d.IdPeriod = @IdPeriod
--and (d.idtypedocument=1  or d.idtypedocument=3)
-----
--INNER JOIN Person p WITH (NOLOCK) ON p.IDPerson = c.IDPerson AND p.isJuridical = 0 -- только физические лица
--LEFT JOIN dbo.Address a WITH (NOLOCK)  ON a.IDAddress = g.IDAddress
--LEFT JOIN dbo.Street s WITH (NOLOCK) ON s.IDStreet = a.IDStreet
--LEFT JOIN dbo.House h WITH (NOLOCK) ON h.IDHouse = a.IDHouse
--group by   gm.idgmeter, gm.Serialnumber
--WHERE c.Account = 1933080
--ORDER BY  COUNT(c.IDContract) DESC
ORDER BY c.Account

SELECT *
  FROM #tmpTab0015 t;

SELECT
       t.Account AS Account,
       t.IDContract,
       COUNT(t.SerialNumber) as CountGMeter
  FROM #tmpTab0015 as t
 WHERE t.Account NOT IN (3322000, 2933000, 3282174, 1261065)
 GROUP BY t.Account, t.IDContract
HAVING COUNT(t.SerialNumber) > 1;


DROP TABLE #tmpTab0015;

--set statistics profile off

