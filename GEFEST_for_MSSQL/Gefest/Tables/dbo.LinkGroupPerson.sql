CREATE TABLE [dbo].[LinkGroupPerson] (
  [IDLinkGroupPerson] [int] IDENTITY,
  [IDGroupPerson] [int] NULL,
  [IDPerson] [int] NULL,
  PRIMARY KEY CLUSTERED ([IDLinkGroupPerson])
)
ON [PRIMARY]
GO

ALTER TABLE [dbo].[LinkGroupPerson]
  ADD FOREIGN KEY ([IDGroupPerson]) REFERENCES [dbo].[GroupPerson] ([IDGroupPerson])
GO

ALTER TABLE [dbo].[LinkGroupPerson] WITH NOCHECK
  ADD CONSTRAINT [FK_LinkGroupPerson_Person] FOREIGN KEY ([IDPerson]) REFERENCES [dbo].[Person] ([IDPerson])
GO