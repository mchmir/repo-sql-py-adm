SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO


create PROCEDURE [dbo].[spEPAYCreateBatch] AS BEGIN
     SET NOCOUNT ON;

declare @idbatch as int
declare @numbatch as varchar(20)
declare @idperiod as int
declare @bdate as datetime
declare @iddispatcher as int
declare @idcashier as int
set @iddispatcher=125
set @idcashier=125
set @idbatch=0

	set @bdate=(select dbo.DateOnly(GetDate()))
	set @idperiod=(select max(idperiod) from period)
	set @numbatch=(select top 1 convert(varchar, CONVERT(INT,isnull(NumberBatch,0))+1) NumberBatch
	from Batch with (nolock) where idcashier=@idcashier order by CONVERT(INT,NumberBatch) desc)
	insert into batch (idtypepay,idperiod,iddispatcher,idcashier,name,batchcount,batchamount,batchdate,idtypebatch,idstatusbatch,numberbatch)
	values(1,@idperiod,@iddispatcher,@idcashier,'Терминал',0,0,@bdate,1,1,isnull(@numbatch,1))
	insert into cashbalance (AmountBalance,DateCash,IdCashier)
	values (0,@bdate,@idcashier)

END
GO