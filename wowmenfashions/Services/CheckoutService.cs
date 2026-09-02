using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using wowmenfashions.Models;
using System.Linq;

namespace wowmenfashions.Services
{
    public interface ICheckoutService
    {
        Task<int> CreatePendingOrderAsync(OrderDto order);
        Task<bool> VerifyAndCompleteOrderAsync(int orderId, string razorpayOrderId, string razorpayPaymentId, string signature);
        Task<bool> ProcessPaymentAsync(CheckoutModel paymentDetails);
    }

    public class CheckoutService : ICheckoutService
    {
        private readonly string _connectionString;
        private readonly IEncryptionService _encryptionService;
        private readonly ILogger<CheckoutService> _logger;
        private readonly IRazorpayService _razorpayService;

        public CheckoutService(IConfiguration configuration, IEncryptionService encryptionService, ILogger<CheckoutService> logger, IRazorpayService razorpayService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? throw new InvalidOperationException("DefaultConnection string not found.");
            _encryptionService = encryptionService;
            _logger = logger;
            _razorpayService = razorpayService;
        }

        public async Task<int> CreatePendingOrderAsync(OrderDto order)
        {
            _logger.LogInformation("Creating pending order for {CustomerEmail}", order.CustomerEmail);
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();

            try
            {
                var p = new DynamicParameters();
                p.Add("@Subtotal", order.Subtotal);
                p.Add("@ShippingFee", order.ShippingFee);
                p.Add("@TaxAmount", order.TaxAmount);
                p.Add("@DiscountAmount", order.DiscountAmount);
                p.Add("@TotalAmount", order.TotalAmount);
                p.Add("@Status", "Pending");
                p.Add("@CustomerName", order.CustomerName);
                p.Add("@CustomerEmail", order.CustomerEmail);
                p.Add("@ShippingAddressLine1", _encryptionService.Encrypt(order.ShippingAddressLine1));
                p.Add("@ShippingAddressLine2", order.ShippingAddressLine2 != null ? _encryptionService.Encrypt(order.ShippingAddressLine2) : null);
                p.Add("@ShippingCity", order.ShippingCity);
                p.Add("@ShippingState", order.ShippingState);
                p.Add("@ShippingCountry", order.ShippingCountry);
                p.Add("@ShippingPostalCode", _encryptionService.Encrypt(order.ShippingPostalCode));
                p.Add("@ShippingContactNumber", _encryptionService.Encrypt(order.ShippingContactNumber));
                p.Add("@ShippingLandmark", order.ShippingLandmark != null ? _encryptionService.Encrypt(order.ShippingLandmark) : null);
                p.Add("@RazorpayOrderId", order.RazorpayOrderId);
                p.Add("@RazorpayPaymentId", (string?)null);
                p.Add("@PaymentStatus", "Pending");
                p.Add("@OrderId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(
                    "dbo.Order_Create", 
                    p, 
                    transaction: transaction,
                    commandType: CommandType.StoredProcedure);

                int newOrderId = p.Get<int>("@OrderId");

                foreach (var item in order.Items)
                {
                    await connection.ExecuteAsync(
                        "dbo.OrderItem_Create",
                        new {
                            OrderId = newOrderId,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            SelectedColor = item.SelectedColor
                        },
                        transaction: transaction,
                        commandType: CommandType.StoredProcedure);
                }

                transaction.Commit();
                _logger.LogInformation("Pending order {OrderId} created successfully", newOrderId);
                return newOrderId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create pending order");
                transaction.Rollback();
                throw;
            }
        }

        public async Task<bool> VerifyAndCompleteOrderAsync(int orderId, string razorpayOrderId, string razorpayPaymentId, string signature)
        {
            _logger.LogInformation("Verifying and completing order {OrderId}", orderId);
            
            bool isSignatureValid = _razorpayService.VerifyPaymentSignature(razorpayOrderId, razorpayPaymentId, signature);
            if (!isSignatureValid)
            {
                _logger.LogWarning("Invalid payment signature for order {OrderId}. Transaction aborted.", orderId);
                return false;
            }

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            try
            {
                await connection.ExecuteAsync(
                    "dbo.Order_UpdatePayment",
                    new {
                        OrderId = orderId,
                        RazorpayOrderId = razorpayOrderId,
                        RazorpayPaymentId = razorpayPaymentId,
                        RazorpaySignature = signature,
                        PaymentStatus = "Captured",
                        Status = "PaymentCompleted"
                    },
                    commandType: CommandType.StoredProcedure);

                _logger.LogInformation("Order {OrderId} completed successfully", orderId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update payment status for order {OrderId}", orderId);
                throw;
            }
        }

        public async Task<bool> ProcessPaymentAsync(CheckoutModel paymentDetails)
        {
            // Dummy payment simulation
            await Task.Delay(2000); 
            // In a real app we'd call Stripe/PayPal API here.
            return true; 
        }
    }
}
