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

        public async Task AddNewProductCategoryAsync(NewProductsCategoryVM data)
        {
            var newProductsCategory = new ProductsCategory()
            {
                Name = data.Name,
                Description = data.Description,
                ImageURL = data.ImageURL,
                SellerId = (int)data.SellerId,
                StartDate = data.StartDate,
                EndDate= data.EndDate,
                ProductCategory=data.ProductCategory,
                ManufacturerId= (int)data.ManufacturerId
            };
            await _context.ProductCategories.AddAsync(newProductsCategory);
            await _context.SaveChangesAsync();

            //Add ProductsCategory Products 
            foreach(var productId in data.ProductsIds)
            {
                var newProductCategory = new Product_Category()
                {
                     CategoryId = newProductsCategory.Id,
                     ProductId= productId
                };
                await _context.Product_Categories.AddAsync(newProductCategory);
            }
            await _context.SaveChangesAsync();
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

        public async Task UpdateNewProductCategoryAsync(NewProductsCategoryVM data)
        {
            var dbProductCategory = await _context.ProductCategories.FirstOrDefaultAsync(n=>n.Id==data.Id);

            if(dbProductCategory != null)
            {

                dbProductCategory.Name = data.Name;
                dbProductCategory.Description = data.Description;
                dbProductCategory.ImageURL = data.ImageURL;
                dbProductCategory.SellerId = (int)data.SellerId;
                dbProductCategory.StartDate = data.StartDate;
                dbProductCategory.EndDate = data.EndDate;
                dbProductCategory.ProductCategory = data.ProductCategory;
                dbProductCategory.ManufacturerId = (int)data.ManufacturerId;
                await _context.SaveChangesAsync();
            }

            //Remove exesting Products Category
            var existingProductsCategoryDb = _context.Product_Categories.Where(n=>n.CategoryId==data.Id).ToList();
            _context.Product_Categories.RemoveRange(existingProductsCategoryDb);
            await _context.SaveChangesAsync();

            //Add ProductsCategory Products 
            foreach (var productId in data.ProductsIds)
            {
                var newProductCategory = new Product_Category()
                {
                    CategoryId = data.Id,
                    ProductId = productId
                };
                await _context.Product_Categories.AddAsync(newProductCategory);
            }
            await _context.SaveChangesAsync();
        }
    }
}
