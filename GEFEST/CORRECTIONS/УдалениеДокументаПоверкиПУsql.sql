-- Удаление документа
-- Поверка ПУ typeDocument = 22
-- Акт проверки работы ПУ typeDocument = 20

declare @IDCONTRACT INT;
declare @ACCOUNT VARCHAR(50);

set @ACCOUNT = '3333038'
set @IDCONTRACT = dbo.fGetIDContractAC(@ACCOUNT);

select *
  from DOCUMENT as D
 where D.IDCONTRACT = @IDCONTRACT 
   and D.IDTYPEDOCUMENT = 22
   and D.DOCUMENTDATE = '2025-05-13';

--- next do it manually

-- IDDOCUMENT 24032443

/*

delete DOCUMENT
 OUTPUT DELETED.*
 where IDDOCUMENT = 25796660;

*/