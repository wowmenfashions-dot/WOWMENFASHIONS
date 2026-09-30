namespace wowmenfashions.Services;

public interface IEmailNotificationService
{
    Task SendOrderConfirmationAsync(int orderId);
    Task SendOrderStatusUpdateAsync(int orderId, string newStatus);
}
