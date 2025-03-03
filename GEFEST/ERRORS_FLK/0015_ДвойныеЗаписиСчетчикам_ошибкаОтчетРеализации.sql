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

declare @DEND DATETIME;
declare @IDPERIOD INT;
declare @YEAR INT;
declare @MONTH INT;

set @YEAR = 2025;
set @MONTH = 2;

set @IDPERIOD = DBO.FGETIDPERIODMY(@MONTH, @YEAR);
set @DEND = DBO.FGETDATEPERIOD(@IDPERIOD, 0);


select distinct C.ACCOUNT,
                C.IDCONTRACT,
                GM.IDGMETER,
                GM.SERIALNUMBER,
                DBO.FGETSTATUSPU(@DEND, GM.IDGMETER) as STATUSGMETER
into #TMPTAB0015
from CONTRACT as C with (nolock)
       inner join GOBJECT as G with (nolock) on G.IDCONTRACT = C.IDCONTRACT
       left join GMETER as GM with (nolock) on GM.IDGOBJECT = G.IDGOBJECT
  and DBO.FGETSTATUSPU(@DEND, GM.IDGMETER) = 1
       left join TYPEGMETER as TT with (nolock) on GM.IDTYPEGMETER = TT.IDTYPEGMETER
       left join DOCUMENT as D with (nolock) on D.IDCONTRACT = C.IDCONTRACT
  and D.IDPERIOD = @IDPERIOD
order by C.ACCOUNT

select *
from #TMPTAB0015 T;

select T.ACCOUNT             as ACCOUNT,
       T.IDCONTRACT,
       COUNT(T.SERIALNUMBER) as COUNTGMETER
from #TMPTAB0015 as T
where T.ACCOUNT not in (3322000, 2933000, 3282174, 1261065)
group by T.ACCOUNT, T.IDCONTRACT
having COUNT(T.SERIALNUMBER) > 1;


drop table #TMPTAB0015;

--set statistics profile off

