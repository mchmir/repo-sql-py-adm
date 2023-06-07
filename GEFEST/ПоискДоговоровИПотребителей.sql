DECLARE @Account VARCHAR(50);

SET @Account = 2101050;

SELECT
      c.idcontract AS [ID контракта],
      p.idperson   AS [ID персоны],
      c.account    AS [Лицевой счет], 
      CASE 
        WHEN p.isJuridical = 1 
          THEN p.surname 
        ELSE isnull(p.surname, '') + ' ' + isnull(p.name, '') + ' ' + isnull(p.patronic, '')
      END          AS [Ф.И.О.],
      isnull(s.name, '') + ' ' + isnull(ltrim(str(h.housenumber)), '') +
      isnull(h.housenumberchar, '') +
      '-' + isnull(a.flat, '') AS [Адрес],
      g.countlives AS [Количество проживающих],
      round(dbo.fGetLastBalance(dbo.fGetNowPeriod(), c.idcontract, 0), 2) AS [Баланс],
      CASE 
        WHEN gm.idstatusgmeter = 1 
          THEN tg.name + ', ' + ltrim(CONVERT(nchar (5), tg.classaccuracy))
        ELSE 'Отключен'
      END         AS [Статус ПУ],
      so.name     AS [Объект учета],
      CASE 
        WHEN isnull(Status, 0)= 1 
          THEN 'Активен'
        ELSE CASE 
                WHEN isnull(Status, 0)= 0 
                THEN 'Не определен'
                ELSE 'Закрыт'
             END
        END       AS [Договор],
        CASE 
          WHEN gm.idstatusgmeter = 1 
            THEN LEFT(CONVERT(varchar, gm.datefabrication, 104), 10)
          ELSE ' '
        END       AS [Дата изготовления ПУ],
        p.rnn     AS [РНН],
        CASE 
          WHEN gm.idstatusgmeter = 1
            THEN LEFT(CONVERT(varchar, gm.dateverify, 104), 10)
          ELSE ' '
        END       AS [Дата поверки],
        os.name   AS [Соц.положение]
FROM Person p
  JOIN Address AS a ON a.idaddress = p.idaddress 
  JOIN House   AS h ON h.idhouse   = a.idhouse
  JOIN Street  AS s ON s.idstreet  = h.idstreet
  LEFT JOIN Contract      AS c  ON c.idperson         = p.idperson
  LEFT JOIN Gobject       AS g  ON g.idcontract       = c.idcontract
  LEFT JOIN StatusGObject AS so ON so.idstatusgobject = g.idstatusgobject
  LEFT JOIN Gmeter        AS gm ON g.idgobject        = gm.idgobject AND gm.idstatusgmeter = 1
  LEFT JOIN TypeGmeter    AS tg ON gm.idtypegmeter    = tg.idtypegmeter
  LEFT JOIN OwnerShip     AS os ON p.idownership      = os.idownership
WHERE c.account = @Account
ORDER BY CASE 
           WHEN p.isJuridical = 1 
             THEN p.Name
           ELSE isnull(p.Surname, '') + ' ' + isnull(p.Name, '') + ' ' + isnull(p.Patronic, '')
         END
;
         
         
         
         
         
         
         
         
         
         
         
         
         
         