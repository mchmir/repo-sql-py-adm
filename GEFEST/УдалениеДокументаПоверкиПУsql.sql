-- Удаление документа Поверка ПУ
-- typeDocument = 22
declare @IDCONTRACT INT;
declare @ACCOUNT VARCHAR(50);

set @ACCOUNT = '2672058'

select @IDCONTRACT = C.IDCONTRACT 
  from CONTRACT as C 
 where C.ACCOUNT = @ACCOUNT;

select *
  from DOCUMENT as D
 where D.IDCONTRACT = @IDCONTRACT 
   and D.IDTYPEDOCUMENT = 22
   and D.DOCUMENTDATE = '2023-05-04';

--- next in manual 

-- IDDOCUMENT 22925072

/*

delete 
  from DOCUMENT
 where IDDOCUMENT = 23156439;

*/