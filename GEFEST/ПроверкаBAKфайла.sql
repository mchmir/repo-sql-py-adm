RESTORE DATABASE Gmeter
FROM DISK='C:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\Backup\27-10-2022.bak';

RESTORE VERIFYONLY FROM DISK ='C:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\Backup\27-10-2022.bak';

RESTORE HEADERONLY FROM DISK = 'C:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\Backup\27-10-2022.bak';

SELECT @@VERSION;


RESTORE FILELISTONLY FROM DISK = 'C:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\Backup\27-10-2022.bak';


