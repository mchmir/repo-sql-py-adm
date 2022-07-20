SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO









CREATE PROCEDURE [dbo].[spPCGetConsamerPay] (@Account varchar(20))
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
	--select @IdPeriod=[IDPeriod]  FROM [Period] where [Year]=@year and [Month]=@Month
	--declare @IdPeriodPred int
	--select top 1 @IdPeriodPred=dbo.fGetPredPeriodVariable(@IdPeriod)
--select @IdGRU=IdGRU from GObject with (nolock) where IdContract=@IdContract
--select @IDGObject=IDGObject,@IdGRU=IdGRU from GObject with (nolock) where IdContract=@IdContract

SELECT TOP 100
	   d.[IDTypeDocument] 
	   ,d.[DocumentDate]
      ,Round(d.[DocumentAmount],2)
  FROM [Gefest].[dbo].[Document] d
  inner join dbo.Contract c on c.IDContract=d.IDContract
  where d.[IDTypeDocument] in (1,3,5,7,11,13,14,23,24) and c.Account=@Account and d.[DocumentAmount]>0
  order by d.[DocumentDate] desc
 
--gm.IDGObject=@IDGObject

--select @IdContract,@IdPeriod,@IDGObject, @IdGRU--
END








GO