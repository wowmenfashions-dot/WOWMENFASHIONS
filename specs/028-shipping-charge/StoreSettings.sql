USE [wowmenfashions];
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'StoreSettings')
BEGIN
    CREATE TABLE dbo.StoreSettings (
        Id INT PRIMARY KEY,
        FreeShippingThreshold DECIMAL(18,2) NOT NULL DEFAULT 1000.00,
        ShippingChargeAmount DECIMAL(18,2) NOT NULL DEFAULT 100.00
    );

    -- Insert default row
    INSERT INTO dbo.StoreSettings (Id, FreeShippingThreshold, ShippingChargeAmount)
    VALUES (1, 1000.00, 100.00);
END
GO

CREATE OR ALTER PROCEDURE dbo.StoreSettings_Get
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, FreeShippingThreshold, ShippingChargeAmount
    FROM dbo.StoreSettings
    WHERE Id = 1;
END
GO

CREATE OR ALTER PROCEDURE dbo.StoreSettings_Update
    @FreeShippingThreshold DECIMAL(18,2),
    @ShippingChargeAmount DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.StoreSettings
    SET FreeShippingThreshold = @FreeShippingThreshold,
        ShippingChargeAmount = @ShippingChargeAmount
    WHERE Id = 1;
END
GO
