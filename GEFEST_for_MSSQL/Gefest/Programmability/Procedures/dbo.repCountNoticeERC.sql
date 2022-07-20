SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
CREATE procedure [dbo].[repCountNoticeERC] (@IdGRU int, @IdPeriod int, @IDContract int) as 


if @IdPeriod=122
begin
exec dbo.repGMeterForCountNoticeNewTariff @IdGRU, @IdPeriod, @IDContract
end
ELSE
begin
declare @IdPeriodPred int
select top 1 @IdPeriodPred=dbo.fGetPredPeriodVariable(@IdPeriod)
declare @dBegin datetime
declare @dEnd datetime
declare @IDGmeter int
declare @GmeterCount int
set @dBegin= dbo.fGetDatePeriod(@IdPeriod,1)
set @dEnd= dbo.fGetDatePeriod(@IdPeriod,0)

select @IdGRU IdGRU,@IdPeriod IdPeriod,c.Account, c.IdContract, gm.idgmeter, dbo.fGetStatusPU (@dEnd,gm.idgmeter) StatusGMeter,
dbo.fGetCountLives(g.idgobject ,@IdPeriod) CL,
convert(float, 0.00) as BalanceBegin,
convert(float, 0.00) as SummOplat,
convert(float, 0.00) as SummaNach,
convert(float, 0.00) as BalanceEnd,
convert(float,0.00) as FactAmount,
convert(float, dbo.fGetLastPGValue(@IDPeriod, 1, @idgru)) as Tariff,
convert(float, dbo.fGetLastPGNorma(@IDPeriod, 1, @idgru)) as Norma,
isnull(dbo.fLastIndicationIDPeriodGet(gm.IdGMeter,@idperiod),0) GetIndication, --конечные показание
convert(datetime, null) DateGetIndication, --дата конечных показаний
isnull(dbo.fLastIndicationIDPeriod(gm.IdGMeter, @IdPeriodPred),0) LastIndication, --начальные показание
convert(datetime, null) DateLastIndication ,
isnull(tt.name,'')+'('+isnull(ltrim(tt.ClassAccuracy),'')+')' Marka,
isnull(gm.Serialnumber,'б/н') Serialnumber,
convert(int, 1) as FactUse, @dBegin as Date, dateadd(yy, tt.servicelife, case when gm.dateverify='1800-01-01' then gm.datefabrication else gm.dateverify end) dateverify,
convert(int,1) as TypeCloseGmeter,
  --------
ISNULL(p.Surname,'') + ' ' + ISNULL(p.Name,'') +' ' + ISNULL(P.Patronic,'') AS FIO,
  (SELECT YEAR FROM PERIOD WHERE idPeriod = @IdPeriod) AS Year,
  (SELECT Month FROM PERIOD WHERE idPeriod = @IdPeriod) AS Month,
  --ISNULL(s.Name,'')+' '+ISNULL(CAST(h.HouseNumber AS VARCHAR(10)),'')+ISNULL(h.HouseNumberChar,'')+','+ISNULL(a.Flat,'') AS Address
  ISNULL(s.Name,'') AS Street,
  ISNULL(CAST(h.HouseNumber AS VARCHAR(10)),'') AS HouseNumber,
  ISNULL(h.HouseNumberChar,'') AS HouseNumberChar,
  ISNULL(a.Flat,'') AS Flat
  --------

into #tmpChetIzvehePU
from contract c with (nolock) 
inner join GObject g  with (nolock) on g.IdContract=c.IdContract
	and g.IdGRU=@IdGru 	
left join GMeter gm  with (nolock) on gm.IdGObject=g.IdGObject
	and dbo.fGetStatusPU (@dEnd,gm.idgmeter)=1 
left join typegmeter tt with (nolock)  on gm.idtypegmeter=tt.idtypegmeter
left join Document d  with (nolock) on d.idcontract=c.idcontract
and  d.IdPeriod=@IdPeriod and (d.idtypedocument=1  or d.idtypedocument=3)
  -----
