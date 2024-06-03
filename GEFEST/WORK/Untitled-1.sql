SELECT * 
  FROM Document AS d
 WHERE d.IDTypeDocument = 22
   AND d.IDPeriod = dbo.fGetIDPeriodMY(9,2022)


SELECT *
  FROM Document AS d
 WHERE d.IDContract in (select idContract 
                          from Contract as c 
                         where c.Account in (0631050, 0631045, 0631063))
   AND d.IDTypeDocument = 22;


-- IDDOCUMENT in (22154395,22154396)

SELECT *
      FROM PD AS pd
      WHERE pd.IDDocument in (22272920, 22272704, 22272956)


SELECT 
*
  FROM GMeter AS g
 WHERE g.IDGMeter in (
                SELECT value
                FROM PD AS pd
                WHERE pd.IDDocument in (22272920, 22272704, 22272956)
                  AND IDTypePD = 7
                )

select c.ACCOUNT, f.*
from indication as i
join GMETER as G on g.IDGMETER=i.IDGMETER
join GOBJECT as GOB on G.IDGOBJECT = GOB.IDGOBJECT
join CONTRACT C on GOB.IDCONTRACT = C.IDCONTRACT
join FACTUSE as F on GOB.IDGOBJECT = F.IDGOBJECT and i.IDINDICATION = f.IDINDICATION
where DATEDISPLAY='2024-06-01' and i.IDUSER=37


select *
from CONTRACT as C
where ACCOUNT='1182001';

-- 922870
Select *
from BALANCE
where IDCONTRACT = 922870;

select *
from BALANCEREAL
where IDCONTRACT = 922870;




