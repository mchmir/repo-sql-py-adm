CREATE TABLE [dbo].[Enterprise] (
  [IDEnterprise] [int] IDENTITY,
  [IDBank] [int] NULL,
  [IDPerson] [int] NULL,
  [Account] [varchar](50) NULL,
  PRIMARY KEY CLUSTERED ([IDEnterprise])
)
ON [PRIMARY]
GO

ALTER TABLE [dbo].[Enterprise]
  ADD FOREIGN KEY ([IDBank]) REFERENCES [dbo].[Bank] ([IDBank])
GO

ALTER TABLE [dbo].[Enterprise] WITH NOCHECK
  ADD CONSTRAINT [FK__Enterpris__IDPer__2A164134] FOREIGN KEY ([IDPerson]) REFERENCES [dbo].[Person] ([IDPerson])
GO