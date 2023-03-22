using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace techBar.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderitemId { get; set; }

        public int Amount { get; set; }

        public double Price { get; set; }

        public int ProductsCategoryId { get; set;}

        [ForeignKey("ProductsCategoryId")]
        public ProductsCategory Productscategory { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Orders orders { get; set; }
    }
}
