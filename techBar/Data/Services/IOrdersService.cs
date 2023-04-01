using techBar.Models;

namespace techBar.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);

        Task<List<Orders>> GetOrdersByUserIdAndRoleAsync(string userId,string userRole);
    }
}
