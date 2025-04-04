-- SETTING
declare @Potreblenie bit;
declare @Month1_1    int;
declare @Year1_1     int;
declare @Month1_2    int;
declare @Year1_2     int;

declare @Month2_1    int;
declare @Year2_1     int;
declare @Month2_2    int;
declare @Year2_2     int;

declare @Period1_1   int;
declare @Period1_2   int;
declare @Period2_1   int;
declare @Period2_2   int;

----------the first period-------------
set @Month1_1 = 1;
set @Year1_1  = 2024;

set @Month1_2 = 12;
set @Year1_2  = 2024;
/*
----------the second period -----------
set @Month2_1 = 7;
set @Year2_1  = 2021;
set @Month2_2 = 6;
set @Year2_2  = 2022;

 */
--------------------------------------
-- с нулевым потреблением, оставляем 0 только для подключенных ОУ и ПУ
-- 1 - без 0 в потрблении 
set @Potreblenie = 1;

--------------------------------------
-- END SETTING


--- PROGRAM
-- первый период
set @Period1_1 = dbo.fGetIDPeriodMY(@Month1_1, @Year1_1);
set @Period1_2 = dbo.fGetIDPeriodMY(@Month1_2, @Year1_2);

/*
-- второй период
set @Period2_1 = dbo.fGetIDPeriodMY(@Month2_1, @Year2_1);
set @Period2_2 = dbo.fGetIDPeriodMY(@Month2_2, @Year2_2);
*/

-- основная таблица
select C.ACCOUNT,
       S.NAME + ', ' + LTRIM(STR(H.HOUSENUMBER)) + ISNULL(H.HOUSENUMBERCHAR, '') + '-' + LTRIM(A.FLAT) as ADDRESS
  into #tmpAccount
from CONTRACT as C,
     GOBJECT as G,
     PERSON as P,
     ADDRESS as A,
     STREET as S,
     HOUSE as H
where C.IDCONTRACT = G.IDCONTRACT
  and C.IDPERSON = P.IDPERSON
  and P.IDADDRESS = A.IDADDRESS
  and A.IDSTREET = S.IDSTREET
  and A.IDHOUSE = H.IDHOUSE
order by C.ACCOUNT;

-- первый период
select C.ACCOUNT,
       sum(FU.FACTAMOUNT) as SUMP1
  into #tmpPotreblenie1
from FACTUSE as FU,
     TYPEFU as TF,
     CONTRACT as C,
     GOBJECT as G
where TF.IDTYPEFU = FU.IDTYPEFU
  and FU.IDGOBJECT = G.IDGOBJECT
  and C.IDCONTRACT = G.IDCONTRACT
  and FU.IDPERIOD >= @PERIOD1_1
  and FU.IDPERIOD <= @PERIOD1_2
  --AND g.IDStatusGObject = 1
  and FU.IDTYPEFU in (1, 2) -- 1 по ПУ, 2 - по кол-ву проживающих
group by C.ACCOUNT
order by C.ACCOUNT;

/*
-- второй период

select C.ACCOUNT,
       sum(FU.FACTAMOUNT) as SUMP2
  into #tmpPotreblenie2
from FACTUSE as FU,
     TYPEFU as TF,
     CONTRACT as C,
     GOBJECT as G
where TF.IDTYPEFU = FU.IDTYPEFU
  and FU.IDGOBJECT = G.IDGOBJECT
  and C.IDCONTRACT = G.IDCONTRACT
  and FU.IDPERIOD >= @PERIOD2_1
  and FU.IDPERIOD <= @PERIOD2_2
  --AND g.IDStatusGObject = 1
  and FU.IDTYPEFU in (1, 2)
group by C.ACCOUNT
order by C.ACCOUNT;
*/

/*
-- третий период
select C.ACCOUNT
     , sum(FU.FACTAMOUNT) as SUMM3
into #tmpPotreblenie3
from FACTUSE FU,
     TYPEFU TF,
     CONTRACT C,
     GOBJECT G
where TF.IDTYPEFU = FU.IDTYPEFU
  and FU.IDGOBJECT = G.IDGOBJECT
  and C.IDCONTRACT = G.IDCONTRACT
  and FU.IDPERIOD >= 186
  and FU.IDPERIOD <= 197
--AND g.IDStatusGObject = 1
  and FU.IDTYPEFU in (1, 2)
group by C.ACCOUNT
order by C.ACCOUNT;
*/

select distinct A.ACCOUNT,
                A.ADDRESS,
                ISNULL(P1.SUMP1, 0) as [2024г]
-- ,ISNULL(p2.sumP2, 0) as [0721г-0622г]
-- ,ISNULL(p.summ3, 0) AS [0721г-0622г]
  into #tmpItog
from #tmpAccount as A
  left join #tmpPotreblenie1 as P1
on A.ACCOUNT = P1.ACCOUNT
-- left join #tmpPotreblenie2 as p2 on a.Account = p2.Account
-- left join #tmpPotreblenie3 p3 ON a.Account = p3.Account
order by A.ACCOUNT;

IF @Potreblenie = 0
  begin
      -- с нулевым потреблением
      -- но оставляем с 0 только там где ОУ и ПУ подключены
    select ROW_NUMBER() OVER(order by i.Account asc) as ROW#,*
    from #tmpitog as I
    where (I.ACCOUNT in (select C.ACCOUNT
                         from CONTRACT as C
                                join GOBJECT as G on C.IDCONTRACT = G.IDCONTRACT
                                join GMETER as G1 on G.IDGOBJECT = G1.IDGOBJECT
                         where G.IDSTATUSGOBJECT = 1
                           and G1.IDSTATUSGMETER = 1)
      )
     --  or (I.[0721Г - 0622Г] <> 0)
       or (I.[2024г] <> 0)
    --WHERE i.[0721г-0622г] <> 0
    --OR i.[2021г] <> 0
    --OR i.[2020г] <> 0
    order by I.ACCOUNT;
  end
else
  begin
    select ROW_NUMBER() OVER(order by I.ACCOUNT asc) as ROW#,*
    from #tmpItog as I
    where
       -- I.[0721Г - 0622Г] <> 0 or
        I.[2024г] <> 0
    order by I.ACCOUNT;
  end
 
---------------------------------------------------------


drop table #tmpAccount;
drop table #tmpPotreblenie1;
--drop table #tmpPotreblenie2;
--DROP TABLE #tmpPotreblenie3;
drop table #tmpItog;

