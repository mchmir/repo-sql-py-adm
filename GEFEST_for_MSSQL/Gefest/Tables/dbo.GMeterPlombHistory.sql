CREATE TABLE [dbo].[GMeterPlombHistory] (
  [IDGMeter] [int] NOT NULL,
  [PlombNumber1] [varchar](50) NULL,
  [PlombNumber2] [varchar](50) NULL,
  [DatePlomb] [datetime] NULL,
  [IndicationPlomb] [float] NULL,
  [IDAgentPlomb] [int] NULL,
  [PlombMemo] [varchar](200) NULL,
  [IDPlombHistory] [int] IDENTITY,
  [IDTypePlombWork] [int] NULL,
  CONSTRAINT [PK_GMeterPlombHistory] PRIMARY KEY CLUSTERED ([IDPlombHistory])
)
ON [PRIMARY]
GO