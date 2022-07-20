CREATE TABLE [dbo].[PrintLog] (
  [IDPrintLog] [int] IDENTITY,
  [DatePrintLog] [datetime] NULL CONSTRAINT [DF_PrintLog_DatePrintLog] DEFAULT (getdate()),
  [IDUser] [int] NOT NULL,
  [TypePrintLog] [int] NOT NULL,
  [Note] [varchar](100) NULL,
  CONSTRAINT [PK_PrintLog] PRIMARY KEY CLUSTERED ([IDPrintLog])
)
ON [PRIMARY]
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [Trigger_Name]
   ON  [dbo].[PrintLog]
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;
declare @id  bigint
set @id=(select IDPrintLog from inserted)
update PrintLog 
    set DatePrintLog=getdate()
where IDPrintLog=@id

END
GO