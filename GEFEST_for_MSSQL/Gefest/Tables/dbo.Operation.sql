CREATE TABLE [dbo].[Operation] (
  [IDOperation] [int] IDENTITY,
  [DateOperation] [datetime] NULL,
  [AmountOperation] [float] NULL,
  [NumberOperation] [int] NULL,
  [IDBalance] [int] NOT NULL,
  [IDDocument] [int] NOT NULL,
  [IdTypeOperation] [int] NOT NULL,
  CONSTRAINT [PK_Operation] PRIMARY KEY CLUSTERED ([IDOperation])
)
ON [PRIMARY]
GO

CREATE INDEX [_dta_index_Operation_7_885578193__K5_K1_3_4]
  ON [dbo].[Operation] ([IDBalance], [IDOperation])
  INCLUDE ([AmountOperation], [NumberOperation])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_Operation_8_885578193__K6_K1_3_4_7]
  ON [dbo].[Operation] ([IDDocument], [IDOperation])
  INCLUDE ([AmountOperation], [NumberOperation], [IdTypeOperation])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_Operation_9_885578193__K1_K6]
  ON [dbo].[Operation] ([IDOperation], [IDDocument])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_Operation_9_885578193__K5_K1_K7_3]
  ON [dbo].[Operation] ([IDBalance], [IDOperation], [IdTypeOperation])
  INCLUDE ([AmountOperation])
  ON [PRIMARY]
GO

CREATE INDEX [IX_Operation]
  ON [dbo].[Operation] ([IDDocument])
  ON [PRIMARY]
GO

CREATE INDEX [IX_Operation_1]
  ON [dbo].[Operation] ([IDBalance])
  ON [PRIMARY]
GO

CREATE INDEX [IX_Operation_2]
  ON [dbo].[Operation] ([NumberOperation])
  ON [PRIMARY]
GO

CREATE STATISTICS [_dta_stat_885578193_1_5_7]
  ON [dbo].[Operation] ([IDOperation], [IDBalance], [IdTypeOperation])
GO

CREATE STATISTICS [_dta_stat_885578193_7_5]
  ON [dbo].[Operation] ([IdTypeOperation], [IDBalance])
GO

ALTER TABLE [dbo].[Operation]
  ADD CONSTRAINT [FK_Operation_Balance] FOREIGN KEY ([IDBalance]) REFERENCES [dbo].[Balance] ([IDBalance])
GO

ALTER TABLE [dbo].[Operation] WITH NOCHECK
  ADD CONSTRAINT [FK_Operation_Document] FOREIGN KEY ([IDDocument]) REFERENCES [dbo].[Document] ([IDDocument])
GO

ALTER TABLE [dbo].[Operation]
  ADD CONSTRAINT [FK_Operation_TypeOperation] FOREIGN KEY ([IdTypeOperation]) REFERENCES [dbo].[TypeOperation] ([IDTypeOperation])
GO