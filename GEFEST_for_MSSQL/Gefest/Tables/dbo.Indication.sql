CREATE TABLE [dbo].[Indication] (
  [IDIndication] [int] IDENTITY,
  [Display] [float] NULL,
  [DateDisplay] [datetime] NOT NULL,
  [IdGMeter] [int] NOT NULL,
  [IdUser] [int] NULL,
  [DateAdd] [datetime] NULL,
  [IdModify] [int] NULL,
  [DateModify] [datetime] NULL,
  [Premech] [varchar](2000) NULL,
  [IDTypeIndication] [int] NULL,
  [IDAgent] [int] NULL,
  CONSTRAINT [PK_Indication] PRIMARY KEY NONCLUSTERED ([IDIndication])
)
ON [PRIMARY]
GO

CREATE INDEX [_dta_index_Indication_7_1856725667__K10_K6_K5_K11_K1_2_3_4_7_8_9]
  ON [dbo].[Indication] ([IDTypeIndication], [DateAdd], [IdUser], [IDAgent], [IDIndication])
  INCLUDE ([Display], [DateDisplay], [IdGMeter], [IdModify], [DateModify], [Premech])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_Indication_9_1856725667__K3_1_4]
  ON [dbo].[Indication] ([DateDisplay])
  INCLUDE ([IDIndication], [IdGMeter])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_Indication_9_1856725667__K4_K1_K3_2]
  ON [dbo].[Indication] ([IdGMeter], [IDIndication], [DateDisplay])
  INCLUDE ([Display])
  ON [PRIMARY]
GO

CREATE INDEX [IX_Indication_1]
  ON [dbo].[Indication] ([DateDisplay])
  ON [PRIMARY]
GO

CREATE CLUSTERED INDEX [IX_Indication_2]
  ON [dbo].[Indication] ([IdGMeter])
  ON [PRIMARY]
GO

CREATE STATISTICS [_dta_stat_1856725667_1_5_10_11]
  ON [dbo].[Indication] ([IDIndication], [IdUser], [IDTypeIndication], [IDAgent])
GO

CREATE STATISTICS [_dta_stat_1856725667_11_6_5]
  ON [dbo].[Indication] ([IDAgent], [DateAdd], [IdUser])
GO

CREATE STATISTICS [_dta_stat_1856725667_3_1]
  ON [dbo].[Indication] ([DateDisplay], [IDIndication])
GO

CREATE STATISTICS [_dta_stat_1856725667_4_3]
  ON [dbo].[Indication] ([IdGMeter], [DateDisplay])
GO

CREATE STATISTICS [_dta_stat_1856725667_5_10_11_6_1]
  ON [dbo].[Indication] ([IdUser], [IDTypeIndication], [IDAgent], [DateAdd], [IDIndication])
GO

CREATE STATISTICS [_dta_stat_1856725667_6_5_10]
  ON [dbo].[Indication] ([DateAdd], [IdUser], [IDTypeIndication])
GO