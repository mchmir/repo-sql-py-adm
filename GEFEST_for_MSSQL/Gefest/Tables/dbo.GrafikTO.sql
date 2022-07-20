CREATE TABLE [dbo].[GrafikTO] (
  [IDGrafikTO] [int] IDENTITY,
  [IDContract] [int] NOT NULL,
  [Month] [int] NOT NULL,
  [Year] [int] NOT NULL,
  [Amount] [float] NOT NULL,
  CONSTRAINT [PK_GrafikTO] PRIMARY KEY CLUSTERED ([IDGrafikTO])
)
ON [PRIMARY]
GO