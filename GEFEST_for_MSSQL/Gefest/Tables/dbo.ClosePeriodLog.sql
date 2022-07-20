CREATE TABLE [dbo].[ClosePeriodLog] (
  [IDLog] [int] IDENTITY,
  [SPName] [varchar](50) NOT NULL,
  [StepName] [varchar](200) NOT NULL,
  [DateExec] [datetime] NOT NULL,
  CONSTRAINT [PK_ClosePeriodLog] PRIMARY KEY CLUSTERED ([IDLog])
)
ON [PRIMARY]
GO