declare @IDPeriod int
declare @BDate datetime
declare @EDate datetime
set @IDPeriod=224
set @Bdate=dbo.fGetDatePeriod(@IDPeriod,1)
set @EDate=dbo.fGetDatePeriod(@IDPeriod,2)
select @IDPeriod as IDPeriod
select @BDate
select @EDate
declare @Q as VarChar(1000)

select count(idlogs) as countlogs from Logs where DataLog<=@EDate

delete from Logs where  DataLog<=@EDate
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)



--select min(DataLog) from logs
--select count(*) from logs
--
--select * from period