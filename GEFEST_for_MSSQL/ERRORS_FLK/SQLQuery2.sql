use Gefest
go


select * 
from period p
where p.Month = 10 and p.Year = 2022;

-------------------------------------------------

SELECT 
       spid as idproc,
       db_name(dbid) as 'Имя БД',
       program_name as 'Программа',
       loginame as 'имя входа SQL Server',
       hostname AS 'Имя рабочей станции',
       status
FROM sys.sysprocesses
WHERE dbid > 0
order by 'Имя БД';
-----------------------------------------------

select top 5 * 
from Period p
order by p.DateBegin Desc;

select count(*)
from AAAERC7;

select top 1000 *
from AAAERC7;

