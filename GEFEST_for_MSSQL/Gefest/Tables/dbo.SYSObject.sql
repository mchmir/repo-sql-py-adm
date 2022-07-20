CREATE TABLE [dbo].[SYSObject] (
  [IDObject] [int] IDENTITY,
  [IDTypeObject] [int] NULL,
  [Name] [varchar](50) NULL,
  PRIMARY KEY CLUSTERED ([IDObject])
)
ON [PRIMARY]
GO

ALTER TABLE [dbo].[SYSObject]
  ADD FOREIGN KEY ([IDTypeObject]) REFERENCES [dbo].[SYSTypeObject] ([IDTypeObject])
GO