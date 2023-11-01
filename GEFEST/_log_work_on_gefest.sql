------------------------------------------------------------------------------------------------------------------------
--
--
--
------------------------------------------------------------------------------------------------------------------------





------------------------------------------------------------------------------------------------------------------------
--
--
--
------------------------------------------------------------------------------------------------------------------------






------------------------------------------------------------------------------------------------------------------------
-- 31 октября 2023
-- Новая форма ЕПД
-- Добавлено новое поле "Количество проживающих"
------------------------------------------------------------------------------------------------------------------------
CREATE procedure dbo.repCountNoticeERC21 (@IdGRU int, @IdPeriod int) as
...
--строка 199 --#############################################################################
--.. 200 --INSERT INTO AAAERC -- для версии номер 3
SELECT
  cip.Account,
  cip.Year,
  cip.Month,
  cip.FIO,
  --- добавлено 31.10.2023 новый ЕПД --------------
  cip.Occupancy,
  -------------------------------------------------
  CASE
  WHEN cip.LastIndication>0 AND ISNULL(cip.GetIndication,0) = 0 THEN 'По норме:'
  WHEN ISNULL(cip.LastIndication,0)=0 AND ISNULL(cip.GetIndication,0)=0 THEN 'По норме:'
  ELSE  'ПУ:№'+ CAST(cip.Serialnumber AS VARCHAR) + ','+ convert(varchar(10), cip.dateverify, 104)
  END AS PersUstr,
  -----.. 214-------


------------------------------------------------------------------------------------------------------------------------
-- 18 июля 2023
-- Оптимизация процедур spRecalcBalances, spRecalcBalancesRealOnePeriodByContract
-- Оптимизация вычислений до 3 знаков в балансах
------------------------------------------------------------------------------------------------------------------------
create procedure dbo.spRecalcBalances(
  @IDCONTRACT    as int,
  @IDPERIOD      as int
) as
declare
  @IDPREVPERIOD  as int
-------------------Процедура по перерасчету баланса по контракту--------------------------
select @IDPREVPERIOD = (select top 1 IDPERIOD
                        from PERIOD with (nolock)
                        where IDPERIOD < @IDPERIOD
                        order by IDPERIOD desc)

insert into BALANCE (IDACCOUNTING, IDPERIOD, IDCONTRACT, AMOUNTBALANCE, AMOUNTCHARGE, AMOUNTPAY)
  (select B1.IDACCOUNTING,
          @IDPERIOD IDPERIOD,
          B1.IDCONTRACT,
          ROUND(B1.AMOUNTBALANCE, 3),
          ROUND(B1.AMOUNTCHARGE, 3),
          ROUND(B1.AMOUNTPAY, 3)
   from BALANCE B1 with (nolock)
          left outer join BALANCE B2 with (nolock) on B1.IDCONTRACT = B2.IDCONTRACT
     and B1.IDACCOUNTING = B2.IDACCOUNTING
     and B2.IDPERIOD = @IDPERIOD
   where B2.IDBALANCE is null
     and B1.AMOUNTBALANCE <> 0
     and B1.IDPERIOD = @IDPREVPERIOD
     and B1.IDCONTRACT = @IDCONTRACT
  )

update BALANCE
set BALANCE.AMOUNTBALANCE=ROUND(T.AMOUNTBALANCE, 3),
    BALANCE.AMOUNTCHARGE=ROUND(T.AMOUNTCHARGE, 3),
    BALANCE.AMOUNTPAY=ROUND(T.AMOUNTPAY, 3)
from BALANCE with (nolock)
       inner join (select NEWBAL.IDACCOUNTING                                                               IDACCOUNTING
                        , @IDPERIOD                                                                         IDPERIOD
                        , NEWBAL.IDCONTRACT                                                                 IDCONTRACT
                        , isnull(BALANCE.AMOUNTBALANCE, 0) + isnull(sum(OPERATION.AMOUNTOPERATION), 0)   as AMOUNTBALANCE
                        , isnull(BALANCE.AMOUNTCHARGE, 0) + isnull(
          sum(case when TYPEOPERATION.IDTYPEOPERATION = 2 then OPERATION.AMOUNTOPERATION else 0 end), 0) as AMOUNTCHARGE
                        , isnull(BALANCE.AMOUNTPAY, 0) + isnull(
          sum(case when TYPEOPERATION.IDTYPEOPERATION = 1 then OPERATION.AMOUNTOPERATION else 0 end), 0) as AMOUNTPAY
                   from (select * from BALANCE BBB with (nolock) where IDPERIOD = @IDPREVPERIOD) BALANCE
                          right outer join BALANCE NEWBAL with (nolock) on BALANCE.IDCONTRACT = NEWBAL.IDCONTRACT and
                                                                           BALANCE.IDACCOUNTING = NEWBAL.IDACCOUNTING
                          left outer join OPERATION with (nolock) on OPERATION.IDBALANCE = NEWBAL.IDBALANCE
                          left outer join TYPEOPERATION with (nolock)
                                          on OPERATION.IDTYPEOPERATION = TYPEOPERATION.IDTYPEOPERATION
                   where NEWBAL.IDPERIOD = @IDPERIOD
                   group by NEWBAL.IDACCOUNTING, NEWBAL.IDCONTRACT, BALANCE.AMOUNTBALANCE, BALANCE.AMOUNTCHARGE,
                            BALANCE.AMOUNTPAY
) T on BALANCE.IDPERIOD = T.IDPERIOD and BALANCE.IDCONTRACT = T.IDCONTRACT and BALANCE.IDACCOUNTING = T.IDACCOUNTING
where BALANCE.IDCONTRACT = @IDCONTRACT
go



------------------------------------------------------------------------------------------------------------------------
-- 29 июня 2023
-- Оптимизация таблицы AUTODOCUMENT
-- Очистка, создание индекса (idautobatch, number, IDAutoDocument)
------------------------------------------------------------------------------------------------------------------------
select 'Всего', count(*)
from AUTODOCUMENT

union all

select 'не 2023', count(*)
from AUTODOCUMENT
where Year(DOCUMENTDATE)<>2023;

select Year(DOCUMENTDATE), count(*)
from AUTODOCUMENT
group by Year(DOCUMENTDATE);
+----+------+
|    |      |
+----+------+
|2015|217853|
|2016|254816|
|2017|277711|
|2018|181234|
|2019|480086|
|2020|501232|
|2021|515496|
|2022|530483|
|2023|265772|
|2026|1     |
+----+------+

+-------+-------+
|       |       |
+-------+-------+
|Всего  |3224684|
|не 2023|2958912|
+-------+-------+

--delete from AUTODOCUMENT where Year(DOCUMENTDATE)<>2023;

CREATE INDEX IX_AutoDocument_IdBatch_Number_IDAutoDocument
ON AutoDocument (idautobatch, number, IDAutoDocument);

--DROP INDEX IX_AutoDocument_IdBatch_Number_IDAutoDocument ON AutoDocument;
------------------------------------------------------------------------------------------------------------------------