-- Новая норма для ГРУ с мая 2025 - 2,631

INSERT INTO PG (IDTYPEPG, IDGRU, IDPERIOD, "Value", NORMA, ISDISTR)
SELECT
    1 AS IDTYPEPG,
    IDGRU,
    244 AS IDPERIOD,
    1 AS "Value",
    2.631 AS NORMA,
    NULL AS ISDISTR
FROM GRU
ORDER BY IDGRU

--- Тест ------------
select IDGRU, Norma
from PG  with (nolock)
where IDPeriod <= 245
	and IDTypePG = 1
order by IDPeriod desc

select dbo.fGetLastPGNorma(245, 1, 8494) -- 2.631 июнь
select dbo.fGetLastPGNorma(244, 1, 8494) -- 2.631 май
select dbo.fGetLastPGNorma(243, 1, 8494) -- 2.428 апрель

