CREATE TABLE [dbo].[Personal] (
  [IDPersonal] [int] IDENTITY,
  [Name] [varchar](100) NULL,
  CONSTRAINT [PK_Personal] PRIMARY KEY CLUSTERED ([IDPersonal])
)
ON [PRIMARY]
GO