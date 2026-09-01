using System.Threading.Tasks;

namespace wowmenfashions.Services
{
    public interface IOrderService
    {
        Task UpdateOrderStatusAsync(int orderId, string status);
        Task UpdateOrderTrackingAsync(int orderId, string courierName, string trackingNumber, string trackingUrl);
        Task<System.Collections.Generic.IEnumerable<wowmenfashions.Models.OrderDto>> GetAllOrdersAsync();
    }
}
