﻿using Microsoft.EntityFrameworkCore;
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
                Price = data.Price,
                ImageURL = data.ImageURL,
                SellerId = data.SellerId,
                StartDate = data.StartDate,
                EndDate= data.EndDate,
                ProductCategory=data.ProductCategory,
                ManufacturerId= data.ManufacturerId
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

        public async Task<ProductsCategory> GetCategoryIdAsync(int id)
        {
            
            var productsCategories = await _context.ProductCategories
                .Include(c => c.Sellers)
                .Include(p => p.Manufacturer)
                .Include(pc => pc.Products_Categories).ThenInclude(a => a.Product)
                .FirstOrDefaultAsync(n=>n.Id == id);

            
            return productsCategories;

        }

        public async Task UpdateProductAsync(NewProductsCategoryVM data)
        {
            var dbCategory = await _context.ProductCategories.FirstOrDefaultAsync(n=>n.Id==data.Id);
            if (dbCategory != null)
            {
                dbCategory.Name = data.Name;
                dbCategory.Description = data.Description;
                dbCategory.ImageURL = data.ImageURL;
                dbCategory.SellerId = data.SellerId;
                dbCategory.StartDate = data.StartDate;
                dbCategory.EndDate = data.EndDate;
                dbCategory.ProductCategory = data.ProductCategory;
                dbCategory.ManufacturerId = data.ManufacturerId;
                await _context.SaveChangesAsync();
            }

            //Remove exesting products 

            var exestingPrdoductDb=_context.Product_Categories.Where(n=>n.ProductId==data.Id).ToList(); 
            _context.Product_Categories.RemoveRange(exestingPrdoductDb);
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
