DECLARE @idperiodprev INT;
DECLARE @idperiod INT;
DECLARE @month INT;
DECLARE @year INT;

DECLARE @idperiodprev2 INT;
DECLARE @idperiod2 INT;
DECLARE @month2 INT;
DECLARE @year2 INT;

DECLARE @fdate DATETIME;
DECLARE @sdate DATETIME;

DECLARE @fdatastr AS VARCHAR(10);
DECLARE @sdatastr AS VARCHAR(10);


-- данные на 1 июля 2022 года
SET @month = 7
SET @year  = 2022

SET @idperiod = (SELECT
                        IDPERIOD
                   FROM PERIOD AS P
                  WHERE P.MONTH = @month
                    AND P.YEAR  = @year);

SET @idperiodprev = dbo.fgetpredperiodvariable(@idperiod);

--- данные на 1 октября 2022 года
SET @month2 = 10
SET @year2  = 2022

SET @idperiod2 = (SELECT
                         IDPERIOD
                    FROM PERIOD AS P
                   WHERE P.MONTH = @month2
                     AND P.YEAR  = @year2);

SET @idperiodprev2 = dbo.fgetpredperiodvariable(@idperiod2);


SELECT
       @fdate = DATEBEGIN
  FROM PERIOD
 WHERE IDPERIOD = @idperiod;

SELECT
       @sdate = DATEBEGIN
  FROM PERIOD
 WHERE IDPERIOD = @idperiod2;

SET @fdatastr = (SELECT
                       RIGHT('0' + CONVERT(VARCHAR(2), DATEPART(DAY, @fdate)), 2) + '.' +
                       RIGHT('0' + CONVERT(VARCHAR(2), DATEPART(MONTH, @fdate)), 2) + '.' +
                       CAST(DATEPART(YEAR, @fdate) AS VARCHAR(4))
                 );

SET @sdatastr = (SELECT
                       RIGHT('0' + CONVERT(VARCHAR(2), DATEPART(DAY, @sdate)), 2) + '.' +
                       RIGHT('0' + CONVERT(VARCHAR(2), DATEPART(MONTH, @sdate)), 2) + '.' +
                       CAST(DATEPART(YEAR, @sdate) AS VARCHAR(4))
                 );


-- Запрос №1 Сумма Дебетовой задолженности на начало периода--- 

SELECT
      IDC,
      IDP,
      ACC,
      SUM(ROUND(AB, 2)) AS AA
 INTO #TMPDOLG
 FROM (SELECT
              C.IDCONTRACT IDC,
              C.IDPERSON IDP,
              C.ACCOUNT ACC,
              SUM(AMOUNTBALANCE) AB
         FROM BALANCEREAL B WITH (NOLOCK)
           INNER JOIN CONTRACT C WITH (NOLOCK) ON B.IDCONTRACT = C.IDCONTRACT
             AND IDPERIOD = @idperiodprev
         GROUP BY C.ACCOUNT, C.IDCONTRACT, C.IDPERSON) AS QQ
 WHERE AB < 0
 GROUP BY QQ.IDC,
           QQ.ACC,
           QQ.IDP;

SELECT
      IDC,
      IDP,
      ACC,
      SUM(ROUND(AB, 2)) AA
 INTO #TMPDOLGDEC
 FROM (SELECT
              C.IDCONTRACT IDC,
              C.IDPERSON IDP,
              C.ACCOUNT ACC,
              SUM(AMOUNTBALANCE) AS AB
         FROM BALANCEREAL B WITH (NOLOCK)
           INNER JOIN CONTRACT C WITH (NOLOCK) ON B.IDCONTRACT = C.IDCONTRACT
             AND IDPERIOD = @idperiodprev2
        GROUP BY C.ACCOUNT, C.IDCONTRACT, C.IDPERSON) AS QQ
  WHERE AB < 0
  GROUP BY QQ.IDC,
           QQ.ACC,
           QQ.IDP;

-- Задолжники (Свернутое сальдо без начисления по среднему)
/*SELECT
      D.ACC,
      ISNULL(P.SURNAME, '') + ' ' + ISNULL(P.NAME, '') + ' ' + ISNULL(P.PATRONIC, '') AS [ФИО],
      D.AA
 FROM #TMPDOLG AS D
   JOIN PERSON P ON P.IDPERSON = D.IDP
WHERE D.IDC NOT IN (SELECT
                           D.IDCONTRACT
                      FROM DOCUMENT D
                     WHERE D.IDTYPEDOCUMENT = 1
                       AND D.IDPERIOD IN (@idperiod, @idperiod + 1, @idperiod + 2)
                    )
ORDER BY D.AA */

-- Задолжники (Свернутое сальдо без начисления по среднему) 
DECLARE @str VARCHAR(2500)
SET @str = '
SELECT
       d.ACC AS [Лицевой счет],
       ISNULL(p.Surname,'''') + '' '' + ISNULL(p.Name,'''') +'' '' + ISNULL(p.Patronic,'''') AS [ФИО],
       d.AA  AS [на ' + @fdatastr + '],
       dd.AA AS [на ' + @sdatastr + ']
  FROM #tmpDolg AS d 
    JOIN Person p ON p.IDPerson = d.IDP 
    LEFT JOIN #tmpDolgDec AS dd ON d.IDC = dd.IDC
 WHERE d.IDC NOT IN (SELECT d.IDContract 
                       FROM Document AS d 
                      WHERE d.IDTypeDocument = 1 AND d.IDPeriod IN (' +
                                                                      CONVERT(VARCHAR(10), @idperiod) + ',' +
                                                                      CAST(@idperiod + 1 AS VARCHAR(10)) + ',' +
                                                                      CAST(@idperiod + 2 AS VARCHAR(10)) + ')) ' +
'ORDER BY d.AA ASC, dd.AA ASC '

--SELECT @str

EXECUTE (@str)

--SELECT * FROM #tmpDolgDec dd


DROP TABLE #TMPDOLG
DROP TABLE #TMPDOLGDEC
