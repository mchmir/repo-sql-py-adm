SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO









CREATE PROCEDURE [dbo].[spPCSaveComment] (@Account varchar(20),@MessageTitle varchar(20),@MessageBody varchar(200))
AS
BEGIN
	SET NOCOUNT ON;
INSERT INTO [Gefest].[dbo].[PersonalCabinetMessage]
           ([MessageTitle]
           ,[MessageBody]
           ,[Account]
           ,[MessageType])
     VALUES
           (@MessageTitle
           ,@MessageBody
           ,@Account
           ,1)
	select 'YES'

END








GO