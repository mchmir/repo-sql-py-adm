-- Переменные для хранения данных
declare @IDCONTRACT as INT;
declare @IDDOCUMENT as INT;
declare @COUNTDOCUMENTS as INT;
declare @CONFIRMATION CHAR(1);  -- Переменная для подтверждения

-- Подсчет количества документов, которые будут затронуты
select @COUNTDOCUMENTS = COUNT(*)
from DOCUMENT D
where D.IDTYPEDOCUMENT = 22
  and D.DOCUMENTDATE >= '2024-10-01';

-- Сообщение о количестве записей, которые будут удалены
print 'Количество записей для удаления: ' + CAST(@COUNTDOCUMENTS as NVARCHAR);

-- Здесь используется условие для продолжения
-- Для защита от случайного запуска N
-- Для запуска скрипта установить на Y
set @CONFIRMATION = 'Y';

if @CONFIRMATION = 'Y'
  begin
    -- Курсор для удаления данных
    declare CURSIDDOC cursor local fast_forward for
      select D.IDDOCUMENT, D.IDCONTRACT
      from DOCUMENT D
      where D.IDTYPEDOCUMENT = 22
        and D.DOCUMENTDATE >= '2024-10-01';

    -- Открытие курсора
    open CURSIDDOC;

    -- Чтение первой строки
    fetch next from CURSIDDOC into @IDDOCUMENT, @IDCONTRACT;

    -- Удаление записей, если есть данные
    while @@FETCH_STATUS = 0
      begin
        -- Удаление из связанных таблиц
        delete from OPERATION where IDDOCUMENT = @IDDOCUMENT;
        delete from PD where IDDOCUMENT = @IDDOCUMENT;
        delete from DOCUMENT where IDDOCUMENT = @IDDOCUMENT;

        -- Чтение следующей строки
        fetch next from CURSIDDOC into @IDDOCUMENT, @IDCONTRACT;
      end;

    -- Закрытие и освобождение курсора
    close CURSIDDOC;
    deallocate CURSIDDOC;

    print 'Удаление завершено успешно.';
  end
else
  begin
    print 'Операция удаления отменена.';
  end;


--select * from Sending with (nolock) where idCorrespondent=2 and numbersending=238 order by IDSending
