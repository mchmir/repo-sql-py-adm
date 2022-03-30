declare @IDPeriod int
declare @BDate datetime
declare @EDate datetime
set @IDPeriod=43-- 48-€нварь 2009 года
set @Bdate=dbo.fGetDatePeriod(@IDPeriod,1)
set @EDate=dbo.fGetDatePeriod(@IDPeriod,2)
select @IDPeriod as IDPeriod
select @BDate
select @EDate
declare @Q as VarChar(1000)

--удал€ем потребление
delete fu 
from factuse fu 
inner join operation o on fu.idoperation=o.idoperation
inner join document d on d.iddocument=o.iddocument
and d.idperiod=@IDPeriod
select '”далили потребление с операци€ми'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--удал€ем операции
delete  o from 
operation o 
inner join document d on d.iddocument=o.iddocument
and d.idperiod=@IDPeriod
select '”далили операции'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--удал€ем параметры документов
delete pd from 
pd
inner join document d on d.iddocument=pd.iddocument
and d.idperiod=@IDPeriod
select '”далили параметры документов'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--удал€ем документы
delete from document where idperiod=@IDPeriod
select '”далили документы'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--удал€ем параметры пачек
delete pb from 
pb
inner join batch b on b.idbatch=pb.idbatch
and b.idperiod=@IDPeriod
select '”далили параметры пачек'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--удал€ем автодокументы
delete ad 
from AutoDocument ad
inner join AutoBatch ab on ad.IDAutoBatch=ab.IDAutoBatch
where BatchDate>=@BDate and BatchDate<=@EDate
--удал€ем автодокументы
delete ad 
from AutoDocument ad
inner join AutoBatch ab on ad.IDAutoBatch=ab.IDAutoBatch
inner join batch b on ab.IDBatch=b.idbatch
where b.idperiod=@IDPeriod
select '”далили автодокументы'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--удал€ем автопачки
delete ab 
from AutoBatch ab
inner join batch b on ab.IDBatch=b.idbatch
where b.idperiod=@IDPeriod
select '”далили автопачки по пачкам'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--удал€ем пачки
delete from batch where idperiod=@IDPeriod
select '”далили пачки'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--удал€ем автопачки
delete from AutoBatch 
where BatchDate>=@BDate and BatchDate<=@EDate
select '”далили автопачки'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--удал€ем потребление
delete from factuse where idperiod=@IDPeriod
select '”далили потребление'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--удал€ем балансы
delete from balance where idperiod=@IDPeriod
delete from balancereal where idperiod=@IDPeriod
select '”далили балансы'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--удал€ем наличный баланс
delete from CashBalance where DateCash>=@BDate and DateCash<=@EDate
--удал€ем показани€
delete from Indication where DateDisplay>=@BDate and DateDisplay<=@EDate
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
