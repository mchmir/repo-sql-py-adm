CREATE TABLE [dbo].[Document] (
  [IDDocument] [int] IDENTITY,
  [IDContract] [int] NULL,
  [IDPeriod] [int] NOT NULL,
  [IDBatch] [int] NULL,
  [IDTypeDocument] [int] NOT NULL,
  [DocumentNumber] [varchar](20) NULL,
  [DocumentDate] [datetime] NOT NULL,
  [DocumentAmount] [float] NULL,
  [IDGru] [int] NULL,
  [IdUser] [int] NULL,
  [DateAdd] [datetime] NULL,
  [IdModify] [int] NULL,
  [DateModify] [datetime] NULL,
  [Note] [varchar](8000) NULL,
  [TerminalData] [varchar](20) NULL,
  CONSTRAINT [PK__Document__20C1E124] PRIMARY KEY CLUSTERED ([IDDocument])
)
ON [PRIMARY]
GO

CREATE INDEX [_dta_index_Document_8_1892201791__K2_K5_K1_7]
  ON [dbo].[Document] ([IDContract], [IDTypeDocument], [IDDocument])
  INCLUDE ([DocumentDate])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_Document_9_1892201791__K2_K4_8]
  ON [dbo].[Document] ([IDContract], [IDBatch])
  INCLUDE ([DocumentAmount])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_Document_9_1892201791__K3_K5_K2_K1_8]
  ON [dbo].[Document] ([IDPeriod], [IDTypeDocument], [IDContract], [IDDocument])
  INCLUDE ([DocumentAmount])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_Document_9_1892201791__K3_K5_K7_K4_K1_8]
  ON [dbo].[Document] ([IDPeriod], [IDTypeDocument], [DocumentDate], [IDBatch], [IDDocument])
  INCLUDE ([DocumentAmount])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_Document_9_1892201791__K5_K1_K2_K7_8]
  ON [dbo].[Document] ([IDTypeDocument], [IDDocument], [IDContract], [DocumentDate])
  INCLUDE ([DocumentAmount])
  ON [PRIMARY]
GO

CREATE INDEX [IX_Document]
  ON [dbo].[Document] ([IDTypeDocument])
  ON [PRIMARY]
GO

CREATE INDEX [IX_Document_1]
  ON [dbo].[Document] ([IDPeriod])
  ON [PRIMARY]
GO

CREATE INDEX [IX_Document_2]
  ON [dbo].[Document] ([IDContract])
  ON [PRIMARY]
GO

CREATE INDEX [IX_Document_3]
  ON [dbo].[Document] ([DocumentDate])
  ON [PRIMARY]
GO

CREATE INDEX [IX_Document_4]
  ON [dbo].[Document] ([IDPeriod], [IDTypeDocument])
  ON [PRIMARY]
GO

CREATE STATISTICS [_dta_stat_1892201791_1_2]
  ON [dbo].[Document] ([IDDocument], [IDContract])
GO

CREATE STATISTICS [_dta_stat_1892201791_1_3_5_7]
  ON [dbo].[Document] ([IDDocument], [IDPeriod], [IDTypeDocument], [DocumentDate])
GO

CREATE STATISTICS [_dta_stat_1892201791_1_4_2_3]
  ON [dbo].[Document] ([IDDocument], [IDBatch], [IDContract], [IDPeriod])
GO

CREATE STATISTICS [_dta_stat_1892201791_1_5]
  ON [dbo].[Document] ([IDDocument], [IDTypeDocument])
GO

CREATE STATISTICS [_dta_stat_1892201791_2_1_3]
  ON [dbo].[Document] ([IDContract], [IDDocument], [IDPeriod])
GO

CREATE STATISTICS [_dta_stat_1892201791_2_3]
  ON [dbo].[Document] ([IDContract], [IDPeriod])
GO

CREATE STATISTICS [_dta_stat_1892201791_4_1_3]
  ON [dbo].[Document] ([IDBatch], [IDDocument], [IDPeriod])
GO

CREATE STATISTICS [_dta_stat_1892201791_4_2]
  ON [dbo].[Document] ([IDBatch], [IDContract])
GO

CREATE STATISTICS [_dta_stat_1892201791_5_1_4_2_3]
  ON [dbo].[Document] ([IDTypeDocument], [IDDocument], [IDBatch], [IDContract], [IDPeriod])
GO

CREATE STATISTICS [_dta_stat_1892201791_5_2]
  ON [dbo].[Document] ([IDTypeDocument], [IDContract])
GO

CREATE STATISTICS [_dta_stat_1892201791_5_4_1_3]
  ON [dbo].[Document] ([IDTypeDocument], [IDBatch], [IDDocument], [IDPeriod])
GO

CREATE STATISTICS [_dta_stat_1892201791_7_1_5_2]
  ON [dbo].[Document] ([DocumentDate], [IDDocument], [IDTypeDocument], [IDContract])
GO

CREATE STATISTICS [_dta_stat_1892201791_7_2_5]
  ON [dbo].[Document] ([DocumentDate], [IDContract], [IDTypeDocument])
GO

CREATE STATISTICS [_dta_stat_1892201791_7_5]
  ON [dbo].[Document] ([DocumentDate], [IDTypeDocument])
GO

CREATE STATISTICS [_dta_stat_1892201791_7_5_1_4_3]
  ON [dbo].[Document] ([DocumentDate], [IDTypeDocument], [IDDocument], [IDBatch], [IDPeriod])
GO

ALTER TABLE [dbo].[Document] WITH NOCHECK
  ADD CONSTRAINT [FK_Document_Batch] FOREIGN KEY ([IDBatch]) REFERENCES [dbo].[Batch] ([IDBatch])
GO