SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

CREATE PROCEDURE [dbo].[repBatch](@idBatch as int) AS
select b.batchdate, b.batchamount, b.batchcount, d.documentamount, p.surname, p.name, p.patronic, a1.name cashier, a2.name dispatcher, c.account
from batch b  with (nolock) 
left join document d  with (nolock) on d.idbatch=b.idbatch
left join contract c with (nolock)  on c.idcontract=d.idcontract
left join person p with (nolock)  on p.idperson=c.idperson
left join agent a1 with (nolock)  on a1.idagent=b.idcashier
left join agent a2  with (nolock) on a2.idagent=b.iddispatcher
where b.idbatch=@idBatch order by d.iddocument asc
GO