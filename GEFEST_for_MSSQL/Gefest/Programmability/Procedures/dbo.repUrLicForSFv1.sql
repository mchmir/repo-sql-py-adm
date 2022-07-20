SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
CREATE PROCEDURE [dbo].[repUrLicForSFv1](@Month AS INT, @Year AS INT, @Doc AS INT, @Status AS INT) AS

set nocount on
---- Отчет для 
DECLARE @idPeriod AS INT
DECLARE @dEnd AS DATETIME

 -- @Doc 1 есть договор на ТО, 0 - нет договора
 -- @Status 1 статус юр.лица Активен, 0 - все кроме Акт, 2 - все данные


SET @idPeriod = (SELECT p.idPeriod FROM  Period p WHERE p.Year = @Year AND p.MONTH = @Month)
set @dEnd= dbo.fGetDatePeriod(@IdPeriod,0)

select 
      c.idContract as [IDContract],
      c.account as [Account], 
      p.RNN AS [BIN],
      ltrim(isnull(p.surname,'')) AS [Name],
      ISNULL(os.Name,'')+' '+isnull(p.name,'')+' '+isnull(p.patronic,'') as  [Named],
	  s.name+' '+ltrim(str(h.housenumber))+isnull(h.housenumberchar,'')+'-'+isnull(a.flat,'') as [Adress],
      dbo.fGetLastBalance(@idperiod, c.idcontract, 0) as [Balance], 
    
     case when dbo.fGetStatusContract(c.idcontract,@idperiod)=0 then 'Не опр.' else
     case when dbo.fGetStatusContract(c.idcontract,@idperiod)=1 then 'Акт.' else
     case when dbo.fGetStatusContract(c.idcontract,@idperiod)=2 then 'Закр.'  end end end AS [Status], 
     dbo.fGetStatusContract(c.idcontract,@idperiod) ff,
     aa.name as [Agent], 
     p.NumberDog AS [NumberDog], 
     convert(varchar(50),p.DateDog,104) AS [DateDog],  --- 104(или 4) convert в строку dd.mm.yyyy
     pp.numberphone AS [NumberPhone], 
     c.memo AS [Memo],
     convert(int, 0) AS [idTypeFU],  -- тип начисления
     CONVERT(VARCHAR(50),'') AS [TypeFU],  -- тип начисления
     convert(float, 0.00) as [NachislenoCubeM],
     convert(float, 0.00) as [NachislenoTenge],
     convert(float, 0.00) as [Penya],
     convert(float, 0.00) as [NachislenoCorrect]  
     
into #tmpReestURlicTO
from contract c with (nolock)
inner join person p with (nolock) on p.idperson=c.idperson
and (dbo.fGetIsJuridical (c.idperson,@idperiod)=1  OR (dbo.fGetIsJuridical (c.idperson,@idperiod)<>1 AND c.Account IN (2771128,2253013)))
inner join address a with (nolock) on a.idaddress=p.idaddress
inner join house h with (nolock) on a.idhouse=h.idhouse
inner join street s with (nolock) on s.idstreet=a.idstreet
inner join gobject gob with (nolock) on gob.idcontract=c.idcontract --AND gob.IDStatusGObject = 1
inner join gru with (nolock) on gru.idgru=gob.idgru
inner join agent aa with (nolock) on aa.idagent=gru.idagent
INNER JOIN OwnerShip os WITH (NOLOCK) ON p.IDOwnership = os.IDOwnership
left join GMeter gm  with (nolock) on gm.IdGObject=gob.IdGObject
	and dbo.fGetStatusPU (@dEnd,gm.idgmeter)=1 
