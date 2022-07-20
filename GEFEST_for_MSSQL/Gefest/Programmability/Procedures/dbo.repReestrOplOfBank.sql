SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO









CREATE PROCEDURE [dbo].[repReestrOplOfBank] (@Date datetime,@dateE datetime,  @idagent int) AS
---------------**************Реестр по оплаты услуг ВДГО из банка*********-------------------
SET NOCOUNT ON

--declare @Date datetime
--declare @dateE datetime
--declare @idagent int
--set @idagent=0
--set @date='2010-10-13'
--set @dateE='2010-10-23'
declare @idperiod int
set @idperiod=dbo.[fGetPeriodDate](@date)
set @Date=dbo.DateOnly(@Date)
set @dateE=dbo.DateOnly(@dateE)

--AGrigoryev, 6.03.2013
select c.account as SCHET,np.Surname+' '+left(ltrim(isnull(np.name,'')),1)+'.' +left(ltrim(isnull(np.Patronic,'')),1)as FIO,
i.adres as ADRES, 
convert(decimal(10,2),sum(o.amountoperation)) as Suma,
--convert(decimal(10,2),o.amountoperation) as Suma,
a.name as BANK,a.name+' ('+convert(varchar(200),d.DocumentAmount)+')' as BANK2,a.idagent as ID,d.documentdate documentdate
into #tmpUSl
from document d
inner join contract c on c.idcontract=d.idcontract
and idtypedocument=1 --and dbo.DateOnly(d.documentdate)>=@Date
and d.idperiod=@idperiod
and dbo.DateOnly(d.documentdate)<=@DateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=6
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=2
inner join agent a with (nolock) on a.idagent=bb.iddispatcher
inner join person np with (nolock)  on np.idperson=c.Idperson
inner join(select distinct s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(aa.flat) as ADRES,
c.idcontract from contract c
inner join document d on c.idcontract=d.idcontract
and idtypedocument=1 --and dbo.DateOnly(d.documentdate)>=@Date
and d.idperiod=@idperiod
and dbo.DateOnly(d.documentdate)<=@DateE
inner join GObject g  with (nolock) on g.IdContract=c.IdContract
inner join address aa  with (nolock) on aa.idaddress=g.idaddress
inner join street s with (nolock)  on s.idstreet=aa.idstreet
inner join house hs  with (nolock) on hs.idhouse=aa.idhouse)i on i.idcontract=c.idcontract
group by c.account,np.Surname+' '+left(ltrim(isnull(np.name,'')),1)+'.' +left(ltrim(isnull(np.Patronic,'')),1),
i.adres, a.name,a.idagent,d.documentdate ,d.DocumentAmount

if (isnull(@idagent,0)<>0)
begin
delete #tmpUSl where id<>@idagent
end

declare @SSum as decimal(10,2)
select @SSum=sum(Suma) from #tmpUSl
--select SCHET, FIO, ADRES, sum(Suma) Suma, BANK, ID, documentdate,@SSum from #tmpUSlk 
--group by SCHET, FIO, ADRES, BANK, ID, documentdate,@SSum
--order by  documentdate

select *, @SSum SSum from #tmpUSl order by documentdate

--select * from #tmpUSl order by documentdate

drop table #tmpUSl








GO