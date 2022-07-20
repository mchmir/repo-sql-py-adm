CREATE ROLE [Manager] AUTHORIZATION [dbo]
GO

EXEC sp_addrolemember N'Manager', N'apolivianova'
GO

EXEC sp_addrolemember N'Manager', N'apolivyanova'
GO

EXEC sp_addrolemember N'Manager', N'dmirsalimova'
GO

EXEC sp_addrolemember N'Manager', N'ekarabelnikov'
GO