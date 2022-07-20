CREATE TABLE [dbo].[SYSUser] (
  [IDUser] [int] IDENTITY,
  [IDGroupUser] [int] NULL,
  [Password] [varchar](50) NULL,
  [Login] [varchar](50) NULL,
  [FIO] [varchar](50) NULL,
  [NewLogin] [varchar](50) NULL,
  [NewID] [int] NULL,
  [NNew] [int] NULL,
  PRIMARY KEY CLUSTERED ([IDUser])
)
ON [PRIMARY]
GO

ALTER TABLE [dbo].[SYSUser]
  ADD FOREIGN KEY ([IDGroupUser]) REFERENCES [dbo].[SYSGroupUser] ([IDGroupUser])
GO