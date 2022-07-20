SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO





CREATE PROCEDURE [dbo].[repPaymentsMonth] (@idPeriodB int, @idPeriodE int, @Path varchar(1000), @DateName varchar(200) ) AS
---------------**************Отчет о поступлении денежных средств по месяцам*********-------------------
SET NOCOUNT ON
/*
declare @IdPeriodB int
declare @IdPeriodE int

set @IdPeriodB=93
set @IdPeriodE=84
*/


declare @TblPeriod table(irow int identity(1,1) ,idperiod int, mnt int, yer int)
insert into @TblPeriod (idperiod, mnt, yer)
select IDPeriod,[Month], [Year] from period p (nolock)
where p.idperiod>=@IdPeriodB and p.idperiod<=@IdPeriodE

create table #Tbl  (idtypeagent int, idagent int, idtypepay int, TAName varchar(20), AName varchar(50),TPName varchar(20),
Jan decimal(10,2), Feb decimal(10,2), Mar decimal(10,2), Apr decimal(10,2), May decimal(10,2), Jun decimal(10,2), Jul decimal(10,2), Aug decimal(10,2), Sep decimal(10,2), Oct decimal(10,2), Nov decimal(10,2), Dec decimal(10,2))

insert into #Tbl (idtypeagent, idagent, idtypepay, TAName, AName, TPName,Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec)
select ta.idtypeagent, case when a.idagent is null then 999999 else a.idagent end, 
tp.idtypepay, ta.name, case when a.idagent is null then 'Касса от потребителей' else a.name end agent , tp.name,
0,0,0,0,0,0,0,0,0,0,0,0
from batch b with (nolock)
inner join TypePay tp with (nolock) on tp.idtypepay=b.idtypepay
left join Agent a with (nolock) on b.iddispatcher=a.idagent
left join typeagent ta with (nolock) on ta.idtypeagent=a.idtypeagent
where  b.idperiod>=@IdPeriodB and b.idperiod<=@IdPeriodE
	and (b.idtypebatch=1 or b.idtypebatch=2)
group by ta.idtypeagent, a.idagent, tp.idtypepay, ta.name, a.name, tp.name
order by tp.idtypepay,a.idagent

declare @RC int
declare @idPeriod int
declare @iMnt int
declare @SumBatch float
declare @TblAmount table (idagent int, amount float)
set @RC=(select count(idperiod) from @TblPeriod)

while @RC>0
	begin
		set @idPeriod=(select idperiod from @TblPeriod where irow=@RC)
			delete from @TblAmount
			insert into @TblAmount (idagent, amount) 
			select iddispatcher, sum(batchamount) from batch where idperiod=@idPeriod
			and (idtypebatch=1 or idtypebatch=2) and iddispatcher is not null
			group by iddispatcher
			insert into @TblAmount (idagent, amount) 
			select 999999, sum(batchamount) from batch where idperiod=@idPeriod
			and (idtypebatch=1 or idtypebatch=2) and iddispatcher is null
			group by iddispatcher
		set @iMnt=(select mnt from @TblPeriod where irow=@RC)
		if @iMnt=1
			begin
				update #Tbl set Jan=Amount from #Tbl t	
				inner join (select idagent, amount from  @TblAmount) TA on TA.idagent=t.idagent
			end
		if @iMnt=2
			begin
				update #Tbl set Feb=Amount from #Tbl t	
				inner join (select idagent, amount from  @TblAmount) TA on TA.idagent=t.idagent
			end
		if @iMnt=3
			begin
				update #Tbl set Mar=Amount from #Tbl t	
				inner join (select idagent, amount from  @TblAmount) TA on TA.idagent=t.idagent
			end
		if @iMnt=4
			begin
				update #Tbl set Apr=Amount from #Tbl t	
				inner join (select idagent, amount from  @TblAmount) TA on TA.idagent=t.idagent
			end
		if @iMnt=5
			begin
				update #Tbl set May=Amount from #Tbl t	
				inner join (select idagent, amount from  @TblAmount) TA on TA.idagent=t.idagent
			end
		if @iMnt=6
			begin
				update #Tbl set Jun=Amount from #Tbl t	
				inner join (select idagent, amount from  @TblAmount) TA on TA.idagent=t.idagent
			end
        if @iMnt=7
			begin
				update #Tbl set Jul=Amount from #Tbl t	
				inner join (select idagent, amount from  @TblAmount) TA on TA.idagent=t.idagent
			end
		if @iMnt=8
			begin
				update #Tbl set Aug=Amount from #Tbl t	
				inner join (select idagent, amount from  @TblAmount) TA on TA.idagent=t.idagent
			end
		if @iMnt=9
			begin
				update #Tbl set Sep=Amount from #Tbl t	
				inner join (select idagent, amount from  @TblAmount) TA on TA.idagent=t.idagent
			end
		if @iMnt=10
			begin
				update #Tbl set Oct=Amount from #Tbl t	
				inner join (select idagent, amount from  @TblAmount) TA on TA.idagent=t.idagent
			end
		if @iMnt=11
			begin
				update #Tbl set Nov=Amount from #Tbl t	
				inner join (select idagent, amount from  @TblAmount) TA on TA.idagent=t.idagent
			end
		if @iMnt=12
			begin
				update #Tbl set Dec=Amount from #Tbl t	
				inner join (select idagent, amount from  @TblAmount) TA on TA.idagent=t.idagent
			end
		set @RC=@RC-1
	end

--select * from @TblPeriod
--
--select * from #Tbl

declare @SQL varchar(8000)
set @SQL='insert OpenDataSource(''Microsoft.Jet.OLEDB.4.0'',''Data Source="' + @path +
'";User ID=Admin;Password=;Extended properties=Excel 5.0'')...[Лист2$]
(c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14, c15, c16, c17)
select' 
set @SQL=@SQL+' taname, tpname, aname, replace(jan,''.'','',''),  replace(feb,''.'','',''), replace(mar,''.'','',''), '
set @SQL=@SQL+' replace(apr,''.'','',''), replace(may,''.'','',''), replace(jun,''.'','',''), replace(jul,''.'','',''), '
set @SQL=@SQL+' replace(aug,''.'','',''), replace(sep,''.'','',''), '
set @SQL=@SQL+' replace(oct,''.'','',''), replace(nov,''.'','',''), '
set @SQL=@SQL+' replace(dec,''.'','',''),replace(jan+feb+mar+apr+may+jun+jul+aug+sep+oct+nov+dec,''.'','',''), '''+@DateName+''''
set @SQL=@SQL+' from #Tbl '
	

--select @SQL
exec (@SQL)

drop table #Tbl






GO