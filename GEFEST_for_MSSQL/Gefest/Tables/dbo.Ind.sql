CREATE TABLE [dbo].[Ind] (
  [IDIndication] [int] NOT NULL,
  [Display] [float] NULL,
  [DateDisplay] [datetime] NOT NULL,
  [IdGMeter] [int] NOT NULL,
  [IdUser] [int] NULL,
  [DateAdd] [datetime] NULL,
  [IdModify] [int] NULL,
  [DateModify] [datetime] NULL,
  [Premech] [varchar](2000) NULL,
  [IDTypeIndication] [int] NULL,
  [IDAgent] [int] NULL
)
ON [PRIMARY]
GO