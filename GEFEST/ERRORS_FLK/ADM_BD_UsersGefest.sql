select db_name(dbid) as db, spid as idproc, loginame, program_name, status
from  sys.sysprocesses
where db_name(dbid) = N'Gefest';



USE GEFEST

SELECT
    u.name AS UserName,
    u.uid AS USERID,
    l.name AS LoginName,
    u.createdate AS USER_CreateDate,
    u.updatedate AS USER_UpdateDate,
    l.createdate AS LOGIN_CreateDate,
    l.updatedate AS LOGIN_UpdateDate
FROM
    sysusers u
JOIN
    sys.syslogins l ON u.sid = l.sid
WHERE
    u.issqluser = 1 AND -- SQL пользователи
    (u.sid IS NOT NULL AND u.sid <> 0x0) AND -- Не пустой идентификатор безопасности
    u.islogin = 1 -- Это аккаунт входа
ORDER BY
    UserID;



