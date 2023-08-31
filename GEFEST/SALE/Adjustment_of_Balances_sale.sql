

select *
from BALANCENAME
where IDOBJECT = 5
   and DATEBALANCE between '2023-07-31' and '2023-08-31'
order by IDBALANCE desc;

/**
  Исправляем остатки на 31 число,
  или на последнее число месяца
  а запускаем пересчет со 1 числа
 */

execute spPerechetOstatkovGNS '2023-08-01', 0, 'GNS';