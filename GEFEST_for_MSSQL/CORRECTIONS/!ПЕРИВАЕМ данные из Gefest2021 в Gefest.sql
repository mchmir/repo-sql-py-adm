-----   ЗАПРОС  ТОЛЬКО ИЗ БД  Gefest2021 ----------------
----- Переливаем данные по документам тип=17 "Отключение ОУ"
---------- Добавление записей по Лицевому счету -------------------
--DECLARE @Account INT;
--
--SET @Account = 1861098
--
--SET IDENTITY_INSERT Gefest.dbo.Document ON;
--
--INSERT INTO Gefest.dbo.Document(IDDocument, IDContract, IDPeriod, IDBatch, IDTypeDocument, DocumentNumber, DocumentDate, DocumentAmount, IDGru, IdUser, DateAdd, IdModify, DateModify, Note, TerminalData)
--SELECT * FROM Document d  
--  WHERE 
--      d.IDContract IN (SELECT c.IDContract  FROM Contract c WHERE c.Account = @Account) 
--      AND d.IDTypeDocument = 17;
--
--SET IDENTITY_INSERT Gefest.dbo.Document OFF;
--
--
--INSERT INTO Gefest.dbo.PD(IDTypePD, IDDocument, Value)
-- SELECT IDTypePD, IDDocument, Value FROM PD 
--  WHERE IDDocument IN (SELECT d.IDDocument FROM Document d
--                         WHERE d.IDContract IN (SELECT c.IDContract  
--                                                 FROM Contract c 
--                                                  WHERE c.Account = @Account) AND d.IDTypeDocument = 17);



SET IDENTITY_INSERT Gefest.dbo.Document ON;

INSERT INTO Gefest.dbo.Document(IDDocument, IDContract, IDPeriod, IDBatch, IDTypeDocument, DocumentNumber, DocumentDate, DocumentAmount, IDGru, IdUser, DateAdd, IdModify, DateModify, Note, TerminalData)
SELECT * 
 FROM Document d  
  WHERE  d.IDTypeDocument = 17 AND  d.idcontract IN (906560, 912260);

SET IDENTITY_INSERT Gefest.dbo.Document OFF;

PRINT 'Документы перелиты.............'

INSERT INTO Gefest.dbo.PD(IDTypePD, IDDocument, Value)
 SELECT IDTypePD, IDDocument, Value 
  FROM PD 
    WHERE IDDocument IN (SELECT d.IDDocument FROM Document d  
                            WHERE  d.IDTypeDocument = 17 AND d.idContract IN (906560, 912260)) ;

