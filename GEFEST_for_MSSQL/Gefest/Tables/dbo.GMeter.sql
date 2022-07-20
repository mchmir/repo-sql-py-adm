CREATE TABLE [dbo].[GMeter] (
  [IDGMeter] [int] IDENTITY,
  [IDStatusGMeter] [int] NULL,
  [IDTypeGMeter] [int] NULL,
  [SerialNumber] [varchar](50) NULL,
  [BeginValue] [float] NULL,
  [DateInstall] [datetime] NULL,
  [DateVerify] [datetime] NULL,
  [Memo] [varchar](8000) NULL,
  [IDGObject] [int] NULL,
  [DateFabrication] [datetime] NULL,
  [IDTypeVerify] [int] NULL,
  [PlombNumber1] [varchar](50) NULL,
  [PlombNumber2] [varchar](50) NULL,
  [DatePlomb] [datetime] NULL,
  [IndicationPlomb] [float] NULL,
  [IDAgentPlomb] [int] NULL,
  [PlombMemo] [varchar](200) NULL,
  [IDTypePlombWork] [int] NULL,
  [DateOnOff] [datetime] NULL,
  CONSTRAINT [PK__GMeter__5070F446] PRIMARY KEY CLUSTERED ([IDGMeter])
)
ON [PRIMARY]
GO

CREATE INDEX [_dta_index_GMeter_7_1796201449__K11_1_2_3_4_5_6_7_8_9_10]
  ON [dbo].[GMeter] ([IDTypeVerify])
  INCLUDE ([IDGMeter], [IDStatusGMeter], [IDTypeGMeter], [SerialNumber], [BeginValue], [DateInstall], [DateVerify], [Memo], [IDGObject], [DateFabrication])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_GMeter_9_1796201449__K2_K9]
  ON [dbo].[GMeter] ([IDStatusGMeter], [IDGObject])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_GMeter_9_1796201449__K2_K9_K1_K3_4]
  ON [dbo].[GMeter] ([IDStatusGMeter], [IDGObject], [IDGMeter], [IDTypeGMeter])
  INCLUDE ([SerialNumber])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_GMeter_9_1796201449__K9_K1_K2D_3_4_5_6_7_8_10]
  ON [dbo].[GMeter] ([IDGObject], [IDGMeter], [IDStatusGMeter] DESC)
  INCLUDE ([IDTypeGMeter], [SerialNumber], [BeginValue], [DateInstall], [DateVerify], [Memo], [DateFabrication])
  ON [PRIMARY]
GO

CREATE INDEX [_dta_index_GMeter_9_1796201449__K9_K2_K1_K6]
  ON [dbo].[GMeter] ([IDGObject], [IDStatusGMeter], [IDGMeter], [DateInstall])
  ON [PRIMARY]
GO

CREATE INDEX [IX_GMeter]
  ON [dbo].[GMeter] ([IDStatusGMeter])
  ON [PRIMARY]
GO

CREATE INDEX [IX_GMeter_1]
  ON [dbo].[GMeter] ([IDGObject])
  ON [PRIMARY]
GO

CREATE INDEX [IX_GMeter_2]
  ON [dbo].[GMeter] ([IDGMeter])
  ON [PRIMARY]
GO

CREATE STATISTICS [_dta_stat_1796201449_1_2]
  ON [dbo].[GMeter] ([IDGMeter], [IDStatusGMeter])
GO

CREATE STATISTICS [_dta_stat_1796201449_1_6_2_9]
  ON [dbo].[GMeter] ([IDGMeter], [DateInstall], [IDStatusGMeter], [IDGObject])
GO

CREATE STATISTICS [_dta_stat_1796201449_2_3]
  ON [dbo].[GMeter] ([IDStatusGMeter], [IDTypeGMeter])
GO

CREATE STATISTICS [_dta_stat_1796201449_3_9]
  ON [dbo].[GMeter] ([IDTypeGMeter], [IDGObject])
GO

CREATE STATISTICS [_dta_stat_1796201449_6_1_9]
  ON [dbo].[GMeter] ([DateInstall], [IDGMeter], [IDGObject])
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO









-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [AddGMeter] on [dbo].[GMeter]
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
	set @id=(select top 1 idGMeter from inserted)
	insert	into changes(idsending,idtypeaction,tablename,idobject)
	values (@idsending,1,'GMeter',@id)
	insert	into conformitygmeter(correspondent1,correspondent2,correspondent3)
	values (null,null,@id)
END




GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO








-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [DelGMeter] on [dbo].[GMeter]
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
	set @id=(select top 1 idGMeter from deleted)
	insert	into changes(idsending,idtypeaction,tablename,idobject)
	values (@idsending,3,'GMeter',@id)
END



GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO












-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [EditMeter] on [dbo].[GMeter]
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
	set @id=(select top 1 idGMeter from inserted)
	set @duplicate=(select count(*) from changes
	where idsending=@idsending and idtypeaction=2 and tablename='GMeter' and idobject=@id)
	if @duplicate=0
		insert	into changes(idsending,idtypeaction,tablename,idobject)
		values (@idsending,2,'GMeter',@id)
END







GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [GMeterPlombHistoryInsert] 
   ON  [dbo].[GMeter] 
   FOR INSERT, UPDATE
