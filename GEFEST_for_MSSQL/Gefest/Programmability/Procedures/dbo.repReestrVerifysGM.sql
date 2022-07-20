SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
-- =============================================
-- Author:		<>
-- Create date: <2017-08-14>
-- Description:	<Реестр по поверенным ПУ>
-- =============================================
CREATE PROCEDURE [dbo].[repReestrVerifysGM](@dBegin datetime, @dEnd datetime, @IDAgent int, @Path varchar(1000)) AS

create table #TReestrVerifyGM (rank int, docdate varchar(12), account varchar(15), address varchar(200), gminfo varchar(50), snumber varchar(20), bdisplay varchar(12), edisplay varchar(12), result varchar(10), agent varchar(50))

declare @DB as varchar(12)
declare @DE as varchar(12)
set @DB = convert(varchar(12), @dBegin,104)
set @DE = convert(varchar(12), @dEnd,104)

if @IDAgent=0
	begin
		insert into #TReestrVerifyGM (rank, docdate, account, address, gminfo, snumber, bdisplay, edisplay, result, agent)
		select row_number() over (order by ct.Account) as rank ,convert(varchar(12), documentdate,104) as docdate, ct.Account, s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat) as ADDRESS,
		tg.name+', '+ltrim(convert(varchar(5),tg.ClassAccuracy)) as gminfo, gm.SerialNumber,
		replace(d.documentamount,'.',',') as begindisplay,  replace(pd4.value,'.',',') as enddisplay,
		case when pd2.value=1 then 'Прошёл' else 'Не прошёл' end as result, ag.name
		from document d
		inner join contract ct on d.idcontract=ct.idcontract
		inner join person p (nolock) on ct.IDPerson=p.idperson
		inner join address a (nolock) on a.idaddress=p.idaddress
		inner join street s with (nolock)  on s.idstreet=a.idstreet
		inner join house hs  with (nolock) on hs.idhouse=a.idhouse
		inner join pd pd1 on pd1.iddocument=d.iddocument
		and pd1.idtypepd=16 --and convert(int,pd1.value)=98 
		inner join agent ag on ag.idagent=convert(int,pd1.value)
		inner join pd as pd2 on pd2.iddocument=d.iddocument
		and pd2.idtypepd=30 --and convert(int,pd2.value)<>1
		inner join pd as pd3 on pd3.iddocument=d.iddocument
		and pd3.idtypepd=7 --and convert(int,pd2.value)<>1
		inner join gmeter gm on gm.idgmeter=convert(int,pd3.value)
		left join TypeGmeter tg on gm.idtypegmeter=tg.idtypegmeter
		inner join pd as pd4 on pd4.iddocument=d.iddocument
		and pd4.idtypepd=27 --and convert(int,pd2.value)<>1
		where idtypedocument=22 and documentdate>=@dBegin and documentdate<=@dEnd
		order by 3,2,9,10
	end
else
	begin
		insert into #TReestrVerifyGM (rank, docdate, account, address, gminfo, snumber, bdisplay, edisplay, result, agent)
		select row_number() over (order by ct.Account) as rank ,convert(varchar(12), documentdate,104) as docdate, ct.Account, s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat) as ADDRESS,
		tg.name+', '+ltrim(convert(varchar(5),tg.ClassAccuracy)) as gminfo, gm.SerialNumber,
		replace(d.documentamount,'.',',') as begindisplay,  replace(pd4.value,'.',',') as enddisplay,
		case when pd2.value=1 then 'Прошёл' else 'Не прошёл' end as result, ag.name
		from document d
		inner join contract ct on d.idcontract=ct.idcontract
		inner join person p (nolock) on ct.IDPerson=p.idperson
		inner join address a (nolock) on a.idaddress=p.idaddress
		inner join street s with (nolock)  on s.idstreet=a.idstreet
		inner join house hs  with (nolock) on hs.idhouse=a.idhouse
		inner join pd pd1 on pd1.iddocument=d.iddocument
		and pd1.idtypepd=16 and convert(int,pd1.value)=@IDAgent 
		inner join agent ag on ag.idagent=convert(int,pd1.value)
		inner join pd as pd2 on pd2.iddocument=d.iddocument
		and pd2.idtypepd=30 --and convert(int,pd2.value)<>1
		inner join pd as pd3 on pd3.iddocument=d.iddocument
		and pd3.idtypepd=7 --and convert(int,pd2.value)<>1
		inner join gmeter gm on gm.idgmeter=convert(int,pd3.value)
		left join TypeGmeter tg on gm.idtypegmeter=tg.idtypegmeter
		inner join pd as pd4 on pd4.iddocument=d.iddocument
		and pd4.idtypepd=27 --and convert(int,pd2.value)<>1
		where idtypedocument=22 and documentdate>=@dBegin and documentdate<=@dEnd
		order by 3,2,9,10
	end


declare @SQL varchar(8000)
set @SQL='insert OpenDataSource(''Microsoft.Jet.OLEDB.4.0'',''Data Source="' + @path +
'";User ID=Admin;Password=;Extended properties=Excel 5.0'')...[Лист2$]
(c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12)'
set @SQL=@SQL+' select' 
set @SQL=@SQL+' rank, docdate, account, address, gminfo, snumber, bdisplay, edisplay, result, agent, '
set @SQL=@SQL+' '''+ @DB +''','''+ @DE +''' '
set @SQL=@SQL+' from #TReestrVerifyGM' 
	

exec (@SQL)
drop table #TReestrVerifyGM
GO