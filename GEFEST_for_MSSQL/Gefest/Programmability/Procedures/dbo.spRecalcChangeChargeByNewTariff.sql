SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO


--процедура пересчета документов "Корректировка начисления" по новому тарифу

CREATE PROCEDURE [dbo].[spRecalcChangeChargeByNewTariff]  AS

declare @DBegin datetime
declare @DEnd datetime
declare @OldTariff numeric(10,2)
declare @NewTariff numeric(10,2)
declare @IDPeriod int

set @DBegin='2017-08-01'
set @DEnd='2017-08-31'
set @OldTariff=429.18
set @NewTariff=489.89
set @IDPEriod=151

DECLARE curAvr CURSOR
READ_ONLY
FOR select d.idcontract, d.iddocument, d.documentamount, pd.idpd from document d
inner join pd on pd.iddocument=d.iddocument and pd.idtypepd=13 and replace(pd.value,',','.')=@OldTariff
and documentdate>=@DBegin and documentdate<=@DEnd and idtypedocument=7

DECLARE @idcontract int
declare @docamount float
declare @iddocument int 
declare @idpd int
declare @summ float

OPEN curAvr

FETCH NEXT FROM curAvr INTO @idcontract, @iddocument, @docamount, @idpd
WHILE (@@fetch_status <> -1)
BEGIN
	IF (@@fetch_status <> -2)
	BEGIN
		--рассчитаем кубы исходя из суммы документа и старого тарифа
		set @summ=@docamount/@OldTariff
		--рассчитаем новую сумму документа
		set @docamount=@summ*@NewTariff
		--обновим сумму документа
		update document set documentamount=@docamount where iddocument=@iddocument
		--обновим сумму операции
		update operation set amountoperation=@docamount where iddocument=@iddocument
		--обновим тариф в параметрах документа
		update pd set value=replace(@NewTariff,'.',',') where idpd=@idpd
		--пересчет остатков
		exec dbo.spRecalcBalances @idcontract, @IdPeriod
	END
	FETCH NEXT FROM curAvr INTO @idcontract, @iddocument, @docamount, @idpd
END

CLOSE curAvr
DEALLOCATE curAvr

--select * from @tmpAvFact





GO