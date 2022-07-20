SET QUOTED_IDENTIFIER, ANSI_NULLS OFF
GO
CREATE FUNCTION [dbo].[fGetNowPeriod]()
RETURNS int
AS  
BEGIN 
return (select max(IDPeriod) from Period with (nolock)  )
END
GO