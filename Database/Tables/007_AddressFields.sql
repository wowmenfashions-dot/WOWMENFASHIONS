USE [wowmenfashions];
GO

-- Add granular fields to Orders table
IF NOT EXISTS (
    SELECT * FROM sys.columns 
    WHERE object_id = OBJECT_ID('Orders') AND name = 'ShippingState'
)
BEGIN
    ALTER TABLE Orders
    ADD 
        ShippingAddressLine1 NVARCHAR(500) NULL,
        ShippingAddressLine2 NVARCHAR(500) NULL,
        ShippingCity NVARCHAR(100) NULL,
        ShippingState NVARCHAR(100) NULL,
        ShippingCountry NVARCHAR(100) NULL,
        ShippingPostalCode NVARCHAR(50) NULL,
        ShippingContactNumber NVARCHAR(50) NULL,
        ShippingLandmark NVARCHAR(255) NULL;
END
GO

-- Add granular fields to CustomerAddresses table (if exists)
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'CustomerAddresses')
BEGIN
    IF NOT EXISTS (
        SELECT * FROM sys.columns 
        WHERE object_id = OBJECT_ID('CustomerAddresses') AND name = 'State'
    )
    BEGIN
        ALTER TABLE CustomerAddresses
        ADD 
            AddressLine2 NVARCHAR(500) NULL,
            State NVARCHAR(100) NULL,
            ContactNumber NVARCHAR(50) NULL,
            Landmark NVARCHAR(255) NULL;
    END
END
GO
