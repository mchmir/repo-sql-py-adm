CREATE TABLE [dbo].[TariffDisconnectionGobject] (
  [IDTariffDisconnectionGobject] [int] IDENTITY,
  [IDPeriod] [int] NULL,
  [Value] [float] NULL,
  CONSTRAINT [PK_TariffDisconnectionGobject] PRIMARY KEY CLUSTERED ([IDTariffDisconnectionGobject])
)
ON [PRIMARY]
GO