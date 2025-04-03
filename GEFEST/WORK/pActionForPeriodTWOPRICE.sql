CREATE Procedure [dbo].[ActionForPeriod] as
begin

  --use Gefest
  -- Монопольно захватываем базу
  declare @error BIT
  declare @Q as VarChar(1000)
  declare @test BIT

          --логируем
      insert into dbo.ClosePeriodLog (SPName, StepName, DateExec)
      values ('ActionForPeriod', N'Начато закрытие периода', GetDate())

  set @test = 0
  --set @Q=db_name()
  set @Q='gefest'
  exec @error=sp_dboption @Q,'single user', 'true'

        --логируем
      insert into dbo.ClosePeriodLog (SPName, StepName, DateExec)
      values ('ActionForPeriod', N'Перевели в однопользовательский режим', GetDate())

  if @error=0
  begin
    declare @path as varchar(1000)
            --логируем
      insert into dbo.ClosePeriodLog (SPName, StepName, DateExec)
      values ('ActionForPeriod', N'Начали BackUp', GetDate())

    --set @path='D:\Backup\Gefest\ClosePeriod'+convert(varchar(10),GetDate(), 120)+'.bak'
    set @path='D:\ClosePeriod'+convert(varchar(10),GetDate(), 120)+'.bak'
    --BACKUP DATABASE [Gefest] TO  DISK = @path  WITH  NOINIT ,  NOUNLOAD ,  NAME = N'Gefest backup',  NOSKIP ,  STATS = 10,  NOFORMAT
    BACKUP DATABASE [Gefest] TO  DISK = @path WITH NOFORMAT, INIT,  NAME = N'Gefest backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10
      --логируем
      insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
      values ('ActionForPeriod', N'Сделали BackUp', GetDate())

    ---препроводим документы на которые не создалась операция
    exec dbo.spRealizationsOfOperations
    --закрываем период
    exec dbo.CloseNowPeriod

    --определим дату начала нового периода

      insert into dbo.ClosePeriodLog (SPName, StepName, DateExec)
      values ('ActionForPeriod', N'Вернулись из CloseNowPeriod', GetDate())

    declare @IdPeriod int
    declare @FirstDate datetime
    set @IdPeriod = dbo.fGetNowPeriod()
    select @FirstDate=DateBegin from period where IdPeriod=@IdPeriod

    --подготовим документы для перепроведения
    select d.iddocument, d.idcontract , d.documentamount, d.documentdate, d.IdTypeDocument, d.IdBatch,
      i.display, d.iduser, i.idindication, d.note, sum(f.factamount) factamount
    into #tmpDoc
    from document d
    left join PD on PD.iddocument=d.iddocument
      and PD.idtypePD=1
    left join indication i on i.idindication=convert(int, PD.Value)
    left join factuse f on f.idindication=i.idindication
    where dbo.DateOnly (d.DocumentDate) >=@FirstDate
      and d.IdTypeDocument=1
    group by d.iddocument, d.idcontract , d.documentamount, d.documentdate, d.IdTypeDocument, d.IdBatch,
      i.display, d.iduser, i.idindication, d.note

      insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
      values ('ActionForPeriod', N'Подготовили документы для перепроведения', GetDate())
    --
    delete operation --, d.idperiod
    from operation  o
      inner join #tmpDoc d on o.iddocument = d.iddocument

    --перенесем документы в новый период
    update document
    set IDPeriod = dbo.fGetNowPeriod()
    from document d
    inner join #tmpDoc td on td.iddocument = d.iddocument

      insert into dbo.ClosePeriodLog (SPName, StepName, DateExec)
      values ('ActionForPeriod', N'Перенесли Документы в новый период', GetDate())

    --и пачки тоже
    update Batch
    set IDPeriod = dbo.fGetNowPeriod()
    from Batch b
    where  dbo.DateOnly (BatchDate)>=@FirstDate

      insert into dbo.ClosePeriodLog (SPName, StepName, DateExec)
      values ('ActionForPeriod', N'Перенесли Пачки в новый период', GetDate())

      --логируем
      insert into dbo.ClosePeriodLog (SPName, StepName, DateExec)
      values ('ActionForPeriod', N'Начинаем перепроведение документов', GetDate())

    --- перепроведем документы
    declare @IDDocument as int
    declare @IDContract as int
    declare @DocumentAmount as float
    declare @DocumentDate as datetime
    declare @IdTypeDocument int
    declare @IdBatch int
    declare @display float
    declare @iduser int
    declare @idindication int
    declare @note varchar(8000)
    declare @factamount float
    declare @err  as  bit
    declare @AmountVDGO as float

    declare Loc cursor for
    select d.iddocument, d.idcontract , d.documentamount, d.documentdate, d.IdTypeDocument, d.IdBatch,
      d.display, d.iduser, d.idindication, d.note, d.factamount
    from #tmpDoc d

    open Loc
      fetch next from loc into @IDDocument, @IDContract, @DocumentAmount, @DocumentDate, @IdTypeDocument, @IdBatch,
          @display, @iduser, @idindication, @note, @factamount
      while @@FETCH_STATUS=0
        begin
          set @err=0
            --28.03.2018 - сначала пересчитаем балансы
            exec spRecalcBalances @IDContract, @IdPeriod
            --потом перепроведем документы Оплата
            exec DocumentPay @IDPeriod, @IDBatch, @IDContract, @IDTypeDocument, @DocumentDate, @DocumentAmount, @Display, 0, @IDUser, @IDDocument, @IDIndication, @FactAmount, '', @note, @err, @AmountVDGO

          if @err<>0
            select 'error'+str(@err)

          FETCH NEXT FROM Loc INTO @IDDocument, @IDContract, @DocumentAmount, @DocumentDate, @IdTypeDocument, @IdBatch,
            @display, @iduser, @idindication, @note, @factamount

        end
    close Loc
    deallocate Loc

      insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
      values ('ActionForPeriod', N'Перепровели документы', GetDate())

  -- Чистка мусора
  --declare @Q as VarChar(1000)
  --set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
  --Exec(@Q)
  --set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
  --Exec(@Q)

      insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
      values ('ActionForPeriod', N'Начали работать с Балансами', GetDate())

    declare @i int
    declare @r int
    set @i=1

    while (@i<15)
    begin
      insert Balance (idContract,idAccounting,idPeriod,AmountBalance,AmountCharge,AmountPay)
      select Document.idContract, 1, Document.idPeriod,0,0,0
      from Document
        inner join operation on operation.iddocument=document.iddocument
        inner join balance on balance.idBalance=operation.idBalance
          and Document.idContract<>Balance.idContract
      left outer join balance BB on BB.idContract=Document.idContract
          and BB.idPeriod=Document.idPeriod
      where BB.idBalance is null

      /* Ищем двойные балансы */
      select count(1) Counter,Balance.idPeriod,Balance.idContract, balance.idaccounting
      into #TempBB
      from Balance
       /* ------ добавлено 070121--------------------
         WHERE IDContract IS NOT NULL
        -------------------------------------------*/
      group by Balance.idPeriod, Balance.idContract, balance.idaccounting
      having count(1)>1
      order by Balance.idContract, Balance.idPeriod

      if ((select count(*) from #TempBB)>0)
        set @r=1
      else
        set @r=0

      insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
      values ('ActionForPeriod', N'Удаляем ненужные дубли в Балансах ' + cast(@i as varchar(3)), GetDate())

      /* Удаляем ненужные дубли */
      declare @idContract1 int
      declare @idPeriod1 int
      declare @idBalance int
      --declare @idContract int
      --declare @idPeriod int
      declare @idOperation int
      declare @idaccounting int
      declare @idaccounting1 int

      declare Curss cursor
      for select Balance.idBalance,Balance.idContract, Balance.idPeriod,Operation.idOperation, Balance.idaccounting
      from #TempBB
      inner join Balance on #TempBB.idContract=Balance.idContract
        and #TempBB.idPeriod=Balance.idPeriod
        and #TempBB.idaccounting=Balance.idaccounting
      left outer join operation on operation.idBalance=Balance.idBalance
      order by #TempBB.idContract, #TempBB.idPeriod, Operation.idOperation DESC

      set @idContract1 = 0
      set @idPeriod1 = 0

      begin tran
      open curss
        fetch next from curss into @idBalance, @idContract, @idPeriod, @idOperation , @idaccounting
        while @@Fetch_Status=0
        begin
          if @idContract1 = @idContract and @idPeriod1 = @idPeriod and @idaccounting1 = @idaccounting and @idOperation is null
          begin
            delete Balance where idBalance=@idBalance
          end
          else
            if @idContract1 = @idContract and @idPeriod1 = @idPeriod and @idaccounting1 = @idaccounting
              begin
                /* Переносим операции на один баланс */
                update operation
                set idBalance=@idBalance
                from operation
                inner join Balance on operation.idbalance=balance.idbalance
                  and balance.idperiod=@idPeriod
                  and balance.idContract=@idContract
              end

          set @idContract1 = @idContract
          set @idPeriod1 = @idPeriod
          set @idaccounting1=@idaccounting

          fetch next from curss into @idBalance, @idContract, @idPeriod, @idOperation, @idaccounting
      end
      close curss
      deallocate curss
      commit tran

      drop Table #TempBB

      insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
      values ('ActionForPeriod', N'Удаляем ненужные дубли в Балансах - Выполнено #' + cast(@i as varchar(3)), GetDate())

      /* Исправление привязки операций */
      update operation
      set idBalance=BB.idBalance
      from Document
      inner join operation on operation.iddocument=document.iddocument
      inner join balance on balance.idBalance=operation.idBalance
        and Document.idContract<>Balance.idContract
      left outer join balance BB on BB.idContract=Document.idContract
        and BB.idPeriod=Document.idPeriod

      if (@r=0)
      begin
        set @i=15
        select 'Двойных балансов нет'
      end
      else
        set @i=@i+1

    end

     ------- если вдруг были пустоты- 1701--------------------------------------------
    DELETE FROM Balance  WHERE IDContract IS NULL AND IDPeriod =  dbo.fGetPredPeriod();
    DELETE FROM BalanceReal  WHERE IDContract IS NULL AND IDPeriod = dbo.fGetPredPeriod();

    DELETE FROM BalanceReal  WHERE IDContract IS NULL AND IDPeriod = dbo.fGetNowPeriod();
    DELETE FROM Balance  WHERE IDContract IS NULL AND IDPeriod =  dbo.fGetNowPeriod();

      insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
      values ('ActionForPeriod', N'Исправление привязки операций в Балансах - Выполнено', GetDate())

      insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
      values ('ActionForPeriod', N'Пересчет балансов пред.периода - начали', GetDate())

    set @IdPeriod = dbo.fGetPredPeriod()
    exec  dbo.spRecalcBalancesOnePeriod  @IdPeriod
    set @IdPeriod = dbo.fGetPredPeriod()
    exec dbo.spRecalcBalancesRealOnePeriod @IdPeriod

      insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
      values ('ActionForPeriod', N'Пересчет балансов пред.периода - Закончили', GetDate())

      insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
      values ('ActionForPeriod', N'Пересчет балансов нового периода - начали', GetDate())

    set @IdPeriod = dbo.fGetNowPeriod()
    exec  dbo.spRecalcBalancesOnePeriod  @IdPeriod
    set @IdPeriod = dbo.fGetNowPeriod()
    exec dbo.spRecalcBalancesRealOnePeriod @IdPeriod

      insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
      values ('ActionForPeriod', N'Пересчет балансов нового периода - Закончили', GetDate())

      insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
      values ('ActionForPeriod', N'Закончили пересчет балансов.', GetDate())

    insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
    values ('ActionForPeriod', N'Наполнение PD - начали', GetDate())

    insert pd ( IDTypePD, IDDocument, Value)
    select 22,ss,2
    from (
      select
        pd.value vv,
        d.iddocument ss
    from document d
      inner join contract c on c.idcontract=d.idcontract
        and idtypedocument=13
        and dbo.fGetLastBalance(@IdPeriod, d.IdContract, 2)>=0
      left join pd on pd.iddocument=d.iddocument and pd.value=2 and idtypepd=22) as dd
    where vv is null

      insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
      values ('ActionForPeriod', N'Наполнение PD - закончили', GetDate())

      --логируем
      insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
      values ('ActionForPeriod', N'Период закрыт', GetDate())

  -- Вернуть базу в рабочий режим
  declare @error bit
  declare @Q as VarChar(1000)
  set @Q=db_name()
  set @Q='gefest'
  exec @error=sp_dboption @Q,'single user', 'false'

  -- Чистка мусора
  declare @Q as VarChar(1000)
  set @Q='BACKUP LOG '+db_name()+' WITH TRUNCATE_ONLY'
  Exec(@Q)
  set @Q='DBCC SHRINKDATABASE ('+db_name()+', 10)'
  Exec(@Q)

  select N'Период закрыт'

  end
  else
    begin
      select N'Ошибка захвата базы монопольно'
    end
end
go

