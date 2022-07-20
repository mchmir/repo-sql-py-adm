CREATE TABLE [dbo].[TypeInfringements] (
  [IDTypeInfringements] [int] IDENTITY,
  [Name] [varchar](8000) NULL,
  CONSTRAINT [PK_TypeInfringements] PRIMARY KEY CLUSTERED ([IDTypeInfringements])
)
ON [PRIMARY]
GO