declare @IDPeriod int
declare @BDate datetime
declare @EDate datetime
set @IDPeriod=43-- 48-������ 2009 ����
set @Bdate=dbo.fGetDatePeriod(@IDPeriod,1)
set @EDate=dbo.fGetDatePeriod(@IDPeriod,2)
select @IDPeriod as IDPeriod
select @BDate
select @EDate
declare @Q as VarChar(1000)

--������� �����������
delete fu 
from factuse fu 
inner join operation o on fu.idoperation=o.idoperation
inner join document d on d.iddocument=o.iddocument
and d.idperiod=@IDPeriod
select '������� ����������� � ����������'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--������� ��������
delete  o from 
operation o 
inner join document d on d.iddocument=o.iddocument
and d.idperiod=@IDPeriod
select '������� ��������'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--������� ��������� ����������
delete pd from 
pd
inner join document d on d.iddocument=pd.iddocument
and d.idperiod=@IDPeriod
select '������� ��������� ����������'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--������� ���������
delete from document where idperiod=@IDPeriod
select '������� ���������'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--������� ��������� �����
delete pb from 
pb
inner join batch b on b.idbatch=pb.idbatch
and b.idperiod=@IDPeriod
select '������� ��������� �����'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--������� �������������
delete ad 
from AutoDocument ad
inner join AutoBatch ab on ad.IDAutoBatch=ab.IDAutoBatch
where BatchDate>=@BDate and BatchDate<=@EDate
--������� �������������
delete ad 
from AutoDocument ad
inner join AutoBatch ab on ad.IDAutoBatch=ab.IDAutoBatch
inner join batch b on ab.IDBatch=b.idbatch
where b.idperiod=@IDPeriod
select '������� �������������'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--������� ���������
delete ab 
from AutoBatch ab
inner join batch b on ab.IDBatch=b.idbatch
where b.idperiod=@IDPeriod
select '������� ��������� �� ������'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--������� �����
delete from batch where idperiod=@IDPeriod
select '������� �����'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--������� ���������
delete from AutoBatch 
where BatchDate>=@BDate and BatchDate<=@EDate
select '������� ���������'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--������� �����������
delete from factuse where idperiod=@IDPeriod
select '������� �����������'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--������� �������
delete from balance where idperiod=@IDPeriod
delete from balancereal where idperiod=@IDPeriod
select '������� �������'
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
--������� �������� ������
delete from CashBalance where DateCash>=@BDate and DateCash<=@EDate
--������� ���������
delete from Indication where DateDisplay>=@BDate and DateDisplay<=@EDate
set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
Exec(@Q)
set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
Exec(@Q)
