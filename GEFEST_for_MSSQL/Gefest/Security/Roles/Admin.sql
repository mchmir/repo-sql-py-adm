CREATE ROLE [Admin] AUTHORIZATION [dbo]
GO

EXEC sp_addrolemember N'Admin', N'achernyaev'
GO

EXEC sp_addrolemember N'Admin', N'agrigoryev'
GO

EXEC sp_addrolemember N'Admin', N'MChmir'
GO

EXEC sp_addrolemember N'Admin', N'rsumlikina'
GO