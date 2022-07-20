SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO







CREATE PROCEDURE [dbo].[spEPAYGetConsamersInfo] 
AS
BEGIN
	SET NOCOUNT ON;
select cn.idcontract, isnull(p.Surname,'')+' '+isnull(p.name,'')+' '+isnull(p.Patronic,'') fio,
'ул. '+s.name+ ', д. '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ ', кв. '+ltrim(a.flat) adress,
cn.account,round(isnull(b.amountbalance,0),0) balance
from dbo.Contract cn
inner join dbo.Person p on p.IDPerson=cn.IDPerson
inner join dbo.Address a on a.IDAddress=p.IDAddress
left join dbo.vwBalance b on b.IDContract=cn.IDContract
left join dbo.Street s on s.IDStreet=a.IDStreet
left join dbo.House hs on hs.IDHouse=a.IDHouse
order by cn.account
--where cn.Account=@Account
END






GO