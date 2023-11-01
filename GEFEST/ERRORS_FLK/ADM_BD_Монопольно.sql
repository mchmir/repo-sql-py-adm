-- Монопольно захватываем базу
declare @error bit
declare @Q as VarChar(1000)

set @Q='gefest'

-- Монопольный режим
--exec @error=sp_dboption @Q,'single user', 'true'

-- Вернуть базу в рабочий режим
exec @error=sp_dboption @Q,'single user', 'false'
