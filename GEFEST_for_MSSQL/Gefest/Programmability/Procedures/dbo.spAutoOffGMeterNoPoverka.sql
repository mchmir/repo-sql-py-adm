SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO










--Процедура по начислению пени
CREATE PROCEDURE [dbo].[spAutoOffGMeterNoPoverka] AS
declare @idperiod int
declare @idprevperiod int
declare @days int
declare @stavka float
declare @dated datetime
declare @Ref float
declare @DateEnd datetime
declare @IDContract int
declare @IDGMeter int
--логируем
--insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
--values ('spAutoOffGMeterNoPoverka','Начали',GetDate())
--
set @idperiod=dbo.fGetNowPeriod()
set @idprevperiod=dbo.fGetPredPeriod()
--
set @dated=dbo.fGetDatePeriod(@idperiod, 1)
--
declare  @WorkTable table(idcontract int,idgmeter int, dateverify datetime, ind float, cntlives int)
insert @WorkTable (idcontract,idgmeter, dateverify, ind, cntlives)
select ct.idcontract,gm.idgmeter,dateadd(yy, t.servicelife, case when gm.dateverify='1800-01-01' then gm.datefabrication else gm.dateverify end),
dbo.fLastIndicationIDPeriodGet(gm.idgmeter, @idprevperiod),dbo.fGetCountLives(go.idgobject,@idperiod)
from gmeter gm
inner join gobject go with (nolock) on go.idgobject=gm.idgobject
inner join contract ct with (nolock) on go.idcontract=ct.idcontract
inner join typegmeter t with (nolock) on t.idtypegmeter=gm.idtypegmeter
where gm.idstatusgmeter=1
and dateadd(yy, t.servicelife, case when gm.dateverify='1800-01-01' then gm.datefabrication else gm.dateverify end)<@dated
and isnull(dbo.fLastIndicationIDPeriodGet(gm.idgmeter, @idprevperiod),0)>0
and isnull(dbo.fGetCountLives(go.idgobject,@idperiod),0)>0


DECLARE curGM CURSOR
READ_ONLY
FOR select distinct idcontract, idgmeter from @WorkTable

declare @iddocument int

set @idcontract=0
set @idgmeter=0
OPEN curGM

FETCH NEXT FROM curGM INTO @idcontract,@idgmeter
WHILE (@@fetch_status <> -1)
BEGIN
	IF (@@fetch_status <> -2)
	BEGIN
			insert document (IDContract, IDPeriod, IDTypeDocument, DocumentNumber, DocumentDate, Note)
			values (@idcontract, @IdPeriod,  18, 'Отключение ПУ', @dated, 'нет поверки')
			set @iddocument=scope_identity()
            
            update gmeter set IDStatusGMeter=2 where idgmeter=@idgmeter		
			
			insert into PD	(IDTypePD,IDDocument,Value)
			values (7,@iddocument,str(@idgmeter))

			insert into PD	(IDTypePD,IDDocument,Value)
			values (16,@iddocument,'88')	

			insert into PD	(IDTypePD,IDDocument,Value)
			values (33,@iddocument,'2')	
		
	END
	FETCH NEXT FROM curGM INTO @idcontract,@idgmeter
END

CLOSE curGM
DEALLOCATE curGM
--логируем
--insert into dbo.ClosePeriodLog (SPName,StepName,DateExec)
--values ('spAutoOffGMeterNoPoverka','Закончили',GetDate())
--


















GO