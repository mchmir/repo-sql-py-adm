CREATE TABLE [dbo].[Stavka] (
  [IDStavka] [int] IDENTITY,
  [Name] [float] NULL,
  [Date] [datetime] NULL,
  CONSTRAINT [PK_Stavka] PRIMARY KEY CLUSTERED ([IDStavka])
)
ON [PRIMARY]
GO