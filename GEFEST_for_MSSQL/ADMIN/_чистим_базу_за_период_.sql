declare @IDPeriod int
declare @BDate datetime
declare @EDate datetime
set @IDPeriod=49 
set @Bdate=dbo.fGetDatePeriod(@IDPeriod,1)
set @EDate=dbo.fGetDatePeriod(@IDPeriod,2)
select @IDPeriod as IDPeriod
select @BDate
select @EDate
declare @Q as VarChar(1000)

--удаляем потребление
delete fu 
from factuse fu 
inner join operation o on fu.idoperation=o.idoperation
inner join document d on d.iddocument=o.iddocument
and d.idperiod=@IDPeriod
select 'Удалили потребление с операциями'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--удаляем операции
delete  o from 
operation o 
inner join document d on d.iddocument=o.iddocument
and d.idperiod=@IDPeriod
select 'Удалили операции'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--удаляем параметры документов
delete pd from 
pd
inner join document d on d.iddocument=pd.iddocument
and d.idperiod=@IDPeriod
select 'Удалили параметры документов'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--удаляем документы
delete from document where idperiod=@IDPeriod
select 'Удалили документы'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--удаляем параметры пачек
delete pb from 
pb
inner join batch b on b.idbatch=pb.idbatch
and b.idperiod=@IDPeriod
select 'Удалили параметры пачек'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--удаляем автодокументы
delete ad 
from AutoDocument ad
inner join AutoBatch ab on ad.IDAutoBatch=ab.IDAutoBatch
where BatchDate>=@BDate and BatchDate<=@EDate
--удаляем автодокументы
delete ad 
from AutoDocument ad
inner join AutoBatch ab on ad.IDAutoBatch=ab.IDAutoBatch
inner join batch b on ab.IDBatch=b.idbatch
where b.idperiod=@IDPeriod
select 'Удалили автодокументы'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--удаляем автопачки
delete ab 
from AutoBatch ab
inner join batch b on ab.IDBatch=b.idbatch
where b.idperiod=@IDPeriod
select 'Удалили автопачки по пачкам'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--удаляем пачки
delete from batch where idperiod=@IDPeriod
select 'Удалили пачки'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--удаляем автопачки
delete from AutoBatch 
where BatchDate>=@BDate and BatchDate<=@EDate
select 'Удалили автопачки'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--удаляем потребление
delete from factuse where idperiod=@IDPeriod
select 'Удалили потребление'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--удаляем балансы
delete from balance where idperiod=@IDPeriod
delete from balancereal where idperiod=@IDPeriod
select 'Удалили балансы'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--удаляем наличный баланс
delete from CashBalance where DateCash>=@BDate and DateCash<=@EDate
--удаляем показания
delete from Indication where DateDisplay>=@BDate and DateDisplay<=@EDate
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)