USE [msdb]
GO
/****** Object:  Job [CreateBatchForTerminal]    Script Date: 01/04/2023 20:45:31 ******/
BEGIN TRANSACTION
DECLARE @ReturnCode INT
SELECT @ReturnCode = 0
/****** Object:  JobCategory [[Uncategorized (Local)]]]    Script Date: 01/04/2023 20:45:31 ******/
IF NOT EXISTS (SELECT name FROM msdb.dbo.syscategories WHERE name=N'[Uncategorized (Local)]' AND category_class=1)
BEGIN
EXEC @ReturnCode = msdb.dbo.sp_add_category @class=N'JOB', @type=N'LOCAL', @name=N'[Uncategorized (Local)]'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback

END

DECLARE @jobId BINARY(16)
EXEC @ReturnCode =  msdb.dbo.sp_add_job @job_name=N'CreateBatchForTerminal', 
    @enabled=1, 
    @notify_level_eventlog=0, 
    @notify_level_email=0, 
    @notify_level_netsend=0, 
    @notify_level_page=0, 
    @delete_level=0, 
    @description=N'No description available.', 
    @category_name=N'[Uncategorized (Local)]', 
    @owner_login_name=N'sa', @job_id = @jobId OUTPUT
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [MyStep]    Script Date: 01/04/2023 20:45:31 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'MyStep', 
    @step_id=1, 
    @cmdexec_success_code=0, 
    @on_success_action=1, 
    @on_success_step_id=0, 
    @on_fail_action=2, 
    @on_fail_step_id=0, 
    @retry_attempts=0, 
    @retry_interval=0, 
    @os_run_priority=0, @subsystem=N'TSQL', 
    @command=N'update batch set idstatusbatch=2 where idstatusbatch=1

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
values(1,@pIDPeriod,@iddispatcher,@idcashier,''Терминал'',0,0,@bdate,1,1,isnull(@numbatch,1))
insert into cashbalance (AmountBalance,DateCash,IdCashier)
values (0,@bdate,@idcashier)
', 
    @database_name=N'Gefest', 
    @flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'MyShedule', 
    @enabled=1, 
    @freq_type=4, 
    @freq_interval=1, 
    @freq_subday_type=1, 
    @freq_subday_interval=0, 
    @freq_relative_interval=0, 
    @freq_recurrence_factor=0, 
    @active_start_date=20140110, 
    @active_end_date=99991231, 
    @active_start_time=1, 
    @active_end_time=235959
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'(local)'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
COMMIT TRANSACTION
GOTO EndSave
QuitWithRollback:
  IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION
EndSave:
