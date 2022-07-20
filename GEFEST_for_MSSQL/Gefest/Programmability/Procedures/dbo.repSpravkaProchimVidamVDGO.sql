SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO



create PROCEDURE [dbo].[repSpravkaProchimVidamVDGO] (@idPeriod int) AS
---------------**************Справка по прочив видам деятельности*********-------------------
SET NOCOUNT ON
--declare @T table (Account varchar(20), FIO varchar (300), Address varchar(300), AmountB decimal(10,2))
--insert into @T (Account,FIO,Address,AmountB,)
select  ct.Account as LIC_ACC, isnull(p.Surname,'-')+' '+isnull(p.name,'-')+' '+isnull(p.Patronic,'-') as FIO ,
s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat) as ADDRESS,
convert(decimal(10,2),AmountBalance) AB
from balanceReal b
inner join Contract ct with (nolock) on b.IdContract=ct.IdContract
	and IdPeriod= @idPeriod and idaccounting=6
inner join person p (nolock) on ct.IDPerson=p.idperson
inner join address a (nolock) on a.idaddress=p.idaddress
inner join street s with (nolock)  on s.idstreet=a.idstreet
inner join house hs  with (nolock) on hs.idhouse=a.idhouse
where AmountBalance>0
GO