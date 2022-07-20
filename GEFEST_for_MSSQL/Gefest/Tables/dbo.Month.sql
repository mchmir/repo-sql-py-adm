CREATE TABLE [dbo].[Month] (
  [idMonth] [int] IDENTITY,
  [nameMonth] [varchar](15) NOT NULL,
  CONSTRAINT [PK_Month] PRIMARY KEY CLUSTERED ([idMonth])
)
ON [PRIMARY]
GO