CREATE PROCEDURE dbo.spUnloadingInTextFile (@IDSending int, @idcorrespondent int) AS
--declare @idcorrespondent int
declare @patch varchar(50)
--set @idcorrespondent=1

declare @FileName  varchar(255) 
declare @Text1 varchar(8000)
declare @FS int 
declare @OLEResult int 
declare @FileID int 
declare @Fail varchar(100)
declare @DAtere varchar(20)
--declare @Hex nvarchar(255)
--declare @idsending int


declare @number int
declare @datesendig datetime

if @idcorrespondent=1
set @patch='D:\НАЦЭКСнаотправку\'
else
set @patch='D:\Ком.Телнаотправку\'
if @IDSending<>0
begin
set @number=(select numberSending from sending where idsending=@idsending)
set @datesendig=(select DateSending from sending where idsending=@idsending)
end
-----------сохраняем в текстовый файл
if @IDSending=0
begin
set @idsending=(select top 1 idsending from sending where idcorrespondent=3 order by idsending desc, datesending desc)
exec dbo.spCleaningChanges @idsending
insert into sending (DateSending, IDCorrespondent, NumberSending)
select dbo.dateonly(Getdate()),3, isnull(max(NumberSending),0)+1
from Sending where IDCorrespondent=3
set @number=(select numberSending from sending where idsending=@idsending)
set @datesendig=(select DateSending from sending where idsending=@idsending)
end

set @Fail=@Patch+'Горгаз №'+ltrim(str(@number))+'.txt'
set @Text1=left(convert(varchar,@datesendig,3),10)+'¶3¶'+ltrim(str(@number))
EXECUTE @OLEResult = sp_OACreate 'Scripting.FileSystemObject', @FS OUTPUT
execute @OLEResult = sp_OAMethod @FS,'CreateTextFile',@FileID OUTPUT, @Fail
execute @OLEResult = sp_OAMethod @FileID, 'writeline', NULL,@Text1
set @Text1='BEGIN TRANSACTION '
execute @OLEResult = sp_OAMethod @FileID, 'writeline', NULL,@Text1
declare @datese datetime

set @Text1=' declare @IDCorrespondent int  set @IDCorrespondent=(select IDCorrespondent from Settings) '
		execute @OLEResult = sp_OAMethod @FileID, 'writeline', NULL,@Text1

