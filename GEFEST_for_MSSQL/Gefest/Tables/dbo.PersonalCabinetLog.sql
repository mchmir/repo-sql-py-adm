CREATE TABLE [dbo].[PersonalCabinetLog] (
  [IdLog] [int] IDENTITY,
  [IDPersonalCabinet] [int] NOT NULL,
  [DateLog] [datetime] NOT NULL CONSTRAINT [DF_PersonalCabinetLog_DateLog] DEFAULT (getdate()),
  [DateLastLogin] [datetime] NOT NULL CONSTRAINT [DF_PersonalCabinetLog_DateLastLogin] DEFAULT (getdate()),
  [note] [varchar](1000) NULL
)
ON [PRIMARY]
GO