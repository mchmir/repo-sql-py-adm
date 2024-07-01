select
  CONTRACT.ACCOUNT      as [ЛИЦЕВОЙ СЧЕТ],
  GOBJECT.COUNTLIVES    as [КОЛ-ВО ПРОЖИВ.],
  PERSON.SURNAME        as ФАМИЛИЯ,
  PERSON.NAME           as ИМЯ,
  PERSON.PATRONIC       as ОТЧЕСТВО,
  PERSON.NSURNAME       as KZ_ФАМИЛИЯ,
  PERSON.NNAME          as KZ_ИМЯ,
  PERSON.NPATRONIC      as KZ_ОТЧЕСТВО,
  GOBJECT.NAME          as ИМЯОБЪЕКТА,
  GRU.INVNUMBER         as [РУ],
  STREET.NAME           as УЛИЦА,
  HOUSE.HOUSENUMBER     as ДОМ,
  HOUSE.HOUSENUMBERCHAR as ЛИТЕРАДОМА,
  ADDRESS.FLAT          as КВАРТИРА,
  PERSON.ISJURIDICAL    as [ПРИЗНАК ЮР.ЛИЦА],
  GMETER.SERIALNUMBER   as [Номер счетчика],
  GMETER.DATEINSTALL    as [Дата установки ПУ],
  GMETER.PLOMBNUMBER1   as [Пломба 1],
  GMETER.PLOMBNUMBER2   as [Пломба 2],
  GMETER.DATEPLOMB      as [Дата пломбы],
  GMETER.PLOMBMEMO      as [Примечание к пломбам]
from GOBJECT
  join GRU on GOBJECT.IDGRU = GRU.IDGRU
  join CONTRACT on GOBJECT.IDCONTRACT = CONTRACT.IDCONTRACT
  join PERSON on PERSON.IDPERSON = CONTRACT.IDPERSON
  join ADDRESS on PERSON.IDADDRESS = ADDRESS.IDADDRESS
  join STREET on ADDRESS.IDSTREET = STREET.IDSTREET
  join HOUSE on ADDRESS.IDHOUSE = HOUSE.IDHOUSE and HOUSE.IDSTREET = STREET.IDSTREET
  join GMETER on GOBJECT.IDGOBJECT = GMETER.IDGOBJECT
where GMETER.IDSTATUSGMETER = 1
order by [ЛИЦЕВОЙ СЧЕТ];

---------------------------------------------------------------------------


select
  G.INVNUMBER,
  G.NAME,
  G.MEMO as PREFIX,
  convert(varchar, G.DATEIN, 104) as [Дата ввода в экспл.],
  A.NAME as [Контроллер],
  G.NOTE as [Примечание]
from GRU as G
  join AGENT A on G.IDAGENT = A.IDAGENT;

------------------------------------------------------------------------------------------------------------------------
-- 1 --
--3.Лицевой счет, объект учета, статус объекта учета.
--- Данные объекта учета ---
select
  GOBJECT.NAME            as ИМЯОБЪЕКТА,
  case GOBJECT.IDSTATUSGOBJECT
    when 1 then 'ОУ Подключен'
    when 2 then 'ОУ Отключен'
  end as [СТАТУС ОБЪЕКТА],
  CONTRACT.ACCOUNT        as [ЛИЦЕВОЙ СЧЕТ],
  GOBJECT.COUNTLIVES      as [КОЛ-ВО ПРОЖИВ.],
  PERSON.RNN              as [БИН/ИИН],
  case when PERSON.ISJURIDICAL=1 then PERSON.SURNAME else null end as [НАИМЕНОВАНИЕ ЮЛ],
  case when PERSON.ISJURIDICAL=1 then PERSON.NAME else null end as [ПОЛНОЕ НАИМЕНОВАНИЕ ЮЛ],
  case when PERSON.ISJURIDICAL=1 then PERSON.PATRONIC else null end  as [Отв.лицо ЮЛ],
  case when PERSON.ISJURIDICAL=0 then PERSON.NSURNAME else null end as KZ_ФАМИЛИЯ,
  case when PERSON.ISJURIDICAL=0 then PERSON.NNAME else null end as KZ_ИМЯ,
  case when PERSON.ISJURIDICAL=0 then PERSON.NPATRONIC else null end as KZ_ОТЧЕСТВО,
  GRU.INVNUMBER           as [РУ],
  STREET.NAME             as УЛИЦА,
  HOUSE.HOUSENUMBER       as ДОМ,
  HOUSE.HOUSENUMBERCHAR   as ЛИТЕРАДОМА,
  ADDRESS.FLAT            as КВАРТИРА,
  PERSON.ISJURIDICAL      as [ПРИЗНАК ЮР.ЛИЦА]
