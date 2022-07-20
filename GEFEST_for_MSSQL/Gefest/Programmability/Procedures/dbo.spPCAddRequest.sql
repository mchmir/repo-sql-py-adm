SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO








CREATE PROCEDURE [dbo].[spPCAddRequest] (@account varchar(50),@FIO varchar(70),@phone varchar(50),@adres varchar(100),@email varchar(50))
AS
BEGIN
	SET NOCOUNT ON;
	declare @state int
	select  @state=State from dbo.PersonalCabinetRequest where Account=@account
	
	if(isnull(@state,4)=4)
	begin
			INSERT INTO [Gefest].[dbo].[PersonalCabinetRequest]
					   ([Account]
					   ,[FIO]
					   ,[Phone]
					   ,[AdresHome]
					   ,[Email])
				 VALUES
					   (@account
					   ,@FIO
					   ,@phone
					   ,@adres
					   ,@email)
					   select 'YES'
           end
           else
           begin
			   select 'NO'
           end
END







GO