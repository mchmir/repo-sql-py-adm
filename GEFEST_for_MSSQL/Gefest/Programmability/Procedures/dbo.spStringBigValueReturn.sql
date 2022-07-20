SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

--процедура для преобразования числа в строку	versoin 1.1 от ( 05.10.2003 )
CREATE PROCEDURE [dbo].[spStringBigValueReturn] (@Value as bigint,@flag as int, @strRes varchar(500) output)
AS
--max @Value = 9223372036854775807
---------------------------------------------------------------
declare @strResult as varchar(500)
set @strResult = ''

create table #TmpSufix (IDTemp int, Range int, shtname varchar(6) collate Cyrillic_General_CI_AS, fulname varchar(10) collate Cyrillic_General_CI_AS, 
	sufix1 varchar(5) collate Cyrillic_General_CI_AS, sufix2 varchar(5) collate Cyrillic_General_CI_AS, sufix3 varchar(5) collate Cyrillic_General_CI_AS)
insert into #TmpSufix(IDTemp, Range, shtname, fulname, sufix1, sufix2, sufix3) 
	values (1,1000000000,'млрд. ','миллиард','а ',' ','ов ')
insert into #TmpSufix(IDTemp, Range, shtname, fulname, sufix1, sufix2, sufix3) 
	values (2,1000000,'млн. ','миллион','а ',' ','ов ')
insert into #TmpSufix(IDTemp, Range, shtname, fulname, sufix1, sufix2, sufix3) 
	values (3,1000,'тыс. ','тысяч','и ','а ',' ')
insert into #TmpSufix(IDTemp, Range, shtname, fulname, sufix1, sufix2, sufix3) 
	values (4,1,'','','','','')

create table #TempExchange (IDTemp int, Unit varchar(20) collate Cyrillic_General_CI_AS, 
	Dcm varchar(20) collate Cyrillic_General_CI_AS, Hndr varchar(20) collate Cyrillic_General_CI_AS) 
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (1,'один ','десять ','сто ')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (2,'две ','двадцать ','двести ')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (3,'три ','тридцать ','триста ')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (4,'четыре ','сорок ','четыреста ')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (5,'пять ','пятьдесят ','пятьсот ')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (6,'шесть ','шестьдесят ','шестьсот ')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (7,'семь ','семьдесят ','семьсот ')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (8,'восемь ','восемьдесят ','восемьсот ')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (9,'девять ','девяносто ','девятьсот ')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (10,'десять ','','')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (11,'одинадцать ','','')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (12,'двенадцать ','','')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (13,'тринадцать ','','')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (14,'четырнадцать ','','')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (15,'пятнадцать ','','')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (16,'шестнадцать ','','')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (17,'семнадцать ','','')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (18,'восемнадцать ','','')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (19,'девятнадцать ','','')

declare @sufix1 as varchar(5), @sufix2 as varchar(5), @sufix3 as varchar(5)
declare @fulname as varchar(20), @shtname as varchar(10)
declare @number as bigint, @fld as int, @digit as int, @range as int, @IDTemp as int 

set @number = @Value

if @number = 0 
	set @strResult = 'ноль'
else
begin
	declare Loc cursor for select * from #TmpSufix 
	open Loc

	FETCH NEXT FROM Loc INTO @IDTemp, @Range, @shtname, @fulname, @sufix1, @sufix2, @sufix3
	WHILE @@FETCH_STATUS=0
	BEGIN
		If @number >= @Range
		begin
			set @fld = @number / @Range
			set @number = @number % @Range

			set @digit = @fld / 100
			set @fld = @fld % 100

			If @digit > 0
				set @strResult = @strResult + (select Hndr from #TempExchange where IDTemp = @digit)

			If @fld > 19
			begin
          				set @digit = @fld / 10
				set @fld = @fld % 10
				set @strResult = @strResult + (select Dcm from #TempExchange where IDTemp = @digit)
			end

			If @fld > 0
			begin
				If (@Range <> 1000) Or (@fld > 2)
					set @strResult = @strResult + (select Unit from #TempExchange where IDTemp = @fld)
				Else If @fld = 1
					set @strResult = @strResult + 'одна '
				Else If @fld = 2
					set @strResult = @strResult + 'две '
	          	End

			If @flag = 1
			begin
				If @fld = 1
					set @strResult = @strResult + @fulname + @sufix2
				Else If @fld > 4 Or @fld < 1
					set @strResult = @strResult + @fulname + @sufix3
				else
					set @strResult = @strResult + @fulname + @sufix1
			end
	        	Else
				set @strResult = @strResult + @shtname
		end

	FETCH NEXT FROM Loc INTO @IDTemp, @Range, @shtname, @fulname, @sufix1, @sufix2, @sufix3
	END
	CLOSE Loc
	DEALLOCATE Loc
end
drop table #TmpSufix
drop table #TempExchange
set @strRes = @strResult
--select @strResult
GO