from GOBJECT
  join GRU on GOBJECT.IDGRU = GRU.IDGRU
  join CONTRACT on GOBJECT.IDCONTRACT = CONTRACT.IDCONTRACT
  join PERSON on PERSON.IDPERSON = CONTRACT.IDPERSON
  join ADDRESS on PERSON.IDADDRESS = ADDRESS.IDADDRESS
  join STREET on ADDRESS.IDSTREET = STREET.IDSTREET
  join HOUSE on ADDRESS.IDHOUSE = HOUSE.IDHOUSE and HOUSE.IDSTREET = STREET.IDSTREET
order by [ЛИЦЕВОЙ СЧЕТ];

-- 2. Лицевой счет, номер счетчика, последнее показание счетчика, дата приема последних показаний,
-- статус счетчика (подключен-отключен),
-- номер пломб, статус пломб, дата установки пломбы, примечание (лицевой счет)

-- Разделим на 2 запроса: Технические данные счетчика и Функциональные(Рабочие) данные счетчика
select
  CONTRACT.ACCOUNT      as [ЛИЦЕВОЙ СЧЕТ],
  GMETER.SERIALNUMBER   as [Номер счетчика],
  TGM.NAME              as [Модель],
  TGM.SERVICELIFE       as [Срок службы],
  case GMETER.IDSTATUSGMETER
    when 1 then 'ПУ Подключен'
    when 2 then 'ПУ Отключен'
  end                   as [СТАТУС ПУ],
  convert(varchar, GMETER.DATEFABRICATION, 104) as [Дата изготовления],
  convert(varchar, GMETER.DATEINSTALL, 104 )    as [Дата установки ПУ],
  convert(varchar, GMETER.DATEVERIFY, 104)      as [Дата проверки],
  GMETER.PLOMBNUMBER1   as [Пломба 1],
  GMETER.PLOMBNUMBER2   as [Пломба 2],
  convert(varchar, GMETER.DATEPLOMB, 104)       as [Дата пломбы],
  GMETER.PLOMBMEMO      as [Примечание к пломбам]
from GOBJECT
  join CONTRACT on GOBJECT.IDCONTRACT = CONTRACT.IDCONTRACT
  join GMETER on GOBJECT.IDGOBJECT = GMETER.IDGOBJECT
  join TYPEGMETER as TGM on GMETER.IDTYPEGMETER = TGM.IDTYPEGMETER
order by [ЛИЦЕВОЙ СЧЕТ];

-- 3 --
-- Функциональные(Рабочие) данные счетчика
select
  CONTRACT.ACCOUNT                      as [ЛИЦЕВОЙ СЧЕТ],
  GMETER.SERIALNUMBER                   as [Номер счетчика],
  case GMETER.IDSTATUSGMETER
    when 1 then 'ПУ Подключен'
    when 2 then 'ПУ Отключен'
  end                                   as [СТАТУС ПУ],
  convert(varchar, I.DATEDISPLAY, 104)  as [Дата последнего показания],
  I.DISPLAY                             as [Показание]
from GOBJECT
  join CONTRACT on GOBJECT.IDCONTRACT = CONTRACT.IDCONTRACT
  join GMETER on GOBJECT.IDGOBJECT = GMETER.IDGOBJECT
  join INDICATION as I on GMETER.IDGMETER = I.IDGMETER
where -- GMETER.IDSTATUSGMETER = 1 and
    I.DATEDISPLAY = (
    select MAX(I2.DATEDISPLAY)
    from INDICATION as I2
    where I2.IDGMETER = I.IDGMETER
  )
order by [ЛИЦЕВОЙ СЧЕТ];

-- 4 --
-- Лицевой счет, вид задолженности, сальдо задолженности.
select
  C.ACCOUNT                 as [ЛИЦЕВОЙ СЧЕТ],
  case GOBJECT.IDSTATUSGOBJECT
    when 1 then 'ОУ Подключен'
    when 2 then 'ОУ Отключен'
  end                       as [СТАТУС ОУ],
  A.NAME                    as [Вид учета],
  convert(varchar, dbo.fGetDatePeriod(B.IDPERIOD, 2), 104) as [Дата],
  isnull(round(B.AMOUNTBALANCE, 3), 0) as [Сумма (со средним)],
  isnull(round(BR.AMOUNTBALANCE, 3), 0) as [Сумма (без среднего)]
from GOBJECT
  join CONTRACT as C on GOBJECT.IDCONTRACT = C.IDCONTRACT
  join BALANCE as B on C.IDCONTRACT = B.IDCONTRACT
  join ACCOUNTING as A on B.IDACCOUNTING = A.IDACCOUNTING
  left join BALANCEREAL as BR on C.IDCONTRACT = BR.IDCONTRACT and BR.IDACCOUNTING = A.IDACCOUNTING and BR.IDPERIOD = dbo.fGetNowPeriod()
