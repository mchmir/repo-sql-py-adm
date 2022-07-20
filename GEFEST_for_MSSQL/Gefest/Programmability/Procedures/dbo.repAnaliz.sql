SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO










CREATE PROCEDURE [dbo].[repAnaliz] ( @year datetime, @path varchar(1000)) AS
------------*****************Отчёт за код с разбивкой по физ. юрд. лицам***************--------------------------------
--declare @path varchar(1000)
--set @path='D:\WORK\Gefest\Report\repAnaliz.xls'
--declare @year datetime
--set @year='2007-05-05'

select @year=convert(datetime,str(year(@year))+'-01-01',20)


declare @idperiod int
select @idperiod=idperiod from period where datebegin=@year

if (@idperiod is null) goto rrr

declare @end datetime
set @end=dbo.fGetDatePeriod(@IdPeriod,0)

create table #Analiz (AbonentovSPU1 int, AbonentovSPUURD1 int,AbonentovBEZPU1 int, AbonentovBEZPUURD1 int, CHISLAbonentovBEZPU1 int, CHISLAbonentovBEZPUURD1 int, CHISLAbonentovSPU1 int, CHISLAbonentovSPUURD1 int,
 OBEMBEZPU1 float, OBEMBEZPUURD1 float, OBEMSPU1 float, OBEMSPUURD1 float, OplataZaGAZ1 float,OplataZaGAZURD1 float,
AbonentovSPU2 int, AbonentovSPUURD2 int,AbonentovBEZPU2 int, AbonentovBEZPUURD2 int, CHISLAbonentovBEZPU2 int, CHISLAbonentovBEZPUURD2 int, CHISLAbonentovSPU2 int, CHISLAbonentovSPUURD2 int,
 OBEMBEZPU2 float, OBEMBEZPUURD2 float, OBEMSPU2 float, OBEMSPUURD2 float, OplataZaGAZ2 float,OplataZaGAZURD2 float,
AbonentovSPU3 int, AbonentovSPUURD3 int,AbonentovBEZPU3 int, AbonentovBEZPUURD3 int, CHISLAbonentovBEZPU3 int, CHISLAbonentovBEZPUURD3 int, CHISLAbonentovSPU3 int, CHISLAbonentovSPUURD3 int,
 OBEMBEZPU3 float, OBEMBEZPUURD3 float, OBEMSPU3 float, OBEMSPUURD3 float, OplataZaGAZ3 float,OplataZaGAZURD3 float,
AbonentovSPU4 int, AbonentovSPUURD4 int,AbonentovBEZPU4 int, AbonentovBEZPUURD4 int, CHISLAbonentovBEZPU4 int, CHISLAbonentovBEZPUURD4 int, CHISLAbonentovSPU4 int, CHISLAbonentovSPUURD4 int,
 OBEMBEZPU4 float, OBEMBEZPUURD4 float, OBEMSPU4 float, OBEMSPUURD4 float, OplataZaGAZ4 float,OplataZaGAZURD4 float,
AbonentovSPU5 int, AbonentovSPUURD5 int,AbonentovBEZPU5 int, AbonentovBEZPUURD5 int, CHISLAbonentovBEZPU5 int, CHISLAbonentovBEZPUURD5 int, CHISLAbonentovSPU5 int, CHISLAbonentovSPUURD5 int,
 OBEMBEZPU5 float, OBEMBEZPUURD5 float, OBEMSPU5 float, OBEMSPUURD5 float, OplataZaGAZ5 float,OplataZaGAZURD5 float,
AbonentovSPU6 int, AbonentovSPUURD6 int,AbonentovBEZPU6 int, AbonentovBEZPUURD6 int, CHISLAbonentovBEZPU6 int, CHISLAbonentovBEZPUURD6 int, CHISLAbonentovSPU6 int, CHISLAbonentovSPUURD6 int,
 OBEMBEZPU6 float, OBEMBEZPUURD6 float, OBEMSPU6 float, OBEMSPUURD6 float, OplataZaGAZ6 float,OplataZaGAZURD6 float,
AbonentovSPU7 int, AbonentovSPUURD7 int,AbonentovBEZPU7 int, AbonentovBEZPUURD7 int, CHISLAbonentovBEZPU7 int, CHISLAbonentovBEZPUURD7 int, CHISLAbonentovSPU7 int, CHISLAbonentovSPUURD7 int,
 OBEMBEZPU7 float, OBEMBEZPUURD7 float, OBEMSPU7 float, OBEMSPUURD7 float, OplataZaGAZ7 float,OplataZaGAZURD7 float,
AbonentovSPU8 int, AbonentovSPUURD8 int,AbonentovBEZPU8 int, AbonentovBEZPUURD8 int, CHISLAbonentovBEZPU8 int, CHISLAbonentovBEZPUURD8 int, CHISLAbonentovSPU8 int, CHISLAbonentovSPUURD8 int,
 OBEMBEZPU8 float, OBEMBEZPUURD8 float, OBEMSPU8 float, OBEMSPUURD8 float, OplataZaGAZ8 float,OplataZaGAZURD8 float,
