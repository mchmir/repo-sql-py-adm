CREATE ROLE [Jurist] AUTHORIZATION [dbo]
GO

EXEC sp_addrolemember N'Jurist', N'egluhih'
GO

EXEC sp_addrolemember N'Jurist', N'jurcontract'
GO

EXEC sp_addrolemember N'Jurist', N'lkshanovskaya'
GO

EXEC sp_addrolemember N'Jurist', N'pmalkov'
GO