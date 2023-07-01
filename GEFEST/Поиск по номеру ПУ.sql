declare @SERIALNUMBER as VARCHAR(50);

set @SERIALNUMBER = '1782045';
set @SERIALNUMBER = @SERIALNUMBER + '%';

select C.IDCONTRACT,
       P.IDPERSON,
       C.ACCOUNT,
       case
         when P.ISJURIDICAL = 1 then P.SURNAME
         else isnull(P.SURNAME, '') + ' ' + isnull(P.NAME, '') + '
' + isnull(P.PATRONIC, '') end                                                                               FIO,
       isnull(S.NAME, '') + ' ' + isnull(ltrim(str(H.HOUSENUMBER)), '') + isnull(H.HOUSENUMBERCHAR, '') + '-' +
       isnull(A.FLAT, '')                                                                                    ADDRESS,
       G.COUNTLIVES,
       round(DBO.FGETLASTBALANCE(DBO.FGETNOWPERIOD(), C.IDCONTRACT, 0), 2)                                   BALANCE,
       case
         when GM.IDSTATUSGMETER = 1 then TG.NAME + ', ' + ltrim(convert(nchar(5), TG.CLASSACCURACY))
         else 'Отключен' end                                                                                 PU,
       SO.NAME                                                                                               OU,
       case
         when isnull(STATUS, 0) = 1 then 'Активен'
         else case
                when isnull(STATUS, 0) = 0 then 'Не определен'
                else 'Закрыт' end end                                                                        CONTRACT,
       case when GM.IDSTATUSGMETER = 1 then left(convert(varchar, GM.DATEFABRICATION, 104), 10) else ' ' end DATE,
       P.RNN,
       case
         when
           GM.IDSTATUSGMETER = 1 then left(convert(varchar, GM.DATEVERIFY, 104), 10)
         else ' ' end                                                                                        DATEVERIFY,
       OS.NAME
from PERSON P
       inner join ADDRESS A on
  A.IDADDRESS = P.IDADDRESS
       inner join HOUSE H on H.IDHOUSE = A.IDHOUSE
       inner join STREET S on S.IDSTREET = H.IDSTREET
       inner join CONTRACT C on
  C.IDPERSON = P.IDPERSON
       inner join GOBJECT G on G.IDCONTRACT = C.IDCONTRACT
       inner join STATUSGOBJECT SO on SO.IDSTATUSGOBJECT = G.IDSTATUSGOBJECT
       inner join
     GMETER GM on G.IDGOBJECT = GM.IDGOBJECT and SERIALNUMBER like @SERIALNUMBER
       inner join TYPEGMETER TG on GM.IDTYPEGMETER = TG.IDTYPEGMETER
       left join OWNERSHIP OS on
  P.IDOWNERSHIP = OS.IDOWNERSHIP
order by C.ACCOUNT;