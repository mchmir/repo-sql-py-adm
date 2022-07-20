CREATE TABLE [dbo].[TypeAddress] (
  [IDTypeAddress] [int] IDENTITY,
  [Name] [varchar](50) NULL,
  [Memo] [varchar](100) NULL,
  PRIMARY KEY CLUSTERED ([IDTypeAddress])
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
CREATE TRIGGER [AddTypeAddress] on [dbo].[TypeAddress]
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
	set @id=(select top 1 idTypeAddress from inserted)
	insert	into changes(idsending,idtypeaction,tablename,idobject)
	values (@idsending,1,'TypeAddress',@id)
END








GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO













-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [DelTypeAddress] on [dbo].[TypeAddress]
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
	set @id=(select top 1 idTypeAddress from deleted)
	insert	into changes(idsending,idtypeaction,tablename,idobject)
	values (@idsending,3,'TypeAddress',@id)
END








GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO














-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [EditTypeAddress] on [dbo].[TypeAddress]
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
	set @id=(select top 1 idTypeAddress from inserted)
	set @duplicate=(select count(*) from changes
	where idsending=@idsending and idtypeaction=2 and tablename='TypeAddress' and idobject=@id)
	if @duplicate=0
		insert	into changes(idsending,idtypeaction,tablename,idobject)
		values (@idsending,2,'TypeAddress',@id)
END









GO