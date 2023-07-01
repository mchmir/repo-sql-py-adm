

select DocumentDate, count(*) 
from Document
where IDDocument in ( 
select IDObject
from Changes
where IDSending in (1183,1184,1185,1186) and TableName='Document'
)
group by DocumentDate
order by DocumentDate;

-- 518551 - 518600: 1183
-- 518601	520128: 1184
-- 520129	520502: 1185
-- 520503	521853: 1186

select *
from Changes
where IDSending=1190

update Changes 
set IDSending = 1190 
where IDSending in (1183,1184,1185,1186);