AbonentovSPU9 int, AbonentovSPUURD9 int,AbonentovBEZPU9 int, AbonentovBEZPUURD9 int, CHISLAbonentovBEZPU9 int, CHISLAbonentovBEZPUURD9 int, CHISLAbonentovSPU9 int, CHISLAbonentovSPUURD9 int,
 OBEMBEZPU9 float, OBEMBEZPUURD9 float, OBEMSPU9 float, OBEMSPUURD9 float, OplataZaGAZ9 float,OplataZaGAZURD9 float,
AbonentovSPU10 int, AbonentovSPUURD10 int,AbonentovBEZPU10 int, AbonentovBEZPUURD10 int, CHISLAbonentovBEZPU10 int, CHISLAbonentovBEZPUURD10 int, CHISLAbonentovSPU10 int, CHISLAbonentovSPUURD10 int,
 OBEMBEZPU10 float, OBEMBEZPUURD10 float, OBEMSPU10 float, OBEMSPUURD10 float, OplataZaGAZ10 float,OplataZaGAZURD10 float,
AbonentovSPU11 int, AbonentovSPUURD11 int,AbonentovBEZPU11 int, AbonentovBEZPUURD11 int, CHISLAbonentovBEZPU11 int, CHISLAbonentovBEZPUURD11 int, CHISLAbonentovSPU11 int, CHISLAbonentovSPUURD11 int,
 OBEMBEZPU11 float, OBEMBEZPUURD11 float, OBEMSPU11 float, OBEMSPUURD11 float, OplataZaGAZ11 float,OplataZaGAZURD11 float,
AbonentovSPU12 int, AbonentovSPUURD12 int,AbonentovBEZPU12 int, AbonentovBEZPUURD12 int, CHISLAbonentovBEZPU12 int, CHISLAbonentovBEZPUURD12 int, CHISLAbonentovSPU12 int, CHISLAbonentovSPUURD12 int,
 OBEMBEZPU12 float, OBEMBEZPUURD12 float, OBEMSPU12 float, OBEMSPUURD12 float, OplataZaGAZ12 float,OplataZaGAZURD12 float)

insert #analiz (AbonentovSPU1)
select count(0) from contract c  with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1 

update #analiz 
set AbonentovSPUURD1=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovBEZPU1=(select  count(0) from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-AbonentovSPU1

update #analiz 
set AbonentovBEZPUURD1=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-AbonentovSPUURD1



update #analiz 
set CHISLAbonentovSPU1=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q  with (nolock) on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovSPUURD1=(select  sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovBEZPU1=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-CHISLAbonentovSPU1

update #analiz 
set CHISLAbonentovBEZPUURD1=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-CHISLAbonentovSPUURD1

update #analiz 
set OBEMBEZPU1=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)


update #analiz 
set OBEMBEZPUURD1=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)

update #analiz 
set OBEMSPU1=(select sum(f.factamount)
from contract c
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f with (nolock)  on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OBEMSPUURD1=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OplataZaGAZ1=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod 
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)-(select isnull(sum(o.amountoperation),0) dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)

update #analiz 
set OplataZaGAZURD1=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)


----------1

set @idperiod=(select top 1 idperiod from period where idperiod>@idperiod order by idperiod)
if (@idperiod is null) goto rrr


set @end=dbo.fGetDatePeriod(@IdPeriod,0)

update #analiz set AbonentovSPU2=(select count(0) from contract c  with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovSPUURD2=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovBEZPU2=(select  count(0) from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-AbonentovSPU2

update #analiz 
set AbonentovBEZPUURD2=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-AbonentovSPUURD2


update #analiz 
set CHISLAbonentovSPU2=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q  with (nolock) on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovSPUURD2=(select  sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovBEZPU2=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-CHISLAbonentovSPU2

update #analiz 
set CHISLAbonentovBEZPUURD2=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-CHISLAbonentovSPUURD2


update #analiz 
set OBEMBEZPU2=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)


update #analiz 
set OBEMBEZPUURD2=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)

update #analiz 
set OBEMSPU2=(select sum(f.factamount)
from contract c
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f with (nolock)  on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OBEMSPUURD2=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OplataZaGAZ2=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)


update #analiz 
set OplataZaGAZURD2=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)



set @idperiod=(select top 1 idperiod from period where idperiod>@idperiod order by idperiod)
if (@idperiod is null) goto rrr

set @end=dbo.fGetDatePeriod(@IdPeriod,0)

update #analiz set AbonentovSPU3=(select count(0) from contract c  with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovSPUURD3=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovBEZPU3=(select  count(0) from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-AbonentovSPU3

update #analiz 
set AbonentovBEZPUURD3=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-AbonentovSPUURD3


update #analiz 
set CHISLAbonentovSPU3=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q  with (nolock) on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovSPUURD3=(select  sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovBEZPU3=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-CHISLAbonentovSPU3

update #analiz 
set CHISLAbonentovBEZPUURD3=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-CHISLAbonentovSPUURD3


update #analiz 
set OBEMBEZPU3=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)


update #analiz 
set OBEMBEZPUURD3=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)

update #analiz 
set OBEMSPU3=(select sum(f.factamount)
from contract c
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f with (nolock)  on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OBEMSPUURD3=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OplataZaGAZ3=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)


