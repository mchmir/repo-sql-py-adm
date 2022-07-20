SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO








CREATE PROCEDURE [dbo].[spPCFAQ] (@lng varchar(20))
AS
BEGIN
	SET NOCOUNT ON;

select [FAQTitle],[FAQBody] from [PersonalCabinetFAQ] where [FAQLng]=@lng
END







GO