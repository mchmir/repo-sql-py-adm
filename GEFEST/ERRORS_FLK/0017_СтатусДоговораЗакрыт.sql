-- Статус договора закрыт, но объект учета подключен

select
  c.Account as [Лицевой счет],
  case c.Status when 0 then 'Не определен'
                when 1 then 'Активен'
                when 2 then 'Закрыт' end as [Статус договора],
  case when gob.IDStatusGObject = 1 then 'ОУ подключен' else 'ОУ отключен' end as [ОУ],
  case when g.IDStatusGMeter = 1 then 'ПУ подключен' else 'ПУ отключен' end as [ПУ],
  g.SerialNumber as [Серийный номер ПУ]
from Contract as c
  join dbo.GObject as gob on gob.IDContract = c.IDContract
  join dbo.GMeter as g on gob.IDGObject = g.IDGObject
where isnull(c.status,0) = 2
  and gob.IDStatusGObject = 1
order by c.Account, g.IDStatusGMeter;

/*

+------------+---------------+------------+------------+-----------------+
|Лицевой счет|Статус договора|ОУ          |ПУ          |Серийный номер ПУ|
+------------+---------------+------------+------------+-----------------+
|2232076     |Закрыт         |ОУ подключен|ПУ подключен|19010247         |
|2232076     |Закрыт         |ОУ подключен|ПУ отключен |9610911          |
|2476015     |Закрыт         |ОУ подключен|ПУ подключен|04871164         |
|2476015     |Закрыт         |ОУ подключен|ПУ отключен |004845885*       |
+------------+---------------+------------+------------+-----------------+



*/



-- ЛС в которых есть долг, но статус договора закрыт
-- При закрытом статусе договора, не производится Расчет Пени.

select c.Account,
  case c.Status when 0 then 'Не определен'
                when 1 then 'Активен'
                when 2 then 'Закрыт' end as [Статус договора],
  case when gob.IDStatusGObject = 1 then 'ОУ подключен' else 'ОУ отключен' end as [ОУ],
       sum(b.AmountBalance)
from Contract as c
join dbo.Balance as b on c.IDContract = b.IDContract
join dbo.GObject as gob on gob.IDContract = c.IDContract
where b.IDPeriod = dbo.fGetNowPeriod() -- текущий период
and isnull(c.status,0) = 2 and isnull(c.ChargePeny,1)=1
GROUP by c.Account, c.Status, gob.IDStatusGObject
having sum(b.AmountBalance) between -1000000 and -100
order by sum(b.AmountBalance);

/*
+-------+---------------+-----------+-------------------+
|Account|Статус договора|ОУ         |                   |
+-------+---------------+-----------+-------------------+
|1935045|Закрыт         |ОУ отключен|-1273.611          |
|2751021|Закрыт         |ОУ отключен|-235.33254359719194|
|1282001|Закрыт         |ОУ отключен|-140.38999999999987|
+-------+---------------+-----------+-------------------+






*/



