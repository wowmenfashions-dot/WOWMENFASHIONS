
CREATE   PROCEDURE dbo.StoreSettings_Get
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, FreeShippingThreshold, ShippingChargeAmount
    FROM dbo.StoreSettings
    WHERE Id = 1;
END