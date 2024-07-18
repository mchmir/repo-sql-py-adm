-- Справочник Причины отключения
-- select  * from TYPEREASONDISCONNECT;
declare @IDPeriod as INTEGER;

set @IDPeriod = dbo.fGetIDPeriodMY(6, 2024);

select
    C.IDCONTRACT,
    DBO.FGETSTATUSGOBJECT(G.IDGOBJECT, @IDPeriod) as STATUSGOPERIOD,
    GM.IDSTATUSGMETER,
    GM.IDGMETER,
    GM.IDTYPEGMETER as GM_TYPEGMETER,
    GRU.IDAGENT as GRU_IDAGENT,
    GRU.IDGRU as GRU_IDGRU,
    DBO.FGETSTATUSPU(DBO.FGETDATEPERIOD(@IDPeriod, 0),GM.IDGMETER) as STATUSPUPERIOD,
    GM.IDTYPEGMETER,
    C.ACCOUNT,
    case G.IDSTATUSGOBJECT
      when 1 then 'ОУ Подключен'
      when 2 then 'ОУ Отключен'
    end as [СТАТУС ОБЪЕКТА],
    GM.SERIALNUMBER,
    case GM.IDSTATUSGMETER
      when 1 then 'ПУ Подключен'
      when 2 then 'ПУ Отключен'
    end as [СТАТУС ПУ],
    TGM.NAME as [Модель],
    GM.MEMO  as GM_NOTE,
    cast(null as int) as TypeReasonDisconnect,
    cast('' as varchar(100)) as ReasonDisconnect,
    cast('' as varchar(100)) as AGENTDONE,
    cast('' as varchar(500)) as DOCUM_NOTE,
    GM.DATEONOFF as DATEOFF,
    cast(null as datetime) as DOCUM_DATE
into #tmpPU
  from CONTRACT as C with (nolock)
    join GOBJECT as G with (nolock) on G.IDCONTRACT = C.IDCONTRACT
    join GRU on G.IDGRU = GRU.IDGRU
    left join GMETER as GM with (nolock) on GM.IDGOBJECT = G.IDGOBJECT
    left join TYPEGMETER as TGM on GM.IDTYPEGMETER = TGM.IDTYPEGMETER;

/*
+--------+-----------------+
|IDTypePD|Name             |
+--------+-----------------+
|7       |Прибор учета (ID)|
+--------+-----------------+
|16      |Слесарь (агент) (ID)|
+--------+--------------------+
|33      |Причина отключения|
+--------+------------------+

+----------------------+---------------------+
|IDTypeReasonDisconnect|Name                 |
+----------------------+---------------------+
|2                     |нет поверки          |
|3                     |нет доступа          |
|4                     |ПУ не работ.         |
|5                     |самовол. откл./подкл.|
|7                     |прочие               |
|11                    |подкл. без ПУ        |
|12                    |ПУ забрак.           |
|13                    |снят на поверку      |
|14                    |нет опломбировки     |
+----------------------+---------------------+


*/

update #tmpPU
set ReasonDisconnect = REASON, TypeReasonDisconnect=IDTYPEREASONDISCONNECT
from (
    select
      D.IDCONTRACT              as IDCONTRACT,
      TR.NAME                   as REASON,
      PD2.VALUE                 as IDGMETER,
      TR.IDTYPEREASONDISCONNECT as IDTYPEREASONDISCONNECT
    from DOCUMENT as D
     left join PD as PD1 on D.IDDOCUMENT = PD1.IDDOCUMENT and PD1.IDTYPEPD = 33
     left join PD as PD2 on D.IDDOCUMENT = PD2.IDDOCUMENT and PD2.IDTYPEPD = 7
     left join TYPEREASONDISCONNECT as TR on TR.IDTYPEREASONDISCONNECT = PD1.VALUE
    where D.IDDOCUMENT in (select max(IDDOCUMENT) from DOCUMENT as D2 where IDTYPEDOCUMENT = 18 and D.IDCONTRACT = D2.IDCONTRACT)
     ) as REASON
  join #tmpPU as t on t.IDCONTRACT = REASON.IDCONTRACT and t.IDGMETER = REASON.IDGMETER;


