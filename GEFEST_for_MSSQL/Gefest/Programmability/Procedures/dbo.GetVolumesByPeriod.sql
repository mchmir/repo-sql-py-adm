SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO





CREATE Procedure [dbo].[GetVolumesByPeriod] as
begin

declare @tbl table (Account varchar (10), idhouse int, haddress varchar (200), address varchar (200), 
a2014 float,a2015 float,f2014 float,f2015 float)
insert into @tbl (Account, idhouse, haddress, address, f2015) --a2014, a2015, f2014, f2015)
select ct.Account as LIC_ACC, hs.idhouse, s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'') as haddress,
s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat) as ADDRESS,
--isnull(fu2014.FactAmount,0)+isnull(fu2014k.FactAmount,0) as '2014',
isnull(fu2015.FactAmount,0)+isnull(fu2015k.FactAmount,0) as '2016'  
from contract ct (nolock)
inner join person p (nolock) on ct.IDPerson=p.idperson
inner join address a (nolock) on a.idaddress=p.idaddress
inner join street s with (nolock)  on s.idstreet=a.idstreet
inner join house hs  with (nolock) on hs.idhouse=a.idhouse
	--and hs.idhouse in (34571,34476,34087,34115,34160,34164,34419,34438,34228,34526)
inner join gobject go with (nolock) on go.idcontract=ct.idcontract
inner join gru with (nolock) on go.idgru=gru.idgru

----2014
--left join (select round(sum(f.FactAmount),3) as FactAmount, sum(d.DocumentAmount) as DocumentAmount, f.idgobject from factuse f with (nolock)
--	inner join operation o   with (nolock) on o.idoperation=f.idoperation
--	inner join document d with (nolock) on d.iddocument=o.iddocument
--	and idtypedocument=5
--	and  f.IdPeriod>=132 and  f.IdPeriod<=132
--	inner join contract c with (nolock) on d.idcontract=c.idcontract 
--	where isnull(f.IdTypeFU,0)=1 or isnull(f.IdTypeFU,0)=2
--	group by idgobject) fu2014 on go.idgobject=fu2014.idgobject
----2014k
--left join (select sum(factamount) FactAmount, sum(d.DocumentAmount) as DocumentAmount, f.idgobject
--	from document d with (nolock) 
--	inner join contract c  with (nolock)  on c.idcontract=d.idcontract
--	and idtypedocument=7 and d.idperiod>=132 and  d.IdPeriod<=132
--	inner join pd with (nolock)  on pd.iddocument=d.iddocument
--	and idtypepd=13 
--	inner join factuse f with (nolock)  on f.iddocument=d.iddocument
--	group by f.idgobject) fu2014k on go.idgobject=fu2014k.idgobject
--2015
left join (select sum(f.FactAmount) as FactAmount, sum(d.DocumentAmount) as DocumentAmount, f.idgobject from factuse f with (nolock)
	inner join operation o   with (nolock) on o.idoperation=f.idoperation
	inner join document d with (nolock) on d.iddocument=o.iddocument
	and idtypedocument=5
	and  f.IdPeriod>=141 and  f.IdPeriod<=152
	inner join contract c with (nolock) on d.idcontract=c.idcontract 
	where isnull(f.IdTypeFU,0)=1 or isnull(f.IdTypeFU,0)=2
	group by idgobject) fu2015 on go.idgobject=fu2015.idgobject
--2015k
left join (select sum(f.FactAmount) FactAmount, sum(d.DocumentAmount) as DocumentAmount, f.idgobject
	from document d with (nolock) 
	inner join contract c  with (nolock)  on c.idcontract=d.idcontract
	and idtypedocument=7 and d.idperiod>=141 and  d.IdPeriod<=152
	inner join pd with (nolock)  on pd.iddocument=d.iddocument
	and idtypepd=13 
	inner join factuse f with (nolock)  on f.iddocument=d.iddocument
	group by f.idgobject) fu2015k on go.idgobject=fu2015k.idgobject


where ct.account<>'9999999' 
order by 2

--delete from @tbl where f2012=0 and f2013=0 and f2014=0 and f2015=0 and f20152=0
--select * from @tbl
select ROW_NUMBER() OVER(ORDER BY haddress) as num, account, haddress, address, f2014, f2015
from @tbl
where f2015>0 --and f2014>0
order by haddress

--select sum(f2014) from @tbl

select sum(f2015) from @tbl


end


GO