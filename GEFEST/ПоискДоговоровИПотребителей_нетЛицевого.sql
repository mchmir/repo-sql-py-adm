
SELECT
      c.idcontract AS [ID контракта],
      p.idperson AS [ID персоны],
      c.account AS [Лицевой счет], 
      CASE 
        WHEN p.isJuridical = 1 
        THEN p.Surname 
        ELSE isnull(p.Surname, '')+ ' ' + isnull(p.Name, '')+ ' ' + isnull(p.Patronic, '')
      END AS [Ф.И.О.],
      isnull(s.Name, '') + ' ' + isnull(ltrim(str(h.housenumber)), '') + isnull(h.housenumberchar, '') + '-' + isnull(a.flat, '') AS [Адрес],
      g.Countlives AS [Количество проживающих],
      round(dbo.fGetLastBalance(dbo.fGetNowPeriod(), c.IdContract, 0), 2) AS [Баланс],
      CASE 
        WHEN gm.idstatusgmeter = 1 
        THEN tg.name + ', ' + ltrim(CONVERT(nchar (5), tg.ClassAccuracy))
        ELSE 'Отключен'
      END AS [Статус ПУ],
      so.Name AS [Объект учета],
      CASE 
        WHEN isnull(Status, 0)= 1 
        THEN 'Активен'
        ELSE CASE 
                WHEN isnull(Status, 0)= 0 
                THEN 'Не определен'
                ELSE 'Закрыт'
             END
        END AS [Договор],
        CASE 
          WHEN gm.idstatusgmeter = 1 
          THEN LEFT(CONVERT(varchar, gm.DateFabrication, 104), 10)
          ELSE ' '
        END AS [Дата изготовления ПУ],
        p.RNN AS [РНН],
        CASE 
          WHEN gm.idstatusgmeter = 1
          THEN LEFT(CONVERT(varchar, gm.DateVerify, 104), 10)
          ELSE ' '
        END AS [Дата поверки],
        os.Name AS [Соц.положение]
FROM person p
  JOIN address AS a ON a.idaddress = p.idaddress --AND p.Surname LIKE '%%'
  JOIN house AS h ON h.idhouse = a.idhouse
  JOIN street AS s ON s.idstreet = h.idstreet
  LEFT JOIN contract AS c ON c.idperson = p.idperson
  LEFT JOIN gobject AS g ON g.idcontract = c.idcontract
  LEFT JOIN StatusGObject AS so ON so.IDStatusGObject = g.IDStatusGObject
  LEFT JOIN gmeter AS gm ON g.idgobject = gm.idgobject AND gm.idstatusgmeter = 1
  LEFT JOIN TypeGmeter AS tg ON gm.idtypegmeter = tg.idtypegmeter
  LEFT JOIN OwnerShip AS os ON p.IDOwnership = os.IDOwnership
WHERE c.IDContract IS null
ORDER BY CASE 
           WHEN p.isJuridical = 1 
           THEN p.Name
           ELSE isnull(p.Surname, '')+ ' ' + isnull(p.Name, '')+ ' ' + isnull(p.Patronic, '')
         END
         
         
         
         
         
         
         
         
         
         
         
         
         
         