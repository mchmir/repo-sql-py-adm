CREATE TABLE [dbo].[ConformityDocument] (
  [Correspondent1] [int] NULL,
  [Correspondent2] [int] NULL,
  [Correspondent3] [int] NOT NULL
)
ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty N'MS_Description', N'IDDocument в БД Нацэкса', 'SCHEMA', N'dbo', 'TABLE', N'ConformityDocument', 'COLUMN', N'Correspondent1'
GO

EXEC sys.sp_addextendedproperty N'MS_Description', N'IDDocument в БД КомСтройТела', 'SCHEMA', N'dbo', 'TABLE', N'ConformityDocument', 'COLUMN', N'Correspondent2'
GO

EXEC sys.sp_addextendedproperty N'MS_Description', N'IDDocument в БД Горгаза', 'SCHEMA', N'dbo', 'TABLE', N'ConformityDocument', 'COLUMN', N'Correspondent3'
GO