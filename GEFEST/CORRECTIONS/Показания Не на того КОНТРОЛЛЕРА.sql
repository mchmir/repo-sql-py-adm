﻿use GEFEST
go

/*
142 - WhatsApp
*/
/*
+----------------+--------------+
|IDTypeIndication|Name          |
+----------------+--------------+
|1               |По телефону   |
|2               |От контролера |
|3               |По Акту       |
|4               |От абонента   |
|5               |От слесаря    |
|6               |Личный кабинет|
|7               |Фото          |
|8               |Сообщение     |
+----------------+--------------+
*/

select *
from INDICATION as I
where I.DATEDISPLAY >= '2024-12-01'
  --and I.IDUSER = 62
  and I.IDAGENT = 142
  and I.IDTYPEINDICATION = 3

------------------------
select GM.IDGMETER, C.IDCONTRACT, C.ACCOUNT
from GOBJECT as Gobj
 join CONTRACT C on GOBJ.IDCONTRACT = C.IDCONTRACT
 join GMETER GM on GOBJ.IDGOBJECT = GM.IDGOBJECT
where GM.IDGMETER in (
                      select I.IDGMETER
                      from INDICATION as I
                      where I.DATEDISPLAY >= '2024-12-01'
                        --and I.IDUSER = 62
                        and I.IDAGENT = 142
                        and I.IDTYPEINDICATION = 3);

with IND as (select I.IDGMETER as IDGMETER
             from INDICATION as I
             where I.DATEDISPLAY >= '2024-12-01'
               --and I.IDUSER = 62
               and I.IDAGENT = 142
               and I.IDTYPEINDICATION = 3)
select GM.IDGMETER, C.IDCONTRACT, C.ACCOUNT
from GOBJECT as GOBJ
       join CONTRACT C on GOBJ.IDCONTRACT = C.IDCONTRACT
       join GMETER GM on GOBJ.IDGOBJECT = GM.IDGOBJECT
where GM.IDGMETER in (
  select IDGMETER
  from IND
);

/*
SQL Server 2005 оптимизирует JOIN лучше, чем подзапросы с IN.
Индексы на столбце IDGMETER могут быть эффективно использованы.
- Выполнить анализ с помощью SET STATISTICS IO ON или SET STATISTICS TIME ON
*/

SET STATISTICS IO ON;
SET STATISTICS TIME ON;

with IND as (
  select I.IDGMETER
  from INDICATION as I
  where I.DATEDISPLAY >= '2024-12-01'
    and I.IDAGENT = 142
    and I.IDTYPEINDICATION = 3
)
select GM.IDGMETER, C.IDCONTRACT, C.ACCOUNT
from GOBJECT as GOBJ
       join CONTRACT C on GOBJ.IDCONTRACT = C.IDCONTRACT
       join GMETER GM on GOBJ.IDGOBJECT = GM.IDGOBJECT
       join IND on GM.IDGMETER = IND.IDGMETER;

---- Контроллеры--------
select *
from AGENT as A
where name like '%азаренко%'

----- Пользователи -----------
select S.UID,
       S.STATUS,
       S.NAME,
       S.CREATEDATE,
       S.UPDATEDATE
from SYS.SYSUSERS as S

------------------------------------
begin try

  --Начало транзакции
  begin transaction
    --Пакет №1 Начало


    update INDICATION
    set IDAGENT = null,
        IDTYPEINDICATION = 1 -- по телефону
        -- select * from agent as a where idagent=154;
    output DELETED.IDAGENT, INSERTED.IDAGENT
    --OUTPUT DELETED.* -- старые
    --INSERTED.* -- новые
    where DATEDISPLAY = '2024-09-09'
      and IDUSER = 62
      and IDAGENT = 81
      and IDTYPEINDICATION = 2
    /*
     UPDATE Indication
        SET DATEDISPLAY='03-11-2024'
            OUTPUT DELETED.IDINDICATION, DELETED.DATEDISPLAY, INSERTED.DATEDISPLAY
           --OUTPUT DELETED.* -- старые
           --INSERTED.* -- новые
      WHERE DATEDISPLAY = '03-13-2024'
        AND IDUSER      = 71;
    */
-- 154

    --Пакет №1 Конец
end try
begin catch
  --В случае непредвиденной ошибки
  --Откат транзакции
  rollback transaction

  --Выводим сообщение об ошибке
  select ERROR_NUMBER()  as [Номер ошибки],
         ERROR_MESSAGE() as [Описание ошибки]

  --Прекращаем выполнение инструкции
  return;
end catch --Если все хорошо. Сохраняем все изменения
commit transaction

------------------------------------------------------------------
/* Изменить дату показаний */
begin try

  --Начало транзакции
  begin transaction
    --Пакет №1 Начало
    update INDICATION
    set DATEDISPLAY = '2024-08-19'
    output DELETED.*, INSERTED.*
    --OUTPUT DELETED.* -- старые
    --INSERTED.* -- новые
    where DATEDISPLAY = '2024-08-18'
      and IDUSER = 62
      and IDTYPEINDICATION = 1
   --Пакет №1 Конец
end try
begin catch
  --В случае непредвиденной ошибки
  --Откат транзакции
  rollback transaction

  --Выводим сообщение об ошибке
  select ERROR_NUMBER()  as [Номер ошибки],
         ERROR_MESSAGE() as [Описание ошибки]

  --Прекращаем выполнение инструкции
  return;
end catch --Если все хорошо. Сохраняем все изменения
commit transaction

