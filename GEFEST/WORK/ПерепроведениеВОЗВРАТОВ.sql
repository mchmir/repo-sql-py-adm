declare @IDPERIOD INT;
set @IDPERIOD = 243;

declare @IDPERIODPRED INT;
set @IDPERIODPRED = 242;

declare @IDBATCH INT;
set @IDBATCH = 99126;

declare @DATE DATETIME;

select @DATE = case
                 when [MONTH] < 12 then convert(DATETIME, str([YEAR]) + '-' + str([MONTH] + 1) + '-01', 20) - 1
                 else convert(DATETIME, str([YEAR] + 1) + '-01-01', 20) - 1
  end
from PERIOD with (nolock)
where IDPERIOD = @IDPERIOD;

declare @TMPAVFACT TABLE
                   (
                     ACCOUNT    VARCHAR(20),
                     IDCONTRACT INT,
                     IDGOBJECT  INT,
                     DOCAMOUNT  FLOAT,
                     FACTAMOUNT FLOAT
                   );

-- Заполняем @TMPAVFACT: случай 1
insert @TMPAVFACT (ACCOUNT, IDCONTRACT, IDGOBJECT)
select distinct C.ACCOUNT, C.IDCONTRACT, G.IDGOBJECT
from CONTRACT C
       inner join GOBJECT G on G.IDCONTRACT = C.IDCONTRACT
       inner join FACTUSE F on F.IDGOBJECT = G.IDGOBJECT
  and F.IDTYPEFU = 3
  and F.IDPERIOD = @IDPERIODPRED
  and F.FACTAMOUNT > 0
       inner join FACTUSE FU on FU.IDGOBJECT = G.IDGOBJECT
  and FU.IDPERIOD = @IDPERIOD
  and FU.IDTYPEFU = 1

-- Заполняем @TMPAVFACT: случай 2
insert @TMPAVFACT (ACCOUNT, IDCONTRACT, IDGOBJECT)
select distinct C.ACCOUNT, C.IDCONTRACT, G.IDGOBJECT
from CONTRACT C
       inner join GOBJECT G on G.IDCONTRACT = C.IDCONTRACT
       inner join FACTUSE F on F.IDGOBJECT = G.IDGOBJECT
  and F.IDTYPEFU = 3
  and F.IDPERIOD < @IDPERIODPRED
  and F.FACTAMOUNT > 0
       inner join FACTUSE FU on FU.IDGOBJECT = G.IDGOBJECT
  and FU.IDPERIOD = @IDPERIOD
  and FU.IDTYPEFU = 1
       left join DOCUMENT D on D.IDCONTRACT = C.IDCONTRACT
  and D.IDTYPEDOCUMENT = 14
  and D.IDBATCH not in (66908, 68089)
  and D.IDPERIOD < @IDPERIODPRED
       left join @TMPAVFACT T on T.IDGOBJECT = G.IDGOBJECT
where T.IDGOBJECT is null
  and D.IDDOCUMENT is null
group by C.ACCOUNT, C.IDCONTRACT, G.IDGOBJECT

-- Заполняем @TMPAVFACT: случай 3
insert @TMPAVFACT (ACCOUNT, IDCONTRACT, IDGOBJECT)
select distinct C.ACCOUNT, C.IDCONTRACT, G.IDGOBJECT
from CONTRACT C
       inner join GOBJECT G on G.IDCONTRACT = C.IDCONTRACT
       inner join (select IDGOBJECT, max(F.IDPERIOD) as FFFIPEROD
                   from FACTUSE F
                   where F.IDTYPEFU = 3
                     and F.IDPERIOD < @IDPERIODPRED
                     and F.FACTAMOUNT > 0
                   group by IDGOBJECT) FFF on FFF.IDGOBJECT = G.IDGOBJECT
       inner join FACTUSE FU on FU.IDGOBJECT = G.IDGOBJECT
  and FU.IDPERIOD = @IDPERIOD
  and FU.IDTYPEFU = 1
       inner join (select IDCONTRACT, max(IDPERIOD) as PER
                   from DOCUMENT D
                   where D.IDTYPEDOCUMENT = 14
                     and D.IDBATCH not in (66908, 68089)
                   group by IDCONTRACT) DDD on DDD.IDCONTRACT = C.IDCONTRACT
  and FFF.FFFIPEROD > DDD.PER
       left join @TMPAVFACT T on T.IDGOBJECT = G.IDGOBJECT
