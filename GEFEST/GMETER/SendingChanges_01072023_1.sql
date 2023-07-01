select *
from Sending
-- where IDCorrespondent = 3
order by  IDSending DESC;

update Sending
set DateSending = '30.06.2023'
where IDSending= 1191;  

update Sending 
set IDCorrespondent = 2, NumberSending=198
where IDSending = 1190;

update Sending 
set IDCorrespondent = 2, NumberSending=197
where IDSending = 1189;

select *
from Sending
where NumberSending = 197
order by  IDSending DESC;

select * from 
Document
where IDDocument in ( 
select IDObject
from Changes
where IDSending in (1183,1184,1185,1186, 1187, 1188, 1189, 1190) and TableName='Document'
)
order by DocumentDate;

select DocumentDate, count(*) 
from Document
where IDDocument in ( 
select IDObject
from Changes
where IDSending in (1183,1184,1185,1186) and TableName='Document'
)
group by DocumentDate
order by DocumentDate;




Select * from Agent;

select *
from Document where 
DocumentDate between '01.06.2023' and '07.06.2023';


select *
from Sending
where IDSending in (
	select IDSending
	from Changes
	where TableName='Document' and IDObject between '56444' and '56468'

)
order by  IDSending DESC;


select *
from Changes
order by IDSending DESC;


select *
from Changes
where TableName='Document' and IDObject between '56444' and '56468'	;


select * 
from Correspondent;