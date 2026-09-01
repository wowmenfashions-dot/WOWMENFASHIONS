CREATE OR ALTER PROCEDURE [dbo].[Cart_AddItem]
    @GuestCartId UNIQUEIDENTIFIER = NULL,
    @CustomerId INT = NULL,
    @ProductId INT,
    @Quantity INT,
    @SelectedColor NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @CartId UNIQUEIDENTIFIER;
    
    -- Try to find existing cart
    IF @CustomerId IS NOT NULL
    BEGIN
        SELECT @CartId = Id FROM Carts WHERE CustomerId = @CustomerId;
    END
    ELSE IF @GuestCartId IS NOT NULL
    BEGIN
        SELECT @CartId = Id FROM Carts WHERE GuestCartId = @GuestCartId;
    END

    -- Create new cart if it doesn't exist
    IF @CartId IS NULL
    BEGIN
        SET @CartId = NEWID();
        
        -- If GuestCartId was not provided and CustomerId is null, generate a new GuestCartId
        IF @GuestCartId IS NULL AND @CustomerId IS NULL
            SET @GuestCartId = NEWID();

        INSERT INTO Carts (Id, CustomerId, GuestCartId, CreatedAt, UpdatedAt)
        VALUES (@CartId, @CustomerId, @GuestCartId, GETUTCDATE(), GETUTCDATE());
    END

    -- Check if product already exists in cart
    IF EXISTS (SELECT 1 FROM CartItems WHERE CartId = @CartId AND ProductId = @ProductId)
    BEGIN
        UPDATE CartItems
        SET Quantity = Quantity + @Quantity,
            UpdatedAt = GETUTCDATE()
        WHERE CartId = @CartId AND ProductId = @ProductId;
    END
    ELSE
    BEGIN
        INSERT INTO CartItems (CartId, ProductId, Quantity, CreatedAt, UpdatedAt, SelectedColor)
        VALUES (@CartId, @ProductId, @Quantity, GETUTCDATE(), GETUTCDATE(), @SelectedColor);
    END
    
    -- Return the CartId and GuestCartId (useful if a new guest cart was created)
    SELECT @CartId AS CartId, @GuestCartId AS GuestCartId;
END
GO
