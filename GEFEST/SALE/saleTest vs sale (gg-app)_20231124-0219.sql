
SET LANGUAGE 'Russian'
SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT, XACT_ABORT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS OFF
GO

--
-- Резервное копирование базы данных sale
--
--
-- Создать папку для резервной копии базы данных
--
IF OBJECT_ID('xp_create_subdir') IS NOT NULL
  EXEC xp_create_subdir N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\Backup'
--
-- Создать резервную копию базы данных в файл с именем: <имя_базы_данных>_<гггг>_<мм>_<дд>_<чч>_<мин>.bak
--
DECLARE @filepath NVARCHAR(4000)
SET @filepath =
/*определить основную часть*/ N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\Backup\sale_' +
/*добавить дату*/ REPLACE(CONVERT(NVARCHAR(10), GETDATE(), 102), '.', '_') + '_' +
/*добавить время*/ REPLACE(CONVERT(NVARCHAR(5), GETDATE(), 108), ':', '_') + '.bak'
BACKUP DATABASE sale TO DISK = @filepath WITH INIT
GO

USE sale
GO

IF DB_NAME() <> N'sale' SET NOEXEC ON
GO


--
-- Начать транзакцию
--
BEGIN TRANSACTION

--
-- Выключение DML триггеров таблицы dbo.Kontragent
--
GO
DISABLE TRIGGER ALL ON dbo.Kontragent
GO

IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END

--
-- Удаление данных из таблицы dbo.Kontragent
--
DELETE dbo.Kontragent WHERE IDKontragent = 222
DELETE dbo.Kontragent WHERE IDKontragent = 223


--
-- Вставка данных в таблицу dbo.Kontragent
--
GO

IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
SET IDENTITY_INSERT dbo.Kontragent ON
GO

IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (14, 'ИП Вакарев С.А.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (32, 'ТОО "Алаугаз"')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (45, ' Баукенов')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (59, 'Барлубаев')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (120, 'ф.л. Ермаганбетов')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (145, 'Вагнер Е.П.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (146, 'Наурызбаев Д.К.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (148, 'ИП Вагнер П.Д.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (151, 'Нурсейтов М.К.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (152, 'Рожнов А.Г.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (153, 'Языкова О.Г.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (154, 'Семенчукова И.В.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (155, 'Нургалиева Р.Б.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (157, 'Султанова Н.О.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (158, 'Елеманова С.А.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (159, 'Поротиков А.Н.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (160, 'Маликов')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (161, 'Омарова С.Ш.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (162, 'Макенов Е.С.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (165, 'Ульман Д.С.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (166, 'Смагулов О.У.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (170, 'Аладинский А.А')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (171, 'Литвиц В.В.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (172, 'Баймурзинов С.Е.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (174, 'Сейтов К.К.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (176, 'Баукенов М.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (177, 'Жолудев В.П.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (178, 'Какимов А.Т.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (179, 'Байжанов С.К.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (180, 'Казанцев А.В')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (181, 'Балахонцев А.Н')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (182, 'Райцинов РВ')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (185, 'Кукса')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (186, 'Баукенова')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (187, 'Ашимов ТА')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (188, 'Елгащин')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (189, 'Аскеров')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (190, 'Макатова АЖ')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (191, 'Каскина')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (192, 'Жумабаев АС')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (196, 'Рашитов БС')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (197, 'Макажанова Р')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (198, 'Хитров')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (200, 'Исаев ДК')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (201, 'Карменов АК')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (202, 'Керейбаев А.')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (203, 'Дербисова')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (204, 'Ясманов')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (205, 'Башкиров')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (206, 'Мукажанова')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (207, 'Маутов')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (208, 'Жумабаева ЖК')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (209, 'Мустафина')
INSERT dbo.Kontragent(IDKontragent, Name) VALUES (210, 'Сухов В')
GO
SET IDENTITY_INSERT dbo.Kontragent OFF
GO

IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END

--
-- Задать Identity значение для dbo.Kontragent
--
DBCC CHECKIDENT('dbo.Kontragent', RESEED, 221)
GO

IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END

--
-- Включение DML триггеров таблицы dbo.Kontragent
--
GO
ENABLE TRIGGER ALL ON dbo.Kontragent
GO

IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END


--
-- Фиксировать транзакцию
--
IF @@TRANCOUNT>0 COMMIT TRANSACTION
SET NOEXEC OFF
GO