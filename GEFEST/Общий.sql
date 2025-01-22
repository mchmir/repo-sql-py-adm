use GEFEST
go

declare @ACC as VARCHAR(50);
declare @IDCONTRACT as INT;
declare @IDPERIOD as INT;
declare @YEAR as INT;
declare @MONTH as INT;
declare @IDGOBJECT as INT;

set @ACC = '1921039';
set @YEAR = 2025;
set @MONTH = 1;


set @IDPERIOD = DBO.FGETIDPERIODMY(@MONTH, @YEAR);
set @IDCONTRACT = (select C.IDCONTRACT
                   from CONTRACT as C
                   where C.ACCOUNT = @ACC);
set @IDGOBJECT = (select G.IDGOBJECT
                  from GOBJECT as G
                  where G.IDCONTRACT = @IDCONTRACT);

--SELECT @idPeriod
--SELECT @idContract
--SELECT @idGObject

-- Запрос 1  ДОКУМЕНТЫ --
select *
from DOCUMENT as D,
     TYPEDOCUMENT as TD
where D.IDCONTRACT = @IDCONTRACT
  and D.IDPERIOD = @IDPERIOD
  and D.IDTYPEDOCUMENT = TD.IDTYPEDOCUMENT;

-- Запрос 2 ОПЕРАЦИИ --
select O.*,
       D.DOCUMENTNUMBER as [№ документа],
       TD.NAME          as [Тип документа],
       TYPOP.NAME       as [Тип операции]
from OPERATION as O,
     DOCUMENT as D,
     TYPEDOCUMENT as TD,
     TYPEOPERATION as TYPOP
where O.IDDOCUMENT = D.IDDOCUMENT
  and D.IDTYPEDOCUMENT = TD.IDTYPEDOCUMENT
  and O.IDTYPEOPERATION = TYPOP.IDTYPEOPERATION
  and D.IDCONTRACT = @IDCONTRACT
  and D.IDPERIOD = @IDPERIOD;

-- Запрос 3 BALANS Сальдо-- 
select B.IDBALANCE,
       B.IDACCOUNTING,
       B.IDPERIOD,
       B.IDCONTRACT,
       B.AMOUNTBALANCE as [Ост на счете],
       B.AMOUNTCHARGE  as [Остаток на нач мес],
       B.AMOUNTPAY     as [Оплачено],
       A.NAME          as [Счет]
from BALANCE as B,
     ACCOUNTING as A
where B.IDCONTRACT = @IDCONTRACT
  and B.IDPERIOD = @IDPERIOD
  and A.IDACCOUNTING = B.IDACCOUNTING;

-- Запрос 4 BALANSreal Сальдо без среднего-- 
select B.IDBALANCEREAL,
       B.IDACCOUNTING,
       B.IDPERIOD,
       B.IDCONTRACT,
       B.AMOUNTBALANCE as [Ост на счете],
       B.AMOUNTCHARGE  as [Остаток на нач мес],
       B.AMOUNTPAY     as [Оплачено],
       A.NAME          as [Счет]
from BALANCEREAL as B,
     ACCOUNTING as A
where B.IDCONTRACT = @IDCONTRACT
  and B.IDPERIOD = @IDPERIOD
  and A.IDACCOUNTING = B.IDACCOUNTING;

-- Запрос 5 Потребление --
select FU.*,
       TF.NAME
from FACTUSE as FU,
     TYPEFU as TF
where TF.IDTYPEFU = FU.IDTYPEFU
  and FU.IDPERIOD = @IDPERIOD
  and FU.IDGOBJECT = @IDGOBJECT;

-- Запрос 6 Показания --
select I.IDINDICATION,
       I.DISPLAY,
       I.DATEDISPLAY,
       TI.NAME,
       I.IDGMETER,
       G.IDSTATUSGMETER,
       SG.NAME,
       TG.NAME,
       G.SERIALNUMBER,
       G.DATEINSTALL,
       I.IDUSER
