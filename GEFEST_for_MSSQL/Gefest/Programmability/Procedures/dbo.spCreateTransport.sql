SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

CREATE PROCEDURE [dbo].[spCreateTransport](@idPeriod int) AS

--declare @IDPERIOD int
--set @idperiod=63
select idgru, invNumber,  convert(float,0.00) kg, convert(float,0.00) cub,  convert(datetime,'') dateDoc 
into #Real
from gru with (nolock) 
-------Собир кубы
update #Real
set cub=ROUND(isnull(cc.cubes,0), 3)
from #Real p
inner join (select sum(f.FactAmount)cubes, g.idgru
from FactUse f with (nolock) 
inner join gobject g with (nolock) on g.idgobject=f.idgobject
	and (isnull(f.IdTypeFU,0)=1 or isnull(f.IdTypeFU,0)=2)
	and f.IdPeriod=@idperiod
group by g.idgru) cc on p.idgru=cc.idgru
---------собираем кг
  update #Real
  set kg=ROUND(p.cub*2.7,3)
  from #Real p
              --update #Real
              --set kg=isnull(cc.kg,0)
              --from #Real p
              --left join (select sum(f.FactAmount) kg, g.idgru
              --from FactUse f with (nolock) 
              --inner join gobject g with (nolock) on g.idgobject=f.idgobject
              --	and isnull(f.IdTypeFU,0)=2
              --	and f.IdPeriod=@idperiod
              --group by g.idgru) cc on p.idgru=cc.idgru

update #Real
  set cub=0
  from #Real p

update #Real
set dateDoc=CONVERT(datetime, (select top 1  d.documentdate
from  document d  with (nolock)
	where d.idtypedocument=5
	and d.IdPeriod=@idperiod),20)

select * from #Real

drop table #Real
GO