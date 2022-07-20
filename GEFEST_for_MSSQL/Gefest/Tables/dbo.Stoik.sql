CREATE TABLE [dbo].[Stoik] (
  [IDStoik] [int] IDENTITY,
  [Name] [varchar](50) NULL,
  [ColKranov] [int] NULL,
  [IDTypeArmatura] [int] NULL,
  [Podezd] [int] NULL,
  [IDHouse] [int] NULL,
  CONSTRAINT [PK_Stoik] PRIMARY KEY CLUSTERED ([IDStoik])
)
ON [PRIMARY]
GO

ALTER TABLE [dbo].[Stoik]
  ADD CONSTRAINT [FK_Stoik_House] FOREIGN KEY ([IDHouse]) REFERENCES [dbo].[House] ([IDHouse])
GO

ALTER TABLE [dbo].[Stoik]
  ADD CONSTRAINT [FK_Stoik_TypeArmatura] FOREIGN KEY ([IDTypeArmatura]) REFERENCES [dbo].[TypeArmatura] ([IDTypeArmatura])
GO