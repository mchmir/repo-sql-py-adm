CREATE TABLE [dbo].[House] (
  [IDHouse] [int] IDENTITY,
  [IDStreet] [int] NULL,
  [IDTypeHouse] [int] NULL,
  [HouseNumber] [int] NULL,
  [HouseNumberChar] [varchar](20) NULL,
  [IsEvenSide] [int] NULL,
  [IsComfortable] [int] NULL,
  [ColPodezdov] [int] NULL,
  [ColKvartir] [int] NULL,
  [ColEtazh] [int] NULL,
  [LengthGazProvod] [float] NULL,
  [GRU] [int] NULL,
  [VGP] [int] NULL,
  [NGP] [int] NULL,
  [ZU] [int] NULL,
  CONSTRAINT [PK__House__628FA481] PRIMARY KEY CLUSTERED ([IDHouse])
)
ON [PRIMARY]
GO

CREATE INDEX [IX_House]
  ON [dbo].[House] ([HouseNumber], [HouseNumberChar])
  ON [PRIMARY]
GO

CREATE INDEX [IX_House_1]
  ON [dbo].[House] ([IDStreet])
  ON [PRIMARY]
GO

CREATE INDEX [IX_House_2]
  ON [dbo].[House] ([HouseNumberChar])
  ON [PRIMARY]
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO









-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [AddHouse] on [dbo].[House]
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
	set @id=(select top 1 idHouse from inserted)
	insert	into changes(idsending,idtypeaction,tablename,idobject)
	values (@idsending,1,'House',@id)
END




GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO









-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [DelHouse] on [dbo].[House]
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
	set @id=(select top 1 idHouse from deleted)
	insert	into changes(idsending,idtypeaction,tablename,idobject)
	values (@idsending,3,'House',@id)
END




GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO










-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [EditHouse] on [dbo].[House]
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
	set @id=(select top 1 idHouse from inserted)
	set @duplicate=(select count(*) from changes
	where idsending=@idsending and idtypeaction=2 and tablename='House' and idobject=@id)
	if @duplicate=0
		insert	into changes(idsending,idtypeaction,tablename,idobject)
		values (@idsending,2,'House',@id)
END





GO

ALTER TABLE [dbo].[House] WITH NOCHECK
  ADD CONSTRAINT [FK__House__IDStreet__1BC821DD] FOREIGN KEY ([IDStreet]) REFERENCES [dbo].[Street] ([IDStreet])
GO

ALTER TABLE [dbo].[House] WITH NOCHECK
  ADD CONSTRAINT [FK__House__IDTypeHou__1CBC4616] FOREIGN KEY ([IDTypeHouse]) REFERENCES [dbo].[TypeHouse] ([IDTypeHouse])
GO