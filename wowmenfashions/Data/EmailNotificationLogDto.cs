namespace wowmenfashions.Data;

public class EmailNotificationLogDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string NotificationType { get; set; } = string.Empty;
    public string RecipientEmail { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime SentAt { get; set; }
    public string? ErrorMessage { get; set; }
    public string? OrderStatus { get; set; }
}