set @Text1=' insert into Sending (DateSending, IDCorrespondent, NumberSending) values( '''+ convert(varchar,dbo.dateonly(@datesendig),20)+''',3, '''+ltrim(str(@number))+''')'
		execute @OLEResult = sp_OAMethod @FileID, 'writeline', NULL,@Text1

set @Text1=' update Settings set IDCorrespondent=3'
		execute @OLEResult = sp_OAMethod @FileID, 'writeline', NULL,@Text1

declare @count int
set @count=0
DECLARE cur CURSOR
READ_ONLY
FOR select idtypeAction, Tablename, idobject from changes where idsending=@idsending order by idchanges

DECLARE @idtypeAction int
DECLARE @Tablename varchar (20)
declare @idobject int


OPEN cur

FETCH NEXT FROM cur INTO @idtypeAction, @Tablename, @idobject
WHILE (@@fetch_status <> -1)
BEGIN
	IF (@@fetch_status <> -2)
	BEGIN	

declare @idstatusgmeter int
declare @IDTypeGMeter int
declare @SerialNumber Varchar(50)
declare @BeginValue float
declare @DateInstall datetime
declare @DateVerify datetime
declare @Memo varchar (8000)
declare @IDGObject int
declare @DateFabrication datetime
declare @idcontract int 
declare @Correspondent1 int
declare @IDTypeAddress int
declare @IDPlace int
declare @IDStreet int
declare @IDHouse int
declare @Flat varchar(20)
declare @IDTypeAddres int
declare @NameTA varchar(50)
declare @memoTA varchar(100)
declare @NameP varchar (50)
declare @IDTypePlace int
declare @IDRegion int
declare @NameTP varchar(50)
declare @ShortName varchar(20)
declare @NameR varchar(50)
declare @NameS varchar(50)
declare @FullName varchar(50)
declare @IdtypeHouse int
declare @HouseNumber int
declare @HouseNumberChar varchar(20)
declare @isEvenSide int
declare @isComfortable int
declare @IDContry int
declare @NameC varchar(50)
declare @ClassAccuracy float
declare @CountDigital int
declare @ServiceLife int
declare @IDAddress int
declare @IDTypeStreet int
declare @IDtypeVerify int
declare @Sootn int
declare @Surname varchar(100)
declare @Patronic varchar(100)
declare @isJuridical int
declare @IDPerson int
		if (ltrim(@Tablename)='GMeter')
			begin
			if (@idtypeAction=1)
			begin
				set @Sootn=dbo.fGetConformity (2,@idcorrespondent,@idobject,0)
				if (@Sootn=0)
				begin
				set @idstatusgmeter=(select idstatusgmeter from gmeter where idgmeter=@idobject)
				set @IDTypeGMeter=(select IDTypeGMeter from gmeter where idgmeter=@idobject)
				set @SerialNumber=(select isnull(SerialNumber,'') from gmeter where idgmeter=@idobject)
				set @BeginValue=(select isnull(BeginValue,0) from gmeter where idgmeter=@idobject)
				set @DateInstall=(select isnull(DateInstall,'1900-01-01') from gmeter where idgmeter=@idobject)
				set @DateVerify=(select isnull(DateVerify,'1900-01-01') from gmeter where idgmeter=@idobject)
				set @Memo=(select isnull(Memo,'') from gmeter where idgmeter=@idobject)
				set @DateFabrication=(select isnull(DateFabrication,'1900-01-01') from gmeter where idgmeter=@idobject)
				set @idcontract=dbo.fGetIDContract (@idobject)
				set @IDtypeVerify=(select isnull(IDtypeVerify,0) from gmeter where idgmeter=@idobject)
										--select @BeginValue,ltrim(convert(varchar,@BeginValue))
				set @Text1='insert Gmeter (IDStatusGMeter, IDTypeGMeter, SerialNumber, BeginValue, DateInstall, DateVerify, Memo, DateFabrication, IDContract, IDConformity, IDtypeVerify) values ('+ltrim(str(@idstatusgmeter))+','+ltrim(str(@IDTypeGMeter))+','''+ltrim(@SerialNumber)+''','+ltrim(convert(varchar,@BeginValue))+','''+convert(varchar,@DateInstall,20)+''','''+convert(varchar,@DateVerify,20)+''','''+ @Memo+''','''+convert(varchar,@DateFabrication,20)+''','+ltrim(str(@idcontract))+','+ltrim(str(@idobject))+','+ltrim(str(@IDtypeVerify))+')'
				end

			end
			if (@idtypeAction=3)
			begin
			set @Correspondent1=(select Correspondent1 from ConformityGMeter where Correspondent3=@idobject)
			if @Correspondent1 is null
			set @Text1='delete Gmeter where idConformity='+ltrim(str(@idobject))
			else
			set @Text1='delete Gmeter where idgmeter='+ltrim(str(@Correspondent1))
			end
			if (@idtypeAction=2)
			begin
				set @idstatusgmeter=(select idstatusgmeter from gmeter where idgmeter=@idobject)
				set @IDTypeGMeter=(select IDTypeGMeter from gmeter where idgmeter=@idobject)
				set @SerialNumber=(select isnull(SerialNumber,'') from gmeter where idgmeter=@idobject)
				set @BeginValue=(select isnull(BeginValue,0) from gmeter where idgmeter=@idobject)
				set @DateInstall=(select isnull(DateInstall,'1900-01-01') from gmeter where idgmeter=@idobject)
				set @DateVerify=(select isnull(DateVerify,'1900-01-01') from gmeter where idgmeter=@idobject)
				set @Memo=(select isnull(Memo,'') from gmeter where idgmeter=@idobject)
				set @DateFabrication=(select isnull(DateFabrication,'1900-01-01') from gmeter where idgmeter=@idobject)
				set @idcontract=dbo.fGetIDContract (@idobject)
				set @IDtypeVerify=(select isnull(IDtypeVerify,0) from gmeter where idgmeter=@idobject)
				set @Correspondent1=(select Correspondent1 from ConformityGMeter where Correspondent3=@idobject)
				if @Correspondent1 is null
				set @Text1='update gmeter set idstatusgmeter='+ltrim(str(@idstatusgmeter))+', IDTypeGMeter='+ltrim(str(@IDTypeGMeter))+', SerialNumber='''+ltrim(@SerialNumber)+''', BeginValue='+ltrim(convert(varchar,@BeginValue))+', DateInstall='''+convert(varchar,@DateInstall,20)+''', DateVerify='''+convert(varchar,@DateVerify,20)+''', Memo='''+ @Memo+''', DateFabrication='''+convert(varchar,@DateFabrication,20)+''', idcontract='+ltrim(str(@idcontract))+', IDConformity='+ltrim(str(@idobject))+', IDtypeVerify='+ltrim(str(@IDtypeVerify))+' where idConformity='+ltrim(str(@idobject))
				else
				set @Text1='update gmeter set idstatusgmeter='+ltrim(str(@idstatusgmeter))+', IDTypeGMeter='+ltrim(str(@IDTypeGMeter))+', SerialNumber='''+ltrim(@SerialNumber)+''', BeginValue='+ltrim(convert(varchar,@BeginValue))+', DateInstall='''+convert(varchar,@DateInstall,20)+''', DateVerify='''+convert(varchar,@DateVerify,20)+''', Memo='''+ @Memo+''', DateFabrication='''+convert(varchar,@DateFabrication,20)+''', idcontract='+ltrim(str(@idcontract))+', IDConformity='+ltrim(str(@idobject))+', IDtypeVerify='+ltrim(str(@IDtypeVerify))+' where idgmeter='+ltrim(str(@Correspondent1))

			end
			end
			if (ltrim(@Tablename)='TypeGMeter')
			begin
			if (@idtypeAction=1)
			begin
				set @Sootn=dbo.fGetConformity (3,@idcorrespondent,@idobject,0)
				if (@Sootn=0)
				begin
				set @NameS=(select Name from TypeGMeter where IDTypeGMeter=@idobject)
				set @memo=(select isnull(Memo,'') from TypeGMeter where IDTypeGMeter=@idobject)
				set @ClassAccuracy=(select ClassAccuracy from TypeGMeter where IDTypeGMeter=@idobject)
				set @CountDigital=(select CountDigital from TypeGMeter where IDTypeGMeter=@idobject)
				set @ServiceLife=(select isnull(ServiceLife,'') from TypeGMeter where IDTypeGMeter=@idobject)
				set @Text1='insert TypeGMeter (IDTypeGMeter, Name, Memo, ClassAccuracy, CountDigital, ServiceLife, IDConformity) values ('+ltrim(str(@idobject))+','''+@NameS+''','''+@Memo+''','+''+ltrim(str(@ClassAccuracy))+''+','+ltrim(str(@CountDigital))+','+ltrim(str(@ServiceLife))+' ,' + ltrim(str(@idobject))+')'
				end
			end
			if (@idtypeAction=3)
			begin
			set @Correspondent1=(select Correspondent1 from ConformityTypeGMeter where Correspondent3=@idobject)
			if @Correspondent1 is null
			set @Text1='delete TypeGMeter where idConformity='+ltrim(str(@idobject))
			else
			set @Text1='delete TypeGMeter where idTypeGMeter='+ltrim(str(@Correspondent1))
			end
			if (@idtypeAction=2)
			begin
				set @NameS=(select Name from TypeGMeter where IDTypeGMeter=@idobject)
				set @memo=(select isnull(Memo,'') from TypeGMeter where IDTypeGMeter=@idobject)
				set @ClassAccuracy=(select ClassAccuracy from TypeGMeter where IDTypeGMeter=@idobject)
				set @CountDigital=(select CountDigital from TypeGMeter where IDTypeGMeter=@idobject)
				set @ServiceLife=(select ServiceLife from TypeGMeter where IDTypeGMeter=@idobject)
				set @Correspondent1=(select Correspondent1 from ConformityTypeGMeter where Correspondent3=@idobject)
				if @Correspondent1 is null
				set @Text1='update TypeGMeter set Name='''+ltrim(@NameS)+''', Memo='''+ @memo+''', ClassAccuracy='+ltrim(str(@ClassAccuracy))+' , CountDigital='+ltrim(str(@CountDigital))+' , ServiceLife='+ltrim(str(@ServiceLife))+' , IDConformity='+ltrim(str(@idobject))+' where IDConformity='+ltrim(str(@idobject))
				else
				set @Text1='update TypeGMeter set Name='''+ltrim(@NameS)+''', Memo='''+ @memo+''', ClassAccuracy='+ltrim(str(@ClassAccuracy))+' , CountDigital='+ltrim(str(@CountDigital))+' , ServiceLife='+ltrim(str(@ServiceLife))+' , IDConformity='+ltrim(str(@idobject))+' where IDTypeGMeter='+ltrim(str(@Correspondent1))

			end
			end
		if (ltrim(@Tablename)='Address')
			begin
			if (@idtypeAction=1)
			begin
				set @IDTypeAddress=(select IDTypeAddress from Address where idAddress=@idobject)
				set @IDPlace=(select IDPlace from Address where idAddress=@idobject)
				set @IDStreet=(select IDStreet from Address where idAddress=@idobject)
				set @IDHouse=(select IDHouse from Address where idAddress=@idobject)
				set @Flat=(select Flat from Address where idAddress=@idobject)
				set @Text1='insert Address (IDAddress, IDTypeAddress, IDPlace, IDStreet, IDHouse, Flat) values ('+ltrim(str(@idobject))+','+ltrim(str(@IDTypeAddress))+','+ltrim(str(@IDPlace))+','+ltrim(str(@IDStreet))+','+ltrim(str(@IDHouse))+','''+@Flat+''')'
			end
			if (@idtypeAction=3)
			begin
			set @Text1='delete Address where idAddress='+ltrim(str(@idobject))
			end
			if (@idtypeAction=2)
			begin
				set @IDTypeAddress=(select IDTypeAddress from Address where idAddress=@idobject)
				set @IDPlace=(select IDPlace from Address where idAddress=@idobject)
				set @IDStreet=(select IDStreet from Address where idAddress=@idobject)
				set @IDHouse=(select IDHouse from Address where idAddress=@idobject)
				set @Flat=(select Flat from Address where idAddress=@idobject)
				set @Text1='update Address set IDTypeAddress='+ltrim(str(@IDTypeAddress))+', IDPlace='+ltrim(str(@IDPlace))+', IDStreet='+ltrim(str(@IDStreet))+', IDHouse='+ltrim(str(@IDHouse))+', Flat='''+@Flat+''' where idAddress='+ltrim(str(@idobject))
			end
			end
			if (ltrim(@Tablename)='IDTypeAddress')
			begin
			if (@idtypeAction=1)
			begin
				set @NameTA=(select isnull(name,'') from TypeAddress where idTypeAddress=@idobject)
				set @memoTA=(select isnull(memo,'') from TypeAddress where idTypeAddress=@idobject)
				set @Text1='insert TypeAddress (TypeAddress, Name, Memo) values ('+ltrim(str(@idobject))+','''+@NameTA+''''+','''+@memoTA+''')'
			end
			if (@idtypeAction=3)
			begin
			set @Text1='delete TypeAddress where idTypeAddress='+ltrim(str(@idobject))
			end
			if (@idtypeAction=2)
			begin
				set @NameTA=(select isnull(name,'') from TypeAddress where idTypeAddress=@idobject)
				set @memoTA=(select isnull(memo,'') from TypeAddress where idTypeAddress=@idobject)
				set @Text1='update TypeAddress set Name='''+str(@NameTA)+''', Memo='+str(@memo)+' where idTypeAddress='+ltrim(str(@idobject))
			end
			end
			if (ltrim(@Tablename)='Place')
			begin
			if (@idtypeAction=1)
			begin
				set @NameP=(select isnull(Name,'') from Place where idPlace=@idobject)
				set @IDTypePlace=(select IDTypePlace from Place where idPlace=@idobject)
				set @IDRegion=(select IDRegion from Place where idPlace=@idobject)
				set @Text1='insert Place (IDPlace, Name, IDTypePlace,IDRegion) values ('+ltrim(str(@idobject))+','''+@NameP+''''+','+ltrim(str(@IDTypePlace))+' ,'+ltrim(str(@IDRegion))+')'
			end
			if (@idtypeAction=3)
			begin
			set @Text1='delete Place where idPlace='+ltrim(str(@idobject))
			end
			if (@idtypeAction=2)
			begin
				set @NameP=(select isnull(Name,'') from Place where idPlace=@idobject)
				set @IDTypePlace=(select IDTypePlace from Place where idPlace=@idobject)
				set @IDRegion=(select IDRegion from Place where idPlace=@idobject)
				set @Text1='update Place set Name='+@NameP+', IDTypePlace='+ltrim(str(@IDTypePlace))+', IDRegion='+ltrim(str(@IDRegion))+' where idPlace='+ltrim(str(@idobject))
			end
			end

			if (ltrim(@Tablename)='TypePlace')
			begin
			if (@idtypeAction=1)
			begin
				set @NameTP=(select isnull(Name,'') from TypePlace where IDTypePlace=@idobject)
				set @ShortName=(select isnull(ShortName,'') from TypePlace where IDTypePlace=@idobject)
				set @Text1='insert TypePlace (IDTypePlace, Name, ShortName) values ('+ltrim(str(@idobject))+','''+@NameTP+''''+','''+str(@ShortName)+''')'
			end
			if (@idtypeAction=3)
			begin
			set @Text1='delete TypePlace where idTypePlace='+ltrim(str(@idobject))
			end
			if (@idtypeAction=2)
			begin
				set @NameTP=(select isnull(Name,'') from TypePlace where IDTypePlace=@idobject)
				set @ShortName=(select isnull(ShortName,'') from TypePlace where IDTypePlace=@idobject)
				set @Text1='update TypePlace set Name='+@NameTP+', ShortName='+str(@ShortName)+' where idTypePlace='+ltrim(str(@idobject))
			end
			end
			if (ltrim(@Tablename)='Region')
			begin
			if (@idtypeAction=1)
			begin
				set @NameR=(select isnull(Name,'') from Region where IDRegion=@idobject)
				set @IDContry=(select IDCountry from Region where IDRegion=@idobject)
				set @Text1='insert Region (IDRegion, Name, IDContry) values ('+ltrim(str(@idobject))+','''+@NameR+''''+','+Ltrim(str(@IDContry))+')'
			end
			if (@idtypeAction=3)
			begin
			set @Text1='delete Region where idRegion='+ltrim(str(@idobject))
			end
			if (@idtypeAction=2)
			begin
				set @NameTP=(select isnull(Name,'') from TypePlace where IDTypePlace=@idobject)
				set @ShortName=(select isnull(ShortName,'') from TypePlace where IDTypePlace=@idobject)
				set @Text1='update Region set Name='''+@NameR+''', IDContry='+ltrim(str(@IDContry))+' where idRegion='+ltrim(str(@idobject))
			end
			end
			if (ltrim(@Tablename)='Country')
			begin
			if (@idtypeAction=1)
			begin
				set @NameC=(select Name from Country where IDCountry=@idobject)
				set @Text1='insert Country (IDCountry, Name) values ('+ltrim(str(@idobject))+','''+@NameC+''''+')'
			end
			if (@idtypeAction=3)
			begin
			set @Text1='delete Country where idCountry='+ltrim(str(@idobject))
			end
			if (@idtypeAction=2)
			begin
				set @NameTP=(select isnull(Name,'') from TypePlace where IDTypePlace=@idobject)
				set @ShortName=(select isnull(ShortName,'') from TypePlace where IDTypePlace=@idobject)
				set @Text1='update Country set Name='+@NameR+' where idCountry='+ltrim(str(@idobject))
			end
			end
			if (ltrim(@Tablename)='Street')
			begin
			if (@idtypeAction=1)
			begin
				set @NameS=(select isnull(Name,'') from Street where IDStreet=@idobject)
				set @IDPlace=(select IDPlace from Street where IDStreet=@idobject)
				set @IDTypeStreet=(select IDTypeStreet from Street where IDStreet=@idobject)
				set @Text1='insert Street (IDStreet,Name,IDPlace,IDTypeSteet) values ('+ltrim(str(@idobject))+','''+@NameS+''','+ltrim(str(@IDPlace))+','+@IDTypeStreet+')'
			end
			if (@idtypeAction=3)
			begin
			set @Text1='delete Street where idStreet='+ltrim(str(@idobject))
			end
			if (@idtypeAction=2)
			begin
				set @NameS=(select isnull(Name,'') from Street where IDStreet=@idobject)
				set @IDPlace=(select IDPlace from Street where IDStreet=@idobject)
				set @IDTypeStreet=(select IDTypeStreet from Street where IDStreet=@idobject)
				set @Text1='update Street set Name='''+@NameS+''', IDPlace='+ltrim(str(@IDPlace))+', IDTypeStreet='+ltrim(str(@IDTypeStreet))+' where IDStreet='+ltrim(str(@idobject))
			end
			end
			if (ltrim(@Tablename)='TypeStreet')
			begin
			if (@idtypeAction=1)
			begin
				set @FullName=(select isnull(FullName,'') from TypeStreet where IDTypeStreet=@idobject)
				set @ShortName=(select isnull(ShortName,'') from TypeStreet where IDTypeStreet=@idobject)
				set @Text1='insert TypeStreet (IDTypeStreet,FullName,ShortName) values ('+ltrim(str(@idobject))+','''+@FullName+''','''+@ShortName+''')'
			end
			if (@idtypeAction=3)
			begin
			set @Text1='delete TypeStreet where idTypeStreet='+ltrim(str(@idobject))
			end
			if (@idtypeAction=2)
			begin
				set @FullName=(select isnull(FullName,'') from TypeStreet where IDTypeStreet=@idobject)
				set @ShortName=(select isnull(ShortName,'') from TypeStreet where IDTypeStreet=@idobject)
				set @Text1='update TypeStreet set FullName='+@FullName+', ShortName='+@ShortName+' where IDTypeStreet='+ltrim(str(@idobject))
			end
			end
		if (ltrim(@Tablename)='House')
			begin
			if (@idtypeAction=1)
			begin
				set @IDStreet=(select IDStreet from House where IDHouse=@idobject)
				set @IDTypeHouse=(select IDTypeHouse from House where IDHouse=@idobject)
				set @HouseNumber=(select HouseNumber from House where IDHouse=@idobject)
				set @HouseNumberChar=(select isnull(HouseNumberChar,'') from House where IDHouse=@idobject)
				set @IsEvenSide=(select IsEvenSide from House where IDHouse=@idobject)
				set @IsComfortable=(select IsComfortable from House where IDHouse=@idobject)
				set @Text1='insert House (IDHouse, IDStreet, IDTypeHouse, HouseNumber, HouseNumberChar, IsEvenSide, IsComfortable) values ('+ltrim(str(@idobject))+','+ltrim(str(@IDStreet))+','+ltrim(str(@IDTypeHouse))+','+ltrim(str(@HouseNumber))+','''+@HouseNumberChar+''','+ltrim(str(@IsEvenSide))+','+ltrim(str(@IsComfortable))+')'
			end
			if (@idtypeAction=3)
			begin
			set @Text1='delete House where idHouse='+ltrim(str(@idobject))
			end
			if (@idtypeAction=2)
			begin
				set @IDStreet=(select IDStreet from House where IDHouse=@idobject)
				set @IDTypeHouse=(select IDTypeHouse from House where IDHouse=@idobject)
				set @HouseNumber=(select HouseNumber from House where IDHouse=@idobject)
				set @HouseNumberChar=(select replace(replace(isnull(HouseNumberChar,''),'(',''),')','') from House where IDHouse=@idobject)
				set @IsEvenSide=(select IsEvenSide from House where IDHouse=@idobject)
				set @IsComfortable=(select IsComfortable from House where IDHouse=@idobject)
				set @Text1='update House set IDStreet='+ltrim(str(@IDStreet))+', IDTypeHouse='+ltrim(str(@IDTypeHouse))+', HouseNumber='+ltrim(str(@HouseNumber))+', HouseNumberChar='+ltrim(str(@HouseNumberChar))+', IsEvenSide='+ltrim(str(@IsEvenSide))+', IsComfortable='+ltrim(str(@IsComfortable))+' where IDHouse='+ltrim(str(@idobject))
			end
			end
			if (ltrim(@Tablename)='TypeHouse')
			begin
			if (@idtypeAction=1)
			begin
				set @NameS=(select isnull(Name,'') from TypeHouse where IDTypeHouse=@idobject)
				set @Memo=(select isnull(MEmo,'') from TypeHouse where IDTypeHouse=@idobject)
				set @Text1='insert TypeHouse (IDTypeHouse,Name,Memo) values ('+ltrim(str(@idobject))+','''+@NameS+''','''+@Memo+''')'
			end
			if (@idtypeAction=3)
			begin
			set @Text1='delete TypeHouse where idTypeHouse='+ltrim(str(@idobject))
			end
			if (@idtypeAction=2)
			begin
			set @NameS=(select isnull(Name,'') from TypeHouse where IDTypeHouse=@idobject)
				set @Memo=(select isnull(MEmo,'') from TypeHouse where IDTypeHouse=@idobject)
				set @Text1='update TypeHouse set Name='+@NameS+', Memo='+@Memo+' where IDTypeHouse='+ltrim(str(@idobject))
			end
			end
			if (@Tablename='Person')
			begin
			if (@idtypeAction=2)
			begin
				set @memo=(select ltrim(isnull(Name,'')) from person with (nolock) where IDPerson=@idobject)
				set @Surname=(select isnull(Surname,'') from person with (nolock) where IDPerson=@idobject)
				set @Patronic=(select  ltrim(isnull(Patronic,'')) from person with (nolock) where IDPerson=@idobject)
				set @idcontract=(select idcontract from person p  with (nolock) inner join contract c on c.idperson=p.idperson where p.IDPerson=@idobject)
				set @isJuridical=(select  isnull(isJuridical,0) from person with (nolock) where IDPerson=@idobject)
				if (@isJuridical=0)
				set @Text1='update Contract set name='''+@memo +''', Surname='''+@Surname+''', Patronic='''+ @Patronic+''' where IDContract='+ltrim(str(@idcontract))
				else
				set @Text1='update Contract set Surname='''+@Surname+''' where IDContract='+ltrim(str(@idcontract))
			end
			end
			if (@Tablename='GObject')
			begin
			if (@idtypeAction=2)
			begin
				set @IDAddress=(select idaddress from gobject with (nolock) where IDgobject=@idobject)
				set @idcontract=(select idcontract from gobject with (nolock) where IDgobject=@idobject)
				set @Text1='update Contract set idaddress='+ltrim(str(@IDAddress)) +' where IDContract='+ltrim(str(@idcontract))
			end
			end

			if (ltrim(@Tablename)='Contract')
			begin
			if (@idtypeAction=1)
			begin
				set @IDContract=(select IDContract from Contract where IDContract=@idobject)
				set @nameS=(select Account from Contract where IDContract=@idobject)
				set @IDAddress=(select IDAddress from Contract c inner join gobject g on c.idcontract=g.idcontract where c.IDContract=@idobject)
				set @Surname=(select isnull(Surname,'') from Contract c inner join person np with (nolock) on c.idperson=np.idperson where IDContract=@idobject)
				set @Patronic=(select  ltrim(isnull(Patronic,''))from Contract c inner join person np with (nolock) on c.idperson=np.idperson where IDContract=@idobject)
				set @memo=(select ltrim(isnull(np.Name,''))  from Contract c inner join person np with (nolock) on c.idperson=np.idperson where IDContract=@idobject)
				set @IDPerson=(select IDPerson from Contract where IDContract=@idobject)
				set @Text1='insert Contract (IDContract, Account, IDAddress, Surname,Patronic,Name,IDPerson) values ('+ltrim(str(@IDContract))+', '+ltrim(str(@nameS))+', '+ltrim(str(@IDAddress))+' ,'''+@Surname+''','''+@Patronic+''','''+@memo+''','+ltrim(str(@IDPerson))+')'
			end
			if (@idtypeAction=3)
			begin
			set @Text1='delete Contract where idContract='+ltrim(str(@idobject))
			end
			if (@idtypeAction=2)
			begin
				set @nameS=(select Account from Contract where IDContract=@idobject)
				set @IDAddress=(select IDAddress from Contract c inner join gobject g on c.idcontract=g.idcontract where c.IDContract=@idobject)
				set @Surname=(select isnull(Surname,'') from Contract c inner join person np with (nolock) on c.idperson=np.idperson where IDContract=@idobject)
				set @Patronic=(select  ltrim(isnull(Patronic,''))from Contract c inner join person np with (nolock) on c.idperson=np.idperson where IDContract=@idobject)
				set @memo=(select ltrim(isnull(np.Name,''))  from Contract c inner join person np with (nolock) on c.idperson=np.idperson where IDContract=@idobject)
				set @IDPerson=(select IDPerson from Contract where IDContract=@idobject)
				set @Text1='update Contract set Account='+ltrim(str(@NameS))+' , IDAddress='+ ltrim(str(@IDAddress))+' , name='''+@memo +''', Surname='''+@Surname+''', Patronic='''+@Patronic+''', IDPerson='+ltrim(str(@IDPerson))+' where IDContract='+ltrim(str(@idobject))
			end
			end


		if (ltrim(@Text1)<>'')
begin
		execute @OLEResult = sp_OAMethod @FileID, 'writeline', NULL,@Text1

		set @Text1='if @@error<>0 begin ROLLBACK TRANSACTION end '
		execute @OLEResult = sp_OAMethod @FileID, 'writeline', NULL,@Text1
end
set @Text1=''
		
	END
	FETCH NEXT FROM cur INTO @idtypeAction, @Tablename, @idobject
END

CLOSE cur
DEALLOCATE cur

set @Text1=' update Settings set IDCorrespondent=@IDCorrespondent '
		execute @OLEResult = sp_OAMethod @FileID, 'writeline', NULL,@Text1

set @Text1=' COMMIT TRANSACTION '
		execute @OLEResult = sp_OAMethod @FileID, 'writeline', NULL,@Text1

EXECUTE @OLEResult = sp_OADestroy @FileID
EXECUTE @OLEResult = sp_OADestroy @FS