CREATE TABLE [dbo].[StatusPlita] (
  [IDStatusPlita] [int] IDENTITY,
  [Name] [varchar](50) NULL,
  CONSTRAINT [PK_StatusPlita] PRIMARY KEY CLUSTERED ([IDStatusPlita])
)
ON [PRIMARY]
GO

ALTER TABLE [dbo].[StatusPlita]
  ADD CONSTRAINT [FK_StatusPlita_StatusPlita] FOREIGN KEY ([IDStatusPlita]) REFERENCES [dbo].[StatusPlita] ([IDStatusPlita])
GO