SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

--процедура для преобразования числа в строку	versoin 1.1 от ( 05.10.2003 )
CREATE PROCEDURE [dbo].[spStringBigValueReturnKZ] (@Value as bigint,@flag as int, @strRes varchar(500) output)
AS
--max @Value = 9223372036854775807
---------------------------------------------------------------
declare @strResult as varchar(500)
set @strResult = ''

create table #TmpSufix (IDTemp int, Range int, shtname varchar(6) collate Cyrillic_General_CI_AS, fulname varchar(10) collate Cyrillic_General_CI_AS, 
	sufix1 varchar(5) collate Cyrillic_General_CI_AS, sufix2 varchar(5) collate Cyrillic_General_CI_AS, sufix3 varchar(5) collate Cyrillic_General_CI_AS)
insert into #TmpSufix(IDTemp, Range, shtname, fulname, sufix1, sufix2, sufix3) 
	values (1,1000000000,'млрд. ','миллиардт','ы ',' ','ардың ')
insert into #TmpSufix(IDTemp, Range, shtname, fulname, sufix1, sufix2, sufix3) 
	values (2,1000000,'млн. ','миллионд','ы ',' ','ардың ')
insert into #TmpSufix(IDTemp, Range, shtname, fulname, sufix1, sufix2, sufix3) 
	values (3,1000,'мың. ','мың',' ','дар ',' ')
insert into #TmpSufix(IDTemp, Range, shtname, fulname, sufix1, sufix2, sufix3) 
	values (4,1,'','','','','')

create table #TempExchange (IDTemp int, Unit varchar(20) collate Cyrillic_General_CI_AS, 
	Dcm varchar(20) collate Cyrillic_General_CI_AS, Hndr varchar(20) collate Cyrillic_General_CI_AS) 
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (1,'бір ','он ','жүз ')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (2,'екі ','жиырма ','екі жүз ')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (3,'үш ','отыз ','үш жүз ')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (4,'төрт ','қырық ','төрт жүз ')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (5,'бес ','елу ','бес жүз ')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (6,'алты ','алпыс ','алты жүз ')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (7,'жеті ','жетпіс ','жеті жүз ')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (8,'сегіз ','сексен ','сегіз жүз ')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (9,'тоғыз ','тоқсан ','тоғыз жүз ')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (10,'он ','','')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (11,'он бір ','','')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (12,'он екі ','','')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (13,'он үш ','','')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (14,'он төрт ','','')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (15,'он бес ','','')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (16,'он алты ','','')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (17,'он жеті ','','')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (18,'он сегіз ','','')
insert into #TempExchange(IDTemp, Unit, Dcm, Hndr) values (19,'он тоғыз ','','')

declare @sufix1 as varchar(5), @sufix2 as varchar(5), @sufix3 as varchar(5)
declare @fulname as varchar(20), @shtname as varchar(10)
declare @number as bigint, @fld as int, @digit as int, @range as int, @IDTemp as int 

set @number = @Value

if @number = 0 
	set @strResult = 'нөл'
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
					set @strResult = @strResult + 'бір '
				Else If @fld = 2
					set @strResult = @strResult + 'екі '
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