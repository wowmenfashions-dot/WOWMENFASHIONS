using Razorpay.Api;

namespace wowmenfashions.Services
{
    public class RazorpayService : IRazorpayService
    {
        private readonly string _keyId;
        private readonly string _keySecret;
        private readonly ILogger<RazorpayService> _logger;

        public RazorpayService(IConfiguration configuration, ILogger<RazorpayService> logger)
        {
            _keyId = configuration["Razorpay:KeyId"] ?? throw new InvalidOperationException("Razorpay KeyId not found.");
            _keySecret = configuration["Razorpay:KeySecret"] ?? throw new InvalidOperationException("Razorpay KeySecret not found.");
            _logger = logger;
        }

        public Task<string> CreateOrderAsync(decimal amountInInr, string receiptId)
        {
            _logger.LogInformation("Creating Razorpay order for receipt {ReceiptId} with amount {AmountInInr}", receiptId, amountInInr);
            try
            {
                RazorpayClient client = new RazorpayClient(_keyId, _keySecret);

                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", (int)(amountInInr * 100)); // amount in the smallest currency unit (paise)
                options.Add("currency", "INR");
                options.Add("receipt", receiptId);
                options.Add("payment_capture", 1);

                Order order = client.Order.Create(options);
                string orderId = order["id"].ToString() ?? "";
                
                _logger.LogInformation("Razorpay order {OrderId} created successfully for receipt {ReceiptId}", orderId, receiptId);
                return Task.FromResult(orderId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Razorpay order creation failed for receipt {ReceiptId}", receiptId);
                throw new ApplicationException($"Razorpay order creation failed: {ex.Message}", ex);
            }
        }

        public bool VerifyPaymentSignature(string orderId, string paymentId, string signature)
        {
            try
            {
                _logger.LogInformation("Verifying Razorpay payment signature for OrderId: {OrderId}, PaymentId: {PaymentId}", orderId, paymentId);
                
                Dictionary<string, string> attributes = new Dictionary<string, string>();
                attributes.Add("razorpay_order_id", orderId);
                attributes.Add("razorpay_payment_id", paymentId);
                attributes.Add("razorpay_signature", signature);

                Utils.verifyPaymentSignature(attributes);
                
                _logger.LogInformation("Razorpay payment signature verified successfully for OrderId: {OrderId}", orderId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Razorpay payment signature verification failed for OrderId: {OrderId}", orderId);
                return false;
            }
        }
    }
}
