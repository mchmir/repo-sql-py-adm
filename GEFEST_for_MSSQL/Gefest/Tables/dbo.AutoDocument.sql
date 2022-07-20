CREATE TABLE [dbo].[AutoDocument] (
  [IDAutoDocument] [int] IDENTITY,
  [IDAutoBatch] [int] NOT NULL,
  [IDDocument] [int] NULL,
  [DocumentDate] [datetime] NOT NULL,
  [DocumentAmount] [float] NOT NULL,
  [IDStatusAutoDocument] [int] NOT NULL,
  [Account] [varchar](12) NOT NULL,
  [FIO] [varchar](200) NOT NULL,
  [Number] [varchar](10) NULL,
  CONSTRAINT [PK_AutoDocument] PRIMARY KEY CLUSTERED ([IDAutoDocument])
)
ON [PRIMARY]
GO

ALTER TABLE [dbo].[AutoDocument]
  ADD CONSTRAINT [FK_AutoDocument_AutoBatch] FOREIGN KEY ([IDAutoBatch]) REFERENCES [dbo].[AutoBatch] ([IDAutoBatch])
GO