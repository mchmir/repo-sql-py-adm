CREATE TABLE [dbo].[FactUse] (
  [IDFactUse] [int] IDENTITY,
  [IDPeriod] [int] NOT NULL,
  [FactAmount] [float] NULL,
  [IDIndication] [int] NULL,
  [IDGObject] [int] NOT NULL,
  [IDDocument] [int] NULL,
  [IDTypeFU] [int] NULL,
  [IDOperation] [int] NULL,
  CONSTRAINT [PK_FactUse] PRIMARY KEY CLUSTERED ([IDFactUse])
)
ON [PRIMARY]
GO

CREATE INDEX [_dta_index_FactUse_9_68195293__K2_3_4_8]
  ON [dbo].[FactUse] ([IDPeriod])
  INCLUDE ([FactAmount], [IDIndication], [IDOperation])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_FactUse_9_68195293__K2_K1_K4]
  ON [dbo].[FactUse] ([IDPeriod], [IDFactUse], [IDIndication])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_FactUse_9_68195293__K2_K4_3]
  ON [dbo].[FactUse] ([IDPeriod], [IDIndication])
  INCLUDE ([FactAmount])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_FactUse_9_68195293__K2_K6_K1_3_7]
  ON [dbo].[FactUse] ([IDPeriod], [IDDocument], [IDFactUse])
  INCLUDE ([FactAmount], [IDTypeFU])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_FactUse_9_68195293__K7_K6_3]
  ON [dbo].[FactUse] ([IDTypeFU], [IDDocument])
  INCLUDE ([FactAmount])
  ON [PRIMARY]
GO

CREATE INDEX [IX_FactUse]
  ON [dbo].[FactUse] ([IDGObject], [IDPeriod])
  ON [PRIMARY]
GO

CREATE INDEX [IX_FactUse_1]
  ON [dbo].[FactUse] ([IDIndication], [IDPeriod])
  ON [PRIMARY]
GO

CREATE INDEX [IX_FactUse_2]
  ON [dbo].[FactUse] ([IDPeriod])
  ON [PRIMARY]
GO

CREATE INDEX [IX_FactUse_3]
  ON [dbo].[FactUse] ([IDOperation])
  ON [PRIMARY]
GO

CREATE STATISTICS [_dta_stat_68195293_1_2_4]
  ON [dbo].[FactUse] ([IDFactUse], [IDPeriod], [IDIndication])
GO

CREATE STATISTICS [_dta_stat_68195293_1_2_6]
  ON [dbo].[FactUse] ([IDFactUse], [IDPeriod], [IDDocument])
GO

CREATE STATISTICS [_dta_stat_68195293_1_4]
  ON [dbo].[FactUse] ([IDFactUse], [IDIndication])
GO

CREATE STATISTICS [_dta_stat_68195293_2_1_8]
  ON [dbo].[FactUse] ([IDPeriod], [IDFactUse], [IDOperation])
GO

CREATE STATISTICS [_dta_stat_68195293_2_4_8]
  ON [dbo].[FactUse] ([IDPeriod], [IDIndication], [IDOperation])
GO

CREATE STATISTICS [_dta_stat_68195293_2_8]
  ON [dbo].[FactUse] ([IDPeriod], [IDOperation])
GO

CREATE STATISTICS [_dta_stat_68195293_6_4_2]
  ON [dbo].[FactUse] ([IDDocument], [IDIndication], [IDPeriod])
GO

CREATE STATISTICS [_dta_stat_68195293_8_4]
  ON [dbo].[FactUse] ([IDOperation], [IDIndication])
GO

ALTER TABLE [dbo].[FactUse] WITH NOCHECK
  ADD CONSTRAINT [FK_FactUse_Document] FOREIGN KEY ([IDDocument]) REFERENCES [dbo].[Document] ([IDDocument])
GO

ALTER TABLE [dbo].[FactUse] WITH NOCHECK
  ADD CONSTRAINT [FK_FactUse_GObject] FOREIGN KEY ([IDGObject]) REFERENCES [dbo].[GObject] ([IDGObject])
GO

ALTER TABLE [dbo].[FactUse] WITH NOCHECK
  ADD CONSTRAINT [FK_FactUse_Operation] FOREIGN KEY ([IDOperation]) REFERENCES [dbo].[Operation] ([IDOperation])
GO

ALTER TABLE [dbo].[FactUse] WITH NOCHECK
  ADD CONSTRAINT [FK_FactUse_Period] FOREIGN KEY ([IDPeriod]) REFERENCES [dbo].[Period] ([IDPeriod])
GO

ALTER TABLE [dbo].[FactUse] WITH NOCHECK
  ADD CONSTRAINT [FK_FactUse_TypeFU] FOREIGN KEY ([IDTypeFU]) REFERENCES [dbo].[TypeFU] ([IDTypeFU])
GO