AS 
declare @idgmeter as int
declare @PlombNumber1 as varchar(50)
declare @PlombNumber2 as varchar(50)
declare @DatePlomb as datetime
declare @IndicationPlomb as float
declare @nPlombNumber1 as varchar(50)
declare @nPlombNumber2 as varchar(50)
declare @nDatePlomb as datetime
declare @nIndicationPlomb as float
declare @IDAgentPlomb as int
declare @nIDAgentPlomb as int
declare @PlombMemo as varchar(200)
declare @nPlombMemo as varchar(200)
declare @IDTypePlombWork as int

select @PlombNumber1=PlombNumber1,@PlombNumber2=PlombNumber2,@PlombMemo=PlombMemo,
	@DatePlomb=DatePlomb,@IndicationPlomb=IndicationPlomb, @IDAgentPlomb=IDAgentPlomb,
 @idgmeter=idgmeter from deleted
select @nPlombNumber1=PlombNumber1,@nPlombNumber2=PlombNumber2,@nPlombMemo=PlombMemo,
	@nDatePlomb=DatePlomb,@nIndicationPlomb=IndicationPlomb, @nIDAgentPlomb=IDAgentPlomb, @IDTypePlombWork=IDTypePlombWork,
 @idgmeter=idgmeter from inserted
--if isnull(@DatePlomb,'1800-01-01')<>'1800-01-01'
if @nIndicationPlomb>0 
begin
	--будем записывать в историю
	if (isnull(@PlombNumber1,'')<>@nPlombNumber1) 
		or (isnull(@PlombNumber2,'')<>@nPlombNumber2) 
		or (isnull(@DatePlomb,'1800-01-01')<>@nDatePlomb)
		or (isnull(@IndicationPlomb,0)<>@nIndicationPlomb)
		or (isnull(@IDAgentPlomb,0)<>@nIDAgentPlomb)
		or (isnull(@PlombMemo,'')<>isnull(@nPlombMemo,'')) 
	insert into dbo.GMeterPlombHistory(IDGMeter,PlombNumber1,PlombNumber2,DatePlomb,IndicationPlomb,IDAgentPlomb,PlombMemo,IDTypePlombWork)
	values(@idgmeter,@nPlombNumber1,@nPlombNumber2,@nDatePlomb,@nIndicationPlomb,@nIDAgentPlomb,@nPlombMemo,@IDTypePlombWork)
end






GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO




CREATE TRIGGER [old_value5] ON [dbo].[GMeter] 
FOR UPDATE
AS

declare @idobject as int
declare @value as int
declare @nvalue as int
declare @dateonoff as datetime
declare @date as datetime

select @value=IDStatusGMeter, @idobject=idgmeter from deleted
select @nvalue=IDStatusGMeter, @idobject=idgmeter from inserted
select @dateonoff=dateonoff from inserted

set @date=dbo.DateOnly(GetDate())
if @dateonoff is not null 
	begin
		set @date=dbo.DateOnly(@dateonoff)
	end

--будем записывать в oldvalues значение статуса ПУ
if (@nvalue<>@value)
	begin
		insert into oldvalues(nametable,namecolumn,idobject,idperiod,datevalues,name)
		values('GMeter','IDStatusGMeter',@idobject,dbo.fGetNowPeriod(),@date,@value)
	end



GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO








CREATE TRIGGER [old_value6] ON [dbo].[GMeter] 
FOR insert
AS

declare @idobject as int
declare @dateonoff as datetime
declare @date as datetime
--declare @nvalue as int

select @idobject=idgmeter from inserted
select @dateonoff=dateonoff from inserted

set @date=dbo.DateOnly(GetDate())
if @dateonoff is not null 
	begin
		set @date=dbo.DateOnly(@dateonoff)
	end
--будем записывать в oldvalues значение статуса ПУ
insert into oldvalues(nametable,namecolumn,idobject,idperiod,datevalues,name)
values('GMeter','IDStatusGMeter',@idobject,dbo.fGetNowPeriod(),@date,2)







GO

DENY
  ALTER,
  CONTROL,
  DELETE,
  INSERT,
  UPDATE
ON [dbo].[GMeter] TO [GTMprosmotr]
GO

ALTER TABLE [dbo].[GMeter] WITH NOCHECK
  ADD CONSTRAINT [FK__GMeter__IDStatus__03F0984C] FOREIGN KEY ([IDStatusGMeter]) REFERENCES [dbo].[StatusGMeter] ([IDStatusGMeter])
GO

ALTER TABLE [dbo].[GMeter] WITH NOCHECK
  ADD CONSTRAINT [FK__GMeter__IDTypeGM__04E4BC85] FOREIGN KEY ([IDTypeGMeter]) REFERENCES [dbo].[TypeGMeter] ([IDTypeGMeter])
GO

ALTER TABLE [dbo].[GMeter] WITH NOCHECK
  ADD CONSTRAINT [FK_GMeter_GObject] FOREIGN KEY ([IDGObject]) REFERENCES [dbo].[GObject] ([IDGObject])
GO

ALTER TABLE [dbo].[GMeter]
  ADD CONSTRAINT [FK_GMeter_TypeVerify] FOREIGN KEY ([IDTypeVerify]) REFERENCES [dbo].[TypeVerify] ([IDTypeVerify])
GO