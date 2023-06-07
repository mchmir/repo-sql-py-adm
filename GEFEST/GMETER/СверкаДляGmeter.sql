select
  ' if not exists (select * from contract  where idcontract=' +
  CAST(C.IDCONTRACT as varchar(12)) + ') ' +
  ' insert Contract (IDContract, Account, IDAddress, Surname,Patronic,Name,IDPerson) values (' +
  CAST(C.IDCONTRACT as varchar(12)) + ', ' +
  C.ACCOUNT + ', ' +
  CAST(AD.IDADDRESS as varchar(12)) + ', ' + '''' +
  SURNAME + '''' + ', ' + '''' +
  PATRONIC + '''' + ', ' + '''' +
  NAME + '''' + ', ' +
  CAST(P.IDPERSON as varchar(12)) + ');'

from CONTRACT C
inner join PERSON P
  on C.IDPERSON = P.IDPERSON
inner join ADDRESS AD
  on P.IDADDRESS = AD.IDADDRESS

where IDCONTRACT between 921786 and 922464






  ---insert Address (IDAddress, IDTypeAddress, IDPlace, IDStreet, IDHouse, Flat) values
   --INSERT INTO Street (IDStreet,Name, IDPlace, IDTypeStreet) VALUES
   --INSERT INTO Person(IDPerson,IDUser,IDAddress,RNN,isJuridical,Surname,Name,Patronic,IDOwnership,FIOMainBuch,IDClassifier)
  -- INSERT INTO House (IDHouse,IDStreet,IDTypeHouse,HouseNumber,HouseNumberChar,IsEvenSide,IsComfortable)


SELECT
      C.IDCONTRACT,
      P.IDPERSON,
      C.ACCOUNT,
      AD.IDADDRESS,
      SURNAME,
      NAME,
      PATRONIC
 FROM CONTRACT as C
   INNER JOIN PERSON AS P ON C.IDPERSON = P.IDPERSON
   INNER JOIN ADDRESS as AD ON P.IDADDRESS = AD.IDADDRESS  
  

  UPDATE Contract SET Account = '0'+ Account
  WHERE IDContract IN (SELECT c.IDContract FROM Contract c WHERE LEN(c.Account)=6);    