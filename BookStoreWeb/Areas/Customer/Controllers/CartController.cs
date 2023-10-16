using BookStore.DataAccess.Repository.RepositoryInterface;
using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Web.Areas.Customer.Services.CartService;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookStore.Web.Areas.Customer.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IUnitOfWork _unitOfWork;
        public CartController(ICartService cartService, IUnitOfWork unitOfWork)
        {
            _cartService = cartService;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Cart()
        {
            var viewModel = new CartViewModel
            {
                CartItems = _cartService.GetCartItemsAsync().Result,
                CartTotal = _cartService.GetTotalAsync().Result,
            };
            return View(viewModel);
        }

        public IActionResult CartSummary()
        {
            ViewData["CartCount"] = _cartService.GetCountAsync().Result;
            return PartialView("CartSummary");
        }

        public IActionResult EmptyCart()
        {
            _cartService.EmptyCart();
            TempData["Message"] = "Cart was cleared";

            return RedirectToAction("Cart");
        }

        #region API calls
        public IActionResult AddToCart(int id)
        {
            Product product = _unitOfWork.Product.Get(u => u.Id == id);
            int itemCount = _cartService.AddToCartAsync(product).Result;

            // Display the confirmation message
            var results = new
            {
                Message = $"Book {product.Title} was added to cart",
                CartTotal = _cartService.GetTotalAsync().Result,
                CartCount = _cartService.GetCountAsync().Result,
                ItemCount = itemCount,
                AddedId = _cartService
                        .GetCartItemsAsync().Result
                        .Where(c => c.ProductId == id)
                        .FirstOrDefault().ProductId
            };
            return Json(results);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            int productId = _cartService.GetCartItemAsync(id).Result.ProductId;
            string productName = _unitOfWork.Product.Get(u => u.Id == id).Title;
            int itemCount = _cartService.RemoveFromCart(id);

            // Display the confirmation message
            var results = new
            {
                Message = $"Book {productName} was removed for cart",
                CartTotal = _cartService.GetTotalAsync().Result,
                CartCount = _cartService.GetCountAsync().Result,
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }

        public IActionResult EmptyCartApi()
        {
            _cartService.EmptyCart();
            return Ok();
        }
        #endregion

    }
}

