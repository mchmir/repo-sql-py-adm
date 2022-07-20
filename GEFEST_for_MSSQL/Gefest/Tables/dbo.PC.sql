CREATE TABLE [dbo].[PC] (
  [IDPC] [int] IDENTITY,
  [IDPeriod] [int] NULL,
  [IDContract] [int] NULL,
  [IDTypePC] [int] NULL,
  [Value] [varchar](50) NULL,
  PRIMARY KEY CLUSTERED ([IDPC])
)
ON [PRIMARY]
GO

CREATE INDEX [_dta_index_PC_9_837578022__K3_K1_K4_K2_5]
  ON [dbo].[PC] ([IDContract], [IDPC], [IDTypePC], [IDPeriod])
  INCLUDE ([Value])
  ON [PRIMARY]
GO

CREATE INDEX [IX_PC]
  ON [dbo].[PC] ([IDContract], [IDPeriod], [IDTypePC])
  ON [PRIMARY]
GO

CREATE INDEX [IX_PC_1]
  ON [dbo].[PC] ([IDContract], [IDTypePC])
  ON [PRIMARY]
GO

CREATE STATISTICS [_dta_stat_837578022_4_3_1_2]
  ON [dbo].[PC] ([IDTypePC], [IDContract], [IDPC], [IDPeriod])
GO

ALTER TABLE [dbo].[PC] WITH NOCHECK
  ADD CONSTRAINT [FK__PC__IDContract__43D61337] FOREIGN KEY ([IDContract]) REFERENCES [dbo].[Contract] ([IDContract])
GO

ALTER TABLE [dbo].[PC] WITH NOCHECK
  ADD CONSTRAINT [FK__PC__IDPeriod__30C33EC3] FOREIGN KEY ([IDPeriod]) REFERENCES [dbo].[Period] ([IDPeriod])
GO

ALTER TABLE [dbo].[PC]
  ADD FOREIGN KEY ([IDTypePC]) REFERENCES [dbo].[TypePC] ([IDTypePC])
GO