update #tmpPU
set AGENTDONE = AGENTNAME, DOCUM_NOTE=Doc_NOTE, DOCUM_DATE=DOCUMENTDATE
from (
    select
      A.NAME         as AGENTNAME,
      D.IDCONTRACT   as IDCONTRACT,
      PD2.VALUE      as IDGMETER,
      D.NOTE         as Doc_NOTE,
      D.DOCUMENTDATE as DOCUMENTDATE
    from DOCUMENT as D
     left join PD as PD1  on D.IDDOCUMENT = PD1.IDDOCUMENT and PD1.IDTYPEPD = 16
     left join PD as PD2  on D.IDDOCUMENT = PD2.IDDOCUMENT and PD2.IDTYPEPD = 7
     left join AGENT as A on A.IDAGENT = PD1.VALUE
    where D.IDDOCUMENT in (select max(IDDOCUMENT) from DOCUMENT as D2 where IDTYPEDOCUMENT = 18 and D.IDCONTRACT = D2.IDCONTRACT)
     ) as AGENT
  join #tmpPU as t on t.IDCONTRACT = AGENT.IDCONTRACT and t.IDGMETER = AGENT.IDGMETER;


/*
 1 Не включать РУ, у которых Контролер «Без договора» (кроме РУ №55555),
   эти РУ отключены от газоснабжения и у них в Гефесте нет документа «Отключение ПУ».
   *** GRU.IDAGENT <> 54
 2 Если на л/счете есть ПУ со статусом «Подключен», то такой л/счет не включаем в эту таблицу.
   *** CONTRACT.ACCOUNT not in
 3 Если на л/счете один ПУ со статусом «Отключен» с Маркой «Другие 1,6» (это л/счета без ПУ),
   то такие л/счета не включаем в эту таблицу.
   *** GMETER.IDTYPEGMETER <> 25
*/


select
  ACCOUNT as [Лицевой счет],
  [СТАТУС ОБЪЕКТА],
  SERIALNUMBER as [ПУ],
   case STATUSPUPERIOD
      when 1 then 'ПУ Подключен'
      when 2 then 'ПУ Отключен'
    end as [СТАТУС ПУ на 01.07],
  [СТАТУС ПУ] as [Текущий статус ПУ],
  [Модель],
  convert(varchar, DOCUM_DATE, 104) as [Дата],
  AGENTDONE as [Выполнил],
  REASONDISCONNECT as [Причина],
  DOCUM_NOTE  as [Примечание]
into #tmpPU_2
from #tmpPU
where (GRU_IDAGENT<>54 or GRU_IDGRU=8828)
  and STATUSPUPERIOD=2 and IDTYPEGMETER<>25
 -- тест ЛС
 -- and ACCOUNT in ('0301086', '0311003', '2665030', '1471067','3772052', '0302012', '0391011')
  and ACCOUNT not in (select tC.ACCOUNT
                      from GOBJECT as tGOB
                      join CONTRACT as tC on tGOB.IDCONTRACT = tC.IDCONTRACT
                      join GMETER as tG on tGOB.IDGOBJECT = tG.IDGOBJECT
                      where dbo.fGetStatusPU(dbo.fGetDatePeriod(@IDPeriod, 0),tG.IDGMETER)=1)
order by ACCOUNT;



with CTE as (
  select [Лицевой счет],
         [СТАТУС ОБЪЕКТА],
         [ПУ],
         [СТАТУС ПУ на 01.07],
         [Текущий статус ПУ],
         [Модель],
         [Дата],
         [Выполнил],
         [Причина],
         [Примечание],
         ROW_NUMBER() over (
           partition by [Лицевой счет]
           order by
             case
               when [Дата] is null then 2
               else 0
               end,
             [Дата]
           ) as RN
  from #tmpPU_2
)
select [Лицевой счет],
       [СТАТУС ОБЪЕКТА],
       [ПУ],
       [СТАТУС ПУ на 01.07],
       [Текущий статус ПУ],
       [Модель],
       [Дата],
       [Выполнил],
       [Причина],
       [Примечание]
from CTE
where RN = 1;


drop table #tmpPU;
drop table #tmpPU_2;







/*
select * from DOCUMENT as D
where IDCONTRACT in (select IDCONTRACT from CONTRACT where ACCOUNT='0301086')
and IDDOCUMENT in (select max(IDDOCUMENT) from DOCUMENT as D2 where IDTYPEDOCUMENT = 18 and D.IDCONTRACT = D2.IDCONTRACT)