update #analiz 
set OplataZaGAZURD3=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)



set @idperiod=(select top 1 idperiod from period where idperiod>@idperiod order by idperiod)
if (@idperiod is null) goto rrr

set @end=dbo.fGetDatePeriod(@IdPeriod,0)


update #analiz set AbonentovSPU4=(select count(0) from contract c  with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovSPUURD4=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovBEZPU4=(select  count(0) from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-AbonentovSPU4

update #analiz 
set AbonentovBEZPUURD4=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-AbonentovSPUURD4


update #analiz 
set CHISLAbonentovSPU4=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q  with (nolock) on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovSPUURD4=(select  sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovBEZPU4=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-CHISLAbonentovSPU4

update #analiz 
set CHISLAbonentovBEZPUURD4=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-CHISLAbonentovSPUURD4


update #analiz 
set OBEMBEZPU4=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)


update #analiz 
set OBEMBEZPUURD4=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)

update #analiz 
set OBEMSPU4=(select sum(f.factamount)
from contract c
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f with (nolock)  on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OBEMSPUURD4=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OplataZaGAZ4=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)


update #analiz 
set OplataZaGAZURD4=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6) and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)




set @idperiod=(select top 1 idperiod from period where idperiod>@idperiod order by idperiod)
if (@idperiod is null) goto rrr

set @end=dbo.fGetDatePeriod(@IdPeriod,0)


update #analiz set AbonentovSPU5=(select count(0) from contract c  with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovSPUURD5=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovBEZPU5=(select  count(0) from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-AbonentovSPU5

update #analiz 
set AbonentovBEZPUURD5=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-AbonentovSPUURD5


update #analiz 
set CHISLAbonentovSPU5=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q  with (nolock) on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovSPUURD5=(select  sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovBEZPU5=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-CHISLAbonentovSPU5

update #analiz 
set CHISLAbonentovBEZPUURD5=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-CHISLAbonentovSPUURD5


update #analiz 
set OBEMBEZPU5=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)


update #analiz 
set OBEMBEZPUURD5=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)

update #analiz 
set OBEMSPU5=(select sum(f.factamount)
from contract c
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f with (nolock)  on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OBEMSPUURD5=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OplataZaGAZ5=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)


update #analiz 
set OplataZaGAZURD5=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)

set @idperiod=(select top 1 idperiod from period where idperiod>@idperiod order by idperiod)
if (@idperiod is null) goto rrr

set @end=dbo.fGetDatePeriod(@IdPeriod,0)
update #analiz set AbonentovSPU6=(select count(0) from contract c  with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovSPUURD6=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovBEZPU6=(select  count(0) from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-AbonentovSPU6

update #analiz 
set AbonentovBEZPUURD6=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-AbonentovSPUURD6


update #analiz 
set CHISLAbonentovSPU6=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q  with (nolock) on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovSPUURD6=(select  sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovBEZPU6=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-CHISLAbonentovSPU6

update #analiz 
set CHISLAbonentovBEZPUURD6=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-CHISLAbonentovSPUURD6


update #analiz 
set OBEMBEZPU6=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)


update #analiz 
set OBEMBEZPUURD6=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)

update #analiz 
set OBEMSPU6=(select sum(f.factamount)
from contract c
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f with (nolock)  on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OBEMSPUURD6=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OplataZaGAZ6=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod 
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)


update #analiz 
set OplataZaGAZURD6=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod 
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)




set @idperiod=(select top 1 idperiod from period where idperiod>@idperiod order by idperiod)
if (@idperiod is null) goto rrr

set @end=dbo.fGetDatePeriod(@IdPeriod,0)
update #analiz set AbonentovSPU7=(select count(0) from contract c  with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovSPUURD7=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovBEZPU7=(select  count(0) from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-AbonentovSPU7

update #analiz 
set AbonentovBEZPUURD7=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-AbonentovSPUURD7


update #analiz 
set CHISLAbonentovSPU7=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q  with (nolock) on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovSPUURD7=(select  sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovBEZPU7=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-CHISLAbonentovSPU7

update #analiz 
set CHISLAbonentovBEZPUURD7=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-CHISLAbonentovSPUURD7


update #analiz 
set OBEMBEZPU7=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)


update #analiz 
set OBEMBEZPUURD7=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)

update #analiz 
set OBEMSPU7=(select sum(f.factamount)
from contract c
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f with (nolock)  on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OBEMSPUURD7=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OplataZaGAZ7=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)


update #analiz 
set OplataZaGAZURD7=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)




set @idperiod=(select top 1 idperiod from period where idperiod>@idperiod order by idperiod)
if (@idperiod is null) goto rrr

set @end=dbo.fGetDatePeriod(@IdPeriod,0)
update #analiz set AbonentovSPU8=(select count(0) from contract c  with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovSPUURD8=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovBEZPU8=(select  count(0) from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-AbonentovSPU8

update #analiz 
set AbonentovBEZPUURD8=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-AbonentovSPUURD8


update #analiz 
set CHISLAbonentovSPU8=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q  with (nolock) on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovSPUURD8=(select  sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovBEZPU8=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-CHISLAbonentovSPU8

update #analiz 
set CHISLAbonentovBEZPUURD8=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-CHISLAbonentovSPUURD8


update #analiz 
set OBEMBEZPU8=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)


