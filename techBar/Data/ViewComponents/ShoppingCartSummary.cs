using Microsoft.AspNetCore.Mvc;
using techBar.Data.Cart;

namespace techBar.Data.ViewComponents
{
    
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
        
        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var items=_shoppingCart.GetShoppingCartItems();

            return View(items.Count);
        }
    }
}
