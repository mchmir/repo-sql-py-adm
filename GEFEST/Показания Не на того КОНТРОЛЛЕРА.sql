
USE Gefest
GO

SELECT * 
  FROM Indication AS i 
 WHERE i.DateDisplay = '25.10.2021' 
   AND i.IdUser      = 38
   AND i.IDAgent     = 149

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
    SET IDAgent = 127
        OUTPUT DELETED.IDAgent, INSERTED.IDAgent
       --OUTPUT DELETED.* -- старые  
       --INSERTED.* -- новые
  WHERE DateDisplay = '25.10.2021' 
    AND IdUser      = 38
    AND IDAgent     = 149

   
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

