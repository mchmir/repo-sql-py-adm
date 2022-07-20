SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

CREATE PROCEDURE [dbo].[spPCGetConsamerIndications] (@idGmeter varchar(20))
AS
BEGIN
SELECT TOP 100 [DateDisplay],[Display]     
  FROM [Gefest].[dbo].[Indication]
  where [IdGMeter]=@idGmeter
  order by 1 desc
END
GO