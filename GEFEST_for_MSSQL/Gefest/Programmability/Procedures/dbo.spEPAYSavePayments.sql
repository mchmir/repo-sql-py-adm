SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO





CREATE PROCEDURE [dbo].[spEPAYSavePayments](@Account int,@AmmountPay float,@TerminalData varchar(20),@DatePay datetime,@IDDispatcher int) AS BEGIN
     SET NOCOUNT ON;

declare @pIDPeriod int
declare @pIDBatch int
declare @pIDContract int
declare @pIDUser int
declare @pDocumentNumber int
declare @DocumentDate datetime
declare @bdate datetime
--declare @idperiod int
declare @idcashier int
declare @blerr bit
declare @AmountVDGO float
declare @BatchName varchar(100)
declare @numbatch as varchar(20)

--declare @IDDispatcher int
--set @IDDispatcher=125 -- платежная система

select @pIDPeriod=max(IDPeriod) from dbo.Period
--IDDispatcher=125 - платежная система
--select @pIDBatch=max(IDBatch)
--from dbo.Batch
--where IDPeriod=@pIDPeriod and IDTypeBatch=1 and IDDispatcher=125

select @pIDContract=IDContract
from dbo.[Contract]
where Account=@Account

set @DocumentDate=@DatePay
--IDUser=37 - epayuser
set @pIDUser=37

--проверяем, есть ли открытая пачка для агента за день оплаты
set @pIDBatch=0
select @pIDBatch=idbatch from batch where IDDispatcher=@IDDispatcher
and batchdate=dbo.DateOnly(@DatePay) --and idstatusbatch=1

if isnull(@pIDBatch,0)=0
begin
	set @bdate=dbo.DateOnly(@DatePay) --(select dbo.DateOnly(GetDate()))
	--set @idperiod=(select max(idperiod) from period)
	if day(@DatePay)=1
		begin
			set	@numbatch=(select isnull(NumberBatch,'1') 
			from Agent with (nolock) where IDAgent=@IDDispatcher)
		end
	else
		begin
			set	@numbatch=(select top 1 convert(varchar, CONVERT(INT,isnull(NumberBatch,0))+1) NumberBatch
			from Batch with (nolock) where IDDispatcher=@IDDispatcher order by idbatch desc)
		end
	if @IDDispatcher=125
		begin
			set @idcashier=@IDDispatcher
			insert into batch (idtypepay,idperiod,iddispatcher,idcashier,name,batchcount,batchamount,batchdate,idtypebatch,idstatusbatch,numberbatch)
			values(1,@pIDPeriod,@iddispatcher,@idcashier,'Терминал',0,0,@bdate,1,1,isnull(@numbatch,1))
			
			set @pIDBatch=scope_identity()
			
			insert into cashbalance (AmountBalance,DateCash,IdCashier)
			values (0,@bdate,@idcashier)
		end
	else
		begin
			select @BatchName=name from agent where idagent=@IDDispatcher
			set @BatchName=@BatchName+'_онлайн_'+@numbatch
			insert into batch (idtypepay,idperiod,iddispatcher,name,batchcount,batchamount,batchdate,idtypebatch,idstatusbatch,numberbatch)
			values(2,@pIDPeriod,@iddispatcher,@BatchName,0,0,@bdate,1,1,isnull(@numbatch,1))

			set @pIDBatch=scope_identity()
		end

end
--

select @pDocumentNumber=BatchCount+1
from dbo.Batch
where IDBatch=@pIDBatch
--if @IDDispatcher=125
--	begin
		exec DocumentPayTerminal @pIDPeriod,@pIDBatch,@pIDContract,1,@DocumentDate,@AmmountPay,0,1,@pIDUser,0,0,0,@pDocumentNumber,'',@blerr,@AmountVDGO,@TerminalData
--	end
--else
--	begin
--		exec DocumentPay @pIDPeriod,@pIDBatch,@pIDContract,1,@DocumentDate,@AmmountPay,0,1,@pIDUser,0,0,0,@pDocumentNumber,'',@blerr,@AmountVDGO
--	end
END




GO