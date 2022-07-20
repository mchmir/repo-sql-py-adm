CREATE TABLE [dbo].[Tariff] (
  [IdTariff] [int] IDENTITY,
  [Value] [float] NULL,
  [IdPeriod] [int] NULL,
  [IDTypeTariff] [int] NULL,
  CONSTRAINT [PK_Tariff] PRIMARY KEY CLUSTERED ([IdTariff])
)
ON [PRIMARY]
GO

ALTER TABLE [dbo].[Tariff] WITH NOCHECK
  ADD CONSTRAINT [FK_Tariff_Period] FOREIGN KEY ([IdPeriod]) REFERENCES [dbo].[Period] ([IDPeriod])
GO

ALTER TABLE [dbo].[Tariff] WITH NOCHECK
  ADD CONSTRAINT [FK_Tariff_TypeTariff] FOREIGN KEY ([IDTypeTariff]) REFERENCES [dbo].[TypeTariff] ([IDTypeTariff])
GO