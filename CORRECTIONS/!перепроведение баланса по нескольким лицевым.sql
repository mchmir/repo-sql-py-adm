DECLARE @Acc AS VARCHAR(50)
DECLARE @idContract As INT
DECLARE @idPeriod AS INT
DECLARE @Year AS INT
DECLARE @Month AS INT
DECLARE @idGObject AS INT

SET @Year = 2020
SET @Month = 2
SET @idPeriod = (SELECT p.idPeriod FROM  Period p WHERE p.Year = @Year AND p.MONTH = @Month)
     
          DECLARE cursC CURSOR LOCAL FAST_FORWARD FOR
          
          SELECT c.IDContract  FROM Contract c WHERE c.Account IN (2101050)
            --(2771203,2101050,0322028,2498063,1141107,2432184,1141106,3331010,1753041,3151094,3332033,3332030,3332040,2781262,1012029,1712147,1034032,1434078,3681110,2332012,1833105,2124032,3731088,2521095,1935011,2053043,1221006,1221006,1812047,2682012,2141129,2472051,2062058,1952113,3591050,1665052,2664117,1802086)   

          OPEN cursC
              FETCH NEXT FROM cursC INTO @idContract                      
              WHILE @@FETCH_STATUS = 0 BEGIN
                -------------------------------------------------------------------------------------
                   exec dbo.spRecalcBalances @IDContract, @IDPeriod
                -------------------------------------------------------------------------------------
              FETCH NEXT FROM cursC INTO @idContract
              END
              
           CLOSE cursC
           DEALLOCATE cursC  