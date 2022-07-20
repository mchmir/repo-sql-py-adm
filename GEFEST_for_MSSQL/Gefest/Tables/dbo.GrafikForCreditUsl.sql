CREATE TABLE [dbo].[GrafikForCreditUsl] (
  [IDGrafik] [int] IDENTITY,
  [IDPeriod] [int] NOT NULL,
  [IDContract] [int] NOT NULL,
  [IDDocument] [int] NOT NULL,
  [AmountPay] [float] NOT NULL,
  CONSTRAINT [PK_GrafikForCreditUsl] PRIMARY KEY CLUSTERED ([IDGrafik])
)
ON [PRIMARY]
GO