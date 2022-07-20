SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO


CREATE procedure [dbo].[repRaznoska] (@dBegin datetime, @dEnd datetime) as 
SET NOCOUNT ON
--declare @dBegin datetime
--declare @dEnd datetime
--set @dBegin='2007-10-01'
--set @dEnd='2007-10-31'

create table #ana (Iduser int, indication int, nameoperator varchar(20), document int, indicationTelefon int)

insert #ana (iduser, nameoperator, indication, document, indicationTelefon)
select nnew,login, 0, 0,0
from sysuser  with (nolock)
where iduser in(3,7,8,9,10,11,18,21,22,24,23,26)

update #ana
set indication=coun
from(select  count (1) coun ,iduser
from indication i  with (nolock)
where dbo.dateonly(i.dateadd)>=dbo.dateonly(@dBegin) and dbo.dateonly(i.dateadd)<=dbo.dateonly(@dEnd) and isnull(idtypeindication,0)<>1
group by iduser)dd inner join #ana a on a.iduser=dd.iduser

update #ana
set indicationTelefon=coun
from(select  count (1) coun ,iduser
from indication i  with (nolock)
where dbo.dateonly(i.dateadd)>=dbo.dateonly(@dBegin) and dbo.dateonly(i.dateadd)<=dbo.dateonly(@dEnd) and isnull(idtypeindication,0)=1
group by iduser)dd inner join #ana a on a.iduser=dd.iduser

update #ana
set document=coun
from(select  count (1) coun ,iduser
from document d  with (nolock)
where dbo.dateonly(d.dateadd)>=dbo.dateonly(@dBegin) and dbo.dateonly(d.dateadd)<=dbo.dateonly(@dEnd)
group by iduser)dd inner join #ana a on a.iduser=dd.iduser

select * from #ana
order by nameoperator
drop table #ana

GO