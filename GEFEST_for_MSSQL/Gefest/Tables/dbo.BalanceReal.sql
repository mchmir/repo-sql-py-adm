CREATE TABLE [dbo].[BalanceReal] (
  [IDBalanceReal] [int] IDENTITY,
  [IDAccounting] [int] NULL,
  [IDPeriod] [int] NULL,
  [IDContract] [int] NULL,
  [AmountBalance] [float] NULL,
  [AmountCharge] [float] NULL,
  [AmountPay] [float] NULL,
  CONSTRAINT [PK__BalanceReal__24927208] PRIMARY KEY CLUSTERED ([IDBalanceReal])
)
ON [PRIMARY]
GO

CREATE INDEX [_dta_index_BalanceReal_7_757577737__K3_K4_K2]
  ON [dbo].[BalanceReal] ([IDPeriod], [IDContract], [IDAccounting])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_BalanceReal_8_757577737__K2_K3_K4_5]
  ON [dbo].[BalanceReal] ([IDAccounting], [IDPeriod], [IDContract])
  INCLUDE ([AmountBalance])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_BalanceReal_9_757577737__K3_1_2_4_5]
  ON [dbo].[BalanceReal] ([IDPeriod])
  INCLUDE ([IDBalanceReal], [IDAccounting], [IDContract], [AmountBalance])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_BalanceReal_9_757577737__K3_K1_K4_K2]
  ON [dbo].[BalanceReal] ([IDPeriod], [IDBalanceReal], [IDContract], [IDAccounting])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_BalanceReal_9_757577737__K3_K4_K1_5]
  ON [dbo].[BalanceReal] ([IDPeriod], [IDContract], [IDBalanceReal])
  INCLUDE ([AmountBalance])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_BalanceReal_9_757577737__K4_K3_K1]
  ON [dbo].[BalanceReal] ([IDContract], [IDPeriod], [IDBalanceReal])
  ON [PRIMARY]
GO

CREATE STATISTICS [_dta_stat_757577737_2_3]
  ON [dbo].[BalanceReal] ([IDAccounting], [IDPeriod])
GO

CREATE STATISTICS [_dta_stat_757577737_3_1_4]
  ON [dbo].[BalanceReal] ([IDPeriod], [IDBalanceReal], [IDContract])
GO

CREATE STATISTICS [_dta_stat_757577737_3_4]
  ON [dbo].[BalanceReal] ([IDPeriod], [IDContract])
GO

CREATE STATISTICS [_dta_stat_757577737_3_4_2_1]
  ON [dbo].[BalanceReal] ([IDPeriod], [IDContract], [IDAccounting], [IDBalanceReal])
GO

CREATE STATISTICS [_dta_stat_757577737_3_5_2_4]
  ON [dbo].[BalanceReal] ([IDPeriod], [AmountBalance], [IDAccounting], [IDContract])
GO

CREATE STATISTICS [_dta_stat_757577737_3_5_4]
  ON [dbo].[BalanceReal] ([IDPeriod], [AmountBalance], [IDContract])
GO

CREATE STATISTICS [_dta_stat_757577737_4_2_1]
  ON [dbo].[BalanceReal] ([IDContract], [IDAccounting], [IDBalanceReal])
GO

CREATE STATISTICS [_dta_stat_757577737_5_4_2]
  ON [dbo].[BalanceReal] ([AmountBalance], [IDContract], [IDAccounting])
GO

ALTER TABLE [dbo].[BalanceReal]
  ADD CONSTRAINT [FK__BalanceReal__IDAccou__6C190EBB] FOREIGN KEY ([IDAccounting]) REFERENCES [dbo].[Accounting] ([IDAccounting])
GO

ALTER TABLE [dbo].[BalanceReal] WITH NOCHECK
  ADD CONSTRAINT [FK__BalanceReal__IDContr__6E01572D] FOREIGN KEY ([IDContract]) REFERENCES [dbo].[Contract] ([IDContract])
GO

ALTER TABLE [dbo].[BalanceReal] WITH NOCHECK
  ADD CONSTRAINT [FK__BalanceReal__IDPerio__6D0D32F4] FOREIGN KEY ([IDPeriod]) REFERENCES [dbo].[Period] ([IDPeriod])
GO