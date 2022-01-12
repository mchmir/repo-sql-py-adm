Select @@SERVERNAME as [Server\Instance];
Select @@VERSION as SQLServerVersion;
Select @@ServiceName AS ServiceInstance;
Select DB_NAME() AS CurrentDB_Name;
EXEC sp_helpdb;
EXEC sp_Helpfile;

