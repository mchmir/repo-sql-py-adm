CREATE TABLE [dbo].[Changes] (
  [IDChanges] [int] IDENTITY,
  [IDSending] [int] NOT NULL,
  [IDTypeAction] [int] NOT NULL,
  [TableName] [varchar](50) NOT NULL,
  [IDObject] [int] NOT NULL,
  CONSTRAINT [PK_Changes] PRIMARY KEY CLUSTERED ([IDChanges])
)
ON [PRIMARY]
GO

ALTER TABLE [dbo].[Changes]
  ADD CONSTRAINT [FK_Changes_Sending] FOREIGN KEY ([IDSending]) REFERENCES [dbo].[Sending] ([IDSending])
GO

ALTER TABLE [dbo].[Changes]
  ADD CONSTRAINT [FK_Changes_TypeAction] FOREIGN KEY ([IDTypeAction]) REFERENCES [dbo].[TypeAction] ([IDTypeAction])
GO