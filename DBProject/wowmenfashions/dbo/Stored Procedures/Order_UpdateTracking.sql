CREATE PROCEDURE [dbo].[Order_UpdateTracking]
    @OrderId INT,
    @CourierName NVARCHAR(100),
    @TrackingNumber NVARCHAR(100),
    @TrackingUrl NVARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Orders
    SET CourierName = @CourierName,
        TrackingNumber = @TrackingNumber,
        TrackingUrl = @TrackingUrl
    WHERE Id = @OrderId;
END