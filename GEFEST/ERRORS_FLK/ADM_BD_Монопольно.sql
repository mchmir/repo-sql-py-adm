-- ���������� ����������� ����
declare @error bit
declare @Q as VarChar(1000)
--set @Q='gefest'
--exec @error=sp_dboption @Q,'single user', 'true'

-- ������� ���� � ������� �����
set @Q='gefest'
exec @error=sp_dboption @Q,'single user', 'false'