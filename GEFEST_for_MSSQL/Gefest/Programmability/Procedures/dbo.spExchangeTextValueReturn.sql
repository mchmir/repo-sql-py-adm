SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
--процедура возвращает количество денег определенной валюты в строковом выражении	version 1.1 от ( 05.10.2003 )
CREATE PROCEDURE [dbo].[spExchangeTextValueReturn] (@IDExchange as int,@Value as money,@flagLong as int, @flagValueMax as int)
AS
declare @NameMin as varchar(50), @NameMax as varchar(50)
declare @strResult as varchar(1000)
declare @ValueMin as int, @ValueMax as bigint, @IsChange as int
declare @strValueMin as varchar(500), @strValueMax as varchar(500)

set @strResult = ''
set @ValueMax = round(@Value,0,1)
set @ValueMin = (@Value - @ValueMax)*100


if @IDExchange=0
	begin
		set @NameMax = 'тенге'
		set @isChange=0
	end
else
	select @NameMin=lower(NameMin), @NameMax=lower(NameMax), @IsChange=isnull(IsChange,0) from Exchange where IDExchange = @IDExchange

execute dbo.spStringBigValueReturn @ValueMax,@flagLong,@strValueMax output

if @flagValueMax = 1
begin
	execute dbo.spStringBigValueReturn @ValueMin,@flagLong,@strValueMin output
	if datalength(@strValueMin) = 1
		set @strValueMin = @strValueMin + '0'
end

if @IsChange = 0
begin
	if @NameMax='тенге'
	begin
		set @strResult = @strValueMax + 'тенге'  
		if @flagValueMax = 1
			set @strResult = @strResult + ' ' + @strValueMin +  ' тиын'
	end
	else
	begin
		set @strResult = @strValueMax + @NameMax  
		if @flagValueMax = 1
			set @strResult = @strResult + ' ' + @strValueMin + ' ' + @NameMin
	end
end 
else
begin
	declare @fldMax as int
	declare @fldMin as int
	set @fldMax = @ValueMax % 100
	set @fldMin = @ValueMin % 100

	if @NameMax = 'доллар'
	begin
		if (@fldMax >= 5 and @fldMax <= 19) or @fldMax = 0
			set @strResult = @strValueMax + 'долларов '
		else 
		begin
			set @fldMax = @ValueMax % 10
			if @fldMax = 1 
				set @strResult = @strValueMax + @NameMax + ' '
			else if @fldMax >= 2 and @fldMax <=4
				set @strResult = @strValueMax + 'доллара '
			else
				set @strResult = @strValueMax + 'долларов '
		end

		if @flagValueMax = 1
		begin
			if (@fldMin >= 5 and @fldMin <= 19) or @fldMin = 0 
				set @strResult = @strResult + @strValueMin + 'центов '
			else
			begin
				set @fldMin = @ValueMin % 10
				if @fldMin = 1 
					set @strResult = @strResult + @strValueMin + @NameMin + ' '
				else if @fldMin >= 2 and @fldMin <=4
					set @strResult = @strResult + @strValueMin + 'цента '
				else
					set @strResult = @strResult + @strValueMin + 'центов '
			end
		end
	end

	if @NameMax = 'рубль'
	begin
		if (@fldMax >= 5 and @fldMax <= 19) or @fldMax = 0
			set @strResult = @strValueMax + 'рублей '
		else 
		begin
			set @fldMax = @ValueMax % 10
			if @fldMax = 1 
				set @strResult = @strValueMax + @NameMax + ' '
			else if @fldMax >= 2 and @fldMax <=4
				set @strResult = @strValueMax + 'рубля '
			else
				set @strResult = @strValueMax + 'рублей '
		end
		if @flagValueMax = 1
		begin
			if (@fldMin >= 5 and @fldMin <= 19) or @fldMin = 0 
				set @strResult = @strResult + @strValueMin + 'копеек '
			else
			begin
				set @fldMin = @ValueMin % 10
				if @fldMin = 1 
					set @strResult = @strResult + @strValueMin + @NameMin + ' '
				else if @fldMin >= 2 and @fldMin <=4
					set @strResult = @strResult + @strValueMin + 'копейки '
				else
					set @strResult = @strResult + @strValueMin + 'копеек '
			end
		end
	end
end
select @strResult
GO