-- Выполняем сначала из первого второй
-- потом из второго третий
-------

--- 1й
select f.IDFACTUSE
from factuse f
inner join indication i on i.idindication=f.idindication
	and month(i.datedisplay)=6
	and year(i.datedisplay)=2025
left join document d on d.iddocument=f.iddocument
	and d.idtypedocument=7
where d.iddocument is null

except

-- 2й
select f.IDFACTUSE
from gobject g with (nolock)
inner join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
inner join indication i  with (nolock)  on gm.idgmeter=i.idgmeter
	and i.datedisplay<'2025-07-01'
inner join factuse f with (nolock) on f.idindication=i.idindication
	and f.idperiod=245
left join operation o with (nolock) on o.idoperation=f.idoperation
left join document d with (nolock) on o.iddocument=d.iddocument
	and d.idtypedocument=7
where isnull(d.iddocument,0)=0

except

--3й
select f.IDFACTUSE
from factuse f
inner join indication i on i.idindication=f.idindication
	and month(i.datedisplay)=6
	and year(i.datedisplay)=2025
left join document d on d.iddocument=f.iddocument
	and d.idtypedocument=7
where d.iddocument is null

--------
select * from FACTUSE where IDFACTUSE = 14507013 -- (0.55)
select * from INDICATION where IDINDICATION = 12613677
select * from GOBJECT where IDGOBJECT = 894925
select * from GMETER where IDGOBJECT=894925 -- IDGMETER = 818194
select * from GMETER where IDGMETER = 818198
select * from CONTRACT where IDCONTRACT = 894692



select c.ACCOUNT, f.FACTAMOUNT, gm.IDGMETER, i.DATEDISPLAY, i.DISPLAY, f.IDINDICATION, i.IDINDICATION, f.IDFACTUSE
from factuse f with (nolock)
 join GOBJECT g on F.IDGOBJECT = G.IDGOBJECT
 join Contract C on G.IDCONTRACT = C.IDCONTRACT
  join gmeter as gm on G.IDGOBJECT = GM.IDGOBJECT and gm.IDSTATUSGMETER = 1
 join indication as i on i.IDGMETER = gm.IDGMETER
  left join operation o with (nolock) on o.idoperation=f.idoperation
  left join document d with (nolock) on o.iddocument=d.iddocument and d.idtypedocument=7
where f.IDFACTUSE in (14373787, 14375540, 14372140, 14364923)
	and i.datedisplay<'2025-05-01'
	and f.idperiod=243
  and isnull(d.iddocument,0) = 0

select f.IDFACTUSE
from gobject g with (nolock)
inner join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
inner join indication i  with (nolock)  on gm.idgmeter=i.idgmeter
	and i.datedisplay<'2025-05-01'
inner join factuse f with (nolock) on f.idindication=i.idindication
	and f.idperiod=243
left join operation o with (nolock) on o.idoperation=f.idoperation
left join document d with (nolock) on o.iddocument=d.iddocument
	and d.idtypedocument=7
where isnull(d.iddocument,0)=0
and f.IDFACTUSE in (14373787, 14375540, 14372140, 14364923)

select *
from INDICATION
where IDINDICATION in (12484973,12492199,12493855,12495607)

-- верные ---
select *
from INDICATION
where IDINDICATION in (12451023,12451829,12450999,12450642)

select c.ACCOUNT, gm.IDGMETER --, i.DATEDISPLAY, i.DISPLAY,  i.IDINDICATION
from GOBJECT g
 join Contract C on G.IDCONTRACT = C.IDCONTRACT
  join gmeter as gm on G.IDGOBJECT = GM.IDGOBJECT --and gm.IDSTATUSGMETER = 1
 -- join indication as i on i.IDGMETER = gm.IDGMETER
where  --i.IDINDICATION in (12484973,12492199,12493855,12495607)
   gm.IDGMETER in (817595, 817612, 817598, 817573)

+--------+
|IdGMeter|
+--------+
|817595  |
|817612  |
|817598  |
|817573  |
+--------+

select * from Gmeter where IDGMETER in (817595, 817612, 817598, 817573)
select * from INDICATION where IDGMETER in (817595, 817612, 817598, 817573)
+------------+-------+-----------------------+--------+------+-----------------------+--------+----------+-------+----------------+-------+
|IDIndication|Display|DateDisplay            |IdGMeter|IdUser|DateAdd                |IdModify|DateModify|Premech|IDTypeIndication|IDAgent|
+------------+-------+-----------------------+--------+------+-----------------------+--------+----------+-------+----------------+-------+
|12495607    |1      |2025-04-24 00:00:00.000|817573  |62    |2025-04-24 00:00:00.000|null    |null      |null   |1               |null   |
|12484973    |0.544  |2025-04-22 00:00:00.000|817595  |38    |2025-04-22 00:00:00.000|null    |null      |null   |2               |15     |
|12493855    |0.223  |2025-04-24 00:00:00.000|817598  |62    |2025-04-24 00:00:00.000|null    |null      |null   |2               |155    |
|12492199    |0.955  |2025-04-24 00:00:00.000|817612  |74    |2025-04-24 00:00:00.000|null    |null      |null   |7               |142    |
+------------+-------+-----------------------+--------+------+-----------------------+--------+----------+-------+----------------+-------+

+------------+------------+---------+
|IDINDICATION|IDINDICATION|IDFACTUSE|
+------------+------------+---------+
|12484973    |12451023    |14364923 |
|12492199    |12451829    |14372140 |
|12493855    |12450999    |14373787 |
|12495607    |12450642    |14375540 |
+------------+------------+---------+

-- update FACTUSE set IDINDICATION =12484973  where IDINDICATION=12451023 and IDFACTUSE=14364923;
-- update FACTUSE set IDINDICATION =12492199  where IDINDICATION=12451829 and IDFACTUSE=14372140;
-- update FACTUSE set IDINDICATION =12493855  where IDINDICATION=12450999 and IDFACTUSE=14373787;
-- update FACTUSE set IDINDICATION =12495607  where IDINDICATION=12450642 and IDFACTUSE=14375540;

