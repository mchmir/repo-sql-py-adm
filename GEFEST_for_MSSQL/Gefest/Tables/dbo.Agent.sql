CREATE TABLE [dbo].[Agent] (
  [IDAgent] [int] IDENTITY,
  [IDTypeAgent] [int] NULL,
  [Name] [varchar](50) NULL,
  [NumberAgent] [int] NULL,
  [IdSector] [int] NULL,
  [NumberBatch] [varchar](50) NULL,
  CONSTRAINT [PK__Agent__5165187F] PRIMARY KEY CLUSTERED ([IDAgent])
)
ON [PRIMARY]
GO

ALTER TABLE [dbo].[Agent] WITH NOCHECK
  ADD CONSTRAINT [FK__Agent__IDTypeAge__778AC167] FOREIGN KEY ([IDTypeAgent]) REFERENCES [dbo].[TypeAgent] ([IDTypeAgent])
GO

ALTER TABLE [dbo].[Agent] WITH NOCHECK
  ADD CONSTRAINT [FK__Agent__IDTypeAge__787EE5A0] FOREIGN KEY ([IDTypeAgent]) REFERENCES [dbo].[TypeAgent] ([IDTypeAgent])
GO

ALTER TABLE [dbo].[Agent] WITH NOCHECK
  ADD CONSTRAINT [FK__Agent__IDTypeAge__797309D9] FOREIGN KEY ([IDTypeAgent]) REFERENCES [dbo].[TypeAgent] ([IDTypeAgent])
GO