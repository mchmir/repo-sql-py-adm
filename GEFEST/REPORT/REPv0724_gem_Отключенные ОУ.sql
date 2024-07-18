-- Справочник Причины отключения
-- select  * from TYPEREASONDISCONNECT;

declare @IDPeriod as INTEGER;

set @IDPeriod = dbo.fGetIDPeriodMY(6, 2024);

select
    G.IDGOBJECT                          as IDGOBJECT,
    C.IDCONTRACT                         as IDCONTRACT,
    DBO.FGETSTATUSGOBJECT(
      G.IDGOBJECT,
      @IDPeriod)                as STATUSGOPERIOD,
    GRU.IDAGENT                          as GRU_IDAGENT,
    GRU.IDGRU                            as GRU_IDGRU,
    C.ACCOUNT                            as ACCOUNT,
    case G.IDSTATUSGOBJECT
      when 1 then 'ОУ Подключен'
      when 2 then 'ОУ Отключен'
    end                                  as CURRENT_STATUSGO,
    cast(null as int)                    as IDTypeEND,
    cast('' as varchar(100))             as TypeEND,
    cast('' as varchar(100))             as AGENTDONE,
    cast('' as varchar(500))             as DOCUM_NOTE,
    cast(null as datetime)               as DOCUM_DATE
into #tmpPU
  from CONTRACT  as C with (nolock)
         join GOBJECT    as G  with (nolock) on G.IDCONTRACT = C.IDCONTRACT
         join GRU                            on G.IDGRU = GRU.IDGRU
;

/*

+--------------+------------------------+
|IDTypeDocument|Name                    |
+--------------+------------------------+
|17            |Отключение объекта учета|
+--------------+------------------------+

+--------+--------------------+
|IDTypePD|Name                |
+--------+--------------------+
|14      |Тип окончания       |  -- Тип отключения
|15      |Объект учета (ID)   |  -- IDGOBJECT
|16      |Слесарь (агент) (ID)|  -- IDAGENT
+--------+--------------------+

+---------+------------------+
|IDTypeEnd|Name              |
+---------+------------------+
|1        |На сварку         |
|2        |Стакан заглушка   |
|3        |Заглушка          |
|4        |Заглушка без крана|
+---------+------------------+

*/

update #tmpPU
set TYPEEND = TYPEENDPD
from (
    select
      D.IDCONTRACT              as IDCONTRACT,
      TE.NAME                   as TYPEENDPD,
      PD2.VALUE                 as IDGOBJECT
    from DOCUMENT as D
     left join PD as PD1 on D.IDDOCUMENT = PD1.IDDOCUMENT and PD1.IDTYPEPD = 14
     left join PD as PD2 on D.IDDOCUMENT = PD2.IDDOCUMENT and PD2.IDTYPEPD = 15
     left join TYPEEND as TE on TE.IDTYPEEND = PD1.VALUE
     -- 17 - Документ Отключение объекта учета
    where D.IDDOCUMENT in (select max(IDDOCUMENT)
                           from DOCUMENT as D2
                           where IDTYPEDOCUMENT = 17
                             and D.IDCONTRACT = D2.IDCONTRACT)
     ) as TYPEEND
  join #tmpPU as t on t.IDCONTRACT = TYPEEND.IDCONTRACT and t.IDGOBJECT = TYPEEND.IDGOBJECT;


update #tmpPU
set AGENTDONE = AGENTNAME, DOCUM_NOTE=Doc_NOTE, DOCUM_DATE=DOCUMENTDATE
from (
    select
      A.NAME         as AGENTNAME,
      D.IDCONTRACT   as IDCONTRACT,
      PD2.VALUE      as IDGOBJECT,
      D.NOTE         as Doc_NOTE,
      D.DOCUMENTDATE as DOCUMENTDATE
    from DOCUMENT as D
     left join PD as PD1  on D.IDDOCUMENT = PD1.IDDOCUMENT and PD1.IDTYPEPD = 16
     left join PD as PD2 on D.IDDOCUMENT = PD2.IDDOCUMENT and PD2.IDTYPEPD = 15
     left join AGENT as A on A.IDAGENT = PD1.VALUE
    where D.IDDOCUMENT in (select max(IDDOCUMENT) from DOCUMENT as D2 where IDTYPEDOCUMENT = 17 and D.IDCONTRACT = D2.IDCONTRACT)
     ) as AGENT
  join #tmpPU as t on t.IDCONTRACT = AGENT.IDCONTRACT and t.IDGOBJECT = AGENT.IDGOBJECT;


/*
 1 Не включать РУ, у которых Контролер «Без договора» (кроме РУ №55555 IDGRU=8828),
   эти РУ отключены от газоснабжения и у них в Гефесте нет документа «Отключение ПУ».
   *** GRU.IDAGENT <> 54
*/


select
  ACCOUNT                           as [Лицевой счет],
  CURRENT_STATUSGO                  as [Текущий статус ОУ],
  case STATUSGOPERIOD
      when 1 then 'ОУ Подключен'
      when 2 then 'ОУ Отключен'
    end                             as [СТАТУС ОУ на 01.07],
  convert(varchar, DOCUM_DATE, 104) as [Дата],
  AGENTDONE                         as [Выполнил],
  TypeEND                           as [Тип отключения],
  DOCUM_NOTE                        as [Примечание]
from #tmpPU
where (GRU_IDAGENT <> 54 or GRU_IDGRU = 8828)
  and STATUSGOPERIOD = 2
 -- тест ЛС
 -- and ACCOUNT in ('3772052', '3091095', '1477047')
order by [Лицевой счет]
;


drop table #tmpPU;


/*
select * from GOBJECT where IDCONTRACT in (select IDContract from CONTRACT where ACCOUNT=3772052)
select * from PD where IDTYPEPD=15 and VALUE='900445';
select * from PD where IDDOCUMENT=24598541;
select * from DOCUMENT where IDDOCUMENT=24598539;
select * from GRU where IDAGENT=54;

select * from GOBJECT where IDCONTRACT in (select IDContract from CONTRACT where ACCOUNT=3772052)
900212

select * from DOCUMENT where idcontract=900212 and idtypedocument=18;



 */