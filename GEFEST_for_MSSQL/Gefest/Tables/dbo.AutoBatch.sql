CREATE TABLE [dbo].[AutoBatch] (
  [IDAutoBatch] [int] IDENTITY,
  [IDBatch] [int] NULL,
  [BatchDate] [datetime] NOT NULL,
  [BatchCount] [int] NOT NULL,
  [BatchAmount] [float] NOT NULL,
  [Path] [varchar](1000) NOT NULL,
  CONSTRAINT [PK_AutoBatch] PRIMARY KEY CLUSTERED ([IDAutoBatch])
)
ON [PRIMARY]
GO

ALTER TABLE [dbo].[AutoBatch] WITH NOCHECK
  ADD CONSTRAINT [FK_AutoBatch_Batch] FOREIGN KEY ([IDBatch]) REFERENCES [dbo].[Batch] ([IDBatch])
GO