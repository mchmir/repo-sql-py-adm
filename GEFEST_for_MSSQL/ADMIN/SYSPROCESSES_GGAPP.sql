SELECT 
       spid as idproc,
       db_name(dbid) as 'Имя БД',
       program_name as 'Программа',
       loginame as 'Имя входа SQL Server',
       hostname AS 'Имя рабочей станции',
       status
  FROM sys.sysprocesses
 WHERE dbid > 0
 ORDER BY 'Имя БД';