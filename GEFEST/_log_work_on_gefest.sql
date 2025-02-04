--
--
--





------------------------------------------------------------------------------------------------------------------------
-- 3 февраля 2025 года
-- Создана новая база данных ClosePeriodLog
-- в ней создана таблица ClosePeriodLogs
-- В бд Гефест для таблицы ClosePeriod создан триггер
-- дублирование логов в базу данных ClosePeriodLog

CREATE TRIGGER TR_ClosePeriodLog_Audit
ON dbo.ClosePeriodLog
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Дублирование в другую базу
    INSERT INTO CLOSEPERIODLOG.DBO.CLOSEPERIODLOGS (SPNAME, STEPNAME, DATEEXEC)
    SELECT SPNAME, STEPNAME, DATEEXEC FROM inserted;
END;
go

-- Процедура ActionForPeriod - добавлены более подробное логирование

------------------------------------------------------------------------------------------------------------------------
-- Функция определения IDCONTRACT по Лицевому счету
-- Функция получения значения параметра для документа
-- Update date: 2024-07-30
-- Author: Mikhail Chmir

if OBJECT_ID('dbo.fGetPDValue', 'FN') is not null
  drop function dbo.fGetIDContractAC;
go

create function [dbo].[fGetIDContractAC](@ACCOUNT varchar(50))
/**
 * Returns the IDCONTRACT by the ACCOUNT
 */
  returns int as
begin

  declare @GET int

  set @GET = (select top 1 IDCONTRACT
              from CONTRACT (nolock)
              where  ACCOUNT = @ACCOUNT)
  return @GET

end


if OBJECT_ID('dbo.fGetPDValue', 'FN') is not null
  drop function dbo.fGetPDValue;

go

create function [dbo].[fGetPDValue](@IDDOCUMENT int, @IDTYPEPD int)
/**
 * Returns the value for the document
 */
  returns varchar(50) as
begin

  declare @GET varchar(50)

  set @GET = (select VALUE
              from PD (nolock)
              where  IDTYPEPD=@IDTYPEPD and IDDOCUMENT=@IDDOCUMENT)
  return @GET

end






------------------------------------------------------------------------------------------------------------------------
-- Отчет Анализ дебиторской задолженности
-- Update date: 2023-11-28
-- Author: Mikhail Chmir
-- Description: Удален закоментированный код.
-- Удалено условие PeriodPay > 3
------------------------------------------------------------------------------------------------------------------------
-- =============================================
-- Author: Sumlikin,Aleksandr
-- Create date: 2007-10-08
-- Description:
--
-- Update date: 2023-11-28
-- Author: Mikhail Chmir
-- Description: Удален закоментированный код.
-- Удалено условие PeriodPay > 3
-- =============================================
ALTER PROCEDURE repAnalysisDebtsDuty(@dateB datetime,@amount float,@OnlyOn int)
AS
BEGIN

select  c.idcontract, c.account+'; '+isnull(p.Surname,'')+' '+isnull(p.name,'')+' '+isnull(p.Patronic,'') as account ,case when g.idstatusgobject=1 then 'П'else 'О'end statusgobject,a.name,
isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (dateadd(month,-11,@dateB)), c.IdContract, 0),0) b12 ,
isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (dateadd(month,-10,@dateB)), c.IdContract, 0),0) b11 ,
isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (dateadd(month,-9,@dateB)), c.IdContract, 0),0) b10 ,
isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (dateadd(month,-8,@dateB)), c.IdContract, 0),0) b9 ,
isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (dateadd(month,-7,@dateB)), c.IdContract, 0),0) b8 ,
isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (dateadd(month,-6,@dateB)), c.IdContract, 0),0) b7 ,
isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (dateadd(month,-5,@dateB)), c.IdContract, 0),0) b6 ,
isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (dateadd(month,-4,@dateB)), c.IdContract, 0),0) b5 ,
isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (dateadd(month,-3,@dateB)), c.IdContract, 0),0) b4 ,
isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (dateadd(month,-2,@dateB)), c.IdContract, 0),0) b3 ,
isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (dateadd(month,-1,@dateB)), c.IdContract, 0),0) b2 ,
dbo.fGetLastBalance (dbo.fGetPeriodDate (@dateB), c.IdContract, 0) b1,
case
  when isnull(gm.idstatusgmeter,2) = 1 then 'П'
  else 'О'
  end  idstatusgmeter,
convert(varchar,'') urddok,
0 as PeriodPay
into #tmpSaldoPOGRU
from contract c with (nolock)
inner join person p (nolock) on c.IDPerson=p.idperson
inner join gobject g with (nolock) on c.idcontract=g.idcontract
and isnull(dbo.fGetLastBalance (dbo.fGetPeriodDate (@dateB), c.IdContract, 0),0)<@amount
inner join gru q with (nolock) on q.idgru=g.idgru
inner join agent a with (nolock) on q.idagent=a.idagent
left join gmeter gm with (nolock) on gm.idgobject=g.idgobject and idstatusgmeter=1
order by account, a.name

if @OnlyOn=1
	begin
		delete from #tmpSaldoPOGRU where statusgobject='О'
	end

update #tmpSaldoPOGRU
set PeriodPay=n.idper
from #tmpSaldoPOGRU t
inner join ( select idcontract, max(idperiod) as idper from document d with (nolock)
where d.idtypedocument=1 group by idcontract) n on t.idcontract=n.idcontract

-- Условие удалено 2023-11-28
-- delete from #tmpSaldoPOGRU where PeriodPay>dbo.fGetPeriodDate (dateadd(month,-3,@dateB))

update #tmpSaldoPOGRU
set urddok='Ю/д'
from #tmpSaldoPOGRU t
inner join balance b on b.idcontract=t.idcontract and b.idperiod=dbo.fGetPeriodDate (@dateB) and b.idaccounting=2 and b.amountbalance<0

select * from #tmpSaldoPOGRU
drop table #tmpSaldoPOGRU

END

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