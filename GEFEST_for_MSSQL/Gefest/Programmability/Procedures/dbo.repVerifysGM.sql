SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
-- =============================================
-- Author:		<>
-- Create date: <2017-08-14>
-- Description:	<Отчет по поверенным ПУ>
-- =============================================
CREATE PROCEDURE [dbo].[repVerifysGM](@Year varchar(4)) AS

/*
select count(d.iddocument) as nok, month(documentdate) as mnth 
from document d
inner join pd pd1 on pd1.iddocument=d.iddocument
and pd1.idtypepd=16 and convert(int,pd1.value)=94
left join pd as pd2 on pd2.iddocument=d.iddocument
and pd2.idtypepd=30 and convert(int,pd2.value)=0
where idtypedocument=22 and documentdate>='2017-01-01'
group by month(documentdate)
*/

declare @dBegin datetime
declare @dEnd datetime

set @dBegin=@Year+'-01-01'
set @dEnd=@Year+'-12-31'

declare @T table (mnth int, nokN int, okN int, itogoN int, nokG int, okG int, itogoG int, itogonok int, itogook int, itogo int)

declare @i int
set @i=1
WHILE @i<=13
begin
	insert into @T (mnth, nokN, okN, itogoN, nokG, okG, itogoG, itogonok, itogook, itogo)
	values (@i, 0, 0, 0, 0, 0, 0, 0, 0, 0)
	set @i=@i+1
end

update @T
set nokN=isnull(ff.nok,0)
from @T aa
inner join (select count(0) as nok, month(documentdate) as mnth from document d
inner join pd pd1 on pd1.iddocument=d.iddocument
and pd1.idtypepd=16 and convert(int,pd1.value)=98 and idtypedocument=22 and documentdate>=@dBegin and documentdate<=@dEnd
inner join pd as pd2 on pd2.iddocument=d.iddocument
and pd2.idtypepd=30 and convert(int,pd2.value)<>1
group by month(documentdate)
)ff on ff.mnth=aa.mnth

update @T
set okN=isnull(ff.ok,0)
from @T aa
inner join (select count(0) as ok, month(documentdate) as mnth from document d
inner join pd pd1 on pd1.iddocument=d.iddocument
and pd1.idtypepd=16 and convert(int,pd1.value)=98 and idtypedocument=22 and documentdate>=@dBegin and documentdate<=@dEnd
inner join pd as pd2 on pd2.iddocument=d.iddocument
and pd2.idtypepd=30 and convert(int,pd2.value)=1
group by month(documentdate)
)ff on ff.mnth=aa.mnth

update @T
set nokG=isnull(ff.nok,0)
from @T aa
inner join (select count(0) as nok, month(documentdate) as mnth from document d
inner join pd pd1 on pd1.iddocument=d.iddocument
and pd1.idtypepd=16 and convert(int,pd1.value) in (94,132) and idtypedocument=22 and documentdate>=@dBegin and documentdate<=@dEnd
inner join pd as pd2 on pd2.iddocument=d.iddocument
and pd2.idtypepd=30 and convert(int,pd2.value)<>1
group by month(documentdate)
)ff on ff.mnth=aa.mnth

update @T
set okG=isnull(ff.ok,0)
from @T aa
inner join (select count(0) as ok, month(documentdate) as mnth from document d
inner join pd pd1 on pd1.iddocument=d.iddocument
and pd1.idtypepd=16 and convert(int,pd1.value) in (94,132) and idtypedocument=22 and documentdate>=@dBegin and documentdate<=@dEnd
inner join pd as pd2 on pd2.iddocument=d.iddocument
and pd2.idtypepd=30 and convert(int,pd2.value)=1
group by month(documentdate)
)ff on ff.mnth=aa.mnth

update @T set itogoN = nokN + okN 
update @T set itogoG = nokG + okG
update @T set itogonok = nokN + nokG
update @T set itogook = okN + okG
update @T set itogo = itogonok + itogook

select t.*, m.namemonth from @T t
inner join [Month] m on m.idMonth=t.mnth



--select * from document d
--where idtypedocument=22 and documentdate>='2017-07-01'
--
--select * from pd where iddocument=15608008
GO