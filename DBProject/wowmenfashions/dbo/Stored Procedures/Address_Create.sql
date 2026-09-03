CREATE   PROCEDURE dbo.Address_Create
    @CustomerId INT,
    @FullName NVARCHAR(100),
    @AddressLine NVARCHAR(500),
    @AddressLine2 NVARCHAR(500),
    @City NVARCHAR(100),
    @State NVARCHAR(100),
    @PostalCode NVARCHAR(50),
    @Country NVARCHAR(100),
    @ContactNumber NVARCHAR(50),
    @Landmark NVARCHAR(255),
    @IsDefaultShipping BIT,
    @IsDefaultBilling BIT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO CustomerAddresses (
        CustomerId, FullName, AddressLine, AddressLine2, City, State,
        PostalCode, Country, ContactNumber, Landmark, IsDefaultShipping, IsDefaultBilling
    )
    VALUES (
        @CustomerId, @FullName, @AddressLine, @AddressLine2, @City, @State,
        @PostalCode, @Country, @ContactNumber, @Landmark, @IsDefaultShipping, @IsDefaultBilling
    );
END