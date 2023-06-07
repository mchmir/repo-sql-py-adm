DECLARE @path AS VARCHAR(200)
SET @path = N'D:\SQL-SOLUTION\Gefest-' + CONVERT(varchar(10), getdate(), 104) + '.bak'
   
   BACKUP DATABASE [Gefest] TO DISK = @path 
   WITH NOFORMAT, INIT,  NAME = N'База данных Gefest', 
   SKIP, NOREWIND, NOUNLOAD,  STATS = 10
   GO