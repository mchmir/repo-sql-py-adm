SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO




create function [dbo].[fnEPAYGetConsamersInfo] ()
RETURNS 
@MyTable table (fio varchar (50), adress varchar (50), bal float)
AS
BEGIN
insert into @MyTable (fio, adress, bal)
select (p.Surname +' ' +p.Name+' '+p.Patronic) fio,('ул. '+s.Name+' дом '+(CAST(h.HouseNumber AS varchar(50)))+' '+h.HouseNumberChar+' кв.'+a.Flat) adress,round(b.amountbalance,0)
from dbo.Contract cn
inner join dbo.Person p on p.IDPerson=cn.IDPerson
inner join dbo.Address a on a.IDAddress=p.IDAddress
inner join dbo.vwBalance b on b.IDContract=cn.IDContract
left join dbo.Street s on s.IDStreet=a.IDStreet
left join dbo.House h on h.IDHouse=a.IDHouse
--where cn.Account=@Account
return
END



GO