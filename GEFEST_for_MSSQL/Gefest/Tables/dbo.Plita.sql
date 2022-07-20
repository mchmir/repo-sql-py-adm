CREATE TABLE [dbo].[Plita] (
  [IDPlita] [int] IDENTITY,
  [IDStatusPlita] [int] NOT NULL,
  [IDTypePlita] [int] NOT NULL,
  [Soedinenie] [bit] NOT NULL,
  [IDGObject] [int] NOT NULL,
  [DateInstall] [datetime] NOT NULL,
  [DateVerify] [datetime] NULL,
  [Memo] [varchar](8000) NULL,
  [IDSchemaConnect] [int] NULL,
  CONSTRAINT [PK_Plita] PRIMARY KEY CLUSTERED ([IDPlita])
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
CREATE TRIGGER [PlitaSchemaConnectHistoryInsert] 
   ON  [dbo].[Plita] 
   FOR INSERT, UPDATE
AS 
declare @idplita as int
declare @IDSchemaConnect as int
declare @IDSchemaConnectN as int
declare @DateSchema as datetime

set @DateSchema=getdate()

select @IDSchemaConnect=IDSchemaConnect, @idplita=idplita from deleted
select @IDSchemaConnectN=IDSchemaConnect, @idplita=idplita from inserted
--if isnull(@DatePlomb,'1800-01-01')<>'1800-01-01'
--begin
	--будем записывать в историю
	if (isnull(@IDSchemaConnect,0)<>isnull(@IDSchemaConnectN,0))
	begin 
		insert into dbo.PlitaSchemaConnectHistory(IDPlita,IDSchemaConnect,DateChange)
		values(@idplita,@IDSchemaConnectN,@DateSchema)
	end





GO

ALTER TABLE [dbo].[Plita]
  ADD CONSTRAINT [FK_Plita_GObject] FOREIGN KEY ([IDGObject]) REFERENCES [dbo].[GObject] ([IDGObject])
GO

ALTER TABLE [dbo].[Plita]
  ADD CONSTRAINT [FK_Plita_StatusPlita] FOREIGN KEY ([IDStatusPlita]) REFERENCES [dbo].[StatusPlita] ([IDStatusPlita])
GO

ALTER TABLE [dbo].[Plita]
  ADD CONSTRAINT [FK_Plita_TypePlita] FOREIGN KEY ([IDTypePlita]) REFERENCES [dbo].[TypePlita] ([IDTypePlita])
GO