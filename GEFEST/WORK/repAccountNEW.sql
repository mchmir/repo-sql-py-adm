CREATE PROCEDURE dbo.repAccount (@IdContract int, @dBegin datetime,  @dEnd datetime, @IdAcc int) AS
------------*****************Отчёт карточка абонента***************--------------------------------
--declare @idcontract int
--set @IdContract=(select idcontract from contract where account=1021014)
--declare @dBegin datetime
--declare @dEnd datetime
--set @dBegin='2005-05-01'
--set @dEnd='2009-02-02'
--declare @IDAcc int
--set @IDAcc=0
--
if @IdAcc=1
	begin
		exec repAccountUsl @IdContract, @dBegin,  @dEnd
	end
else
	begin

-- таблица двойных периодов
CREATE TABLE #DTPERIOD (VAL INT)
INSERT INTO #DTPERIOD (VAL) VALUES (242) -- март
INSERT INTO #DTPERIOD (VAL) VALUES (244) -- май

set @dBegin=dbo.dateonly(@dBegin)
set @dEnd=dbo.dateonly(@dEnd)

declare @yearB int
set @yearB =convert(int,year(@dBegin))
declare @MonthB int
set @MonthB =convert(int,month(@dBegin))
declare @yearE int
set @yearE =convert(int,year(@dEnd))
declare @MonthE int
set @MonthE = convert(int,month(@dEnd))

declare @IdGRU int
declare @Account varchar(20)
declare @strMonthB varchar(20)
declare @strMonthE varchar(20)
declare @idperiodNach int
declare @idperiodKonech int
set @idperiodNach=(select case when isnull(idperiod,0)=0 then 1 else idperiod end   from period  where year = year (@dBegin)  and month = month(@dBegin))
select  @idperiodKonech=idperiod from period  where year = year (@dEnd)  and month = month(@dEnd)

print '@idperiodNach'+ convert(varchar(10), @idperiodNach)
print '@idperiodKonech'+convert(varchar(10), @idperiodKonech)

select @Account=Account from Contract where IdContract=@IdContract
set @strMonthB = ltrim(str(@MonthB))
if len(@strMonthB)=1
 	set @strMonthB='0'+@strMonthB
set @strMonthB = ltrim(str(@yearB))+@strMonthB

set @strMonthE = ltrim(str(@MonthE))
if len(@strMonthE)=1
 	set @strMonthE ='0'+@strMonthE
set @strMonthE = ltrim(str(@yearE))+@strMonthE

create table #tmpAccount (IdPeriod int, Period varchar(20), CL varchar(10), Norma float, Koeff float, Tariff float,
BeginBalance float, Amount float, DateDocument DateTime, EndBalance float, Indication  float,
Imya nvarchar(200)collate Cyrillic_General_CI_AS, Account varchar(20), NameDoc varchar(200)collate Cyrillic_General_CI_AS,
Address varchar(200)collate Cyrillic_General_CI_AS, NumberDoc varchar(20),
Gobject varchar(100)collate Cyrillic_General_CI_AS,  GmeterName varchar(50),
GmeterClassA float, GmeterSerialNumber varchar(20), GmeterDateVerify datetime, GmeterDateFabrication datetime,
StatusGmeter int, factuse float, perioddate datetime, typeFactuse int,
typeindication varchar(100)collate Cyrillic_General_CI_AS,typedocument int, AmountNach float, AmountPay float,NumberPhone varchar(150), penyNach float, iddocument int)
--
if @idperiodNach=0

begin
print 'idperiodNach=0'
declare @q varchar(8000)
declare @Z int
if @strMonthE <200504
	set @Z = @strMonthE
else
	set @Z=200504

