SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO





CREATE PROCEDURE [dbo].[repReestrOplOfKass] (@Date datetime, @dateE datetime,  @idagent int) AS
---------------**************Реестр по оплаты услуг ВДГО из Кассы*********-------------------
SET NOCOUNT ON

--declare @Date datetime
-- declare @dateE datetime
--declare @idagent int
--set @idagent=0
--set @date='2010-10-13'
-- set @dateE='2010-10-23'
set @Date=dbo.DateOnly(@Date)
set @dateE=dbo.DateOnly(@dateE)

declare @T table (idcontract int, adres varchar(200))
insert into @T (idcontract, adres)
select distinct c.idcontract,s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(aa.flat) as ADRES
from document d
inner join contract c on c.idcontract=d.idcontract
and idtypedocument=1 and dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=6
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=1inner join GObject g  with (nolock) on g.IdContract=c.IdContract
inner join address aa  with (nolock) on aa.idaddress=g.idaddress
inner join street s with (nolock)  on s.idstreet=aa.idstreet
inner join house hs  with (nolock) on hs.idhouse=aa.idhouse


--AGrigoryev, 26.02.2013
select distinct c.account as SCHET,np.Surname+' '+left(ltrim(isnull(np.name,'')),1)+'.' +left(ltrim(isnull(np.Patronic,'')),1)as FIO,
--s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(aa.flat) as ADRES, sum(convert(decimal(10,2),o.amountoperation)) as Suma,a.name as BANK,a.idagent as ID,d.documentdate documentdate
--s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(aa.flat) as ADRES, convert(decimal(10,2),o.amountoperation) as Suma,a.name as BANK,a.idagent as ID,d.documentdate documentdate
adr.adres as ADRES, sum(convert(decimal(10,2),o.amountoperation)) as Suma,a.name as BANK,a.idagent as ID,d.documentdate documentdate
into #tmpUSlk
from document d
inner join contract c on c.idcontract=d.idcontract
and idtypedocument=1 and dbo.DateOnly(d.documentdate)>=@Date and dbo.DateOnly(d.documentdate)<=@dateE
inner join operation o with (nolock) on d.iddocument=o.iddocument
inner join balance b with (nolock) on o.idbalance=b.idbalance
and b.idaccounting=6
inner join batch bb with (nolock) on bb.idbatch=d.idbatch
and idtypepay=1
inner join agent a with (nolock) on a.idagent=bb.idCashier
inner join @T adr on adr.idcontract=c.idcontract
--inner join GObject g  with (nolock) on g.IdContract=c.IdContract
--inner join address aa  with (nolock) on aa.idaddress=g.idaddress
--inner join street s with (nolock)  on s.idstreet=aa.idstreet
--inner join house hs  with (nolock) on hs.idhouse=aa.idhouse
inner join person np with (nolock)  on np.idperson=c.Idperson
--AGrigoryev, 26.02.2013
group by c.account,np.Surname+' '+left(ltrim(isnull(np.name,'')),1)+'.' +left(ltrim(isnull(np.Patronic,'')),1),
--s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(aa.flat),
adr.adres,a.name,a.idagent,d.documentdate 

if (isnull(@idagent,0)<>0)
begin
delete #tmpUSlk where id<>@idagent
end
--AGrigoryev, 26.02.2013
select * from #tmpUSlk order by  documentdate
--select SCHET, FIO, ADRES, sum(Suma) Suma, BANK, ID, documentdate from #tmpUSlk 
--group by SCHET, FIO, ADRES, BANK, ID, documentdate
--order by  documentdate

drop table #tmpUSlk




GO