SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

CREATE PROCEDURE [dbo].[repProvercaGP]  AS

select  dbo.fGetLastPlitaDateVerify(a.idhouse)fir, s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'') Adres,count (c.idcontract)cou
from contract c with (nolock) 
inner join GObject g  with (nolock) on g.IdContract=c.IdContract
	--and g.IdGRU=@IdGru 	and isnull(c.PrintChetIzvehen,0)=0
inner join address a  with (nolock) on a.idaddress=g.idaddress
inner join street s with (nolock)  on s.idstreet=a.idstreet
inner join house hs  with (nolock) on hs.idhouse=a.idhouse
--left join plita p with (nolock) on p.idgobject=g.idgobject
group by s.name, hs.housenumber,hs.housenumberchar,a.idhouse
order by Adres


GO