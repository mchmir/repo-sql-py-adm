SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO






-- =============================================
-- Author:		<Sumlikin,Aleksandr>
-- Create date: <2010-10-12>
-- Description:	<Отчет по ВДГО>
-- =============================================
CREATE PROCEDURE [dbo].[repUslugiVDGO](@idperiod int,@idagent int) AS
--
--declare @idperiod int
--declare @idagent int
--set @idperiod=79
--set @idagent=0

create table #tmpVDGO (iddocument int, account  varchar(20), 
 FIO varchar(500), address varchar(500),date datetime, Typeuslug varchar(200),
Ispol varchar(200),docamaunt float,idagent int) 
	
insert #tmpVDGO (iddocument,account,FIO,address,date,Typeuslug,docamaunt)
select distinct d.iddocument,c.account,np.Surname+' '+left(ltrim(isnull(np.name,'')),1)+'.' +left(ltrim(isnull(np.Patronic,'')),1),
s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(aa.flat),
d.documentdate, u.Name,d.documentamount from document d
inner join contract c on c.idcontract=d.idcontract
and idtypedocument=24 and d.idperiod=@idperiod
inner join GObject g  with (nolock) on g.IdContract=c.IdContract
inner join address aa  with (nolock) on aa.idaddress=g.idaddress
inner join street s with (nolock)  on s.idstreet=aa.idstreet
inner join house hs  with (nolock) on hs.idhouse=aa.idhouse
inner join person np with (nolock)  on np.idperson=c.Idperson
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35
inner join UslugiVDGO u on u.IDUslugiVDGO=convert(int,pd.value)



insert #tmpVDGO (iddocument,account,FIO,address,date,docamaunt)
select distinct dc.iddocument,c.account,np.Surname+' '+left(ltrim(isnull(np.name,'')),1)+'.' +left(ltrim(isnull(np.Patronic,'')),1),
s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(aa.flat),
dc.documentdate, -dc.documentamount
from document dc with (nolock)
inner join operation o with (nolock)  on o.iddocument=dc.iddocument
and	dc.idtypedocument=7 and dc.idperiod=@idperiod
inner join balance b with (nolock) on b.idbalance=o.idbalance
and idaccounting=6
inner join contract c on c.idcontract=dc.idcontract
inner join GObject g  with (nolock) on g.IdContract=c.IdContract
inner join address aa  with (nolock) on aa.idaddress=g.idaddress
inner join street s with (nolock)  on s.idstreet=aa.idstreet
inner join house hs  with (nolock) on hs.idhouse=aa.idhouse
inner join person np with (nolock)  on np.idperson=c.Idperson

update #tmpVDGO
set ispol=a.name,
idagent=a.idagent
from #tmpVDGO tt
inner join pd on pd.iddocument=tt.iddocument
inner join agent a on a.idagent=convert(int,pd.value) and pd.idtypepd=16

if (isnull(@idagent,0)<>0)
begin
delete #tmpVDGO
where idagent<>@idagent
end

select * from #tmpVDGO order by date asc
drop table #tmpVDGO












GO