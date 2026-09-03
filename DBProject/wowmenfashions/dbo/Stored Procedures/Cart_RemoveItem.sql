CREATE PROCEDURE [dbo].[Cart_RemoveItem]
    @CartItemId INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM CartItems
    WHERE Id = @CartItemId;
END