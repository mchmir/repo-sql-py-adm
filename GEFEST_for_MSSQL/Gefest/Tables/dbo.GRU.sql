CREATE TABLE [dbo].[GRU] (
  [IDGru] [int] IDENTITY,
  [IDAddress] [int] NULL,
  [IDClassGRU] [int] NULL,
  [Name] [varchar](50) NULL,
  [Memo] [varchar](100) NULL,
  [InvNumber] [varchar](50) NULL,
  [DateIn] [datetime] NULL,
  [DateVerify] [datetime] NULL,
  [IdAgent] [int] NULL,
  [Note] [varchar](100) NULL,
  CONSTRAINT [PK__GRU__6477ECF3] PRIMARY KEY CLUSTERED ([IDGru])
)
ON [PRIMARY]
GO

CREATE INDEX [IX_GRU]
  ON [dbo].[GRU] ([IDGru])
  ON [PRIMARY]
GO

SET QUOTED_IDENTIFIER OFF

SET ANSI_NULLS ON
GO
CREATE TRIGGER [old_value4] ON [dbo].[GRU]
FOR UPDATE
AS

declare @idobject as int
declare @value as int
declare @nvalue as int

select @value=idagent,@idobject=idgru from deleted
select @nvalue=idagent,@idobject=idgru from inserted
--будем записывать в oldvalues IDAgent
if (@nvalue<>@value)
insert into oldvalues(nametable,namecolumn,idobject,idperiod,datevalues,name)
values('GRU','IdAgent',@idobject,dbo.fGetNowPeriod(),dbo.DateOnly(GetDate()),@value)

GO

ALTER TABLE [dbo].[GRU] WITH NOCHECK
  ADD CONSTRAINT [FK__GRU__IDAddress__2180FB33] FOREIGN KEY ([IDAddress]) REFERENCES [dbo].[Address] ([IDAddress])
GO

ALTER TABLE [dbo].[GRU] WITH NOCHECK
  ADD CONSTRAINT [FK__GRU__IDClassGRU__22751F6C] FOREIGN KEY ([IDClassGRU]) REFERENCES [dbo].[ClassGRU] ([IDClassGRU])
GO

ALTER TABLE [dbo].[GRU] WITH NOCHECK
  ADD CONSTRAINT [FK_GRU_Agent] FOREIGN KEY ([IdAgent]) REFERENCES [dbo].[Agent] ([IDAgent])
GO