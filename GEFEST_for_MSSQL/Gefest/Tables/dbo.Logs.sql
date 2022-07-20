CREATE TABLE [dbo].[Logs] (
  [IDLogs] [int] IDENTITY,
  [IDUser] [int] NULL,
  [DataLog] [datetime] NULL,
  [Message] [varchar](200) NULL,
  [Application] [varchar](50) NULL,
  [Computer] [varchar](50) NULL,
  [TypeLog] [int] NULL,
  PRIMARY KEY CLUSTERED ([IDLogs])
)
ON [PRIMARY]
GO