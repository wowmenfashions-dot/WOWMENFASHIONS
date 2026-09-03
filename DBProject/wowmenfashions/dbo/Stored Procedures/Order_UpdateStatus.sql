CREATE PROCEDURE [dbo].[Order_UpdateStatus]
    @OrderId INT,
    @Status NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Orders
    SET Status = @Status
    WHERE Id = @OrderId;
END