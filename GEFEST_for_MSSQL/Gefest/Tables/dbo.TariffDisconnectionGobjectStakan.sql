CREATE TABLE [dbo].[TariffDisconnectionGobjectStakan] (
  [IDTariffDisconnectionGobjectStakan] [int] IDENTITY,
  [IDPeriod] [int] NULL,
  [Value] [float] NULL,
  CONSTRAINT [PK_TariffDisconnectionGobjectStakan] PRIMARY KEY CLUSTERED ([IDTariffDisconnectionGobjectStakan])
)
ON [PRIMARY]
GO