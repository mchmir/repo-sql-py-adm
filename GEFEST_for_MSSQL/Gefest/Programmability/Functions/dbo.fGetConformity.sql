SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO



CREATE FUNCTION [dbo].[fGetConformity] (@tablename int, @who int, @id1 int, @id2 int)  
RETURNS int AS  
BEGIN 
declare @ret int

--@tablename=1 document
--@tablename=2 gmeter
--@tablename=3 typegmeter

--@who=1 ищем соответствия ID между записями из таблиц БД Горгаза и Нацэкса
--@who=2 ищем соответствия ID между записями из таблиц БД Горгаза и КомСтройТела

--среди id1, id2 возвращаем значение 0 (входного нулевого)
--@id1 соответствует ID Горгаза
--@id2 соответствует ID Нацэкса/КомСтройТела

if @id1=0 and @id2=0
	return 0
if @who=1
begin
	if @tablename=1
	begin
		if @id1=0
		begin
			set @ret=(select correspondent3 from conformitydocument where correspondent1=@id2)
		end
		if @id2=0
		begin
			set @ret=(select correspondent1 from conformitydocument where correspondent3=@id1)
		end
	end
	if @tablename=2
	begin
		if @id1=0
		begin
			set @ret=(select correspondent3 from conformitygmeter where correspondent1=@id2)
		end
		if @id2=0
		begin
			set @ret=(select correspondent1 from conformitygmeter where correspondent3=@id1)
		end
	end
	if @tablename=3
	begin
		if @id1=0
		begin
			set @ret=(select correspondent3 from conformitytypegmeter where correspondent1=@id2)
		end
		if @id2=0
		begin
			set @ret=(select correspondent1 from conformitytypegmeter where correspondent3=@id1)
		end
	end
end
if @who=2
begin
	if @tablename=1
	begin
		if @id1=0
		begin
			set @ret=(select correspondent3 from conformitydocument where correspondent2=@id2)
		end
		if @id2=0
		begin
			set @ret=(select correspondent2 from conformitydocument where correspondent3=@id1)
		end
	end
	if @tablename=2
	begin
		if @id1=0
		begin
			set @ret=(select correspondent3 from conformitygmeter where correspondent2=@id2)
		end
		if @id2=0
		begin
			set @ret=(select correspondent2 from conformitygmeter where correspondent3=@id1)
		end
	end
	if @tablename=3
	begin
		if @id1=0
		begin
			set @ret=(select correspondent3 from conformitytypegmeter where correspondent2=@id2)
		end
		if @id2=0
		begin
			set @ret=(select correspondent2 from conformitytypegmeter where correspondent3=@id1)
		end
	end
end
if @ret is null
	set @ret=0
return @ret
END

GO