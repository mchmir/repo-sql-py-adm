
USE Gefest
GO

SELECT * 
  FROM Indication AS i 
 WHERE i.DateDisplay = '10-05-2023'
   AND i.IdUser      = 62
   AND i.IDAgent     = 142

---- Контроллеры--------
SELECT * FROM Agent AS a

----- Пользователи -----------
select 
       s.uid, 
       s.status, 
       s.name, 
       s.createdate, 
       s.updatedate 
  from sys.sysusers AS s

------------------------------------
BEGIN TRY

  --Начало транзакции 
BEGIN TRANSACTION 
  --Пакет №1 Начало


 UPDATE Indication 
    SET IDAgent = 154
    -- select * from agent as a where idagent=154;
        OUTPUT DELETED.IDAgent, INSERTED.IDAgent
       --OUTPUT DELETED.* -- старые  
       --INSERTED.* -- новые
  WHERE DateDisplay = '10-05-2023'
    AND IdUser      = 62
    AND IDAgent     = 142

/*
 UPDATE Indication
    SET DATEDISPLAY='03-11-2024'
        OUTPUT DELETED.IDINDICATION, DELETED.DATEDISPLAY, INSERTED.DATEDISPLAY
       --OUTPUT DELETED.* -- старые
       --INSERTED.* -- новые
  WHERE DATEDISPLAY = '03-13-2024'
    AND IDUSER      = 71;
*/
-- 154
   
  --Пакет №1 Конец
END TRY 
            BEGIN CATCH
                --В случае непредвиденной ошибки 
                --Откат транзакции 
                ROLLBACK TRANSACTION 

                --Выводим сообщение об ошибке 
                SELECT ERROR_NUMBER()  AS [Номер ошибки], 
                       ERROR_MESSAGE() AS [Описание ошибки] 

                --Прекращаем выполнение инструкции 
                RETURN; 
            END CATCH --Если все хорошо. Сохраняем все изменения 
COMMIT TRANSACTION

