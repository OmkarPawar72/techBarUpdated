using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using techBar.Models;

namespace techBar.Data.Cart
{
    public class ShoppingCart
    {
        public EcomDbContext Context { get; set; }

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        
        public ShoppingCart(EcomDbContext context)
        {
            Context= context;
        } 

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<EcomDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId=cartId};
        }

        public void AddItemToCart(ProductsCategory productsCategory)
        {
            var shoppingCartItem = Context.ShoppingCartItems.FirstOrDefault(n => n.ProductsCategory.Id == productsCategory.Id && 
            n.ShoppingCartId == ShoppingCartId);

            
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    ProductsCategory = productsCategory,
                    Amount = 1,
                };
                Context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            Context.SaveChanges();
        }

        public void RemoveItemFromCart(ProductsCategory productsCategory)
        {
            var shoppingCartItem = Context.ShoppingCartItems.FirstOrDefault(n => n.ProductsCategory.Id == productsCategory.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    Context.ShoppingCartItems.Add(shoppingCartItem);
                }
            }
            Context.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = Context.ShoppingCartItems.Where(n=>n.ShoppingCartId==ShoppingCartId).Include(n => n.ProductsCategory).ToList());
        }

        public double GetShoppingCartTotal() => Context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.ProductsCategory.Price * n.Amount).Sum();
         
        public async Task ClearShoppingCartAsync()
        {
            var items = await Context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
            Context.ShoppingCartItems.RemoveRange(items);
            await Context.SaveChangesAsync();

            ShoppingCartItems = new List<ShoppingCartItem>();
        }
    }
}
