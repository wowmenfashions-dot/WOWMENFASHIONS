using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using wowmenfashions.Services;
using wowmenfashions.Data;
using Dapper;

namespace wowmenfashions.Controllers
{
    [ApiController]
    [Route("api/webhooks")]
    public class WebhookController : ControllerBase
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly ILogger<WebhookController> _logger;

        public WebhookController(ISqlConnectionFactory sqlConnectionFactory, ILogger<WebhookController> logger)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _logger = logger;
        }

        [HttpPost("razorpay")]
        public async Task<IActionResult> HandleRazorpayWebhook([FromBody] JsonElement payload)
        {
            try
            {
                var eventName = payload.GetProperty("event").GetString();
                var paymentEntity = payload.GetProperty("payload").GetProperty("payment").GetProperty("entity");
                var orderId = paymentEntity.GetProperty("order_id").GetString();

                if (string.IsNullOrEmpty(orderId))
                {
                    return BadRequest("Order ID missing from payload");
                }

                using var connection = _sqlConnectionFactory.CreateConnection();
                var dbOrder = await connection.QueryFirstOrDefaultAsync<int?>(
                    "SELECT Id FROM Orders WHERE RazorpayOrderId = @RazorpayOrderId", 
                    new { RazorpayOrderId = orderId });

                if (dbOrder == null)
                {
                    _logger.LogWarning($"Webhook received for unknown Razorpay Order ID: {orderId}");
                    return Ok(); // Acknowledge receipt even if not found
                }

                if (eventName == "payment.captured")
                {
                    await connection.ExecuteAsync(
                        "UPDATE Orders SET PaymentStatus = 'Captured', Status = 'PaymentCompleted' WHERE RazorpayOrderId = @RazorpayOrderId",
                        new { RazorpayOrderId = orderId });
                    
                    _logger.LogInformation($"Order {dbOrder} payment captured via Webhook.");
                }
                else if (eventName == "payment.failed")
                {
                    await connection.ExecuteAsync(
                        "UPDATE Orders SET PaymentStatus = 'Failed', Status = 'PaymentFailed' WHERE RazorpayOrderId = @RazorpayOrderId",
                        new { RazorpayOrderId = orderId });
                    
                    _logger.LogWarning($"Order {dbOrder} payment failed via Webhook.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing Razorpay webhook");
                return StatusCode(500, "Internal Server Error processing webhook");
            }
        }
    }
}
