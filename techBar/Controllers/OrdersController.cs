using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using techBar.Data.Cart;
using techBar.Data.Services;
using techBar.Data.Static;
using techBar.Data.ViewModels;
using techBar.Models;

namespace techBar.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IProductsCategoryService _productsCategoryService;

        private readonly ShoppingCart _shoppingCart;

        private readonly IOrdersService _ordersService;

        public OrdersController(IProductsCategoryService productsCategoryService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _productsCategoryService = productsCategoryService; 
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }

        public async Task <IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId,userRole);
            return View(orders);
        }


        public IActionResult ShoppingCart()
        {
            
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }

        public async Task<RedirectToActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _productsCategoryService.GetCategoryIdAsync(id);

            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<RedirectToActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _productsCategoryService.GetCategoryIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            

            return View("OrderCompleted");
        }

    }
}
