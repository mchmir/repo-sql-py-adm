USE GEFEST
GO

DECLARE @Acc        AS VARCHAR(50); 
DECLARE @idContract AS INT; 
DECLARE @idPeriod   AS INT;
DECLARE @Year       AS INT;
DECLARE @Month      AS INT;
DECLARE @idGObject  AS INT;

SET @Acc = '1661081';
SET @Year = 2023;
SET @Month = 7;


SET @idPeriod   = dbo.fGetIDPeriodMY(@Month, @Year);
SET @idContract = (SELECT c.IDContract FROM Contract AS c WHERE c.Account    = @Acc);
SET @idGObject  = (SELECT g.IDGObject  FROM GObject  AS g WHERE g.IDContract = @idContract);

--SELECT @idPeriod
--SELECT @idContract
--SELECT @idGObject

-- Запрос 1  ДОКУМЕНТЫ --
SELECT  *
  FROM Document     AS d, 
       TypeDocument AS td  
 WHERE d.IDContract = @idContract 
   AND d.IDPeriod   = @idPeriod 
   AND d.IDTypeDocument = td.IDTypeDocument;

-- Запрос 2 ОПЕРАЦИИ --
SELECT  
       o.*, 
       d.DocumentNumber AS [№ документа],
       td.Name          AS [Тип документа],
       typOp.Name       AS [Тип операции]
  FROM Operation     AS o, 
       Document      AS d, 
       TypeDocument  AS td,
       TypeOperation AS typOp
 WHERE o.IDDocument = d.IDDocument 
   AND d.IDTypeDocument  = td.IDTypeDocument 
   AND o.IdTypeOperation = typOp.IDTypeOperation
   AND d.IDContract = @idContract 
   AND d.IDPeriod = @idPeriod;     

-- Запрос 3 BALANS Сальдо-- 
SELECT 
       b.IDBalance, 
       b.IDAccounting, 
       b.IDPeriod, 
       b.IDContract, 
       b.AmountBalance AS [Ост на счете],
       b.AmountCharge  AS [Остаток на нач мес],
       b.AmountPay     AS [Оплачено],
       a.Name          AS [Счет] 
  FROM Balance    AS b,       Accounting AS a
 WHERE b.IDContract = @idContract 
   AND b.IDPeriod = @idPeriod 
   AND a.IDAccounting = b.IDAccounting;

-- Запрос 4 BALANSreal Сальдо без среднего-- 
SELECT 
       b.IDBalanceReal, 
       b.IDAccounting, 
       b.IDPeriod, 
       b.IDContract, 
       b.AmountBalance AS [Ост на счете],
       b.AmountCharge  AS [Остаток на нач мес],
       b.AmountPay     AS [Оплачено],
       a.Name          AS [Счет]
  FROM BalanceReal AS b,
       Accounting  AS a 
 WHERE b.IDContract = @idContract 
   AND b.IDPeriod = @idPeriod 
   AND a.IDAccounting = b.IDAccounting;

-- Запрос 5 Потребление --
SELECT 
       fu.*, 
       tf.Name 
  FROM FactUse AS fu,
       TypeFU  AS tf  
 WHERE tf.IDTypeFU = fu.IDTypeFU 
   AND fu.IDPeriod = @idPeriod 
   AND fu.IDGObject = @idGObject;

-- Запрос 6 Показания --
SELECT 
       i.IDIndication, 
       i.Display,
       i.DateDisplay,
       ti.Name, 
       i.IdGMeter, 
       g.IDStatusGMeter,
       sg.Name, 
       tg.Name, 
       g.SerialNumber, 
       g.DateInstall,
       i.IdUser
  FROM Indication     AS i,
       GMeter         AS g,
       TypeIndication AS ti, 
       TypeGMeter     AS tg, 
       StatusGMeter   AS sg
 WHERE i.IdGMeter = g.IDGMeter 
   AND i.IDTypeIndication = ti.IDTypeIndication  
   AND tg.IDTypeGMeter    = g.IDTypeGMeter 
   AND sg.IDStatusGMeter  = g.IDStatusGMeter 
   AND i.IdGMeter IN (SELECT g.IDGMeter 
                        FROM GMeter AS g 
                       WHERE g.IDGObject = @idGObject) 
   AND MONTH(i.DateDisplay) = @Month 
   AND YEAR(i.DateDisplay)  = @Year;

-- Запрос 7 Общая информация 
SELECT
       Contract.Account    AS [Лицевой счет],
       GObject.CountLives  AS [Кол-во прожив.],
       Person.Surname      AS Фамилия,
       Person.Name         AS Имя,
       Person.Patronic     AS Отчество,
       Person.NSurname     AS KZ_Фамилия,
       Person.NName        AS KZ_Имя,
       Person.NPatronic    AS KZ_Отчество,
       GRU.InvNumber       AS [РУ_Инв.Номер],
       GRU.Name            AS ТипГРУ,
       GRU.Note            AS ЗаписьГРУ,
       GObject.Name        AS ИмяОбъекта,
       Street.Name         AS Улица,
       House.HouseNumber   AS Дом,
       Address.Flat        AS Квартира,
       TypeGMeter.Name     AS [Тип счетчика],
       CASE GMeter.IDStatusGMeter
         WHEN 1 then N'Подключен'
         WHEN 2 THEN N'Отключен'
       END                 AS Статус,
       GMeter.SerialNumber AS СерийНомерСчетчик,
       Contract.IDContract,
       Person.isJuridical,
       GObject.IDGObject,
       GObject.IDGru,
       GMeter.IDGmeter
  FROM dbo.GObject
    INNER JOIN dbo.Contract   ON GObject.IDContract  = Contract.IDContract
    INNER JOIN dbo.GMeter     ON GMeter.IDGObject    = GObject.IDGObject
    INNER JOIN dbo.GRU        ON GObject.IDGru       = GRU.IDGru
    INNER JOIN dbo.Person     ON Person.IDPerson     = Contract.IDPerson
    INNER JOIN dbo.Address    ON Person.IDAddress    = Address.IDAddress
    INNER JOIN dbo.Street     ON Address.IDStreet    = Street.IDStreet
    INNER JOIN dbo.House      ON Address.IDHouse     = House.IDHouse AND House.IDStreet = Street.IDStreet
    INNER JOIN dbo.TypeGMeter ON GMeter.IDTypeGMeter = TypeGMeter.IDTypeGMeter
 WHERE Contract.IDContract =  @idContract;


 
--SELECT * FROM Balance b WHERE b.IDBalance IN (21899713, 21899909)
--  SELECT * FROM BalanceReal br WHERE br.IDBalanceReal IN (21694757, 21693904)
  
--DELETE FROM Document WHERE IDDocument IN (20728482, 20728484)       
 
 /*SELECT * FROM Balance b WHERE b.IDContract = 914482
  ORDER BY b.IDPeriod, b.IDAccounting 
  
  SELECT * FROM BalanceReal b WHERE b.IDContract = 881318
  ORDER BY b.IDPeriod, b.IDAccounting    */