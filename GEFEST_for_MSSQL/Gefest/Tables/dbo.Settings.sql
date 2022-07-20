CREATE TABLE [dbo].[Settings] (
  [IDSettings] [int] IDENTITY,
  [ReportPath] [varchar](200) NULL,
  [IDUser] [int] NOT NULL,
  [IDAgent] [int] NULL,
  [Startup] [bit] NOT NULL,
  CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED ([IDSettings])
)
ON [PRIMARY]
GO