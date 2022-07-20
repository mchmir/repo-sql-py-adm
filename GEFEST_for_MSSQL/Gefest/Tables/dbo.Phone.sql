CREATE TABLE [dbo].[Phone] (
  [IDPhone] [int] IDENTITY,
  [IDPerson] [int] NULL,
  [NumberPhone] [varchar](100) NULL,
  CONSTRAINT [PK__Phone__6B24EA82] PRIMARY KEY CLUSTERED ([IDPhone])
)
ON [PRIMARY]
GO

CREATE INDEX [_dta_index_Phone_9_725577623__K2_3]
  ON [dbo].[Phone] ([IDPerson])
  INCLUDE ([NumberPhone])
  ON [PRIMARY]
GO

ALTER TABLE [dbo].[Phone] WITH NOCHECK
  ADD CONSTRAINT [FK__Phone__IDPerson__31B762FC] FOREIGN KEY ([IDPerson]) REFERENCES [dbo].[Person] ([IDPerson])
GO