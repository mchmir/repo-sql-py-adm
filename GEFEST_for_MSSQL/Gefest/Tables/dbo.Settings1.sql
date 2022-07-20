CREATE TABLE [dbo].[Settings1] (
  [IDSettings1] [int] IDENTITY,
  [IDCorrespondent] [int] NULL,
  CONSTRAINT [PK_Settings1] PRIMARY KEY CLUSTERED ([IDSettings1])
)
ON [PRIMARY]
GO

ALTER TABLE [dbo].[Settings1]
  ADD CONSTRAINT [FK_Settings1_Correspondent] FOREIGN KEY ([IDCorrespondent]) REFERENCES [dbo].[Correspondent] ([IDCorrespondent])
GO