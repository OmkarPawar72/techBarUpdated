using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using techBar.Data;
using techBar.Data.Services;

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

        //GET : Movies/Create
        public async Task<IActionResult> Create()
        {
            var productDropdownData = await _service.GetNewProductsDropdownsValues();

            ViewBag.Seller = new SelectList(productDropdownData.Seller, "Id", "SellerName");
            ViewBag.Manufacturer = new SelectList(productDropdownData.Manufacturers, "Id", "FullName");
            ViewBag.Product = new SelectList(productDropdownData.Products, "Id", "FullName");

            return View();
        }
    }
}
