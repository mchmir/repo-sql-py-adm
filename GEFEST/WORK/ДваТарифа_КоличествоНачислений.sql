
with
  NACH as (
    select C.IDCONTRACT as IDCONTRACT,
           D.NOTE       as NOTE,
           dbo.fGetIsJuridical(P.IDPERSON, 242) as JUR
    from DOCUMENT as D
      join CONTRACT as C on C.IDCONTRACT = D.IDCONTRACT
      join PERSON as P on P.IDPERSON = C.IDPERSON
    where D.IDPERIOD = 242
    and D.IDTYPEDOCUMENT = 5
    and D.NOTE in ('Начисление по 605.25', 'Начисление по 711.54')
    ),
  TWONACH as (
    select IDCONTRACT, count(IDCONTRACT) as CN
    from NACH
    group by IDCONTRACT
    having count(IDCONTRACT)>1
  ),
  NACH605 as (
    select IDCONTRACT
    from NACH
    where NOTE in ('Начисление по 605.25')
      and IDCONTRACT not in (select IDCONTRACT from TWONACH)
    ),
  NACH711 as (
    select IDCONTRACT
    from NACH
    where NOTE in ('Начисление по 711.54')
      and IDCONTRACT not in (select IDCONTRACT from TWONACH)
    )
select
  (select count(*) from TWONACH) as cnt_both,
  (select count(*) from NACH605) as cnt_only_605,
  (select count(*) from NACH711) as cnt_only_711;
------------------------------------------------------------------------------------------------------------------------

with
  NACH as (
    select C.IDCONTRACT as IDCONTRACT,
           D.NOTE       as NOTE,
           dbo.fGetIsJuridical(P.IDPERSON, 242) as JUR
    from DOCUMENT as D
      join CONTRACT as C on C.IDCONTRACT = D.IDCONTRACT
      join PERSON as P on P.IDPERSON = C.IDPERSON
    where D.IDPERIOD = 242
    and D.IDTYPEDOCUMENT = 5
    and D.NOTE in ('Начисление по 605.25', 'Начисление по 711.54')
    ),
  TWONACH as (
    select IDCONTRACT, count(IDCONTRACT) as CN
    from NACH
    where JUR = 0
    group by IDCONTRACT
    having count(IDCONTRACT)>1
  ),
  NACH605 as (
    select IDCONTRACT
    from NACH
    where NOTE in ('Начисление по 605.25')
      and JUR = 0
      and IDCONTRACT not in (select IDCONTRACT from TWONACH)
    ),
  NACH711 as (
    select IDCONTRACT
    from NACH
    where NOTE in ('Начисление по 711.54')
      and JUR = 0
      and IDCONTRACT not in (select IDCONTRACT from TWONACH)
    )
select
  (select count(*) from TWONACH) as cnt_both,
  (select count(*) from NACH605) as cnt_only_605,
  (select count(*) from NACH711) as cnt_only_711;

-- Все начисления
+--------+------------+------------+
|cnt_both|cnt_only_605|cnt_only_711|
+--------+------------+------------+
|2137    |31301       |23038       |  = 56476
+--------+------------+------------+

-- Только физические лица
+--------+------------+------------+
|cnt_both|cnt_only_605|cnt_only_711|
+--------+------------+------------+
|2137    |31259       |23038       | = 56434
+--------+------------+------------+

-- 42 юр.лица

with
  NACH as (
    select C.ACCOUNT    as ACC,
           C.IDCONTRACT as IDCONTRACT,
           D.NOTE       as NOTE,
           dbo.fGetIsJuridical(P.IDPERSON, 242) as JUR
    from DOCUMENT as D
      join CONTRACT as C on C.IDCONTRACT = D.IDCONTRACT
      join PERSON as P on P.IDPERSON = C.IDPERSON
    where D.IDPERIOD = 242
    and D.IDTYPEDOCUMENT = 5
    and D.NOTE in ('Начисление по 605.25', 'Начисление по 711.54')
    ),
  NACHFL as (
    select IDCONTRACT, ACC
    from NACH
    where JUR = 0
  )
select A.ACCOUNT, NACHFL.ACC
from AAAERC7 as A
  left join NACHFL on NACHFL.ACC = A.ACCOUNT
where NACHFL.ACC is null;


select C.ACCOUNT    as ACC,
       C.IDCONTRACT as IDCONTRACT,
       D.NOTE       as NOTE,
       dbo.fGetIsJuridical(P.IDPERSON, 242) as JUR
from DOCUMENT as D
  join CONTRACT as C on C.IDCONTRACT = D.IDCONTRACT
  join PERSON as P on P.IDPERSON = C.IDPERSON
where D.IDPERIOD = 242
and D.IDTYPEDOCUMENT = 5
and D.NOTE is null
;



select * from OPERATION where IDOPERATION = 27544675
select * from DOCUMENT where IDDOCUMENT = 25639940