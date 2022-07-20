SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO






CREATE PROCEDURE [dbo].[spEPAYGetConsamerInfo] (@Account varchar(20))
AS
BEGIN
	SET NOCOUNT ON;
select isnull(p.Surname,'')+' '+isnull(p.name,'')+' '+isnull(p.Patronic,'') fio,
'ул. '+s.name+ ', д. '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ ', кв. '+ltrim(a.flat) adress, round(isnull(b.amountbalance,0),0)
from dbo.Contract cn
inner join dbo.Person p on p.IDPerson=cn.IDPerson
inner join dbo.Address a on a.IDAddress=p.IDAddress
left join dbo.vwBalance b on b.IDContract=cn.IDContract
left join dbo.Street s on s.IDStreet=a.IDStreet
left join dbo.House hs on hs.IDHouse=a.IDHouse
where cn.Account=@Account

END





GO