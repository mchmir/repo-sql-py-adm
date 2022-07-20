SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO









CREATE PROCEDURE [dbo].[repCashRep] (@IDCashier int, @DateB datetime) AS


--declare @IDCashier int, @DateB datetime
--set @IDCashier=73
--set @DateB='2013-02-06'

if @IDCashier=125 --terminal
	begin
		exec dbo.repCashRepTerminal @IDCashier, @DateB
	end
else
	begin
		exec dbo.repCashRepGG @IDCashier, @DateB
	end
GO