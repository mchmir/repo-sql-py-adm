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
  STREET.NAME           as УЛИЦА,
  HOUSE.HOUSENUMBER     as ДОМ,
  HOUSE.HOUSENUMBERCHAR as ЛИТЕРАДОМА,
  ADDRESS.FLAT          as КВАРТИРА,
  PERSON.ISJURIDICAL    as [ПРИЗНАК ЮР.ЛИЦА]
from DBO.GOBJECT
inner join DBO.CONTRACT on GOBJECT.IDCONTRACT = CONTRACT.IDCONTRACT
inner join DBO.PERSON on PERSON.IDPERSON = CONTRACT.IDPERSON
inner join DBO.ADDRESS on PERSON.IDADDRESS = ADDRESS.IDADDRESS
inner join DBO.STREET on ADDRESS.IDSTREET = STREET.IDSTREET
inner join DBO.HOUSE on ADDRESS.IDHOUSE = HOUSE.IDHOUSE
    and HOUSE.IDSTREET = STREET.IDSTREET
order by [ЛИЦЕВОЙ СЧЕТ]



