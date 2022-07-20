SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO





CREATE Procedure [dbo].[PenyaNewYear] as 
begin

declare @IDPeriod int
declare @IDPredPeriod int
declare @IDBalance int
declare @IDContract int
declare @IdDocument int
declare @IDTypeDocument int
declare @Amount float
declare @DocumentDate datetime

set @IDPeriod=dbo.fGetNowPeriod()
set @IDPredPeriod=@IDPeriod-1

set @IDTypeDocument=7
set @DocumentDate='2013-12-31'

declare @Penya table (idcontract int, amount float)
insert into @Penya (idcontract, amount)
select b.idcontract, b.amountbalance from
balance b (nolock) 
where idperiod=@IDPredPeriod 
and idaccounting=4 and amountbalance<0

declare @NoDolg table (idcontract int, amount float)
insert into @NoDolg (idcontract, amount)
select b.idcontract, sum(b.amountbalance) amountbalance from
balance b (nolock) 
inner join document d (nolock) on d.idcontract=b.idcontract 
where b.idperiod=@IDPeriod and d.idperiod=@IDPeriod and d.idtypedocument=1
group by b.idcontract

delete from @NoDolg
where amount<0

--select * from @NoDolg
declare @T table (idcontract int, amount float)
insert into @T (idcontract,amount)
select P.idcontract,P.amount*-1  from 
@Penya P
inner join @NoDolg ND on P.IDContract=ND.IDContract
inner join contract c on P.IDContract=c.IDContract
/*
select P.*, ND.*, c.account from 
@Penya P
inner join @NoDolg ND on P.IDContract=ND.IDContract
inner join contract c on P.IDContract=c.IDContract
order by P.Amount

select sum(P.amount) from 
@Penya P
inner join @NoDolg ND on P.IDContract=ND.IDContract
*/

declare Loc cursor for 
	select d.idcontract, d.amount 
	from @T d 
	
	open Loc
	
	FETCH NEXT FROM Loc INTO @IDContract, @Amount
	WHILE @@FETCH_STATUS=0
		BEGIN

		--select @IDContract, @Amount
		--сохраняем документ
		insert into Document (IDPeriod, IDContract, IDTypeDocument, DocumentDate, DocumentAmount, Note)
			values(@IDPeriod, @IDContract, @IDTypeDocument, @DocumentDate, @Amount,'Новогодняя акция')
			set @IdDocument = scope_identity()

		--сохраняем параметры документа
		---тариф
		insert into PD (IDTypePD,IDDocument,Value)
		values (13,@IdDocument,'0')
		---тип начисления
		insert into PD (IDTypePD,IDDocument,Value)
		values (34,@IdDocument,'1')

		--ищем баланс по основному долгу
		select top 1 @IDBalance=IDBalance from balance with (nolock) 
		where IDPeriod = @IDPeriod and IDContract=@IDContract and IDAccounting=1
		
		if  isnull (@IDBalance, 0) =0
		begin
			insert Balance(IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)
			select top 1 IDAccounting, @IDPeriod, IDContract, AmountBalance, 0, 0
			from Balance with (nolock) 
			where IdPeriod<@IDPeriod and IdContract=@IdContract and IDAccounting=1
			order by idperiod desc
			set @IDBalance=scope_identity()
			
			if  isnull (@IDBalance, 0) =0
			begin
				insert Balance(IDAccounting, IDPeriod, IDContract, AmountBalance, AmountCharge, AmountPay)	
				values (1, @IDPeriod, @IDContract, 0, 0, 0)
				set @IdBalance=scope_identity()
			end
		end
		
		update balance
			set AmountBalance=AmountBalance+@Amount
			where IDBalance=@IDBalance--IDContract = @IDContract and IdPeriod=@IdPeriod and IdAccounting=1

		--сохраняем операцию
		insert into Operation(DateOperation, AmountOperation , NumberOperation, IDBalance, IDDocument, IdTypeOperation)
		values (@DocumentDate, @Amount,  '99999',  @IDBalance,  @IDDocument,  3)


		FETCH NEXT FROM Loc INTO @IDContract, @Amount
		
	
	end
	CLOSE Loc
	DEALLOCATE Loc


end
GO