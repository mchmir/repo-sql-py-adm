CREATE TABLE [dbo].[Bank] (
  [IDBank] [int] IDENTITY,
  [IDPlace] [int] NULL,
  [Name] [varchar](50) NULL,
  [MFO] [varchar](50) NULL,
  PRIMARY KEY CLUSTERED ([IDBank])
)
ON [PRIMARY]
GO

ALTER TABLE [dbo].[Bank]
  ADD CONSTRAINT [FK__Bank__IDPlace__151B244E] FOREIGN KEY ([IDPlace]) REFERENCES [dbo].[Place] ([IDPlace])
GO