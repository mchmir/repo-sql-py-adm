SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO









CREATE PROCEDURE [dbo].[spPCGetConsamerFactUse] (@Account varchar(20))
AS
BEGIN
	SET NOCOUNT ON;

--SELECT TOP 20 Round([FactAmount],2)
--      ,p.[Year],p.[Month],p.IDPeriod
--  FROM [Gefest].[dbo].[FactUse] f
--  inner join dbo.GObject g on g.IDGObject=f.IDGObject
--  inner join dbo.Contract c on c.IDContract=g.IDContract
--  inner join dbo.Period p on p.IDPeriod=f.IDPeriod
--  where c.Account=@Account
--  order by f.IDPeriod desc

declare @idperiod int

select @idperiod = max([IDPeriod])  FROM [Gefest].[dbo].[Period] 
set @idperiod=@idperiod-15

declare @T table (fu float, y int, m int, idperiod int)
insert into @T (fu, y, m, idperiod)
SELECT  Round([FactAmount],2)
       ,p.[Year],p.[Month],p.IDPeriod
   FROM [Gefest].[dbo].[FactUse] f
   inner join dbo.GObject g on g.IDGObject=f.IDGObject
   inner join dbo.Contract c on c.IDContract=g.IDContract
   inner join dbo.Period p on p.IDPeriod=f.IDPeriod 
   where c.Account=@Account 
delete from @T where idperiod<@idperiod
select * from @T
END








GO