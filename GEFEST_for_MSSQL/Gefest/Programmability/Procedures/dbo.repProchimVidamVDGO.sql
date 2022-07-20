SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO






-- =============================================
-- Author:		<Sumlikin,Aleksandr>
-- Create date: <2010-10-13>
-- Description:	<Отчет по прочим видам деятельности ВДГО>
-- =============================================
CREATE PROCEDURE [dbo].[repProchimVidamVDGO](@idperiod int) AS

----declare @idPeriod int
----set @idPeriod=79
declare @idPeriodPrev int
declare @SummaUslugUL float

set @idPeriodPrev=dbo.fGetPredPeriodVariable(@idPeriod)

declare @AnalSals table (BeginDt float, BeginKt float,IDTypeUslug int,TypeUslug varchar(200),ColAS int, ColVDGO int, ColPTO int, ColMPO int,ColGTM int,
Stoimost float,SummaUSlug float,SummaUSlugFL float,SummaUSlugFL1 float,SummaUSlugFL2 float,SummaUSlugUL float,SummaUSlugUL1 float,SummaUSlugUL2 float,AmountPay float,AmountPayUL float,AmountPayFL float, CarryPay float,CarryPayFL float,CarryPayUL float, korek float,
EndDt float, EndKt float)

insert @AnalSals (IDTypeUslug,TypeUslug,Stoimost)
select IDUslugiVDGO, Name, Value
from dbo.UslugiVDGO with (nolock) 

update @AnalSals 
set BeginKt=isnull(AA,0)
from( select sum(AB) AA from
(select sum(convert(decimal(10,2),AmountBalance)) AB
from balanceReal b
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod=@idPeriodPrev and idaccounting=6
Group by c.IdContract) qq  where AB>0) ww 

update @AnalSals 
set BeginDt=isnull(-AA,0)
from( select sum(AB) AA from
(select sum(convert(decimal(10,2),AmountBalance)) AB
from balanceReal b
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod= @idPeriodPrev and idaccounting=6
Group by c.IdContract) qq  where AB<0) ww 

update @AnalSals 
set EndDt=isnull(-AA,0)
from( select sum(AB) AA from
(select sum(convert(decimal(10,2),AmountBalance)) AB
from balanceReal b
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod= @idPeriod and idaccounting=6
Group by c.IdContract) qq  where AB<0) ww 

update @AnalSals 
set EndKt=isnull(AA,0)
from( select sum(AB) AA from
(select sum(convert(decimal(10,2),AmountBalance)) AB
from balanceReal b
inner join Contract c with (nolock) on b.IdContract=c.IdContract
	and IdPeriod= @idPeriod and idaccounting=6
Group by c.IdContract) qq  where AB>0) ww 
/*
update @AnalSals
set SummaUslug=isnull(ff.ss,0)
from @AnalSals aa
inner join (select sum(documentamount)ss,convert(int,pd.value) dd
from document d
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
group by convert(int,pd.value)) ff on ff.dd=aa.idtypeuslug

update @AnalSals
set SummaUslug=SummaUslug-isnull(ff.ss,0)
from @AnalSals aa
inner join (select sum(documentamount)ss,convert(int,pd.value) dd
from document d
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=7 and d.idperiod=@idperiod
group by convert(int,pd.value)) ff on ff.dd=aa.idtypeuslug
*/
update @AnalSals
set SummaUslugFL1=isnull(ff.ss,0)
from @AnalSals aa
inner join (select isnull(sum(documentamount),0)ss,convert(int,pd.value) dd
from document d
inner join contract c with (nolock) on d.idcontract=c.idcontract
inner join person p with (nolock) on c.idperson=p.idperson
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and (p.isjuridical=0 or (p.IsJuridical=1 and len(isnull(p.NumberDog,''))=0))
--and dbo.fGetIsJuridical(@idperiod,c.idperson)=0
group by convert(int,pd.value)) ff on ff.dd=aa.idtypeuslug

update @AnalSals
set SummaUslugFL2=-isnull(ff.ss,0)
from @AnalSals aa
inner join (select isnull(sum(documentamount),0)ss,convert(int,pd.value) dd
from document d
inner join contract c with (nolock) on d.idcontract=c.idcontract
inner join person p with (nolock) on c.idperson=p.idperson
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=7 and d.idperiod=@idperiod
and (p.isjuridical=0 or (p.IsJuridical=1 and len(isnull(p.NumberDog,''))=0))
--and dbo.fGetIsJuridical(@idperiod,c.idperson)=0
group by convert(int,pd.value)) ff on ff.dd=aa.idtypeuslug

update @AnalSals
set SummaUslugFL=isnull(SummaUslugFL1,0)+isnull(SummaUslugFL2,0)

update @AnalSals
set SummaUslugUL1=isnull(ff.ss,0)
from @AnalSals aa
inner join (select isnull(sum(documentamount),0)ss,convert(int,pd.value) dd
from document d
inner join contract c with (nolock) on d.idcontract=c.idcontract
inner join person p with (nolock) on c.idperson=p.idperson
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
and p.IsJuridical=1 and len(isnull(p.NumberDog,''))>0	
--and dbo.fGetIsJuridical(@idperiod,c.idperson)=1
group by convert(int,pd.value)) ff on ff.dd=aa.idtypeuslug

