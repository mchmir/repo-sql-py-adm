-- Удаление документа
-- typeDocument = 12, 18
/*
+--------------+-------------------------+
|IDTypeDocument|Name                     |
+--------------+-------------------------+
|12            |Подключение прибора учета|
|18            |Отключение прибора учета |
+--------------+-------------------------+

*/

declare @IDCONTRACT INT;
declare @ACCOUNT VARCHAR(50);

set @ACCOUNT = '3171172'

select @IDCONTRACT = C.IDCONTRACT
  from CONTRACT as C
 where C.ACCOUNT = @ACCOUNT;

select *
  from DOCUMENT as D
 where D.IDCONTRACT = @IDCONTRACT
   and D.IDTYPEDOCUMENT in (12, 18)
   and D.DOCUMENTDATE between '2024-10-01' and '2024-10-11';

--- next in manual

-- IDDOCUMENT 22925072

/*

delete
  from DOCUMENT
 where IDDOCUMENT in (24988508, 24988507);


*/