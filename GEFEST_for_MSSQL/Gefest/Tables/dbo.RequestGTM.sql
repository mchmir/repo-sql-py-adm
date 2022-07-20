CREATE TABLE [dbo].[RequestGTM] (
  [IDRequest] [int] IDENTITY,
  [WorkDescription] [ntext] NULL,
  [Perfomer] [nvarchar](255) NULL,
  [Result] [ntext] NULL,
  [ContactInformation] [ntext] NULL,
  [DateRequest] [datetime] NULL,
  [DateExecute] [datetime] NULL,
  [FactDateExecute] [datetime] NULL,
  [Cost] [float] NULL,
  [IDContract] [int] NULL,
  [IDAddress] [int] NULL,
  [FactPay] [float] NULL,
  [StreetName] [nvarchar](255) NULL,
  [House] [nvarchar](255) NULL,
  [Flat] [nvarchar](255) NULL,
  [Account] [nvarchar](255) NULL,
  [IDPerfomer] [int] NULL,
  [GMeterInfo] [nvarchar](255) NULL,
  [Indication] [nvarchar](255) NULL,
  [FIO] [nvarchar](255) NULL,
  [IDTypeWork] [int] NULL,
  [Note] [ntext] NULL,
  [DateManufactured] [datetime] NULL,
  [IDTypeResult] [int] NULL,
  [IDPoveritel] [int] NULL,
  [NamePoveritel] [nvarchar](255) NULL
)
ON [PRIMARY]
TEXTIMAGE_ON [PRIMARY]
GO