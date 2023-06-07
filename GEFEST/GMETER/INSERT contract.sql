select ' if not exists (select * from contract  where idcontract='+
        CAST(c.IDContract AS VARCHAR(12)) + ') '+
       ' insert Contract (IDContract, Account, IDAddress, Surname,Patronic,Name,IDPerson) values ('+
        CAST(c.IDContract AS VARCHAR(12)) + ', ' + 
        c.Account + ', ' +	
        CAST(ad.IDAddress AS VARCHAR(12)) +  ', ' + '''' +
		    Surname + ''''+ ', ' + '''' +
		    Patronic + ''''+ ', ' + ''''+
		    Name + '''' + ', ' +
		    CAST(p.IDPerson AS VARCHAR(12)) + ');' 
       
        FROM Contract c
INNER JOIN Person p
  ON c.IDPerson = p.IDPerson
INNER JOIN Address ad
  ON p.IDAddress = ad.IDAddress
  
where idcontract BETWEEN 921786 AND 922464






  ---insert Address (IDAddress, IDTypeAddress, IDPlace, IDStreet, IDHouse, Flat) values
   --INSERT INTO Street (IDStreet,Name, IDPlace, IDTypeStreet) VALUES
   --INSERT INTO Person(IDPerson,IDUser,IDAddress,RNN,isJuridical,Surname,Name,Patronic,IDOwnership,FIOMainBuch,IDClassifier)
  -- INSERT INTO House (IDHouse,IDStreet,IDTypeHouse,HouseNumber,HouseNumberChar,IsEvenSide,IsComfortable)
