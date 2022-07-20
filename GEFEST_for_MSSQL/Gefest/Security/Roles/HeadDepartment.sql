CREATE ROLE [HeadDepartment] AUTHORIZATION [dbo]
GO

EXEC sp_addrolemember N'HeadDepartment', N'eolenina'
GO

EXEC sp_addrolemember N'HeadDepartment', N'nkomarova'
GO

EXEC sp_addrolemember N'HeadDepartment', N'ukupriyanova'
GO

EXEC sp_addrolemember N'HeadDepartment', N'ushatalova'
GO