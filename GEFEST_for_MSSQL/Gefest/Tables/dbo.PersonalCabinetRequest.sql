CREATE TABLE [dbo].[PersonalCabinetRequest] (
  [IDPersonalCabinetRequest] [int] IDENTITY,
  [DateRequest] [datetime] NOT NULL CONSTRAINT [DF_PersonalCabinetRequest_DateRequest] DEFAULT (getdate()),
  [Account] [varchar](50) NOT NULL,
  [FIO] [varchar](70) NOT NULL,
  [Phone] [varchar](50) NULL,
  [AdresHome] [varchar](100) NOT NULL,
  [Email] [varchar](50) NOT NULL,
  [State] [int] NOT NULL CONSTRAINT [DF_PersonalCabinetRequest_State] DEFAULT (1),
  [NeedSendMessage] [int] NOT NULL CONSTRAINT [DF_PersonalCabinetRequest_NeedSendMessage] DEFAULT (1)
)
ON [PRIMARY]
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create TRIGGER [CommitPCRequest] 
   ON  [dbo].[PersonalCabinetRequest] 
   FOR UPDATE
AS 
declare @idPersonalCabinetRequest as int
declare @state as int
declare @email as varchar(50)
declare @account as varchar(50)

select @state=state,@account=account, @email=email, @idPersonalCabinetRequest=idPersonalCabinetRequest from inserted
--if isnull(@DatePlomb,'1800-01-01')<>'1800-01-01'
if @state in (2,3) 
begin
	update contract set PCEmail=@email where account=@account
end







GO