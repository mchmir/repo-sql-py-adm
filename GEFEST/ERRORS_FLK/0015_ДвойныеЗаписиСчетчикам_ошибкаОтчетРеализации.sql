/**
  Двойные записи по счетчикам
  Только на трех счетах ЛС
    3751001,
    1182001,
    3322000
  два счетчика подключенных и два отключенных
  других ЛС быть не должно
 */

-- set statistics profile on

DECLARE @dEnd     DATETIME;
DECLARE @IdPeriod INT;
DECLARE @Year     INT;
DECLARE @Month    INT;

SET @Year = 2024;
SET @Month = 5;

set @IDPeriod = dbo.fGetIDPeriodMY(@Month, @Year);
set @dEnd     = dbo.fGetDatePeriod(@IdPeriod, 0);


SELECT DISTINCT
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

