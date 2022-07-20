CREATE TABLE [dbo].[Batch] (
  [IDBatch] [int] IDENTITY,
  [IDTypePay] [int] NULL,
  [IDPeriod] [int] NULL,
  [IDDispatcher] [int] NULL,
  [IDCashier] [int] NULL,
  [Name] [varchar](500) NULL,
  [BatchCount] [int] NULL,
  [BatchAmount] [float] NULL,
  [BatchDate] [datetime] NULL,
  [IDTypeBatch] [int] NULL,
  [IDStatusBatch] [int] NULL,
  [NumberBatch] [varchar](20) NULL,
  [Note] [varchar](8000) NULL,
  CONSTRAINT [PK__Batch__1920BF5C] PRIMARY KEY CLUSTERED ([IDBatch])
)
ON [PRIMARY]
GO

ALTER TABLE [dbo].[Batch] WITH NOCHECK
  ADD CONSTRAINT [FK__Batch__ID_D__0B5CAFEA] FOREIGN KEY ([IDDispatcher]) REFERENCES [dbo].[Agent] ([IDAgent])
GO

ALTER TABLE [dbo].[Batch] WITH NOCHECK
  ADD CONSTRAINT [FK__Batch__IDPeriod__51300E55] FOREIGN KEY ([IDPeriod]) REFERENCES [dbo].[Period] ([IDPeriod])
GO

ALTER TABLE [dbo].[Batch] WITH NOCHECK
  ADD CONSTRAINT [FK__Batch__IDStatusB__4E53A1AA] FOREIGN KEY ([IDStatusBatch]) REFERENCES [dbo].[StatusBatch] ([IDStatusBatch])
GO

ALTER TABLE [dbo].[Batch] WITH NOCHECK
  ADD CONSTRAINT [FK__Batch__IDTypeBat__4F47C5E3] FOREIGN KEY ([IDTypeBatch]) REFERENCES [dbo].[TypeBatch] ([IDTypeBatch])
GO

ALTER TABLE [dbo].[Batch] WITH NOCHECK
  ADD CONSTRAINT [FK__Batch__IDTypePay__503BEA1C] FOREIGN KEY ([IDTypePay]) REFERENCES [dbo].[TypePay] ([IDTypePay])
GO