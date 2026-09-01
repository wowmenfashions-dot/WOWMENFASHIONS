-- Migration: Add Courier Tracking to Orders Table
IF NOT EXISTS (
    SELECT * FROM sys.columns 
    WHERE object_id = OBJECT_ID('Orders') AND name = 'CourierName'
)
BEGIN
    ALTER TABLE Orders
    ADD CourierName NVARCHAR(100) NULL,
        TrackingNumber NVARCHAR(100) NULL,
        TrackingUrl NVARCHAR(500) NULL;
END
GO
