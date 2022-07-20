CREATE TABLE [dbo].[TypePlomb] (
  [IDTypePlomb] [int] IDENTITY,
  [NameTypePlomb] [varchar](50) NOT NULL,
  CONSTRAINT [PK_TypePlomb] PRIMARY KEY CLUSTERED ([IDTypePlomb])
)
ON [PRIMARY]
GO