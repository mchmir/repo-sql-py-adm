CREATE TABLE [dbo].[TariffConnectionGobject] (
  [IDTariffConnectionGobject] [int] IDENTITY,
  [IDPeriod] [int] NULL,
  [Value] [float] NULL,
  CONSTRAINT [PK_TariffConnectionGobject] PRIMARY KEY CLUSTERED ([IDTariffConnectionGobject])
)
ON [PRIMARY]
GO