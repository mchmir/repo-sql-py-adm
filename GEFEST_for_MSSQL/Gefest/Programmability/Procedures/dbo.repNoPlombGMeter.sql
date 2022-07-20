SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO




CREATE procedure [dbo].[repNoPlombGMeter] (@idperiod int,@dBegin as datetime,@dEnd as datetime) as

select ct.account,tg.name+', '+ltrim(convert(nchar (5),tg.ClassAccuracy))+', №'+gm.SerialNumber as gminfo, ind.datedisplay as datedisplay, gm.memo from 
contract ct
inner join gobject go on ct.idcontract=go.idcontract
	and go.IDStatusGObject=1
inner join gmeter gm on gm.idgobject=go.idgobject
	and gm.IDStatusGMeter=1 and isnull(gm.PlombNumber1,'')='' and isnull(gm.PlombNumber2,'')=''
--inner join (select max (idindication) idind, max(datedisplay) datedisplay, idgmeter from indication  
inner join (select idindication idind, datedisplay datedisplay, idgmeter from indication  
where  IDTypeIndication in (3,5) and IDAgent in (97,100,94,93,123,137) and datedisplay>=@dBegin and datedisplay<=@dEnd ) ind on ind.idgmeter=gm.idgmeter
left join TypeGmeter tg on gm.idtypegmeter=tg.idtypegmeter
order by ct.account--ind.datedisplay
--union all
--select ct.account,tg.name+', '+ltrim(convert(nchar (5),tg.ClassAccuracy))+', №'+gm.SerialNumber, convert(varchar,ind.datedisplay, 104) datedisplay from 
--contract ct
--inner join gobject go on ct.idcontract=go.idcontract
--	and go.IDStatusGObject=1
--inner join gmeter gm on gm.idgobject=go.idgobject
--	and gm.IDStatusGMeter=1 and isnull(gm.PlombNumber1,'')='' and isnull(gm.PlombNumber2,'')=''
--inner join (select max (idindication) idind, max(datedisplay) datedisplay, idgmeter from indication  
--where IDTypeIndication=5 and IDAgent in (97,100,94,93,123) and datedisplay>='2014-07-01'
--group by idgmeter) ind on ind.idgmeter=gm.idgmeter
--left join TypeGmeter tg on gm.idtypegmeter=tg.idtypegmeter
--order by 3



GO