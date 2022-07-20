CREATE TABLE [dbo].[PersonalCabinetMessage] (
  [IDPersonalCabinetMessage] [int] IDENTITY,
  [MessageTitle] [varchar](50) NOT NULL,
  [MessageBody] [varchar](250) NOT NULL,
  [DateMessage] [datetime] NOT NULL CONSTRAINT [DF_PersonalCabinetMessage_DateMessage] DEFAULT (getdate()),
  [Account] [varchar](50) NOT NULL,
  [MessageType] [int] NOT NULL,
  [NeedSendMessage] [int] NOT NULL CONSTRAINT [DF_PersonalCabinetMessage_NeedSendMessage] DEFAULT (1),
  [IDMessage] [int] NOT NULL CONSTRAINT [DF_PersonalCabinetMessage_IDMessage] DEFAULT (0),
  CONSTRAINT [PK_PersonalCabinetMessage] PRIMARY KEY CLUSTERED ([IDPersonalCabinetMessage])
)
ON [PRIMARY]
GO