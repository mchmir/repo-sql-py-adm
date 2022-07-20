CREATE ROLE [db_accessadmin] AUTHORIZATION [dbo]
GO

EXEC sp_addrolemember N'db_accessadmin', N'epayuser'
GO