declare @ACCOUNT as VARCHAR(50);

set @ACCOUNT = '1782045';
set @ACCOUNT = @ACCOUNT + '%';

select C.IDCONTRACT,
       P.IDPERSON,
       C.ACCOUNT,
       case
         when P.ISJURIDICAL = 1 then P.SURNAME
         else isnull(P.SURNAME, '') + ' ' + isnull(P.NAME, '') + ' ' + isnull(P.PATRONIC, '')
         end                                                                                                 FIO,
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
       case when GM.IDSTATUSGMETER = 1 then left(convert(varchar, GM.DATEFABRICATION, 104), 10) else ' ' end DATE_MADE_PU,
       P.RNN,
       case
         when
           GM.IDSTATUSGMETER = 1 then left(convert(varchar, GM.DATEVERIFY, 104), 10)
         else ' ' end                                                                                        DATE_VERIFY_PU,
       OS.NAME
from PERSON as P
       inner join CONTRACT as C on
  C.IDPERSON = P.IDPERSON and C.ACCOUNT like @ACCOUNT
       left join GOBJECT as G on G.IDCONTRACT = C.IDCONTRACT
       left join STATUSGOBJECT as SO on
  SO.IDSTATUSGOBJECT = G.IDSTATUSGOBJECT
       left join ADDRESS as A on A.IDADDRESS = G.IDADDRESS
       left join HOUSE as H on H.IDHOUSE = A.IDHOUSE
       left join STREET as S on
  S.IDSTREET = H.IDSTREET
       left join GMETER as GM on G.IDGOBJECT = GM.IDGOBJECT and GM.IDSTATUSGMETER = 1
       left join TYPEGMETER as TG on GM.IDTYPEGMETER = TG.IDTYPEGMETER
       left join OWNERSHIP as OS on P.IDOWNERSHIP = OS.IDOWNERSHIP
order by ACCOUNT;