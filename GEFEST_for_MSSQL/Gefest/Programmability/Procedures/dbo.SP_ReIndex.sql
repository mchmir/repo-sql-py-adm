﻿SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
 CREATE PROCEDURE [dbo].[SP_ReIndex] (@DbId SMALLINT = NULL)
        AS
        BEGIN
        /*
                Процедура для массовой переиндексации БД.
        */
        --Запрещаем вывод количества строк
        --Для того чтобы выводилась только интересующая нас информация
        SET NOCOUNT ON; 

        --Табличная переменная для хранения названий объектов (индексов, таблиц) для обслуживания
        DECLARE @IndexTmpTable TABLE (Id INT IDENTITY(1,1) PRIMARY KEY,
                                                                  SchemaName SYSNAME, 
                                                                  TableName SYSNAME, 
                                                                  IndexName SYSNAME, 
                                                                  AvgFrag FLOAT);
        --Вспомогательные переменные
        DECLARE @RowNumber INT 
          SET @RowNumber = 1
        DECLARE @CntRows INT 
        DECLARE @CntReorganize INT
          SET @CntReorganize= 0
        DECLARE @CntRebuild INT 
          SET @CntRebuild= 0;
        DECLARE @SchemaName SYSNAME, @TableName SYSNAME, @IndexName SYSNAME, @AvgFrag FLOAT;
        DECLARE @Command VARCHAR(8000);

        --Идентификатор БД (можно передать во входящем параметре, по умолчанию текущая)
        SELECT @DbId = COALESCE(@DbId, DB_ID()); 

        --Определяем степень фрагментации индексов на основе системной табличной функции
        --sys.dm_db_index_physical_stats, а также название индексов и соответствующих таблиц
        INSERT INTO @IndexTmpTable
                SELECT Sch.name AS SchemaName,
                           Obj.name AS TableName,
                           Inx.name AS IndexName, 
                           AvgFrag.avg_fragmentation_in_percent AS Fragmentation 
                  FROM sys.dm_db_index_physical_stats (@DbId, NULL, NULL, NULL, NULL) AS AvgFrag
                  LEFT JOIN sys.indexes AS Inx ON AvgFrag.object_id = Inx.object_id AND AvgFrag.index_id = Inx.index_id
                  LEFT JOIN sys.objects AS Obj ON AvgFrag.object_id = Obj.object_id 
                  LEFT JOIN sys.schemas AS Sch ON Obj.schema_id = Sch.schema_id
                  WHERE AvgFrag.index_id > 0 
                        AND AvgFrag.avg_fragmentation_in_percent > 5 --5 - это минимальная степень фрагментации индекса

        --Количество строк для обработки
        SELECT @CntRows = COUNT(*)
        FROM @IndexTmpTable

        --Цикл обработки каждого индекса
        WHILE @RowNumber <= @CntRows
                BEGIN
                  --Получаем названия объектов, а также степень фрагментации текущего индекса
                  SELECT @SchemaName = SchemaName, 
                         @TableName = TableName, 
                         @IndexName = IndexName, 
                         @AvgFrag = AvgFrag
                  FROM @IndexTmpTable
                  WHERE Id = @RowNumber
                        
                  --Если степень фрагментации менее 30%, выполняем реорганизацию индекса
                  IF @AvgFrag < 30
                     BEGIN
                       --Формируем строку инструкции и выполняем ее
                       SELECT @Command = 'ALTER INDEX [' + @IndexName + '] ON ' + '[' + @SchemaName + ']' 
                                          + '.[' + @TableName + '] REORGANIZE';
                       EXEC (@Command);
                       SET @CntReorganize = @CntReorganize + 1; --Количество реорганизованных индексов
                      END 
                        
                  --Если степень фрагментации более 30%, выполняем перестроение индекса
                  IF @AvgFrag >= 30
                     BEGIN
                        --Формируем строку инструкции и выполняем ее
                        SELECT @Command = 'ALTER INDEX [' + @IndexName + '] ON ' + '[' + @SchemaName + ']' 
                                            + '.[' + @TableName + '] REBUILD';
                        EXEC (@Command);
                        SET @CntRebuild = @CntRebuild + 1; --Количество перестроенных индексов
                     END
                        
                   --Выводим служебную информацию о текущей операции
                   PRINT 'Выполнена инструкция ' + @Command;
                        
                   --Переходим к следующему индексу
                   SET @RowNumber = @RowNumber + 1
                END
                
                --Итог
                PRINT 'Всего обработано индексов: ' + CAST(@CntRows AS VARCHAR(10)) 
                        + ', Реорганизовано: ' + CAST(@CntReorganize AS VARCHAR(10)) 
                        + ', Перестроено: ' + CAST(@CntRebuild AS VARCHAR(10))
        END
GO