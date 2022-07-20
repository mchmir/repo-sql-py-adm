CREATE ROLE [MainKontroler] AUTHORIZATION [dbo]
GO

EXEC sp_addrolemember N'MainKontroler', N'korneva'
GO

EXEC sp_addrolemember N'MainKontroler', N'nleyman'
GO

EXEC sp_addrolemember N'MainKontroler', N'nnazarenko'
GO