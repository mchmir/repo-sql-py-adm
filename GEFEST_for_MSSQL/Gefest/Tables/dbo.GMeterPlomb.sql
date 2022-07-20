CREATE TABLE [dbo].[GMeterPlomb] (
  [IDPlomb] [int] IDENTITY,
  [IDGmeter] [int] NOT NULL,
  [IDTypePlomb] [int] NOT NULL,
  [PlombNumber] [varchar](50) NOT NULL,
  [DateInstall] [datetime] NOT NULL,
  [DateRemove] [datetime] NULL,
  [Note] [varchar](50) NULL,
  CONSTRAINT [PK_GMeterPlomb] PRIMARY KEY CLUSTERED ([IDPlomb])
)
ON [PRIMARY]
GO