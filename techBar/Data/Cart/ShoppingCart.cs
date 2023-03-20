using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using techBar.Models;

namespace techBar.Data.Cart
{
    public class ShoppingCart
    {
        public EcomDbContext _context { get; set; }

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(EcomDbContext context)
        {
            _context= context;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetRequiredService<EcomDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId=cartId};
        }

        public void AddItemToCart(ProductsCategory productsCategory)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.ProductsCategory.Id == productsCategory.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    ProductsCategory = productsCategory,
                    Amount = 1,
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(ProductsCategory productsCategory)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.ProductsCategory.Id == productsCategory.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Add(shoppingCartItem);
                }
            }
            _context.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n=>n.ShoppingCartId==ShoppingCartId).Include(n => n.ProductsCategory).ToList());
        }

        public double GetShoppingCartTotal()=>
            _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.ProductsCategory.Price * n.Amount).Sum();
         
    }
}
