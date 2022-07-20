CREATE TABLE [dbo].[WorkPeny] (
  [idcontract] [int] NOT NULL,
  [account] [varchar](50) NULL,
  [bal1] [float] NULL,
  [bal2] [float] NULL,
  [amountoperation] [float] NULL,
  [dated] [datetime] NULL,
  [forpeny] [float] NULL,
  [stavka] [float] NULL,
  [days] [int] NULL,
  [peny] [float] NULL,
  [IDPeriod] [int] NULL
)
ON [PRIMARY]
GO