CREATE TABLE [dbo].[CashBalance] (
  [IDCashBalance] [int] IDENTITY,
  [AmountBalance] [float] NULL,
  [DateCash] [datetime] NULL,
  [IdCashier] [int] NULL,
  CONSTRAINT [PK_CashBalance] PRIMARY KEY CLUSTERED ([IDCashBalance])
)
ON [PRIMARY]
GO

CREATE INDEX [IX_CashBalance]
  ON [dbo].[CashBalance] ([IdCashier], [DateCash])
  ON [PRIMARY]
GO

ALTER TABLE [dbo].[CashBalance] WITH NOCHECK
  ADD CONSTRAINT [FK_CashBalance_Agent] FOREIGN KEY ([IdCashier]) REFERENCES [dbo].[Agent] ([IDAgent])
GO