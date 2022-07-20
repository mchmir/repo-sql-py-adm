CREATE TABLE [dbo].[PG] (
  [IdPG] [int] IDENTITY,
  [IdTypePG] [int] NOT NULL,
  [IdGRU] [int] NOT NULL,
  [IdPeriod] [int] NULL,
  [Value] [varchar](50) NULL,
  [Norma] [varchar](50) NULL,
  [IsDistr] [bit] NULL
)
ON [PRIMARY]
GO

CREATE INDEX [_dta_index_PG_9_709577566__K2_K3_K4_6]
  ON [dbo].[PG] ([IdTypePG], [IdGRU], [IdPeriod])
  INCLUDE ([Norma])
  ON [PRIMARY]
GO

CREATE INDEX [IX_PG]
  ON [dbo].[PG] ([IdGRU], [IdTypePG], [IdPeriod])
  ON [PRIMARY]
GO

CREATE INDEX [IX_PG_1]
  ON [dbo].[PG] ([IdGRU])
  ON [PRIMARY]
GO

CREATE INDEX [IX_PG_2]
  ON [dbo].[PG] ([IdPeriod])
  ON [PRIMARY]
GO

CREATE INDEX [IX_PG_3]
  ON [dbo].[PG] ([IdPG])
  ON [PRIMARY]
GO

CREATE INDEX [IX_PG_4]
  ON [dbo].[PG] ([IdTypePG])
  ON [PRIMARY]
GO

ALTER TABLE [dbo].[PG] WITH NOCHECK
  ADD CONSTRAINT [FK_PG_GRU] FOREIGN KEY ([IdGRU]) REFERENCES [dbo].[GRU] ([IDGru])
GO

ALTER TABLE [dbo].[PG] WITH NOCHECK
  ADD CONSTRAINT [FK_PG_Period] FOREIGN KEY ([IdPeriod]) REFERENCES [dbo].[Period] ([IDPeriod])
GO

ALTER TABLE [dbo].[PG]
  ADD CONSTRAINT [FK_PG_TypePG] FOREIGN KEY ([IdTypePG]) REFERENCES [dbo].[TypePG] ([IdTypePG])
GO