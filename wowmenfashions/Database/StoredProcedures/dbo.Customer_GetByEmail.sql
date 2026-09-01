CREATE PROCEDURE [dbo].[Customer_GetByEmail]
    @Email NVARCHAR(256)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [Id],
        [FirstName],
        [LastName],
        [Email],
        [Phone],
        [PasswordHash],
        [IsEmailVerified],
        [IsActive],
        [CreatedAt],
        [UpdatedAt],
        [LastLoginAt]
    FROM 
        [dbo].[Customers]
    WHERE 
        [Email] = @Email;
END
GO
