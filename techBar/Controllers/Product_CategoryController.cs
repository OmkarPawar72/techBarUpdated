using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using techBar.Data;

namespace techBar.Controllers
{
    public class ProductsCategoryController : Controller
    {
        private readonly EcomDbContext _ecomDbContext;

        public ProductsCategoryController(EcomDbContext ecomDbContext)
        {
            _ecomDbContext = ecomDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var allProducts = await _ecomDbContext.ProductCategories.Include(n => n.Sellers).OrderBy(n => n.Name).ToListAsync();
            return View(allProducts);
        }
    }
}
