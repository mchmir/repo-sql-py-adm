SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO









CREATE PROCEDURE [dbo].[spPCLostPWD] (@Account varchar(20),@Email varchar(50),@pwd varchar(50),@PWDch varchar(50))
AS
BEGIN
	SET NOCOUNT ON;
if((select IDPersonalCabinet from dbo.PersonalCabinet where pcemail=@Email and Account=@Account)>0)
begin
UPDATE [Gefest].[dbo].[PersonalCabinet]
   SET [PCPassword] = @PWDch,
	   PCneedchgPWD=0,
	   [PCPass] = @pwd
 WHERE Account=@Account
 
 declare @IDPersonalCabinet int
	select @IDPersonalCabinet=IDPersonalCabinet from dbo.PersonalCabinet where Account=@Account
 	INSERT INTO [Gefest].[dbo].[PersonalCabinetLog]
           ([IDPersonalCabinet],[note])
     VALUES
           (@IDPersonalCabinet,'LostPWD')
	select 'YES'
end
else
begin
select 'NO'
end
END








GO