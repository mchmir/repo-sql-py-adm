/*
Проблема:
1. Мы загрузили транспортный файл по распломбировке приборов учета за 19 декабря.
2. Далее мы проверили счетчики и провели транспортный файл по опломбировке за 19 декабря.
3. Сегодня, вместо загрузки файла по распломбировке за 22 декабря, мы сновы загрузли файл по распломбировке  от 19 декабря.

В итоге:
В истории пломб мы не видим вторую загрузку за 19 декабря, так как логика программы сейчас, просто перезаписала данные.
В истории на лицевом счете, есть финансовые операции и документы по услугам опломбировки, но пломбы сняты.


На основании логики работы, у меня только один вариант исправления:

- необходимо удалить все документы и финансовые операции по опломбировке за 19 декабря;
- таким образом актуальными останутся данные по Распломбировке;
- а потом снова загрузим транспортник по Опломбировке;
- Пломбы должны обновиться.

*/



BEGIN TRANSACTION;

-- 1. НАСТРОЙКИ (ПРОВЕРЬТЕ ЭТИ ДАННЫЕ!)
DECLARE @TargetDate DATE = '2025-12-19'; -- Дата документа из файла
DECLARE @OperDate DATE = '2025-12-22';   -- Дата, когда нажали "Сохранить" (фактическая дата создания записи)
DECLARE @IdUser INT = 29;                -- ID ПОЛЬЗОВАТЕЛЯ (убеждаемся, что он верный)

-- Таблица для хранения ID документов
DECLARE @DocsToDelete TABLE (IDDocument INT, IDContract INT, IDPeriod INT, Account VARCHAR(50), Amount MONEY);

-- 2. ПОИСК ДОКУМЕНТОВ
-- Ищем документы опломбировки, где на счетчиках НЕТ пломб
INSERT INTO @DocsToDelete (IDDocument, IDContract, IDPeriod, Account, Amount)
SELECT
    d.IDDocument,
    d.IDContract,
    d.IDPeriod,
    ct.Account,
    d.DocumentAmount
FROM Document d
JOIN Contract ct ON d.IDContract = ct.IDContract
JOIN Operation op ON d.IDDocument = op.IDDocument
WHERE d.IDTypeDocument = 24 -- Опломбировка
  AND CAST(d.DocumentDate AS DATE) = @TargetDate
  AND CAST(d.DateAdd AS DATE) = @OperDate
  AND d.IdUser = @IdUser
  AND EXISTS (
      -- Проверяем, что у договора есть счетчик БЕЗ пломб (стертых распломбировкой)
      SELECT 1
      FROM GObject gob
      JOIN GMeter gm ON gob.IDGObject = gm.IDGObject
      WHERE gob.IDContract = d.IDContract
      AND (ISNULL(gm.PlombNumber1, '') = '' AND ISNULL(gm.PlombNumber2, '') = '')
  );

-- 3. ВЫВОД РЕЗУЛЬТАТА (ИСПРАВЛЕНО)
DECLARE @CountDoc INT;
SELECT @CountDoc = COUNT(*) FROM @DocsToDelete;

SELECT * FROM @DocsToDelete; -- Показываем таблицу
PRINT 'Найдено документов для удаления: ' + CAST(@CountDoc AS VARCHAR);

-- ==========================================================================
-- БЛОК УДАЛЕНИЯ (Раскомментируйте код между /* и */, если список верный)
-- ==========================================================================

/*
BEGIN TRY
    -- 1. Удаляем связанные записи из FactUse
    DELETE FROM FactUse
    WHERE IDDocument IN (SELECT IDDocument FROM @DocsToDelete);

    -- 2. Удаляем Операции (Operation)
    DELETE FROM Operation
    WHERE IDDocument IN (SELECT IDDocument FROM @DocsToDelete);

    -- 3. Удаляем Детали платежа (PD)
    DELETE FROM PD
    WHERE IDDocument IN (SELECT IDDocument FROM @DocsToDelete);

    -- 4. Удаляем сами Документы (Document)
    -- Теперь это сработает, так как мы удалили хвосты в FactUse, Operation и PD
    DELETE FROM Document
    WHERE IDDocument IN (SELECT IDDocument FROM @DocsToDelete);

    -- 5. Пересчет баланса
    DECLARE @CurContract INT, @CurPeriod INT;
    DECLARE cur CURSOR FOR SELECT IDContract, IDPeriod FROM @DocsToDelete;

    OPEN cur;
    FETCH NEXT FROM cur INTO @CurContract, @CurPeriod;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        EXEC spRecalcBalances @CurContract, @CurPeriod;
        FETCH NEXT FROM cur INTO @CurContract, @CurPeriod;
    END;

    CLOSE cur;
    DEALLOCATE cur;

    PRINT 'УСПЕХ: Документы и записи FactUse удалены, баланс пересчитан.';
END TRY
BEGIN CATCH
    PRINT 'ОШИБКА: ' + ERROR_MESSAGE();
    ROLLBACK TRANSACTION;
    RETURN;
END CATCH;
*/

-- ВНИМАНИЕ: Если мы раскомментировали блок выше, замените ROLLBACK на COMMIT
ROLLBACK TRANSACTION; -- По умолчанию ничего не удаляет
--COMMIT TRANSACTION; -- <--- ВКЛЮЧИТЬ ЭТО ДЛЯ СОХРАНЕНИЯ