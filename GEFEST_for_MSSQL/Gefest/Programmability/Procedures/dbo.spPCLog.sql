SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO









CREATE PROCEDURE [dbo].[spPCLog] (@idPC varchar(20),@LoginDate varchar(50),@note varchar(1000))
AS
BEGIN
	SET NOCOUNT ON;
INSERT INTO [Gefest].[dbo].[PersonalCabinetLog]
           ([IDPersonalCabinet]
           ,[DateLastLogin]
           ,[note])
     VALUES
           (@idPC
           ,@LoginDate
           ,@note)
END








GO