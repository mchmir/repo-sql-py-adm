-- Потребление есть, показаний не существует
-- При удалении показания, иногда ID показания остается в таблице потребления


DECLARE @IDPeriod INT;
DECLARE @Year     INT;
DECLARE @Month    INT;

SET @Year = 2024;
SET @Month = 3;

SET @IDPeriod = dbo.fGetIDPeriodMY(@Month, @Year);

SELECT *
  FROM FactUse as fu
       LEFT  JOIN Indication as  i  ON fu.IDIndication = i.IDIndication
  WHERE i.IDINDICATION is null and fu.IDINDICATION is not null
     AND fu.IDPERIOD = @IDPeriod


-- delete from factuse where idfactuse=13396953;

/*
Сверка двух отчетов. Отчет по реализации СУВГ(СНГ) и Отчет о потреблении газа

select
f.IDFACTUSE
from GRU  g with (nolock)
inner join GObject o  with (nolock)  on o.IdGRU=g.IdGRU
inner join Address a with (nolock) on a.IdAddress=o.IdAddress
inner join factuse f  with (nolock) on f.idgobject=o.idgobject
and f.idperiod=228
and isnull(f.idtypefu,0)=1
left join Gmeter gm with (nolock) on gm.IdGObject=o.IdGObject
and dbo.fGetStatusPU (dbo.fGetDatePeriod(228,0),gm.idgmeter)=1

except

select combined.IDFACTUSE
from (
select f.IDFACTUSE
from  FactUse f with (nolock)
inner join indication i with (nolock) on i.idindication=f.idindication
  and isnull(f.IdTypeFU,0)=1
inner join operation o with (nolock) on o.idoperation=f.idoperation
inner join document d with (nolock) on d.iddocument=o.iddocument
  and idtypedocument=5
  and  f.IdPeriod=228
inner join contract c with (nolock) on d.idcontract=c.idcontract
  and dbo.fGetIsJuridical(c.idperson,228)=0

union

select
f.IDFACTUSE
from document d with (nolock)
inner join contract c  with (nolock)  on c.idcontract=d.idcontract
and idtypedocument=7 and d.idperiod=228
and dbo.fGetIsJuridical(c.idperson,228)=0
inner join pd with (nolock)  on pd.iddocument=d.iddocument
and idtypepd=13
inner join factuse f with (nolock)  on f.iddocument=d.iddocument
and f.idtypefu=1

union

select f.IDFACTUSE
from  FactUse f with (nolock)
inner join indication i with (nolock) on i.idindication=f.idindication
  and isnull(f.IdTypeFU,0)=1
inner join operation o with (nolock) on o.idoperation=f.idoperation
inner join document d with (nolock) on d.iddocument=o.iddocument
  and idtypedocument=5
  and  f.IdPeriod=228
inner join contract c with (nolock) on d.idcontract=c.idcontract
  and dbo.fGetIsJuridical(c.idperson,228)=1
  ) as combined


select * from FACTUSE where IDFACTUSE=13396953
select * from INDICATION where IDINDICATION = 11631152
select * from GOBJECT where IDGOBJECT=870473
select * from CONTRACT where IDCONTRACT=870240 -- 1935035

select * from indication where IDINDICATION in (11631152, 11631170)

 */

