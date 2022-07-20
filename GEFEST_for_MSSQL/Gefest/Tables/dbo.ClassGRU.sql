CREATE TABLE [dbo].[ClassGRU] (
  [IDClassGRU] [int] IDENTITY,
  [IDParent] [int] NOT NULL,
  [Name] [varchar](50) NULL,
  [Memo] [varchar](100) NULL,
  PRIMARY KEY CLUSTERED ([IDClassGRU])
)
ON [PRIMARY]
GO