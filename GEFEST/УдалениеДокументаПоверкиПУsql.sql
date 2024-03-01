﻿-- Удаление документа
-- Поверка ПУ typeDocument = 22
-- Акт проверки работы ПУ typeDocument = 20

declare @IDCONTRACT INT;
declare @ACCOUNT VARCHAR(50);

set @ACCOUNT = '1101037'

select @IDCONTRACT = C.IDCONTRACT 
  from CONTRACT as C 
 where C.ACCOUNT = @ACCOUNT;

select *
  from DOCUMENT as D
 where D.IDCONTRACT = @IDCONTRACT 
   and D.IDTYPEDOCUMENT = 22
   and D.DOCUMENTDATE = '2024-01-15';

--- next in manual 

-- IDDOCUMENT 24032443

/*

delete DOCUMENT
 OUTPUT DELETED.*
 where IDDOCUMENT = 24032443;

*/