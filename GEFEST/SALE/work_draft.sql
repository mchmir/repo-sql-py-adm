select *
from BALANCE
where IDGRU = 8714
  and DATEBALANCE between '2021-10-31' and '2023-08-31'
order by IDBALANCE desc;


select *
from DOCUMENT
where IDGRU = 8714
  and DOCUMENTDATE between '2023-04-01' and '2023-04-30';

select *
from BALANCE


select *
from DOCUMENT
WHERE DOCUMENTNUMBER IN (SELECT MAX(DOCUMENTNUMBER)
                         FROM Document
                         WHERE DOCUMENTDATE = '2023-04-30' AND IDGRU = 8868
                         GROUP BY AMOUNTBT
                         HAVING COUNT(*) > 1
            )
and YEAR(DOCUMENTDATE) = 2023
and MONTH(DOCUMENTDATE) = 4;


select *
from BALANCE
where IDGRU in (
  select BALANCE.IDGRU
  from BALANCE
  where DATEBALANCE = '2021-11-01'
  )
and DATEBALANCE between '2021-10-31' and '2021-11-01'
order by IDBALANCE desc;


select *
from BALANCENAME
where IDOBJECT = 5
   and DATEBALANCE between '2023-07-31' and '2023-08-31'
order by IDBALANCE desc;