While convert(int, @strMonthB ) < @Z
begin
set @strMonthB =ltrim(str(@monthB ))
	if len(@strMonthB )=1
		set @strMonthB ='0'+@strMonthB
	set @strMonthB =ltrim(str(@yearB ))+@strMonthB

	set @q='insert #tmpAccount(Period, CL, Norma, Koeff,BeginBalance, Amount, DateDocument, EndBalance, NumberDoc, NameDoc,typedocument)
		select '''+@strMonthB+''',  KOL, VG,KoEF_GS, SUMD,  OP.SUMO, OP.DT_VB,  SUMD-SUMM+LS.SUMO, OP.N_VB,''Оплата'',1 from
		OldAb..LSCHET'+@strMonthB+' LS
		left join OldAb..OPLATA'+@strMonthB+' OP on OP.LS=LS.LS
		where LS.LS='''+@Account +''''
	exec(@q)
	set @q='insert #tmpAccount(Period, CL, Norma, Koeff,BeginBalance,  DateDocument, EndBalance, Indication,  typeindication, NameDoc,typedocument)
		select '''+@strMonthB+''',  KOL, VG,KoEF_GS, SUMD, OP.DT_VB,  SUMD-SUMM+LS.SUMO, OP.GSEND,'''', ''Показания'',9999 from
		OldAb..LSCHET'+@strMonthB+' LS
		left join OldAb..OPLATA'+@strMonthB+' OP on OP.LS=LS.LS
		where LS.LS='''+@Account +''''
	exec(@q)
	set @q='insert #tmpAccount(Period, CL, Norma,Koeff, BeginBalance,  Amount,   typeFactuse, EndBalance, NameDoc,typedocument,factuse)
		select '''+@strMonthB+''',  KOL, VG,KoEF_GS, SUMD, SUMM,  case when isnull(GS,'''')=''*'' then ''1'' else ''0'' end,  SUMD-SUMM+LS.SUMO, ''Начисление'',5,  case when isnull(GS,'''')=''*'' then sum(OP.GSEND-GSBEG) else NULL end from
		OldAb..LSCHET'+@strMonthB+' LS
		left join OldAb..OPLATA'+@strMonthB+' OP on OP.LS=LS.LS
		where LS.LS='''+@Account +''' group by KOL,VG,KoEF_GS, SUMD,SUMM,GS,LS.SUMO'
	exec(@q)
	set @MonthB=@MonthB+1
	if @MonthB>12
	begin
		set @MonthB=1
		set @yearB=@yearB+1
	end

End
end
select @IdGRU=IdGRU from GObject with (nolock) where IdContract=@IdContract
if @idperiodNach=0
	begin
		set @idperiodNach=1
	end

if @idperiodNach>0 and @idperiodNach<=47
	begin
		if @idperiodKonech>=47
			begin
                print '47'
				insert into #tmpAccount (IdPeriod, Period, CL, Norma, Koeff, Tariff, BeginBalance, Amount, DateDocument, EndBalance, Indication,
				Imya, Account, NameDoc,	Address, NumberDoc,	Gobject,  GmeterName, GmeterClassA, GmeterSerialNumber, GmeterDateVerify, GmeterDateFabrication,
				StatusGmeter, factuse, perioddate, typeFactuse,	typeindication, typedocument, AmountNach, AmountPay, NumberPhone, penyNach)
				select * from Gefest2009.dbo.fRepAction (@IdContract, @idperiodNach, 47) order by 1
				set @idperiodNach=48
			end
			else
			begin
				insert into #tmpAccount (IdPeriod, Period, CL, Norma, Koeff, Tariff, BeginBalance, Amount, DateDocument, EndBalance, Indication,
				Imya, Account, NameDoc,	Address, NumberDoc,	Gobject,  GmeterName, GmeterClassA, GmeterSerialNumber, GmeterDateVerify, GmeterDateFabrication,
				StatusGmeter, factuse, perioddate, typeFactuse,	typeindication, typedocument, AmountNach, AmountPay, NumberPhone, penyNach)
				select * from Gefest2009.dbo.fRepAction (@IdContract, @idperiodNach, @idperiodKonech) order by 1
				set @idperiodNach=0
				set @idperiodKonech=0
			end
	end

insert into #tmpAccount (NumberDoc,namedoc,IdPeriod, Period, Amount, DateDocument,typedocument, iddocument)
select case when d.idtypedocument=1 then isnull(d.DocumentNumber,'') else ''end, t.name, p.IdPeriod, case when len(ltrim(str(p.Month)))=1 then ltrim(str(p.Year))+'0'+ltrim(str(p.Month)) else ltrim(str(p.Year))+ltrim(str(p.Month)) end,
case when d.idtypedocument=13 then d.documentamount*0.03 else d.documentamount end ,dbo.dateonly(d.documentdate),d.idtypedocument,d.iddocument
from document d  with (nolock)
inner join period p  with (nolock) on d.idperiod=p.idperiod
and p.IdPeriod<=@idperiodKonech and  p.IdPeriod>=@idperiodNach and d.IdContract=@IdContract and d.idtypedocument not in (13,5,19,20,7,18,14,11,18,12)
inner join typedocument t  with (nolock) on t.idtypedocument=d.idtypedocument
--посчитаем отдельно по юридическим документам
insert into #tmpAccount (NumberDoc,namedoc,IdPeriod, Period, Amount, DateDocument,typedocument,iddocument)
select case when d.idtypedocument=1 then isnull(d.DocumentNumber,'') else ''end, t.name, p.IdPeriod, case when len(ltrim(str(p.Month)))=1 then ltrim(str(p.Year))+'0'+ltrim(str(p.Month)) else ltrim(str(p.Year))+ltrim(str(p.Month)) end,
d.documentamount/100*isnull(replace(pd37.value,',','.'),3)+isnull(pd29.value,0),dbo.dateonly(d.documentdate),d.idtypedocument,d.iddocument
--d.documentamount/100*isnull(replace(pd37.value,',','.'),3),dbo.dateonly(d.documentdate),d.idtypedocument
from document d  with (nolock)
inner join period p  with (nolock) on d.idperiod=p.idperiod
and p.IdPeriod<=@idperiodKonech and  p.IdPeriod>=@idperiodNach and d.IdContract=@IdContract and d.idtypedocument=13
inner join typedocument t  with (nolock) on t.idtypedocument=d.idtypedocument
left join pd pd29 with (nolock) on pd29.iddocument=d.iddocument and pd29.idtypepd=29
left join pd pd37 with (nolock) on pd37.iddocument=d.iddocument and pd37.idtypepd=37

---

insert into #tmpAccount (amount,namedoc,IdPeriod, Period,  DateDocument,indication,typedocument,iddocument)
select case when d.idtypedocument in (7,11,14) then d.documentamount end, t.name, p.IdPeriod, case when len(ltrim(str(p.Month)))=1 then ltrim(str(p.Year))+'0'+ltrim(str(p.Month)) else ltrim(str(p.Year))+ltrim(str(p.Month)) end,
dbo.dateonly(d.documentdate),i.display, d.idtypedocument,d.iddocument
from document d  with (nolock)
inner join period p  with (nolock) on d.idperiod=p.idperiod
and p.IdPeriod<=@idperiodKonech and  p.IdPeriod>=@idperiodNach and d.IdContract=@IdContract and d.idtypedocument in (7,11,14,18,12)
inner join typedocument t  with (nolock) on t.idtypedocument=d.idtypedocument
left join pd   with (nolock) on pd.iddocument=d.iddocument
and pd.idtypepd=1
left join indication i with (nolock) on i.idindication=convert(int,pd.value)

insert into #tmpAccount (namedoc,IdPeriod, Period,DateDocument,indication, typeindication,typedocument)
select distinct 'Показания', p.IdPeriod, case when len(ltrim(str(p.Month)))=1 then ltrim(str(p.Year))+'0'+ltrim(str(p.Month)) else ltrim(str(p.Year))+ltrim(str(p.Month)) end,
 dbo.dateonly(i.datedisplay),i.display, t.name,9999
from indication i  with (nolock)
inner join factuse f with (nolock) on f.idindication=i.idindication
and f.IdPeriod<=@idperiodKonech and  f.IdPeriod>=@idperiodNach
inner join GObject g with (nolock)  on f.idgobject=g.idgobject
and g.IdContract=@IdContract
--inner join operation o  with (nolock) on o.idoperation=f.idoperation
inner join typeindication t with (nolock) on t.idtypeindication=i.idtypeindication
inner join period p  with (nolock) on p.idperiod=f.idperiod
--left join pd   with (nolock) on convert(int,pd.value)=i.idindication
--and pd.idtypepd=1
left join document d  with (nolock) on f.iddocument=d.iddocument
where isnull(idtypedocument,0) not in (7,11,14,18,12)

insert into #tmpAccount (namedoc,IdPeriod, Period, Amount, DateDocument,factuse,typefactuse,typedocument,iddocument)
select case when isnull(f.idtypefu,0)=3 then left(t.name, 6)+'. без пок.' else t.name end, p.IdPeriod, case when len(ltrim(str(p.Month)))=1 then ltrim(str(p.Year))+'0'+ltrim(str(p.Month)) else ltrim(str(p.Year))+ltrim(str(p.Month)) end,
-sum(o.amountoperation),dbo.dateonly(d.documentdate), sum(f.factamount),f.idtypefu, d.idtypedocument,d.iddocument
from document d  with (nolock)
inner join operation o with (nolock) on o.iddocument=d.iddocument
inner join factuse f with (nolock) on f.idoperation=o.idoperation
inner join period p with (nolock) on d.idperiod=p.idperiod
and p.IdPeriod<=@idperiodKonech and p.idperiod NOT IN (SELECT val FROM #DTPeriod) and  p.IdPeriod>=@idperiodNach and d.IdContract=@IdContract and d.idtypedocument in (5)
inner join typedocument t  with (nolock) on t.idtypedocument=d.idtypedocument
group by d.DocumentNumber,t.name,p.IdPeriod,p.Month,p.Year,d.documentdate, f.idtypefu, d.idtypedocument,d.iddocument--o.amountoperation,
---для двойного тарифа
insert into #tmpAccount (namedoc,IdPeriod, Period, Amount, DateDocument,factuse,typefactuse,typedocument,iddocument)
select case when isnull(f.idtypefu,0)=3 then left(t.name, 6)+'. без пок. (НТ)' else t.name end, p.IdPeriod, case when len(ltrim(str(p.Month)))=1 then ltrim(str(p.Year))+'0'+ltrim(str(p.Month)) else ltrim(str(p.Year))+ltrim(str(p.Month)) end,
-sum(o.amountoperation),dbo.dateonly(d.documentdate), sum(f.factamount),f.idtypefu, d.idtypedocument,d.iddocument
from document d  with (nolock)
inner join operation o with (nolock) on o.iddocument=d.iddocument
inner join factuse f with (nolock) on f.idoperation=o.idoperation
inner join period p with (nolock) on d.idperiod=p.idperiod
and p.idperiod IN (SELECT val FROM #DTPeriod) and d.IdContract=@IdContract and d.idtypedocument in (5)
inner join typedocument t  with (nolock) on t.idtypedocument=d.idtypedocument
left join pd with (nolock) on pd.iddocument=d.iddocument and pd.idtypepd=36
where isnull(pd.value,'2')<>'1'
group by d.DocumentNumber,t.name,p.IdPeriod,p.Month,p.Year,d.documentdate, f.idtypefu, d.idtypedocument,d.iddocument--o.amountoperation,
---ДЛЯ НОВОГО ТАРИФА
insert into #tmpAccount (namedoc,IdPeriod, Period, Amount, DateDocument,factuse,typefactuse,typedocument,iddocument)
select case when isnull(f.idtypefu,0)=3 then left(t.name, 6)+'. без пок. (НТ)' else t.name+' (НТ)' end, p.IdPeriod, case when len(ltrim(str(p.Month)))=1 then ltrim(str(p.Year))+'0'+ltrim(str(p.Month)) else ltrim(str(p.Year))+ltrim(str(p.Month)) end,
-sum(o.amountoperation),dbo.dateonly(d.documentdate), sum(f.factamount),f.idtypefu, d.idtypedocument,d.iddocument
from document d  with (nolock)
inner join operation o with (nolock) on o.iddocument=d.iddocument
inner join factuse f with (nolock) on f.idoperation=o.idoperation
inner join period p with (nolock) on d.idperiod=p.idperiod
and p.idperiod IN (SELECT val FROM #DTPeriod) and d.IdContract=@IdContract and d.idtypedocument in (5)
inner join typedocument t  with (nolock) on t.idtypedocument=d.idtypedocument
inner join pd with (nolock) on pd.iddocument=d.iddocument and pd.idtypepd=36 and pd.value='1'
group by d.DocumentNumber,t.name,p.IdPeriod,p.Month,p.Year,d.documentdate, f.idtypefu, d.idtypedocument,d.iddocument--o.amountoperation,


insert into #tmpAccount (IdPeriod, Period, Amount, DateDocument,typedocument,penyNach,namedoc,iddocument)
select  p.IdPeriod, case when len(ltrim(str(p.Month)))=1 then ltrim(str(p.Year))+'0'+ltrim(str(p.Month)) else ltrim(str(p.Year))+ltrim(str(p.Month)) end,
-sum(o.amountoperation),dbo.dateonly(d.documentdate), 5,-sum(o.amountoperation),'Начисление пени,тенге',d.iddocument
from document d  with (nolock)
inner join operation o with (nolock) on o.iddocument=d.iddocument
inner join balance b with (nolock) on b.idbalance=o.idbalance
and b.idaccounting=4
inner join period p with (nolock) on d.idperiod=p.idperiod
and p.IdPeriod<=@idperiodKonech and  p.IdPeriod>=@idperiodNach and d.IdContract=@IdContract and d.idtypedocument in (5)
group by p.IdPeriod,p.Month,p.Year,d.documentdate,d.iddocument  --o.amountoperation,

--update #tmpAccount
--set penyNach=(o.amountoperation)
--from #tmpAccount t
--inner join balance b on b.idperiod=t.idperiod
--and b.idcontract=@idcontract and b.idaccounting=4
--inner join operation o on o.idbalance=b.idbalance
--and t.typedocument=5


insert into #tmpAccount (idperiod,period)
select p.IdPeriod, case when len(ltrim(str(p.Month)))=1 then ltrim(str(p.Year))+'0'+ltrim(str(p.Month)) else ltrim(str(p.Year))+ltrim(str(p.Month)) end
from period p
left join #tmpAccount t on p.idperiod=t.idperiod
where t.idperiod is null

update #tmpAccount
set cl=dbo.fGetCountLives((select top 1 idgobject from gobject with (nolock) where idcontract=@idcontract),idperiod),
norma=isnull(dbo.fGetLastPGNorma(IdPeriod, 1, @IdGRU),0),
koeff=isnull(dbo.fGetLastPGValue(IdPeriod, 1, @IdGRU),0),
endbalance=isnull(dbo.fGetLastBalance(idperiod,@idcontract,0),0),
beginbalance=isnull(dbo.fGetLastBalance(idperiod-1,@idcontract,0),0),
tariff=(select f.value from tariff f  with (nolock) where f.idperiod=t.idperiod)
from #tmpAccount t
where t.idperiod is not null and t.idperiod>47

update #tmpAccount
set TARIFF =
  case
    when IDPERIOD = 242 and NAMEDOC like '%(НТ)%' then 711.54
    when IDPERIOD = 242 and NAMEDOC not like '%(НТ)%' then 605.25
    when IDPERIOD = 244 and NAMEDOC like '%(НТ)%' then 985.02
    when IDPERIOD = 244 and NAMEDOC not like '%(НТ)%' then 711.54
    else TARIFF
  end

update #tmpAccount
set Norma =
  case
    when IDPERIOD = 244 and NAMEDOC like '%(НТ)%' then 2.631
    when IDPERIOD = 244 and NAMEDOC not like '%(НТ)%' then 2.428
    else TARIFF
  end

update #tmpAccount
	set Tariff = 39
from #tmpAccount
where Period < '200211'

update #tmpAccount
	set Tariff = 42
from #tmpAccount
where Period < '200312' and Period  >= '200211'

update #tmpAccount
	set Tariff = 52
from #tmpAccount
where Period > '200311' and Period  < '200401'

update #tmpAccount
	set Tariff = convert (money, 51.55)
from #tmpAccount
where Period >= '200401' and Period  < '200604'

update #tmpAccount
set GmeterName=t.name,
GmeterClassA=t.ClassAccuracy,GmeterSerialNumber=isnull(gm.SerialNumber,'б/н'),
GmeterDateVerify=gm.DateVerify,GmeterDateFabrication=gm.DateFabrication,
StatusGmeter=dbo.fGetStatusPU (@dEnd,gm.idgmeter), Gobject=st.Name
from contract c with (nolock)
inner join GObject g0  with (nolock) on g0.IdContract=c.IdContract
and g0.IDContract = @IdContract
left join GMeter gm  with (nolock) on gm.IdGObject=g0.IdGObject
and dbo.fGetStatusPU (@dEnd,gm.idgmeter)=1
left join StatusGmeter s  on gm.IDStatusGmeter = s.IDStatusGmeter
left join typegmeter t  on t.idtypegmeter=gm.idtypegmeter
left join StatusGobject st  on g0.IDStatusGobject = st.IDStatusGobject

update #tmpAccount

---------------------
--set Imya=isnull(np.NSurname,'') + ' '+ltrim(isnull(np.NName,'')) + ' '+ ltrim(isnull(np.NPatronic,'')) +' ',
--***************************************
SET  Imya = (CASE WHEN np.isJuridical = 0 THEN
  isnull(np.NSurname,'') + ' '+
  ltrim(isnull(np.NName,'')) + ' '+
  ltrim(isnull(np.NPatronic,'')) +' '
  ELSE
  isnull(np.Surname,'') +
  ' '+ltrim(isnull(np.Name,'')) +
  ' '+ ltrim(isnull(np.Patronic,'')) +' '
  end),
Account=c.Account,
NumberPhone=isnull(pp.NumberPhone,''),
Address='ул.'+s.name + ' дом-' + convert( varchar, isnull(h.housenumber,'') )+''+ltrim(convert(varchar,isnull(h.housenumberchar,'')))+ '  кв. '+ ltrim(ad.flat)
from Person np with (nolock)
inner join contract c with (nolock) on np.idperson = c.idperson
inner join person p with (nolock) on c.idperson = p.idperson
inner join address ad with (nolock) on p.idaddress = ad.idaddress
inner join street s with (nolock) on ad.idstreet = s.idstreet
inner join house h with (nolock) on ad.idhouse = h.idhouse
left join Phone pp with (nolock) on pp.idperson=p.idperson
where c.idcontract = @IDContract

update #tmpAccount
set numberdoc=''
where numberdoc is null

update #tmpAccount
set penyNach=-(o.amountoperation)
from #tmpAccount t
inner join balance b on b.idperiod=t.idperiod
and b.idcontract=@idcontract and b.idaccounting=4
inner join operation o on o.idbalance=b.idbalance
and t.typedocument=5


update #tmpAccount
set period=ltrim(left(period,4)+'-'+right(period,2)+'-01')
where len(period)=6
update #tmpAccount
set perioddate=convert(datetime,period)

update #tmpAccount
set AmountNach=tt.am
from #tmpAccount t
inner join (select sum(amount) am, period from #tmpAccount where typedocument in (5,13) group by period) tt on tt.period=t.period

update #tmpAccount
set AmountNach=isnull(AmountNach,0)-isnull(tt.am,0)
from #tmpAccount t
inner join (select sum(amount)am, period from #tmpAccount where typedocument in (7,11,14,3,8) group by period) tt on tt.period=t.period

update #tmpAccount
set Amountpay=tt.am
from #tmpAccount t
inner join (select sum(amount)am, period from #tmpAccount where typedocument in (1) group by period) tt on tt.period=t.period


--уберем документ "Услуги" - кредитные услуги
delete from #tmpAccount where iddocument in (
select d.iddocument from document d
inner join pd on pd.iddocument=d.iddocument
and idtypepd=35 and idtypedocument=24 and d.idperiod>=@idperiodNach and d.idperiod<=@idperiodKonech
and convert(int,pd.value)=21
)
--

if @strMonthB <=200504
begin
	print '1'
	select t.*
	from #tmpAccount t
	-- where Period <=ltrim(left(@strMonthB,4)+'-'+right(@strMonthB,2)+'-01')
	where isnull(IDPeriod,0)<=(select IDPeriod from Period  with (nolock)  where year = @yearE and month = @MonthE)
	order by Period,  DateDocument, typeFactuse
	--
end
else
begin
	select t.*
	from #tmpAccount t
	where IDPeriod<=(select IDPeriod from Period  with (nolock)  where year = @yearE and month = @MonthE)
	and IDPeriod>=(select IDPeriod from Period with (nolock)  where year = @yearB and month = @MonthB)
	order by Period,  DateDocument, typeFactuse
end

drop table #tmpAccount
drop table #DTPERIOD
end
go

