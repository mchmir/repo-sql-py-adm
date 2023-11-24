USE master;
GO

-- Удаление базы данных, если она уже существует
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'saleTest')
BEGIN
    ALTER DATABASE saleTest SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE saleTest;
END
GO

-- Восстановление базы данных из резервной копии
RESTORE DATABASE saleTest
FROM DISK = 'D:\BackUp\backup_sale_14.11.2023\SALE.bak'
WITH 
    MOVE 'Sale_Data' TO 'D:\SQL_Data\RESERVTEST\saleTest.mdf',    -- Имя файла данных
    MOVE 'Sale_Log' TO 'D:\SQL_Data\RESERVTEST\saleTest_log.ldf', -- Имя файла журнала
    NOUNLOAD, 
    STATS = 5;
GO


--Чтобы узнать точные логические имена файлов данных и журналов, содержащихся в резервной копии
--RESTORE FILELISTONLY 
--FROM DISK = 'D:\BackUp\backup_sale_14.11.2023\SALE.bak';