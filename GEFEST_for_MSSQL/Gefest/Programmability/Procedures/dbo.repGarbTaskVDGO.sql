SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

CREATE procedure [dbo].[repGarbTaskVDGO] (@idhouse int) as

select ct.Account as LIC_ACC, isnull(p.Surname,'')+' '+isnull(p.name,'')+' '+isnull(p.Patronic,'') as FIO ,
s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat) as ADDRESS,
tpl.name, convert(varchar(10),pl.dateinstall,104) as pldateinstall,
sc.path, gm.serialnumber+', '+tgm.name+', '+convert(varchar (5), tgm.classaccuracy) as gmetername, 
convert(varchar(10),gm.datefabrication,104) as datefabrication,
convert(varchar(10),gm.dateinstall,104) as dateinstall,
convert(varchar(10),gm.dateverify,104) as dateverify 
from contract ct (nolock)
inner join person p with (nolock) on ct.IDPerson=p.idperson
inner join address a with (nolock) on a.idaddress=p.idaddress
inner join street s with (nolock)  on s.idstreet=a.idstreet
inner join house hs  with (nolock) on hs.idhouse=a.idhouse
inner join gobject go with (nolock) on ct.idcontract=go.idcontract
left join gmeter gm with (nolock) on gm.idgobject=go.idgobject
	and gm.idstatusgmeter=1
left join typegmeter tgm with (nolock) on tgm.idtypegmeter=gm.idtypegmeter
left join plita pl with (nolock) on pl.idgobject=go.idgobject
	and idstatusplita=1
left join typeplita tpl with (nolock) on tpl.idtypeplita=pl.idtypeplita
left join schemaconnect sc with (nolock) on sc.idschemaconnect=pl.idschemaconnect
where sc.path is not null
order by ct.Account





GO