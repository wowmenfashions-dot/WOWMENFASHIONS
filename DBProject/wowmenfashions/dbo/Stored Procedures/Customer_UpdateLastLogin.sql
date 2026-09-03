CREATE PROCEDURE [dbo].[Customer_UpdateLastLogin]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Customers]
    SET 
        [LastLoginAt] = GETUTCDATE()
    WHERE 
        [Id] = @Id;
END