SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO







CREATE PROCEDURE [dbo].[repSpravkaPoOtklKV] (@idperiod int, @idgru int,@Status int, @isJuridical int, @idagent int, @IndHouse int) AS
--declare @idgru int
--set @idgru=0
--declare @Status int
--set @status=1
--declare @isJuridical int
--set @isJuridical=0
--declare @IndHouse int
--set @IndHouse=0
declare @Date as datetime
set @Date=dbo.fGetDatePeriod(@idperiod,0)

create table #tmpBalance (idcontract int, account  varchar(20),invnumber  varchar(100),  FIO varchar(500), 
	address varchar(500), flat varchar(20) , idstatusgobject varchar(100), gru varchar(100),  agent  varchar(50), 
	DateOtkl datetime, Ispolnitel varchar(50), Memo varchar(8000))

declare @q varchar(8000)
set @q='insert #tmpBalance (idcontract, account, FIO, address, idstatusgobject, 
	 gru,  agent, invnumber,flat,Memo)
select c.idcontract, c.account, ltrim(p.surname)+'' ''+ ltrim(isnull(p.name,''''))+'' ''+ltrim(isnull(p.patronic,'''')) FIO, 
	s.name+'' ''+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'''') address,
	g.idstatusgobject, gru.name gru, ag.name agent, gru.invnumber,a.flat,c.Memo
from contract c with (nolock) 
inner join person p with (nolock) on p.idperson=c.idperson '
if (@Status>0)
set @q=@q+' and replace(c.status,0,1)='+ltrim(str(@Status))
if (@isJuridical>-1)
set @q=@q+' and isJuridical='+ltrim(str(@isJuridical))
set @q=@q+' inner join GObject g with (nolock) on g.idcontract=c.idcontract 
 and g.idstatusgobject=2  
 inner join GRU with (nolock) on gru.idgru=g.idgru'
if (isnull(@idgru,0)>0)
	set @q=@q+' and g.idgru='+ltrim(str(@idgru))
set @q=@q+' inner join agent ag with (nolock) on ag.idagent=gru.idagent'
if (isnull(@idagent,0)>0)
	set @q=@q+' and ag.idagent='+ltrim(str(@idagent))
set @q=@q+' inner join address a with (nolock) on a.idaddress=g.idaddress
inner join house h on h.idhouse=a.idhouse 
inner join street s with (nolock) on s.idstreet=a.idstreet'

if (@IndHouse>0)
begin
	set @q=@q+' where convert(int,substring(c.account,4,1))='+str(@IndHouse)
end



exec (@q)

update #tmpBalance
set idstatusgobject=(select top 1 name from document d   with (nolock)  
 		inner join PD  with (nolock)   on pd.iddocument=d.iddocument 
 			and pd.idtypepd=14 
 			and d.idtypedocument=17 
 			and d.idcontract=#tmpBalance.idcontract
 		inner join typeend t  with (nolock)   on t.idtypeend=pd.value
 		order by  d.dateadd desc)

update #tmpBalance
set idstatusgobject='ОТКЛ'
where idstatusgobject is null

update #tmpBalance
set ispolnitel=(select top 1 a.name from document d   with (nolock)  
 		inner join PD  with (nolock)   on pd.iddocument=d.iddocument 
 			and pd.idtypepd=16 
 			and d.idtypedocument=17 
 			and d.idcontract=#tmpBalance.idcontract
 		inner join agent a  with (nolock) on a.idagent=convert(int,ltrim(pd.value))
 		order by d.documentdate desc)

update #tmpBalance
set DateOtkl=(select top 1 documentdate from document d   with (nolock)  
 		where d.idtypedocument=17 
 			and d.idcontract=#tmpBalance.idcontract order by documentdate desc)

delete from #tmpBalance where DateOtkl>=@Date

select * from #tmpBalance order by invnumber, account
drop table #tmpBalance





GO