CREATE PROCEDURE [dbo].[Cart_UpdateItem]
    @CartItemId INT,
    @Quantity INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Assuming validation happens before or here
    IF @Quantity <= 0
    BEGIN
        EXEC dbo.Cart_RemoveItem @CartItemId = @CartItemId;
        RETURN;
    END

    UPDATE CartItems
    SET Quantity = @Quantity,
        UpdatedAt = GETUTCDATE()
    WHERE Id = @CartItemId;
END