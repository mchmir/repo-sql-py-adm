CREATE TABLE [dbo].[Country] (
  [IDCountry] [int] IDENTITY,
  [Name] [varchar](50) NOT NULL,
  CONSTRAINT [PK__Country__398D8EEE] PRIMARY KEY CLUSTERED ([IDCountry])
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
CREATE TRIGGER [AddCountry] on [dbo].[Country]
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
	set @id=(select top 1 idCountry from inserted)
	insert	into changes(idsending,idtypeaction,tablename,idobject)
	values (@idsending,1,'Country',@id)
END
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [DelCountry] on [dbo].[Country]
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
	set @id=(select top 1 idCountry from deleted)
	insert	into changes(idsending,idtypeaction,tablename,idobject)
	values (@idsending,3,'Country',@id)
END
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [EditCountry] on [dbo].[Country]
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
	set @id=(select top 1 idCountry from inserted)
	set @duplicate=(select count(*) from changes
	where idsending=@idsending and idtypeaction=2 and tablename='Country' and idobject=@id)
	if @duplicate=0
		insert	into changes(idsending,idtypeaction,tablename,idobject)
		values (@idsending,2,'Country',@id)
END
GO