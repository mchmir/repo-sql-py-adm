CREATE TABLE [dbo].[Balance] (
  [IDBalance] [int] IDENTITY,
  [IDAccounting] [int] NULL,
  [IDPeriod] [int] NULL,
  [IDContract] [int] NULL,
  [AmountBalance] [float] NULL,
  [AmountCharge] [float] NULL,
  [AmountPay] [float] NULL,
  CONSTRAINT [PK__Balance__24927208] PRIMARY KEY CLUSTERED ([IDBalance])
)
ON [PRIMARY]
GO

CREATE INDEX [_dta_index_Balance_9_741577680__K3_K4_K1_5]
  ON [dbo].[Balance] ([IDPeriod], [IDContract], [IDBalance])
  INCLUDE ([AmountBalance])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_Balance_9_741577680__K3_K4_K2_5_6_7]
  ON [dbo].[Balance] ([IDPeriod], [IDContract], [IDAccounting])
  INCLUDE ([AmountBalance], [AmountCharge], [AmountPay])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_Balance_9_741577680__K3_K4_K2_K1]
  ON [dbo].[Balance] ([IDPeriod], [IDContract], [IDAccounting], [IDBalance])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_Balance_9_741577680__K4_K3_K1_2_5_6_7]
  ON [dbo].[Balance] ([IDContract], [IDPeriod], [IDBalance])
  INCLUDE ([IDAccounting], [AmountBalance], [AmountCharge], [AmountPay])
  ON [PRIMARY]
GO

CREATE INDEX [IX_Balance]
  ON [dbo].[Balance] ([IDContract], [IDPeriod], [IDBalance])
  ON [PRIMARY]
GO

CREATE STATISTICS [_dta_stat_741577680_1_3]
  ON [dbo].[Balance] ([IDBalance], [IDPeriod])
GO

CREATE STATISTICS [_dta_stat_741577680_1_3_2]
  ON [dbo].[Balance] ([IDBalance], [IDPeriod], [IDAccounting])
GO

CREATE STATISTICS [_dta_stat_741577680_2_3]
  ON [dbo].[Balance] ([IDAccounting], [IDPeriod])
GO

CREATE STATISTICS [_dta_stat_741577680_2_4_1]
  ON [dbo].[Balance] ([IDAccounting], [IDContract], [IDBalance])
GO

CREATE STATISTICS [_dta_stat_741577680_4_1]
  ON [dbo].[Balance] ([IDContract], [IDBalance])
GO

CREATE STATISTICS [_dta_stat_741577680_4_2]
  ON [dbo].[Balance] ([IDContract], [IDAccounting])
GO

CREATE STATISTICS [_dta_stat_741577680_4_3_2_1]
  ON [dbo].[Balance] ([IDContract], [IDPeriod], [IDAccounting], [IDBalance])
GO

ALTER TABLE [dbo].[Balance]
  ADD CONSTRAINT [FK__Balance__IDAccou__6C190EBB] FOREIGN KEY ([IDAccounting]) REFERENCES [dbo].[Accounting] ([IDAccounting])
GO

ALTER TABLE [dbo].[Balance] WITH NOCHECK
  ADD CONSTRAINT [FK__Balance__IDContr__6E01572D] FOREIGN KEY ([IDContract]) REFERENCES [dbo].[Contract] ([IDContract])
GO

ALTER TABLE [dbo].[Balance] WITH NOCHECK
  ADD CONSTRAINT [FK__Balance__IDPerio__6D0D32F4] FOREIGN KEY ([IDPeriod]) REFERENCES [dbo].[Period] ([IDPeriod])
GO