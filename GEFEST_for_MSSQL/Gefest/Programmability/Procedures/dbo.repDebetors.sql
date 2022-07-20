SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO



CREATE procedure [dbo].[repDebetors] (@CountMonth int, @SumDolg int) as 

declare @IDPeriod int
set @IDPeriod=dbo.fGetNowPeriod()

declare @tBalance table (idcontract int, AmountBalance float)

insert into @tBalance (idcontract, AmountBalance)
select b.idcontract, Round(sum(b.AmountBalance),2) AmountBalance
from balance b (nolock)
where b.idperiod=@IDPeriod 
group by b.idcontract

delete from @tBalance where AmountBalance>=0

delete from @tBalance where AmountBalance>=@SumDolg*-1
--select * from @tBalance
--order by idcontract

declare @tPay table(idcontract int, idperiod int, datepay datetime)

insert into @tPay (idcontract, idperiod, datepay)
select d.idcontract, max(d.idperiod) idperiod, max(d.DocumentDate) ddate
from document d (nolock)
--inner join @tBalance tb on tb.idcontract=d.idcontract
where d.idtypedocument=1
group by d.idcontract

declare @Tbl table (idcontract int, LS varchar(20), FIO varchar(200), Address varchar(200), DolgNow float, PU int, 
IDPeriodOplata int, DateOplata varchar(10), Controler varchar(50), IskDate varchar(10), CountLives int,OtklDate varchar(10), Note varchar(200))

insert into @Tbl (idcontract, LS, FIO, Address, DolgNow, PU,IDPeriodOplata,DateOplata,Controler,IskDate,CountLives,OtklDate,Note)
select distinct ct.idcontract, ct.Account as LIC_ACC, p.Surname+' '+p.name+' '+p.Patronic as FIO ,
s.name+ ', '+ltrim(str(hs.housenumber))+isnull(hs.housenumberchar,'')+ '-'+ltrim(a.flat) as ADDRESS,
Round(tb.AmountBalance,2), gm.idgmeter, tp.idperiod, convert(varchar(10),tp.datepay,104),ag.name,convert(varchar(10),jd.documentdate,104),
go.CountLives, case when gm.idgmeter is null then convert(varchar(10),do.documentdate,104) else '' end,
case when gm.idgmeter is null then do.note else '' end
from contract ct (nolock)
inner join person p (nolock) on ct.IDPerson=p.idperson
inner join address a (nolock) on a.idaddress=p.idaddress
inner join street s with (nolock)  on s.idstreet=a.idstreet
inner join house hs  with (nolock) on hs.idhouse=a.idhouse
inner join gobject go with (nolock) on go.idcontract=ct.idcontract
inner join gru with (nolock) on gru.idgru=go.idgru
inner join agent ag with (nolock) on gru.idagent=ag.idagent
left join gmeter gm (nolock) on go.idgobject=gm.idgobject and gm.IDStatusGMeter=1
left join (select idcontract, max(documentdate) documentdate from document where idtypedocument=13 group by idcontract) jd on jd.idcontract=ct.idcontract
left join (select idcontract, max(iddocument) iddocument from document where idtypedocument=18 group by idcontract) iddo on iddo.idcontract=ct.idcontract
left join (select iddocument, documentdate, note from document where idtypedocument=18) do on do.iddocument=iddo.iddocument
inner join @tBalance tb on tb.idcontract=ct.idcontract
inner join @tPay tp on tp.idcontract=ct.idcontract
where ct.account<>'9999999' and ct.status<>2
--group by ct.idcontract, ct.Account, p.Surname, p.name, p.Patronic,
--s.name,hs.housenumber,hs.housenumberchar,a.flat,gm.idgmeter
order by 4



--update @Tbl set IDPeriodOplata=qq.IDPeriod 
--from @Tbl T inner join (select max(IDPeriod) IDPeriod, IDContract from document
--where idtypedocument=1 group by idcontract)qq on qq.idcontract=T.idcontract
set @IDPeriod=@IDPeriod-@CountMonth

select LS, isnull(FIO,'н/д') FIO, Address, DolgNow,case when isnull(PU,0)=0 then 'нет' else 'есть' end PU,
DateOplata,Controler,IskDate,CountLives,OtklDate,Note from @Tbl 
where IDPeriodOplata<@IDPeriod
order by DolgNow

GO