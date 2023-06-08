select IDIndication, DateDisplay, IdGMeter, IDAgent
  from Indication as I
 where IDAgent = 123
   and DateDisplay > '2023-05-31'
order by DateDisplay desc;

+------------+-----------------------+--------+-------+
|IDIndication|DateDisplay            |IdGMeter|IDAgent|
+------------+-----------------------+--------+-------+
|11233784    |2023-06-02 00:00:00.000|813272  |123    |
|11233772    |2023-06-02 00:00:00.000|805317  |123    |
|11233776    |2023-06-02 00:00:00.000|797768  |123    |
|11233791    |2023-06-02 00:00:00.000|774687  |123    |
|11233782    |2023-06-02 00:00:00.000|729273  |123    |
+------------+-----------------------+--------+-------+

select c.Account, g.IDGMeter
from GMeter as G
join GObject as O on G.IDGObject = O.IDGObject
join Contract as C on O.IDContract = C.IDContract
where g.IDGMeter in (813272,805317,797768,774687,729273);

+-------+--------+
|Account|IDGMeter|
+-------+--------+
|2021097|729273  |
|1516004|774687  |
|3511012|797768  |
|1491170|805317  |
|2021097|813272  |
+-------+--------+


------------------------------------------------------------------------------------------------------------------------
BEGIN TRANSACTION;
    select IDIndication, DateDisplay, IdGMeter, IDAgent
      into #Agent123
      from Indication as I
     where IDAgent = 123
       and DateDisplay <='2023-05-31'
    order by DateDisplay desc;

    select IDIndication, DateDisplay, IdGMeter, IDAgent
      into #Agent137
      from Indication as I
     where IDAgent = 137
       and DateDisplay <='2023-05-31'
    order by DateDisplay desc;

    update Indication set IDAgent = 137
    where exists (
        select 1
          from #Agent123 as A123
         where Indication.IDIndication = A123.IDIndication
           and Indication.DateDisplay = A123.DateDisplay
           and Indication.IdGMeter = A123.IdGMeter
    );

    update Indication set IDAgent = 123
    where exists (
        select 1
          from #Agent137 as A137
         where Indication.IDIndication = A137.IDIndication
           and Indication.DateDisplay = A137.DateDisplay
           and Indication.IdGMeter = A137.IdGMeter
    );

    select * from #Agent123;
    select * from #Agent137;

    drop table #Agent123;
    drop table #Agent137;

COMMIT TRANSACTION;

-----------------------------------------------------------------------------------------------------------------------
-- 1. Обновление установки пломб
select * from GMeter as G
 where IDAgentPlomb = 137
   and IDTypePlombWork = 2
   and DatePlomb > '2023-05-31'
order by DatePlomb Desc;

BEGIN TRANSACTION;
    select IDGMeter, PlombNumber1, PlombNumber2
    into #FilteredGMeter
    from GMeter
    where IDAgentPlomb = 137
      and IDTypePlombWork = 1
      and DatePlomb <= '2023-05-31';

    update GMeterPlombHistory
    set IDAgentPlomb = 123
    where exists (
        select 1
          from #FilteredGMeter as FGM
         where GMeterPlombHistory.IDGMeter = FGM.IDGMeter
           and GMeterPlombHistory.PlombNumber1 = FGM.PlombNumber1
           and GMeterPlombHistory.PlombNumber2 = FGM.PlombNumber2
    );

    update GMeter
    set IDAgentPlomb = 123
    where exists (
        select 1
          from #FilteredGMeter as FGM
         where GMeter.IDGMeter = FGM.IDGMeter
           and GMeter.PlombNumber1 = FGM.PlombNumber1
           and GMeter.PlombNumber2 = FGM.PlombNumber2
    );
DROP TABLE #FilteredGMeter;
COMMIT TRANSACTION;
-------------------------------------------------------------
BEGIN TRANSACTION;
    select IDGMeter
    into #FilteredGMeter
    from GMeter
    where IDAgentPlomb = 137
      and IDTypePlombWork = 2
      and DatePlomb <= '2023-05-31';

    update GMeterPlombHistory
    set IDAgentPlomb = 123
    where exists (
        select 1
          from #FilteredGMeter as FGM
         where GMeterPlombHistory.IDGMeter = FGM.IDGMeter
    );

    update GMeter
    set IDAgentPlomb = 123
    where exists (
        select 1
          from #FilteredGMeter as FGM
         where GMeter.IDGMeter = FGM.IDGMeter
    );
