CREATE   PROCEDURE dbo.Address_Update
    @Id INT,
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

    UPDATE CustomerAddresses
    SET 
        FullName = @FullName, 
        AddressLine = @AddressLine, 
        AddressLine2 = @AddressLine2,
        City = @City, 
        State = @State,
        PostalCode = @PostalCode, 
        Country = @Country, 
        ContactNumber = @ContactNumber,
        Landmark = @Landmark,
        IsDefaultShipping = @IsDefaultShipping, 
        IsDefaultBilling = @IsDefaultBilling
    WHERE Id = @Id;
END