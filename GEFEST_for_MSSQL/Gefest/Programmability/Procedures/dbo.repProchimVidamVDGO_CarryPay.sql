SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO




















-- =============================================
-- Author:		<Sumlikin,Aleksandr>
-- Create date: <2010-10-13>
-- Description:	<Отчет по прочим видам деятельности ВДГО>
-- =============================================
CREATE PROCEDURE [dbo].[repProchimVidamVDGO_CarryPay](@idperiod int) AS

select c.account, sum(o.amountoperation)*-1 summa
from operation o  with (nolock) 
inner join document d  with (nolock) on o.iddocument=d.iddocument
and d.idperiod=@idperiod --and idtypedocument=3
inner join balance b with (nolock) on b.idbalance=o.idbalance
inner join contract c with (nolock) on b.idcontract=c.idcontract 
	and b.idperiod=@idperiod and idtypedocument<>7
	and b.idaccounting=6 and o.idtypeoperation=3
group by c.account
order by c.account

















GO