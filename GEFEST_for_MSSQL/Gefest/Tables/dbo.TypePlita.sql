CREATE TABLE [dbo].[TypePlita] (
  [IDTypePlita] [int] IDENTITY,
  [Name] [varchar](50) NULL,
  [ColGorel] [int] NULL,
  [Electro] [bit] NULL,
  CONSTRAINT [PK_TypePlita] PRIMARY KEY CLUSTERED ([IDTypePlita])
)
ON [PRIMARY]
GO