from INDICATION as I,
     GMETER as G,
     TYPEINDICATION as TI,
     TYPEGMETER as TG,
     STATUSGMETER as SG
where I.IDGMETER = G.IDGMETER
  and I.IDTYPEINDICATION = TI.IDTYPEINDICATION
  and TG.IDTYPEGMETER = G.IDTYPEGMETER
  and SG.IDSTATUSGMETER = G.IDSTATUSGMETER
  and I.IDGMETER in (select G.IDGMETER
                     from GMETER as G
                     where G.IDGOBJECT = @IDGOBJECT)
  and MONTH(I.DATEDISPLAY) = @MONTH
  and YEAR(I.DATEDISPLAY) = @YEAR;

-- Запрос 7 Общая информация 
select CONTRACT.ACCOUNT    as [Лицевой счет],
       GOBJECT.COUNTLIVES  as [Кол-во прожив.],
       PERSON.SURNAME      as ФАМИЛИЯ,
       PERSON.NAME         as ИМЯ,
       PERSON.PATRONIC     as ОТЧЕСТВО,
       PERSON.NSURNAME     as KZ_ФАМИЛИЯ,
       PERSON.NNAME        as KZ_ИМЯ,
       PERSON.NPATRONIC    as KZ_ОТЧЕСТВО,
       GRU.INVNUMBER       as [РУ_Инв.Номер],
       GRU.NAME            as ТИПГРУ,
       GRU.NOTE            as ЗАПИСЬГРУ,
       GOBJECT.NAME        as ИМЯОБЪЕКТА,
       STREET.NAME         as УЛИЦА,
       HOUSE.HOUSENUMBER   as ДОМ,
       ADDRESS.FLAT        as КВАРТИРА,
       TYPEGMETER.NAME     as [Тип счетчика],
       case GMETER.IDSTATUSGMETER
         when 1 then N'Подключен'
         when 2 then N'Отключен'
         end               as СТАТУС,
       GMETER.SERIALNUMBER as СЕРИЙНОМЕРСЧЕТЧИК,
       CONTRACT.IDCONTRACT,
       PERSON.ISJURIDICAL,
       GOBJECT.IDGOBJECT,
       GOBJECT.IDGRU,
       GMETER.IDGMETER
from DBO.GOBJECT
       inner join DBO.CONTRACT on GOBJECT.IDCONTRACT = CONTRACT.IDCONTRACT
       inner join DBO.GMETER on GMETER.IDGOBJECT = GOBJECT.IDGOBJECT
       inner join DBO.GRU on GOBJECT.IDGRU = GRU.IDGRU
       inner join DBO.PERSON on PERSON.IDPERSON = CONTRACT.IDPERSON
       inner join DBO.ADDRESS on PERSON.IDADDRESS = ADDRESS.IDADDRESS
       inner join DBO.STREET on ADDRESS.IDSTREET = STREET.IDSTREET
       inner join DBO.HOUSE on ADDRESS.IDHOUSE = HOUSE.IDHOUSE and HOUSE.IDSTREET = STREET.IDSTREET
       inner join DBO.TYPEGMETER on GMETER.IDTYPEGMETER = TYPEGMETER.IDTYPEGMETER
where CONTRACT.IDCONTRACT = @IDCONTRACT;



--SELECT * FROM Balance b WHERE b.IDBalance IN (21899713, 21899909)
--  SELECT * FROM BalanceReal br WHERE br.IDBalanceReal IN (21694757, 21693904)

--DELETE FROM Document WHERE IDDocument IN (20728482, 20728484)       

/*SELECT * FROM Balance b WHERE b.IDContract = 914482
 ORDER BY b.IDPeriod, b.IDAccounting

 SELECT * FROM BalanceReal b WHERE b.IDContract = 881318
 ORDER BY b.IDPeriod, b.IDAccounting    */