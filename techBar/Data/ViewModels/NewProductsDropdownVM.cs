using techBar.Models;

namespace techBar.Data.ViewModels
{
    public class NewProductsDropdownVM
    {

        public NewProductsDropdownVM()
        {
            Manufacturers = new List<Manufacturer>();
            Seller = new List<Sellers>();
            Products = new List<Product>();
        }

        public List<Manufacturer> Manufacturers { get; set; }

        public List<Sellers> Seller { get; set; }

        public List<Product> Products { get; set; }
    }
}
