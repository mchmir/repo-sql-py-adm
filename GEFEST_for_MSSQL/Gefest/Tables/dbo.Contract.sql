CREATE TABLE [dbo].[Contract] (
  [IDContract] [int] IDENTITY,
  [IDTypeContract] [int] NOT NULL,
  [IDPerson] [int] NULL,
  [Account] [varchar](50) NOT NULL,
  [DateBegin] [datetime] NULL,
  [DateEnd] [datetime] NULL,
  [Status] [int] NULL,
  [Memo] [varchar](8000) NULL,
  [IDTypeTariff] [int] NOT NULL,
  [SummaDogovora] [float] NULL,
  [PrintChetIzvehen] [int] NULL,
  [IDTypeInfringements] [int] NULL,
  [ChargePeny] [int] NULL,
  [CountDoVerify] [int] NULL,
  [DateDoVerify] [datetime] NULL,
  [PCPassword] [varchar](50) NULL,
  [PCEmail] [varchar](50) NULL,
  [PCPass] [varchar](50) NULL,
  [PCneedchgPWD] [tinyint] NULL,
  [IDPersonalCabinet] [int] NULL,
  CONSTRAINT [PK__Contract__66603565] PRIMARY KEY CLUSTERED ([IDContract]),
  CONSTRAINT [IX_Contract] UNIQUE ([Account])
)
ON [PRIMARY]
GO

CREATE INDEX [_dta_index_Contract_9_852198086__K1_7]
  ON [dbo].[Contract] ([IDContract])
  INCLUDE ([Status])
  ON [PRIMARY]
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [AddContract] on [dbo].[Contract]
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
	set @id=(select top 1 idContract from inserted)
	insert	into changes(idsending,idtypeaction,tablename,idobject)
	values (@idsending,1,'Contract',@id)
END

GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [DelContract] on [dbo].[Contract]
   AFTER Delete
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
	set @id=(select top 1 idContract from deleted)
	insert	into changes(idsending,idtypeaction,tablename,idobject)
	values (@idsending,3,'Contract',@id)
END


GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO









-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [EditContract] on [dbo].[Contract]
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

	declare @value1 as varchar(50)
	declare @nvalue1 as varchar(50)
	declare @value2 as int
	declare @nvalue2 as int
	
	select @value1=isnull(account,''), @value2=isnull(idperson,0) from deleted
	select @nvalue1=isnull(account,''), @nvalue2=isnull(idperson,0) from inserted
	
	if (@nvalue1<>@value1 or @nvalue2<>@value2)
	begin
		set @idcorrespondent=(select top 1 idcorrespondent from settings1)
		set @idsending=(select top 1 idsending from sending where idcorrespondent=@idcorrespondent order by idsending desc)
		set @id=(select top 1 idContract from inserted)
		set @duplicate=(select count(*) from changes
		where idsending=@idsending and idtypeaction=2 and tablename='Contract' and idobject=@id)
		if @duplicate=0
			insert	into changes(idsending,idtypeaction,tablename,idobject)
			values (@idsending,2,'Contract',@id)
	end
END




GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

Create TRIGGER [old_TypeInfringements] ON [dbo].[Contract] 
FOR UPDATE
AS

declare @idobject as int
declare @value as int
declare @nvalue as int

select @value=idTypeInfringements,@idobject=idcontract from deleted
select @nvalue=idTypeInfringements,@idobject=idcontract from inserted
--будем записывать в oldvalues значение idTypeInfringements
--if(COLUMNS_UPDATED() & 7)!=0 --если изменился 7 столбец 
if(@nvalue<>@value)
insert into oldvalues(nametable,namecolumn,idobject,idperiod,datevalues,name)
values('Contract','TypeInfringements',@idobject,dbo.fGetNowPeriod(),dbo.DateOnly(GetDate()),@value)


GO

SET QUOTED_IDENTIFIER OFF

SET ANSI_NULLS ON
GO
CREATE TRIGGER [old_value2] ON [dbo].[Contract] 
FOR UPDATE
AS

declare @idobject as int
declare @value as int
declare @nvalue as int

select @value=Status,@idobject=idcontract from deleted
select @nvalue=Status,@idobject=idcontract from inserted
--будем записывать в oldvalues значение статуса
--if(COLUMNS_UPDATED() & 7)!=0 --если изменился 7 столбец 
if(@nvalue<>@value)
insert into oldvalues(nametable,namecolumn,idobject,idperiod,datevalues,name)
values('Contract','Status',@idobject,dbo.fGetNowPeriod(),dbo.DateOnly(GetDate()),@value)

GO

DENY
  ALTER,
  CONTROL,
  DELETE,
  INSERT,
  UPDATE
ON [dbo].[Contract] TO [GTMprosmotr]
GO

ALTER TABLE [dbo].[Contract] WITH NOCHECK
  ADD CONSTRAINT [FK__Contract__IDPers__2645B050] FOREIGN KEY ([IDPerson]) REFERENCES [dbo].[Person] ([IDPerson])
GO

ALTER TABLE [dbo].[Contract] WITH NOCHECK
  ADD CONSTRAINT [FK__Contract__IDType__2739D489] FOREIGN KEY ([IDTypeContract]) REFERENCES [dbo].[TypeContract] ([IDTypeContract])
GO

ALTER TABLE [dbo].[Contract] WITH NOCHECK
  ADD CONSTRAINT [FK__Contract__IDType__282DF8C2] FOREIGN KEY ([IDTypeContract]) REFERENCES [dbo].[TypeContract] ([IDTypeContract])
GO

ALTER TABLE [dbo].[Contract]
  ADD CONSTRAINT [FK_Contract_TypeInfringements] FOREIGN KEY ([IDTypeInfringements]) REFERENCES [dbo].[TypeInfringements] ([IDTypeInfringements])
GO

ALTER TABLE [dbo].[Contract] WITH NOCHECK
  ADD CONSTRAINT [FK_Contract_TypeTariff] FOREIGN KEY ([IDTypeTariff]) REFERENCES [dbo].[TypeTariff] ([IDTypeTariff])
GO