using System.Data;
using Azure;
using Azure.Communication.Email;
using Dapper;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using wowmenfashions.Data;
using wowmenfashions.Models;
using wowmenfashions.Services;

namespace wowmenfashions.Services;

public class EmailNotificationService : IEmailNotificationService
{
    private readonly IConfiguration _configuration;
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IEncryptionService _encryptionService;
    private readonly IEmailNotificationLogRepository _logRepository;
    private readonly ILogger<EmailNotificationService> _logger;

    public EmailNotificationService(
        IConfiguration configuration,
        ISqlConnectionFactory connectionFactory,
        IEncryptionService encryptionService,
        IEmailNotificationLogRepository logRepository,
        ILogger<EmailNotificationService> logger)
    {
        _configuration = configuration;
        _connectionFactory = connectionFactory;
        _encryptionService = encryptionService;
        _logRepository = logRepository;
        _logger = logger;
    }

    // ────────────────────────────────────────────────────────
    // US1: Order Confirmation Email
    // ────────────────────────────────────────────────────────
    public async Task SendOrderConfirmationAsync(int orderId)
    {
        try
        {
            var order = await GetOrderByIdAsync(orderId);
            if (order == null)
            {
                _logger.LogWarning("SendOrderConfirmationAsync: Order {OrderId} not found. Skipping email.", orderId);
                return;
            }

            if (string.IsNullOrWhiteSpace(order.CustomerEmail))
            {
                _logger.LogWarning("SendOrderConfirmationAsync: Order {OrderId} has no customer email. Skipping.", orderId);
                return;
            }

            DecryptOrderPii(order);

            var subject = $"Your WOWMENFASHIONS Order #{order.Id} is Confirmed! 🎉";
            var htmlBody = BuildOrderConfirmationHtml(order);

            await SendEmailAsync(order.CustomerEmail, subject, htmlBody);

            await _logRepository.LogAsync(
                orderId: order.Id,
                notificationType: "OrderConfirmation",
                recipientEmail: order.CustomerEmail,
                status: "Sent");

            _logger.LogInformation("Order confirmation email sent for Order {OrderId} to {Email}", order.Id, order.CustomerEmail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send order confirmation email for Order {OrderId}", orderId);
            try
            {
                await _logRepository.LogAsync(
                    orderId: orderId,
                    notificationType: "OrderConfirmation",
                    recipientEmail: "unknown",
                    status: "Failed",
                    errorMessage: ex.Message);
            }
            catch (Exception logEx)
            {
                _logger.LogError(logEx, "Failed to log email notification failure for Order {OrderId}", orderId);
            }
        }
    }

    // ────────────────────────────────────────────────────────
    // US2: Order Status Update Email
    // ────────────────────────────────────────────────────────
    public async Task SendOrderStatusUpdateAsync(int orderId, string newStatus)
    {
        try
        {
            var order = await GetOrderByIdAsync(orderId);
            if (order == null)
            {
                _logger.LogWarning("SendOrderStatusUpdateAsync: Order {OrderId} not found. Skipping email.", orderId);
                return;
            }

            if (string.IsNullOrWhiteSpace(order.CustomerEmail))
            {
                _logger.LogWarning("SendOrderStatusUpdateAsync: Order {OrderId} has no customer email. Skipping.", orderId);
                return;
            }

            DecryptOrderPii(order);

            var subject = newStatus switch
            {
                "Shipped"   => $"Your WOWMENFASHIONS Order #{order.Id} has been Shipped! 📦",
                "Delivered" => $"Your WOWMENFASHIONS Order #{order.Id} has been Delivered! 🎉",
                "Cancelled" => $"Update on your WOWMENFASHIONS Order #{order.Id}",
                _           => $"Update on your WOWMENFASHIONS Order #{order.Id} — {newStatus}"
            };

            var htmlBody = BuildOrderStatusUpdateHtml(order, newStatus);

            await SendEmailAsync(order.CustomerEmail, subject, htmlBody);

            await _logRepository.LogAsync(
                orderId: order.Id,
                notificationType: "StatusUpdate",
                recipientEmail: order.CustomerEmail,
                status: "Sent",
                orderStatus: newStatus);

            _logger.LogInformation("Status update email sent for Order {OrderId} (status: {Status}) to {Email}", order.Id, newStatus, order.CustomerEmail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send status update email for Order {OrderId} (status: {Status})", orderId, newStatus);
            try
            {
                await _logRepository.LogAsync(
                    orderId: orderId,
                    notificationType: "StatusUpdate",
                    recipientEmail: "unknown",
                    status: "Failed",
                    orderStatus: newStatus,
                    errorMessage: ex.Message);
            }
            catch (Exception logEx)
            {
                _logger.LogError(logEx, "Failed to log status update email failure for Order {OrderId}", orderId);
            }
        }
    }

    // ────────────────────────────────────────────────────────
    // Private: Send via ACS
    // ────────────────────────────────────────────────────────
    private async Task SendEmailAsync(string toAddress, string subject, string htmlBody)
    {
        var connectionString = _configuration["AzureCommunicationServices:ConnectionString"];
        var senderAddress    = _configuration["AzureCommunicationServices:SenderAddress"];

        if (string.IsNullOrWhiteSpace(connectionString) || string.IsNullOrWhiteSpace(senderAddress))
        {
            _logger.LogWarning("Azure Communication Services not configured. Email to {To} skipped.", toAddress);
            return;
        }

        var emailClient = new EmailClient(connectionString);
        var emailContent = new EmailContent(subject)
        {
            Html = htmlBody
        };

        var emailMessage = new EmailMessage(
            senderAddress: senderAddress,
            recipientAddress: toAddress,
            content: emailContent);

        emailMessage.Recipients.CC.Add(new EmailAddress("wowmenfashions@gmail.com"));

        await emailClient.SendAsync(WaitUntil.Started, emailMessage);
    }

    // ────────────────────────────────────────────────────────
    // Private: Fetch order from DB
    // ────────────────────────────────────────────────────────
    private async Task<OrderDto?> GetOrderByIdAsync(int orderId)
    {
        using var connection = _connectionFactory.CreateConnection();

        var order = await connection.QuerySingleOrDefaultAsync<OrderDto>(
            "SELECT * FROM Orders WHERE Id = @Id",
            new { Id = orderId });

        if (order == null) return null;

        var items = await connection.QueryAsync<OrderItemDto>(
            "dbo.OrderItem_GetByOrderId",
            new { OrderId = orderId },
            commandType: CommandType.StoredProcedure);

        order.Items = items.AsList();
        return order;
    }

    // ────────────────────────────────────────────────────────
    // Private: Decrypt PII fields
    // ────────────────────────────────────────────────────────
    private void DecryptOrderPii(OrderDto order)
    {
        try { if (!string.IsNullOrEmpty(order.ShippingAddressLine1)) order.ShippingAddressLine1 = _encryptionService.Decrypt(order.ShippingAddressLine1); } catch { }
        try { if (!string.IsNullOrEmpty(order.ShippingAddressLine2)) order.ShippingAddressLine2 = _encryptionService.Decrypt(order.ShippingAddressLine2); } catch { }
        try { if (!string.IsNullOrEmpty(order.ShippingPostalCode))   order.ShippingPostalCode   = _encryptionService.Decrypt(order.ShippingPostalCode);   } catch { }
        try { if (!string.IsNullOrEmpty(order.ShippingContactNumber)) order.ShippingContactNumber = _encryptionService.Decrypt(order.ShippingContactNumber); } catch { }
        try { if (!string.IsNullOrEmpty(order.ShippingLandmark))      order.ShippingLandmark      = _encryptionService.Decrypt(order.ShippingLandmark);      } catch { }
    }

    // ────────────────────────────────────────────────────────
    // Private: HTML Templates
    // ────────────────────────────────────────────────────────
    private string BuildOrderConfirmationHtml(OrderDto order)
    {
        var itemsHtml = string.Join("", order.Items.Select(i => $@"
            <tr>
                <td style='padding:10px 8px;border-bottom:1px solid #2a2a2a;color:#e0e0e0;'>{i.ProductName}{(i.SelectedSize != null ? $" ({i.SelectedSize})" : "")}{(i.SelectedColor != null ? $" — {i.SelectedColor}" : "")}</td>
                <td style='padding:10px 8px;border-bottom:1px solid #2a2a2a;color:#e0e0e0;text-align:center;'>{i.Quantity}</td>
                <td style='padding:10px 8px;border-bottom:1px solid #2a2a2a;color:#e0e0e0;text-align:right;'>₹{i.Price:N2}</td>
            </tr>"));

        return $@"
<!DOCTYPE html>
<html>
<head><meta charset='UTF-8'><meta name='viewport' content='width=device-width,initial-scale=1'></head>
<body style='margin:0;padding:0;background:#111111;font-family:Arial,Helvetica,sans-serif;'>
  <table width='100%' cellpadding='0' cellspacing='0' style='background:#111111;'>
    <tr><td align='center' style='padding:40px 20px;'>
      <table width='600' cellpadding='0' cellspacing='0' style='background:#1a1a1a;border-radius:12px;overflow:hidden;max-width:600px;width:100%;'>

        <!-- Header -->
        <tr><td style='background:linear-gradient(135deg,#1a1a1a 0%,#2a2a2a 100%);padding:36px 40px;text-align:center;border-bottom:3px solid #c9a84c;'>
          <h1 style='margin:0;color:#c9a84c;font-size:28px;letter-spacing:4px;font-weight:800;'>WOWMEN</h1>
          <p style='margin:8px 0 0;color:#999;font-size:13px;letter-spacing:2px;text-transform:uppercase;'>Fashion & Style</p>
        </td></tr>

        <!-- Body -->
        <tr><td style='padding:36px 40px;'>
          <h2 style='color:#c9a84c;font-size:22px;margin:0 0 8px;'>Order Confirmed! 🎉</h2>
          <p style='color:#aaa;margin:0 0 24px;font-size:15px;'>Thank you, {order.CustomerName}! Your order has been placed successfully.</p>

          <div style='background:#222;border-radius:8px;padding:16px 20px;margin-bottom:24px;'>
            <p style='margin:0;color:#888;font-size:13px;text-transform:uppercase;letter-spacing:1px;'>Order Number</p>
            <p style='margin:4px 0 0;color:#c9a84c;font-size:24px;font-weight:700;'>#{order.Id}</p>
          </div>

          <!-- Items Table -->
          <table width='100%' cellpadding='0' cellspacing='0' style='margin-bottom:24px;'>
            <tr style='background:#252525;'>
              <th style='padding:10px 8px;color:#888;font-size:12px;text-transform:uppercase;text-align:left;letter-spacing:1px;'>Item</th>
              <th style='padding:10px 8px;color:#888;font-size:12px;text-transform:uppercase;text-align:center;letter-spacing:1px;'>Qty</th>
              <th style='padding:10px 8px;color:#888;font-size:12px;text-transform:uppercase;text-align:right;letter-spacing:1px;'>Price</th>
            </tr>
            {itemsHtml}
            <tr style='background:#252525;'>
              <td colspan='2' style='padding:12px 8px;color:#c9a84c;font-weight:700;font-size:15px;'>Total</td>
              <td style='padding:12px 8px;color:#c9a84c;font-weight:700;font-size:15px;text-align:right;'>₹{order.TotalAmount:N2}</td>
            </tr>
          </table>

          <!-- Shipping Address -->
          <div style='background:#222;border-radius:8px;padding:16px 20px;margin-bottom:24px;'>
            <p style='margin:0 0 8px;color:#888;font-size:12px;text-transform:uppercase;letter-spacing:1px;'>Shipping To</p>
            <p style='margin:0;color:#e0e0e0;font-size:14px;line-height:1.6;'>
              {order.ShippingAddressLine1}{(string.IsNullOrEmpty(order.ShippingAddressLine2) ? "" : $", {order.ShippingAddressLine2}")}<br>
              {order.ShippingCity}, {order.ShippingState} — {order.ShippingPostalCode}<br>
              {order.ShippingCountry}
            </p>
          </div>

          <p style='color:#888;font-size:14px;margin:0;'>We'll send you another email once your order ships. 📦</p>
        </td></tr>

        <!-- Footer -->
        <tr><td style='background:#111;padding:24px 40px;text-align:center;border-top:1px solid #2a2a2a;'>
          <p style='margin:0;color:#555;font-size:12px;'>© 2026 WOWMEN Fashions. All rights reserved.</p>
          <p style='margin:6px 0 0;color:#555;font-size:12px;'>This is a transactional email regarding your order.</p>
        </td></tr>

      </table>
    </td></tr>
  </table>
</body>
</html>";
    }

    private string BuildOrderStatusUpdateHtml(OrderDto order, string status)
    {
        var statusColor = status switch
        {
            "Shipped"    => "#3b82f6",
            "Delivered"  => "#22c55e",
            "Cancelled"  => "#ef4444",
            "Processing" => "#f59e0b",
            _            => "#c9a84c"
        };

        var statusEmoji = status switch
        {
            "Shipped"    => "📦",
            "Delivered"  => "🎉",
            "Cancelled"  => "❌",
            "Processing" => "⚙️",
            _            => "📋"
        };

        var trackingSection = string.Empty;
        if (status == "Shipped" && !string.IsNullOrEmpty(order.TrackingNumber))
        {
            trackingSection = $@"
          <div style='background:#222;border-radius:8px;padding:16px 20px;margin:24px 0;border-left:4px solid #3b82f6;'>
            <p style='margin:0 0 8px;color:#888;font-size:12px;text-transform:uppercase;letter-spacing:1px;'>Tracking Details</p>
            <p style='margin:0 0 4px;color:#e0e0e0;font-size:14px;'><strong style='color:#aaa;'>Courier:</strong> {order.CourierName}</p>
            <p style='margin:0 0 4px;color:#e0e0e0;font-size:14px;'><strong style='color:#aaa;'>Tracking #:</strong> {order.TrackingNumber}</p>
            {(string.IsNullOrEmpty(order.TrackingUrl) ? "" : $"<p style='margin:8px 0 0;'><a href='{order.TrackingUrl}' style='color:#3b82f6;font-size:14px;'>Track your shipment →</a></p>")}
          </div>";
        }

        var reviewSection = status == "Delivered" ? @"
          <div style='text-align:center;margin:24px 0;'>
            <p style='color:#aaa;font-size:14px;margin:0 0 12px;'>Enjoying your purchase? Share your thoughts!</p>
            <a href='https://wowmenfashions.com' style='display:inline-block;background:#c9a84c;color:#111;padding:12px 28px;border-radius:6px;text-decoration:none;font-weight:700;font-size:14px;'>Leave a Review</a>
          </div>" : string.Empty;

        return $@"
<!DOCTYPE html>
<html>
<head><meta charset='UTF-8'><meta name='viewport' content='width=device-width,initial-scale=1'></head>
<body style='margin:0;padding:0;background:#111111;font-family:Arial,Helvetica,sans-serif;'>
  <table width='100%' cellpadding='0' cellspacing='0' style='background:#111111;'>
    <tr><td align='center' style='padding:40px 20px;'>
      <table width='600' cellpadding='0' cellspacing='0' style='background:#1a1a1a;border-radius:12px;overflow:hidden;max-width:600px;width:100%;'>

        <!-- Header -->
        <tr><td style='background:linear-gradient(135deg,#1a1a1a 0%,#2a2a2a 100%);padding:36px 40px;text-align:center;border-bottom:3px solid #c9a84c;'>
          <h1 style='margin:0;color:#c9a84c;font-size:28px;letter-spacing:4px;font-weight:800;'>WOWMEN</h1>
          <p style='margin:8px 0 0;color:#999;font-size:13px;letter-spacing:2px;text-transform:uppercase;'>Fashion & Style</p>
        </td></tr>

        <!-- Body -->
        <tr><td style='padding:36px 40px;'>
          <h2 style='color:#e0e0e0;font-size:22px;margin:0 0 8px;'>Order Update {statusEmoji}</h2>
          <p style='color:#aaa;margin:0 0 24px;font-size:15px;'>Hi {order.CustomerName}, here's an update on your order.</p>

          <div style='background:#222;border-radius:8px;padding:16px 20px;margin-bottom:24px;display:flex;align-items:center;gap:16px;'>
            <div>
              <p style='margin:0;color:#888;font-size:13px;text-transform:uppercase;letter-spacing:1px;'>Order #{order.Id}</p>
              <p style='margin:6px 0 0;'>
                <span style='background:{statusColor}22;color:{statusColor};padding:4px 12px;border-radius:20px;font-size:13px;font-weight:700;border:1px solid {statusColor}44;'>
                  {status.ToUpper()}
                </span>
              </p>
            </div>
          </div>

          {trackingSection}
          {reviewSection}

          {(status == "Cancelled" ? "<div style='background:#1a0a0a;border-radius:8px;padding:16px 20px;border-left:4px solid #ef4444;'><p style='margin:0;color:#aaa;font-size:14px;'>If you have any questions about this cancellation, please contact our support team.</p></div>" : "")}
        </td></tr>

        <!-- Footer -->
        <tr><td style='background:#111;padding:24px 40px;text-align:center;border-top:1px solid #2a2a2a;'>
          <p style='margin:0;color:#555;font-size:12px;'>© 2026 WOWMEN Fashions. All rights reserved.</p>
          <p style='margin:6px 0 0;color:#555;font-size:12px;'>This is a transactional email regarding your order.</p>
        </td></tr>

      </table>
    </td></tr>
  </table>
</body>
</html>";
    }
}
