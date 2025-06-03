CREATE   Procedure [dbo].[CloseNowPeriod] as
begin
  declare @Tariff numeric (10,4)
  declare @IdPeriod int
  declare @NormaA float
  declare @NormaB float
  declare @NormaC float
  declare @NormaD float

  set @NormaA=0.705
  set @NormaB=1.867
  set @NormaC=2.631
  set @NormaD=2.428

  insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
  values ('CloseNowPeriod', N'Начали spReturnAverage - возврат по среднему', GetDate())

  exec spReturnAverage

  insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
  values ('CloseNowPeriod', N'Выполнили spReturnAverage - возврат по среднему', GetDate())

  set @IdPeriod = dbo.fGetNowPeriod()

  insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
  values ('CloseNowPeriod', N'Начали CloseNowPeriod', GetDate())

  --
  -- распределим без ПУ
  -- Заполним потребление по норме 0.705
  select
    gob.IdGObject,
    gob.IdContract,
    isnull(gob.CountLives, 0) countLive,
    @NormaA Norma,
    f.IdFactUse IdFactUse,
    676980 as IDINDICATION
  into #tmpFU
  from Contract c with (nolock)
  inner join GObject GOb with (nolock) on c.idcontract = gob.idcontract
          and idstatusgobject = 1
  left join gmeter gm with (nolock) on gm.idgobject = gob.idgobject
          and idstatusgmeter = 1
  left join FactUse f with (nolock) on f.IdGObject=gob.IdGObject
             and f.IdTypeFU=2
             and f.IdPeriod=@IdPeriod
             and f.IdOperation is null
             and f.IDIndication is null
  where gm.IdGmeter is null

  insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
  values ('CloseNowPeriod', N'Распределили без ПУ по норме 0.705', GetDate())

  -- Заполним потребление по норме 1.867
  insert #tmpFU (IdGObject, IdContract, countLive, Norma,  IdFactUse, IDINDICATION)
  (select
    gob.IdGObject,
    gob.IdContract,
    isnull(gob.CountLives, 0) countLive,
    @NormaB Norma,
    f.IdFactUse IdFactUse,
    676981 as IDINDICATION
  from Contract c with (nolock)
  inner join GObject GOb with (nolock) on c.idcontract = gob.idcontract
          and idstatusgobject = 1
  left join gmeter gm with (nolock) on gm.idgobject = gob.idgobject
          and idstatusgmeter = 1
  left join FactUse f with (nolock) on f.IdGObject=gob.IdGObject
             and f.IdTypeFU=2
             and f.IdPeriod=@IdPeriod
             and f.IdOperation is null
             and f.IDIndication is null
  where gm.IdGmeter is null)

  insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
  values ('CloseNowPeriod', N'Распределили без ПУ по норме 1.867', GetDate())

  update FactUse
  set FactAmount = Norma*CountLive
  from factuse f with (nolock)
  inner join #tmpFU fu on fu.IdFactUse=f.IdFactUse

  insert FactUse (IdPeriod, FactAmount, IDTypeFU, IdGObject, IDINDICATION)
  select @IdPeriod, Norma*CountLive, 2, IdGObject, IDINDICATION
  from #tmpFU
  where IdFactUse is null

  drop table #tmpFU

  insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
  values ('CloseNowPeriod', N'Распределили потребление без ПУ', GetDate())

  --
  -- начислим по среднему тем у кого есть ПУ и нет показаний (среднее по общей норме)
  select distinct
    gob.IdGObject,
    gob.IdContract,
    isnull(gob.CountLives,0) countLive,
    @NormaC Norma,
    f.IdFactUse IdFactUse,
    c.account,
    676981 as IDINDICATION
  into #tmpFUP
  from Contract c with (nolock)
  inner join GObject gob with (nolock)  on c.idcontract = gob. idcontract
    and idstatusgobject = 1 and isnull(gob.CountLives,0) <> 0 --Добавил если численость 0 зачем создавать лишние записи
  inner join gmeter gm with (nolock) on gm.idgobject = gob.idgobject
    and idstatusgmeter = 1
  left join document d with (nolock)  on d.idcontract=c.idcontract
    and (d.idtypedocument=7 or d.idtypedocument=12 or d.idtypedocument=18)
    and d.idperiod=@IdPeriod
  left join FactUse f with (nolock) on f.IdGObject=gob.IdGObject
    and f.IdTypeFU=1
    and f.IdPeriod=@IdPeriod
  where f.idfactuse is null and d.iddocument is null

  insert FactUse (IdPeriod, FactAmount, IDTypeFU, IdGObject, IDINDICATION)
  select @IdPeriod, Norma*CountLive, 3, IdGObject, IDINDICATION
  from #tmpFUP
  where IdFactUse is null

  drop table #tmpFUP

  insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
  values ('CloseNowPeriod', N'Начислили по среднему - есть ПУ нет показаний', GetDate())

  --
  -- определим дату начисления
  declare @Date DateTime
  select @Date=case when month<12 then convert(datetime, str(year)+'-'+str(month+1)+'-01', 20)-1
  else convert(datetime, str(year+1)+'-01-01', 20)-1  end from Period  with (nolock) where IdPeriod=@IdPeriod

  if month(GetDate()) <> month(@Date)
  begin
    -- определим текущий тариф
    select top 1 @Tariff = value
    from Tariff with (nolock)
      order by IdPeriod desc

    --подготовим выборку по показаниям
    select
      c.IdContract,
      f.IdFactUse,
      f.FactAmount,
      isnull(f.IdTypeFU,0) IdTypeFU,
      case when i.DATEDISPLAY < '2025-05-10' then @NormaD else @NormaC end Norma,
      null IdDocument,
      isnull(b.IdBalance,0) IdBalance,
      case when i.DATEDISPLAY < '2025-05-10' then 711.54 else 985.02 end a_TARIFF
    into #tmpFact
    from FactUse f with (nolock)
    inner join GObject gob with (nolock)  on gob.IdGObject=f.IdGObject
      and f.Idoperation is null
    inner join indication i with (nolock)  on i.idindication=f.idindication
      and dbo.DateOnly(i.DateDisplay) <= @Date and f.IDINDICATION not in (676980, 676981)
    inner join Contract c with (nolock)  on c.IdContract=gob.IdContract
    left join Balance b with (nolock)  on b.IdContract=c.IdContract
      and b.IdPeriod = @IdPeriod
      and b.idaccounting = 1

    insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
    values ('CloseNowPeriod', N'Сформирована выборка по показаниям', GetDate())

    -- подготовим выборку по норме
    ---insert #tmpFact (IdContract, IdFactUse, FactAmount, IdTypeFU, PG, Norma, IdDocument, IdBalance)
    insert #tmpFact (IdContract, IdFactUse, FactAmount, IdTypeFU,  Norma, IdDocument, IdBalance, a_TARIFF)
    (select
       c.IdContract,
       f.IdFactUse,
       f.FactAmount,
       isnull(f.IdTypeFU, 0) IdTypeFU,
       case when f.IDINDICATION = 676980 then @NormaD else @NormaC end Norma,
       null IdDocument,
       isnull(b.IdBalance,0) IdBalance,
       case when f.IDINDICATION = 676980 then 711.54 else 985.02 end a_TARIFF
    from FactUse as f with (nolock)
    inner join GObject as gob with (nolock) on gob.IdGObject=f.IdGObject
      and f.IdOperation is null
      and f.IdTypeFU <> 1
    inner join Contract as c with (nolock)  on c.IdContract=gob.IdContract
    left join Balance as b with (nolock)  on b.IdContract=c.IdContract
      and b.IdPeriod=@IdPeriod
      and b.idaccounting = 1)

    update FactUse
    set IDINDICATION = null where IDINDICATION in (676980, 676981) and IDPERIOD = 244

    insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
    values ('CloseNowPeriod', N'Сформирована выборка по норме', GetDate())

    -- добавим пачку для документов начисление
    declare @IdBatch int
    insert Batch (IDTypePay, IDPeriod, Name, BatchCount, BatchAmount, BatchDate, IDTypeBatch, IDStatusBatch, NumberBatch)
    values (1, @IdPeriod, 'Начисление', 0, 0, @Date, 4, 2, str(@IdPeriod))

    set @IdBatch=scope_identity()

    insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
    values ('CloseNowPeriod', N'Создали пачку для документов Начисление', GetDate())

    insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
    values ('CloseNowPeriod', N'Начинаем создание документов Начисление ПО СТАРОМУ до 10 мая', GetDate())
    --
    -- добавим документы начисление
    insert Document (IDContract, IDPeriod, IDBatch, IDTypeDocument, DocumentNumber, DocumentDate, DocumentAmount, NOTE)
    (select distinct IdContract, @IdPeriod, @IDBatch, 5, 'Начисление',  @Date, 0, 'Начисление по 711.54'
     from #tmpFact
     where A_TARIFF = 711.54)

    insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
    values ('CloseNowPeriod', N'Добавили документы Начисление ПО СТАРОМУ до 10 мая', GetDate())

    insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
    values ('CloseNowPeriod', N'Начинаем создание документов Начисление ПО НОВОМУ с 10 мая', GetDate())

    insert Document (IDContract, IDPeriod, IDBatch, IDTypeDocument, DocumentNumber, DocumentDate, DocumentAmount, NOTE)
    (select distinct IdContract, @IdPeriod, @IDBatch, 5, 'Начисление (НТ)',  @Date, 0, 'Начисление по 985.02'
     from #tmpFact
     where A_TARIFF = 985.02)

    insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
    values ('CloseNowPeriod', N'Добавили документы Начисление', GetDate())

    insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
    values ('CloseNowPeriod', N'Начинаем обновление ID документов', GetDate())

    -- Сначала ставим «Начисление»
    update f
    set IdDocument = d.IdDocument
    from #tmpFact f
    inner join Document d with (nolock)
       on d.IdContract = f.IdContract
      and d.IdTypeDocument = 5
      and d.IdPeriod = @IdPeriod
      and d.DocumentDate = @Date
      and d.DOCUMENTNUMBER = 'Начисление'
      and f.A_TARIFF = 711.54

    -- Теперь ставим «Начисление (НТ)»
    update f
    set IdDocument = d.IdDocument
    from #tmpFact f
    inner join Document d with (nolock)
       on d.IdContract = f.IdContract
      and d.IdTypeDocument = 5
      and d.IdPeriod = @IdPeriod
      and d.DocumentDate = @Date
      and d.DOCUMENTNUMBER = 'Начисление (НТ)'
      and f.A_TARIFF = 985.02
    where f.IdDocument is null

    insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
    values ('CloseNowPeriod', N'Закончили обновление ID документов', GetDate())

    insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
    values ('CloseNowPeriod', N'Начинаем формировать Балансы и Операции документов Начисление', GetDate())

    declare @Count int
    declare @IdDoc int
    DECLARE @IdContract int
    DECLARE @IdFactUse int
    DECLARE @FactAmount float
    DECLARE @IdTypeFU int
    --DECLARE @PG float
    DECLARE @Norma float
    DECLARE @IdBalance int
    declare @AmountOperation float
    declare @a_Tariff numeric (10,4)
    declare @number int
    declare @IDOperation int
    DECLARE cur CURSOR
    READ_ONLY
    --FOR select  distinct  IdContract, IdFactUse, FactAmount, IdTypeFU, PG, Norma, IdDocument, IdBalance from #tmpFact
    FOR select  distinct  IdContract, IdFactUse, FactAmount, IdTypeFU,  Norma, IdDocument, IdBalance, a_TARIFF from #tmpFact

    OPEN cur
    --поехали
    set @Count=0
    --FETCH NEXT FROM cur INTO @IdContract, @IdFactUse, @FactAmount, @IdTypeFU, @PG, @Norma, @IdDoc, @IdBalance
    FETCH NEXT FROM cur INTO @IdContract, @IdFactUse, @FactAmount, @IdTypeFU,  @Norma, @IdDoc, @IdBalance, @a_Tariff

    WHILE (@@fetch_status <> -1)
    BEGIN
      IF (@@fetch_status <> -2)
      BEGIN
        set @Count=@Count+1
        --Если нужно добавляем баланс
        if @IdBalance=0
        begin
          insert Balance (IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
          values (1, @IdPeriod, @IdContract, 0, 0, 0)
          set @IdBalance=scope_identity()
        end
        set @number=0
        set @AmountOperation=0
        --расчитаем сумму операции
        if @IdTypeFU=1
          set @AmountOperation=@FactAmount*@a_Tariff --кубы
          --set @AmountOperation=@FactAmount*@PG*@Tariff --кубы
        if @IdTypeFU=2
          set @AmountOperation=@FactAmount*@a_Tariff --кг
        if @IdTypeFU=3
        begin
          set @number=99999
          set @AmountOperation=@FactAmount*985.02 --кг
        end

        --Добавляем операцию начисление
        insert Operation (DateOperation, AmountOperation, NumberOperation, IDBalance, IDDocument, IdTypeOperation)
        values (@Date, -@AmountOperation, @number, @IdBalance, @IdDoc, 2)
        set @Idoperation=scope_identity()

        update Factuse
        set idoperation=@idoperation
        where idfactuse=@idfactuse

      END
    --	FETCH NEXT FROM cur INTO @IdContract, @IdFactUse, @FactAmount, @IdTypeFU, @PG, @Norma, @IdDoc, @IdBalance
      FETCH NEXT FROM cur INTO @IdContract, @IdFactUse, @FactAmount, @IdTypeFU,  @Norma, @IdDoc, @IdBalance, @a_Tariff

    END
    CLOSE cur
    DEALLOCATE cur

    insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
    values ('CloseNowPeriod', N'Добавили Балансы и Операции документов Начисление', GetDate())

    --Начислем пеню
    exec spChargePeny
    --Начислим за услуги ВДГО
    --exec spChargeVDGO
    --Начислим за кредитные услуги по графику
    --exec spChargeCreditUsl
    --пересчитаем сумму документов
    update document
    set DocumentAmount=-t.summa
    from document with (nolock)
    left join (select sum(operation.AmountOperation) as summa,iddocument
      from operation with (nolock)
      group by operation.iddocument
    ) T on document.iddocument=t.iddocument
    where document.idtypedocument=5 and document.idperiod=@idPeriod

    drop table #tmpFact

    insert into dbo.ClosePeriodLog (SPName, StepName, DateExec)
    values ('CloseNowPeriod', N'Пересчитаем балансы после начисления пени', GetDate())

    --пересчитаем сумму остатков
    exec dbo.spRecalcBalancesOnePeriod @IdPeriod
    exec dbo.spRecalcBalancesRealOnePeriod @idPeriod

    insert into dbo.ClosePeriodLog (SPName, StepName, DateExec)
    values ('CloseNowPeriod', N'Заполним PD для type = 36', GetDate())

    -- TypePD=36 Двойной тариф 1 - новый, 2 - старый
    insert pd ( IDTypePD, IDDocument, Value)
    select 36, OLD.IDDocument,2
    from (select D.IDDOCUMENT as IDDocument
          from Document as D
          where D.IDPERIOD = @IDPERIOD
            and D.IDTYPEDOCUMENT = 5
            and D.DOCUMENTNUMBER = 'Начисление') as OLD

    insert pd ( IDTypePD, IDDocument, Value)
    select 36, NEW.IDDocument,1
    from (select D.IDDOCUMENT as IDDocument
          from Document as D
          where D.IDPERIOD = @IDPERIOD
            and D.IDTYPEDOCUMENT = 5
            and D.DOCUMENTNUMBER = 'Начисление (НТ)') as NEW

    -- Определяем дату следующего периода
    select @Date=case when month<12 then convert(datetime, str(year)+'-'+str(month+1)+'-01', 20) else convert(datetime, str(year+1)+'-01-01', 20)  end from Period where IdPeriod=@IdPeriod

    insert into dbo.ClosePeriodLog (SPName, StepName, DateExec)
    values ('CloseNowPeriod', N'Добавляем номер нового периода', GetDate())

    insert Period (Year, Month, DateBegin, DateEnd)
    values (Year(@Date), Month(@Date), @Date, case when month(@Date)<12 then convert(datetime, str(year(@Date))+'-'+str(month(@Date)+1)+'-01', 20)-1
    else convert(datetime, str(year(@Date)+1)+'-01-01', 20)-1  end)

    set @IdPeriod=scope_identity()

    insert Tariff (Value, IdPeriod,IDTypeTariff)
    select
      985.02,
      @IdPeriod,
      1

    update factuse
    set IdPeriod=@idperiod
    from FactUse f with (nolock)
    inner join indication i with (nolock)  on i.idindication=f.idindication
      and dbo.DateOnly(i.DateDisplay) >= @Date

    insert into dbo.ClosePeriodLog (SPName, StepName, DateExec)
    values ('CloseNowPeriod', N'Добавили тариф для периода и обновили потребление', GetDate())

    insert into dbo.ClosePeriodLog (SPName, StepName, DateExec)
    values ('CloseNowPeriod', N'Начинаем изменять численность проживающих где есть документы со сроком действия', GetDate())
    --меняем численность проживающих, где есть документы со сроком действия.

    declare @idperiodpr int
    declare @month int
    declare @year int

    select @idperiodpr=dbo.fGetPredPeriod()
    select @month=month(dateend), @year=year(dateend) from period where Idperiod=@idperiodpr

    update gobject
    set countlives=convert(int, pd1.value)
    from document d
    inner join PD on pd.iddocument=d.iddocument
      and d.idtypedocument=2
      --and d.idperiod=33
      and pd.idtypepd=6
      and month(convert(datetime, pd.value, 104))=@month
      and year(convert(datetime, pd.value, 104))=@year
    inner join contract c on c.idcontract=d.idcontract
    inner join PD PD1 on pd1.iddocument=d.iddocument
      and pd1.idtypepd=5
    left join document d1 on d1.idcontract=c.idcontract
    and d1.idtypedocument=2 and d1.documentdate>d.documentdate
    inner join gobject g on g.idcontract=c.idcontract
    where d1.iddocument is null

    insert into oldvalues(nametable,namecolumn,idobject,idperiod,datevalues,name)
    select 'GObject','CountLives',g.idgobject,dbo.fGetNowPeriod(),dbo.DateOnly(GetDate()), pd1.value
    from document d
    inner join PD on pd.iddocument=d.iddocument
      and d.idtypedocument=2
      --and d.idperiod=33
      and pd.idtypepd=6
      and month(convert(datetime, pd.value, 104))=@month
      and year(convert(datetime, pd.value, 104))=@year
    inner join contract c on c.idcontract=d.idcontract
    inner join PD PD1 on pd1.iddocument=d.iddocument
      and pd1.idtypepd=5
    left join document d1 on d1.idcontract=c.idcontract
    and d1.idtypedocument=2 and d1.documentdate>d.documentdate
    inner join gobject g on g.idcontract=c.idcontract
    where d1.iddocument is null


    insert into dbo.ClosePeriodLog (SPName, StepName, DateExec)
    values ('CloseNowPeriod', N'Изменили численность проживающих где есть документы со сроком действия', GetDate())
    --exec dbo.RecalcBalancesOnePeriod @IdPeriod

    insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
    values ('CloseNowPeriod', N'Закончили CloseNowPeriod', GetDate())

  end
end
go

