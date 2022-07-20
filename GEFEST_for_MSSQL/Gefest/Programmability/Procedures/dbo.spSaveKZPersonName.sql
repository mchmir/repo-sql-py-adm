SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO


-------------****************Проведение юридического документа********************-------------------------------------
CREATE PROCEDURE [dbo].[spSaveKZPersonName] (@IDPerson as int, @Surname as nvarchar(100), 
	@Name as nvarchar(100),
	@Patronic as nvarchar(100),
	@blErr as bit output
)
AS

begin transaction

set @blErr = 0
select @Patronic

update person set nSurname=@Surname, nname=@Name, npatronic=@Patronic where idperson=@IDPerson

--declare @Q as varchar(500)
--set @Q='update person set nname=N'''+@Name+''', nsurname=N'''+@Surname+''', npatronic=N'''+@Patronic+''' where idperson='+convert(varchar(50), @IDPerson)
--exec(@Q)

	if @@Error <> 0	set @blErr = 1

	if @blErr=0
		commit transaction
	else 
		rollback transaction

select @blErr
GO