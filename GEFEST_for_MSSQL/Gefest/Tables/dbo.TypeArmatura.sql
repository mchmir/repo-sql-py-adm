CREATE TABLE [dbo].[TypeArmatura] (
  [IDTypeArmatura] [int] IDENTITY,
  [Name] [varchar](50) NULL,
  CONSTRAINT [PK_TypeArmatura] PRIMARY KEY CLUSTERED ([IDTypeArmatura])
)
ON [PRIMARY]
GO