using Microsoft.EntityFrameworkCore;
using techBar.Data.Base;
using techBar.Data.Enums;
using techBar.Models;

namespace techBar.Data.Services
{
    public class ProductsCategoryService : EntityBaseRepository<ProductsCategory>, IProductsCategoryService
    {

        private readonly EcomDbContext _context;

        public ProductsCategoryService(EcomDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<ProductsCategory> GetProductsCategoryIdAsysnc(int id)
        {
            var productsCategories = await _context.ProductCategories
                .Include(c => c.Sellers)
                .Include(p => p.Manufacturer)
                .Include(pc => pc.Products_Categories).ThenInclude(a => a.Product)
                .FirstOrDefaultAsync(n=>n.Id == id);

            return productsCategories;

        }
    }
}
