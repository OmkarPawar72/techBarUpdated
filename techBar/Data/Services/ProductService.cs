using Microsoft.EntityFrameworkCore;
using techBar.Data.Base;
using techBar.Models;


namespace techBar.Data.Services
{
    public class ProductService : EntityBaseRepository<Product>,IProductService
    {

        public ProductService(EcomDbContext context) : base(context)
        {

        }

    }
}
