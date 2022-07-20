SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO






CREATE PROCEDURE [dbo].[repOldDebet] (@CountMonth int, @Dolg float, @Path varchar(1000)) AS
---------------**************Просроченная задолженность*********-------------------
SET NOCOUNT ON
declare @IDPeriod int
declare @IDPeriodNow int
declare @Str varchar(50)
set @IDPeriod=(select max(IDPeriod) from Period)-(@CountMonth+1)
set @IDPeriodNow=(select max(IDPeriod) from Period)
set @Str='с долгом свыше '+convert(varchar(20),@Dolg)+', образовавшаяся '+convert(varchar(20),@CountMonth)+' месяца.'
--declare @TblGRU table (num varchar(5), namegru varchar(50))
--insert into @TblGRU (num, namegru) values ('01','Центр')
--insert into @TblGRU (num, namegru) values ('02','Молодежный поселок_БЖ')
--insert into @TblGRU (num, namegru) values ('03','20мкр')
--insert into @TblGRU (num, namegru) values ('04','19мкр')
--insert into @TblGRU (num, namegru) values ('05','Вокзал')
--insert into @TblGRU (num, namegru) values ('06','Бензострой')
--insert into @TblGRU (num, namegru) values ('07','РабочийПоселок')
--insert into @TblGRU (num, namegru) values ('08','ДСР')
--insert into @TblGRU (num, namegru) values ('09','мкрЮбилейный')

create table #Tbl  (idcontract int, LS varchar(20), FIO varchar(200), Address varchar(200), DolgOld float, DolgNow float, PeriodPay int)--, DolgIsk float, gruinvnumber varchar(50))

insert into #Tbl (idcontract, LS, FIO, Address)
select ct.idcontract, ct.Account as LIC_ACC, p.Surname+' '+p.name+' '+p.Patronic as FIO ,
s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat) as ADDRESS
--left(gru.invnumber,2) gruinvnumber
--case when count(d.iddocument)>0  then 'есть' else 'нет' end,
--b.amountbalance SumDolg, ac.*,bb.amountbalance SumDolg
from contract ct (nolock)
--left join document d (nolock) on ct.idcontract=d.idcontract
--	and d.idperiod>=@IDPeriod and d.idtypedocument=13
inner join person p (nolock) on ct.IDPerson=p.idperson
inner join address a (nolock) on a.idaddress=p.idaddress
inner join street s with (nolock)  on s.idstreet=a.idstreet
inner join house hs  with (nolock) on hs.idhouse=a.idhouse
inner join gobject go (nolock) on go.idcontract=ct.idcontract
--left join gru (nolock) on gru.idgru=go.idgru
--inner join balance b (nolock) on b.idcontract=ct.idcontract
--left join balance bb (nolock) on bb.idcontract=ct.idcontract
--inner join accounting ac (nolock) on b.idaccounting=ac.idaccounting
where ct.account<>'9999999' and ct.status<>2
--and b.idperiod=93 and bb.idperiod=@IDPeriod
--group by ct.idcontract, ct.Account, p.Surname, p.name, p.Patronic,
--s.name,hs.housenumber,hs.housenumberchar,a.flat
order by 4


update #Tbl
set PeriodPay=n.idper
from #Tbl t
inner join ( select idcontract, max(idperiod) as idper from document d with (nolock) 
where d.idtypedocument=1 group by idcontract) n on t.idcontract=n.idcontract

delete from #Tbl where PeriodPay>@IDPeriod

update #Tbl 
set dolgold=isnull(AA,0)
from #Tbl inner join ( select sum(round (AB,2)) AA ,  IdContract idCt from
(select sum(AmountBalance) AB, c.IdContract
from balance b with (nolock)
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod= @idPeriod and idaccounting<>6
Group by c.IdContract) qq  where AB<0 Group by IdContract ) ww on ww.idCt=idcontract 

update #Tbl 
set dolgnow=isnull(AA,0)
from #Tbl inner join ( select sum(round (AB,2)) AA ,  IdContract idCt from
(select sum(AmountBalance) AB, c.IdContract
from balance b with (nolock)
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod= @IDPeriodNow and idaccounting<>6
Group by c.IdContract) qq  where AB<0 Group by IdContract ) ww on ww.idCt=idcontract 

--update @Tbl 
--set dolgisk=isnull(AA,0)
--from @Tbl inner join ( select sum(round (AB,2)) AA ,  IdContract idCt from
--(select sum(AmountBalance) AB, c.IdContract
--from balance b with (nolock)
--inner join Contract c with (nolock) on b.IdContract=c.IdContract
--	and IdPeriod= @IDPeriodNow and idaccounting=2
--Group by c.IdContract) qq  where AB<0 Group by IdContract ) ww on ww.idCt=idcontract 

--update @Tbl set gruinvnumber=gruinvnumber+'-'+namegru 
--from @Tbl left join(select num, namegru from @TblGRU) TblGRU on num=gruinvnumber

update #Tbl 
set dolgold=isnull(dolgold*-1,0)

update #Tbl 
set dolgnow=isnull(dolgnow*-1,0)

--
--update @Tbl 
--set dolgisk=isnull(dolgisk*-1,0)

--update #Tbl set dolgold=dolgnow where dolgold>dolgnow

update #Tbl set FIO='Нет данных' where FIO is NUll


--select LS,  FIO, Address, replace(DolgOld,'.',',') dolgold,  replace(DolgNow,'.',',') DolgNow from #Tbl
--where dolgold>@dolg and dolgnow<>0
--order by dolgold desc

declare @SQL varchar(8000)
set @SQL='insert OpenDataSource(''Microsoft.Jet.OLEDB.4.0'',''Data Source="' + @path +
'";User ID=Admin;Password=;Extended properties=Excel 5.0'')...[Лист2$]
(c1, c2, c3, c4, c5, c6)
select' 

		set @SQL=@SQL+' LS,  FIO, Address, replace(DolgOld,''.'','','') dolgold,  replace(DolgNow,''.'','','') DolgNow, '
		set @SQL=@SQL+' '''+@Str+''''
		set @SQL=@SQL+' from #Tbl where dolgold>'+convert(varchar(20),@Dolg)+' and dolgnow<>0 order by 4'

--select @SQL
exec (@SQL)

drop table #Tbl








GO