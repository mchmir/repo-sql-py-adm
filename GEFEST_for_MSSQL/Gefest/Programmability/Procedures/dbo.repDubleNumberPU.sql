SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO


-- =============================================
-- Author:		<Asumlikin,Aleksandr>
-- Create date: <2007-06-20,,>
-- Description:	<Справка по ПУ с одинаковыми номерами>
-- =============================================
CREATE PROCEDURE [dbo].[repDubleNumberPU]   (@serialnumber varchar(50), @idtypegmeter int)
AS
--declare @serialnumber varchar(50)
--set @serialnumber='000772'

if @serialnumber=0
begin 
select  c.account, ltrim(isnull(p.surname,' '))+' '+ Left(ltrim(isnull(p.name,' ')),1)+' '+ltrim(left(isnull(p.patronic,' '),1)) FIO, 
s.name+', '+ltrim(str(isnull(h.housenumber,'')))+isnull(h.housenumberchar,'')+', '+isnull(a.flat,'') address
,g.idstatusgobject, ag.name, gm.serialnumber,case when gm.idstatusgmeter=1 then 'подкл.' else 'откл.' end  StatusPU, t.name namegmeter
,DateFabrication from contract c with (nolock) 
inner join person p with (nolock) on p.idperson=c.idperson
inner join GObject g with (nolock) on g.idcontract=c.idcontract 
inner join GRU with (nolock) on gru.idgru=g.idgru
inner join agent ag with (nolock) on ag.idagent=gru.idagent
inner join address a with (nolock) on a.idaddress=g.idaddress
inner join house h on h.idhouse=a.idhouse
inner join street s with (nolock) on s.idstreet=a.idstreet
inner join gmeter gm on gm.idgobject=g.idgobject
--inner join (select count (serialnumber) c, serialnumber, idtypegmeter from gmeter group by serialnumber,idtypegmeter having count (serialnumber)>1 and serialnumber is not null) ff
inner join (select count (serialnumber) c, serialnumber from gmeter group by serialnumber having count (serialnumber)>1 and serialnumber is not null) ff
on gm.serialnumber=ff.serialnumber 
--and ff.idtypegmeter=gm.idtypegmeter
inner join typegmeter t on t.idtypegmeter=gm.idtypegmeter
where gm.serialnumber is not null and len(gm.serialnumber)>3 
order by gm.serialnumber
end 
else 
begin 
select  c.account, ltrim(isnull(p.surname,' '))+' '+ Left(ltrim(isnull(p.name,' ')),1)+' '+ltrim(left(isnull(p.patronic,' '),1)) FIO, 
s.name+', '+ltrim(str(isnull(h.housenumber,'')))+isnull(h.housenumberchar,'')+', '+isnull(a.flat,'') address
,g.idstatusgobject, ag.name, gm.serialnumber,case when gm.idstatusgmeter=1 then 'подкл.' else 'откл.' end  StatusPU, t.name namegmeter
,DateFabrication from contract c with (nolock) 
inner join person p with (nolock) on p.idperson=c.idperson
inner join GObject g with (nolock) on g.idcontract=c.idcontract 
inner join GRU with (nolock) on gru.idgru=g.idgru
inner join agent ag with (nolock) on ag.idagent=gru.idagent
inner join address a with (nolock) on a.idaddress=g.idaddress
inner join house h on h.idhouse=a.idhouse
inner join street s with (nolock) on s.idstreet=a.idstreet
inner join gmeter gm on gm.idgobject=g.idgobject
inner join typegmeter t on t.idtypegmeter=gm.idtypegmeter
where gm.serialnumber=@serialnumber and gm.idtypegmeter=@idtypegmeter
order by gm.serialnumber
end

GO