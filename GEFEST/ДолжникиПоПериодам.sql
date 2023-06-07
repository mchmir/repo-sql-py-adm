declare @IDPERIODPREV INT;
declare @IDPERIOD INT;
declare @MONTH INT;
declare @YEAR INT;

declare @IDPERIODPREV2 INT;
declare @IDPERIOD2 INT;
declare @MONTH2 INT;
declare @YEAR2 INT;

declare @FDATE DATETIME;
declare @SDATE DATETIME;

declare @FDATASTR as VARCHAR(10);
declare @SDATASTR as VARCHAR(10);


-- данные на 1 августа 2022 года
set @MONTH = 8
set @YEAR = 2022

set @IDPERIOD = (select
    IDPERIOD
  from PERIOD as P
  where P.MONTH = @MONTH
    and P.YEAR = @YEAR);

set @IDPERIODPREV = DBO.FGETPREDPERIODVARIABLE(@IDPERIOD);

--- данные на 1 ноября 2022 года
set @MONTH2 = 11
set @YEAR2 = 2022

set @IDPERIOD2 = (select
    IDPERIOD
  from PERIOD P
  where P.MONTH = @MONTH2
    and P.YEAR = @YEAR2);

set @IDPERIODPREV2 = DBO.FGETPREDPERIODVARIABLE(@IDPERIOD2);


select
  @FDATE = DATEBEGIN
 from PERIOD
where IDPERIOD = @IDPERIOD;

select
  @SDATE = DATEBEGIN
from PERIOD
where IDPERIOD = @IDPERIOD2;

set @FDATASTR = (select
    right('0' + convert(VARCHAR(2), datepart(day, @FDATE)), 2) + '.' +
    right('0' + convert(VARCHAR(2), datepart(month, @FDATE)), 2) + '.' +
    cast(datepart(year, @FDATE) as VARCHAR(4)));

set @SDATASTR = (select
    right('0' + convert(VARCHAR(2), datepart(day, @SDATE)), 2) + '.' +
    right('0' + convert(VARCHAR(2), datepart(month, @SDATE)), 2) + '.' +
    cast(datepart(year, @SDATE) as VARCHAR(4)));


-- Запрос №1 Сумма Дебетовой задолженности на начало периода--- 

select
  IDC,
  IDP,
  ACC,
  sum(round(AB, 2)) as AA
into #TMPDOLG
from (select
    C.IDCONTRACT       IDC,
    C.IDPERSON         IDP,
    C.ACCOUNT          ACC,
    sum(AMOUNTBALANCE) AB
  from BALANCEREAL B with (nolock)
  inner join CONTRACT C with (nolock) on B.IDCONTRACT = C.IDCONTRACT
      and IDPERIOD = @IDPERIODPREV
    group by C.ACCOUNT,
             C.IDCONTRACT,
             C.IDPERSON) as QQ
where AB < 0
  group by QQ.IDC,
           QQ.ACC,
           QQ.IDP;

select
  IDC,
  IDP,
  ACC,
  sum(round(AB, 2)) AA
into #TMPDOLGDEC
from (select
    C.IDCONTRACT       IDC,
    C.IDPERSON         IDP,
    C.ACCOUNT          ACC,
    sum(AMOUNTBALANCE) as AB
  from BALANCEREAL B with (nolock)
  inner join CONTRACT C with (nolock) on B.IDCONTRACT = C.IDCONTRACT
      and IDPERIOD = @IDPERIODPREV2
    group by C.ACCOUNT,
             C.IDCONTRACT,
             C.IDPERSON) as QQ
where AB < 0
  group by QQ.IDC,
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
declare @STR VARCHAR(2500)
set @STR = '
SELECT
       d.ACC AS [Лицевой счет],
       ISNULL(p.Surname,'''') + '' '' + ISNULL(p.Name,'''') +'' '' + ISNULL(p.Patronic,'''') AS [ФИО],
       d.AA  AS [на ' + @FDATASTR + '],
       dd.AA AS [на ' + @SDATASTR + ']
  FROM #tmpDolg AS d 
    JOIN Person p ON p.IDPerson = d.IDP 
    LEFT JOIN #tmpDolgDec AS dd ON d.IDC = dd.IDC
 WHERE d.IDC NOT IN (SELECT d.IDContract 
                       FROM Document AS d 
                      WHERE d.IDTypeDocument = 1 AND d.IDPeriod IN (' +
convert(VARCHAR(10), @IDPERIOD) + ',' +
cast(@IDPERIOD + 1 as VARCHAR(10)) + ',' +
cast(@IDPERIOD + 2 as VARCHAR(10)) + ')) ' +
'ORDER BY d.AA ASC, dd.AA ASC '

--SELECT @str

execute (@STR)

--SELECT * FROM #tmpDolgDec dd


drop table #TMPDOLG
drop table #TMPDOLGDEC