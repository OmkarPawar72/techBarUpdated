using Microsoft.AspNetCore.Mvc;
using techBar.Data.Cart;
using techBar.Data.Services;
using techBar.Data.ViewModels;
using techBar.Models;

namespace techBar.Controllers
{
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
            string userId = "";
            var orders = await _ordersService.GetOrdersByUserIdAsync(userId);
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

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _productsCategoryService.GetCategoryIdAsync(id);

            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
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
            string userId = "";
            string userEmailAddress = "";

            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();
            return View("OrderCompleted");
        }

    }
}