update #analiz 
set OBEMBEZPUURD8=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)

update #analiz 
set OBEMSPU8=(select sum(f.factamount)
from contract c
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f with (nolock)  on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OBEMSPUURD8=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OplataZaGAZ8=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)


update #analiz 
set OplataZaGAZURD8=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)




set @idperiod=(select top 1 idperiod from period where idperiod>@idperiod order by idperiod)
if (@idperiod is null) goto rrr

set @end=dbo.fGetDatePeriod(@IdPeriod,0)
update #analiz set AbonentovSPU9=(select count(0) from contract c  with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovSPUURD9=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovBEZPU9=(select  count(0) from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-AbonentovSPU9

update #analiz 
set AbonentovBEZPUURD9=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-AbonentovSPUURD9


update #analiz 
set CHISLAbonentovSPU9=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q  with (nolock) on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovSPUURD9=(select  sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovBEZPU9=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-CHISLAbonentovSPU9

update #analiz 
set CHISLAbonentovBEZPUURD9=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-CHISLAbonentovSPUURD9


update #analiz 
set OBEMBEZPU9=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)


update #analiz 
set OBEMBEZPUURD9=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)

update #analiz 
set OBEMSPU9=(select sum(f.factamount)
from contract c
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f with (nolock)  on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OBEMSPUURD9=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OplataZaGAZ9=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)


update #analiz 
set OplataZaGAZURD9=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)




set @idperiod=(select top 1 idperiod from period where idperiod>@idperiod order by idperiod)
if (@idperiod is null) goto rrr

set @end=dbo.fGetDatePeriod(@IdPeriod,0)
update #analiz set AbonentovSPU10=(select count(0) from contract c  with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovSPUURD10=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovBEZPU10=(select  count(0) from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-AbonentovSPU10

update #analiz 
set AbonentovBEZPUURD10=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-AbonentovSPUURD10


update #analiz 
set CHISLAbonentovSPU10=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q  with (nolock) on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovSPUURD10=(select  sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovBEZPU10=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-CHISLAbonentovSPU10

update #analiz 
set CHISLAbonentovBEZPUURD10=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-CHISLAbonentovSPUURD10


update #analiz 
set OBEMBEZPU10=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)


update #analiz 
set OBEMBEZPUURD10=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)

update #analiz 
set OBEMSPU10=(select sum(f.factamount)
from contract c
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f with (nolock)  on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OBEMSPUURD10=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OplataZaGAZ10=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)


update #analiz 
set OplataZaGAZURD10=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)




set @idperiod=(select top 1 idperiod from period where idperiod>@idperiod order by idperiod)
if (@idperiod is null) goto rrr

set @end=dbo.fGetDatePeriod(@IdPeriod,0)
update #analiz set AbonentovSPU11=(select count(0) from contract c  with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovSPUURD11=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovBEZPU11=(select  count(0) from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-AbonentovSPU11

update #analiz 
set AbonentovBEZPUURD11=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-AbonentovSPUURD11


update #analiz 
set CHISLAbonentovSPU11=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q  with (nolock) on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovSPUURD11=(select  sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovBEZPU11=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-CHISLAbonentovSPU11

update #analiz 
set CHISLAbonentovBEZPUURD11=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-CHISLAbonentovSPUURD11


update #analiz 
set OBEMBEZPU11=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)


update #analiz 
set OBEMBEZPUURD11=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)

update #analiz 
set OBEMSPU11=(select sum(f.factamount)
from contract c
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f with (nolock)  on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OBEMSPUURD11=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OplataZaGAZ11=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)


update #analiz 
set OplataZaGAZURD11=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)




set @idperiod=(select top 1 idperiod from period where idperiod>@idperiod order by idperiod)
if (@idperiod is null) goto rrr

set @end=dbo.fGetDatePeriod(@IdPeriod,0)

update #analiz set AbonentovSPU12=(select count(0) from contract c  with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovSPUURD12=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set AbonentovBEZPU12=(select  count(0) from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-AbonentovSPU12

update #analiz 
set AbonentovBEZPUURD12=(select count(0) from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-AbonentovSPUURD12


update #analiz 
set CHISLAbonentovSPU12=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join gmeter q  with (nolock) on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovSPUURD12=(select  sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join gmeter q with (nolock)  on q.idgobject=g.idgobject
and dbo.fGetStatusPU (@end,q.idgmeter)=1)

update #analiz 
set CHISLAbonentovBEZPU12=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
)-CHISLAbonentovSPU12

update #analiz 
set CHISLAbonentovBEZPUURD12=(select sum(dbo.fGetCountLives(g.idgobject ,@IdPeriod))
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
)-CHISLAbonentovSPUURD12


update #analiz 
set OBEMBEZPU12=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)


update #analiz 
set OBEMBEZPUURD12=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=2)

