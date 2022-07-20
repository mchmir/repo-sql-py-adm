SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO





CREATE PROCEDURE [dbo].[AddIndication] (@Display as float,  @IDGMeter as int) 

AS

begin tran

declare @DateDisplay as datetime
declare @IDIndication int
declare @FactAmount float
declare @IDGobject int
declare @IDPeriod int
declare @LastDisplay float
declare @LastDateDisplay datetime
declare @blErr bit
declare @IDPeriodDisplay int
--
set @blErr=0
--
set @DateDisplay=dbo.dateonly(getdate())
set @LastDisplay=isnull(dbo.fIndicationDisplay (@IDGMeter),0)
set @LastDateDisplay=isnull(dbo.fIndicationDate (@IDGMeter), @DateDisplay)
set @IDPeriodDisplay=isnull(dbo.fGetPeriodDate(@DateDisplay),dbo.fGetNowPeriod())
--
if (@LastDisplay>@Display)
	begin
		set @blErr=1
	end
if (@LastDateDisplay>=@DateDisplay)
	begin
		set @blErr=1
	end
if (dbo.fGetPeriodDate(@LastDateDisplay)=@IDPeriodDisplay)
	begin
		set @blErr=1
	end
--
if (@blErr=0)
	begin
		--iduser - epayuser = 37
		--IDTypeIndication = 4 - от абонента
		insert into Indication (Display, DateDisplay, IDGmeter, IDUser, [DateAdd], Premech, IDTypeIndication)
		values(@Display, @DateDisplay, @IDGMeter, 37, getdate(), 'личный кабинет', 6)

		if @@error<>0 set @blErr = 1

		set @IDIndication=scope_identity()
		set @IDGobject=(select IDGobject from GMeter where IDGMeter=@IDGMeter)
		set @IDPeriod=(select max(IDPeriod) from Period)

		--расчет потребления
		set @FactAmount=0
		if (@LastDisplay>0)
			begin
				set @FactAmount=@Display-@LastDisplay
				if @@error<>0 set @blErr = 1
			end
		--IDTypeFU = 1 - по ПУ
		insert into FactUse (IDPeriod, FactAmount, IDIndication, IDGobject, IDTypeFU)
		values (@IDPEriod, @FactAmount, @IDIndication, @IDGobject, 1)

		if @@error<>0 set @blErr = 1
		----
	end


if @blErr = 0
	commit tran
else
	rollback tran


select @blErr

GO