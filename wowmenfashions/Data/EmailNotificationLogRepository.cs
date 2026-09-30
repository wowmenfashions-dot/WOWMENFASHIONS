using System.Data;
using Dapper;
using wowmenfashions.Data;

namespace wowmenfashions.Data;

public interface IEmailNotificationLogRepository
{
    Task LogAsync(int orderId, string notificationType, string recipientEmail, string status, string? orderStatus = null, string? errorMessage = null);
    Task<IEnumerable<EmailNotificationLogDto>> GetByOrderIdAsync(int orderId);
}

public class EmailNotificationLogRepository : IEmailNotificationLogRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public EmailNotificationLogRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task LogAsync(
        int orderId,
        string notificationType,
        string recipientEmail,
        string status,
        string? orderStatus = null,
        string? errorMessage = null)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            INSERT INTO EmailNotificationLogs 
                (OrderId, NotificationType, RecipientEmail, Status, SentAt, ErrorMessage, OrderStatus)
            VALUES 
                (@OrderId, @NotificationType, @RecipientEmail, @Status, GETUTCDATE(), @ErrorMessage, @OrderStatus)";

        await connection.ExecuteAsync(sql, new
        {
            OrderId = orderId,
            NotificationType = notificationType,
            RecipientEmail = recipientEmail,
            Status = status,
            ErrorMessage = errorMessage,
            OrderStatus = orderStatus
        });
    }

    public async Task<IEnumerable<EmailNotificationLogDto>> GetByOrderIdAsync(int orderId)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            SELECT Id, OrderId, NotificationType, RecipientEmail, Status, SentAt, ErrorMessage, OrderStatus
            FROM EmailNotificationLogs
            WHERE OrderId = @OrderId
            ORDER BY SentAt DESC";

        return await connection.QueryAsync<EmailNotificationLogDto>(sql, new { OrderId = orderId });
    }
}
