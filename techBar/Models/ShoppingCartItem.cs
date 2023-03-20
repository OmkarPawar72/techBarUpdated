using System.ComponentModel.DataAnnotations;

namespace techBar.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int ShoppingcartitemId { get; set; }

        public ProductsCategory ProductsCategory { get; set; }

        public int Amount { get; set; }

        public string ShoppingCartId { get; set; }
    }
}