update #analiz 
set OBEMSPU12=(select sum(f.factamount)
from contract c
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0
inner join factuse f with (nolock)  on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OBEMSPUURD12=(select sum(f.factamount)
from contract c with (nolock) 
inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1
inner join factuse f  with (nolock) on f.idgobject=g.idgobject
and f.idperiod=@idperiod and f.idtypefu=1)

update #analiz 
set OplataZaGAZ12=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g  with (nolock) on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=0)


update #analiz 
set OplataZaGAZURD12=(select sum(documentamount)
from contract c with (nolock) 
--inner join gobject g with (nolock)  on g.idcontract=c.idcontract
--and dbo.fGetStatusGobject (g.idgobject,@idperiod)=1
inner join document d  with (nolock) on d.idcontract=c.idcontract
and d.idtypedocument=1 and d.idperiod=@idperiod
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)-(select isnull(sum(o.amountoperation),0)  dd
from operation o  with (nolock) 
inner join balance b on b.idbalance=o.idbalance
	and b.idperiod=@idperiod
	and (b.idaccounting=3 or b.idaccounting=4 or b.idaccounting=6)  and o.idtypeoperation=1
inner join contract c  with (nolock) on b.idcontract=c.idcontract
and dbo.fGetIsJuridical (c.idperson,@idperiod)=1)


--select * from #Analiz

--declare @path varchar(1000)
--set @path='D:\WORK\Gefest\Report\repAnaliz.xls'
rrr:
declare @SQL varchar(8000)
set @SQL='insert OpenDataSource(''Microsoft.Jet.OLEDB.4.0'',''Data Source="' + @path +
'";User ID=Admin;Password=;Extended properties=Excel 5.0'')...[Лист2$]
(c1, c2, c3, c4,  c5, c6, c7,c8,c9, c10,c11,c12,c13,c14,c15,c16,c17,c18,c19,
c20,c21,c22,c23,c24,c25,c26,c27,c28,c29,c30,c31,c32,c33,c34,c35,c36,c37,c38,
c39,c40, c41, c42, c43, c44, c45, c46,  c47, c48, c49,c50,c51, c52,c53,c54,
c55,c56,c57,c58,c59,c60,c61,c62,c63,c64,c65,c66,c67,c68,c69,c70,c71,c72,c73,
c74,c75,c76,c77,c78,c79,c80,c81,c82, c83, c84, c85, c86, c87, c88, c89, c90, 
c91, c92,c93,c94, c95,c96,c97,c98,c99,c100,c101,c102,c103,c104,c105,c106,c107,
c108,c109,c110,c111,c112,c113,c114,c115,c116,c117,c118,c119,c120,c121,c122,c123,
c124,c125, c126, c127, c128, c129, c130, c131,  c132, c133, c134,c135,c136, c137,
c138,c139,c140,c141,c142,c143,c144,c145,c146,c147,c148,c149,c150,c151,c152,c153,
c154,c155,c156,c157,c158,c159,c160,c161,c162,c163,c164,c165,c166,c167,c168)
select 
AbonentovSPU1, AbonentovSPUURD1,AbonentovBEZPU1, AbonentovBEZPUURD1, 
CHISLAbonentovBEZPU1, CHISLAbonentovBEZPUURD1, CHISLAbonentovSPU1, CHISLAbonentovSPUURD1,
 replace(convert(decimal(10,2),OBEMBEZPU1),''.'','',''), replace(convert(decimal(10,2),OBEMBEZPUURD1),''.'','',''), replace(convert(decimal(10,2),OBEMSPU1),''.'','',''), replace(convert(decimal(10,2),OBEMSPUURD1),''.'','',''), replace(convert(decimal(10,2),OplataZaGAZ1),''.'','',''), 
replace(convert(decimal(10,2),OplataZaGAZURD1),''.'','',''),
AbonentovSPU2, AbonentovSPUURD2, AbonentovBEZPU2, AbonentovBEZPUURD2, 
CHISLAbonentovBEZPU2, CHISLAbonentovBEZPUURD2, CHISLAbonentovSPU2, CHISLAbonentovSPUURD2,
 replace(convert(decimal(10,2),OBEMBEZPU2),''.'','',''), replace(convert(decimal(10,2),OBEMBEZPUURD2),''.'','',''), replace(convert(decimal(10,2),OBEMSPU2),''.'','',''), replace(convert(decimal(10,2),OBEMSPUURD2),''.'','',''), replace(convert(decimal(10,2),OplataZaGAZ2),''.'','',''), 
replace(convert(decimal(10,2),OplataZaGAZURD2),''.'','',''),
AbonentovSPU3, AbonentovSPUURD3,AbonentovBEZPU3, AbonentovBEZPUURD3, CHISLAbonentovBEZPU3, 
CHISLAbonentovBEZPUURD3, CHISLAbonentovSPU3, CHISLAbonentovSPUURD3,
 replace(convert(decimal(10,2),OBEMBEZPU3),''.'','',''), replace(convert(decimal(10,2),OBEMBEZPUURD3),''.'','',''), replace(convert(decimal(10,2),OBEMSPU3),''.'','',''), replace(convert(decimal(10,2),OBEMSPUURD3),''.'','',''), replace(convert(decimal(10,2),OplataZaGAZ3),''.'','',''), 
