
CREATE   PROCEDURE sp_LogAdminAction
    @Action NVARCHAR(255),
    @AdminEmail NVARCHAR(255),
    @EntityId NVARCHAR(255)
AS
BEGIN
    INSERT INTO AdminAuditLogs (Action, AdminEmail, EntityId, Timestamp)
    VALUES (@Action, @AdminEmail, @EntityId, GETDATE())
END