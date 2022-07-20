SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO



CREATE procedure [dbo].[repSpravkaVnovUstPU] (@Bdate datetime, @Edate datetime,@idagent int) as 

--declare @Bdate datetime
--declare @Edate datetime
--declare @idagent int
--
--set @Bdate='2008-01-01'
--set @Edate='2008-01-31'
--set @idagent=0

declare @qqqq varchar(8000)
set @qqqq=' select ROW_NUMBER() OVER(ORDER BY g.dateinstall),c.account, ltrim(isnull(p.surname,''''))+'' ''+left(ltrim(isnull(p.name,'''')),1)+'' ''+left(ltrim(isnull(p.patronic,'''')),1) FIO,
	s.name+'' ''+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'''')+''-''+isnull(a.flat,'''') address,ltrim(isnull(ph.NumberPhone,'''')) tel,
	t.name+'', ''+ltrim(str(t.classaccuracy, 3, 1)) gmeter, g.dateinstall, aa.name,g.DateFabrication
from contract c with (nolock)
inner join person p with (nolock) on p.idperson=c.idperson
left join phone ph (nolock) on p.idperson=ph.idperson
inner join address a with (nolock) on a.idaddress=p.idaddress
inner join house h with (nolock) on a.idhouse=h.idhouse
inner join street s with (nolock) on s.idstreet=a.idstreet
inner join gobject go with (nolock) on go.idcontract=c.idcontract
inner join gmeter g  with (nolock) on g.idgobject=go.idgobject
and g.idstatusgmeter=1 and g.dateinstall>='+convert(varchar,round(convert(float,@Bdate),0, 1))+' and g.dateinstall<='+convert(varchar,round(convert(float,@Edate),0, 1))+'
inner join typegmeter t  with (nolock)  on t.idtypegmeter=g.idtypegmeter
inner join document d with (nolock) on c.idcontract=d.idcontract
and d.idtypedocument=12
inner join pd with (nolock)  on  pd.iddocument=d.iddocument
and idtypepd=7 and convert(int,pd.value)=g.idgmeter
inner join pd pds with (nolock) on pds.iddocument=d.iddocument
and pds.idtypepd=16
inner join agent aa with (nolock) on aa.idagent=convert(int,pds.value)'

if (isnull(@idagent,0)>0)
	set @qqqq=@qqqq+' and aa.idagent='+ltrim(str(@idagent))
set @qqqq=@qqqq+' order by  g.dateinstall'

exec(@qqqq)




GO