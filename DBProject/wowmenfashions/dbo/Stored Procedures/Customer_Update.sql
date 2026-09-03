CREATE PROCEDURE [dbo].[Customer_Update]
    @Id INT,
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @Phone NVARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Customers]
    SET 
        [FirstName] = @FirstName,
        [LastName] = @LastName,
        [Phone] = @Phone,
        [UpdatedAt] = GETUTCDATE()
    WHERE 
        [Id] = @Id;
END