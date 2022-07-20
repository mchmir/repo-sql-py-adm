CREATE TABLE [dbo].[SYSPermission] (
  [IDPermission] [int] IDENTITY,
  [IDObject] [int] NULL,
  [IDGroupUser] [int] NULL,
  [IsRead] [int] NULL,
  [IsAdd] [int] NULL,
  [IsDelete] [int] NULL,
  [IsRun] [int] NULL,
  PRIMARY KEY CLUSTERED ([IDPermission])
)
ON [PRIMARY]
GO

ALTER TABLE [dbo].[SYSPermission]
  ADD FOREIGN KEY ([IDObject]) REFERENCES [dbo].[SYSObject] ([IDObject])
GO