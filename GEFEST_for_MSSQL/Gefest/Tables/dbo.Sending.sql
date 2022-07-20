CREATE TABLE [dbo].[Sending] (
  [IDSending] [int] IDENTITY,
  [DateSending] [datetime] NOT NULL,
  [IDCorrespondent] [int] NOT NULL,
  [NumberSending] [int] NOT NULL,
  CONSTRAINT [PK_Sending] PRIMARY KEY CLUSTERED ([IDSending])
)
ON [PRIMARY]
GO

ALTER TABLE [dbo].[Sending]
  ADD CONSTRAINT [FK_Sending_Correspondent] FOREIGN KEY ([IDCorrespondent]) REFERENCES [dbo].[Correspondent] ([IDCorrespondent])
GO