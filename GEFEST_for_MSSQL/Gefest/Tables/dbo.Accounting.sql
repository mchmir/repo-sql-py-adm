CREATE TABLE [dbo].[Accounting] (
  [IDAccounting] [int] IDENTITY,
  [Name] [varchar](50) NULL,
  [TypeAccounting] [int] NULL,
  [NName] [nvarchar](50) NULL,
  PRIMARY KEY CLUSTERED ([IDAccounting])
)
ON [PRIMARY]
GO