update @AnalSals
set SummaUslugUL2=-isnull(ff.ss,0)
from @AnalSals aa
inner join (select sum(documentamount)ss,convert(int,pd.value) dd
from document d
inner join contract c with (nolock) on d.idcontract=c.idcontract
inner join person p with (nolock) on c.idperson=p.idperson
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=7 and d.idperiod=@idperiod
and p.IsJuridical=1 and len(isnull(p.NumberDog,''))>0	
--and dbo.fGetIsJuridical(@idperiod,c.idperson)=1
group by convert(int,pd.value)) ff on ff.dd=aa.idtypeuslug

update @AnalSals
set SummaUslugUL=isnull(SummaUslugUL1,0)+isnull(SummaUslugUL2,0)

update @AnalSals
set SummaUslug=SummaUslugFL+SummaUslugUL
--update @analSals
--set korek=-qq.DocumentAmount
--from (select  sum(o.amountoperation) DocumentAmount
--from document dc with (nolock)
--inner join operation o with (nolock)  on o.iddocument=dc.iddocument
--and	dc.idtypedocument=7 and dc.idperiod=@idPeriod
--inner join balance b with (nolock) on b.idbalance=o.idbalance
--and idaccounting=6 
--) qq 

update @AnalSals 
set AmountPay=isnull(dd,0)
from (select sum(convert(decimal(10,2),o.amountoperation)) dd
from operation oinner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and b.idaccounting=6 and o.idtypeoperation=1) qq
	--and b.idaccounting=6 and (o.idtypeoperation=1 or o.idtypeoperation=3)) qq

update @AnalSals 
set AmountPayUL=isnull(dd,0)
from (select sum(convert(decimal(10,2),o.amountoperation)) dd
from operation oinner join balance b on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract
inner join person p with (nolock) on c.idperson=p.idperson
	where b.idperiod=@idperiod and p.isjuridical=1 and len(isnull(p.NumberDog,''))>0
	and b.idaccounting=6 and o.idtypeoperation=1) qq
	--and b.idaccounting=6 and (o.idtypeoperation=1 or o.idtypeoperation=3)) qq

update @AnalSals 
set AmountPayFL=isnull(dd,0)
from (select sum(convert(decimal(10,2),o.amountoperation)) dd
from operation oinner join balance b on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract
inner join person p with (nolock) on c.idperson=p.idperson
	where b.idperiod=@idperiod and (p.isjuridical=0 or (p.IsJuridical=1 and len(isnull(p.NumberDog,''))=0))
	and b.idaccounting=6 and o.idtypeoperation=1) qq

update @AnalSals 
set CarryPay=isnull(dd,0)
from (select sum(o.amountoperation)*-1 dd
from operation o  with (nolock) 
inner join document d  with (nolock) on o.iddocument=d.iddocument
and d.idperiod=@idperiod --and idtypedocument=3
inner join balance b with (nolock) on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract 
	and b.idperiod=@idperiod and idtypedocument<>7
	and b.idaccounting=6 and o.idtypeoperation=3) qq--(o.idtypeoperation=1 or o.idtypeoperation=3)) qq


update @AnalSals 
set CarryPayFL=isnull(dd,0)
from (select sum(o.amountoperation)*-1 dd
from operation o  with (nolock) 
inner join document d  with (nolock) on o.iddocument=d.iddocument
and d.idperiod=@idperiod  --and idtypedocument=3
inner join balance b with (nolock) on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract --and dbo.fGetIsJuridical(c.idperson,@idperiod)=0 
inner join person p with (nolock) on c.idperson=p.idperson
	where b.idperiod=@idperiod and idtypedocument<>7
	and b.idaccounting=6 and o.idtypeoperation=3
	and (p.isjuridical=0 or (p.IsJuridical=1 and len(isnull(p.NumberDog,''))=0))) qq


update @AnalSals 
set CarryPayUL=isnull(dd,0)
from (select sum(o.amountoperation)*-1 dd
from operation o  with (nolock) 
inner join document d  with (nolock) on o.iddocument=d.iddocument
and d.idperiod=@idperiod and idtypedocument<>7 --and idtypedocument=3
inner join balance b with (nolock) on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract --and dbo.fGetIsJuridical(c.idperson,@idperiod)=1
inner join person p with (nolock) on c.idperson=p.idperson
	where b.idperiod=@idperiod
	and b.idaccounting=6 and o.idtypeoperation=3
	and (p.IsJuridical=1 and len(isnull(p.NumberDog,''))>0)) qq--(o.idtypeoperation=1 or o.idtypeoperation=3)) qq


update @AnalSals
set ColVDGO=ff.gg
from @AnalSals aa
inner join (select count(0)gg ,convert(int,ss.value) dd
from document d
inner join pd as ss on ss.iddocument=d.iddocument
and ss.idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
inner join pd as dd on dd.iddocument=d.iddocument
and dd.idtypepd=16 and convert(int,dd.value)=90
group by convert(int,ss.value)) ff on ff.dd=aa.idtypeuslug

