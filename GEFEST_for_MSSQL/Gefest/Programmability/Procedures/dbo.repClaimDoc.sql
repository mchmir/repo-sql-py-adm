SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE PROCEDURE [dbo].[repClaimDoc](@strIdDoc varchar (8000)) AS
declare @q as varchar (8000)
set @q='select account, case when p.isJuridical=1 then p.Surname else isnull(p.Surname,'''')+'' ''+isnull(p.Name,'''')+'' ''+isnull(p.Patronic,'''') end FIO,
isnull(s.Name,'''')+'' ''+isnull(ltrim(str(h.housenumber)),'''')+isnull(h.housenumberchar, '''')+''-''+isnull(a.flat,'''') address,
d.documentdate, d.documentamount, dateadd(d, 21, d.documentdate) dateto
from document d with (nolock) 
inner join contract c with (nolock) on c.idcontract=d.idcontract
	and d.iddocument in ('+@strIdDoc+')
inner join person p with (nolock) on p.idperson=c.idperson
left join address a with (nolock) on a.idaddress=p.idaddress 
left join house h with (nolock) on h.idhouse=a.idhouse 
left join street s with (nolock) on s.idstreet=h.idstreet '
exec (@q)
GO