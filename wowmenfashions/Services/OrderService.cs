using System.Data;
using System.Threading.Tasks;
using Dapper;
using wowmenfashions.Data;

namespace wowmenfashions.Services
{
    public class OrderService : IOrderService
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IEncryptionService _encryptionService;

        public OrderService(ISqlConnectionFactory sqlConnectionFactory, IEncryptionService encryptionService)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _encryptionService = encryptionService;
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(
                "dbo.Order_UpdateStatus",
                new { OrderId = orderId, Status = status },
                commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateOrderTrackingAsync(int orderId, string courierName, string trackingNumber, string trackingUrl)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(
                "dbo.Order_UpdateTracking",
                new { OrderId = orderId, CourierName = courierName, TrackingNumber = trackingNumber, TrackingUrl = trackingUrl },
                commandType: CommandType.StoredProcedure);
        }
        public async Task<System.Collections.Generic.IEnumerable<wowmenfashions.Models.OrderDto>> GetAllOrdersAsync()
        {
            using var connection = _sqlConnectionFactory.CreateConnection();
            var orders = (await connection.QueryAsync<wowmenfashions.Models.OrderDto>(
                "dbo.Order_GetAll",
                commandType: CommandType.StoredProcedure)).AsList();

            foreach(var order in orders)
            {
                var items = await connection.QueryAsync<wowmenfashions.Models.OrderItemDto>(
                    "dbo.OrderItem_GetByOrderId",
                    new { OrderId = order.Id },
                    commandType: CommandType.StoredProcedure);
                order.Items = items.AsList();
                
                // Decrypt PII
                if (!string.IsNullOrEmpty(order.ShippingAddress))
                {
                    order.ShippingAddress = _encryptionService.Decrypt(order.ShippingAddress);
                }
            }

            return orders;
        }
    }
}
