using Razorpay.Api;

namespace wowmenfashions.Services
{
    public class RazorpayService : IRazorpayService
    {
        private readonly string _keyId;
        private readonly string _keySecret;

        public RazorpayService(IConfiguration configuration)
        {
            _keyId = configuration["Razorpay:KeyId"] ?? throw new InvalidOperationException("Razorpay KeyId not found.");
            _keySecret = configuration["Razorpay:KeySecret"] ?? throw new InvalidOperationException("Razorpay KeySecret not found.");
        }

        public Task<string> CreateOrderAsync(decimal amountInInr, string receiptId)
        {
            try
            {
                RazorpayClient client = new RazorpayClient(_keyId, _keySecret);

                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", (int)(amountInInr * 100)); // amount in the smallest currency unit (paise)
                options.Add("currency", "INR");
                options.Add("receipt", receiptId);
                options.Add("payment_capture", 1);

                Order order = client.Order.Create(options);
                return Task.FromResult(order["id"].ToString());
            }
            catch (Exception ex)
            {
                // In production, log this appropriately
                throw new ApplicationException($"Razorpay order creation failed: {ex.Message}", ex);
            }
        }
    }
}
