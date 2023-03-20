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

        public OrdersController(IProductsCategoryService productsCategoryService, ShoppingCart shoppingCart)
        {
            _productsCategoryService = productsCategoryService; 
            _shoppingCart = shoppingCart;
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
            var item = await _productsCategoryService.GetCategoryIdAsysnc(id);

            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _productsCategoryService.GetCategoryIdAsysnc(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
    }
}
