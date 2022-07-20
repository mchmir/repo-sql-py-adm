CREATE TABLE [dbo].[OwnerShip] (
  [IDOwnership] [int] IDENTITY,
  [Name] [varchar](50) NULL,
  [Level] [int] NULL,
  PRIMARY KEY CLUSTERED ([IDOwnership])
)
ON [PRIMARY]
GO