SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO









CREATE PROCEDURE [dbo].[spPCGetConsamerData] (@Account varchar(20),@year int,@Month int)
AS
BEGIN
	SET NOCOUNT ON;

--declare @IdContract int
declare @IdPeriod int
--declare @IDGObject int
--declare @IdGRU int
--declare @dBegin datetime
--declare @dEnd datetime
--set @dBegin='2005-05-01'
--set @dEnd='2009-02-02'


--select idcontract from contract where account=2851003
--select @IdContract=idcontract from contract where account=2851003

	--SELECT [IDPeriod]  FROM [Gefest].[dbo].[Period] where [Year]=2017 and [Month]=4
	select @IdPeriod=[IDPeriod]  FROM [Period] where [Year]=@year and [Month]=@Month
	declare @IdPeriodPred int
	select top 1 @IdPeriodPred=dbo.fGetPredPeriodVariable(@IdPeriod)
--select @IdGRU=IdGRU from GObject with (nolock) where IdContract=@IdContract
--select @IDGObject=IDGObject,@IdGRU=IdGRU from GObject with (nolock) where IdContract=@IdContract

select isnull(p.NumberPhone,0),sm.Name,tm.Name,tm.ClassAccuracy,gm.SerialNumber,gm.DateFabrication,dateadd(yy, tm.servicelife, case when gm.dateverify='1800-01-01' then gm.datefabrication else gm.dateverify end) dateverify,
isnull(dbo.fIndicationDisplay(gm.IdGMeter),0) LastIndication,dbo.fIndicationDate(gm.IdGMeter) inddate,gm.IDGObject,gm.IdGMeter,c.IdContract,pc.IDPersonalCabinet,pc.PCneedchgPWD

from Contract c
inner join GObject gb with (nolock) on gb.IdContract=c.IdContract
inner join GMeter gm with (nolock) on gb.IDGObject=gm.IDGObject
inner join dbo.StatusGMeter sm on sm.IDStatusGMeter=gm.IDStatusGMeter
inner join dbo.TypeGMeter tm on tm.IDTypeGMeter=gm.IDTypeGMeter
left join dbo.PersonalCabinet pc on pc.account=c.account
left join dbo.Phone p on p.IDPerson=c.IDPerson


where c.account=@Account 
order by  gm.IDStatusGMeter , gm.dateverify desc
 
--gm.IDGObject=@IDGObject

--select @IdContract,@IdPeriod,@IDGObject, @IdGRU--
END








GO