SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO




CREATE PROCEDURE [dbo].[repAccountList] (@Type int) AS

if @type=1
begin
select @type Type, ct.Account as LIC_ACC, isnull(p.Surname,'')+' '+isnull(p.name,'')+' '+isnull(p.Patronic,'') as FIO ,
s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat) as ADDRESS,
ag.name
from contract ct (nolock)
inner join person p (nolock) on ct.IDPerson=p.idperson
inner join address a (nolock) on a.idaddress=p.idaddress
inner join street s (nolock)  on s.idstreet=a.idstreet
inner join house hs (nolock) on hs.idhouse=a.idhouse
inner join gobject go (nolock) on go.idcontract=ct.idcontract
inner join gru (nolock) on gru.idgru=go.idgru
inner join agent ag (nolock) on ag.idagent=gru.idagent
where ct.printchetizvehen=1
order by ag.name
end
else
begin
select @type, ct.Account as LIC_ACC, isnull(p.Surname,'')+' '+isnull(p.name,'')+' '+isnull(p.Patronic,'') as FIO ,
s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat) as ADDRESS,
ag.name
from contract ct (nolock)
inner join person p (nolock) on ct.IDPerson=p.idperson
inner join address a (nolock) on a.idaddress=p.idaddress
inner join street s (nolock)  on s.idstreet=a.idstreet
inner join house hs (nolock) on hs.idhouse=a.idhouse
inner join gobject go (nolock) on go.idcontract=ct.idcontract
inner join gru (nolock) on gru.idgru=go.idgru
inner join agent ag (nolock) on ag.idagent=gru.idagent
where ct.chargepeny=0
order by ag.name
end






GO