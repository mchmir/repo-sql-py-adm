CREATE TABLE [dbo].[Correspondent] (
  [IDCorrespondent] [int] IDENTITY,
  [Name] [varchar](200) NOT NULL,
  CONSTRAINT [PK_Correspondent] PRIMARY KEY CLUSTERED ([IDCorrespondent])
)
ON [PRIMARY]
GO