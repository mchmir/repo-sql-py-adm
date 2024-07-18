/*
exec  dbo.repReferenceAddingNotAverage 233;
*/
------------------------------------------------------------------------------------------------------------------------
select
    C.IDCONTRACT,
    DBO.FGETSTATUSGOBJECT(G.IDGOBJECT, 233) as STATUSGOPERIOD,
    GM.IDSTATUSGMETER,
    GM.IDGMETER,
    DBO.FGETSTATUSPU(DBO.FGETDATEPERIOD(233, 0),GM.IDGMETER) as STATUSPUPERIOD,
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
    cast('' as varchar(100)) as ReasonDisconnect
into #tmpPU
  from CONTRACT as C with (nolock)
    join GOBJECT as G with (nolock) on G.IDCONTRACT = C.IDCONTRACT
    left join GMETER as GM with (nolock) on GM.IDGOBJECT = G.IDGOBJECT
    left join TYPEGMETER as TGM on GM.IDTYPEGMETER = TGM.IDTYPEGMETER;

/*
статус ОУ «Подключен»,
статус ПУ «Отключен»
марка ПУ  «***Другие 1,6»
*/
select ACCOUNT, [СТАТУС ОБЪЕКТА], SERIALNUMBER, [СТАТУС ПУ], [Модель]
from #tmpPU
where STATUSGOPERIOD=1
  and (STATUSPUPERIOD=2 or (STATUSPUPERIOD=1 and IDTYPEGMETER=25))
order by ACCOUNT;

---
select ACCOUNT, [СТАТУС ОБЪЕКТА], SERIALNUMBER, [СТАТУС ПУ], [Модель]
from #tmpPU
where STATUSGOPERIOD=1
  and [Модель] is null or IDSTATUSGMETER is null
order by [СТАТУС ОБЪЕКТА] desc, ACCOUNT;

--- Причина отключения
update #tmpPU
set ReasonDisconnect = REASON, TypeReasonDisconnect=IDTYPEREASONDISCONNECT
from (
    select D.IDCONTRACT as IDCONTRACT, TR.NAME as REASON, PD2.VALUE as IDGMETER, TR.IDTYPEREASONDISCONNECT as IDTYPEREASONDISCONNECT
    from DOCUMENT as D
     left join PD as PD1 on D.IDDOCUMENT = PD1.IDDOCUMENT and PD1.IDTYPEPD = 33
     left join PD as PD2 on D.IDDOCUMENT = PD2.IDDOCUMENT and PD2.IDTYPEPD = 7
     left join TYPEREASONDISCONNECT as TR on TR.IDTYPEREASONDISCONNECT = PD1.VALUE
    where D.IDDOCUMENT in (select max(IDDOCUMENT) from DOCUMENT as D2 where IDTYPEDOCUMENT = 18 and D.IDCONTRACT = D2.IDCONTRACT)
     ) as REASON
  join #tmpPU as t on t.IDCONTRACT = REASON.IDCONTRACT and t.IDGMETER = REASON.IDGMETER;

--- Причина отключения "Подкл. без ПУ"
select ACCOUNT, [СТАТУС ОБЪЕКТА], SERIALNUMBER, [СТАТУС ПУ], [Модель], ReasonDisconnect, GM_NOTE
from #tmpPU
where STATUSGOPERIOD=1
  and STATUSPUPERIOD=2 and TypeReasonDisconnect=11
order by ACCOUNT;



drop table #tmpPU;
