SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO








CREATE PROCEDURE [dbo].[spPCChangePWD] (@Account varchar(20),@pwd varchar(50))
AS
BEGIN
	SET NOCOUNT ON;

UPDATE [Gefest].[dbo].[PersonalCabinet]
   SET [PCPassword] = @pwd,
   PCneedchgPWD=1
 WHERE Account=@Account
 
 declare @IDPersonalCabinet int
 set @IDPersonalCabinet=0
	select @IDPersonalCabinet=IDPersonalCabinet from dbo.PersonalCabinet where Account=@Account
	if @IDPersonalCabinet>0
	begin
 	INSERT INTO [Gefest].[dbo].[PersonalCabinetLog]
           ([IDPersonalCabinet],[note])
     VALUES
           (@IDPersonalCabinet,'ChangePWD')
	end

END







GO