update @AnalSals
set ColVDGO=ColVDGO-ff.gg
from @AnalSals aa
inner join (select count(0)gg ,convert(int,ss.value) dd
from document d
inner join pd as ss on ss.iddocument=d.iddocument
and ss.idtypepd=35 and idtypedocument=7 and d.idperiod=@idperiod
inner join pd as dd on dd.iddocument=d.iddocument
and dd.idtypepd=16 and convert(int,dd.value)=90
group by convert(int,ss.value)) ff on ff.dd=aa.idtypeuslug


update @AnalSals
set ColAS=ff.gg
from @AnalSals aa
inner join (select count(0)gg ,convert(int,ss.value) dd
from document d
inner join pd as ss on ss.iddocument=d.iddocument
and ss.idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
inner join pd as dd on dd.iddocument=d.iddocument
and dd.idtypepd=16 and convert(int,dd.value)=89
group by convert(int,ss.value)) ff on ff.dd=aa.idtypeuslug

update @AnalSals
set ColAS=ColAS-ff.gg
from @AnalSals aa
inner join (select count(0)gg ,convert(int,ss.value) dd
from document d
inner join pd as ss on ss.iddocument=d.iddocument
and ss.idtypepd=35 and idtypedocument=7 and d.idperiod=@idperiod
inner join pd as dd on dd.iddocument=d.iddocument
and dd.idtypepd=16 and convert(int,dd.value)=89
group by convert(int,ss.value)) ff on ff.dd=aa.idtypeuslug


update @AnalSals
set ColPTO=ff.gg
from @AnalSals aa
inner join (select count(0)gg ,convert(int,ss.value) dd
from document d
inner join pd as ss on ss.iddocument=d.iddocument
and ss.idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
inner join pd as dd on dd.iddocument=d.iddocument
and dd.idtypepd=16 and convert(int,dd.value)=120
group by convert(int,ss.value)) ff on ff.dd=aa.idtypeuslug

update @AnalSals
set ColPTO=ColPTO-ff.gg
from @AnalSals aa
inner join (select count(0)gg ,convert(int,ss.value) dd
from document d
inner join pd as ss on ss.iddocument=d.iddocument
and ss.idtypepd=35 and idtypedocument=7 and d.idperiod=@idperiod
inner join pd as dd on dd.iddocument=d.iddocument
and dd.idtypepd=16 and convert(int,dd.value)=120
group by convert(int,ss.value)) ff on ff.dd=aa.idtypeuslug

update @AnalSals
set ColMPO=ff.gg
from @AnalSals aa
inner join (select count(0)gg ,convert(int,ss.value) dd
from document d
inner join pd as ss on ss.iddocument=d.iddocument
and ss.idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
inner join pd as dd on dd.iddocument=d.iddocument
and dd.idtypepd=16 and convert(int,dd.value)=121
group by convert(int,ss.value)) ff on ff.dd=aa.idtypeuslug

update @AnalSals
set ColMPO=ColMPO-ff.gg
from @AnalSals aa
inner join (select count(0)gg ,convert(int,ss.value) dd
from document d
inner join pd as ss on ss.iddocument=d.iddocument
and ss.idtypepd=35 and idtypedocument=7 and d.idperiod=@idperiod
inner join pd as dd on dd.iddocument=d.iddocument
and dd.idtypepd=16 and convert(int,dd.value)=121
group by convert(int,ss.value)) ff on ff.dd=aa.idtypeuslug

update @AnalSals
set ColGTM=ff.gg
from @AnalSals aa
inner join (select count(0)gg ,convert(int,ss.value) dd
from document d
inner join pd as ss on ss.iddocument=d.iddocument
and ss.idtypepd=35 and idtypedocument=24 and d.idperiod=@idperiod
inner join pd as dd on dd.iddocument=d.iddocument
and dd.idtypepd=16 and convert(int,dd.value)=137
group by convert(int,ss.value)) ff on ff.dd=aa.idtypeuslug

update @AnalSals
set ColGTM=ColGTM-ff.gg
from @AnalSals aa
inner join (select count(0)gg ,convert(int,ss.value) dd
from document d
inner join pd as ss on ss.iddocument=d.iddocument
and ss.idtypepd=35 and idtypedocument=7 and d.idperiod=@idperiod
inner join pd as dd on dd.iddocument=d.iddocument
and dd.idtypepd=16 and convert(int,dd.value)=137
group by convert(int,ss.value)) ff on ff.dd=aa.idtypeuslug


update @AnalSals set ColAS=0 where ColAS is null
update @AnalSals set ColVDGO=0 where ColVDGO is null
update @AnalSals set ColPTO=0 where ColPTO is null
update @AnalSals set ColMPO=0 where ColMPO is null
update @AnalSals set ColGTM=0 where ColGTM is null

--update @AnalSals set SummaUSlug=isnull(SummaUSlug,0)+isnull(korek,0) where idtypeuslug=1
select * from @AnalSals
where ColAS+ColVDGO+ColPTO+ColMPO+ColGTM>0
























GO