where T.IDGOBJECT is null
group by C.ACCOUNT, C.IDCONTRACT, G.IDGOBJECT

-- Курсор
declare CURAVR cursor read_only for
  select IDCONTRACT, IDGOBJECT
  from @TMPAVFACT

declare @IDCONTRACT INT;
declare @IDGOBJECT INT;
declare @IDLASTPERIOD INT;
declare @FACTAMOUNT FLOAT;
declare @FACTAMOUNTM3 FLOAT;
declare @DOCAMOUNT FLOAT;
declare @IDDOCUMENT INT;

open CURAVR;

fetch next from CURAVR into @IDCONTRACT, @IDGOBJECT
while (@@fetch_status <> -1)
  begin
    if (@@fetch_status <> -2)
      begin
        set @IDLASTPERIOD = null;
        set @FACTAMOUNT = 0;
        set @FACTAMOUNTM3 = 0;

        select top 1 @IDLASTPERIOD = isnull(D.IDPERIOD, 0)
        from DOCUMENT D
        where D.IDCONTRACT = @IDCONTRACT
          and D.IDPERIOD < @IDPERIOD
          and D.IDTYPEDOCUMENT = 14
          and D.IDBATCH not in (66908, 68089)
        order by D.IDPERIOD desc;

        if @IDLASTPERIOD is null
          set @IDLASTPERIOD = 1;

        select @DOCAMOUNT = sum(FA)
        from (select F.FACTAMOUNT * (select VALUE from TARIFF where IDPERIOD = F.IDPERIOD) as FA
              from FACTUSE F
              where F.IDGOBJECT = @IDGOBJECT
                and isnull(F.IDTYPEFU, 0) = 3
                and F.IDPERIOD > @IDLASTPERIOD
                and F.IDPERIOD < 243 -- исключаем период, который закрывался
                --and @IDGOBJECT = 887949  -- тест
             ) QQ;

        select @IDDOCUMENT = D.IDDOCUMENT
        from (select IDDOCUMENT as IDDOCUMENT
              from DOCUMENT
              where IDCONTRACT = @IDCONTRACT
                and IDTYPEDOCUMENT = 14
                and IDBATCH = 99126
                --and @IDCONTRACT = 887716  -- test
                ) as D

        update @TMPAVFACT
        set DOCAMOUNT = @DOCAMOUNT
        where IDCONTRACT = @IDCONTRACT;

        update DOCUMENT
        set DOCUMENTAMOUNT = @DOCAMOUNT
        where IDDOCUMENT = @IDDOCUMENT;

        update OPERATION
        set AMOUNTOPERATION = @DOCAMOUNT
        where IDDOCUMENT = @IDDOCUMENT;

        exec dbo.spRecalcBalancesRealOnePeriodByContract @IDCONTRACT, @IDPeriod
        exec dbo.spRecalcBalances @IDCONTRACT, @IDPeriod
        exec dbo.spRecalcBalancesRealOnePeriodByContract @IDCONTRACT, 244
        exec dbo.spRecalcBalances @IDCONTRACT, 244

      end -- конец IF
    fetch next from CURAVR into @IDCONTRACT, @IDGOBJECT
  end -- конец WHILE

close CURAVR;
deallocate CURAVR;

-- Финальный вывод
select *
from @TMPAVFACT
order by ACCOUNT;


