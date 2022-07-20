CREATE TABLE [dbo].[PersonalCabinetFAQ] (
  [IDPersonalCabinetFAQ] [int] IDENTITY,
  [FAQTitle] [nvarchar](100) NOT NULL,
  [FAQBody] [nvarchar](400) NOT NULL,
  [FAQLng] [nvarchar](50) NOT NULL,
  CONSTRAINT [PK_PersonalCabinetFAQ] PRIMARY KEY CLUSTERED ([IDPersonalCabinetFAQ])
)
ON [PRIMARY]
GO