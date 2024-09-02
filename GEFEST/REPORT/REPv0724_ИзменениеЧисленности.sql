/*
Документ изменение численности Type=2

+--------+--------------------+
|IDTypePD|Name                |
+--------+--------------------+
|5       |Старое значение чел.|
|6       |Дата окончания      |
+--------+--------------------+

*/
declare
  @MONTH    as int,
  @YEAR     as int,
  @IDPERIOD as int;

------- INData ---------------------------------------------------
set @MONTH = 7;
set @YEAR = 2024;
set @IDPERIOD = dbo.fGetIDPeriodMY(@MONTH, @YEAR);
------------------------------------------------------------------


select count(*) as [Количество документов]
from DOCUMENT as D
where D.IDTYPEDOCUMENT = 2
  and D.IDPERIOD = @IDPERIOD
;

select
  C.ACCOUNT                                               as [Лицевой счет],
  G.COUNTLIVES                                            as [Новая численность],
  dbo.fGetPDValue(D.IDDOCUMENT, 5) as [Старое значение],
  dbo.fGetPDValue(D.IDDOCUMENT, 6) as [По период],
  convert(varchar, D.DOCUMENTDATE, 104)                   as [Дата документа],
  D.NOTE                                                  as [Примечание],
  dbo.fGetUser(D.IDUSER)                           as [Автор],
  convert(varchar, D.DATEADD, 104)                        as [Дата добавления]
into #temp_ONE
from CONTRACT as C
  join GOBJECT as G on C.IDCONTRACT = G.IDCONTRACT
  join DOCUMENT as D on C.IDCONTRACT = D.IDCONTRACT and D.IDTYPEDOCUMENT = 2
where D.IDPERIOD =  @IDPERIOD
order by D.DOCUMENTDATE desc
;

select *
from #temp_ONE
;

drop table #temp_ONE;



------------------------------------------------------------------------------------------------------------------------



/*

select * from PD
where IDDOCUMENT=24333872;