INNER JOIN Person p WITH (NOLOCK) ON p.IDPerson = c.IDPerson --AND p.isJuridical = 0 -- только физические лица
LEFT JOIN dbo.Address a WITH (NOLOCK)  ON a.IDAddress = g.IDAddress
LEFT JOIN dbo.Street s WITH (NOLOCK) ON s.IDStreet = a.IDStreet
LEFT JOIN dbo.House h WITH (NOLOCK) ON h.IDHouse = a.IDHouse
  -------
where c.idcontract=@IDContract
 group by c.IdContract, c.Account, g.IDGobject, 
 gm.IdGMeter,  tt.name, tt.ClassAccuracy,
 gm.Serialnumber,tt.servicelife,gm.dateverify,gm.DateFabrication
  -----
  , ISNULL(p.Surname,'') + ' ' + ISNULL(p.Name,'') +' ' + ISNULL(P.Patronic,'')
  , ISNULL(s.Name,''), ISNULL(CAST(h.HouseNumber AS VARCHAR(10)),''), ISNULL(h.HouseNumberChar,''),ISNULL(a.Flat,'') 
  -----

set @idgmeter=(select top 1 idgmeter from #tmpChetIzvehePU)
set @gmetercount=(select count(ISNULL(idgmeter,0)) from #tmpChetIzvehePU)
set @idgmeter=(select top 1 idgmeter from #tmpChetIzvehePU)

	update #tmpChetIzvehePU
	set SummaNach=convert(float,qq.DA),FactUse=isnull(ff.idtypefu,1)
	from #tmpChetIzvehePU c
	inner join (select -sum(ISNULL(o.amountoperation,0)) DA, f.IdContract, f.iddocument
	from Document f  with (nolock)
	inner join operation o  with (nolock) on o.iddocument=f.iddocument
	inner join balance b with (nolock) on b.idbalance=o.idbalance
	and b.idaccounting<>4
	where f.IdPeriod=@IdPeriod and f.idcontract=@IDContract
		and f.idtypedocument=5 
	group by f.IdContract,f.iddocument) qq on qq.IdContract=c.IdContract
	inner join operation o  with (nolock) on o.iddocument=qq.iddocument
	inner join factuse ff with (nolock) on o.idoperation=ff.idoperation

update #tmpChetIzvehePU
set Tariff=Tariff*(select top 1 value from Tariff with (nolock) where idperiod=@idperiod),
Norma=Norma*(select top 1 value from Tariff with (nolock) where idperiod=@idperiod)

if isnull(@idgmeter,0)=0 and @gmetercount<2 
begin
	update #tmpChetIzvehePU
	set FactAmount=qq.FA
	from #tmpChetIzvehePU c
	inner join (select sum(ISNULL(f.Factamount,0)) fA, g.IdContract
	from Factuse f  with (nolock)
	inner join Gobject g  with (nolock) on g.idgobject=f.idgobject
	inner join operation o with (nolock) on o.idoperation=f.idoperation
	inner join document d  with (nolock) on d.iddocument=o.iddocument
	and f.IdPeriod=@IdPeriod and d.idtypedocument=5
	
	group by g.IdContract) qq on qq.IdContract=c.IdContract
end 
	else
begin
	update #tmpChetIzvehePU
	set FactAmount=qq.FA
	from #tmpChetIzvehePU c
	inner join (select sum(ISNULL(f.Factamount,0)) fA, g.IdContract, gm.idgmeter
	from Factuse f  with (nolock)
	inner join Gobject g  with (nolock) on g.idgobject=f.idgobject
	left join GMeter gm  with (nolock) on gm.IdGObject=g.IdGObject
	inner join operation o with (nolock) on o.idoperation=f.idoperation
	inner join document d  with (nolock) on d.iddocument=o.iddocument
	and f.IdPeriod=@IdPeriod and d.idtypedocument=5 and g.IdContract=@IDContract
	group by g.IdContract,gm.idgmeter) qq on qq.IdContract=c.IdContract and qq.idgmeter=c.idgmeter
end 





update #tmpChetIzvehePU
set DateGetIndication=(select top 1 convert(datetime,qq.datedisplay,20) from  indication qq  with (nolock) 
where qq.display=c.GetIndication and c.idgmeter=qq.idgmeter order by datedisplay desc)
from #tmpChetIzvehePU c

update #tmpChetIzvehePU
set TypeCloseGmeter=(
select top 1 pd.value from document d 
inner join pd on pd.iddocument=d.iddocument
and idtypepd=33 and d.idcontract=c.idcontract and d.idcontract=@IDContract
order by d.documentdate desc) from #tmpChetIzvehePU c where c.statusGmeter=2

update #tmpChetIzvehePU
set TypeCloseGmeter=999 from #tmpChetIzvehePU
inner join (select idcontract from gobject where idstatusgobject=2 and idcontract=@IDContract) obj on obj.idcontract=#tmpChetIzvehePU.idcontract


update #tmpChetIzvehePU
set TypeCloseGmeter=1
where TypeCloseGmeter is null

update #tmpChetIzvehePU
set dategetindication=null where getindication=0

update #tmpChetIzvehePU
set DateLastIndication=(select top 1 convert(datetime,qq.datedisplay,20) from  indication qq  with (nolock) 
where qq.display=c.LastIndication and c.idgmeter=qq.idgmeter and  qq.datedisplay<isnull(c.DateGetIndication,GetDate()) order by datedisplay desc)
from #tmpChetIzvehePU c

select ac.idaccounting, ac.nname, @IDContract idcontract,
isnull( dbo.fGetLastBalance(@IdPeriodPred, @IDContract, ac.idaccounting),0) as BalanceBegin,
convert(float, 0.00) as SummOplat,
convert(float, 0.00) as SummaNach,
isnull( dbo.fGetLastBalance(@IdPeriod, @IDContract, ac.idaccounting),0) as BalanceEnd
into #tmpAcc
from accounting ac

update #tmpAcc
set SummOplat=convert(float,qq.AO)
from #tmpAcc c
inner join (select sum(ISNULL(o.amountoperation,0)) AO, b.idaccounting
from Document f  with (nolock) 
inner join operation o with (nolock) on f.iddocument=o.iddocument
and f.IdPeriod=@IdPeriod and f.idtypedocument=1 
inner join balance b with (nolock) on o.idbalance=b.idbalance
where f.idcontract = @IDContract
group by b.idaccounting) qq on qq.idaccounting=c.idaccounting

update #tmpAcc
set SummaNach=convert(float,qq.AO)*-1
from #tmpAcc c
inner join (select sum(ISNULL(o.amountoperation,0)) AO, b.idaccounting
from Document f  with (nolock) 
inner join operation o with (nolock) on f.iddocument=o.iddocument
and f.IdPeriod=@IdPeriod and f.idtypedocument<>1
inner join balance b with (nolock) on o.idbalance=b.idbalance
where f.idcontract = @IDContract
group by b.idaccounting) qq on qq.idaccounting=c.idaccounting


SET @IdPeriodPred = dbo.fGetPredPeriodVariable(@IdPeriod)

--#############################################################################
--INSERT INTO AAAERC -- для версии номер 3
SELECT 
  cip.Account,
  cip.Year,
  cip.Month,
  cip.FIO,
  ------------
  CASE 
  WHEN cip.LastIndication>0 AND ISNULL(cip.GetIndication,0) = 0 THEN 'По норме:'
  WHEN ISNULL(cip.LastIndication,0)=0 AND ISNULL(cip.GetIndication,0)=0 THEN 'По норме:'
  ELSE  'ПУ:№'+ CAST(cip.Serialnumber AS VARCHAR) + ','+ CAST(DAY(cip.dateverify) AS VARCHAR)+'.'+CAST(Month(cip.dateverify) AS VARCHAR)+'.'+CAST(YEAR(cip.dateverify) AS VARCHAR)
  END AS PersUstr,
  ------------
  (SELECT ROUND(a.BalanceBegin,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 1) AS SaldoNachGaz,
  (SELECT ROUND(a.BalanceBegin,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 4) AS SaldoNachPenya,
  (SELECT ROUND(a.BalanceBegin,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 2) AS SaldoNachSudVzysk,
  (SELECT ROUND(a.BalanceBegin,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 3) AS SaldoNachSudIzder,
   convert(float, 0.00) AS SaldoNachSHtraf, --(SELECT ROUND(a.BalanceBegin,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 5) AS SaldoNachSHtraf,
  (SELECT ROUND(a.BalanceBegin,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 6) AS SaldoNachDopUslug,
  (SELECT ROUND(SUM(ISNULL(a.BalanceBegin,0)),2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract ) AS SaldoNachItog,
  --------
  (SELECT ROUND(a.Summoplat ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 1) AS OplachenoGaz,
  (SELECT ROUND(a.Summoplat ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 4) AS OplachenoPenya,
  (SELECT ROUND(a.Summoplat ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 2) AS OplachenoSudVzysk,
  (SELECT ROUND(a.Summoplat ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 3) AS OplachenoSudIzder,
   convert(float, 0.00) AS OplachenoSHtraf, --(SELECT ROUND(a.Summoplat ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 5) AS OplachenoSHtraf,
  (SELECT ROUND(a.Summoplat ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 6) AS OplachenoDopUslug,
  (SELECT ROUND(SUM(ISNULL(a.Summoplat,0)),2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract ) AS OplachenoItog,
  --------
  ROUND(cip.LastIndication,2) AS PredPokaz,
  ROUND(cip.GetIndication, 2) AS TekPokaz,
  CASE 
  WHEN cip.LastIndication>0 AND ISNULL(cip.GetIndication,0) = 0 THEN ROUND(cip.Norma,2)
  WHEN ISNULL(cip.LastIndication,0)=0 AND ISNULL(cip.GetIndication,0)=0 THEN ROUND(cip.Norma,2)
  ELSE ROUND(cip.Tariff, 2)
  END AS Cena,
  ----------
  CASE 
  WHEN cip.LastIndication>0 AND ISNULL(cip.GetIndication,0) = 0 THEN CAST(cip.CL AS CHAR(2))+' чел.'
  WHEN ISNULL(cip.LastIndication,0)=0 AND ISNULL(cip.GetIndication,0)=0 THEN CAST(cip.CL AS CHAR(2))+' чел.'
  ELSE REPLACE(CAST(cip.FactAmount AS VARCHAR) + ' m3','.',',')
  END AS Kolvo,
  ----------
  ROUND(cip.SummaNach, 2) AS SummaNach,
  ----------
  (SELECT ROUND(a.SummaNach ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 1) AS SummNachislGaz,
  (SELECT ROUND(a.SummaNach ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 4) AS SummNachislPenya,
  (SELECT ROUND(a.SummaNach ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 2) AS SummNachislVzysk,
  (SELECT ROUND(a.SummaNach ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 3) AS SummNachislIzder,
   convert(float, 0.00) AS SummNachislSHtraf, --(SELECT ROUND(a.SummaNach ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 5) AS SummNachislSHtraf,
  (SELECT ROUND(a.SummaNach ,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 6) AS SummNachislDopUslug,
  (SELECT ROUND(SUM(ISNULL(a.SummaNach,0)),2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract ) AS SummNachislItog,
  ----------
  (SELECT ROUND(a.BalanceEnd,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 1) AS SaldoKonecGaz,
  (SELECT ROUND(a.BalanceEnd,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 4) AS SaldoKonecPenya,
  (SELECT ROUND(a.BalanceEnd,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 2) AS SaldoKonecVzysk,
  (SELECT ROUND(a.BalanceEnd,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 3) AS SaldoKonecSudIzder,
   convert(float, 0.00) AS SaldoKonecSHtraf, --(SELECT ROUND(a.BalanceEnd,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 5) AS SaldoKonecSHtraf,
  (SELECT ROUND(a.BalanceEnd,2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract AND a.IDAccounting = 6) AS SaldoKonecDopUslug,
  (SELECT ROUND(SUM(ISNULL(a.BalanceEnd,0)),2) FROM #tmpAcc a WHERE a.idcontract = cip.IDContract ) AS SaldoKonecItog,
  -----
  CASE 
  WHEN cip.dateverify < GetDATE() THEN 'Срочно сдайте ПУ на поверку!'
  WHEN cip.TypeCloseGmeter = 2 THEN  'Срочно сдайте ПУ на поверку!'
  WHEN cip.TypeCloseGmeter = 14 THEN 'ПУ не опломбирован!'
  ELSE 'ЕК тексеру/поверка ПУ '+CAST(DAY(cip.dateverify) AS VARCHAR)+'.'+CAST(Month(cip.dateverify) AS VARCHAR)+'.'+CAST(YEAR(cip.dateverify) AS VARCHAR)
  END AS Primechanie,
  cip.Street AS Street,
  cip.HouseNumber AS HouseNumber,
  cip.HouseNumberChar AS HouseNumberChar,
  cip.Flat AS Flat
 -- cip.TypeCloseGmeter
  -----

  FROM #tmpChetIzvehePU cip
 left join (select idcontract 
            from gobject  with (nolock) 
              -- пул ЛС где ОУ=отключен И текущий баланс по счету >-1
              where idstatusgobject=2 and idgru=@idgru and isnull( dbo.fGetLastBalance (@IdPeriod, IdContract, 0),0)>-1 
             -- И исключаем счета которые отключались в закрываемом периоде
              AND IDContract NOT IN 
                (SELECT d.idContract FROM  Document d 
                      WHERE d.IDPeriod =@IdPeriod  AND d.IDTypeDocument = 17
             --- или у которых есть изменения в балансе за 2 последних периода
                 UNION
                 SELECT g.IDContract  FROM GObject g  
                      WHERE g.IDStatusGObject = 2 and dbo.fGetLastBalanceTotal(@IdPeriod, g.IDContract) <> dbo.fGetLastBalanceTotal(@IdPeriodPred, g.IDContract)
             --- условие на январь 2021--
                 UNION
                 SELECT c.idContract FROM Contract c WHERE c.Account IN (3511045,2831056,1674012,3163030,0632015,3632027,1651072,3651064,2751017,2653080,0522046,1482029,0611043,1802050,1585013,1641074,1943068,1763016,1921046,3941056,1171166,1434078,1161020,3652052,0521043,1231255,2491052,2001055,0401022,2371098,2361083,1934072,1751027,1301002,2041238,1713060,1171037,1753080,3691004,3351077,1051276,2721015,2295022,3652058,2841037,2302054,2441062,3651049,2772012,1781034,3001004,2772081,2831111,1931047,2498038,1051032)
                 )
            ) c on c.idcontract=cip.idcontract
WHERE c.idcontract is NULL

--select t.* 
--from #tmpChetIzvehe t
--left join (select idcontract from gobject  with (nolock)  where idstatusgobject=2 and idgru=@idgru and isnull( dbo.fGetLastBalance (@IdPeriod, IdContract, 0),0)>-1) c on c.idcontract=t.idcontract
--where c.idcontract is null
--order by account

--#############################################################################


drop table #tmpChetIzvehePU
drop table #tmpAcc
--drop table #tmpAcc2

end


GO