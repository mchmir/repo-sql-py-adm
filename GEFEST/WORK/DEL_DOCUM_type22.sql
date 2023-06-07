
SELECT *
  FROM Document AS d
 WHERE d.DocumentDate = '2023-04-03'
   AND d.IDContract in (select idContract 
                          from Contract as c 
                         where c.Account in (3631047))
   AND d.IDTypeDocument = 22
   AND d.[DateAdd] is NULL;

-- IDDOCUMENT in (22154395,22154396)

SELECT *
      FROM PD AS pd
      WHERE pd.IDDocument in (23047404)


SELECT 
       g.IDGMeter,
       g.SerialNumber
  FROM GMeter AS g
 WHERE g.IDGMeter in (
                SELECT value
                FROM PD AS pd
                WHERE pd.IDDocument in (22279938)
                  AND IDTypePD = 7
                )

-------------------------------------------------------------------------
BEGIN TRY

  --Начало транзакции 
BEGIN TRANSACTION 
  --Пакет №1 Начало 

DELETE FROM PD
       OUTPUT DELETED.*
       --OUTPUT DELETED.* -- старые  
       --INSERTED.* -- новые
      WHERE IDDocument in (23047406);

DELETE FROM Document
      OUTPUT DELETED.*
      WHERE IDDocument in (23047406);


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