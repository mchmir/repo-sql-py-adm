SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO





CREATE PROCEDURE [dbo].[spCleaningChanges](@idsending int) AS

delete from changes
where idsending=@idsending
and patindex('%1%',dbo.fGetChangesForObject(idsending,tablename,idobject))<>0
and patindex('%3%',dbo.fGetChangesForObject(idsending,tablename,idobject))<>0

delete from changes
where idsending=@idsending
and patindex('%1%',dbo.fGetChangesForObject(idsending,tablename,idobject))<>0
and patindex('%2%',dbo.fGetChangesForObject(idsending,tablename,idobject))<>0
and idtypeaction=2

delete from changes
where idsending=@idsending
and patindex('%2%',dbo.fGetChangesForObject(idsending,tablename,idobject))<>0
and patindex('%3%',dbo.fGetChangesForObject(idsending,tablename,idobject))<>0
and idtypeaction=2



GO