CREATE TABLE [dbo].[Classifier] (
  [IDClassifier] [int] IDENTITY,
  [Name] [varchar](50) NULL,
  [IDParent] [int] NULL,
  [Level] [int] NULL,
  PRIMARY KEY CLUSTERED ([IDClassifier])
)
ON [PRIMARY]
GO