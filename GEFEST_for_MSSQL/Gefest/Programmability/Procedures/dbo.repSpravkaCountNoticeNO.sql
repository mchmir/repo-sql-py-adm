SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO


create procedure [dbo].[repSpravkaCountNoticeNO]  as 


select c.Account, c.IdContract, 
np.Surname+' '+left(ltrim(isnull(np.name,'')),1)+'.' +left(ltrim(isnull(np.Patronic,'')),1)  FIO,  
s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat) Adres, aa.name

from contract c with (nolock) 
inner join GObject g  with (nolock) on g.IdContract=c.IdContract
	and isnull(c.PrintChetIzvehen,0)=1
inner join address a  with (nolock) on a.idaddress=g.idaddress
inner join street s with (nolock)  on s.idstreet=a.idstreet
inner join house hs  with (nolock) on hs.idhouse=a.idhouse
inner join gru h with (nolock)  on h.idgru=g.idgru
inner join agent aa with (nolock)  on aa.idagent=h.idagent
inner join person np with (nolock)  on np.idperson=c.Idperson
GO