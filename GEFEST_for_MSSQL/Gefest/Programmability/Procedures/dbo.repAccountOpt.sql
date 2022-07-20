SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

CREATE PROCEDURE [dbo].[repAccountOpt] (@IdGRU int) AS
--declare @IdGRU int
--set @IdGru=8633
declare @idperiod int
set @idperiod=dbo.fGetNowPeriod()
declare @dBegin datetime
declare @dEnd datetime
set @dBegin= dbo.fGetDatePeriod(@IdPeriod,1)
set @dEnd= dbo.fGetDatePeriod(@IdPeriod,0)

select c.Account, c.IdContract, gm.idgmeter, dbo.fGetStatusPU (@dEnd,gm.idgmeter) StatusGMeter,
dbo.fGetCountLives(g.idgobject ,@IdPeriod) CL,
np.Surname +' '+ltrim(isnull(np.name,''))+' '+ltrim(isnull(np.Patronic,''))FIO,  
s.name+ ', дом-'+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '  кв. '+ltrim(a.flat) Adres, 
isnull(tt.name,'') Marka,
isnull(gm.Serialnumber,'б/н') Serialnumber,
dateadd(yy, tt.servicelife, case when gm.dateverify='1800-01-01' then gm.datefabrication else gm.dateverify end) dateverify,
gm.DateFabrication as GmeterDateFabrication, gm.dateverify as sdateverify,  isnull(ltrim(tt.ClassAccuracy),'') Tip,  st.Name as Gobject,
isnull(pp.NumberPhone,'') as NumberPhone, @dEnd as Date

into #tmpChetIzvehe
from contract c with (nolock) 
inner join GObject g  with (nolock) on g.IdContract=c.IdContract
	and g.IdGRU=@IdGru 	--and isnull(c.PrintChetIzvehen,0)=0
inner join address a  with (nolock) on a.idaddress=g.idaddress
inner join street s with (nolock)  on s.idstreet=a.idstreet
inner join house hs  with (nolock) on hs.idhouse=a.idhouse
inner join gru h with (nolock)  on h.idgru=g.idgru
inner join person np with (nolock)  on np.idperson=c.Idperson
left join GMeter gm  with (nolock) on gm.IdGObject=g.IdGObject
	and dbo.fGetStatusPU (@dEnd,gm.idgmeter)=1 
left join StatusGobject st with (nolock) on g.IDStatusGobject = st.IDStatusGobject
left join Phone pp with (nolock) on pp.idperson=np.idperson
left join typegmeter tt with (nolock)  on gm.idtypegmeter=tt.idtypegmeter
left join Document d  with (nolock) on d.idcontract=c.idcontract
and  d.IdPeriod=@IdPeriod and (d.idtypedocument=1  or d.idtypedocument=3)
 group by c.IdContract, c.Account, g.IDGobject, 
 np.Surname, np.name, np.Patronic,  h.name ,s.name, hs.housenumber, hs.housenumberchar, 
 gm.IdGMeter, a.flat, tt.name, tt.ClassAccuracy,
 gm.Serialnumber,tt.servicelife,gm.dateverify,gm.DateFabrication,st.Name,pp.NumberPhone,gm.dateverify



select t.* 
from #tmpChetIzvehe t
left join (select idcontract from gobject  with (nolock)  where idstatusgobject=2 and idgru=@idgru and isnull( dbo.fGetLastBalance (@IdPeriod, IdContract, 0),0)>-1) c on c.idcontract=t.idcontract
--where c.idcontract is null
order by account

drop table #tmpChetIzvehe



GO