SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
Create FUNCTION [dbo].[fGetIDContract] (@IdGmeter int)  
RETURNS int AS  
BEGIN 
declare @ret int

select @ret=idcontract from gmeter g 
inner join gobject q on q.idgobject=g.idgobject
where idgmeter=@IdGmeter
return @ret
end
GO