CREATE PROCEDURE [dbo].[Customer_GetById]
    @Id INT
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
        [Id] = @Id;
END