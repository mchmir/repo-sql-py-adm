-- Удаление документа
-- Поверка ПУ typeDocument = 22
-- Акт проверки работы ПУ typeDocument = 20

declare @IDCONTRACT INT;
declare @ACCOUNT VARCHAR(50);

set @ACCOUNT = '1821012'
set @IDCONTRACT = dbo.fGetIDContractAC(@ACCOUNT );

select *
  from DOCUMENT as D
 where D.IDCONTRACT = @IDCONTRACT 
   and D.IDTYPEDOCUMENT = 22
   and D.DOCUMENTDATE = '2024-07-10';

--- next do it manually

-- IDDOCUMENT 24032443

/*

delete DOCUMENT
 OUTPUT DELETED.*
 where IDDOCUMENT = 24698340;

*/