update batch set idstatusbatch=2 where idstatusbatch=1

declare @pIDUser int
declare @pIDPeriod int
declare @IDDispatcher int
declare @idcashier int
declare @AmountVDGO float
declare @numbatch as varchar(20)
declare @bdate datetime

set @IDDispatcher=125 -- платежная система
select @pIDPeriod=max(IDPeriod) from dbo.Period

set @pIDUser=37
set @bdate=(select dbo.DateOnly(GetDate()))
set @numbatch=(select top 1 convert(varchar, CONVERT(INT,isnull(NumberBatch,0))+1) NumberBatch
from Batch with (nolock) where IDDispatcher=@IDDispatcher order by idbatch desc)
set @idcashier=@IDDispatcher
insert into batch (idtypepay,idperiod,iddispatcher,idcashier,name,batchcount,batchamount,batchdate,idtypebatch,idstatusbatch,numberbatch)
values(1,@pIDPeriod,@iddispatcher,@idcashier,'Терминал',0,0,@bdate,1,1,isnull(@numbatch,1))
insert into cashbalance (AmountBalance,DateCash,IdCashier)
values (0,@bdate,@idcashier)