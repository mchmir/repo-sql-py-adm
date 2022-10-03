/*declare @IDPeriod int
declare @BDate datetime
declare @EDate datetime
set @IDPeriod=154 
set @Bdate=dbo.fGetDatePeriod(@IDPeriod,1)
set @EDate=dbo.fGetDatePeriod(@IDPeriod+5,2)
select @IDPeriod as IDPeriod
select @BDate
select @EDate
declare @Q as VarChar(1000)

select count(idlogs) as countlogs from Logs where DataLog>=@BDate and DataLog<=@EDate

delete from Logs where DataLog>=@BDate and DataLog<=@EDate
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)

*/
declare @IDPeriod int
declare @BDate datetime
declare @Q as VarChar(1000)

set @IDPeriod=172
set @Bdate=dbo.fGetDatePeriod(@IDPeriod,1)

SELECT dbo.fGetDatePeriod(@IDPeriod,1)
select count(idlogs) as countlogs from Logs where DataLog<@BDate

delete from Logs where DataLog<@BDate

set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)