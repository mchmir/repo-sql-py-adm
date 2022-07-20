CREATE ROLE [Cashier] AUTHORIZATION [dbo]
GO

EXEC sp_addrolemember N'Cashier', N'kassir1'
GO

EXEC sp_addrolemember N'Cashier', N'osaponenko'
GO

EXEC sp_addrolemember N'Cashier', N'sganenko'
GO

EXEC sp_addrolemember N'Cashier', N'sneymysheva'
GO

EXEC sp_addrolemember N'Cashier', N'vmuhametshina'
GO