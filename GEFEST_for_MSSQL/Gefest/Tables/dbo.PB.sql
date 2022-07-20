CREATE TABLE [dbo].[PB] (
  [IDPB] [int] IDENTITY,
  [IDTypePB] [int] NULL,
  [IDBatch] [int] NULL,
  [Value] [varchar](50) NULL,
  CONSTRAINT [PK__ParameterBatch__3E1D39E1] PRIMARY KEY CLUSTERED ([IDPB])
)
ON [PRIMARY]
GO

ALTER TABLE [dbo].[PB] WITH NOCHECK
  ADD CONSTRAINT [FK__Parameter__IDBat__44CA3770] FOREIGN KEY ([IDBatch]) REFERENCES [dbo].[Batch] ([IDBatch])
GO

ALTER TABLE [dbo].[PB]
  ADD CONSTRAINT [FK__Parameter__IDTyp__43D61337] FOREIGN KEY ([IDTypePB]) REFERENCES [dbo].[TypePB] ([IDTypePB])
GO