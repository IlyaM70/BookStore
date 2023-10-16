using BookStore.Models;
using System.Web.Helpers;

namespace BookStore.Web.Areas.Customer.Services.CartService
{
    public interface ICartService
    {
        public string CartId { get; set; }
        public Task<int> AddToCartAsync(Product product);
        public int RemoveFromCart(int id);
        public void EmptyCart();
        public Task<List<CartItem>> GetCartItemsAsync();
        public Task<CartItem> GetCartItemAsync(int id);
        public Task<int> GetCountAsync();
        public Task<decimal> GetTotalAsync();
        public Task<int> CreateOrderAsync(Order order);
        public void MigrateCart(string userName);
    }
}
