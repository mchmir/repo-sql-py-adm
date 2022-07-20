CREATE TABLE [dbo].[TypePC] (
  [IDTypePC] [int] IDENTITY,
  [IDTypeContract] [int] NULL,
  [Name] [varchar](50) NULL,
  PRIMARY KEY CLUSTERED ([IDTypePC])
)
ON [PRIMARY]
GO

ALTER TABLE [dbo].[TypePC] WITH NOCHECK
  ADD CONSTRAINT [FK__TypePC__IDTypeCo__03F0984C] FOREIGN KEY ([IDTypeContract]) REFERENCES [dbo].[TypeContract] ([IDTypeContract])
GO