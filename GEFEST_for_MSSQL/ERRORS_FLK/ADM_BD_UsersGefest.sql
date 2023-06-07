select db_name(dbid) as db, spid as idproc, loginame, program_name, status
from  sys.sysprocesses
where db_name(dbid) = N'Gefest'