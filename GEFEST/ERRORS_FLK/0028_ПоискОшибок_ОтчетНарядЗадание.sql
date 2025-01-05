-- исправляем только где  1 и 2
-- где 2 и 1 для информации, является ошибкой с т.з. базы но на отчеты не влияет


declare @IDPERIOD as INT;
declare @DEND as DATETIME;
declare @MONTH as INT;
declare @YEAR as INT;

set @MONTH = 12;
set @YEAR = 2024;

set @IDPERIOD = DBO.FGETIDPERIODMY(@MONTH, @YEAR);
set @DEND = DBO.FGETDATEPERIOD(@IDPERIOD, 0);


select C.ACCOUNT,
       C.IDCONTRACT,
       G.IDGMETER,
       G.IDSTATUSGMETER,
       DBO.FGETSTATUSPU2(@DEND, G.IDGMETER)
from GMETER as G,
     GOBJECT as G1,
     CONTRACT as C
where G1.IDGOBJECT = G.IDGOBJECT
  and C.IDCONTRACT = G1.IDCONTRACT
  --AND g.IDGMeter =722547
  and G.IDSTATUSGMETER <> DBO.FGETSTATUSPU2(@DEND, G.IDGMETER)
  and DBO.FGETSTATUSPU2(@DEND, G.IDGMETER) <> 3
order by G.IDSTATUSGMETER;


-- Исправление подстановка IDGmeter и исправление Name = 2
/*

+-------+----------+--------+--------------+-+
|ACCOUNT|IDCONTRACT|IDGMETER|IDSTATUSGMETER| |
+-------+----------+--------+--------------+-+
|2831051|894553    |752252  |1             |2|
+-------+----------+--------+--------------+-+



*/

/*
select *
from OLDVALUES as OV
where OV.IDOBJECT = 752252   -- IDGmeter = idObject !!!
order by OV.DATEVALUES desc;

update OLDVALUES
set NAME = 2
where IDOLDVALUES in (1180492);

--SELECT * FROM GMeter g WHERE g.IDGMeter = 718821  778139 803778


select C.ACCOUNT
from CONTRACT as C
where C.IDCONTRACT = (select IDCONTRACT
                      from GOBJECT
                      where IDGOBJECT = 904658)
                       * /

