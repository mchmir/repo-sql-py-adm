CREATE TABLE [dbo].[DocumentVDGO] (
  [IDDocumentVDGO] [int] IDENTITY,
  [IDTypeDocumentVDGO] [int] NOT NULL,
  [IDGobject] [int] NULL,
  [IDPlita] [int] NULL,
  [DocumentDate] [datetime] NULL,
  [Memo] [varchar](8000) NULL,
  [IDHouse] [int] NULL,
  [DocDopusk] [varchar](8000) NULL,
  [IDPersonal] [int] NULL,
  [DocumentNumber] [varchar](20) NULL,
  [DocumentAmount] [float] NULL,
  CONSTRAINT [PK_DocumentVDGO] PRIMARY KEY CLUSTERED ([IDDocumentVDGO])
)
ON [PRIMARY]
GO

ALTER TABLE [dbo].[DocumentVDGO]
  ADD CONSTRAINT [FK_DocumentVDGO_Personal] FOREIGN KEY ([IDPersonal]) REFERENCES [dbo].[Personal] ([IDPersonal])
GO

ALTER TABLE [dbo].[DocumentVDGO]
  ADD CONSTRAINT [FK_DocumentVDGO_TypeDocumentVDGO] FOREIGN KEY ([IDTypeDocumentVDGO]) REFERENCES [dbo].[TypeDocumentVDGO] ([IDTypeDocumentVDGO])
GO