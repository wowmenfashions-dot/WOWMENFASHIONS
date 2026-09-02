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
        Task<int> PlaceOrderAsync(OrderDto order);
        Task<bool> ProcessPaymentAsync(CheckoutModel paymentDetails);
    }

    public class CheckoutService : ICheckoutService
    {
        private readonly string _connectionString;
        private readonly IEncryptionService _encryptionService;

        public CheckoutService(IConfiguration configuration, IEncryptionService encryptionService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? throw new InvalidOperationException("DefaultConnection string not found.");
            _encryptionService = encryptionService;
        }

        public async Task<int> PlaceOrderAsync(OrderDto order)
        {
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
                p.Add("@Status", order.Status);
                p.Add("@CustomerName", order.CustomerName);
                p.Add("@CustomerEmail", order.CustomerEmail);
                p.Add("@ShippingAddress", _encryptionService.Encrypt(order.ShippingAddress));
                p.Add("@RazorpayOrderId", order.RazorpayOrderId);
                p.Add("@RazorpayPaymentId", order.RazorpayPaymentId);
                p.Add("@PaymentStatus", order.PaymentStatus);
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
                return newOrderId;
            }
            catch
            {
                transaction.Rollback();
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
