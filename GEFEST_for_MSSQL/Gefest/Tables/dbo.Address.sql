CREATE TABLE [dbo].[Address] (
  [IDAddress] [int] IDENTITY,
  [IDTypeAddress] [int] NULL,
  [IDPlace] [int] NULL,
  [IDStreet] [int] NULL,
  [IDHouse] [int] NULL,
  [Flat] [varchar](20) NULL,
  CONSTRAINT [PK__Address__6383C8BA] PRIMARY KEY CLUSTERED ([IDAddress])
)
ON [PRIMARY]
GO

CREATE INDEX [_dta_index_Address_9_763149764__K1_K5_K4_K6]
  ON [dbo].[Address] ([IDAddress], [IDHouse], [IDStreet], [Flat])
  ON [PRIMARY]
GO

CREATE INDEX [IX_Address]
  ON [dbo].[Address] ([IDHouse])
  ON [PRIMARY]
GO

CREATE INDEX [IX_Address_1]
  ON [dbo].[Address] ([IDHouse])
  ON [PRIMARY]
GO

CREATE INDEX [IX_Address_2]
  ON [dbo].[Address] ([IDPlace])
  ON [PRIMARY]
GO

CREATE INDEX [IX_Address_3]
  ON [dbo].[Address] ([IDStreet])
  ON [PRIMARY]
GO

CREATE INDEX [IX_Address_4]
  ON [dbo].[Address] ([IDAddress])
  ON [PRIMARY]
GO

CREATE STATISTICS [_dta_stat_763149764_4_5_1_6]
  ON [dbo].[Address] ([IDStreet], [IDHouse], [IDAddress], [Flat])
GO

CREATE STATISTICS [_dta_stat_763149764_6_5_4]
  ON [dbo].[Address] ([Flat], [IDHouse], [IDStreet])
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [AddAddress] on [dbo].[Address]
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
	set @id=(select top 1 idaddress from inserted)
	insert	into changes(idsending,idtypeaction,tablename,idobject)
	values (@idsending,1,'Address',@id)
END
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [DelAddress] on [dbo].[Address]
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
	set @id=(select top 1 idaddress from deleted)
	insert	into changes(idsending,idtypeaction,tablename,idobject)
	values (@idsending,3,'Address',@id)
END

GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [EditAddress] on [dbo].[Address]
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
	set @id=(select top 1 idaddress from inserted)
	set @duplicate=(select count(*) from changes
	where idsending=@idsending and idtypeaction=2 and tablename='Address' and idobject=@id)
	if @duplicate=0
		insert	into changes(idsending,idtypeaction,tablename,idobject)
		values (@idsending,2,'Address',@id)
END


GO

ALTER TABLE [dbo].[Address] WITH NOCHECK
  ADD CONSTRAINT [FK__Address__IDHouse__1DB06A4F] FOREIGN KEY ([IDHouse]) REFERENCES [dbo].[House] ([IDHouse])
GO

ALTER TABLE [dbo].[Address] WITH NOCHECK
  ADD CONSTRAINT [FK__Address__IDPlace__1EA48E88] FOREIGN KEY ([IDPlace]) REFERENCES [dbo].[Place] ([IDPlace])
GO

ALTER TABLE [dbo].[Address] WITH NOCHECK
  ADD CONSTRAINT [FK__Address__IDStree__1F98B2C1] FOREIGN KEY ([IDStreet]) REFERENCES [dbo].[Street] ([IDStreet])
GO

ALTER TABLE [dbo].[Address] WITH NOCHECK
  ADD CONSTRAINT [FK__Address__IDTypeA__208CD6FA] FOREIGN KEY ([IDTypeAddress]) REFERENCES [dbo].[TypeAddress] ([IDTypeAddress])
GO