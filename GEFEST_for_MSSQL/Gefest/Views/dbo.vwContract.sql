SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
CREATE VIEW [dbo].[vwContract] 
AS SELECT
  c.IDContract
 ,p.IDPerson
 ,c.Account
 ,ad.IDAddress
 ,p.Surname
 ,p.Name
 ,p.Patronic
FROM dbo.Contract c
INNER JOIN dbo.Person p
  ON c.IDPerson = p.IDPerson
INNER JOIN dbo.Address ad
  ON p.IDAddress = ad.IDAddress
GO

EXEC sys.sp_addextendedproperty N'MS_Description', 'table Contract in format DB Gmeter', 'SCHEMA', N'dbo', 'VIEW', N'vwContract'
GO