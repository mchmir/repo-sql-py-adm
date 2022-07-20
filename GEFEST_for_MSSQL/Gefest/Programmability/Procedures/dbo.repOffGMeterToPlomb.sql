SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO







CREATE procedure [dbo].[repOffGMeterToPlomb] (@idperiod int) as

declare @T table (account varchar(7), FIO varchar(200), ADDRESS varchar(200), pu varchar(200), documentdate datetime, idgm int, agentname varchar(50))
insert into @T (account, FIO, ADDRESS, pu, documentdate, idgm, agentname)
select ct.account,isnull(p.Surname,'')+' '+isnull(p.name,'')+' '+isnull(p.Patronic,'')+', т.'+ltrim(isnull(ph.NumberPhone,'-')) as FIO ,
s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat) as ADDRESS, 
tg.name+', '+ltrim(convert(nchar (3),tg.ClassAccuracy))+', №'+gm.SerialNumber as pu, doc.documentdate as documentdate,
gm2.idgmeter, ag.name
from contract ct
inner join person p (nolock) on ct.IDPerson=p.idperson
left join phone ph (nolock) on p.idperson=ph.idperson
inner join address a (nolock) on a.idaddress=p.idaddress
inner join street s with (nolock)  on s.idstreet=a.idstreet
inner join house hs  with (nolock) on hs.idhouse=a.idhouse
inner join (select max(iddocument) iddocument, idcontract from document doc (nolock) where
	doc.idtypedocument=18 group by idcontract) dc on dc.idcontract=ct.idcontract
inner join document doc (nolock) on doc.iddocument=dc.iddocument
--inner join document doc (nolock) on doc.idcontract=ct.idcontract
--	and doc.idtypedocument=18
inner join pd (nolock) on pd.iddocument=doc.iddocument
	and pd.idtypepd=33 and pd.value='14'
inner join pd pd2 (nolock) on pd2.iddocument=doc.iddocument
	and pd2.idtypepd=7
inner join gobject go (nolock) on ct.idcontract=go.idcontract
	and go.IDStatusGObject=1
inner join gru (nolock) on gru.idgru=go.idgru
inner join gmeter gm (nolock) on gm.idgobject=go.idgobject
	and gm.IDStatusGMeter=2 and gm.idgmeter=pd2.value
left join gmeter gm2 (nolock) on gm2.idgobject=go.idgobject
	and gm2.IDStatusGMeter=1 
inner join agent ag (nolock) on ag.idagent=gru.idagent
left join TypeGmeter tg (nolock) on gm.idtypegmeter=tg.idtypegmeter
order by ag.name, doc.documentdate

delete from @T where idgm is not null

select account, FIO, ADDRESS, pu, convert(varchar(10),max(documentdate),104) as documentdate, agentname from @T
group by account, FIO, ADDRESS, pu, agentname
order by agentname, documentdate





GO