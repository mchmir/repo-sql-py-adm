SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
CREATE PROCEDURE [dbo].[HexToChar]
    @BinValue varbinary(255),
    @HexCharValue nvarchar(255) OUTPUT
AS
    DECLARE @CharValue nvarchar(255);
    DECLARE @Position int;
    DECLARE @Length int;
    DECLARE @HexString nchar(16);
    SELECT @CharValue = N'0x';
    SELECT @Position = 1;
    SELECT @Length = DATALENGTH(@BinValue);
    SELECT @HexString = N'0123456789ABCDEF';
    WHILE (@Position <= @Length)
    BEGIN
        DECLARE @TempInt int;
        DECLARE @FirstInt int;
        DECLARE @SecondInt int;
        SELECT @TempInt = CONVERT(int, SUBSTRING(@BinValue,@Position,1));
        SELECT @FirstInt = FLOOR(@TempInt/16);
        SELECT @SecondInt = @TempInt - (@FirstInt*16);
        SELECT @CharValue = @CharValue +
            SUBSTRING(@HexString, @FirstInt+1, 1) +
            SUBSTRING(@HexString, @SecondInt+1, 1);
        SELECT @Position = @Position + 1;
    END
    SELECT @HexCharValue = @CharValue;
GO