replace(convert(decimal(10,2),OplataZaGAZURD3),''.'','',''),
AbonentovSPU4, AbonentovSPUURD4, AbonentovBEZPU4, AbonentovBEZPUURD4, CHISLAbonentovBEZPU4,
 CHISLAbonentovBEZPUURD4, CHISLAbonentovSPU4, CHISLAbonentovSPUURD4,
 replace(convert(decimal(10,2),OBEMBEZPU4),''.'','',''), replace(convert(decimal(10,2),OBEMBEZPUURD4),''.'','',''), replace(convert(decimal(10,2),OBEMSPU4),''.'','',''), replace(convert(decimal(10,2),OBEMSPUURD4),''.'','',''), replace(convert(decimal(10,2),OplataZaGAZ4),''.'','',''), 
replace(convert(decimal(10,2),OplataZaGAZURD4),''.'','',''),
AbonentovSPU5, AbonentovSPUURD5, AbonentovBEZPU5, AbonentovBEZPUURD5, CHISLAbonentovBEZPU5, 
CHISLAbonentovBEZPUURD5, CHISLAbonentovSPU5, CHISLAbonentovSPUURD5,
 replace(convert(decimal(10,2),OBEMBEZPU5),''.'','',''), replace(convert(decimal(10,2),OBEMBEZPUURD5),''.'','',''), replace(convert(decimal(10,2),OBEMSPU5),''.'','',''), replace(convert(decimal(10,2),OBEMSPUURD5),''.'','',''), replace(convert(decimal(10,2),OplataZaGAZ5),''.'','',''),
 replace(convert(decimal(10,2),OplataZaGAZURD5),''.'','',''),
AbonentovSPU6, AbonentovSPUURD6, AbonentovBEZPU6, AbonentovBEZPUURD6, CHISLAbonentovBEZPU6,
 CHISLAbonentovBEZPUURD6, CHISLAbonentovSPU6, CHISLAbonentovSPUURD6,
 replace(convert(decimal(10,2),OBEMBEZPU6),''.'','',''), replace(convert(decimal(10,2),OBEMBEZPUURD6),''.'','',''), replace(convert(decimal(10,2),OBEMSPU6),''.'','',''), replace(convert(decimal(10,2),OBEMSPUURD6),''.'','',''), replace(convert(decimal(10,2),OplataZaGAZ6),''.'','',''),
 replace(convert(decimal(10,2),OplataZaGAZURD6),''.'','',''),
AbonentovSPU7, AbonentovSPUURD7, AbonentovBEZPU7, AbonentovBEZPUURD7, CHISLAbonentovBEZPU7,
 CHISLAbonentovBEZPUURD7, CHISLAbonentovSPU7, CHISLAbonentovSPUURD7,
 replace(convert(decimal(10,2),OBEMBEZPU7),''.'','',''), replace(convert(decimal(10,2),OBEMBEZPUURD7),''.'','',''), replace(convert(decimal(10,2),OBEMSPU7),''.'','',''), replace(convert(decimal(10,2),OBEMSPUURD7),''.'','',''), replace(convert(decimal(10,2),OplataZaGAZ7),''.'','',''), 
replace(convert(decimal(10,2),OplataZaGAZURD7),''.'','',''),
AbonentovSPU8, AbonentovSPUURD8, AbonentovBEZPU8, AbonentovBEZPUURD8, CHISLAbonentovBEZPU8,
 CHISLAbonentovBEZPUURD8, CHISLAbonentovSPU8, CHISLAbonentovSPUURD8,
 replace(convert(decimal(10,2),OBEMBEZPU8),''.'','',''), replace(convert(decimal(10,2),OBEMBEZPUURD8),''.'','',''), replace(convert(decimal(10,2),OBEMSPU8),''.'','',''), replace(convert(decimal(10,2),OBEMSPUURD8),''.'','',''), replace(convert(decimal(10,2),OplataZaGAZ8),''.'','',''), 
replace(convert(decimal(10,2),OplataZaGAZURD8),''.'','',''),
AbonentovSPU9, AbonentovSPUURD9, AbonentovBEZPU9, AbonentovBEZPUURD9, CHISLAbonentovBEZPU9, 
CHISLAbonentovBEZPUURD9, CHISLAbonentovSPU9, CHISLAbonentovSPUURD9,
 replace(convert(decimal(10,2),OBEMBEZPU9),''.'','',''), replace(convert(decimal(10,2),OBEMBEZPUURD9),''.'','',''), replace(convert(decimal(10,2),OBEMSPU9),''.'','',''), replace(convert(decimal(10,2),OBEMSPUURD9),''.'','',''), replace(convert(decimal(10,2),OplataZaGAZ9),''.'','',''), 
replace(convert(decimal(10,2),OplataZaGAZURD9),''.'','',''),
AbonentovSPU10, AbonentovSPUURD10,AbonentovBEZPU10, AbonentovBEZPUURD10, CHISLAbonentovBEZPU10, 
CHISLAbonentovBEZPUURD10, CHISLAbonentovSPU10, CHISLAbonentovSPUURD10,
 replace(convert(decimal(10,2),OBEMBEZPU10),''.'','',''), replace(convert(decimal(10,2),OBEMBEZPUURD10),''.'','',''), replace(convert(decimal(10,2),OBEMSPU10),''.'','',''), replace(convert(decimal(10,2),OBEMSPUURD10),''.'','',''), replace(convert(decimal(10,2),OplataZaGAZ10),''.'','',''), 
