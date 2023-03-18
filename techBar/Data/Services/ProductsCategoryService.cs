using Microsoft.EntityFrameworkCore;
using techBar.Data.Base;
using techBar.Data.Enums;
using techBar.Data.ViewModels;
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

        public async Task<NewProductsDropdownVM> GetNewProductsDropdownsValues()
        {
            var response = new NewProductsDropdownVM()
            {
                Products = await _context.Products.OrderBy(n => n.FullName).ToListAsync(),
                Seller = await _context.Sellers.OrderBy(n => n.SellerName).ToListAsync(),
                Manufacturers = await _context.Manufacturers.OrderBy(n => n.FullName).ToListAsync()
        };

            return response;
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
