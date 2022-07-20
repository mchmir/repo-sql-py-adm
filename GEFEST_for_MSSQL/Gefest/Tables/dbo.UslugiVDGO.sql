CREATE TABLE [dbo].[UslugiVDGO] (
  [IDUslugiVDGO] [int] IDENTITY,
  [Name] [nvarchar](500) NULL,
  [Value] [float] NULL,
  CONSTRAINT [PK_UslugiVDGO] PRIMARY KEY CLUSTERED ([IDUslugiVDGO])
)
ON [PRIMARY]
GO