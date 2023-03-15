using Microsoft.AspNetCore.Mvc;
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
    }
}
