SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE PROCEDURE [dbo].[repPayments](@Month int,
@Year int,
@IdTypeAgent int,
@IdtypePay int,
@IdAgent int) AS
/*
declare @IdPeriod int
declare @IdTypeAgent int
declare @IdtypePay int
declare @IdAgent int

set @idperiod=23
set @IdtypePay=0
set @IdTypeAgent=0
set @IdAgent=70
*/
declare @IdPeriod int
select @IdPeriod=idperiod from period where Month=@Month and year=@year
create table #Payments (IdTypeAgent int, IdAgent int, IdTypePay int, 
			TypeAgent varchar(50), Agent varchar(50), TypePay varchar(50),
			d1 float, d2 float, d3 float, d4 float,d5  float,d6 float,
			d7 float,d8 float,d9 float,d10 float,d11 float,d12 float,
			d13 float,d14 float,d15 float,d16 float,d17 float,d18 float,
			d19 float,d20 float,d21 float,d22 float,d23 float,d24 float,
			d25 float,d26 float,d27 float,d28 float,d29 float,d30 float,d31 float,den float)

declare @q varchar(8000)

insert #Payments (IdTypeAgent, IdAgent, IdTypePay, TypeAgent, Agent, TypePay,
			d1,d2,d3,d4,d5,d6,d7,d8,d9,d10,d11,d12,d13,d14,d15,d16,d17,d18,
			d19,d20,d21,d22,d23,d24,d25,d26,d27,d28,d29,d30,d31, den)
select ta.idtypeagent, a.idagent, tp.idtypepay, ta.name, a.name, tp.name,
	case when day(b.batchdate)=1 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=2 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=3 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=4 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=5 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=6 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=7 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=8 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=9 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=10 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=11 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=12 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=13 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=14 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=15 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=16 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=17 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=18 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=19 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=20 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=21 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=22 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=23 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=24 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=25 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=26 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=27 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=28 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=29 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=30 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when day(b.batchdate)=31 and Month(b.batchdate)=@Month and year(b.batchdate)=@year then b.batchamount else 0 end,
	case when Month(b.batchdate)<>@Month or year(b.batchdate)<>@year then b.batchamount else 0 end	
from batch b with (nolock)
inner join TypePay tp with (nolock) on tp.idtypepay=b.idtypepay
	--and Month(b.batchdate)=@Month
	--and year(b.batchdate)=@year
	and b.idperiod=@idperiod
	and (b.idtypebatch=1 or b.idtypebatch=2)
left join Agent a with (nolock) on b.iddispatcher=a.idagent
left join typeagent ta with (nolock) on ta.idtypeagent=a.idtypeagent

--case when typeagent is null then ''Касса'' else typeagent end typeagent
set @q='select idtypeagent, idagent, idtypepay, typeagent, 
	case when agent is null then ''Касса от потребителей'' else agent end agent, typepay,
	sum(d1) d1, sum(d2) d2, sum(d3) d3, sum(d4) d4, sum(d5) d5, 
	sum(d6) d6, sum(d7) d7, sum(d8) d8, sum(d9) d9, sum(d10) d10, 
	sum(d11) d11, sum(d12) d12, sum(d13) d13, sum(d14) d14, sum(d15) d15, 
	sum(d16) d16, sum(d17) d17, sum(d18) d18, sum(d19) d19, sum(d20) d20, 
	sum(d21) d21, sum(d22) d22, sum(d23) d23, sum(d24) d24, sum(d25) d25, 
	sum(d26) d26, sum(d27) d27, sum(d28) d28, sum(d29) d29, sum(d30) d30, 
	sum(d31) d31, sum(den) den
from #Payments '
set @q=@q+' where 1=1'

if isnull(@IdTypePay,0)>0
	set @q=@q+' and IdTypePay='+ltrim(str(@IdTypePay))
if isnull(@IdTypeAgent,0)>0
	set @q=@q+' and IdTypeAgent='+ltrim(str(@IdTypeAgent))
if isnull(@IdAgent,0)>0
	set @q=@q+' and IdAgent='+ltrim(str(@IdAgent))

set @q=@q+' group by idtypeagent, idagent, idtypepay, typeagent, agent, typepay'

exec(@q)

drop table #Payments
GO