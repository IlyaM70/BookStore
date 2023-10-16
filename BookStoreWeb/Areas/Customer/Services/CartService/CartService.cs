using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.RepositoryInterface;
using BookStore.Models;
using System.Web.Helpers;

namespace BookStore.Web.Areas.Customer.Services.CartService
{
    public class CartService: ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        public string CartId { get; set; }
        public CartService(IUnitOfWork unitOfWork, IHttpContextAccessor context)
        {
            _unitOfWork = unitOfWork;
            CartId = context.HttpContext.Session.GetString("CardId");
        }

        public async Task<int> AddToCartAsync(Product product)
        {
            // Get the matching cart and product instances
            var cartItem = _unitOfWork.CartItem.Get(
                c => c.CartId == CartId
                && c.ProductId == product.Id);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new CartItem
                {
                    ProductId = product.Id,
                    CartId = CartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                _unitOfWork.CartItem.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.Count++;
            }
            // Save changes
            _unitOfWork.Save();
            return cartItem.Count;
        }

        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = _unitOfWork.CartItem.Get(
                cart => cart.CartId == CartId
                && cart.Id == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    _unitOfWork.CartItem.Remove(cartItem);
                }
                // Save changes
                _unitOfWork.Save();
            }
            return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = _unitOfWork.CartItem.GetAll(cart => cart.CartId == CartId);

            foreach (var cartItem in cartItems)
            {
                _unitOfWork.CartItem.Remove(cartItem);
            }
            // Save changes
            _unitOfWork.Save();
        }
        public async Task<List<CartItem>> GetCartItemsAsync()
        {
            return _unitOfWork.CartItem
                .GetAll(cart => cart.CartId == CartId, "Product").ToList();
        }
        public async Task<int> GetCountAsync()
        {
            // Get the count of each item in the cart and sum them up
            int? count =  (from cartItems in _unitOfWork.CartItem
                           .GetAll(c=>c.CartId==CartId)
                            select (int?)cartItems.Count).Sum();
            

            // Return 0 if all entries are null
            return count ?? 0;
        }
        public async Task<decimal> GetTotalAsync()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (decimal?)(from cartItems in _unitOfWork.CartItem
                                        .GetAll(c => c.CartId == CartId)
                                        select (int?)cartItems.Count *
                                        cartItems.Product.Price).Sum();

            return total ?? decimal.Zero;
        }
        public async Task<CartItem> GetCartItemAsync(int id)
        {
            return  _unitOfWork.CartItem.Get(c => c.Id == id);
        }

        public async Task<int> CreateOrderAsync(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItemsAsync();
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems.Result)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    OrderId = order.Id,
                    UnitPrice = (decimal)item.Product.Price,
                    Quantity = item.Count
                };
                // Set the order total of the shopping cart
                orderTotal += (decimal)(item.Count * item.Product.Price);

                //await _unitOfWork.OrderDetails.AddAsync(orderDetail);

            }
            // Set the order's total to the orderTotal count
            order.TotalPrice = orderTotal;

            // Save the order
            _unitOfWork.Save();
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return order.Id;
        }

        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = _unitOfWork.CartItem.GetAll().Where(
                c => c.CartId == CartId);

            foreach (CartItem item in shoppingCart)
            {
                item.CartId = userName;
            }
            _unitOfWork.Save();
        }

    }
}

