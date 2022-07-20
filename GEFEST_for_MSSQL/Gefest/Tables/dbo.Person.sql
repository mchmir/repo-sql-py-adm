CREATE TABLE [dbo].[Person] (
  [IDPerson] [int] IDENTITY,
  [IDUser] [int] NULL,
  [IDAddress] [int] NULL,
  [RNN] [varchar](12) NULL,
  [isJuridical] [int] NULL,
  [Surname] [varchar](100) NULL,
  [Name] [varchar](100) NULL,
  [Patronic] [varchar](100) NULL,
  [IDOwnership] [int] NULL,
  [FIOMainBuch] [varchar](100) NULL,
  [IDClassifier] [int] NULL,
  [WorkPlace] [varchar](100) NULL,
  [Memo] [varchar](8000) NULL,
  [NumberUDL] [varchar](100) NULL,
  [NumberDog] [varchar](50) NULL,
  [DateDog] [datetime] NULL,
  [CostDog] [float] NULL,
  [NSurname] [nvarchar](100) NULL,
  [NName] [nvarchar](100) NULL,
  [NPatronic] [nvarchar](100) NULL,
  CONSTRAINT [PK__Person__656C112C] PRIMARY KEY CLUSTERED ([IDPerson])
)
ON [PRIMARY]
GO

CREATE INDEX [_dta_index_Person_9_1364199910__K1_K6_K7_K8]
  ON [dbo].[Person] ([IDPerson], [Surname], [Name], [Patronic])
  ON [PRIMARY]
GO

CREATE INDEX [IX_Person]
  ON [dbo].[Person] ([IDAddress])
  ON [PRIMARY]
GO

CREATE INDEX [IX_Person_1]
  ON [dbo].[Person] ([IDOwnership])
  ON [PRIMARY]
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [AddPerson] on [dbo].[Person]
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
	set @id=(select top 1 idPerson from inserted)
	insert	into changes(idsending,idtypeaction,tablename,idobject)
	values (@idsending,1,'Person',@id)
END
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [EditPerson] on [dbo].[Person]
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

	declare @value1 as varchar(100)
	declare @value2 as varchar(100)
	declare @value3 as varchar(100)
	declare @nvalue1 as varchar(100)
	declare @nvalue2 as varchar(100)
	declare @nvalue3 as varchar(100)

	select @value1=isnull(Surname,''),@value2=isnull(Name,''),@value3=isnull(Patronic,'') from deleted
	select @nvalue1=isnull(Surname,''),@nvalue2=isnull(Name,''),@nvalue3=isnull(Patronic,'') from inserted
	
	if (@nvalue1<>@value1 or @nvalue2<>@value2 or @nvalue3<>@value3)
	begin
		set @idcorrespondent=(select top 1 idcorrespondent from settings1)
		set @idsending=(select top 1 idsending from sending where idcorrespondent=@idcorrespondent order by idsending desc)
		set @id=(select top 1 idPerson from inserted)
		set @duplicate=(select count(*) from changes
		where idsending=@idsending and idtypeaction=2 and tablename='Person' and idobject=@id)
		if @duplicate=0
			insert	into changes(idsending,idtypeaction,tablename,idobject)
			values (@idsending,2,'Person',@id)
	end
END
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
CREATE TRIGGER [old_value3] ON [dbo].[Person]
FOR UPDATE
AS

declare @idobject as int
declare @value1 as varchar(100)
declare @value2 as varchar(100)
declare @value3 as varchar(100)
declare @value4 as int
declare @value5 as int
declare @value6 as varchar(100)
declare @value7 as varchar(100)
declare @value8 as varchar(100)
declare @nvalue1 as varchar(100)
declare @nvalue2 as varchar(100)
declare @nvalue3 as varchar(100)
declare @nvalue4 as int
declare @nvalue5 as int
declare @nvalue6 as varchar(100)
declare @nvalue7 as varchar(100)
declare @nvalue8 as varchar(100)

