CREATE TABLE [dbo].[IndicationTemp] (
  [IDIndicationTemp] [int] IDENTITY,
  [Display] [float] NULL,
  [DateDisplay] [datetime] NOT NULL,
  [IdGMeter] [int] NOT NULL,
  [IdUser] [int] NULL,
  [DateAdd] [datetime] NULL,
  [IdModify] [int] NULL,
  [DateModify] [datetime] NULL,
  [Premech] [varchar](2000) NULL,
  [IDTypeIndication] [int] NULL,
  [IDAgent] [int] NULL,
  [State] [int] NULL,
  CONSTRAINT [PK_IndicationTemp] PRIMARY KEY NONCLUSTERED ([IDIndicationTemp])
)
ON [PRIMARY]
GO