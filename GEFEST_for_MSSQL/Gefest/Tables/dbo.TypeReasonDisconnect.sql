CREATE TABLE [dbo].[TypeReasonDisconnect] (
  [IDTypeReasonDisconnect] [int] IDENTITY,
  [Name] [varchar](100) NOT NULL,
  CONSTRAINT [PK_TypeReasonDisconnect] PRIMARY KEY CLUSTERED ([IDTypeReasonDisconnect])
)
ON [PRIMARY]
GO