left join typegmeter t with (nolock) on t.idtypegmeter=gm.idtypegmeter
left join phone pp with (nolock) on pp.idperson=p.idperson
order by account

  	update #tmpReestURlicTO
  	set NachislenoCubeM=qq.FA
  	from #tmpReestURlicTO c
  	inner join (select sum(ISNULL(f.Factamount,0)) fA, g.IdContract
  	from Factuse f  with (nolock)
  	inner join Gobject g  with (nolock) on g.idgobject=f.idgobject
  	inner join operation o with (nolock) on o.idoperation=f.idoperation
  	inner join document d  with (nolock) on d.iddocument=o.iddocument
  	and f.IdPeriod=@IdPeriod 
      --- только начисления
      and d.idtypedocument=5 

  	group by g.IdContract) qq on qq.IdContract=c.IdContract

    update #tmpReestURlicTO
    set idTypeFU=qq.FA, TypeFU = qq.FB
    --set TypeFU = qq.FB
  	from #tmpReestURlicTO c
  	inner join (select f.IDTypeFU AS FA, tf.Name AS FB, g.IDContract
      	from Factuse f  with (nolock)
  	inner join Gobject g  with (nolock) on g.idgobject=f.idgobject
  	inner join operation o with (nolock) on o.idoperation=f.idoperation
  	inner join document d  with (nolock) on d.iddocument=o.iddocument
    INNER JOIN TypeFU tf WITH (NOLOCK) ON f.IDTypeFU = tf.IDTypeFU
  	and f.IdPeriod=@IdPeriod and d.idtypedocument=5
  	) qq on qq.IdContract=c.IdContract
    

    update #tmpReestURlicTO
    set NachislenoTenge=convert(float,qq.AO)*-1
    from #tmpReestURlicTO c
    inner join (select sum(ISNULL(o.amountoperation,0)) AO, f.IDContract
    from Document f  with (nolock) 
    inner join operation o with (nolock) on f.iddocument=o.iddocument
    and f.IdPeriod=@IdPeriod and f.idtypedocument=5
    inner join balance b with (nolock) on o.idbalance=b.idbalance
    where  b.IDAccounting = 1  -- начислено за газ
    group by f.IDContract) qq on qq.IdContract=c.IdContract

    update #tmpReestURlicTO
    set Penya=convert(float,qq.AO)*-1
    from #tmpReestURlicTO c
    inner join (select sum(ISNULL(o.amountoperation,0)) AO, f.IDContract
    from Document f  with (nolock) 
    inner join operation o with (nolock) on f.iddocument=o.iddocument
    and f.IdPeriod=@IdPeriod and f.idtypedocument<>1
    inner join balance b with (nolock) on o.idbalance=b.idbalance
    where b.IDAccounting = 4 and o.IDTYPEOPERATION = 2 -- начислено за газ
    group by f.IDContract) qq on qq.IdContract=c.IdContract


    update #tmpReestURlicTO
  	set NachislenoCorrect=(qq.FA)*-1
  	from #tmpReestURlicTO c
  	inner join (select sum(ISNULL(d.DocumentAmount,0)) fA, d.IdContract
  	from Document d  with (nolock)
    WHERE  d.IdPeriod=@IdPeriod and d.idtypedocument=7
  	group by d.IdContract) qq on qq.IdContract=c.IdContract

/*if @per>-1
delete #tmpReestURlicTO where ff<>@per*/

if @Doc = 1 
delete #tmpReestURlicTO where NumberDog is NULL

if  @Doc = 0 
delete #tmpReestURlicTO where NumberDog is not NULL

  -- вывести только юр.лица со статусом АКТ
IF  @Status = 1
DELETE #tmpReestURlicTO WHERE ff <> 1
  -- вывести только юр.лица статуса Неопр и Закр
IF @Status = 0
DELETE #tmpReestURlicTO WHERE ff = 1


--select * from #tmpReestURlicTO AS c WHERE c.Account = 1061392 order by account

select DISTINCT
  --ROW_NUMBER() OVER(ORDER BY c.Account ASC) AS Row,
  c.Account AS [ЛС], 
  c.Name AS [Наименование], 
  c.Named AS [Наименование ЮЛ], 
  c.BIN AS [ИНН/БИН],
  c.NumberDog AS [№Договора],
  c.DateDog AS [Дата договора],
  c.TypeFU AS [Тип начисления],
  c.Status AS [Статус],
  --- при начислении по среднему не выставлять счет---- 
  CASE  WHEN c.idTypeFU = 3 THEN 0 ELSE  ROUND(c.NachislenoCubeM, 4) END  AS [Начислено м.куб],
  CASE  WHEN c.idTypeFU = 3 THEN 0 ELSE  ROUND(c.NachislenoTenge,2) END AS [Начислено тенге],
  ROUND(c.Penya,2) AS [Начислено Пеня],
  ROUND(c.NachislenoCorrect,2) AS [Начислено Коррект.],
  CASE  WHEN c.idTypeFU = 3 THEN c.Penya + c.NachislenoCorrect ELSE  ROUND(c.NachislenoTenge + c.Penya + c.NachislenoCorrect, 2) END   AS [ИТОГО]
from #tmpReestURlicTO AS c
--WHERE c.Account = 3282004 
order by account

-----------------
drop table #tmpReestURlicTO

GO