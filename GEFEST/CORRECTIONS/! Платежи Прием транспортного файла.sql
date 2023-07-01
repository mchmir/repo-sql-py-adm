-- AUTODOCUMENT ->  AUTOBATCH -> BATCH


select * from AUTOBATCH
order by IDAUTOBATCH desc;
+-----------+-------+-----------------------+----------+-----------+-------------------------------------------------------------------+
|IDAutoBatch|IDBatch|BatchDate              |BatchCount|BatchAmount|Path                                                               |
+-----------+-------+-----------------------+----------+-----------+-------------------------------------------------------------------+
|37327      |NULL   |2023-06-22 00:00:00.000|1644      |0          |21.06.23_22.06.23_1644_2273670.9500_160_vse_23.06.2023_10_38_48.csv|
+-----------+-------+-----------------------+----------+-----------+-------------------------------------------------------------------+

declare @IDAUTOBATCH int;

set @IDAUTOBATCH = 37340;

select * from AUTODOCUMENT
where IDAUTOBATCH = @IDAUTOBATCH;

-- проверим нет ли проведенных документов
select 'AUTODOCUMENT', count(*) from AUTODOCUMENT
where IDAUTOBATCH = @IDAUTOBATCH
union
select 'AUTODOCUMENT_IDDOC null', count(*) from AUTODOCUMENT
where IDAUTOBATCH = @IDAUTOBATCH and IDDOCUMENT is null;

delete AUTODOCUMENT OUTPUT DELETED.* where IDAUTOBATCH = @IDAUTOBATCH;
delete AUTOBATCH OUTPUT DELETED.* where IDAUTOBATCH = @IDAUTOBATCH;

