CREATE TABLE [dbo].[GObject] (
  [IDGObject] [int] IDENTITY,
  [IDTypeGObject] [int] NULL,
  [IDGru] [int] NULL,
  [IDAddress] [int] NULL,
  [IDStatusGObject] [int] NULL,
  [IDContract] [int] NULL,
  [Name] [varchar](50) NULL,
  [Memo] [varchar](8000) NULL,
  [CountLives] [int] NULL,
  [Etazch] [int] NULL,
  [IDStoik] [int] NULL,
  CONSTRAINT [PK__GObject__5D95E53A] PRIMARY KEY CLUSTERED ([IDGObject])
)
ON [PRIMARY]
GO

CREATE INDEX [_dta_index_GObject_9_719055__K3_K1_K4_K6]
  ON [dbo].[GObject] ([IDGru], [IDGObject], [IDAddress], [IDContract])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_GObject_9_719055__K6_K1_5]
  ON [dbo].[GObject] ([IDContract], [IDGObject])
  INCLUDE ([IDStatusGObject])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_GObject_9_719055__K6_K1_K3_K4]
  ON [dbo].[GObject] ([IDContract], [IDGObject], [IDGru], [IDAddress])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_GObject_9_719055__K6_K1_K5_2_3_4_7_8_9]
  ON [dbo].[GObject] ([IDContract], [IDGObject], [IDStatusGObject])
  INCLUDE ([IDTypeGObject], [IDGru], [IDAddress], [Name], [Memo], [CountLives])
  ON [PRIMARY]
GO

CREATE INDEX [IX_GObject]
  ON [dbo].[GObject] ([IDGru])
  ON [PRIMARY]
GO

CREATE INDEX [IX_GObject_1]
  ON [dbo].[GObject] ([IDContract])
  ON [PRIMARY]
GO

CREATE INDEX [IX_GObject_2]
  ON [dbo].[GObject] ([IDAddress])
  ON [PRIMARY]
GO

CREATE INDEX [IX_GObject_3]
  ON [dbo].[GObject] ([IDStatusGObject])
  ON [PRIMARY]
GO

CREATE STATISTICS [_dta_stat_719055_1_4_6]
  ON [dbo].[GObject] ([IDGObject], [IDAddress], [IDContract])
GO

CREATE STATISTICS [_dta_stat_719055_1_6_3]
  ON [dbo].[GObject] ([IDGObject], [IDContract], [IDGru])
GO

CREATE STATISTICS [_dta_stat_719055_4_1_3_6]
  ON [dbo].[GObject] ([IDAddress], [IDGObject], [IDGru], [IDContract])
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO









-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [AddGObject] on [dbo].[GObject]
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
	set @id=(select top 1 idGObject from inserted)
	insert	into changes(idsending,idtypeaction,tablename,idobject)
	values (@idsending,1,'GObject',@id)
END




GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO












-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [EditGObject] on [dbo].[GObject]
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

	declare @value1 as int
	declare @nvalue1 as int
	
	select @value1=isnull(idaddress,0) from deleted
	select @nvalue1=isnull(idaddress,0) from inserted
	
	if (@nvalue1<>@value1)
	begin
		set @idcorrespondent=(select top 1 idcorrespondent from settings1)
		set @idsending=(select top 1 idsending from sending where idcorrespondent=@idcorrespondent order by idsending desc)
		set @id=(select top 1 idGObject from inserted)
		set @duplicate=(select count(*) from changes
		where idsending=@idsending and idtypeaction=2 and tablename='GObject' and idobject=@id)
		if @duplicate=0
			insert	into changes(idsending,idtypeaction,tablename,idobject)
			values (@idsending,2,'GObject',@id)
	end
END







GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
CREATE TRIGGER [old_value] ON [dbo].[GObject] 
FOR UPDATE
AS

declare @idobject as int
declare @value as int
declare @value2 as int
declare @nvalue as int
declare @nvalue2 as int

select @value=countlives,@value2=IDStatusGObject ,@idobject=idgobject from deleted
select @nvalue=countlives,@nvalue2=IDStatusGObject ,@idobject=idgobject from inserted
--будем записывать в oldvalues значение численности ОУ
--if(COLUMNS_UPDATED() & 9)!=0 --если изменился 9 столбец 
if (@nvalue<>@value)
insert into oldvalues(nametable,namecolumn,idobject,idperiod,datevalues,name)
values('GObject','CountLives',@idobject,dbo.fGetNowPeriod(),dbo.DateOnly(GetDate()),@value)

--будем записывать в oldvalues значение статуса ОУ
--if(COLUMNS_UPDATED() & 5)!=0 --если изменился 5 столбец
if (@nvalue2<>@value2)
insert into oldvalues(nametable,namecolumn,idobject,idperiod,datevalues,name)
values('GObject','IDStatusGObject',@idobject,dbo.fGetNowPeriod(),dbo.DateOnly(GetDate()),@value2)
GO

ALTER TABLE [dbo].[GObject] WITH NOCHECK
  ADD CONSTRAINT [FK_GObject_Contract] FOREIGN KEY ([IDContract]) REFERENCES [dbo].[Contract] ([IDContract])
GO

ALTER TABLE [dbo].[GObject]
  ADD CONSTRAINT [FK_GObject_GRU] FOREIGN KEY ([IDGru]) REFERENCES [dbo].[GRU] ([IDGru])
GO

ALTER TABLE [dbo].[GObject] WITH NOCHECK
  ADD CONSTRAINT [FK_GObject_TypeGObject] FOREIGN KEY ([IDTypeGObject]) REFERENCES [dbo].[TypeGObject] ([IDTypeGObject])
GO