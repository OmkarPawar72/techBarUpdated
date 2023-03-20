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
    }
}
