 
DELETE FROM dbo.AAAERC6

DECLARE @idGRU INT
DECLARE @idContract INT
DECLARE @idperiod INT
DECLARE @Year INT
DECLARE @Month INT

----------------------------------------------
SET @Year = 2019
SET @Month = 6
----------------------------------------------

SET @idperiod = (SELECT p.IDPeriod FROM dbo.Period p WHERE p.Year = @Year AND p.Month = @Month) 

DECLARE cursGRU CURSOR LOCAL FAST_FORWARD FOR 
  
  SELECT IDgru FROM GRU  WHERE IDGru NOT IN (8518,8743,8626,8666,8742,8738,8772,8771,8768,8747,8828,8781)
    --************************ test GRU ********************************
    --    SELECT IDgru FROM GRU  WHERE IDGru IN (8612,8715)
    --******************************************************************
OPEN cursGRU

FETCH NEXT FROM cursGRU INTO @idGRU

-- executing your stored procedure once for every value of your parameter     
WHILE @@FETCH_STATUS = 0 BEGIN
    --INSERT INTO #tmpStores EXEC repCountNoticeGM @param, 165
    --############################################################################          
          DECLARE cursC CURSOR LOCAL FAST_FORWARD FOR
              SELECT Contract.IDContract  FROM GRu   left JOIN GObject   ON GObject.IDGru = GRU.IDGru
                                                left JOIN dbo.Contract  ON GObject.IDContract = Contract.IDContract
              WHERE gru.IDGru =@idGRU

          OPEN cursC

              FETCH NEXT FROM cursC INTO @idContract
                         
              WHILE @@FETCH_STATUS = 0 BEGIN
                -------------------------------------------------------------------------------------
                   INSERT INTO dbo.AAAERC6  EXEC repCountNoticeERC @idGRU, @idperiod,@idContract

                -------------------------------------------------------------------------------------
                  FETCH NEXT FROM cursC INTO @idContract
              END
              
            CLOSE cursC
           DEALLOCATE cursC  


    --#############################################################################
  ------------------------------------------------------------------
    FETCH NEXT FROM cursGRU INTO @idGRU
END

CLOSE cursGRU
DEALLOCATE cursGRU

--SELECT * FROM #tmpStores 
--SELECT * FROM dbo.AAAERC a

--drop table #tmpStores  