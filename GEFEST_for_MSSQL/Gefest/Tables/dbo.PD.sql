CREATE TABLE [dbo].[PD] (
  [IDPD] [int] IDENTITY,
  [IDTypePD] [int] NULL,
  [IDDocument] [int] NULL,
  [Value] [varchar](2000) NULL,
  CONSTRAINT [PK__ParameterDocumen__4AB81AF0] PRIMARY KEY CLUSTERED ([IDPD])
)
ON [PRIMARY]
GO

CREATE INDEX [_dta_index_PD_9_391672443__K2_K3_1_4]
  ON [dbo].[PD] ([IDTypePD], [IDDocument])
  INCLUDE ([IDPD], [Value])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_PD_9_391672443__K3_K2_K1_4]
  ON [dbo].[PD] ([IDDocument], [IDTypePD], [IDPD])
  INCLUDE ([Value])
  ON [PRIMARY]
GO

CREATE INDEX [IX_PD]
  ON [dbo].[PD] ([IDTypePD], [IDDocument], [IDPD])
  ON [PRIMARY]
GO

CREATE INDEX [IX_PD_1]
  ON [dbo].[PD] ([IDDocument])
  ON [PRIMARY]
GO

ALTER TABLE [dbo].[PD] WITH NOCHECK
  ADD CONSTRAINT [FK__Parameter__IDTyp__02084FDA] FOREIGN KEY ([IDTypePD]) REFERENCES [dbo].[TypePD] ([IDTypePD])
GO

ALTER TABLE [dbo].[PD] WITH NOCHECK
  ADD CONSTRAINT [FK_PD_Document] FOREIGN KEY ([IDDocument]) REFERENCES [dbo].[Document] ([IDDocument]) ON DELETE CASCADE
GO