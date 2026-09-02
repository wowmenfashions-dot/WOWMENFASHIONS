namespace wowmenfashions.Services
{
    public interface IRazorpayService
    {
        Task<string> CreateOrderAsync(decimal amountInInr, string receiptId);
    }
}
