CREATE TABLE [dbo].[Period] (
  [IDPeriod] [int] IDENTITY,
  [Year] [int] NULL,
  [Month] [int] NULL,
  [DateBegin] [datetime] NULL,
  [DateEnd] [datetime] NULL,
  CONSTRAINT [PK__Period__173876EA] PRIMARY KEY CLUSTERED ([IDPeriod])
)
ON [PRIMARY]
GO