select @value1=isnull(Surname,''),@value2=isnull(Name,''),
@value3=isnull(Patronic,''),@value6=isnull(WorkPlace,''),@value7=isnull(RNN,''),
@value4=isjuridical,@value5=iduser,@idobject=idperson ,@value8=isnull(NumberUDL,'') from deleted

select @nvalue1=isnull(Surname,''),@nvalue2=isnull(Name,''),
@nvalue3=isnull(Patronic,''),@nvalue6=isnull(WorkPlace,''),@nvalue7=isnull(RNN,''),
@nvalue4=isjuridical,@nvalue5=iduser,@idobject=idperson,@nvalue8=isnull(NumberUDL,'') from inserted
--будем записывать в oldvalues значение ФИО
--if(COLUMNS_UPDATED() & 6)!=0 --если изменился 6 столбец
if (@nvalue1<>@value1)
insert into oldvalues(nametable,namecolumn,idobject,idperiod,datevalues,name)
values('Person','Surname',@idobject,dbo.fGetNowPeriod(),dbo.DateOnly(GetDate()),@value1)

--if(COLUMNS_UPDATED() & 7)!=0
if (@nvalue2<>@value2)
insert into oldvalues(nametable,namecolumn,idobject,idperiod,datevalues,name)
values('Person','Name',@idobject,dbo.fGetNowPeriod(),dbo.DateOnly(GetDate()),@value2)

--if(COLUMNS_UPDATED() & 8)!=0
if (@nvalue3<>@value3)
insert into oldvalues(nametable,namecolumn,idobject,idperiod,datevalues,name)
values('Person','Patronic',@idobject,dbo.fGetNowPeriod(),dbo.DateOnly(GetDate()),@value3)

if (@nvalue6<>@value6)
insert into oldvalues(nametable,namecolumn,idobject,idperiod,datevalues,name)
values('Person','WorkPlace',@idobject,dbo.fGetNowPeriod(),dbo.DateOnly(GetDate()),@value6)

if (@nvalue4<>@value4)
insert into oldvalues(nametable,namecolumn,idobject,idperiod,datevalues,name)
values('Person','isJuridical',@idobject,dbo.fGetNowPeriod(),dbo.DateOnly(GetDate()),@value4)

if (@nvalue4<>@value4 or @nvalue1<>@value1 or @nvalue2<>@value2 or @nvalue3<>@value3)
insert into oldvalues(nametable,namecolumn,idobject,idperiod,datevalues,name)
values('Person','iduser',@idobject,dbo.fGetNowPeriod(),dbo.DateOnly(GetDate()),isnull(@value5,0))

if (@nvalue7<>@value7)
insert into oldvalues(nametable,namecolumn,idobject,idperiod,datevalues,name)
values('Person','RNN',@idobject,dbo.fGetNowPeriod(),dbo.DateOnly(GetDate()),@value7)

if (@nvalue8<>@value8)
insert into oldvalues(nametable,namecolumn,idobject,idperiod,datevalues,name)
values('Person','NumberUDL',@idobject,dbo.fGetNowPeriod(),dbo.DateOnly(GetDate()),@value8)
GO

ALTER TABLE [dbo].[Person] WITH NOCHECK
  ADD CONSTRAINT [FK__Person__IDAddres__245D67DE] FOREIGN KEY ([IDAddress]) REFERENCES [dbo].[Address] ([IDAddress])
GO

ALTER TABLE [dbo].[Person] WITH NOCHECK
  ADD CONSTRAINT [FK_Person_Classifier] FOREIGN KEY ([IDClassifier]) REFERENCES [dbo].[Classifier] ([IDClassifier])
GO

ALTER TABLE [dbo].[Person] WITH NOCHECK
  ADD CONSTRAINT [FK_Person_OwnerShip] FOREIGN KEY ([IDOwnership]) REFERENCES [dbo].[OwnerShip] ([IDOwnership])
GO