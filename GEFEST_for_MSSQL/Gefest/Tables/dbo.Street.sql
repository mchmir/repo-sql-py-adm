CREATE TABLE [dbo].[Street] (
  [IDStreet] [int] IDENTITY,
  [Name] [varchar](50) NULL,
  [IDPlace] [int] NULL,
  [IDTypeStreet] [int] NULL,
  CONSTRAINT [PK__Street__619B8048] PRIMARY KEY CLUSTERED ([IDStreet])
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
CREATE TRIGGER [AddStreet] on [dbo].[Street]
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    declare @id as int
	declare @idsending as int
	declare @idcorrespondent as int
	set @idcorrespondent=(select top 1 idcorrespondent from settings1)
	set @idsending=(select top 1 idsending from sending where idcorrespondent=@idcorrespondent order by idsending desc)
	set @id=(select top 1 idStreet from inserted)
	insert	into changes(idsending,idtypeaction,tablename,idobject)
	values (@idsending,1,'Street',@id)
END







GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO












-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [DelStreet] on [dbo].[Street]
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    declare @id as int
	declare @idsending as int
	declare @idcorrespondent as int
	set @idcorrespondent=(select top 1 idcorrespondent from settings1)
	set @idsending=(select top 1 idsending from sending where idcorrespondent=@idcorrespondent order by idsending desc)
	set @id=(select top 1 idStreet from deleted)
	insert	into changes(idsending,idtypeaction,tablename,idobject)
	values (@idsending,3,'Street',@id)
END







GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO













-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [EditStreet] on [dbo].[Street]
   AFTER UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    declare @id as int
	declare @idsending as int
	declare @idcorrespondent as int
	declare @duplicate as int
	set @idcorrespondent=(select top 1 idcorrespondent from settings1)
	set @idsending=(select top 1 idsending from sending where idcorrespondent=@idcorrespondent order by idsending desc)
	set @id=(select top 1 idStreet from inserted)
	set @duplicate=(select count(*) from changes
	where idsending=@idsending and idtypeaction=2 and tablename='Street' and idobject=@id)
	if @duplicate=0
		insert	into changes(idsending,idtypeaction,tablename,idobject)
		values (@idsending,2,'Street',@id)
END








GO

ALTER TABLE [dbo].[Street] WITH NOCHECK
  ADD CONSTRAINT [FK__Street__IDPlace__19DFD96B] FOREIGN KEY ([IDPlace]) REFERENCES [dbo].[Place] ([IDPlace])
GO

ALTER TABLE [dbo].[Street] WITH NOCHECK
  ADD CONSTRAINT [FK__Street__IDTypeSt__1AD3FDA4] FOREIGN KEY ([IDTypeStreet]) REFERENCES [dbo].[TypeStreet] ([IDTypeStreet])
GO