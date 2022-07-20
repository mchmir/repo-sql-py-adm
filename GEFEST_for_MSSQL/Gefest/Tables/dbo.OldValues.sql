CREATE TABLE [dbo].[OldValues] (
  [IdOldValues] [int] IDENTITY,
  [NameTable] [varchar](50) NOT NULL,
  [Name] [varchar](500) NOT NULL,
  [IdPeriod] [int] NULL,
  [DateValues] [datetime] NULL,
  [IdObject] [int] NOT NULL,
  [NameColumn] [varchar](50) NULL
)
ON [PRIMARY]
GO

CREATE CLUSTERED INDEX [_dta_index_OldValues_c_9_1188199283__K1]
  ON [dbo].[OldValues] ([IdOldValues])
  ON [PRIMARY]
GO

CREATE INDEX [IX_OldValues]
  ON [dbo].[OldValues] ([IdPeriod])
  ON [PRIMARY]
GO

CREATE INDEX [IX_OldValues_1]
  ON [dbo].[OldValues] ([IdObject])
  ON [PRIMARY]
GO

CREATE INDEX [IX_OldValues_2]
  ON [dbo].[OldValues] ([NameTable])
  ON [PRIMARY]
GO

CREATE INDEX [IX_OldValues_3]
  ON [dbo].[OldValues] ([NameColumn])
  ON [PRIMARY]
GO

CREATE INDEX [IX_OldValues_4]
  ON [dbo].[OldValues] ([NameTable], [NameColumn], [IdObject], [DateValues])
  ON [PRIMARY]
GO

CREATE STATISTICS [_dta_stat_1188199283_1_2_7_6]
  ON [dbo].[OldValues] ([IdOldValues], [NameTable], [NameColumn], [IdObject])
GO

CREATE STATISTICS [_dta_stat_1188199283_6_2_7_5_1]
  ON [dbo].[OldValues] ([IdObject], [NameTable], [NameColumn], [DateValues], [IdOldValues])
GO