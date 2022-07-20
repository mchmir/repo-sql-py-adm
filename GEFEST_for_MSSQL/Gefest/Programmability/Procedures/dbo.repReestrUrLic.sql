SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO











CREATE procedure [dbo].[repReestrUrLic] (@IdPeriod int, @per int) as 

--declare @idperiod int
--declare @per int
--set @idperiod=42
--set @per=-1


declare @dEnd datetime

set @dEnd= dbo.fGetDatePeriod(@IdPeriod,0)
select c.account, ltrim(isnull(p.surname,'')) NamePerson, 
case when len(isnull(p.name,''))=0 then isnull(p.patronic,'') else
isnull(p.name,'')+''+isnull(', '+p.patronic,'') end FIO, p.RNN,
	s.name+' '+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'')+'-'+isnull(a.flat,'') address,
	t.name+', '+ltrim(str(t.classaccuracy, 3, 1)) gmeter,
 dbo.fGetLastBalance(@idperiod, c.idcontract, 0) balance, 
case when dbo.fGetStatusContract(c.idcontract,@idperiod)=0 then 'Не определен' else
case when dbo.fGetStatusContract(c.idcontract,@idperiod)=1 then 'Активен' else
case when dbo.fGetStatusContract(c.idcontract,@idperiod)=2 then 'Закрыт'  end end end as asd, dbo.fGetStatusContract(c.idcontract,@idperiod) ff
, aa.name agent, 
p.NumberDog NumberDog, convert(varchar(50),p.DateDog,104) DateDog, pp.numberphone, c.memo
into #tmpReestURlic
from contract c with (nolock)
inner join person p with (nolock) on p.idperson=c.idperson
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join address a with (nolock) on a.idaddress=p.idaddress
inner join house h with (nolock) on a.idhouse=h.idhouse
inner join street s with (nolock) on s.idstreet=a.idstreet
inner join gobject go with (nolock) on go.idcontract=c.idcontract
inner join gru with (nolock) on gru.idgru=go.idgru
inner join agent aa with (nolock) on aa.idagent=gru.idagent
left join GMeter gm  with (nolock) on gm.IdGObject=go.IdGObject
	and dbo.fGetStatusPU (@dEnd,gm.idgmeter)=1 
left join typegmeter t with (nolock) on t.idtypegmeter=gm.idtypegmeter
left join phone pp with (nolock) on pp.idperson=p.idperson

order by account


if @per>-1
delete #tmpReestURlic where ff<>@per

select * from #tmpReestURlic order by account
drop table #tmpReestURlic


GO