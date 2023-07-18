-- Удаление документа
-- typeDocument = 6, 17
/*
+--------------+-------------------------+
|IDTypeDocument|Name                     |
+--------------+-------------------------+
|6             |Подключение объекта учета|
|17            |Отключение объекта учета |
+--------------+-------------------------+
*/

declare @IDCONTRACT INT;
declare @ACCOUNT VARCHAR(50);

set @ACCOUNT = '1661081'

select @IDCONTRACT = C.IDCONTRACT 
  from CONTRACT as C 
 where C.ACCOUNT = @ACCOUNT;

select *
  from DOCUMENT as D
 where D.IDCONTRACT = @IDCONTRACT 
   and D.IDTYPEDOCUMENT in (6, 17)
   and D.DOCUMENTDATE between '2023-07-01' and '2023-07-14';

--- next in manual 

-- IDDOCUMENT 22925072

/*

delete 
  from DOCUMENT
 where IDDOCUMENT in (23352361, 23352363, 23352362, 23352364);


*/