replace(convert(decimal(10,2),OplataZaGAZURD10),''.'','',''),
AbonentovSPU11, AbonentovSPUURD11, AbonentovBEZPU11, AbonentovBEZPUURD11, CHISLAbonentovBEZPU11,
 CHISLAbonentovBEZPUURD11, CHISLAbonentovSPU11, CHISLAbonentovSPUURD11,
replace(convert(decimal(10,2),OBEMBEZPU11),''.'','',''), replace(convert(decimal(10,2),OBEMBEZPUURD11),''.'','',''), replace(convert(decimal(10,2),OBEMSPU11),''.'','',''), replace(convert(decimal(10,2),OBEMSPUURD11),''.'','',''), replace(convert(decimal(10,2),OplataZaGAZ11),''.'','',''), 
replace(convert(decimal(10,2),OplataZaGAZURD11),''.'','',''),
AbonentovSPU12, AbonentovSPUURD12, AbonentovBEZPU12, AbonentovBEZPUURD12, CHISLAbonentovBEZPU12,
 CHISLAbonentovBEZPUURD12, CHISLAbonentovSPU12, CHISLAbonentovSPUURD12,
 replace(convert(decimal(10,2),OBEMBEZPU12),''.'','',''), replace(convert(decimal(10,2),OBEMBEZPUURD12),''.'','',''), replace(convert(decimal(10,2),OBEMSPU12),''.'','',''), replace(convert(decimal(10,2),OBEMSPUURD12),''.'','',''), replace(convert(decimal(10,2),OplataZaGAZ12),''.'','','') , 
replace(convert(decimal(10,2),OplataZaGAZURD12),''.'','','')
 from #Analiz '
--select @SQL
exec (@SQL)

drop table #Analiz

