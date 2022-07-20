SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO




-------------****************Проведение юридического документа********************-------------------------------------
CREATE PROCEDURE [dbo].[spUndertakingJuridikal] (@IDPeriod as int, 
	@IDContract as int,
	@IdDocument as int output,
	@AmountDocument as float,
	@GosPoshlina as float,
	@blErr as bit output
)
AS
declare @AmountBalance as float
declare @IDBalance1 as int
declare @IDBalance2 as int
declare @IDBalance3 as int
declare @IDBalance4 as int
declare @IDBalance6 as int
declare @Date as datetime
declare @amount1 as float
declare @amount2 as float
declare @amount3 as float
declare @amount4 as float
declare @amount6 as float

set @Date=dbo.DateOnly(getdate()); 
--declare @blErr bit
set @blErr = 0
if not exists(select * from Operation where IdDocument=@IdDocument)
begin
begin transaction
		update document set idperiod=@idperiod where iddocument=@iddocument


		select @IDBalance1=IDBalance from Balance where IdAccounting=1 and IdContract=@IDContract and IdPeriod=@IDPeriod
		if isnull(@IdBalance1,0)=0
		begin
			insert Balance( IDAccounting, IDPeriod, IDContract, AmountBalance)
			values (1, @IDPeriod, @IDContract, 0)
			if @@Error <> 0	set @blErr = 1
			set @IDBalance1=scope_identity()
		end 

		select @IDBalance2=IDBalance from Balance where IdAccounting=2 and IdContract=@IDContract and IdPeriod=@IDPeriod
		if isnull(@IdBalance2,0)=0
		begin
			insert Balance( IDAccounting, IDPeriod, IDContract, AmountBalance)
			values (2, @IDPeriod, @IDContract, 0)
			if @@Error <> 0 set @blErr = 1
			set @IDBalance2=scope_identity()
		end 

		select @IDBalance3=IDBalance from Balance where IdAccounting=3 and IdContract=@IDContract and IdPeriod=@IDPeriod
		if isnull(@IdBalance3,0)=0
		begin
			insert Balance( IDAccounting, IDPeriod, IDContract, AmountBalance)
			values (3, @IDPeriod, @IDContract, 0)
			if @@Error <> 0 set @blErr = 1
			set @IDBalance3=scope_identity()
		end 
		--согласно ТЗ от 22.02.2018
		select @IDBalance4=IDBalance from Balance where IdAccounting=4 and IdContract=@IDContract and IdPeriod=@IDPeriod
		select @IDBalance6=IDBalance from Balance where IdAccounting=6 and IdContract=@IDContract and IdPeriod=@IDPeriod
		--
--		print '@IDBalance1 '+Convert(varchar(30), @IDBalance1)
--		print '@IDBalance2 '+Convert(varchar(30), @IDBalance2)
--		print '@IDBalance3 '+Convert(varchar(30), @IDBalance3)
--		print '@IDBalance4 '+Convert(varchar(30), @IDBalance4)
--		print '@IDBalance6 '+Convert(varchar(30), @IDBalance6)
	-- перенос с основного долга+пеня+услуги на иск 
	if exists(select * from Operation where IdDocument=@IdDocument)
	begin
		delete operation where IdDocument=@IdDocument
		exec dbo.spRecalcBalances @IDContract, @IDPeriod
	end
	--согласно ТЗ от 22.02.2018
	select @amount1=dbo.fGetLastBalance(@IDPeriod, @IDContract, 1)
	select @amount4=dbo.fGetLastBalance(@IDPeriod, @IDContract, 4)
	select @amount6=dbo.fGetLastBalance(@IDPeriod, @IDContract, 6)
	--select @AmountBalance=dbo.fGetLastBalance(@IDPeriod, @IDContract, 1)
	set @AmountBalance=isnull(@amount1,0)+isnull(@amount4,0)+isnull(@amount6,0)
--		print '@amount1 '+Convert(varchar(30), @amount1)
--		print '@amount4 '+Convert(varchar(30), @amount4)
--		print '@amount6 '+Convert(varchar(30), @amount6)
--		print '@AmountBalance '+Convert(varchar(30), @AmountBalance)
	--	
	if @AmountBalance<0 
	begin
		if @AmountDocument>-@AmountBalance
			set @AmountDocument=-@AmountBalance
	end
	else
	begin
		set @AmountDocument=0
	end
--	print '@AmountDocument '+Convert(varchar(30), @AmountDocument)
	set @AmountBalance=@AmountDocument
	if @AmountDocument>0 
	begin
		--согласно ТЗ от 22.02.2018
		if @AmountBalance>0
		begin
			if isnull(@amount4,0)<0
			begin
				if -@amount4>@AmountBalance
				begin
					set @amount4=-@AmountBalance
				end

				set @AmountBalance=@AmountBalance+@amount4

				insert into Operation (DateOperation, AmountOperation, NumberOperation, IDBalance, IDDocument, IdTypeOperation)
				values (@Date, round(-@amount4,6), 0, @IDBalance4, @IdDocument, 3)
				if @@Error <> 0 set @blErr = 1
			end
		end
		if @AmountBalance>0
		begin
			if isnull(@amount6,0)<0
			begin
				if -@amount6>@AmountBalance
				begin
					set @amount6=-@AmountBalance
				end

				set @AmountBalance=@AmountBalance+@amount6

				insert into Operation (DateOperation, AmountOperation, NumberOperation, IDBalance, IDDocument, IdTypeOperation)
				values (@Date, round(-@amount6,6), 0, @IDBalance6, @IdDocument, 3)
				if @@Error <> 0 set @blErr = 1
			end
		end
		----
		if @AmountBalance>0
		begin
			if isnull(@amount1,0)<0
			begin
				if -@amount1>@AmountBalance
				begin
					set @amount1=-@AmountBalance
				end

				set @AmountBalance=@AmountBalance+@amount1

				insert into Operation (DateOperation, AmountOperation, NumberOperation, IDBalance, IDDocument, IdTypeOperation)
				values (@Date, round(-@amount1,6), 0, @IDBalance1, @IdDocument, 3)
				if @@Error <> 0 set @blErr = 1
			end
		end
		----
		insert into Operation (DateOperation, AmountOperation, NumberOperation, IDBalance, IDDocument, IdTypeOperation)
		values (@Date, round(-@AmountDocument,6), 0, @IDBalance2, @IdDocument, 3)
		if @@Error <> 0 set @blErr = 1
	end


	-- начисление гос пошлины
	insert into Operation (DateOperation, AmountOperation, NumberOperation, IDBalance, IDDocument, IdTypeOperation)
	values (@Date, round(-@GosPoshlina,6), 0, @IDBalance3, @IdDocument, 2)
	if @@Error <> 0 set @blErr = 1

	if @blErr=0
		commit transaction
	else 
		rollback transaction

	exec dbo.spRecalcBalances @IDContract, @IDPeriod
end



GO