CREATE TABLE [dbo].[PersonalCabinet] (
  [IDPersonalCabinet] [int] IDENTITY,
  [IDContract] [int] NULL,
  [Account] [varchar](50) NOT NULL,
  [PCPassword] [varchar](50) NOT NULL,
  [PCEmail] [varchar](50) NOT NULL,
  [PCPass] [varchar](50) NULL,
  [PCNeedChgPwd] [tinyint] NULL,
  [State] [int] NULL CONSTRAINT [DF_PersonalCabinet_State] DEFAULT (1),
  CONSTRAINT [PK_PersonalCabinet] PRIMARY KEY CLUSTERED ([IDPersonalCabinet])
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
CREATE TRIGGER [CommitPC] 
   ON  [dbo].[PersonalCabinet] 
   FOR INSERT, UPDATE
AS 
declare @PCPass as varchar(50)
declare @account as varchar(50)
declare @PCEmail as varchar(50)

select @PCPass=PCPass,@account=account, @PCEmail=PCEmail from inserted
--if isnull(@DatePlomb,'1800-01-01')<>'1800-01-01'
if len(@PCPass)>0 
begin
	update personalcabinetrequest set state=4, needsendmessage=1 where account=@account and state in (2,3)
end

update contract set pcemail=@PCEmail where account=@account








GO

ALTER TABLE [dbo].[PersonalCabinet]
  ADD CONSTRAINT [FK_PersonalCabinet_Contract] FOREIGN KEY ([IDContract]) REFERENCES [dbo].[Contract] ([IDContract])
GO