where B.IDPERIOD = dbo.fGetNowPeriod()
  --and C.ACCOUNT = '0731049'
order by [ЛИЦЕВОЙ СЧЕТ];

----- Разница ---------------------------------
with report4 as (
  select
  C.ACCOUNT                 as Account,
  case GOBJECT.IDSTATUSGOBJECT
    when 1 then 'ОУ Подключен'
    when 2 then 'ОУ Отключен'
  end                       as StateOU,
  A.NAME                    as TypeU,
  convert(varchar, dbo.fGetDatePeriod(B.IDPERIOD, 2), 104) as Date,
  isnull(round(B.AMOUNTBALANCE, 3), 0) as SumWithAverage,
  isnull(round(BR.AMOUNTBALANCE, 3), 0) as SumWithoutAverage
from GOBJECT
  join CONTRACT as C on GOBJECT.IDCONTRACT = C.IDCONTRACT
  join BALANCE as B on C.IDCONTRACT = B.IDCONTRACT
  join ACCOUNTING as A on B.IDACCOUNTING = A.IDACCOUNTING
  left join BALANCEREAL as BR on C.IDCONTRACT = BR.IDCONTRACT and BR.IDACCOUNTING = A.IDACCOUNTING and BR.IDPERIOD = dbo.fGetNowPeriod()
where B.IDPERIOD = dbo.fGetNowPeriod()
)
select Account, TypeU, Date, SumWithAverage, SumWithoutAverage, round(abs(SumWithAverage) - abs(SumWithoutAverage), 4) as RAZN
from report4
where SumWithAverage <> SumWithoutAverage
order by RAZN desc;


-- 5. Виды оказываемых услуг, стоимость услуг.
select *
from USLUGIVDGO;

-- 6. Список контроллеров и принимающих показания.
select
  A.NAME  as [Ф.И.О],
  TA.NAME as [Тип агента]
from AGENT as A
  join TYPEAGENT as TA on A.IDTYPEAGENT = TA.IDTYPEAGENT

--- 7.
select G.INVNUMBER,
       G.NAME,
       G.MEMO          as PREFIX,
       case
         when Year(G.DATEIN) > 1900
           then convert(varchar, G.DATEIN, 104)
         else null end as [Дата ввода в экспл.],
       A.NAME          as [Контроллер],
       G.NOTE          as [Примечание]
from GRU as G
       join AGENT A on G.IDAGENT = A.IDAGENT
order by G.INVNUMBER;

-- 8
-- Лицевой счет, договор на ТО, график проверки, дата начала договора, номер договора, ответственный, стоимость

select
  C.ACCOUNT                                                 as [ЛИЦЕВОЙ СЧЕТ],
  P.NUMBERDOG                                               as [Номер Договора],
  convert(varchar, P.DATEDOG, 104)                          as [Дата Договора],
  P.COSTDOG                                                 as [Стоимость Договора],
  GT.YEAR                                                   as [ГОД],
  MAX(case when GT.MONTH = 1 then GT.AMOUNT else null end)  as [ЯНВАРЬ],
  MAX(case when GT.MONTH = 2 then GT.AMOUNT else null end)  as [ФЕВРАЛЬ],
  MAX(case when GT.MONTH = 3 then GT.AMOUNT else null end)  as [МАРТ],
  MAX(case when GT.MONTH = 4 then GT.AMOUNT else null end)  as [АПРЕЛЬ],
  MAX(case when GT.MONTH = 5 then GT.AMOUNT else null end)  as [МАЙ],
  MAX(case when GT.MONTH = 6 then GT.AMOUNT else null end)  as [ИЮНЬ],
  MAX(case when GT.MONTH = 7 then GT.AMOUNT else null end)  as [ИЮЛЬ],
  MAX(case when GT.MONTH = 8 then GT.AMOUNT else null end)  as [АВГУСТ],
  MAX(case when GT.MONTH = 9 then GT.AMOUNT else null end)  as [СЕНТЯБРЬ],
  MAX(case when GT.MONTH = 10 then GT.AMOUNT else null end) as [ОКТЯБРЬ],
  MAX(case when GT.MONTH = 11 then GT.AMOUNT else null end) as [НОЯБРЬ],
  MAX(case when GT.MONTH = 12 then GT.AMOUNT else null end) as [ДЕКАБРЬ]
from CONTRACT as C
  join PERSON as P on C.IDPERSON = P.IDPERSON
  join GRAFIKTO as GT on C.IDCONTRACT = GT.IDCONTRACT
where GT.YEAR = 2024
group by C.ACCOUNT, P.NUMBERDOG, convert(varchar, P.DATEDOG, 104), P.COSTDOG, GT.YEAR
order by [ЛИЦЕВОЙ СЧЕТ], [ГОД];

