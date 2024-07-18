-- Result 2

select
  CONTRACT.IDCONTRACT                           as IDCONTRACT,
  CONTRACT.ACCOUNT                              as LC,
  GMETER.SERIALNUMBER                           as NGM,
  GMETER.IDGMETER                               as IDGMETER,
  TGM.NAME                                      as G_Model,
  TGM.CLASSACCURACY                             as CL_ACCURACY,
  TGM.SERVICELIFE                               as Period_POV,
  case GMETER.IDSTATUSGMETER
    when 1 then 'ПУ Подключен'
    when 2 then 'ПУ Отключен'
  end                                           as StatusPU,
  convert(varchar, GMETER.DATEFABRICATION, 104) as DateMADE,
  convert(varchar, GMETER.DATEINSTALL, 104 )    as DateINSATLL,
  GMETER.BEGINVALUE                             as Ind_BEGINVALUE,
  convert(varchar, GMETER.DATEVERIFY, 104)      as DatePOV,
  TV.NAME                                       as ResultPOV,
  cast('' as varchar(100))                      as POVERIL,
  GMETER.MEMO                                   as GM_NOTE,
  GMETER.PLOMBNUMBER1                           as PLOMBNUMBER1,
  GMETER.PLOMBNUMBER2                           as PLOMBNUMBER2,
  case
    when GMETER.INDICATIONPLOMB = 0
      and GMETER.IDAGENTPLOMB = 98 then null
      else GMETER.INDICATIONPLOMB end           as INDICATIONPLOMB,
  case
    when GMETER.INDICATIONPLOMB = 0
      and GMETER.IDAGENTPLOMB = 98 then null
      else GMETER.IDAGENTPLOMB end              as IDAGENTPLOMB,
   case
    when GMETER.INDICATIONPLOMB = 0
      and GMETER.IDAGENTPLOMB = 98 then null
      else convert(varchar, GMETER.DATEPLOMB, 104) end  as DatePLOMB
into #tmpTable_R2
from GOBJECT
  join CONTRACT on GOBJECT.IDCONTRACT = CONTRACT.IDCONTRACT
  join GMETER on GOBJECT.IDGOBJECT = GMETER.IDGOBJECT
  join TYPEGMETER as TGM on GMETER.IDTYPEGMETER = TGM.IDTYPEGMETER
  left join TYPEVERIFY TV on GMETER.IDTYPEVERIFY = TV.IDTYPEVERIFY
-- where CONTRACT.ACCOUNT=0291033  -- тест
order by LC;

update #tmpTable_R2
set POVERIL = AGENTNAME
from (
    select A.NAME as AGENTNAME, D.IDCONTRACT as IDCONTRACT, PD2.VALUE as IDGMETER
    from DOCUMENT as D
     left join PD as PD1  on D.IDDOCUMENT = PD1.IDDOCUMENT and PD1.IDTYPEPD = 16
     left join PD as PD2 on D.IDDOCUMENT = PD2.IDDOCUMENT and PD2.IDTYPEPD = 7
     left join AGENT as A on A.IDAGENT = PD1.VALUE
    where D.IDDOCUMENT in (select max(IDDOCUMENT) from DOCUMENT as D2 where IDTYPEDOCUMENT = 22 and D.IDCONTRACT = D2.IDCONTRACT)
     ) as AGENT
  join #tmpTable_R2 as t on t.IDCONTRACT = AGENT.IDCONTRACT and t.IDGMETER = AGENT.IDGMETER;

select
  LC              as [ЛИЦЕВОЙ СЧЕТ],
  NGM             as [Номер счетчика],
  G_Model         as [Модель],
  CL_ACCURACY     as [Класс точности],
  Period_POV      as [Период поверки],
  StatusPU        as [СТАТУС ПУ],
  DateMADE        as [Дата изготовления],
  DateINSATLL     as [Дата установки ПУ],
  Ind_BEGINVALUE  as [Начальные показания],
  DatePOV         as [Дата поверки],
  ResultPOV       as [Результат поверки],
  POVERIL         as [Поверил],
  GM_NOTE         as [Примечание],
  PLOMBNUMBER1    as [Пломба 1],
  PLOMBNUMBER2    as [Пломба 2],
  INDICATIONPLOMB as [Показание],
  AGENT.NAME      as [Исполнитель],
  DatePLOMB       as [Дата пломбы]
from #TMPTABLE_R2 as T
 left join AGENT on T.IDAGENTPLOMB = AGENT.IDAGENT
order by [ЛИЦЕВОЙ СЧЕТ];

drop table #TMPTABLE_R2;


