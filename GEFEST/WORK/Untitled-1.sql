
select count(c.idcontract) SumIn
where c.idstatusgobject=1 and c.idstatusgmeter=1

-- Всего с ПУ
select count(c.idcontract)
from contract c  with (nolock)
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and g.idgobject<>916602
left join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
 and gm.idstatusgmeter=1
where g.idstatusgobject=1 and gm.idstatusgmeter=1

-- Количество внесенных показаний
select count(idcontract)
from
 (select distinct g.idcontract
  from gobject g with (nolock)
  inner join gmeter gm with (nolock)  on gm.idgobject=g.idgobject
  inner join indication i  with (nolock)  on gm.idgmeter=i.idgmeter
  inner join factuse f with (nolock) on f.idindication=i.idindication
	and f.idperiod=233) qq


-- Всего с ПУ
select distinct CONTRACT.ACCOUNT
from CONTRACT
where IDCONTRACT
in (
       select C.IDCONTRACT
       from CONTRACT C with (nolock)
              inner join GOBJECT G with (nolock) on G.IDCONTRACT = C.IDCONTRACT
         and G.IDGOBJECT <> 916602
              left join GMETER GM with (nolock) on GM.IDGOBJECT = G.IDGOBJECT
         and GM.IDSTATUSGMETER = 1
       where G.IDSTATUSGOBJECT = 1
         and GM.IDSTATUSGMETER = 1

      except

       select IDCONTRACT
       from (select distinct G.IDCONTRACT
             from GOBJECT G with (nolock)
                    inner join GMETER GM with (nolock) on GM.IDGOBJECT = G.IDGOBJECT
                    inner join INDICATION I with (nolock) on GM.IDGMETER = I.IDGMETER
                    inner join FACTUSE F with (nolock) on F.IDINDICATION = I.IDINDICATION
               and F.IDPERIOD = 233) QQ
     )
;

 select C.IDCONTRACT
       from CONTRACT C with (nolock)
              inner join GOBJECT G with (nolock) on G.IDCONTRACT = C.IDCONTRACT
         and G.IDGOBJECT <> 916602
              left join GMETER GM with (nolock) on GM.IDGOBJECT = G.IDGOBJECT
         and GM.IDSTATUSGMETER = 1
       where G.IDSTATUSGOBJECT = 1
         and GM.IDSTATUSGMETER = 1 and C.ACCOUNT = 1311006
--863080
       select IDCONTRACT
       from (select distinct G.IDCONTRACT
             from GOBJECT G with (nolock)
                    inner join GMETER GM with (nolock) on GM.IDGOBJECT = G.IDGOBJECT
                    inner join INDICATION I with (nolock) on GM.IDGMETER = I.IDGMETER
                    inner join FACTUSE F with (nolock) on F.IDINDICATION = I.IDINDICATION
               and F.IDPERIOD = 233 and g.IDCONTRACT= 863080) QQ

------------------------------------------------------------------------------------------------------------------------
select *
from BATCH
where IDPERIOD = 233
  and BATCHDATE = '2024-06-03';

SELECT *
  FROM Document AS d
 WHERE d.IDBATCH = 97339;

select *
from CONTRACT
where IDCONTRACT = 903712;
-- 2473001

--delete from batch where IDBATCH = 97339;
------------------------------------------------------------------------------------------------------------------------
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


------------------------------------------------------------------------------------------------------------------------

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


select top 25 *
from CASHBALANCE
where IDCASHIER = 151
order by DATECASH desc


