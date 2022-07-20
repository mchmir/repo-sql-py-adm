CREATE ROLE [AccManager] AUTHORIZATION [dbo]
GO

EXEC sp_addrolemember N'AccManager', N'dmirsalimova'
GO

EXEC sp_addrolemember N'AccManager', N'drodina'
GO

EXEC sp_addrolemember N'AccManager', N'nkomarova'
GO

EXEC sp_addrolemember N'AccManager', N'uluparevich'
GO