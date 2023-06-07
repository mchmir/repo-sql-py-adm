DECLARE @Account INT
DECLARE @AmmountPay FLOAT
DECLARE @TerminalData VARCHAR(20)
DECLARE @DatePay DATETIME
DECLARE @idDispatcher INT 
DECLARE @idPay INT  
DECLARE @Year INT


SET @idDispatcher = 134
SET @Year = 2020
     
          DECLARE cursC CURSOR LOCAL FAST_FORWARD FOR
          SELECT p.IDPay  FROM Payments p WHERE p.IsPayComplete = 1 AND YEAR(p.DatePay) =@Year 
          OPEN cursC
              FETCH NEXT FROM cursC INTO @idPay                   
              WHILE @@FETCH_STATUS = 0 BEGIN
                -------------------------------------------------------------------------------------
                  SET @Account = (SELECT p.UserInputData FROM Payments p WHERE p.IDPay=@idPay)   
                  SET @AmmountPay = (SELECT p.PayAmount FROM Payments p WHERE p.IDPay=@idPay)
                  SET @TerminalData = @idPay
                  SET @DatePay = (SELECT p.DatePay FROM Payments p WHERE p.IDPay=@idPay)
                                 
                       EXEC [GG-APP].[Gefest].dbo.spEPAYSavePayments  @Account = @Account
                                                                     ,@AmmountPay = @AmmountPay
                                                                     ,@TerminalData = @TerminalData
                                                                     ,@DatePay = @DatePay
                                                                     ,@IDDispatcher = @idDispatcher
                 IF @@error<>0
                    BEGIN
                       UPDATE Payments SET IsPayComplete = 1 WHERE IDPay =@idPay  
                    END
                 ELSE
                    BEGIN
                       UPDATE Payments SET IsPayComplete = 0 WHERE IDPay =@idPay  
                    END
            
                -------------------------------------------------------------------------------------
              FETCH NEXT FROM cursC INTO @idPay 
              END
              
           CLOSE cursC
           DEALLOCATE cursC  