
CREATE   PROCEDURE dbo.StoreSettings_Update
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