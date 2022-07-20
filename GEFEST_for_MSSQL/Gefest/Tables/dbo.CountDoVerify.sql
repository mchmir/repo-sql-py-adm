CREATE TABLE [dbo].[CountDoVerify] (
  [Count] [int] NOT NULL,
  [DatePrint] [datetime] NOT NULL,
  [Type] [int] NOT NULL,
  [IDRecord] [int] IDENTITY,
  CONSTRAINT [PK_CountDoVerify] PRIMARY KEY CLUSTERED ([IDRecord])
)
ON [PRIMARY]
GO