--insert OpenDataSource('Microsoft.Jet.OLEDB.4.0','Data Source="D:\Report\repAnaliz.xls";User ID=Admin;Password=;Extended properties=Excel 5.0')...[Лист2$]
--(c1, c2, c3, c4,  c5, c6, c7,c8,c9, c10,c11,c12,c13,c14,c15,c16,c17,c18,c19,
--c20,c21,c22,c23,c24,c25,c26,c27,c28,c29,c30,c31,c32,c33,c34,c35,c36,c37,c38,
--c39,c40, c41, c42, c43, c44, c45, c46,  c47, c48, c49,c50,c51, c52,c53,c54,
--c55,c56,c57,c58,c59,c60,c61,c62,c63,c64,c65,c66,c67,c68,c69,c70,c71,c72,c73,
--c74,c75,c76,c77,c78,c79,c80,c81,c82, c83, c84, c85, c86, c87, c88, c89, c90, 
--c91, c92,c93,c94, c95,c96,c97,c98,c99,c100,c101,c102,c103,c104,c105,c106,c107,
--c108,c109,c110,c111,c112,c113,c114,c115,c116,c117,c118,c119,c120,c121,c122,c123,
--c124,c125, c126, c127, c128, c129, c130, c131,  c132, c133, c134,c135,c136, c137,
--c138,c139,c140,c141,c142,c143,c144,c145,c146,c147,c148,c149,c150,c151,c152,c153,
--c154,c155,c156,c157,c158,c159,c160,c161,c162,c163,c164,c165,c166,c167,c168)
--select AbonentovSPU1, AbonentovSPUURD1,AbonentovBEZPU1, AbonentovBEZPUURD1, 
--CHISLAbonentovBEZPU1, CHISLAbonentovBEZPUURD1, CHISLAbonentovSPU1, CHISLAbonentovSPUURD1,
-- OBEMBEZPU1, OBEMBEZPUURD1, OBEMSPU1, OBEMSPUURD1, replace(convert(decimal(10,2),OplataZaGAZ1),'.',','), 
--replace(convert(decimal(10,2),OplataZaGAZURD1),'.',','),
--AbonentovSPU2, AbonentovSPUURD2, AbonentovBEZPU2, AbonentovBEZPUURD2, 
--CHISLAbonentovBEZPU2, CHISLAbonentovBEZPUURD2, CHISLAbonentovSPU2, CHISLAbonentovSPUURD2,
-- OBEMBEZPU2, OBEMBEZPUURD2, OBEMSPU2, OBEMSPUURD2, replace(convert(decimal(10,2),OplataZaGAZ2),'.',','), 
--replace(convert(decimal(10,2),OplataZaGAZURD2),'.',','),
--AbonentovSPU3, AbonentovSPUURD3,AbonentovBEZPU3, AbonentovBEZPUURD3, CHISLAbonentovBEZPU3, 
--CHISLAbonentovBEZPUURD3, CHISLAbonentovSPU3, CHISLAbonentovSPUURD3,
-- OBEMBEZPU3, OBEMBEZPUURD3, OBEMSPU3, OBEMSPUURD3, replace(convert(decimal(10,2),OplataZaGAZ3),'.',','), 
--replace(convert(decimal(10,2),OplataZaGAZURD3),'.',','),
--AbonentovSPU4, AbonentovSPUURD4, AbonentovBEZPU4, AbonentovBEZPUURD4, CHISLAbonentovBEZPU4,
-- CHISLAbonentovBEZPUURD4, CHISLAbonentovSPU4, CHISLAbonentovSPUURD4,
-- OBEMBEZPU4, OBEMBEZPUURD4, OBEMSPU4, OBEMSPUURD4, replace(convert(decimal(10,2),OplataZaGAZ4),'.',','), 
--replace(convert(decimal(10,2),OplataZaGAZURD4),'.',','),
--AbonentovSPU5, AbonentovSPUURD5, AbonentovBEZPU5, AbonentovBEZPUURD5, CHISLAbonentovBEZPU5, 
--CHISLAbonentovBEZPUURD5, CHISLAbonentovSPU5, CHISLAbonentovSPUURD5,
-- OBEMBEZPU5, OBEMBEZPUURD5, OBEMSPU5, OBEMSPUURD5, replace(convert(decimal(10,2),OplataZaGAZ5),'.',','),
-- replace(convert(decimal(10,2),OplataZaGAZURD5),'.',','),
--AbonentovSPU6, AbonentovSPUURD6, AbonentovBEZPU6, AbonentovBEZPUURD6, CHISLAbonentovBEZPU6,
-- CHISLAbonentovBEZPUURD6, CHISLAbonentovSPU6, CHISLAbonentovSPUURD6,
-- OBEMBEZPU6, OBEMBEZPUURD6, OBEMSPU6, OBEMSPUURD6, replace(convert(decimal(10,2),OplataZaGAZ6),'.',','),
-- replace(convert(decimal(10,2),OplataZaGAZURD6),'.',','),
--AbonentovSPU7, AbonentovSPUURD7, AbonentovBEZPU7, AbonentovBEZPUURD7, CHISLAbonentovBEZPU7,
-- CHISLAbonentovBEZPUURD7, CHISLAbonentovSPU7, CHISLAbonentovSPUURD7,
-- OBEMBEZPU7, OBEMBEZPUURD7, OBEMSPU7, OBEMSPUURD7, replace(convert(decimal(10,2),OplataZaGAZ7),'.',','), 
--replace(convert(decimal(10,2),OplataZaGAZURD7),'.',','),
--AbonentovSPU8, AbonentovSPUURD8, AbonentovBEZPU8, AbonentovBEZPUURD8, CHISLAbonentovBEZPU8,
-- CHISLAbonentovBEZPUURD8, CHISLAbonentovSPU8, CHISLAbonentovSPUURD8,
-- OBEMBEZPU8, OBEMBEZPUURD8, OBEMSPU8, OBEMSPUURD8, replace(convert(decimal(10,2),OplataZaGAZ8),'.',','), 
--replace(convert(decimal(10,2),OplataZaGAZURD8),'.',','),
--AbonentovSPU9, AbonentovSPUURD9, AbonentovBEZPU9, AbonentovBEZPUURD9, CHISLAbonentovBEZPU9, 
--CHISLAbonentovBEZPUURD9, CHISLAbonentovSPU9, CHISLAbonentovSPUURD9,
-- OBEMBEZPU9, OBEMBEZPUURD9, OBEMSPU9, OBEMSPUURD9, replace(convert(decimal(10,2),OplataZaGAZ9),'.',','), 
--replace(convert(decimal(10,2),OplataZaGAZURD9),'.',','),
--AbonentovSPU10, AbonentovSPUURD10,AbonentovBEZPU10, AbonentovBEZPUURD10, CHISLAbonentovBEZPU10, 
--CHISLAbonentovBEZPUURD10, CHISLAbonentovSPU10, CHISLAbonentovSPUURD10,
-- OBEMBEZPU10, OBEMBEZPUURD10, OBEMSPU10, OBEMSPUURD10, replace(convert(decimal(10,2),OplataZaGAZ10),'.',','), 
--replace(convert(decimal(10,2),OplataZaGAZURD10),'.',','),
--AbonentovSPU11, AbonentovSPUURD11, AbonentovBEZPU11, AbonentovBEZPUURD11, CHISLAbonentovBEZPU11,
-- CHISLAbonentovBEZPUURD11, CHISLAbonentovSPU11, CHISLAbonentovSPUURD11,
-- OBEMBEZPU11, OBEMBEZPUURD11, OBEMSPU11, OBEMSPUURD11, replace(convert(decimal(10,2),OplataZaGAZ11),'.',','), 
--replace(convert(decimal(10,2),OplataZaGAZURD11),'.',','),
--AbonentovSPU12, AbonentovSPUURD12, AbonentovBEZPU12, AbonentovBEZPUURD12, CHISLAbonentovBEZPU12,
-- CHISLAbonentovBEZPUURD12, CHISLAbonentovSPU12, CHISLAbonentovSPUURD12,
-- OBEMBEZPU12, OBEMBEZPUURD12, OBEMSPU12, OBEMSPUURD12, replace(convert(decimal(10,2),OplataZaGAZ12),'.',','), 
--replace(convert(decimal(10,2),OplataZaGAZURD12),'.',',')
-- from #Analiz









GO