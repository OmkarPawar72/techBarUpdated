using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using techBar.Data;
using techBar.Data.Services;
using techBar.Models;

namespace techBar.Controllers
{
    public class ProductsCategoryController : Controller
    {
        private readonly IProductsCategoryService _service;

        public ProductsCategoryController(IProductsCategoryService productsCategoryService)
        {
            _service = productsCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var allProducts = await _service.GetAllAsync(n=>n.Sellers);
            return View(allProducts);
        }

        //GET: ProductsCategory/Details/1
        public async Task<IActionResult>Details(int id)
        {
            var productDetails = await _service.GetProductsCategoryIdAsysnc(id);
            return View(productDetails);
        }

        //GET : ProductsCategory/Create
        public async Task<IActionResult> Create()
        {
            var productDropdownData = await _service.GetNewProductsDropdownsValues();

            ViewBag.Seller = new SelectList(productDropdownData.Seller, "Id", "SellerName");
            ViewBag.Manufacturer = new SelectList(productDropdownData.Manufacturers, "Id", "FullName");
            ViewBag.Product = new SelectList(productDropdownData.Products, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewProductsCategoryVM productsCategoryVM)
        {
            if (!ModelState.IsValid)
            {
                var productDropdownData = await _service.GetNewProductsDropdownsValues();

                ViewBag.Seller = new SelectList(productDropdownData.Seller, "Id", "SellerName");
                ViewBag.Manufacturer = new SelectList(productDropdownData.Manufacturers, "Id", "FullName");
                ViewBag.Product = new SelectList(productDropdownData.Products, "Id", "FullName");

                return View(productsCategoryVM);
            }
            await _service.AddNewProductCategoryAsync(productsCategoryVM);
            return RedirectToAction(nameof(Index));
        }

        //GET : ProductsCategory/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var productCategoryDetails = await _service.GetProductsCategoryIdAsysnc(id);
            if (productCategoryDetails == null) return View("NotFound");



            var response = new NewProductsCategoryVM()
            {
                Id = productCategoryDetails.Id,  
                Name = productCategoryDetails.Name,
                Description = productCategoryDetails.Description,
                Price = productCategoryDetails.Price,
                ImageURL = productCategoryDetails.ImageURL,
                ProductCategory = productCategoryDetails.ProductCategory,
                SellerId = productCategoryDetails.SellerId,
                ManufacturerId = productCategoryDetails.ManufacturerId,
                ProductsIds = productCategoryDetails.Products_Categories.Select(n => n.ProductId).ToList(),
            };


            var productDropdownData = await _service.GetNewProductsDropdownsValues();

            ViewBag.Seller = new SelectList(productDropdownData.Seller, "Id", "SellerName");
            ViewBag.Manufacturer = new SelectList(productDropdownData.Manufacturers, "Id", "FullName");
            ViewBag.Product = new SelectList(productDropdownData.Products, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewProductsCategoryVM productsCategoryVM)
        {
            if (id != productsCategoryVM.Id)
            {
                View("NotFound");
            }

            if (!ModelState.IsValid)
            {
                var productDropdownData = await _service.GetNewProductsDropdownsValues();

                ViewBag.Seller = new SelectList(productDropdownData.Seller, "Id", "SellerName");
                ViewBag.Manufacturer = new SelectList(productDropdownData.Manufacturers, "Id", "FullName");
                ViewBag.Product = new SelectList(productDropdownData.Products, "Id", "FullName");

                return View(productsCategoryVM);
            }
            await _service.UpdateNewProductCategoryAsync(productsCategoryVM);
            return RedirectToAction(nameof(Index));
        }
    }
}
