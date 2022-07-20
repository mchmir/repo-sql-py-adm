SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

CREATE procedure [dbo].[repRaznoskaIndication] (@idperiod int) as 
--declare @IdPeriod int
--set @IdPeriod=24

declare @dBegin datetime
declare @dEnd datetime
set @dBegin= dbo.fGetDatePeriod(@IdPeriod,1)
set @dEnd= dbo.fGetDatePeriod(@IdPeriod,0)

create table #RaznPocaz (iduser int, FIO varchar(50),TypeI int, Typeindication varchar(20), d1 int, d2 int, d3 int, d4 int, d5 int, 
d6 int, d7 int, d8 int, d9 int, d10 int, d11 int, d12 int, d13 int, d14 int, d15 int, d16 int, d17 int, d18 int, 
d19 int, d20 int, d21 int, d22 int, d23 int, d24 int, d25 int, d26 int, d27 int, d28 int, d29 int, d30 int, d31 int, allcount int)
declare @query varchar (8000)

DECLARE curcount CURSOR
READ_ONLY
FOR select iduser, Day(i.datedisplay), count(idindication), i.idtypeindication
from indication i with (nolock) 
where dbo.dateonly(i.datedisplay)>=@dBegin and dbo.dateonly(i.datedisplay)<=@dEnd
group by iduser,i.datedisplay,  i.idtypeindication

DECLARE @Iduser int
DECLARE @Date int
DECLARE @count int
declare @TypeI int

OPEN curcount

FETCH NEXT FROM curcount INTO @Iduser, @Date, @count,@TypeI
WHILE (@@fetch_status <> -1)
BEGIN
	IF (@@fetch_status <> -2)
	BEGIN
		if exists(select * from #RaznPocaz where iduser=@Iduser and TypeI=@TypeI)
			set @query='update #RaznPocaz set d'+ltrim(str(@Date))+'='+str(@count)+', Allcount=Allcount+'+str(@count)+' where Iduser='+str(@Iduser)+' and TypeI='+str(@TypeI)
		else
			set @query='insert #RaznPocaz (Iduser,TypeI, d'+ltrim(str(@Date))+', Allcount) values('+str(@Iduser)+','+str(@TypeI)+','+str(@count)+','+str(@count)+')'
		exec (@query)
	END
	FETCH NEXT FROM curcount INTO @Iduser, @Date, @count,@TypeI
END

CLOSE curcount
DEALLOCATE curcount

update #RaznPocaz
set fio=s.name,
typeindication=t.name
from #RaznPocaz r
inner join typeindication t with (nolock) on r.typei=t.idtypeindication
inner join sysusers s with (nolock)  on r.iduser=s.uid

select *, 'c '+convert(varchar(20), @dBegin, 104)+' по '+convert(varchar(20), @dEnd, 104) as period  from #RaznPocaz order by fio
drop table #RaznPocaz


GO