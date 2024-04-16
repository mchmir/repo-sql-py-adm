
/*
При вводе новых РУ, при печати счета извещений не отображается цена.
Это связано с отсутствием тарифа.

1) Новын РУ 06035, 06038, 06039, 06040
*/

select * from GRU where INVNUMBER in (06035, 06038, 06039, 06040);

select * from PG where IDGRU in (select IDGRU from GRU where INVNUMBER in (06035, 06038, 06039, 06040));

select top 10 * from PG order by idPeriod DESC;
select top 10 * from PERIOD order by IDPERIOD DESC;

/*
8871, 8872 - январь 2024  - 228
8873, 8874 - февраль 2024 - 229
*/

INSERT INTO PG (IDTYPEPG, IDGRU, IDPERIOD, VALUE, NORMA)
  VALUES (1, 8871, 228, 1, 2.428);
INSERT INTO PG (IDTYPEPG, IDGRU, IDPERIOD, VALUE, NORMA)
  VALUES (1, 8872, 228, 1, 2.428);
INSERT INTO PG (IDTYPEPG, IDGRU, IDPERIOD, VALUE, NORMA)
  VALUES (1, 8873, 229, 1, 2.428);
INSERT INTO PG (IDTYPEPG, IDGRU, IDPERIOD, VALUE, NORMA)
  VALUES (1, 8874, 229, 1, 2.428);

select * from PG where IDGRU in (select IDGRU from GRU where INVNUMBER in (06035, 06038, 06039, 06040));

+-----+--------+-----+--------+-----+-----+-------+
|IdPG |IdTypePG|IdGRU|IdPeriod|Value|Norma|IsDistr|
+-----+--------+-----+--------+-----+-----+-------+
|23620|1       |8871 |228     |1    |2.428|NULL   |
|23621|1       |8872 |228     |1    |2.428|NULL   |
|23622|1       |8873 |229     |1    |2.428|NULL   |
|23623|1       |8874 |229     |1    |2.428|NULL   |
+-----+--------+-----+--------+-----+-----+-------+

