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