DROP TABLE #FilteredGMeter;
COMMIT TRANSACTION;

------------------------------------------------------------------------------------------------------------------------
select * from TypeDocument
select * from TypePD
select * from Document where IDTypeDocument = 24 and IDPeriod = 221
select * from PD where IDDocument = 23223617

select * from TypeDocumentVDGO

select * from PD where IDTypePD = 16 and value = 137 order by IDDocument DESC;

-- IDDocument = 23161729 IDPeriod > 220
select D.IDDocument, D.IDPeriod, D.IDTypeDocument, D.DocumentDate, D.Note, PD.IDTypePD, PD.Value
from Document as D
join PD as pd on D.IDDocument = pd.IDDocument
where  PD.IDTypePD = 16 and PD.value = 137
order by D.IDPeriod DESC, D.IDDocument DESC
;

select PD.IDPD, D.IDDocument, D.IDPeriod, D.IDTypeDocument, D.DocumentDate, D.Note, PD.IDTypePD, PD.Value
from Document as D
join PD as pd on D.IDDocument = pd.IDDocument
where PD.IDTypePD = 16
  and PD.value = 137
  and D.IDTypeDocument = 24
  and D.IDPeriod < 221
order by D.IDPeriod DESC, D.IDDocument DESC
;

select * from Document where IDDocument=23110122

select *  from PD
where IDPD in (select PD.IDPD
from Document as D
join PD as pd on D.IDDocument = pd.IDDocument
where PD.IDTypePD = 16
  and PD.value = 123
  and D.IDTypeDocument = 24
  and D.IDPeriod = 220
);

Select * from Agent where Name like'%Газтех%';


--Update Agent set Name = 'ТОО "ГазТехмонтаж-Эксплуатация"' where IDAgent = 123;
--Update Agent set Name = 'ТОО "ГазТехмонтаж"' where IDAgent = 137;

update PD set Value = 123
where IDPD in (select PD.IDPD
from Document as D
join PD as pd on D.IDDocument = pd.IDDocument
where PD.IDTypePD = 16
  and PD.value = 137
  and D.IDTypeDocument = 24
  and D.IDPeriod < 221
);
------------------------------------------------------------------------------------------------------------------------
-- 1. Обновление установки пломб
select * from GMeter as G
 where IDAgentPlomb = 137
   and IDTypePlombWork = 1
   and DatePlomb <='2023-05-31'
order by DatePlomb Desc;

WITH FilteredGMeter AS (
    SELECT IDGMeter, PlombNumber1, PlombNumber2
    FROM GMeter
    WHERE IDAgentPlomb = 137
      AND IDTypePlombWork = 1
      AND DatePlomb <= '2023-05-31'
)
select *
from GMeter
where IDGMeter in (SELECT IDGMeter FROM FilteredGMeter)
  and PlombNumber1 in (SELECT PlombNumber1 FROM FilteredGMeter)
  and PlombNumber2 in (SELECT PlombNumber2 FROM FilteredGMeter);

-- Update
BEGIN TRANSACTION;
WITH FilteredGMeter AS (
    SELECT IDGMeter, PlombNumber1, PlombNumber2
    FROM GMeter
    WHERE IDAgentPlomb = 137
      AND IDTypePlombWork = 1
      AND DatePlomb <= '2023-05-31'
)
update GMeter
set IDAgentPlomb = 123
where IDGMeter in (SELECT IDGMeter FROM FilteredGMeter)
  and PlombNumber1 in (SELECT PlombNumber1 FROM FilteredGMeter)
  and PlombNumber2 in (SELECT PlombNumber2 FROM FilteredGMeter)

update GMeterPlombHistory
set IDAgentPlomb = 123
where IDGMeter in (SELECT IDGMeter FROM FilteredGMeter)
  and PlombNumber1 in (SELECT PlombNumber1 FROM FilteredGMeter)
  and PlombNumber2 in (SELECT PlombNumber2 FROM FilteredGMeter)
COMMIT TRANSACTION;
----------------------------------------------------------------------------------------------------------

select * from GMeterPlombHistory where IDGMeter=716595