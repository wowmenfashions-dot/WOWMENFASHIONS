CREATE PROCEDURE [dbo].[Customer_Create]
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @Email NVARCHAR(256),
    @Phone NVARCHAR(20) = NULL,
    @PasswordHash NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if email already exists
    IF EXISTS (SELECT 1 FROM [dbo].[Customers] WHERE [Email] = @Email)
    BEGIN
        THROW 50001, 'Email already exists.', 1;
    END

    INSERT INTO [dbo].[Customers] (
        [FirstName], 
        [LastName], 
        [Email], 
        [Phone], 
        [PasswordHash], 
        [IsEmailVerified], 
        [IsActive], 
        [CreatedAt], 
        [UpdatedAt]
    )
    OUTPUT INSERTED.Id
    VALUES (
        @FirstName, 
        @LastName, 
        @Email, 
        @Phone, 
        @PasswordHash, 
        0, 
        1, 
        GETUTCDATE(), 
        GETUTCDATE()
    );
END
GO
