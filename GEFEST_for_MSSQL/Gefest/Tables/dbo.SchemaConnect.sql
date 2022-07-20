CREATE TABLE [dbo].[SchemaConnect] (
  [IDSchemaConnect] [int] IDENTITY,
  [Path] [varchar](50) NOT NULL,
  [Name] [varchar](50) NOT NULL,
  [Note] [varchar](50) NULL,
  [Standart] [int] NULL,
  CONSTRAINT [PK_SchemaConnnect] PRIMARY KEY CLUSTERED ([IDSchemaConnect])
)
ON [PRIMARY]
GO